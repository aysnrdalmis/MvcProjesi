//Veritabanı context sınıfımızı referans veriyoruz
using MvcProjesi.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace MvcProjesi
{
    public class MvcApplication : System.Web.HttpApplication
    {
        //Uygulama ilk başlatıldığında, buradaki metod çalışacak.
        protected void Application_Start()
        {
            //Burada veritabanı sınıfımızdan, bir nesne oluşturuyoruz. using kullanmamızın sebebi,
            //db nesnesinin işi bittiğinde, silinmesini ve hafızada yer tutmamasını sağlamak.
            using (MvcProjesiContext db = new MvcProjesiContext())
            {
                //Bu metod, eğer veritabanımız oluşturulmamış ise, oluşturulmasını sağlıyor.
                db.Database.CreateIfNotExists();

                //Veritabanındaki makalelerin, yorumların, üyelerin ve etiketlerin adetini alıyoruz.
                int makaleAdet = (from i in db.Posts select i).Count();
                int yorumAdet = (from i in db.Comments select i).Count();
                int uyeAdet = (from i in db.Members select i).Count();
                int etiketAdet = (from i in db.Labels select i).Count();

                //Veritabanına, sürekli aynı makalelerin ve yorumların eklenmemesi için
                //en az 5 adet makale ve yorum var mı diye kontrol ediyoruz.
                //Ayrıca sistemde en az 1 üye olduğunu da onaylıyoruz.
                //Bununla birlikte en az 10 adet etiket olduğunu da onaylıyoruz.
                if (makaleAdet < 5 || yorumAdet < 5 || uyeAdet < 1 || etiketAdet < 10)
                {
                    //Bir tane örnek üye oluşturuyoruz.
                    Member uye = new Member() { Name = "Ayşenur", Surname = "Dalmış", EMail = "aysenur@ybu.edu.tr", ImageUrl = "", MembershipDate = DateTime.Now, WebSite = "", Password = "12345" };

                    db.Members.Add(uye);

                    //Makalelerimizi oluşturuyoruz. Ayrıca makalelerin, yukarıda oluşturduğumuz kullanıcı 
                    //tarafından oluşturulduğunu gösteriyoruz.
                    Post makale1 = new Post() { Title = "Post Title 1", Topic = "Post Topic 1", Date = DateTime.Now, Member = uye };
                    Post makale2 = new Post() { Title = "Post Title 2", Topic = "Post Topic 2", Date = DateTime.Now, Member = uye };
                    Post makale3 = new Post() { Title = "Post Title 3", Topic = "Post Topic 3", Date = DateTime.Now, Member = uye };
                    Post makale4 = new Post() { Title = "Post Title 4", Topic = "Post Topic 4", Date = DateTime.Now, Member = uye };
                    Post makale5 = new Post() { Title = "Post Title 5", Topic = "Post Topic 5", Date = DateTime.Now, Member = uye };
                    Post makale6 = new Post() { Title = "Post Title 6", Topic = "Post Topic 6", Date = DateTime.Now, Member = uye };

                    //Makaleleri eklemek için komutumuzu veriyoruz.
                    //SaveChanges() komutu gelene kadar veritabanına kayıt yapılmayacak.
                    db.Posts.Add(makale1);
                    db.Posts.Add(makale2);
                    db.Posts.Add(makale3);
                    db.Posts.Add(makale4);
                    db.Posts.Add(makale5);
                    db.Posts.Add(makale6);

                    //Yorumlarımızı oluşturuyoruz. Ayrıca yorumların, yukarıda oluşturduğumuz kullanıcı 
                    //tarafından oluşturulduğunu gösteriyor, ayrıca makalelerimize de bağlıyoruz.
                    Comment yorum1 = new Comment() { Topic = "Comment for Post 1", Date = DateTime.Now, Post = makale1, Member = uye };
                    Comment yorum2 = new Comment() { Topic = "Comment for Post 2", Date = DateTime.Now, Post = makale1, Member = uye };
                    Comment yorum3 = new Comment() { Topic = "Comment for Post 3", Date = DateTime.Now, Post = makale1, Member = uye };
                    Comment yorum4 = new Comment() { Topic = "Comment for Post 4", Date = DateTime.Now, Post = makale1, Member = uye };
                    Comment yorum5 = new Comment() { Topic = "Comment for Post 5", Date = DateTime.Now, Post = makale1, Member = uye };
                    Comment yorum6 = new Comment() { Topic = "Comment for Post 6", Date = DateTime.Now, Post = makale1, Member = uye };

                    //Yorumları eklemek için komutumuzu veriyoruz.
                    //SaveChanges() komutu gelene kadar veritabanına kayıt yapılmayacak.
                    db.Comments.Add(yorum1);
                    db.Comments.Add(yorum2);
                    db.Comments.Add(yorum3);
                    db.Comments.Add(yorum4);
                    db.Comments.Add(yorum5);
                    db.Comments.Add(yorum6);

                    //Etiketlerimizi oluşturuyoruz. Ayrıca etiketleri, kullanıldığı makalelerimize de bağlıyoruz.
                    Label etiket1 = new Label() { Topic = "Asp.Net", Posts = new List<Post>() { makale1, makale2, makale3, makale4, makale6 } };
                    Label etiket2 = new Label() { Topic = "PHP", Posts = new List<Post>() { makale5, makale3, makale2, makale1 } };
                    Label etiket3 = new Label() { Topic = "Java", Posts = new List<Post>() { makale2, makale4, makale5 } };
                    Label etiket4 = new Label() { Topic = "C#", Posts = new List<Post>() { makale5, makale4 } };
                    Label etiket5 = new Label() { Topic = "Ruby", Posts = new List<Post>() { makale5, makale6 } };
                    Label etiket6 = new Label() { Topic = "C++", Posts = new List<Post>() { makale5, makale2 } };
                    Label etiket7 = new Label() { Topic = "D", Posts = new List<Post>() { makale5, makale1 } };
                    Label etiket8 = new Label() { Topic = "Phyton", Posts = new List<Post>() { makale1, makale4 } };

                    Label etiket9 = new Label() { Topic = "JSF", Posts = new List<Post>() { makale5, makale4 } };
                    Label etiket10 = new Label() { Topic = "JSP", Posts = new List<Post>() { makale5, makale3, makale6 } };
                    Label etiket11 = new Label() { Topic = "XCode", Posts = new List<Post>() { makale5, makale4, makale1 } };
                    Label etiket12 = new Label() { Topic = "ColdFusion", Posts = new List<Post>() { makale5, makale2 } };
                    Label etiket13 = new Label() { Topic = "Pascal", Posts = new List<Post>() { makale5, makale1, makale3 } };
                    Label etiket14 = new Label() { Topic = "Cobol", Posts = new List<Post>() { makale5, makale4, makale3, makale1, makale2 } };

                    //Etiketleri eklemek için komutumuzu veriyoruz.
                    //SaveChanges() komutu gelene kadar veritabanına kayıt yapılmayacak.
                    db.Labels.Add(etiket1);
                    db.Labels.Add(etiket2);
                    db.Labels.Add(etiket3);
                    db.Labels.Add(etiket4);
                    db.Labels.Add(etiket5);
                    db.Labels.Add(etiket6);
                    db.Labels.Add(etiket7);
                    db.Labels.Add(etiket8);
                    db.Labels.Add(etiket9);
                    db.Labels.Add(etiket10);
                    db.Labels.Add(etiket11);
                    db.Labels.Add(etiket12);
                    db.Labels.Add(etiket13);
                    db.Labels.Add(etiket14);

                    //Son olarak da yaptığımız eklemelerin, veritabanına yansıtılmasını
                    //sağlamak için kaydet komutu veriyoruz.
                    db.SaveChanges();

                }
            }
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
}
