using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVCKutuphane.Models.Entity;

namespace MVCKutuphane.Controllers
{
    public class KategoriController : Controller
    {
        // GET: Kategori
        public ActionResult AnaKategori()
        {
            var x = Baglanti.db.Kategoriler.Where(c => c.Durum == true).ToList();
            return View(x);
        }
        [HttpGet]
        public ActionResult YeniKategori()
        {
            return View();
        }
        [HttpPost]
        public ActionResult YeniKategori(Kategoriler k)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            k.Durum = true;
            Baglanti.db.Kategoriler.Add(k);
            Baglanti.db.SaveChanges();
            return RedirectToAction("AnaKategori");
        }
        public ActionResult KategoriSil(Kategoriler k)
        {
            var x = Baglanti.db.Kategoriler.Find(k.ID);
            x.Durum = false;
            Baglanti.db.SaveChanges();
            return RedirectToAction("AnaKategori");
        }
        public ActionResult KategoriGetir(byte id)
        {
            var ktg = Baglanti.db.Kategoriler.Find(id);
            return View("KategoriGetir",ktg);
        }
        public ActionResult KategoriGuncelle(Kategoriler k)
        {
            if (!ModelState.IsValid)
            {
                return View("KategoriGetir");
            }
            var x = Baglanti.db.Kategoriler.Find(k.ID);
            x.Kategori = k.Kategori;
            Baglanti.db.SaveChanges();
            return RedirectToAction("AnaKategori");
        }
    }
}