using System.ComponentModel.DataAnnotations.Schema;

namespace NetCore.SimpleRepository.Abstract
{
    /// <summary>
    /// Abstract database entity
    /// </summary>
    public abstract class DbEntity
    {
        [Column(Order = 0, TypeName = DefaultColumnName.Id)]
        public Guid Id { get; set; }

        [Column(Order = 1000, TypeName = DefaultColumnName.CreatedBy)]
        public string? CreatedBy { get; set; }

        [Column(Order = 1001, TypeName = DefaultColumnName.CreatedDate)]
        public DateTimeOffset CreatedDate { get; set; }

        [Column(Order = 1002, TypeName = DefaultColumnName.LastUpdateBy)]
        public string? LastUpdateBy { get; set; }

        [Column(Order = 1003, TypeName = DefaultColumnName.LastUpdateDate)]
        public DateTimeOffset LastUpdateDate { get; set; }
    }
}
