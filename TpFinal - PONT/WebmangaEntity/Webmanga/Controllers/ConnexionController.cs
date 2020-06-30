using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Webmanga.Models.Utilitaires;
using WebManga.Models.MesExceptions;
using Webmanga.Models.Dao;
using Webmanga.Models;

namespace Webmanga.Controllers
{
    public class ConnexionController : Controller
    {
        // GET: Connexion
        public ActionResult Controle()
        {
            utilisateur unUtilisateur = null;
            try
            {
                String login = Request["login"];
                String mdp = Request["pwd"];
                try
                {
                    unUtilisateur = ServiceUtilisateur.getInstance().getUnUtilsateur(login);
                    
                    if (unUtilisateur != null)
                    {
                        try
                        {
                            String sel = unUtilisateur.Salt;
                            Byte[] salt = MonMotPassHash.transformeEnBytes(unUtilisateur.Salt);
                            Byte[] tempo = MonMotPassHash.transformeEnBytes(unUtilisateur.MotPasse);
                            if (MonMotPassHash.VerifyPassword(salt, mdp, tempo) == true)
                            {
                                Session["id"] = unUtilisateur.NumUtil;
                                Session["role"] = unUtilisateur.role;
                            }
                            else
                            {
                                ModelState.AddModelError("Erreur", "Erreur lors du contrôle du mot de passe pour :" + login);
                                return RedirectToAction("Index", "Home");
                            }
                        }
                        catch (Exception e)
                        {
                            ModelState.AddModelError("Erreur", "Erreur lors du contrôle :" + e.Message);
                            return RedirectToAction("Index", "Home");
                        }
                    }
                    else
                    {
                        ModelState.AddModelError("Erreur", "Erreur login erroné :" + login);
                        return RedirectToAction("Index", "Home");
                    }
                }
                catch (MonException e)
                {
                    ModelState.AddModelError("Erreur", "Erreur lors de l'authentification :" + e.Message);
                    return RedirectToAction("Index", "Home");
                }
                return RedirectToAction("Index", "Manga");
            }
            catch (MonException e)
            {
                ModelState.AddModelError("Erreur", "Erreur lors de l'authentification :" + e.Message);
                return RedirectToAction("Index", "Home");
            }
        }

        public ActionResult Insc()
        {
            String nom = Request["Nom"];
            String prenom = Request["Prenom"];
            String mdp = Request["mdp"];
            String confMdp = Request["confMdp"];

            if (mdp == confMdp)
            {
                try
                {
                    ServiceUtilisateur.getInstance().createUtil(nom, prenom, mdp);
                }
                catch (MonException e)
                {
                    return HttpNotFound();
                }

            } else
            {
                return RedirectToAction("NouvUtil","Home");
            }

            return RedirectToAction("Index", "Home");
        }
    }
}