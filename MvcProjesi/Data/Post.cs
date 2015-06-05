using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MvcProjesi.Data
{
    public class Post
    {
        public int PostId { get; set; }

        [Required(ErrorMessage = "Please enter the post title. ")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "It must be between 3-50 character. ")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Please enter the post topic.")]
        //Girilen metnin, html formatında girilmesini sağlıyoruz.
        [DataType(DataType.Html, ErrorMessage = "Please enter html format. ")]
        public string Topic { get; set; }

        [Required(ErrorMessage = "Please enter the date. ")]
        [DataType(DataType.DateTime, ErrorMessage = "Please enter a valid format. ")]
        public DateTime Date { get; set; }

        public virtual Member Member { get; set; }

        //Bir makalede, birden çok yorum bulunabilir.
        public virtual List<Comment> Comments { get; set; }

        //Bir makale de, birden çok etiket bulunabilir.
        public virtual List<Label> Labels { get; set; }
    }
}