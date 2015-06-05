using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MvcProjesi.Data
{
    public class Comment
    {
        public int CommentId { get; set; }

        [Required(ErrorMessage = "Please enter your comment. ")]
        public string Topic { get; set; }

        [Required(ErrorMessage = "Please enter the date. ")]
        [DataType(DataType.DateTime, ErrorMessage = "Please enter a valid format. ")]
        public DateTime Date { get; set; }

        //Her yorum, yalnızca bir makaleye ait olabilir. Bu yüzden, tek bir makaleye bağlıyoruz. 
        //Dikkat edileceği üzere veri türü (burada aynı zamanda sınıf) olarak Makale yazılıyor.
        public virtual Post Post { get; set; }

        //Her yorumu, yalnızca bir kişi yazmış olabilir.
        public virtual Member Member { get; set; }
    }
}