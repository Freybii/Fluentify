using System.ComponentModel.DataAnnotations;

namespace Fluentify.Web.Areas.Identity.Data
{
    public class FluentifyTask
    {
        [Key]
        public int Id { get; set; }

        public int TaskId { get; set; }

        public int UnitId { get; set; }

        public string audio_path { get; set; }

        public string Text { get; set; }

        public string Words { get; set; }

        public string CorrectWords { get; set; }
    }
}
