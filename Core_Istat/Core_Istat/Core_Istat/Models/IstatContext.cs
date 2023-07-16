using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Core_Istat.Models
{
    public partial class IstatContext : DbContext
    {
        public IstatContext()
        {
        }

        public IstatContext(DbContextOptions<IstatContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Comune> Comunes { get; set; }
        public virtual DbSet<Provincium> Provincia { get; set; }
        public virtual DbSet<Regione> Regiones { get; set; }
        public virtual DbSet<RipartizioneGeografica> RipartizioneGeograficas { get; set; }
        public virtual DbSet<ZonaAltimetrica> ZonaAltimetricas { get; set; }
        public virtual DbSet<ZonaMontana> ZonaMontanas { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=localhost;Database=Istat;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Comune>(entity =>
            {
                entity.ToTable("Comune");

                entity.Property(e => e.CodiceCatastale)
                    .HasMaxLength(4)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.Denominazione)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.IdZonaMontana)
                    .HasMaxLength(2)
                    .IsUnicode(false);

                entity.HasOne(d => d.IdProvinciaNavigation)
                    .WithMany(p => p.Comunes)
                    .HasForeignKey(d => d.IdProvincia)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Comune_Provincia");

                entity.HasOne(d => d.IdZonaAltimetricaNavigation)
                    .WithMany(p => p.Comunes)
                    .HasForeignKey(d => d.IdZonaAltimetrica)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Comune_ZonaAltimetrica");

                entity.HasOne(d => d.IdZonaMontanaNavigation)
                    .WithMany(p => p.Comunes)
                    .HasForeignKey(d => d.IdZonaMontana)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Comune_ZonaMontana");
            });

            modelBuilder.Entity<Provincium>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Denominazione)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Sigla)
                    .HasMaxLength(2)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.HasOne(d => d.IdRegioneNavigation)
                    .WithMany(p => p.Provincia)
                    .HasForeignKey(d => d.IdRegione)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Provincia_Regione");
            });

            modelBuilder.Entity<Regione>(entity =>
            {
                entity.ToTable("Regione");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Denominazione)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.HasOne(d => d.IdRipartizioneNavigation)
                    .WithMany(p => p.Regiones)
                    .HasForeignKey(d => d.IdRipartizione)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Regione_RipartizioneGeografica");
            });

            modelBuilder.Entity<RipartizioneGeografica>(entity =>
            {
                entity.ToTable("RipartizioneGeografica");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Denominazione)
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<ZonaAltimetrica>(entity =>
            {
                entity.ToTable("ZonaAltimetrica");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Denominazione)
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<ZonaMontana>(entity =>
            {
                entity.ToTable("ZonaMontana");

                entity.Property(e => e.Id)
                    .HasMaxLength(2)
                    .IsUnicode(false);

                entity.Property(e => e.Denominazione)
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
