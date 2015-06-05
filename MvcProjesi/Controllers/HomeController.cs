using MvcProjesi.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcProjesi.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/
        public ActionResult Index()
        {
            return View();
        }

        //Son 5 makalenin ana sayfaya yükleneceği Action
        public ActionResult LastFivePost()
        {
            //Veritabanından yeni bir nesne oluşturuyoruz.
            MvcProjesiContext db = new MvcProjesiContext();

            //Veritabanından sorgulamayı Linq ile yapıyoruz.
            //Tarih sırasına göre son makaleleri OrderByDescending ile çekip Take ile de 5 tane almasını istiyoruz.
            List<Post> postList = db.Posts.OrderByDescending(i => i.Date).Take(5).ToList();

            //Normal içeriklerde View döndürürken, burada ise PartialView döndürüyoruz.
            //Ayrıca makaleListe nesnesini de View'de kullanacağımız şekilde model olarak aktarıyoruz.
            return PartialView(postList);
        }

        //Son 5 yorumun ana sayfaya yükleneceği Action
        public ActionResult LastFiveComment()
        {
            MvcProjesiContext db = new MvcProjesiContext();

            //Tarih sırasına göre son makaleleri OrderByDescending ile çekip Take ile de 5 tane almasını istiyoruz.
            List<Comment> commentList = db.Comments.OrderByDescending(i => i.Date).Take(5).ToList();

            //Ayrıca yorumListe nesnesini de View'de kullanacağımız şekilde model olarak aktarıyoruz.
            return PartialView(commentList);
        }

        //En çok kullanılan 5 etiketin ana sayfaya yükleneceği Action
        public ActionResult PopularTenLabel()
        {
            MvcProjesiContext db = new MvcProjesiContext();

            //Etiketleri sorgularken, kaç adet makaleye bağlandığını bulup, ona göre yüksekten,
            //aşağı doğru sıralanmasını sağlıyoruz. Gelen sonuçtan 10 adet alıp, listeye ekliyoruz.
            List<Label> labelList = (from i in db.Labels orderby i.Posts.Count() descending select i).Take(10).ToList();

            //Ayrıca etiketListe nesnesini de View'de kullanacağımız şekilde model olarak aktarıyoruz.
            return PartialView(labelList);
        }

        public ActionResult AllPosts()
        {
            MvcProjesiContext db = new MvcProjesiContext();

            //Tüm makalelerimizi, tarih sırasına göre, büyükten küçüğe olmak üzere çekiyoruz.
            List<Post> postList = (from i in db.Posts orderby i.Date descending select i).ToList();
            return View(postList);
        }

        public ActionResult AllComments()
        {
            MvcProjesiContext db = new MvcProjesiContext();
            List<Comment> commentList = (from i in db.Comments orderby i.Date descending select i).ToList();
            return View(commentList);
        }

        public ActionResult Post_Label(int etiketId)
        {
            MvcProjesiContext db = new MvcProjesiContext();
            var tempList = (from i in db.Labels where i.LabelId == etiketId select i.Posts).ToList();

            //Burada veri içiçe liste halinde geldiği için, içerideki listeyi [0] indexi ile alıp gönderiyoruz.
            return View(tempList[0]);
        }

        public ActionResult PostDetail(int makaleId)
        {
            MvcProjesiContext db = new MvcProjesiContext();

            //Burada verilen id numarasına göre seçili makaleyi alıyoruz.
            Post makale = (from i in db.Posts where i.PostId == makaleId select i).SingleOrDefault();
            return View(makale);
        }
        public ActionResult Post_Comment(int yorumId)
        {
            MvcProjesiContext db = new MvcProjesiContext();

            //Burada verilen yorumId numarasına göre ait olduğu makaleyi alıyoruz.
            Post makale = (from i in db.Comments where i.CommentId==yorumId select i.Post).SingleOrDefault();
            return View(makale);
        }
    }
}