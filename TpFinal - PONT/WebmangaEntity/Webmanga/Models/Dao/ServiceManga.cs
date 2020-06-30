using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebManga.Models.MesExceptions;
using System.Data;
using System.Collections;
using MySql.Data.MySqlClient;
using System.Configuration;

namespace Webmanga.Models.Dao
{
    public class ServiceManga
    {
         private static ServiceManga instance;
         private static mangaEntities unMangaEntity;

         /// <summary>
         /// Obtenir le singleton et le créer s'il n'existe pas
         /// </summary>
        public static ServiceManga getInstance()
            {
                if (ServiceManga.instance == null)
                {
                ServiceManga.instance = new ServiceManga();
                    // on définit un contexte commun à toutes les requêtes
                    unMangaEntity = new mangaEntities();
                }
                return ServiceManga.instance;
            }
        // on rend le constructeur privé
         private ServiceManga()
            { }
   

        public  manga GetunManga(int id)
            {
                manga unManga = null;

                Serreurs er = new Serreurs("Erreur sur lecture des Mangas", "ServiceManga.getunManag()");
                try
                {
                    unManga = (from m in unMangaEntity.manga
                               where m.id_manga == id 
                               select m)
                               .FirstOrDefault();

                    return unManga;
                }
                catch (Exception e)
                {
                    throw new MonException(er.MessageUtilisateur(), er.MessageApplication(), e.Message);
                }
            }

        /// <summary>
        /// Lister les magas de la base
        /// </summary>
        /// <returns>Liste des mangas</returns>
        public  DataTable ListMangas()
        {
            Serreurs er = new Serreurs("Erreur sur lecture des mangas.", "Servivemanga.ListeMangas()");
            DataTable dt = new DataTable();
          /// On construit les colonnes
          /// du datatable
            try
            {
                dt.Columns.Add("id_manga", typeof(int));
                dt.Columns.Add("lib_genre", typeof(String));
                dt.Columns.Add("nom_dessinateur", typeof(String));
                dt.Columns.Add("nom_scenariste", typeof(String));
                dt.Columns.Add("dateParution", typeof(DateTime));
                dt.Columns.Add("titre", typeof(String));
                dt.Columns.Add("prix", typeof(Double));
                dt.Columns.Add("couverture", typeof(String));

                var mesMangas = (from m in unMangaEntity.manga
                                 join g in unMangaEntity.genre on m.id_genre equals g.id_genre
                                 join d in unMangaEntity.dessinateur on m.id_dessinateur equals d.id_dessinateur
                                 join s in unMangaEntity.scenariste on m.id_scenariste equals s.id_scenariste
                                 orderby m.id_manga
                                 select new { m.id_manga, g.lib_genre, d.nom_dessinateur, s.nom_scenariste, m.dateParution,m.titre, m.prix, m.couverture }
                                 );
                foreach (var res in mesMangas)
                {
                    dt.Rows.Add(res.id_manga, res.lib_genre, res.nom_dessinateur, res.nom_scenariste, res.dateParution, res.titre,res.prix,res.couverture);
                }
                return dt;
            }
            catch (Exception e)
            {
                throw new MonException(er.MessageUtilisateur(),
                    er.MessageApplication(), e.Message);
            }
        }

        public DataTable ListGenres()
        {
            Serreurs er = new Serreurs("Erreur sur lecture des genres.", "Servivemanga.ListeGenre()");
            DataTable dt = new DataTable();
            /// On construit les colonnes
            /// du datatable
            try
            {
                dt.Columns.Add("id_genre", typeof(int));
                dt.Columns.Add("lib_genre", typeof(String));

                var mesGenres = (from m in unMangaEntity.genre
                                 select new { m.id_genre, m.lib_genre }
                                 );

                
                foreach (var res in mesGenres)
                {
                    dt.Rows.Add(res.id_genre, res.lib_genre);
                }
                return dt;
            }
            catch (Exception e)
            {
                throw new MonException(er.MessageUtilisateur(),
                    er.MessageApplication(), e.Message);
            }
        }

        public DataTable ListDessinateurs()
        {
            Serreurs er = new Serreurs("Erreur sur lecture des dessinateurs.", "Servivemanga.ListeDessinateurs()");
            DataTable dt = new DataTable();
            /// On construit les colonnes
            /// du datatable
            try
            {
                dt.Columns.Add("id_dessinateur", typeof(int));
                dt.Columns.Add("nom_dessinateur", typeof(String));
                dt.Columns.Add("prenom_dessinateur", typeof(String));

                var mesDessinateurs = (from m in unMangaEntity.dessinateur
                                        select new { m.id_dessinateur, m.nom_dessinateur, m.prenom_dessinateur  }
                                 );
                foreach (var res in mesDessinateurs)
                {
                    dt.Rows.Add(res.id_dessinateur, res.nom_dessinateur, res.prenom_dessinateur);
                }
                return dt;
            }
            catch (Exception e)
            {
                throw new MonException(er.MessageUtilisateur(),
                    er.MessageApplication(), e.Message);
            }
        }

