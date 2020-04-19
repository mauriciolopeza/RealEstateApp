using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace RealEstateApp.Models
{
    public partial class RealEstate_DBContext : DbContext
    {
        public RealEstate_DBContext()
        {
        }

        public RealEstate_DBContext(DbContextOptions<RealEstate_DBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<CommunicationLog> CommunicationLog { get; set; }
        public virtual DbSet<Companies> Companies { get; set; }
        public virtual DbSet<ContactInterests> ContactInterests { get; set; }
        public virtual DbSet<Contacts> Contacts { get; set; }
        public virtual DbSet<Interests> Interests { get; set; }
        public virtual DbSet<Listings> Listings { get; set; }
        public virtual DbSet<Networks> Networks { get; set; }
        public virtual DbSet<Notifications> Notifications { get; set; }
        public virtual DbSet<RouteListings> RouteListings { get; set; }
        public virtual DbSet<Routes> Routes { get; set; }
        public virtual DbSet<Tasks> Tasks { get; set; }
        public virtual DbSet<Users> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                #warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseMySQL("server=192.168.86.10;port=3306;user=root;password=mla@6842;database=RealState_DB");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.4-servicing-10062");

            modelBuilder.Entity<CommunicationLog>(entity =>
            {
                entity.ToTable("communication_log", "realstate_db");

                entity.Property(e => e.CommunicationLogId)
                    .HasColumnName("communication_log_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.CommunicationDate).HasColumnName("communication_date");

                entity.Property(e => e.CommunicationType)
                    .IsRequired()
                    .HasColumnName("communication_type")
                    .HasMaxLength(45)
                    .IsUnicode(false);

                entity.Property(e => e.Notes)
                    .HasColumnName("notes")
                    .HasColumnType("longtext");
            });

            modelBuilder.Entity<Companies>(entity =>
            {
                entity.ToTable("companies", "realstate_db");

                entity.Property(e => e.CompaniesId)
                    .HasColumnName("companies_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Address)
                    .HasColumnName("address")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.AlternateNumber)
                    .HasColumnName("alternate_number")
                    .HasMaxLength(45)
                    .IsUnicode(false);

                entity.Property(e => e.City)
                    .HasColumnName("city")
                    .HasMaxLength(45)
                    .IsUnicode(false);

                entity.Property(e => e.CompanyName)
                    .IsRequired()
                    .HasColumnName("company_name")
                    .HasMaxLength(45)
                    .IsUnicode(false);

                entity.Property(e => e.Department)
                    .HasColumnName("department")
                    .HasMaxLength(45)
                    .IsUnicode(false);

                entity.Property(e => e.LogoUrl)
                    .HasColumnName("logo_url")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.PhoneNumber)
                    .HasColumnName("phone_number")
                    .HasMaxLength(45)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<ContactInterests>(entity =>
            {
                entity.ToTable("contact_interests", "realstate_db");

                entity.HasIndex(e => e.FkContactsId)
                    .HasName("fk_contact_interests_contacts_idx");

                entity.HasIndex(e => e.FkInterestId)
                    .HasName("fk_contact_interests_interests_idx");

                entity.Property(e => e.ContactInterestsId)
                    .HasColumnName("contact_interests_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.FkContactsId)
                    .HasColumnName("fk_contacts_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.FkInterestId)
                    .HasColumnName("fk_interest_id")
                    .HasColumnType("int(11)");

                entity.HasOne(d => d.FkContacts)
                    .WithMany(p => p.ContactInterests)
                    .HasForeignKey(d => d.FkContactsId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_contact_interests_contacts");

                entity.HasOne(d => d.FkInterest)
                    .WithMany(p => p.ContactInterests)
                    .HasForeignKey(d => d.FkInterestId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_contact_interests_interests");
            });

            modelBuilder.Entity<Contacts>(entity =>
            {
                entity.ToTable("contacts", "realstate_db");

                entity.HasIndex(e => e.FkCompaniesId)
                    .HasName("fk_contacts_companies_idx");

                entity.Property(e => e.ContactsId)
                    .HasColumnName("contacts_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.AlternateNumber)
                    .HasColumnName("alternate_number")
                    .HasMaxLength(45)
                    .IsUnicode(false);

                entity.Property(e => e.ContactLastNames)
                    .IsRequired()
                    .HasColumnName("contact_last_names")
                    .HasMaxLength(65)
                    .IsUnicode(false);

                entity.Property(e => e.ContactNames)
                    .IsRequired()
                    .HasColumnName("contact_names")
                    .HasMaxLength(65)
                    .IsUnicode(false);

                entity.Property(e => e.ContactType)
                    .IsRequired()
                    .HasColumnName("contact_type")
                    .HasMaxLength(45)
                    .IsUnicode(false);

                entity.Property(e => e.EmailAddress)
                    .HasColumnName("email_address")
                    .HasMaxLength(45)
                    .IsUnicode(false);

                entity.Property(e => e.FkCompaniesId)
                    .HasColumnName("fk_companies_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.IdNumber)
                    .HasColumnName("id_number")
                    .HasMaxLength(25)
                    .IsUnicode(false);

                entity.Property(e => e.PhoneNumber)
                    .HasColumnName("phone_number")
                    .HasMaxLength(45)
                    .IsUnicode(false);

                entity.HasOne(d => d.FkCompanies)
                    .WithMany(p => p.Contacts)
                    .HasForeignKey(d => d.FkCompaniesId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_contacts_companies");
            });

            modelBuilder.Entity<Interests>(entity =>
            {
                entity.ToTable("interests", "realstate_db");

                entity.HasIndex(e => e.FkCompaniesId)
                    .HasName("fk_interests_companies_idx");

                entity.Property(e => e.InterestsId)
                    .HasColumnName("interests_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Description)
                    .HasColumnName("description")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.FkCompaniesId)
                    .HasColumnName("fk_companies_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.InterestName)
                    .IsRequired()
                    .HasColumnName("interest_name")
                    .HasMaxLength(45)
                    .IsUnicode(false);

                entity.HasOne(d => d.FkCompanies)
                    .WithMany(p => p.Interests)
                    .HasForeignKey(d => d.FkCompaniesId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_interests_companies");
            });

            modelBuilder.Entity<Listings>(entity =>
            {
                entity.ToTable("listings", "realstate_db");

                entity.HasIndex(e => e.FkCompaniesId)
                    .HasName("fk_listings_companies_idx");

                entity.HasIndex(e => e.FkContactsId)
                    .HasName("fk_listings_contacts_idx");

                entity.Property(e => e.ListingsId)
                    .HasColumnName("listings_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Address)
                    .HasColumnName("address")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.AdministrationFee)
                    .HasColumnName("administration_fee")
                    .HasColumnType("decimal(19,2)");

                entity.Property(e => e.AreaSize)
                    .HasColumnName("area_size")
                    .HasColumnType("int(11)");

                entity.Property(e => e.BloqueNumber)
                    .HasColumnName("bloque_number")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.City)
                    .HasColumnName("city")
                    .HasMaxLength(45)
                    .IsUnicode(false);

                entity.Property(e => e.Department)
                    .HasColumnName("department")
                    .HasMaxLength(45)
                    .IsUnicode(false);

                entity.Property(e => e.Estrato)
                    .HasColumnName("estrato")
                    .HasMaxLength(11)
                    .IsUnicode(false);

                entity.Property(e => e.FkCompaniesId)
                    .HasColumnName("fk_companies_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.FkContactsId)
                    .HasColumnName("fk_contacts_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.HasBalcony)
                    .HasColumnName("has_balcony")
                    .HasColumnType("bit(1)");

                entity.Property(e => e.HasElevator)
                    .HasColumnName("has_elevator")
                    .HasColumnType("bit(1)");

                entity.Property(e => e.HasMaidRoom)
                    .HasColumnName("has_maid_room")
                    .HasColumnType("bit(1)");

                entity.Property(e => e.HasOpenKitchen)
                    .HasColumnName("has_open_kitchen")
                    .HasColumnType("bit(1)");

                entity.Property(e => e.HasPool)
                    .HasColumnName("has_pool")
                    .HasColumnType("bit(1)");

                entity.Property(e => e.IsClosedUrbanization)
                    .HasColumnName("is_closed_urbanization")
                    .HasColumnType("bit(1)");

                entity.Property(e => e.IsSmlsPromoted)
                    .HasColumnName("is_smls_promoted")
                    .HasColumnType("bit(1)");

                entity.Property(e => e.IsStudio)
                    .HasColumnName("is_studio")
                    .HasColumnType("bit(1)");

                entity.Property(e => e.IsTuNuevoEspacioPromoted)
                    .HasColumnName("is_tu_nuevo_espacio_promoted")
                    .HasColumnType("bit(1)");

                entity.Property(e => e.ListingNumber)
                    .IsRequired()
                    .HasColumnName("listing_number")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ListingType)
                    .IsRequired()
                    .HasColumnName("listing_type")
                    .HasMaxLength(45)
                    .IsUnicode(false);

                entity.Property(e => e.Neighborhood)
                    .HasColumnName("neighborhood")
                    .HasMaxLength(45)
                    .IsUnicode(false);

                entity.Property(e => e.NumberOfBathrooms)
                    .HasColumnName("number_of_bathrooms")
                    .HasColumnType("int(11)");

                entity.Property(e => e.NumberOfRooms)
                    .HasColumnName("number_of_rooms")
                    .HasColumnType("int(11)");

                entity.Property(e => e.ParkingSpaces)
                    .HasColumnName("parking_spaces")
                    .HasColumnType("int(11)");

                entity.Property(e => e.SalesPrice)
                    .HasColumnName("sales_price")
                    .HasColumnType("decimal(19,2)");

                entity.Property(e => e.SmlsNumber)
                    .HasColumnName("smls_number")
                    .HasMaxLength(45)
                    .IsUnicode(false);

                entity.Property(e => e.TuNuevoEspacioNumber)
                    .HasColumnName("tu_nuevo_espacio_number")
                    .HasMaxLength(45)
                    .IsUnicode(false);

                entity.Property(e => e.UnitNumber)
                    .HasColumnName("unit_number")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.UrbanizationName)
                    .HasColumnName("urbanization_name")
                    .HasMaxLength(45)
                    .IsUnicode(false);

                entity.Property(e => e.YearBuilt)
                    .HasColumnName("year_built")
                    .HasColumnType("int(11)");

                entity.HasOne(d => d.FkCompanies)
                    .WithMany(p => p.Listings)
                    .HasForeignKey(d => d.FkCompaniesId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_listings_companies");

                entity.HasOne(d => d.FkContacts)
                    .WithMany(p => p.Listings)
                    .HasForeignKey(d => d.FkContactsId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_listings_contacts");
            });

            modelBuilder.Entity<Networks>(entity =>
            {
                entity.HasKey(e => e.NetworksId);

                entity.ToTable("networks", "realstate_db");

                entity.HasIndex(e => e.FkCompaniesId)
                    .HasName("fk_networks_companies_idx");

                entity.HasIndex(e => e.FkUsersId)
                    .HasName("fk_networks_users_idx");

                entity.Property(e => e.NetworksId)
                    .HasColumnName("networks_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.FkCompaniesId)
                    .HasColumnName("fk_companies_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.FkUsersId)
                    .HasColumnName("fk_users_id")
                    .HasColumnType("int(11)");

                entity.HasOne(d => d.FkCompanies)
                    .WithMany(p => p.Networks)
                    .HasForeignKey(d => d.FkCompaniesId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_networks_companies");

                entity.HasOne(d => d.FkUsers)
                    .WithMany(p => p.Networks)
                    .HasForeignKey(d => d.FkUsersId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_networks_users");
            });

            modelBuilder.Entity<Notifications>(entity =>
            {
                entity.ToTable("notifications", "realstate_db");

                entity.HasIndex(e => e.FkUsersId)
                    .HasName("fk_notifications_users_idx");

                entity.Property(e => e.NotificationsId)
                    .HasColumnName("notifications_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.FkUsersId)
                    .HasColumnName("fk_users_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Notification)
                    .IsRequired()
                    .HasColumnName("notification")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.NotificationDatetime).HasColumnName("notification_datetime");

                entity.HasOne(d => d.FkUsers)
                    .WithMany(p => p.Notifications)
                    .HasForeignKey(d => d.FkUsersId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_notifications_users");
            });

            modelBuilder.Entity<RouteListings>(entity =>
            {
                entity.ToTable("route_listings", "realstate_db");

                entity.HasIndex(e => e.FkListingsId)
                    .HasName("fk_route_listings_id_idx");

                entity.HasIndex(e => e.FkRoutesId)
                    .HasName("fk_route_listings_routes_idx");

                entity.Property(e => e.RouteListingsId)
                    .HasColumnName("route_listings_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.FkListingsId)
                    .HasColumnName("fk_listings_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.FkRoutesId)
                    .HasColumnName("fk_routes_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.ScheduledTime).HasColumnName("scheduled_time");

                entity.HasOne(d => d.FkListings)
                    .WithMany(p => p.RouteListings)
                    .HasForeignKey(d => d.FkListingsId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_route_listings_listings");

                entity.HasOne(d => d.FkRoutes)
                    .WithMany(p => p.RouteListings)
                    .HasForeignKey(d => d.FkRoutesId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_route_listings_routes");
            });

            modelBuilder.Entity<Routes>(entity =>
            {
                entity.ToTable("routes", "realstate_db");

                entity.HasIndex(e => e.FkContactsId)
                    .HasName("fk_routes_contacts_idx");

                entity.HasIndex(e => e.FkUsersId)
                    .HasName("fk_routes_users_idx");

                entity.Property(e => e.RoutesId)
                    .HasColumnName("routes_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.FkContactsId)
                    .HasColumnName("fk_contacts_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.FkUsersId)
                    .HasColumnName("fk_users_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.RouteDate).HasColumnName("route_date");

                entity.Property(e => e.RouteName)
                    .IsRequired()
                    .HasColumnName("route_name")
                    .HasMaxLength(45)
                    .IsUnicode(false);

                entity.HasOne(d => d.FkContacts)
                    .WithMany(p => p.Routes)
                    .HasForeignKey(d => d.FkContactsId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_routes_contacts");

                entity.HasOne(d => d.FkUsers)
                    .WithMany(p => p.Routes)
                    .HasForeignKey(d => d.FkUsersId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_routes_users");
            });

            modelBuilder.Entity<Tasks>(entity =>
            {
                entity.ToTable("tasks", "realstate_db");

                entity.HasIndex(e => e.FkUsersId)
                    .HasName("fk_tasks_users_idx");

                entity.Property(e => e.TasksId)
                    .HasColumnName("tasks_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Description)
                    .HasColumnName("description")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.FkUsersId)
                    .HasColumnName("fk_users_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.TaskDate).HasColumnName("task_date");

                entity.Property(e => e.TaskName)
                    .IsRequired()
                    .HasColumnName("task_name")
                    .HasMaxLength(45)
                    .IsUnicode(false);

                entity.Property(e => e.TaskReminderDate).HasColumnName("task_reminder_date");

                entity.HasOne(d => d.FkUsers)
                    .WithMany(p => p.Tasks)
                    .HasForeignKey(d => d.FkUsersId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_tasks_users");
            });

            modelBuilder.Entity<Users>(entity =>
            {
                entity.ToTable("users", "realstate_db");

                entity.HasIndex(e => e.FkCompaniesId)
                    .HasName("fk_users_companies_idx");

                entity.Property(e => e.UsersId)
                    .HasColumnName("users_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.AlternateNumber)
                    .HasColumnName("alternate_number")
                    .HasMaxLength(45)
                    .IsUnicode(false);

                entity.Property(e => e.EmailAddress)
                    .HasColumnName("email_address")
                    .HasMaxLength(45)
                    .IsUnicode(false);

                entity.Property(e => e.FkCompaniesId)
                    .HasColumnName("fk_companies_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.LastNames)
                    .IsRequired()
                    .HasColumnName("last_names")
                    .HasMaxLength(55)
                    .IsUnicode(false);

                entity.Property(e => e.Names)
                    .IsRequired()
                    .HasColumnName("names")
                    .HasMaxLength(55)
                    .IsUnicode(false);

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasColumnName("password")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.PhoneNumber)
                    .HasColumnName("phone_number")
                    .HasMaxLength(45)
                    .IsUnicode(false);

                entity.Property(e => e.UserName)
                    .IsRequired()
                    .HasColumnName("user_name")
                    .HasMaxLength(25)
                    .IsUnicode(false);

                entity.HasOne(d => d.FkCompanies)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.FkCompaniesId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_users_companies");
            });
        }
    }
}
