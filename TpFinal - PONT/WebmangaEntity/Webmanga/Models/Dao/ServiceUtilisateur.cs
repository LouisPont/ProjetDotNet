using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using WebManga.Models.MesExceptions;
using Webmanga.Models.Utilitaires;

namespace Webmanga.Models.Dao
{
    public class ServiceUtilisateur
    {
        private static ServiceUtilisateur instance;
        private static mangaEntities unMangaEntity;

     

        /// <summary>
        /// Obtenir le singleton et le créer s'il n'existe pas
        /// </summary>
        public static ServiceUtilisateur getInstance()
        {
            if (ServiceUtilisateur.instance == null)
            {
                ServiceUtilisateur.instance = new ServiceUtilisateur();
                // on définit un contexte commun à toutes les requêtes
                unMangaEntity = new mangaEntities();
            }
            return ServiceUtilisateur.instance;
        }
        // on rend le constructeur privé
        private ServiceUtilisateur()
        { }

        public utilisateur getUnUtilsateur (String nom)
        {
            utilisateur unUtil = null;
            Serreurs er = new Serreurs("Erreur sur lecture des Utilisateur", "ServiceUtilisateur.getunUtilisateur()");
            try
            {
                unUtil = (from u in unMangaEntity.utilisateur
                          where u.NomUtil == nom
                           select u)
                           .FirstOrDefault();

                return unUtil;
            }
            catch (Exception e)
            {
                throw new MonException(er.MessageUtilisateur(), er.MessageApplication(), e.Message);
            }

        }

        public void createUtil(String nom, String prenom, String mdp)
        {
            utilisateur unUtil = (from u in unMangaEntity.utilisateur
                               where u.NumUtil == 1
                               select u)
                           .FirstOrDefault();
            int i = 1;
            while (unUtil != null)
            {
                i++;
                unUtil = (from u in unMangaEntity.utilisateur
                          where u.NumUtil == i
                          select u)
                           .FirstOrDefault();
                
            }

            byte[] saltHash = MonMotPassHash.GenerateSalt();
            byte[] mdpHash = MonMotPassHash.PasswordHashe(mdp, saltHash);
            mdp = MonMotPassHash.BytesToString(mdpHash);
            String salt = MonMotPassHash.BytesToString(saltHash);
            utilisateur Util = new utilisateur
            {
                NumUtil = i,
                NomUtil = nom,
                PrenomUtil = prenom,
                MotPasse = mdp,
                Salt = salt,
                role = "lecture"
            };

            unMangaEntity.utilisateur.Add(Util);
            unMangaEntity.SaveChanges();
        }
    }
}