using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using MVCKutuphane.Models.Entity;

namespace MVCKutuphane.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(Uyeler u)
        {
            var uye = Baglanti.db.Uyeler.FirstOrDefault(c => c.Kullanici == u.Kullanici && c.Sifre == u.Sifre);
            if (uye != null)
            {
                FormsAuthentication.SetAuthCookie(uye.Mail, false);
                Session["ID"] = uye.ID;
                Session["Ad"] = uye.Uye;
                Session["Sifre"] = uye.Sifre;
                Session["Kullanici"] = uye.Kullanici;
                Session["Okul"] = uye.Okul;
                Session["Mail"] = uye.Mail;
                return RedirectToAction("Panel", "Panelim");
            }
            else
            {
                return View();
            }
        }

        [HttpGet]
        public ActionResult KayitOl()
        {
            return View();
        }
        [HttpPost]
        public ActionResult KayitOl(Uyeler u)
        {
            Baglanti.db.Uyeler.Add(u);
            Baglanti.db.SaveChanges();
            return View();
        }
        [HttpGet]
        public ActionResult PLogin()
        {
            return View();
        }
        [HttpPost]
        public ActionResult PLogin(Personeller p)
        {
            var personel = Baglanti.db.Personeller.FirstOrDefault(c => c.Personel == p.Personel && c.Sifre == p.Sifre);
            if (personel != null)
            {
                return RedirectToAction("AnaKategori", "Kategori");
            }
            else
            {
                return View();
            }
        }
        public ActionResult SiteyeDon()
        {
            return View();
        }
    }
}