        public DataTable ListScenaristes()
        {
            Serreurs er = new Serreurs("Erreur sur lecture des scenaristes.", "Servivemanga.ListeDessinateurs()");
            DataTable dt = new DataTable();
            /// On construit les colonnes
            /// du datatable
            try
            {
                dt.Columns.Add("id_scenariste", typeof(int));
                dt.Columns.Add("nom_scenariste", typeof(String));
                dt.Columns.Add("prenom_scenariste", typeof(String));

                var mesScenaristes = (from m in unMangaEntity.scenariste
                                       select new { m.id_scenariste, m.nom_scenariste, m.prenom_scenariste }
                                 );
                foreach (var res in mesScenaristes)
                {
                    dt.Rows.Add(res.id_scenariste, res.nom_scenariste, res.prenom_scenariste);
                }
                return dt;
            }
            catch (Exception e)
            {
                throw new MonException(er.MessageUtilisateur(),
                    er.MessageApplication(), e.Message);
            }
        }
        /// <summary>
        /// Fonction qui met à jour un manga
        /// </summary>
        /// <param name="unM"></param>
        public void UpdateManga(manga unM)
        {
            manga unManga = null;
            Serreurs er = new Serreurs("Erreur sur l'écriture d'un manga.", "ServiceManga.update()");

            // requête de sélection pour une mise à jour .

            unManga = GetunManga(unM.id_manga);

            // On modifie les données 
         
                unManga.id_dessinateur = unM.id_dessinateur;
                unManga.id_genre = unM.id_genre;
                unManga.id_scenariste = unM.id_scenariste;
                unManga.dateParution= unM.dateParution;
                unManga.prix = unM.prix;
                unManga.titre = unM.titre;
                unManga.couverture = unM.couverture;
           

            // On enregistre les modifications dans la base de données .
            try
            {
                unMangaEntity.SaveChanges();
            }
            catch (Exception e)
            {
                throw new MonException(er.MessageUtilisateur(),
                    er.MessageApplication(), e.Message);
            }
        }

        public void createDessinateur(String nom, String prenom)
        {
            DataTable td = ListDessinateurs();
            int i = td.Rows.Count+1;

            dessinateur desc2 = new dessinateur
            {
                id_dessinateur = i,
                nom_dessinateur = nom,
                prenom_dessinateur = prenom
            };

            unMangaEntity.dessinateur.Add(desc2);
            unMangaEntity.SaveChanges();
        }

        public void createScenariste(String nom, String prenom)
        {
            DataTable td = ListScenaristes();
            int i = td.Rows.Count+1;

            scenariste scen2 = new scenariste
            {
                id_scenariste = i,
                nom_scenariste = nom,
                prenom_scenariste = prenom
            };

            unMangaEntity.scenariste.Add(scen2);
            unMangaEntity.SaveChanges();
        }

        public void createStyle(String libele)
        {
            DataTable td = ListGenres();
            int i = td.Rows.Count+1;


            genre genn = new genre
            {
                id_genre = i,
                lib_genre = libele
            };

            unMangaEntity.genre.Add(genn);
            unMangaEntity.SaveChanges();
        }

        public void createManga(int id_dessinateur, int id_scenariste, decimal prix, String titre, DateTime dateParution, String couverture, int id_genre)
        {
            DataTable td = ListMangas();
            int i = td.Rows.Count+1;

            manga manga = new manga
            {
                id_manga = i,
                id_dessinateur = id_dessinateur,
                id_scenariste = id_scenariste,
                prix = prix,
                titre = titre,
                dateParution = dateParution,
                couverture = couverture,
                id_genre = id_genre
            };

            unMangaEntity.manga.Add(manga);
            unMangaEntity.SaveChanges();
        }

        public void suppManga( int id)
        {
            manga m = unMangaEntity.manga.Find(id);
            unMangaEntity.manga.Remove(m);
            unMangaEntity.SaveChanges();
        }

