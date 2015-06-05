using System;
using System.Collections.Generic;

//Bu isim uzayını eklememizin sebebi, [DataType],[StringLength] gibi Attribute'leri tanımlıyor olmamız.
using System.ComponentModel.DataAnnotations;

using System.Linq;
using System.Web;

namespace MvcProjesi.Data
{
    public class Member
    {
        //Bu alan tablonun id'si olacak. Tablo adının sonuna Id takısı eklendiğinde, framework bu alanı otomatik olarak, 
        //tablonun anahtar sütunu yani primary key'si yapacaktır. Bu varsayılanı değiştirmek mümkündür, ancak bu konuya 
        //girmeyeceğiz. Detaylı bilgi için, Code First Fluent olarak araştırma yapabilirsiniz.
        public int MemberId { get; set; }

        //Bu alanı zorunlu hale getiriyoruz. Böylelikle boş geçilemeyecek.
        [Required(ErrorMessage = "Please enter your name. ")]
        //Girilen metnin uzunluğunu belirtiyoruz. İlk değişken minimum uzunluk olurken, sonrakiler ise, opsiyonel 
        //girdilerdir.
        [StringLength(50, MinimumLength = 3, ErrorMessage = "It must be between 3-50 character. ")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Please enter your surname. ")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "It must be between 3-50 character. ")]
        public string Surname { get; set; }

        [Required(ErrorMessage = "Please enter your e-mail address. ")]
        //Girilen metnin, geçerli bir e-posta adresi formatında girilmesini sağlıyoruz. 
        //DataType tipleri, Microsoft tarafından Framework'e eklenen hazır tiplerdir.
        [DataType(DataType.EmailAddress, ErrorMessage = "Please enter a valid format. ")]
        public string EMail { get; set; }

        //Required Attribute'si eklemediğimiz için, bu alan zorunlu olmayacak ve boş geçilebiliyor olacak.
        //Girilen metnin, geçerli bir web sitesi adresi formatında girilmesini sağlıyoruz. 
        [DataType(DataType.Url, ErrorMessage = "Please enter a valid format. ")]
        public string WebSite { get; set; }

        //Girilen metnin, geçerli bir resim yolu formatında girilmesini sağlıyoruz. 
        [DataType(DataType.ImageUrl, ErrorMessage = "Please enter a valid format. ")]
        public string ImageUrl { get; set; }


        [Required(ErrorMessage = "Please enter your membership date. ")]
        //Girilen tarihin, geçerli bir tarih ve saat formatında girilmesini sağlıyoruz.
        [DataType(DataType.DateTime, ErrorMessage = "Please enter a valid format. ")]
        public DateTime MembershipDate { get; set; }

        //Bir üyenin, birden çok yorumu olabileceği için Yorumları bir liste içerisine alıyoruz. 
        //Property adının sonuna s takısı koymamızın sebebi, veri tipinin çoğul olduğunun daha kolay anlaşılabilmesi 
        //içindir. Entity varsayılan olarak, liste verilerin sonuna s takısı koymaktadır.
        //Biz de bu standarda uyum gösterdik. Eğer veri tipinin adı zaten s ile bitiyorsa o zaman da s yi siliyoruz.
        //Örneğin hayvanlarla ilgili bir tabloda, Kus adlı bir sınıfımız çoğul olarak temsil edilecekse Ku olarak 
        //kullanabiliriz.
        public virtual List<Comment> Comments { get; set; }

        //Bir üyenin eklediği, birden çok makale olabilir.
        public virtual List<Post> Posts { get; set; }

        //Baştan eklemeyi unuttuğumuz şifre kısmını ekledik.
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Please enter your password. ")]
        public string Password { get; set; }
    }
}