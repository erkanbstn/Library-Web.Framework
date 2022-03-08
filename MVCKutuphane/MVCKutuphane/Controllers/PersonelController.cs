using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVCKutuphane.Models.Entity;

namespace MVCKutuphane.Controllers
{
    public class PersonelController : Controller
    {
        // GET: Personel
        public ActionResult AnaPersonel()
        {
            var x = Baglanti.db.Personeller.ToList();
            return View(x);
        }
        public ActionResult PersonelSil(byte id)
        {
            var x = Baglanti.db.Personeller.Find(id);
            Baglanti.db.Personeller.Remove(x);
            Baglanti.db.SaveChanges();
            return RedirectToAction("AnaPersonel");
        }
        [HttpGet]
        public ActionResult YeniPersonel()
        {
            return View();
        }
        [HttpPost]
        public ActionResult YeniPersonel(Personeller p)
        {
            if (!ModelState.IsValid)
            {
                return View("YeniPersonel");
            }
            Baglanti.db.Personeller.Add(p);
            Baglanti.db.SaveChanges();
            return RedirectToAction("AnaPersonel");
        }
        public ActionResult PersonelGetir(byte id)
        {
            var x = Baglanti.db.Personeller.Find(id);
            return View("PersonelGetir", x);
        }
        public ActionResult PersonelGuncelle(Personeller p)
        {
            if (!ModelState.IsValid)
            {
                return View("PersonelGetir");
            }
            var x = Baglanti.db.Personeller.Find(p.ID);
            x.Personel = p.Personel;
            Baglanti.db.SaveChanges();
            return RedirectToAction("AnaPersonel");
        }
    }
}