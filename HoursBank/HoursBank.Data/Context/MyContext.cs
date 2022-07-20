using HoursBank.Data.Mapping;
using HoursBank.Domain.Entities;
using HoursBank.Util;
using Microsoft.EntityFrameworkCore;
using System;

namespace HoursBank.Data.Context
{
    public class MyContext : DbContext
    {
        public DbSet<BankEntity> Bank { get; set; }
        public DbSet<CoordinatorEntity> Coordinators { get; set; }
        public DbSet<TeamEntity> Teams { get; set; }
        public DbSet<TypeEntity> Types { get; set; }
        public DbSet<UserEntity> Users { get; set; }

        public MyContext(DbContextOptions<MyContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TypeEntity>(new TypeMap().Configure);
            modelBuilder.Entity<TeamEntity>(new TeamMap().Configure);
            modelBuilder.Entity<UserEntity>(new UserMap().Configure);
            modelBuilder.Entity<CoordinatorEntity>(new CoordinatorMap().Configure);
            modelBuilder.Entity<BankEntity>(new BankMap().Configure);

            // Seeds
            modelBuilder.Entity<TypeEntity>().HasData(
                new TypeEntity
                {
                    Id = 1,
                    Description = "POSITIVE",
                    Increase = true,
                    CreatedAt = DateTime.Now 
                },
                new TypeEntity
                {
                    Id = 2,
                    Description = "NEGATIVE",
                    Increase = false,
                    CreatedAt = DateTime.Now
                }
            );
            modelBuilder.Entity<TeamEntity>().HasData(
                new TeamEntity
                {
                    Id = 1,
                    Name = "Administrators",
                    CreatedAt = DateTime.Now
                }
            );
            modelBuilder.Entity<UserEntity>().HasData(
                new UserEntity
                {
                    Id = 1,
                    Name = "Administrador",
                    Admin = true,
                    Active = true,
                    Email = "banco.horas.admin@gmail.com",
                    Password = BHCrypto.Encode("*Bh@2022"),
                    TeamId = 1,
                    CreatedAt = DateTime.Now
                }
            );

            base.OnModelCreating(modelBuilder);
        }
    }
}
