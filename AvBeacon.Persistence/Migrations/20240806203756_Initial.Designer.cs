﻿// <auto-generated />
using System;
using AvBeacon.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace AvBeacon.Persistence.Migrations
{
    [DbContext(typeof(AvBeaconDbContext))]
    [Migration("20240806203756_Initial")]
    partial class Initial
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.7")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("ApplicantSkill", b =>
                {
                    b.Property<Guid>("ApplicantId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("SkillId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("ApplicantId", "SkillId");

                    b.HasIndex("SkillId");

                    b.ToTable("ApplicantSkill");
                });

            modelBuilder.Entity("AvBeacon.Domain.Users.Applicants.Educations.Education", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("ApplicantId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedOnUtc")
                        .HasColumnType("datetime2");

                    b.Property<bool>("Deleted")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(false);

                    b.Property<DateTime?>("DeletedOnUtc")
                        .HasColumnType("datetime2");

                    b.Property<int>("EducationType")
                        .HasColumnType("int");

                    b.Property<DateTime?>("ModifiedOnUtc")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("ApplicantId");

                    b.ToTable("Education");
                });

            modelBuilder.Entity("AvBeacon.Domain.Users.Applicants.Experiences.Experience", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("ApplicantId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedOnUtc")
                        .HasColumnType("datetime2");

                    b.Property<bool>("Deleted")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(false);

                    b.Property<DateTime?>("DeletedOnUtc")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("End")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("ModifiedOnUtc")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("Start")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("ApplicantId");

                    b.ToTable("Experience");
                });

            modelBuilder.Entity("AvBeacon.Domain.Users.Applicants.JobApplications.JobApplication", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("ApplicantId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedOnUtc")
                        .HasColumnType("datetime2");

                    b.Property<bool>("Deleted")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(false);

                    b.Property<DateTime?>("DeletedOnUtc")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("JobOfferId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("ModifiedOnUtc")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("ApplicantId");

                    b.HasIndex("JobOfferId");

                    b.ToTable("JobApplication");
                });

            modelBuilder.Entity("AvBeacon.Domain.Users.Applicants.Skills.Skill", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.ToTable("Skill");
                });

            modelBuilder.Entity("AvBeacon.Domain.Users.Recruiters.JobOffers.JobOffer", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedOnUtc")
                        .HasColumnType("datetime2");

                    b.Property<bool>("Deleted")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(false);

                    b.Property<DateTime?>("DeletedOnUtc")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("ModifiedOnUtc")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("RecruiterId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("RecruiterId");

                    b.ToTable("JobOffer");
                });

            modelBuilder.Entity("AvBeacon.Domain.Users.Shared.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedOnUtc")
                        .HasColumnType("datetime2");

                    b.Property<bool>("Deleted")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(false);

                    b.Property<DateTime?>("DeletedOnUtc")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("ModifiedOnUtc")
                        .HasColumnType("datetime2");

                    b.Property<int>("UserType")
                        .HasColumnType("int");

                    b.Property<string>("_passwordHash")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("PasswordHash");

                    b.HasKey("Id");

                    b.ToTable("User");

                    b.HasDiscriminator<int>("UserType").HasValue(0);

                    b.UseTphMappingStrategy();
                });

            modelBuilder.Entity("AvBeacon.Domain.Users.Applicants.Applicant", b =>
                {
                    b.HasBaseType("AvBeacon.Domain.Users.Shared.User");

                    b.HasDiscriminator().HasValue(2);
                });

            modelBuilder.Entity("AvBeacon.Domain.Users.Recruiters.Recruiter", b =>
                {
                    b.HasBaseType("AvBeacon.Domain.Users.Shared.User");

                    b.HasDiscriminator().HasValue(1);
                });

            modelBuilder.Entity("ApplicantSkill", b =>
                {
                    b.HasOne("AvBeacon.Domain.Users.Applicants.Applicant", null)
                        .WithMany()
                        .HasForeignKey("ApplicantId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AvBeacon.Domain.Users.Applicants.Skills.Skill", null)
                        .WithMany()
                        .HasForeignKey("SkillId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("AvBeacon.Domain.Users.Applicants.Educations.Education", b =>
                {
                    b.HasOne("AvBeacon.Domain.Users.Applicants.Applicant", null)
                        .WithMany()
                        .HasForeignKey("ApplicantId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.OwnsOne("AvBeacon.Domain._Shared.Description", "Description", b1 =>
                        {
                            b1.Property<Guid>("EducationId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<string>("Value")
                                .IsRequired()
                                .HasMaxLength(400)
                                .HasColumnType("nvarchar(400)")
                                .HasColumnName("Description");

                            b1.HasKey("EducationId");

                            b1.ToTable("Education");

                            b1.WithOwner()
                                .HasForeignKey("EducationId");
                        });

                    b.OwnsOne("AvBeacon.Domain._Shared.Title", "Title", b1 =>
                        {
                            b1.Property<Guid>("EducationId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<string>("Value")
                                .IsRequired()
                                .HasMaxLength(100)
                                .HasColumnType("nvarchar(100)")
                                .HasColumnName("Title");

                            b1.HasKey("EducationId");

                            b1.ToTable("Education");

                            b1.WithOwner()
                                .HasForeignKey("EducationId");
                        });

                    b.Navigation("Description")
                        .IsRequired();

                    b.Navigation("Title")
                        .IsRequired();
                });

            modelBuilder.Entity("AvBeacon.Domain.Users.Applicants.Experiences.Experience", b =>
                {
                    b.HasOne("AvBeacon.Domain.Users.Applicants.Applicant", null)
                        .WithMany()
                        .HasForeignKey("ApplicantId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.OwnsOne("AvBeacon.Domain._Shared.Description", "Description", b1 =>
                        {
                            b1.Property<Guid>("ExperienceId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<string>("Value")
                                .IsRequired()
                                .HasMaxLength(400)
                                .HasColumnType("nvarchar(400)")
                                .HasColumnName("Description");

                            b1.HasKey("ExperienceId");

                            b1.ToTable("Experience");

                            b1.WithOwner()
                                .HasForeignKey("ExperienceId");
                        });

                    b.OwnsOne("AvBeacon.Domain._Shared.Title", "Title", b1 =>
                        {
                            b1.Property<Guid>("ExperienceId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<string>("Value")
                                .IsRequired()
                                .HasMaxLength(100)
                                .HasColumnType("nvarchar(100)")
                                .HasColumnName("Title");

                            b1.HasKey("ExperienceId");

                            b1.ToTable("Experience");

                            b1.WithOwner()
                                .HasForeignKey("ExperienceId");
                        });

                    b.Navigation("Description")
                        .IsRequired();

                    b.Navigation("Title")
                        .IsRequired();
                });

            modelBuilder.Entity("AvBeacon.Domain.Users.Applicants.JobApplications.JobApplication", b =>
                {
                    b.HasOne("AvBeacon.Domain.Users.Applicants.Applicant", null)
                        .WithMany()
                        .HasForeignKey("ApplicantId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AvBeacon.Domain.Users.Recruiters.JobOffers.JobOffer", null)
                        .WithMany()
                        .HasForeignKey("JobOfferId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.OwnsOne("AvBeacon.Domain.Users.Applicants.JobApplications.JobApplicationState", "State", b1 =>
                        {
                            b1.Property<Guid>("JobApplicationId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<int>("Value")
                                .HasColumnType("int")
                                .HasColumnName("State");

                            b1.HasKey("JobApplicationId");

                            b1.ToTable("JobApplication");

                            b1.WithOwner()
                                .HasForeignKey("JobApplicationId");
                        });

                    b.Navigation("State")
                        .IsRequired();
                });

            modelBuilder.Entity("AvBeacon.Domain.Users.Applicants.Skills.Skill", b =>
                {
                    b.OwnsOne("AvBeacon.Domain._Shared.Title", "Title", b1 =>
                        {
                            b1.Property<Guid>("SkillId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<string>("Value")
                                .IsRequired()
                                .HasMaxLength(100)
                                .HasColumnType("nvarchar(100)")
                                .HasColumnName("Title");

                            b1.HasKey("SkillId");

                            b1.ToTable("Skill");

                            b1.WithOwner()
                                .HasForeignKey("SkillId");
                        });

                    b.Navigation("Title")
                        .IsRequired();
                });

            modelBuilder.Entity("AvBeacon.Domain.Users.Recruiters.JobOffers.JobOffer", b =>
                {
                    b.HasOne("AvBeacon.Domain.Users.Recruiters.Recruiter", null)
                        .WithMany()
                        .HasForeignKey("RecruiterId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.OwnsOne("AvBeacon.Domain._Shared.Description", "Description", b1 =>
                        {
                            b1.Property<Guid>("JobOfferId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<string>("Value")
                                .IsRequired()
                                .HasMaxLength(400)
                                .HasColumnType("nvarchar(400)")
                                .HasColumnName("Description");

                            b1.HasKey("JobOfferId");

                            b1.ToTable("JobOffer");

                            b1.WithOwner()
                                .HasForeignKey("JobOfferId");
                        });

                    b.OwnsOne("AvBeacon.Domain._Shared.Title", "Title", b1 =>
                        {
                            b1.Property<Guid>("JobOfferId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<string>("Value")
                                .IsRequired()
                                .HasMaxLength(100)
                                .HasColumnType("nvarchar(100)")
                                .HasColumnName("Title");

                            b1.HasKey("JobOfferId");

                            b1.ToTable("JobOffer");

                            b1.WithOwner()
                                .HasForeignKey("JobOfferId");
                        });

                    b.Navigation("Description")
                        .IsRequired();

                    b.Navigation("Title")
                        .IsRequired();
                });

            modelBuilder.Entity("AvBeacon.Domain.Users.Shared.User", b =>
                {
                    b.OwnsOne("AvBeacon.Domain.Users.Shared.Email", "Email", b1 =>
                        {
                            b1.Property<Guid>("UserId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<string>("Value")
                                .IsRequired()
                                .HasMaxLength(256)
                                .HasColumnType("nvarchar(256)")
                                .HasColumnName("Email");

                            b1.HasKey("UserId");

                            b1.ToTable("User");

                            b1.WithOwner()
                                .HasForeignKey("UserId");
                        });

                    b.OwnsOne("AvBeacon.Domain.Users.Shared.FirstName", "FirstName", b1 =>
                        {
                            b1.Property<Guid>("UserId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<string>("Value")
                                .IsRequired()
                                .HasMaxLength(100)
                                .HasColumnType("nvarchar(100)")
                                .HasColumnName("FirstName");

                            b1.HasKey("UserId");

                            b1.ToTable("User");

                            b1.WithOwner()
                                .HasForeignKey("UserId");
                        });

                    b.OwnsOne("AvBeacon.Domain.Users.Shared.LastName", "LastName", b1 =>
                        {
                            b1.Property<Guid>("UserId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<string>("Value")
                                .IsRequired()
                                .HasMaxLength(100)
                                .HasColumnType("nvarchar(100)")
                                .HasColumnName("LastName");

                            b1.HasKey("UserId");

                            b1.ToTable("User");

                            b1.WithOwner()
                                .HasForeignKey("UserId");
                        });

                    b.Navigation("Email")
                        .IsRequired();

                    b.Navigation("FirstName")
                        .IsRequired();

                    b.Navigation("LastName")
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
