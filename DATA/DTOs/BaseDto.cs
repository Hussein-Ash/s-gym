using System.ComponentModel.DataAnnotations;

namespace EvaluationBackend.DATA.DTOs
{
    public class BaseDto<TId>
    {
        [Key]
        public TId Id { get; set; }

        public bool Deleted { get; set; } = false;
        public DateTime? CreationDate { get; set; } = DateTime.UtcNow;
    }
}