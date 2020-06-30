using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Webmanga.Models.Dao;
using WebManga.Models.MesExceptions;

namespace Webmanga.Controllers
{
    public class NouveauController : Controller
    {
        // GET: Nouveau
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult addManga()
        {
            int id_des = int.Parse(Request["Dessinateur"]);
            int id_scn = int.Parse(Request["Scenariste"]);
            String prixString = Request["prix"].Replace(".", ",");
            decimal prix = decimal.Parse(prixString);
            String titre = Request["titre"];
            DateTime dateParution = Convert.ToDateTime(Request["Date"]);
            String couverture = Request["couverture"];
            int id_genre = int.Parse(Request["genre"]);


            try
            {
                ServiceManga.getInstance().createManga(id_des, id_scn, prix, titre, dateParution, couverture, id_genre);
            }
            catch (MonException e)
            {
                return HttpNotFound();
            }
            return RedirectToAction("Index", "Manga");
        }

        public ActionResult addDessinateur()
        {
            String nom = Request["Nom"];
            String prenom = Request["Prenom"];
            try
            {
                ServiceManga.getInstance().createDessinateur(nom, prenom);
            }
            catch (MonException e)
            {
                return HttpNotFound();
            }
            return RedirectToAction("Index", "Manga");
        }

        public ActionResult addScenariste()
        {
            String nom = Request["Nom"];
            String prenom = Request["Prenom"];
            try
            {
                ServiceManga.getInstance().createScenariste(nom, prenom);
            }
            catch (MonException e)
            {
                return HttpNotFound();
            }
            return RedirectToAction("Index", "Manga");
        }

        public ActionResult addStyle()
        {

            String libele = Request["libele"];
            try
            {
                ServiceManga.getInstance().createStyle(libele);
            }
            catch (MonException e)
            {
                return HttpNotFound();
            }

            return RedirectToAction("Index", "Manga");

        }


    }
}