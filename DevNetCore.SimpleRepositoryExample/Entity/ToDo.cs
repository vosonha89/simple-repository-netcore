using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DevNetCore.SimpleRepository.Abstract;

namespace DevNetCore.SimpleRepositoryExample.Entity
{
    [Table("ToDo")]
    public partial class ToDo : DbEntity
    {
        [Column("description")] [MaxLength(500)] public string? Description { get; set; }

        [Column("completed")] public bool Completed { get; set; }

        [Column("userId")] public long UserId { get; set; }
        [Column("refId")] public long RefId { get; set; }
    }
}