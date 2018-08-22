using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Chess_GEMA.Models
{
    public class File
    {
        public int Id { get; set; }

        [Required]
        [StringLength(255)]
        public string Name { get; set; }

        public string Owner { get; set; }

        public long Size { get; set; }

        public string Content { get; set; }

        public string Description { get; set; }

        public string Format { get; set; }

        public string Path { get; set; }
    }
}