using DevNetCore.SimpleRepository.Interface;
using DevNetCore.SimpleRepositoryExample;
using DevNetCore.SimpleRepositoryExample.Entity;
using DevNetCore.SimpleRepositoryExample.ExternalRequest.ToDo;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

string connectionString =
    builder.Configuration.GetConnectionString("ExampleDatabase")
    ?? throw new InvalidOperationException("Connection string"
                                           + "'ExampleDatabase' not found.");
Console.WriteLine($"Connection string: {connectionString}");

builder.Services.AddHttpContextAccessor();
builder.Services.AddDbContext<SimpleRepositoryExampleContext>(options => options.UseNpgsql(connectionString));
builder.Services.AddScoped<ISimpleRepository, SimpleRepositoryExample>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapGet("/getTodoList", async (ISimpleRepository repository) =>
    {
        List<ToDo> todos = await repository.GetList<ToDo>(a => a.Id != Guid.Empty).ToListAsync();
        return todos;
    })
    .WithName("Get all todo")
    .WithOpenApi();

app.MapGet("/getTodo", async (HttpContext context, ISimpleRepository repository) =>
    {
        string? id = context.Request.Query["id"];
        if (id == null) id = string.Empty;
        ToDo? todo = await repository.Get<ToDo>(a => a.Id == new Guid(id));
        return todo;
    })
    .WithName("Get single todo")
    .WithOpenApi();

app.MapGet("/delete", async (HttpContext context, ISimpleRepository repository) =>
    {
        string? id = context.Request.Query["id"];
        if (id == null) id = string.Empty;
        ToDo? todo = await repository.Get<ToDo>(a => a.Id == new Guid(id));
        if (todo != null)
        {
            repository.Delete(todo);
            await repository.SaveChanges();
            return Results.Ok(true);
        }
        else
        {
            return Results.NotFound(false);
        }
    })
    .WithName("Delete single todo")
    .WithOpenApi();

app.MapGet("/syncTodos", async (ISimpleRepository repository) =>
    {
        List<ToDo> returnData = new List<ToDo>();
        ToDoSync toDoSync = new ToDoSync(new HttpClient());
        List<TodoItem> todoItems = await toDoSync.SyncData();
        if (todoItems.Count > 0)
        {
            for (int i = 0; i < todoItems.Count; i++)
            {
                try
                {
                    ToDo? todo = await repository.Get<ToDo>(a => a.RefId == todoItems[i].Id);
                    if (todo != null)
                    {
                        todo.RefId = todoItems[i].Id;
                        todo.LastUpdateBy = "System";
                        todo.LastUpdateDate = DateTime.UtcNow;
                        repository.Update(todo);
                    }
                    else
                    {
                        todo = new ToDo
                        {
                            Description = todoItems[i].TodoData,
                            UserId = todoItems[i].UserId,
                            RefId = todoItems[i].Id,
                            CreatedBy = "System",
                            LastUpdateBy = String.Empty,
                            LastUpdateDate = DateTime.UtcNow
                        };
                        await repository.Insert(todo);
                    }

                    await repository.SaveChanges();
                    returnData.Add(todo);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
            }
        }

        return returnData;
    })
    .WithName("Get all todos")
    .WithOpenApi();
app.Run();