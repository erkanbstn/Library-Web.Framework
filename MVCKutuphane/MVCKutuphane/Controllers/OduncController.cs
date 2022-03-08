using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVCKutuphane.Models.Entity;
namespace MVCKutuphane.Controllers
{
    public class OduncController : Controller
    {
        // GET: Odunc
        public ActionResult OduncAl()
        {
            var x = Baglanti.db.Hareketler.Where(c => c.IslemDurum == false).ToList();
            return View(x);
        }
        [HttpGet]
        public ActionResult OduncVer()
        {
            List<SelectListItem> uye = (from x in Baglanti.db.Uyeler.ToList()
                                        select new SelectListItem
                                        {
                                            Text = x.Uye,
                                            Value = x.ID.ToString()
                                        }).ToList();
            ViewBag.dgr = uye;

            List<SelectListItem> kitap = (from x in Baglanti.db.Kitaplar.Where(c => c.Durum == true).ToList()
                                          select new SelectListItem
                                          {
                                              Text = x.Kitap,
                                              Value = x.ID.ToString()
                                          }).ToList();
            ViewBag.kitap = kitap;

            List<SelectListItem> personel = (from x in Baglanti.db.Personeller.ToList()
                                             select new SelectListItem
                                             {
                                                 Text = x.Personel,
                                                 Value = x.ID.ToString()
                                             }).ToList();
            ViewBag.personel = personel;

            return View();
        }
        [HttpPost]
        public ActionResult OduncVer(Hareketler k)
        {
            var kitap = Baglanti.db.Kitaplar.Where(c => c.ID == k.Kitaplar.ID).FirstOrDefault();
            var personel = Baglanti.db.Personeller.Where(c => c.ID == k.Personeller.ID).FirstOrDefault();
            var uye = Baglanti.db.Uyeler.Where(c => c.ID == k.Uyeler.ID).FirstOrDefault();
            k.Kitaplar = kitap;
            k.Personeller = personel;
            k.Uyeler = uye;
            Baglanti.db.Hareketler.Add(k);
            Baglanti.db.SaveChanges();
            return RedirectToAction("OduncAl");
        }
        public ActionResult OduncIade(Hareketler p)
        {
            var x = Baglanti.db.Hareketler.Find(p.ID);
            return View("OduncIade", x);
        }
        public ActionResult OduncGuncelle(Hareketler k)
        {
            var x = Baglanti.db.Hareketler.Find(k.ID);
            x.UyeGetirdigiTarih = k.UyeGetirdigiTarih;
            x.IslemDurum = true;
            Baglanti.db.SaveChanges();
            return RedirectToAction("OduncAl");
        }
    }
}