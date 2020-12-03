using System;
using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace SBD.Models
{
    public partial class ModelContext : DbContext
    {
        public ModelContext()
        {
        }

        public ModelContext(DbContextOptions<ModelContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Adres> Adres { get; set; }
        public virtual DbSet<Badania> Badania { get; set; }
        public virtual DbSet<Bankkrwi> Bankkrwi { get; set; }
        public virtual DbSet<Donacja> Donacja { get; set; }
        public virtual DbSet<Donator> Donator { get; set; }
        public virtual DbSet<Kartazdrowia> Kartazdrowia { get; set; }
        public virtual DbSet<Osoba> Osoba { get; set; }
        public virtual DbSet<Pacjent> Pacjent { get; set; }
        public virtual DbSet<Pielegniarka> Pielegniarka { get; set; }
        public virtual DbSet<Transfuzja> Transfuzja { get; set; }
        public virtual DbSet<TypDonacji> TypDonacji { get; set; }
        public virtual DbSet<Worek> Worek { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseOracle("Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST=212.33.90.213)(PORT=1521))(CONNECT_DATA=(SERVICE_NAME=xe)));Persist Security Info=True;User Id=SBD_ST_PS3_3;Password=Haslo;",
                    o => o.UseOracleSQLCompatibility("11"));
                optionsBuilder.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:DefaultSchema", "SBD_ST_PS3_3");

            modelBuilder.Entity<Adres>(entity =>
            {
                entity.ToTable("ADRES");

                //entity.HasKey(e => e.Adresid);

                entity.Property(e => e.Adresid)
                    .HasColumnName("ADRESID")
                    .HasColumnType("NUMBER(6)");

                entity.Property(e => e.Kodpocztowy)
                    .HasColumnName("KODPOCZTOWY")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Miasto)
                    .HasColumnName("MIASTO")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Nrbudynku)
                    .HasColumnName("NRBUDYNKU")
                    .HasColumnType("NUMBER");

                entity.Property(e => e.Ulica)
                    .HasColumnName("ULICA")
                    .HasMaxLength(20)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Badania>(entity =>
            {
                entity.ToTable("BADANIA");

                //entity.HasKey(e => e.Kartaid);

                entity.HasIndex(e => e.Kartaid)
                    .HasName("BADANIA__IDX");

                entity.Property(e => e.Badaniaid)
                    .HasColumnName("BADANIAID")
                    .HasColumnType("NUMBER(6)")
;

                entity.Property(e => e.Cisnienie)
                    .HasColumnName("CISNIENIE")
                    .HasMaxLength(8)
                    .IsUnicode(false);

                entity.Property(e => e.Hemoglobina)
                    .HasColumnName("HEMOGLOBINA")
                    .HasColumnType("NUMBER(5,2)");

                entity.Property(e => e.Kartaid)
                    .HasColumnName("KARTAID")
                    .HasColumnType("NUMBER(6)");

                entity.Property(e => e.Temperatura)
                    .HasColumnName("TEMPERATURA")
                    .HasColumnType("NUMBER(5,2)");

                entity.Property(e => e.Tetno)
                    .HasColumnName("TETNO")
                    .HasColumnType("NUMBER");

                entity.HasOne(d => d.Karta)
                    .WithOne(p => p.Badania)
                    .HasForeignKey<Badania>(d => d.Kartaid)
                    .HasConstraintName("BADANIA_KARTAZDROWIA_FK");
            });

            modelBuilder.Entity<Bankkrwi>(entity =>
            {
                entity.HasKey(e => e.Bankid)
                    .HasName("BANKKRWI_PK");


                entity.ToTable("BANKKRWI");

                entity.HasIndex(e => e.Adresid)
                    .HasName("BANKKRWI__IDX");

                entity.Property(e => e.Bankid)
                    .HasColumnName("BANKID")
                    .HasColumnType("NUMBER(6)");

                entity.Property(e => e.Adresid)
                    .HasColumnName("ADRESID")
                    .HasColumnType("NUMBER(6)");

                entity.Property(e => e.Typkrwi)
                    .HasColumnName("TYPKRWI")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.HasOne(d => d.Adres)
                    .WithOne(p => p.Bankkrwi)
                    .HasForeignKey<Bankkrwi>(d => d.Adresid)
                    .HasConstraintName("BANKKRWI_ADRES_FK");
            });

            modelBuilder.Entity<Donacja>(entity =>
            {
                entity.ToTable("DONACJA");

                //entity.HasKey(e => e.Badaniaid);

                entity.HasIndex(e => e.Badaniaid)
                    .HasName("DONACJA__IDX");

                entity.Property(e => e.Donacjaid)
                    .HasColumnName("DONACJAID")
                    .HasColumnType("NUMBER(6)");

                entity.Property(e => e.Badaniaid)
                    .HasColumnName("BADANIAID")
                    .HasColumnType("NUMBER(6)");

                entity.Property(e => e.Datadonacji)
                    .HasColumnName("DATADONACJI")
                    .HasColumnType("DATE");

                entity.Property(e => e.Donatorid)
                    .HasColumnName("DONATORID")
                    .HasColumnType("NUMBER(6)");

                entity.Property(e => e.IloscDonacji)
                    .HasColumnName("ILOSC_DONACJI")
                    .HasColumnType("NUMBER");

                entity.Property(e => e.Pielegniarkaid)
                    .HasColumnName("PIELEGNIARKAID")
                    .HasColumnType("NUMBER(6)");

                entity.Property(e => e.Typid)
                    .HasColumnName("TYPID")
                    .HasColumnType("NUMBER(6)");

                entity.HasOne(d => d.Badania)
                    .WithOne(p => p.Donacja)
                    .HasForeignKey<Donacja>(d => d.Badaniaid)
                    .HasConstraintName("DONACJA_BADANIA_FK");

                entity.HasOne(d => d.Donator)
                    .WithMany(p => p.Donacja)
                    .HasForeignKey(d => d.Donatorid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("DONACJA_DONATOR_FK");

                entity.HasOne(d => d.Pielegniarka)
                    .WithMany(p => p.Donacja)
                    .HasForeignKey(d => d.Pielegniarkaid)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("DONACJA_PIELEGNIARKA_FK");

                entity.HasOne(d => d.Typ)
                    .WithMany(p => p.Donacja)
                    .HasForeignKey(d => d.Typid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("DONACJA_TYP_DONACJI_FK");
            });

            modelBuilder.Entity<Donator>(entity =>
            {
                entity.ToTable("DONATOR");

                entity.HasIndex(e => e.Osobaid)
                    .HasName("DONATOR__IDX");

                //entity.HasKey(x => x.Donatorid);

                entity.Property(e => e.Donatorid)
                    .HasColumnName("DONATORID")
                    .HasColumnType("NUMBER(6)");

                entity.Property(e => e.GrupaKrwi)
                    .HasColumnName("GRUPA_KRWI")
                    .HasMaxLength(3)
                    .IsUnicode(false);

                entity.Property(e => e.Nastepnadonacja)
                    .HasColumnName("NASTEPNADONACJA")
                    .HasColumnType("DATE");

                entity.Property(e => e.Osobaid)
                    .HasColumnName("OSOBAID")
                    .HasColumnType("NUMBER(6)");

                entity.Property(e => e.Rh)
                    .HasColumnName("RH")
                    .HasMaxLength(1)
                    .IsUnicode(false);

                entity.Property(e => e.Waga)
                    .HasColumnName("WAGA")
                    .HasColumnType("NUMBER");

                entity.Property(e => e.Wzrost)
                    .HasColumnName("WZROST")
                    .HasColumnType("NUMBER");

                entity.HasOne(d => d.Osoba)
                    .WithOne(p => p.Donator)
                    .HasForeignKey<Donator>(d => d.Osobaid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("DONATOR_OSOBA_FK");
            });

            modelBuilder.Entity<Kartazdrowia>(entity =>
            {
                entity.HasKey(e => e.Kartaid)
                    .HasName("KARTAZDROWIA_PK");

                entity.ToTable("KARTAZDROWIA");

                entity.Property(e => e.Kartaid)
                    .HasColumnName("KARTAID")
                    .HasColumnType("NUMBER(6)");

                entity.Property(e => e.Hiv)
                    .HasColumnName("HIV")
                    .HasColumnType("CHAR(1)");

                entity.Property(e => e.Malaria)
                    .HasColumnName("MALARIA")
                    .HasColumnType("CHAR(1)");

                entity.Property(e => e.Syfilis)
                    .HasColumnName("SYFILIS")
                    .HasColumnType("CHAR(1)");

                entity.Property(e => e.Zapaleniewatrobyb)
                    .HasColumnName("ZAPALENIEWATROBYB")
                    .HasColumnType("CHAR(1)");

                entity.Property(e => e.Zapaleniewatrobyc)
                    .HasColumnName("ZAPALENIEWATROBYC")
                    .HasColumnType("CHAR(1)");
            });

            modelBuilder.Entity<Osoba>(entity =>
            {
                entity.ToTable("OSOBA");

                //entity.HasKey(e => e.Osobaid);
                entity.Property(e => e.Osobaid)
                    .HasColumnName("OSOBAID")
                    .HasColumnType("NUMBER(6)");

                entity.Property(e => e.DataUrodzenia)
                    .HasColumnName("DATA_URODZENIA")
                    .HasColumnType("DATE");

                entity.Property(e => e.Imie)
                    .HasColumnName("IMIE")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Nazwisko)
                    .HasColumnName("NAZWISKO")
                    .HasMaxLength(30)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Pacjent>(entity =>
            {
                entity.ToTable("PACJENT");

                entity.HasIndex(e => e.Osobaid)
                    .HasName("PACJENT__IDX");

                //entity.HasKey(x => x.Pacjentid);

                entity.Property(e => e.Pacjentid)
                    .HasColumnName("PACJENTID")
                    .HasColumnType("NUMBER(6)");

                entity.Property(e => e.GrupaKrwi)
                    .HasColumnName("GRUPA_KRWI")
                    .HasMaxLength(3)
                    .IsUnicode(false);

                entity.Property(e => e.Osobaid)
                    .HasColumnName("OSOBAID")
                    .HasColumnType("NUMBER(6)");

                entity.Property(e => e.Priorytet)
                    .HasColumnName("PRIORYTET")
                    .HasMaxLength(6)
                    .IsUnicode(false);

                entity.Property(e => e.Waga)
                    .HasColumnName("WAGA")
                    .HasColumnType("NUMBER");

                entity.HasOne(d => d.Osoba)
                    .WithOne(p => p.Pacjent)
                    .HasForeignKey<Pacjent>(d => d.Osobaid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("PACJENT_OSOBA_FK");
            });

            modelBuilder.Entity<Pielegniarka>(entity =>
            {
                entity.ToTable("PIELEGNIARKA");

                entity.HasIndex(e => e.Osobaid)
                    .HasName("PIELEGNIARKA__IDX");

                //entity.HasKey(x => x.Pielegniarkaid);
                entity.Property(e => e.Pielegniarkaid)
                    .HasColumnName("PIELEGNIARKAID")
                    .HasColumnType("NUMBER(6)");

                entity.Property(e => e.Doswiadczenie)
                    .HasColumnName("DOSWIADCZENIE")
                    .HasColumnType("NUMBER");

                entity.Property(e => e.Osobaid)
                    .HasColumnName("OSOBAID")
                    .HasColumnType("NUMBER(6)");

                entity.HasOne(d => d.Osoba)
                    .WithOne(p => p.Pielegniarka)
                    .HasForeignKey<Pielegniarka>(d => d.Osobaid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("PIELEGNIARKA_OSOBA_FK");
            });

            modelBuilder.Entity<Transfuzja>(entity =>
            {
                entity.ToTable("TRANSFUZJA");

                entity.HasIndex(e => e.Badaniaid)
                    .HasName("TRANSFUZJA__IDX");

                //entity.HasKey(x => x.Transfuzjaid);

                entity.Property(e => e.Transfuzjaid)
                    .HasColumnName("TRANSFUZJAID")
                    .HasColumnType("NUMBER(6)");

                entity.Property(e => e.Badaniaid)
                    .HasColumnName("BADANIAID")
                    .HasColumnType("NUMBER(6)");

                entity.Property(e => e.DataTransfuzji)
                    .HasColumnName("DATA_TRANSFUZJI")
                    .HasColumnType("DATE");

                entity.Property(e => e.Pacjentid)
                    .HasColumnName("PACJENTID")
                    .HasColumnType("NUMBER(6)");

                entity.Property(e => e.Pielegniarkaid)
                    .HasColumnName("PIELEGNIARKAID")
                    .HasColumnType("NUMBER(6)");

                entity.Property(e => e.PotrzebnaIlosc)
                    .HasColumnName("POTRZEBNA_ILOSC")
                    .HasColumnType("NUMBER(5,2)");

                entity.HasOne(d => d.Badania)
                    .WithOne(p => p.Transfuzja)
                    .HasForeignKey<Transfuzja>(d => d.Badaniaid)
                    .HasConstraintName("TRANSFUZJA_BADANIA_FK");

                entity.HasOne(d => d.Pacjent)
                    .WithMany(p => p.Transfuzja)
                    .HasForeignKey(d => d.Pacjentid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("TRANSFUZJA_PACJENT_FK");

                entity.HasOne(d => d.Pielegniarka)
                    .WithMany(p => p.Transfuzja)
                    .HasForeignKey(d => d.Pielegniarkaid)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("TRANSFUZJA_PIELEGNIARKA_FK");
            });

            modelBuilder.Entity<TypDonacji>(entity =>
            {
                entity.HasKey(e => e.Typid)
                    .HasName("TYP_DONACJI_PK");

                entity.ToTable("TYP_DONACJI");

                //entity.HasKey(e => e.Typid);

                entity.Property(e => e.Typid)
                    .HasColumnName("TYPID")
                    .HasColumnType("NUMBER(6)");

                entity.Property(e => e.Czestotliwosc)
                    .HasColumnName("CZESTOTLIWOSC")
                    .HasColumnType("NUMBER");

                entity.Property(e => e.Typ)
                    .HasColumnName("TYP")
                    .HasMaxLength(20)
                    .IsUnicode(false);
            });


            modelBuilder.Entity<Worek>(entity =>
            {
                entity.ToTable("WOREK");
                

                //entity.HasKey(e => e.Worekid);

                entity.Property(e => e.Worekid)
                    .HasColumnName("WOREKID")
                    .HasColumnType("NUMBER(6)");

                entity.Property(e => e.Bankid)
                    .HasColumnName("BANKID")
                    .HasColumnType("NUMBER(6)");

                entity.Property(e => e.Donacjaid)
                    .HasColumnName("DONACJAID")
                    .HasColumnType("NUMBER(6)");

                entity.Property(e => e.Grupakrwi)
                    .HasColumnName("GRUPAKRWI")
                    .HasMaxLength(2)
                    .IsUnicode(false);

                entity.Property(e => e.Rh)
                    .HasColumnName("RH")
                    .HasMaxLength(1)
                    .IsUnicode(false);

                entity.Property(e => e.Transfuzjaid)
                    .HasColumnName("TRANSFUZJAID")
                    .HasColumnType("NUMBER(6)");

                entity.Property(e => e.Wielkosc)
                    .HasColumnName("WIELKOSC")
                    .HasColumnType("NUMBER(5,2)");

                entity.HasOne(d => d.Bank)
                    .WithMany(p => p.Worek)
                    .HasForeignKey(d => d.Bankid)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("WOREK_BANKKRWI_FK");

                entity.HasOne(d => d.Donacja)
                    .WithMany(p => p.Worek)
                    .HasForeignKey(d => d.Donacjaid)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("WOREK_DONACJA_FK");

                entity.HasOne(d => d.Transfuzja)
                    .WithMany(p => p.Worek)
                    .HasForeignKey(d => d.Transfuzjaid)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("WOREK_TRANSFUZJA_FK");
            });
            #region sekwencje

            modelBuilder.HasSequence("ADRES_SQ");

            modelBuilder.HasSequence("BADANIA_SQ");

            modelBuilder.HasSequence("BANKKRWI_SQ");

            modelBuilder.HasSequence("DONACJA_SQ");

            modelBuilder.HasSequence("DONATOR_SQ");

            modelBuilder.HasSequence("KARTAZDROWIA_SQ");

            modelBuilder.HasSequence("OSOBA_SQ");

            modelBuilder.HasSequence("PACJENT_SQ");

            modelBuilder.HasSequence("PIELEGNIARKA_SQ");

            modelBuilder.HasSequence("TRANSFUZJA_SQ");

            modelBuilder.HasSequence("TYP_DONACJI_SQ");

            modelBuilder.HasSequence("WOREK_SQ");

            OnModelCreatingPartial(modelBuilder);
            #endregion
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
