using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Bachelor_Server.Models
{
    public partial class BachelorDBContext : DbContext
    {
        public BachelorDBContext()
        {
        }

        public BachelorDBContext(DbContextOptions<BachelorDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Account> Accounts { get; set; } = null!;
        public virtual DbSet<Apikey> Apikeys { get; set; } = null!;
        public virtual DbSet<BasicAuth> BasicAuths { get; set; } = null!;
        public virtual DbSet<BearerToken> BearerTokens { get; set; } = null!;
        public virtual DbSet<FormDatum> FormData { get; set; } = null!;
        public virtual DbSet<Header> Headers { get; set; } = null!;
        public virtual DbSet<Log> Logs { get; set; } = null!;
        public virtual DbSet<Oauth20> Oauth20s { get; set; } = null!;
        public virtual DbSet<Parameter> Parameters { get; set; } = null!;
        public virtual DbSet<Raw> Raws { get; set; } = null!;
        public virtual DbSet<WorkerConfiguration> WorkerConfigurations { get; set; } = null!;
        public virtual DbSet<WorkerStatistic> WorkerStatistics { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=tcp:bachelorserver2022.database.windows.net,1433;Initial Catalog=BachelorDB;Persist Security Info=False;User ID=bacheloradmin;Password=Hello1234_;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Account>(entity =>
            {
                entity.HasKey(e => e.PkAccountId)
                    .HasName("PK__Account__FEE2E2F623604773");

                entity.ToTable("Account");

                entity.Property(e => e.PkAccountId).HasColumnName("PK_AccountID");

                entity.Property(e => e.DisplayName).HasMaxLength(1000);

                entity.Property(e => e.Email).HasMaxLength(100);

                entity.Property(e => e.Password).HasMaxLength(100);

                entity.Property(e => e.Type).HasMaxLength(100);
            });

            modelBuilder.Entity<Apikey>(entity =>
            {
                entity.HasKey(e => e.PkApikeyId)
                    .HasName("PK__APIKey__83305EDEDAB9FA0F");

                entity.ToTable("APIKey");

                entity.Property(e => e.PkApikeyId).HasColumnName("PK_APIKeyID");

                entity.Property(e => e.AddTo).HasMaxLength(100);

                entity.Property(e => e.Key).HasMaxLength(100);

                entity.Property(e => e.Value).HasMaxLength(100);
            });

            modelBuilder.Entity<BasicAuth>(entity =>
            {
                entity.HasKey(e => e.PkBasicAuthId)
                    .HasName("PK__BasicAut__25C14421CA2D8588");

                entity.ToTable("BasicAuth");

                entity.Property(e => e.PkBasicAuthId).HasColumnName("PK_BasicAuthID");

                entity.Property(e => e.Password)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Username)
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<BearerToken>(entity =>
            {
                entity.HasKey(e => e.PkBearerTokenId)
                    .HasName("PK__BearerTo__2C1B738E89CF3432");

                entity.ToTable("BearerToken");

                entity.Property(e => e.PkBearerTokenId).HasColumnName("PK_BearerTokenID");

                entity.Property(e => e.Token)
                    .HasMaxLength(1000)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<FormDatum>(entity =>
            {
                entity.HasKey(e => e.PkFormDataId)
                    .HasName("PK__FormData__F56ACFAAFE743363");

                entity.Property(e => e.PkFormDataId).HasColumnName("PK_FormDataID");

                entity.Property(e => e.Description).HasMaxLength(1000);

                entity.Property(e => e.FkWorkerConfigurationId).HasColumnName("FK_WorkerConfigurationID");

                entity.Property(e => e.Key).HasMaxLength(100);

                entity.Property(e => e.Value).HasMaxLength(100);

                entity.HasOne(d => d.FkWorkerConfiguration)
                    .WithMany(p => p.FormData)
                    .HasForeignKey(d => d.FkWorkerConfigurationId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__FormData__FK_Wor__08B54D69");
            });

            modelBuilder.Entity<Header>(entity =>
            {
                entity.HasKey(e => e.PkHeaderId)
                    .HasName("PK__Header__F47C01B12EA3BCC4");

                entity.ToTable("Header");

                entity.Property(e => e.PkHeaderId).HasColumnName("PK_HeaderID");

                entity.Property(e => e.Description).HasMaxLength(1000);

                entity.Property(e => e.FkWorkerConfigurationId).HasColumnName("FK_WorkerConfigurationID");

                entity.Property(e => e.Key).HasMaxLength(100);

                entity.Property(e => e.Value).HasMaxLength(100);

                entity.HasOne(d => d.FkWorkerConfiguration)
                    .WithMany(p => p.Headers)
                    .HasForeignKey(d => d.FkWorkerConfigurationId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Header__Descript__7D439ABD");
            });

            modelBuilder.Entity<Log>(entity =>
            {
                entity.HasKey(e => e.PkLogId)
                    .HasName("PK__Logs__93AA50A34F0B84CE");

                entity.Property(e => e.PkLogId).HasColumnName("PK_LogID");

                entity.Property(e => e.Date).HasMaxLength(100);

                entity.Property(e => e.StackTrace).HasColumnName("Stack Trace");
            });

            modelBuilder.Entity<Oauth20>(entity =>
            {
                entity.HasKey(e => e.PkOauth20id)
                    .HasName("PK__OAuth2.0__F5DD92D1A6951515");

                entity.ToTable("OAuth2.0");

                entity.Property(e => e.PkOauth20id).HasColumnName("PK_OAuth2.0ID");

                entity.Property(e => e.AccessToken)
                    .HasMaxLength(1000)
                    .IsUnicode(false);

                entity.Property(e => e.HeaderPrefix)
                    .HasMaxLength(1000)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Parameter>(entity =>
            {
                entity.HasKey(e => e.PkParameterId)
                    .HasName("PK__Paramete__BB78C8B660418636");

                entity.ToTable("Parameter");

                entity.Property(e => e.PkParameterId).HasColumnName("PK_ParameterID");

                entity.Property(e => e.Description).HasMaxLength(1000);

                entity.Property(e => e.FkWorkerConfigurationId).HasColumnName("FK_WorkerConfigurationID");

                entity.Property(e => e.Key).HasMaxLength(100);

                entity.Property(e => e.Value).HasMaxLength(100);

                entity.HasOne(d => d.FkWorkerConfiguration)
                    .WithMany(p => p.Parameters)
                    .HasForeignKey(d => d.FkWorkerConfigurationId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Parameter__FK_Wo__00200768");
            });

            modelBuilder.Entity<Raw>(entity =>
            {
                entity.HasKey(e => e.PkRawId)
                    .HasName("PK__Raw__475314EE50113D3A");

                entity.ToTable("Raw");

                entity.Property(e => e.PkRawId).HasColumnName("PK_RawID");

                entity.Property(e => e.Text).IsUnicode(false);
            });

            modelBuilder.Entity<WorkerConfiguration>(entity =>
            {
                entity.HasKey(e => e.PkWorkerConfigurationId)
                    .HasName("PK__WorkerCo__6A41E2B7C8D0FB21");

                entity.ToTable("WorkerConfiguration");

                entity.Property(e => e.PkWorkerConfigurationId).HasColumnName("PK_WorkerConfigurationID");

                entity.Property(e => e.FkApikeyId).HasColumnName("FK_APIKeyID");

                entity.Property(e => e.FkBasicAuthId).HasColumnName("FK_BasicAuthID");

                entity.Property(e => e.FkBearerTokenId).HasColumnName("FK_BearerTokenID");

                entity.Property(e => e.FkOauth20id).HasColumnName("FK_OAuth2.0ID");

                entity.Property(e => e.FkRawId).HasColumnName("FK_RawID");

                entity.Property(e => e.LastSavedAuth)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.LastSavedBody)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.RequestType)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ScheduleRate).HasMaxLength(100);

                entity.Property(e => e.StartDate).HasColumnType("datetime");

                entity.Property(e => e.Url)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("URL");

                entity.HasOne(d => d.FkApikey)
                    .WithMany(p => p.WorkerConfigurations)
                    .HasForeignKey(d => d.FkApikeyId)
                    .HasConstraintName("FK__WorkerCon__FK_AP__6D0D32F4");

                entity.HasOne(d => d.FkBasicAuth)
                    .WithMany(p => p.WorkerConfigurations)
                    .HasForeignKey(d => d.FkBasicAuthId)
                    .HasConstraintName("FK__WorkerCon__FK_Ba__6B24EA82");

                entity.HasOne(d => d.FkBearerToken)
                    .WithMany(p => p.WorkerConfigurations)
                    .HasForeignKey(d => d.FkBearerTokenId)
                    .HasConstraintName("FK__WorkerCon__FK_Be__6C190EBB");

                entity.HasOne(d => d.FkOauth20)
                    .WithMany(p => p.WorkerConfigurations)
                    .HasForeignKey(d => d.FkOauth20id)
                    .HasConstraintName("FK__WorkerCon__FK_OA__6EF57B66");

                entity.HasOne(d => d.FkRaw)
                    .WithMany(p => p.WorkerConfigurations)
                    .HasForeignKey(d => d.FkRawId)
                    .HasConstraintName("FK__WorkerCon__FK_Ra__6A30C649");
            });

            modelBuilder.Entity<WorkerStatistic>(entity =>
            {
                entity.HasKey(e => e.PkWorkerStatisticsId)
                    .HasName("PK__WorkerSt__960072C56F60390E");

                entity.Property(e => e.PkWorkerStatisticsId).HasColumnName("PK_WorkerStatisticsID");

                entity.Property(e => e.EndTime).HasColumnType("datetime");

                entity.Property(e => e.FkWorkerConfigurationId).HasColumnName("FK_WorkerConfigurationID");

                entity.Property(e => e.StartTime).HasColumnType("datetime");

                entity.Property(e => e.Status).HasMaxLength(100);

                entity.HasOne(d => d.FkWorkerConfiguration)
                    .WithMany(p => p.WorkerStatistics)
                    .HasForeignKey(d => d.FkWorkerConfigurationId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__WorkerS__FK_WorkerC");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
