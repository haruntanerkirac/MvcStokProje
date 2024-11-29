using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcStokProje.Models.Entity; //
using PagedList;
using PagedList.Mvc;

namespace MvcStokProje.Controllers
{
    public class KategoriController : Controller
    {
        MvcDbStokEntities db = new MvcDbStokEntities();
        public ActionResult Index(int sayfa = 1)
        { 
            //var kategoriListesi = db.TBLKATEGORILER.ToList();
            var kategoriListesi = db.TBLKATEGORILER.ToList().ToPagedList(sayfa,4);
            return View(kategoriListesi);
        }

        [HttpGet]
        public ActionResult YeniKategori()
        {
            return View();
        }

        [HttpPost]
        public ActionResult YeniKategori(TBLKATEGORILER kategori)
        {
            if (!ModelState.IsValid)
            {
                return View("YeniKategori");
            }
            db.TBLKATEGORILER.Add(kategori);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Sil(int id)
        {
            var silinecekKategori = db.TBLKATEGORILER.Find(id);
            db.TBLKATEGORILER.Remove(silinecekKategori);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult KategoriGetir(int id)
        {
            var kategori = db.TBLKATEGORILER.Find(id);
            return View("KategoriGetir", kategori);
        }

        public ActionResult Guncelle(TBLKATEGORILER kategori)
        {
            var ktgr = db.TBLKATEGORILER.Find(kategori.KATEGORIID);
            ktgr.KATEGORIAD = kategori.KATEGORIAD;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}