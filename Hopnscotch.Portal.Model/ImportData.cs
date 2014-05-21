using System.ComponentModel.DataAnnotations;

namespace Hopnscotch.Portal.Model
{
    public class ImportData
    {
        public int Id { get; set; }
        public string ResponseType { get; set; }

        [MaxLength]
        public string ResponseData { get; set; }
    }
}