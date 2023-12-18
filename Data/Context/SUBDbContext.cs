using Common.Dto;
using Common.Enums;
using Data.Comparers;
using Data.Converters;
using Data.Entity;
using GoogleApi.Entities.Common.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Data.Context
{
    public class SUBDbContext : DbContext
    {
        public SUBDbContext(DbContextOptions<SUBDbContext> options) : base(options)
        { }
        public DbSet<StopEntity> Stop { get; set; }
        public DbSet<PlaceEntity> Place { get; set; }
        public DbSet<TripEntity> Trip { get; set; }
        public DbSet<UserEntity> User { get; set; }
        public DbSet<DirectionEntity> Direction { get; set; }
        public DbSet<RouteEntity> Route { get; set; }
        public DbSet<PasswordTokenEntity> PasswordToken { get; set; }
        public DbSet<UserActiveTokenEntity> UserActiveToken { get; set; }
        public DbSet<OfferEntity> Offer { get; set; }
        public DbSet<TripOfferMovements> TripOfferMovements { get; set; }

        public DbSet<AppBusinessConfigEntity> AppBusinessConfig { get; set; }

        public DbSet<ScheduledJobEntity> ScheduledJobs { get; set; }

        public DbSet<EmailConfigEntity> EmailConfig { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            var amenitiesConverter = new EnumCollectionJsonValueConverter<Amenities>();
            var amenitiesComparer = new CollectionValueComparer<Amenities>();
            var notificationsConverter = new EnumCollectionJsonValueConverter<Notifications>();
            var notificationsComparer = new CollectionValueComparer<Notifications>();
            var pointsConverter = new JsonArrayConverter<GeoPoint>();
            var pointsComparer = new CollectionValueComparer<GeoPoint>();

            var enumConverters = new Dictionary<Type, ValueConverter>
            {
                { typeof(Currency), new EnumValueConverter<Currency>() },
                { typeof(TripInvoicePayType), new EnumValueConverter<TripInvoicePayType>() },
                { typeof(InvoiceType), new EnumValueConverter<InvoiceType>() },
                { typeof(Amenities), new EnumValueConverter<Amenities>() },
                { typeof(EuroStandard), new EnumValueConverter<EuroStandard>() },
                { typeof(UserRole), new EnumValueConverter<UserRole>() },
                { typeof(Country), new EnumValueConverter<Country>() },
                { typeof(PlaceResolution), new EnumValueConverter<PlaceResolution>() },
                { typeof(TransportType), new EnumValueConverter<TransportType>() },
                { typeof(TripOfferState), new EnumValueConverter<TripOfferState>() },
                { typeof(Language), new EnumValueConverter<Language>() },
                { typeof(EmailType), new EnumValueConverter<EmailType>() }
            };

            foreach (var entityType in builder.Model.GetEntityTypes())
            {
                foreach (var type in enumConverters.Keys)
                {
                    var properties = entityType.ClrType.GetProperties().Where(p => p.PropertyType == type);
                    foreach (var property in properties)
                    {
                        builder.Entity(entityType.Name).Property(property.Name).HasConversion(enumConverters[type]);
                    }
                }
            }

            builder.Entity<PlaceEntity>()
               .Property(e => e.Point)
               .HasConversion(new JsonValueConverter<GeoPoint>());

            builder.Entity<DirectionEntity>()
                .Property(e => e.Points)
                .HasConversion(pointsConverter)
                .Metadata.SetValueComparer(pointsComparer);

            builder.Entity<TripEntity>()
                .Property(e => e.Amenities)
                .HasConversion(amenitiesConverter)
                .Metadata.SetValueComparer(amenitiesComparer);

            builder.Entity<UserEntity>()
                .Property(u => u.Notifications)
                .HasConversion(notificationsConverter)
                .Metadata.SetValueComparer(notificationsComparer);

        }
    }
}
