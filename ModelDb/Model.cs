using System;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace Sanator
{
    public partial class Model1 : DbContext
    {
        public Model1()
            : base("name=Model11")
        {
        }

        public virtual DbSet<Client> Clients { get; set; }
        public virtual DbSet<Kategory> Kategory { get; set; }
        public virtual DbSet<Log> Log { get; set; }
        public virtual DbSet<Pay> Pay { get; set; }
        public virtual DbSet<Service> Service { get; set; }
        public virtual DbSet<Status> Status { get; set; }
        public virtual DbSet<Uchet> Uchet { get; set; }
        public virtual DbSet<Worker> Worker { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Client>()
                .Property(e => e.FIO)
                .IsFixedLength();

            modelBuilder.Entity<Client>()
                .Property(e => e.passport)
                .IsFixedLength();

            modelBuilder.Entity<Client>()
                .Property(e => e.number)
                .IsFixedLength();

            modelBuilder.Entity<Client>()
                .HasMany(e => e.Log)
                .WithRequired(e => e.Client)
                .HasForeignKey(e => e.ID_client_FK);

            modelBuilder.Entity<Client>()
                .HasMany(e => e.Pay)
                .WithRequired(e => e.Client)
                .HasForeignKey(e => e.ID_client_FK);

            modelBuilder.Entity<Client>()
                .HasMany(e => e.Uchet)
                .WithRequired(e => e.Client)
                .HasForeignKey(e => e.ID_client_FK);

            modelBuilder.Entity<Kategory>()
                .Property(e => e.name)
                .IsFixedLength();

            modelBuilder.Entity<Kategory>()
                .Property(e => e.description)
                .IsFixedLength();

            modelBuilder.Entity<Kategory>()
                .HasMany(e => e.Number)
                .WithRequired(e => e.Kategory)
                .HasForeignKey(e => e.ID_type_FK);

            modelBuilder.Entity<Pay>()
                .HasMany(e => e.Log)
                .WithOptional(e => e.Pay)
                .HasForeignKey(e => e.ID_pay_FK);

            modelBuilder.Entity<Pay>()
                .HasMany(e => e.Uchet)
                .WithOptional(e => e.Pay)
                .HasForeignKey(e => e.ID_pay_FK);

            modelBuilder.Entity<Service>()
                .Property(e => e.name)
                .IsFixedLength();

            modelBuilder.Entity<Service>()
                .Property(e => e.description)
                .IsFixedLength();

            modelBuilder.Entity<Service>()
                .HasMany(e => e.Log)
                .WithRequired(e => e.Service)
                .HasForeignKey(e => e.ID_service_FK);

            modelBuilder.Entity<Status>()
                .Property(e => e.name)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Status>()
                .HasMany(e => e.Number)
                .WithRequired(e => e.Status)
                .HasForeignKey(e => e.ID_status_FK);

            modelBuilder.Entity<Worker>()
                .Property(e => e.FIO)
                .IsFixedLength();

            modelBuilder.Entity<Worker>()
                .Property(e => e.number)
                .IsFixedLength();

            modelBuilder.Entity<Worker>()
                .Property(e => e.position)
                .IsFixedLength();

            modelBuilder.Entity<Worker>()
                .Property(e => e.passport)
                .IsFixedLength();

            modelBuilder.Entity<Worker>()
                .Property(e => e.login)
                .IsFixedLength();

            modelBuilder.Entity<Worker>()
                .Property(e => e.parol)
                .IsFixedLength();

            modelBuilder.Entity<Worker>()
                .HasMany(e => e.Uchet)
                .WithRequired(e => e.Worker)
                .HasForeignKey(e => e.ID_worker_FK)
                .WillCascadeOnDelete(false);

        }
    }
}
