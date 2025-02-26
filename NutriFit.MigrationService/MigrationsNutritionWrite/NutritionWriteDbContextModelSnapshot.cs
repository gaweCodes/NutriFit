﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using Nutrition.Infrastructure.Write.Database;

#nullable disable

namespace NutriFit.MigrationService.MigrationsNutritionWrite
{
    [DbContext(typeof(NutritionWriteDbContext))]
    partial class NutritionWriteDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Nutrition.Domain.MenuPlans.MenuPlan", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid");

                    b.Property<DateOnly>("_endDate")
                        .HasColumnType("date")
                        .HasColumnName("EndDate");

                    b.Property<bool>("_isDeleted")
                        .HasColumnType("boolean")
                        .HasColumnName("IsDeleted");

                    b.Property<DateOnly>("_startDate")
                        .HasColumnType("date")
                        .HasColumnName("StartDate");

                    b.HasKey("Id");

                    b.ToTable("MenuPlans", (string)null);
                });

            modelBuilder.Entity("Nutrition.Domain.Recipes.Recipe", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Recipes", (string)null);
                });

            modelBuilder.Entity("Nutrition.Domain.MenuPlans.MenuPlan", b =>
                {
                    b.OwnsMany("Nutrition.Domain.MenuPlans.DayPlan", "_days", b1 =>
                        {
                            b1.Property<Guid>("Id")
                                .HasColumnType("uuid");

                            b1.Property<DateOnly>("Date")
                                .HasColumnType("date");

                            b1.Property<Guid>("MenuPlanId")
                                .HasColumnType("uuid");

                            b1.HasKey("Id");

                            b1.HasIndex("MenuPlanId");

                            b1.ToTable("DayPlans", (string)null);

                            b1.WithOwner()
                                .HasForeignKey("MenuPlanId");

                            b1.OwnsMany("Nutrition.Domain.MenuPlans.MealSlot", "_mealSlots", b2 =>
                                {
                                    b2.Property<Guid>("Id")
                                        .HasColumnType("uuid");

                                    b2.Property<Guid>("DayPlanId")
                                        .HasColumnType("uuid");

                                    b2.Property<int>("MealType")
                                        .HasColumnType("integer");

                                    b2.HasKey("Id");

                                    b2.HasIndex("DayPlanId");

                                    b2.ToTable("MealSlotss", (string)null);

                                    b2.WithOwner()
                                        .HasForeignKey("DayPlanId");
                                });

                            b1.Navigation("_mealSlots");
                        });

                    b.Navigation("_days");
                });
#pragma warning restore 612, 618
        }
    }
}
