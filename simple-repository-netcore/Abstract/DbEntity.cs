using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DevNetCore.SimpleRepository.Abstract
{
    /// <summary>
    /// Abstract database entity
    /// </summary>
    public abstract class DbEntity
    {
        [Key]
        [Column(DefaultColumnName.Id, Order = 0)]
        public Guid Id { get; set; }

        [Column(DefaultColumnName.CreatedBy, Order = 1000)]
        public string? CreatedBy { get; set; }

        [Column(DefaultColumnName.CreatedDate, Order = 1001)]
        public DateTimeOffset CreatedDate { get; set; }

        [Column(DefaultColumnName.LastUpdateBy, Order = 1002)]
        public string? LastUpdateBy { get; set; }

        [Column(DefaultColumnName.LastUpdateDate, Order = 1003)]
        public DateTimeOffset LastUpdateDate { get; set; }

        protected DbEntity()
        {
            this.Id = Guid.NewGuid();
            this.CreatedBy = string.Empty;
            this.CreatedDate = DateTimeOffset.Now;
        }
    }
}