        public DataTable RechercherMangas(String s1, String s2)
        {

            Serreurs er = new Serreurs("Erreur sur lecture des mangas.", "ServiceManga.ListeMangas()");
            DataTable dt = new DataTable();
            /// On construit les colonnes
            /// du datatable
            try
            {
                dt.Columns.Add("id_manga", typeof(int));
                dt.Columns.Add("lib_genre", typeof(String));
                dt.Columns.Add("nom_dessinateur", typeof(String));
                dt.Columns.Add("nom_scenariste", typeof(String));
                dt.Columns.Add("dateParution", typeof(DateTime));
                dt.Columns.Add("titre", typeof(String));
                dt.Columns.Add("prix", typeof(Double));
                dt.Columns.Add("couverture", typeof(String));

                var mesMangas = (from m in unMangaEntity.manga
                                             join g in unMangaEntity.genre on m.id_genre equals g.id_genre
                                             join d in unMangaEntity.dessinateur on m.id_dessinateur equals d.id_dessinateur
                                             join s in unMangaEntity.scenariste on m.id_scenariste equals s.id_scenariste
                                             orderby m.id_manga
                                             where m.titre.ToUpper() == s2.ToUpper()
                                             select new { m.id_manga, g.lib_genre, d.nom_dessinateur, s.nom_scenariste, m.dateParution, m.titre, m.prix, m.couverture }
                                );

                switch (s1) {
                case "nom_dessinateur":
                     mesMangas = (from m in unMangaEntity.manga
                                     join g in unMangaEntity.genre on m.id_genre equals g.id_genre
                                     join d in unMangaEntity.dessinateur on m.id_dessinateur equals d.id_dessinateur
                                     join s in unMangaEntity.scenariste on m.id_scenariste equals s.id_scenariste
                                     orderby m.id_manga
                                     where d.nom_dessinateur.ToUpper() == s2.ToUpper()
                                     select new { m.id_manga, g.lib_genre, d.nom_dessinateur, s.nom_scenariste, m.dateParution, m.titre, m.prix, m.couverture }
                                );
                    break;
                case "lib_genre":
                        mesMangas = (from m in unMangaEntity.manga
                                         join g in unMangaEntity.genre on m.id_genre equals g.id_genre
                                         join d in unMangaEntity.dessinateur on m.id_dessinateur equals d.id_dessinateur
                                         join s in unMangaEntity.scenariste on m.id_scenariste equals s.id_scenariste
                                         orderby m.id_manga
                                         where g.lib_genre.ToUpper() == s2.ToUpper()
                                         select new { m.id_manga, g.lib_genre, d.nom_dessinateur, s.nom_scenariste, m.dateParution, m.titre, m.prix, m.couverture }
                                    );
                        break;
                case "nom_scenariste":
                        mesMangas = (from m in unMangaEntity.manga
                                         join g in unMangaEntity.genre on m.id_genre equals g.id_genre
                                         join d in unMangaEntity.dessinateur on m.id_dessinateur equals d.id_dessinateur
                                         join s in unMangaEntity.scenariste on m.id_scenariste equals s.id_scenariste
                                         orderby m.id_manga
                                         where s.nom_scenariste.ToUpper() == s2.ToUpper()
                                         select new { m.id_manga, g.lib_genre, d.nom_dessinateur, s.nom_scenariste, m.dateParution, m.titre, m.prix, m.couverture }
                                    );
                        break;
                case "titre":
                         
                        break;
                }

                foreach (var res in mesMangas)
                {
                    dt.Rows.Add(res.id_manga, res.lib_genre, res.nom_dessinateur, res.nom_scenariste, res.dateParution, res.titre, res.prix, res.couverture);
                    System.Diagnostics.Debug.WriteLine(res.titre);
                }

                return dt;
            }
            catch (Exception e)
            {
                throw new MonException(er.MessageUtilisateur(),
                    er.MessageApplication(), e.Message);
            }
        }

        

        public void Procedure_Stockee(String procedure, ArrayList alParams)
        {

            System.Diagnostics.Debug.WriteLine("oui");
            MySqlConnection cnx = null;
            MySqlCommand cmd = null;
            int i, nbligne;
            string[] alContenu;
            try
            {
                String strConnexion = ConfigurationManager.ConnectionStrings["bddCourante"].ConnectionString;
                cnx = new MySqlConnection(strConnexion);
                cnx.Open();
                cmd = new MySqlCommand();
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = procedure;
                nbligne = alParams.Count;
                for (i = 0; i < nbligne; i++)
                {
                    alContenu = alParams[i].ToString().Split(';');
                    cmd.Parameters.AddWithValue(alContenu[0], alContenu[1]);
                }
                cmd.Connection = cnx;
                cmd.ExecuteNonQuery();
            }

            catch (Exception e)
            {
                throw e;
            }
        }

    }
}
