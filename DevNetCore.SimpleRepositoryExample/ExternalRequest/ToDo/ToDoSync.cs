using System.Text.Json.Serialization;

namespace DevNetCore.SimpleRepositoryExample.ExternalRequest.ToDo;

public class ToDoSync
{
    private readonly HttpClient _httpClient;
    
    public ToDoSync(HttpClient httpClient)
    {
        this._httpClient = httpClient;
    }

    /// <summary>
    /// Sync data example
    /// </summary>
    /// <returns></returns>
    public async Task<List<TodoItem>> SyncData()
    {
        using HttpResponseMessage response = await this._httpClient.GetAsync("https://dummyjson.com/todos");
        response.EnsureSuccessStatusCode();
        var jsonResponse = await response.Content.ReadAsStringAsync();
        Console.WriteLine($"{jsonResponse}\n");
        TodoResponse toDoResponse = TodoResponse.FromJson(jsonResponse);
        return toDoResponse.Todos;
    }
}