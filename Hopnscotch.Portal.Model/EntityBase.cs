using System.ComponentModel.DataAnnotations;

namespace Hopnscotch.Portal.Model
{
    public abstract class EntityBase
    {
        [Key]
        public int Id { get; set; }
    }
}