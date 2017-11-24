namespace SMSApp.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class UsuarioContext : DbContext
    {
        public UsuarioContext()
            : base("name=UsuarioContext")
        {
        }

        public virtual DbSet<Table> Tables { get; set; }
        public virtual DbSet<Usuario> Usuarios { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        }
    }
}
