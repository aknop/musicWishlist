﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MusicLibrary
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class TrueEntities : DbContext
    {
        public TrueEntities()
            : base("name=TrueEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<artist> artists { get; set; }
        public virtual DbSet<song> songs { get; set; }
        public virtual DbSet<album> albums { get; set; }
        public virtual DbSet<genre> genres { get; set; }
    }
}
