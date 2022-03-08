using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVCKutuphane.Models.Entity;

namespace MVCKutuphane.Controllers
{
    public class KitapController : Controller
    {
        // GET: Kitap
        public ActionResult AnaKitap(string x)
        {

            var kitap = from c in Baglanti.db.Kitaplar select c;
            if (!string.IsNullOrEmpty(x))
            {
                kitap = kitap.Where(c => c.Kitap.Contains(x) && c.Durum == true);
            }
            return View(kitap.ToList());
        }
        [HttpGet]
        public ActionResult YeniKitap()
        {
            List<SelectListItem> kategori = (from x in Baglanti.db.Kategoriler.ToList()
                                             select new SelectListItem
                                             {
                                                 Text = x.Kategori,
                                                 Value = x.ID.ToString()
                                             }).ToList();
            ViewBag.dgr = kategori;

            List<SelectListItem> yazar = (from x in Baglanti.db.Yazarlar.ToList()
                                          select new SelectListItem
                                          {
                                              Text = x.Yazar,
                                              Value = x.ID.ToString()
                                          }).ToList();
            ViewBag.dgr2 = yazar;


            return View();
        }
        [HttpPost]
        public ActionResult YeniKitap(Kitaplar k)
        {
            var kategori = Baglanti.db.Kategoriler.Where(c => c.ID == k.Kategoriler.ID).FirstOrDefault();
            var yazar = Baglanti.db.Yazarlar.Where(c => c.ID == k.Yazarlar.ID).FirstOrDefault();
            k.Kategoriler = kategori;
            k.Yazarlar = yazar;
            Baglanti.db.Kitaplar.Add(k);
            Baglanti.db.SaveChanges();
            return RedirectToAction("AnaKitap");
        }
        public ActionResult KitapSil(byte id)
        {
            var kitap = Baglanti.db.Kitaplar.Find(id);
            Baglanti.db.Kitaplar.Remove(kitap);
            Baglanti.db.SaveChanges();
            return RedirectToAction("AnaKitap");
        }
        public ActionResult KitapGetir(byte id)
        {
            var ktp = Baglanti.db.Kitaplar.Find(id);
            List<SelectListItem> kategori = (from x in Baglanti.db.Kategoriler.ToList()
                                             select new SelectListItem
                                             {
                                                 Text = x.Kategori,
                                                 Value = x.ID.ToString()
                                             }).ToList();
            ViewBag.dgr = kategori;

            List<SelectListItem> yazar = (from x in Baglanti.db.Yazarlar.ToList()
                                          select new SelectListItem
                                          {
                                              Text = x.Yazar,
                                              Value = x.ID.ToString()
                                          }).ToList();
            ViewBag.dgr2 = yazar;
            return View("KitapGetir", ktp);
        }
        public ActionResult KitapGuncelle(Kitaplar k)
        {
            var kategori = Baglanti.db.Kategoriler.Where(c => c.ID == k.Kategoriler.ID).FirstOrDefault();
            var yazar = Baglanti.db.Yazarlar.Where(c => c.ID == k.Yazarlar.ID).FirstOrDefault();
            var x = Baglanti.db.Kitaplar.Find(k.ID);
            x.Kitap = k.Kitap;
            x.Basim = k.Basim;
            x.Durum = true;
            x.Kategori = k.Kategori;
            x.Sayfa = k.Sayfa;
            x.YayinEvi = k.YayinEvi;
            x.Kategori = kategori.ID;
            x.Yazar = yazar.ID;
            Baglanti.db.SaveChanges();
            return RedirectToAction("AnaKitap");
        }
    }
}