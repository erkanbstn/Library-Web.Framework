using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVCKutuphane.Models.Entity;
namespace MVCKutuphane.Controllers
{
    public class YazarController : Controller
    {
        // GET: Yazar
        public ActionResult AnaYazar()
        {
            var x = Baglanti.db.Yazarlar.ToList();
            return View(x);
        }
        public ActionResult YazarKitaplar(byte id)
        {
            var x = Baglanti.db.Kitaplar.Where(c => c.Yazar == id).ToList();
            var y = Baglanti.db.Yazarlar.Where(c => c.ID == id).FirstOrDefault();
            ViewBag.yzr = y.Yazar;
            return View(x);
        }
        [HttpGet]
        public ActionResult YeniYazar()
        {
            return View();
        }
        [HttpPost]
        public ActionResult YeniYazar(Yazarlar y)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            Baglanti.db.Yazarlar.Add(y);
            Baglanti.db.SaveChanges();
            return RedirectToAction("AnaYazar");
        }
        public ActionResult YazarSil(byte id)
        {
            var x = Baglanti.db.Yazarlar.Find(id);
            Baglanti.db.Yazarlar.Remove(x);
            Baglanti.db.SaveChanges();
            return RedirectToAction("AnaYazar");
        }
        public ActionResult YazarGetir(byte id)
        {
            var yzr = Baglanti.db.Yazarlar.Find(id);
            return View("YazarGetir", yzr);
        }
        public ActionResult YazarGuncelle(Yazarlar y)
        {
            var x = Baglanti.db.Yazarlar.Find(y.ID);
            if (!ModelState.IsValid)
            {
                return View("YazarGetir");
            }
            x.Yazar = y.Yazar;
            x.Detay = y.Detay;
            Baglanti.db.SaveChanges();
            return RedirectToAction("AnaYazar");
        }
    }
}