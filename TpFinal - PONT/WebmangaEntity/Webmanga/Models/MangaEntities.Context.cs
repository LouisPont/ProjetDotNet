﻿//------------------------------------------------------------------------------
// <auto-generated>
//    Ce code a été généré à partir d'un modèle.
//
//    Des modifications manuelles apportées à ce fichier peuvent conduire à un comportement inattendu de votre application.
//    Les modifications manuelles apportées à ce fichier sont remplacées si le code est régénéré.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Webmanga.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class mangaEntities : DbContext
    {
        public mangaEntities()
            : base("name=mangaEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public DbSet<dessinateur> dessinateur { get; set; }
        public DbSet<genre> genre { get; set; }
        public DbSet<manga> manga { get; set; }
        public DbSet<scenariste> scenariste { get; set; }
        public DbSet<utilisateur> utilisateur { get; set; }
    }
}