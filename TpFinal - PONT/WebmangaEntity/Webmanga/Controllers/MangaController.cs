using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebManga.Models.MesExceptions;
using Webmanga.Models.Dao;
using Webmanga.Models;
using System.Data;
using System.Collections;
using MySql.Data.MySqlClient;
using System.Data.SqlClient;
using System.Configuration;

namespace Webmanga.Controllers
{

    public class MangaController : Controller
    {

        public ActionResult Index()
        {
            if (Request["Option"] != null)
            {
                String s1 = Request["Option"];
                String s2 = Request["recherche"];

                DataTable mesMangas = null;

                try
                {
                    mesMangas = ServiceManga.getInstance().RechercherMangas(s1, s2);
                }
                catch (MonException e)
                {
                    ModelState.AddModelError("Erreur", "Erreur lors de la récupération des mangas : " + e.Message);
                }
                return View(mesMangas);
            }
            else
            {
                DataTable mesMangas = null;
                try
                {
                    mesMangas = ServiceManga.getInstance().ListMangas();
                }
                catch (MonException e)
                {
                    ModelState.AddModelError("Erreur", "Erreur lors de la récupération des mangas : " + e.Message);
                }
                return View(mesMangas);
            }


        }

        // GET: Commande/Edit/5
        public ActionResult Modifier(int id)
        {
            manga unManga = null;
            try
            {
                unManga = ServiceManga.getInstance().GetunManga(id);
                return View(unManga);
            }
            catch (MonException e)
            {
                return HttpNotFound();
            }
        }

        public ActionResult MonterPrix()
        {
            try
            {
                return View();
            }
            catch (MonException e)
            {
                return HttpNotFound();
            }
        }

        public ActionResult AugmenterPrix()
        {

            double prix = Convert.ToDouble(Request["hausse"]);
            prix = prix / 100;
            String prixStringed = Convert.ToString(prix);
            prixStringed = prixStringed.Replace(",", ".");
            System.Diagnostics.Debug.WriteLine(prixStringed);

            Serreurs er = new Serreurs("Erreur lors de l'execution de la procédure.", "MangaControler.AugmenterPrix()");
            ArrayList myArray = new ArrayList();
            myArray.Add("augmente;" + prixStringed);

            System.Diagnostics.Debug.WriteLine(myArray[0]);
            ServiceManga.getInstance().Procedure_Stockee("articles_augm_prix", myArray);
            return RedirectToAction("Index", "Manga");
        }

        public ActionResult Supprimer(int id)
        {
            try
            {
                ServiceManga.getInstance().suppManga(id);
                return RedirectToAction("Index");
            }
            catch (MonException e)
            {
                return HttpNotFound();
            }
        }

        [HttpPost]
        public ActionResult Modifier(manga unM)
        {
            try
            {
                ServiceManga.getInstance().UpdateManga(unM);
                return RedirectToAction("Index", "Manga");
            }
            catch (MonException e)
            {
                return HttpNotFound();
            }
        }

        public ActionResult AjouterManga()
        {

            MyDataViewModel myDataObject = new MyDataViewModel();

            DataTable mesGenres = null;
            DataTable mesDessinateurs = null;
            DataTable mesScenaristes = null;

            try
            {
                mesGenres = ServiceManga.getInstance().ListGenres();
                mesDessinateurs = ServiceManga.getInstance().ListDessinateurs();
                mesScenaristes = ServiceManga.getInstance().ListScenaristes();

                myDataObject.FirstTable = mesGenres;
                myDataObject.SecondTable = mesDessinateurs;
                myDataObject.ThirdTable = mesScenaristes;
            }
            catch (MonException e)
            {
                ModelState.AddModelError("Erreur", "Erreur lors de la récupération des Genres ou des Dessinateurs : " + e.Message);
            }
            System.Diagnostics.Debug.WriteLine("");
            return View(myDataObject);

        }

        public ActionResult AjouterDessinateur()
        {
            try
            {
                return View();
            }
            catch (MonException e)
            {
                return HttpNotFound();
            }
        }
         public ActionResult Video()
        {
            try
            {
                object nom = "YuGiHo.mp4";
                return View(nom);
            } catch (MonException e)
            {
                return HttpNotFound();
            }
        }

        public ActionResult AjouterScenariste()
        {
            try
            {
                return View();
            }
            catch (MonException e)
            {
                return HttpNotFound();
            }
        }

        public ActionResult AjouterStyle()
        {
            try
            {
                return View();
            }
            catch (MonException e)
            {
                return HttpNotFound();
            }
        }

    }
}