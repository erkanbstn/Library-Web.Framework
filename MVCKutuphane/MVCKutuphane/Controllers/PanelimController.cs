using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using MVCKutuphane.Models.Entity;

namespace MVCKutuphane.Controllers
{
    public class PanelimController : Controller
    {
        // GET: Panelim
        [Authorize]
        [HttpGet]
        public ActionResult Panel()
        {
            var x = (string)Session["Kullanici"];
            var deger = Baglanti.db.Uyeler.FirstOrDefault(c => c.Kullanici == x);
            return View(deger);
        }
        [HttpPost]
        public ActionResult Panel(Uyeler u)
        {
            var x = (string)Session["Sifre"];
            var x2 = Baglanti.db.Uyeler.FirstOrDefault(c => c.Sifre == x);
            x2.Sifre = u.Sifre;
            x2.Okul = u.Okul;
            x2.Kullanici = u.Kullanici;
            x2.Uye = u.Uye;
            x2.Mail = u.Mail;
            Baglanti.db.SaveChanges();
            return View();
        }
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login","Login");
        }
    }
}