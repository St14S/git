using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiTest.Models
{
    public class RestaurantContext : DbContext
    {
        public RestaurantContext(DbContextOptions<RestaurantContext> options)
            : base(options)
        {
        }

        public virtual DbSet<City> City { get; set; }
        public virtual DbSet<Restaurant> Restaurant { get; set; }
        public virtual DbSet<LocationRestaurant> LocationRest { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<City>()
            .HasMany(e => e.locationRestaurants)
            .WithOne(e => e._city)
            .HasForeignKey(e => e.Id_ct);

            modelBuilder.Entity<Restaurant>()
           .HasMany(e => e.locationRestaurants)
           .WithOne(e => e._restaurant)
           .HasForeignKey(e => e.Id_rs);
        }
    }
}
