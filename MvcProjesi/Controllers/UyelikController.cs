using MvcProjesi.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcProjesi.Controllers
{
    public class UyelikController : Controller
    {
        public ActionResult Register()
        {
            return View();
        }

        //Formumuzun geliş metodu Post
        //Dikkat ederseniz aynı isimli, iki adet Action var.
        //Üsttekinin metodu boş olduğu için Get oluyor.
        //Alttakinin üzerinde [HttpPost] olduğu için metodu Post oluyor.
        //Burada eğer sayfa içinden bir form gönderimi yapılmışsa, Post olan Action çağrılır.
        //Normal adres üzerinden sayfaya talepte bulunulursa, Get metodlu olan çağrılır.
        [HttpPost]
        public ActionResult Register(Member model, string textBoxBirth, HttpPostedFileBase file)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            if (String.IsNullOrEmpty(textBoxBirth))
            {
                //Burada Uye modelimizde olmayan bir elemanla çalıştığımız için, kendimiz elle hata
                //mesajını, sayfadaki hata listesine (@Html.ValidationSummary()) ekliyoruz.
                ModelState.AddModelError("textBoxBirth", "It can't be blank.");

                //Hata oluşması halinde sayfayı tekrar yüklüyoruz.
                //Böylelikle otomatik olarak hatalar sayfada gösteriliyor.
                return View();
            }
            int year = int.Parse(textBoxBirth.Substring(6));
            if (year > 2002)
            {
                ModelState.AddModelError("textBoxBirth", "Your age can't be smaller than 12.");
                return View();
            }
            Member uye = new Member();
            if (file != null)
            {
                //Sunucuya dosya kaydedilirken, sunucunun dosya sistemini, yolunu bilemeyeceğimiz için
                //Server.MapPath() ile sitemizin ana dizinine gelmiş oluruz. Devamında da sitemizdeki
                //yolu tanımlarız.
                file.SaveAs(Server.MapPath("~/Images/") + file.FileName);
                uye.ImageUrl = "/Images/" + file.FileName;
            }
            uye.Name = model.Name;
            uye.EMail = model.EMail;
            uye.Surname = model.Surname;
            uye.MembershipDate = DateTime.Now;
            uye.WebSite = model.WebSite;
            uye.Password = model.Password;
            using (MvcProjesiContext db = new MvcProjesiContext())
            {
                db.Members.Add(uye);
                db.SaveChanges();

                //İşlemimiz başarıyla biterse, başarılı olduğuna dair bir sayfaya yönlendiriyoruz.
                return RedirectToAction("SuccessfullyMembership");
            }
        }

        public ActionResult SuccessfullyMembership()
        {
            return View();
        }

        public ActionResult LogIn()
        {
            return View();
        }

        [HttpPost]
        public string LoggIn()
        {
            //Request.Form["html elementinin name özelliği"] ile Post edilen formdaki elemanların
            //değerlerini alabiliyoruz. Bu metod yalnızca Post ile çalışır.
            string posta = Request.Form["txtPosta"];
            string sifre = Request.Form["pswSifre"];
            if (String.IsNullOrEmpty(posta) && String.IsNullOrEmpty(sifre))
            {
                return "You must enter an email address and a password";
            }
            else if (String.IsNullOrEmpty(posta))
            {
                return "You must enter an email address.";
            }
            else if (string.IsNullOrEmpty(sifre))
            {
                return "You must enter a password.";
            }
            else
            {
                using (MvcProjesiContext db = new MvcProjesiContext())
                {
                    //Normalde şifreyi hashleyerek yazdırmamız ve kontrol etmemiz gerekir.
                    var uye = (from i in db.Members where i.Password == sifre && i.EMail == posta select i).SingleOrDefault();

                    if (uye == null) return "Your email address/password is wrong.";

                    //Session'da müşteri ile ilgili bilgileri saklamaktayız.
                    //Güvenlik açısından bilgileri şifreleyerek saklamamız daha doğru bir yöntemdir.
                    //Asp.Net Membership yapısı, bu güvenliği sunmaktadır.
                    Session["uyeId"] = uye.MemberId;

                    //Burada eğer, kullanıcı bilgileri, sistemde eşleşirse, geriye girişin başarılı
                    //olduğuna dair bir mesaj ve 3 saniye sonra, ana sayfaya yönlendirecek bir
                    //javascript kodu ekliyoruz.
                    return "You enter the system successfully.<script type='text/javascript'>setTimeout(function(){window.location='/'},3000);</script>";
                }
            }
        }
    }
}