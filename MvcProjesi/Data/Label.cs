using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MvcProjesi.Data
{
    public class Label
    {
        public int LabelId { get; set; }

        [Required(ErrorMessage = "Please enter the topic of label. ")]
        [StringLength(50, ErrorMessage = "Label can't be longer than 50 character. ")]
        public string Topic { get; set; }

        //Aynı etiket, birden çok makale de kullanılıyor olabilir.
        public virtual List<Post> Posts { get; set; }
    }
}