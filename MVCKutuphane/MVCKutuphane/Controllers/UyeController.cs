using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVCKutuphane.Models.Entity;

namespace MVCKutuphane.Controllers
{
    public class UyeController : Controller
    {
        // GET: Uye
        public ActionResult AnaUye()
        {
            var x = Baglanti.db.Uyeler.ToList();
            return View(x);
        }
        [HttpGet]
        public ActionResult YeniUye()
        {
            return View();
        }
        [HttpPost]
        public ActionResult YeniUye(Uyeler u)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            Baglanti.db.Uyeler.Add(u);
            Baglanti.db.SaveChanges();
            return RedirectToAction("AnaUye");
        }
        public ActionResult UyeSil(byte id)
        {
            var x = Baglanti.db.Uyeler.Find(id);
            Baglanti.db.Uyeler.Remove(x);
            Baglanti.db.SaveChanges();
            return RedirectToAction("AnaUye");
        }
        public ActionResult UyeGetir(byte id)
        {
            var x = Baglanti.db.Uyeler.Find(id);
            return View("UyeGetir", x);
        }
        public ActionResult UyeGuncelle(Uyeler u)
        {
            if (!ModelState.IsValid)
            {
                return View("UyeGetir");
            }
            var x = Baglanti.db.Uyeler.Find(u.ID);
            x.Uye = u.Uye;
            x.Mail = u.Mail;
            x.Kullanici = u.Kullanici;
            x.Sifre = u.Sifre;
            x.Telefon = u.Telefon;
            x.Okul = u.Okul;
            x.Fotograf = u.Fotograf;
            return RedirectToAction("AnaUye");
        }
        public ActionResult UyeGecmis(byte id)
        {
            var gecmis = Baglanti.db.Hareketler.Where(c => c.Uye == id).ToList();
            var y = Baglanti.db.Uyeler.Where(c => c.ID == id).FirstOrDefault();
            ViewBag.uye = y.Uye;
            return View(gecmis);
        }
    }
}