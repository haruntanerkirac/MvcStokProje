using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcStokProje.Models.Entity; //

namespace MvcStokProje.Controllers
{
    public class UrunController : Controller
    {
        MvcDbStokEntities db = new MvcDbStokEntities();
        public ActionResult Index()
        {
            var urunListesi = db.TBLURUNLER.ToList();
            return View(urunListesi);
        }

        [HttpGet]
        public ActionResult YeniUrun()
        {
            List<SelectListItem> kategoriler = (from i in db.TBLKATEGORILER
                                                select new SelectListItem
                                                {
                                                    Text = i.KATEGORIAD,
                                                    Value = i.KATEGORIID.ToString()
                                                }).ToList();
            ViewBag.ktgr = kategoriler;
            return View();
        }

        [HttpPost]
        public ActionResult YeniUrun(TBLURUNLER urun)
        {
            var ktg = db.TBLKATEGORILER.Where(m => m.KATEGORIID == urun.TBLKATEGORILER.KATEGORIID).FirstOrDefault();
            urun.TBLKATEGORILER = ktg;
            db.TBLURUNLER.Add(urun);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Sil(int id)
        {
            var silinecekUrun = db.TBLURUNLER.Find(id);
            db.TBLURUNLER.Remove(silinecekUrun);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult UrunGetir(int id)
        {
            var urun = db.TBLURUNLER.Find(id);
            List<SelectListItem> kategoriler = (from i in db.TBLKATEGORILER
                                                select new SelectListItem
                                                {
                                                    Text = i.KATEGORIAD,
                                                    Value = i.KATEGORIID.ToString()
                                                }).ToList();
            ViewBag.ktgr = kategoriler;
            return View("UrunGetir", urun);
        }

        public ActionResult Guncelle(TBLURUNLER p)
        {
            var urun = db.TBLURUNLER.Find(p.URUNID);
            urun.URUNAD = p.URUNAD;
            urun.MARKA = p.MARKA;
            urun.FIYAT = p.FIYAT;
            urun.STOK = p.STOK;
            //urun.URUNKATEGORI = p.URUNKATEGORI;
            var ktg = db.TBLKATEGORILER.Where(m => m.KATEGORIID == p.TBLKATEGORILER.KATEGORIID).FirstOrDefault();
            urun.URUNKATEGORI = ktg.KATEGORIID;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}