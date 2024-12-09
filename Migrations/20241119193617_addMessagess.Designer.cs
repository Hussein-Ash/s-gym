﻿// <auto-generated />
using System;
using System.Collections.Generic;
using EvaluationBackend.DATA;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace EvaluationBackend.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20241119193617_addMessagess")]
    partial class addMessagess
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("EvaluationBackend.Entities.AppUser", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime?>("CreationDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<bool>("Deleted")
                        .HasColumnType("boolean");

                    b.Property<string>("FullName")
                        .HasColumnType("text");

                    b.Property<string>("Img")
                        .HasColumnType("text");

                    b.Property<DateTime?>("ModifiedDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Password")
                        .HasColumnType("text");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("text");

                    b.Property<string>("RefreshToken")
                        .HasColumnType("text");

                    b.Property<int>("Role")
                        .HasColumnType("integer");

                    b.Property<Guid?>("SubId")
                        .HasColumnType("uuid");

                    b.Property<string>("UserName")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Users");

                    b.HasData(
                        new
                        {
                            Id = new Guid("0f8f8a71-fa93-4897-7a54-45a069619c0e"),
                            CreationDate = new DateTime(2024, 11, 19, 19, 36, 16, 783, DateTimeKind.Utc).AddTicks(8157),
                            Deleted = false,
                            Password = "12345678",
                            PhoneNumber = "07816565518",
                            Role = 0,
                            UserName = "SuperAdmin"
                        });
                });

            modelBuilder.Entity("EvaluationBackend.Entities.Connection", b =>
                {
                    b.Property<string>("ConnectionId")
                        .HasColumnType("text");

                    b.Property<string>("GroupName")
                        .HasColumnType("text");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("ConnectionId");

                    b.HasIndex("GroupName");

                    b.ToTable("Connections");
                });

            modelBuilder.Entity("EvaluationBackend.Entities.Course", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime?>("CreationDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<int?>("DayCount")
                        .HasColumnType("integer");

                    b.Property<bool>("Deleted")
                        .HasColumnType("boolean");

                    b.Property<DateTime?>("ModifiedDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<Guid?>("SectionId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("SectionId");

                    b.ToTable("Courses");
                });

            modelBuilder.Entity("EvaluationBackend.Entities.Day", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid?>("CourseId")
                        .HasColumnType("uuid");

                    b.Property<DateTime?>("CreationDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<int?>("DaySeq")
                        .HasColumnType("integer");

                    b.Property<bool>("Deleted")
                        .HasColumnType("boolean");

                    b.Property<DateTime?>("ModifiedDate")
                        .HasColumnType("timestamp without time zone");

                    b.HasKey("Id");

                    b.HasIndex("CourseId");

                    b.ToTable("Days");
                });

            modelBuilder.Entity("EvaluationBackend.Entities.DayExercise", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime?>("CreationDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<Guid>("DayId")
                        .HasColumnType("uuid");

                    b.Property<bool>("Deleted")
                        .HasColumnType("boolean");

                    b.Property<Guid?>("ExerciseId")
                        .HasColumnType("uuid");

                    b.Property<DateTime?>("ModifiedDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<Guid?>("MuscleId")
                        .HasColumnType("uuid");

                    b.Property<Guid?>("SetsId")
                        .HasColumnType("uuid");

                    b.Property<bool>("Super")
                        .HasColumnType("boolean");

                    b.HasKey("Id");

                    b.HasIndex("DayId");

                    b.HasIndex("ExerciseId");

                    b.HasIndex("MuscleId");

                    b.HasIndex("SetsId");

                    b.ToTable("DayExercises");
                });

            modelBuilder.Entity("EvaluationBackend.Entities.Exercise", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime?>("CreationDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<bool>("Deleted")
                        .HasColumnType("boolean");

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<Guid?>("ExerciseId")
                        .HasColumnType("uuid");

                    b.Property<string>("Img")
                        .HasColumnType("text");

                    b.Property<DateTime?>("ModifiedDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<Guid?>("MuscleId")
                        .HasColumnType("uuid");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<string>("YouTubeLink")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("ExerciseId");

                    b.HasIndex("MuscleId")
                        .IsUnique();

                    b.ToTable("Exercises");
                });

            modelBuilder.Entity("EvaluationBackend.Entities.Group", b =>
                {
                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.HasKey("Name");

                    b.ToTable("Groups");
                });

            modelBuilder.Entity("EvaluationBackend.Entities.Message", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Content")
                        .HasColumnType("text");

                    b.Property<DateTime?>("CreationDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<bool>("Deleted")
                        .HasColumnType("boolean");

                    b.Property<List<string>>("Imgs")
                        .HasColumnType("text[]");

                    b.Property<DateTime>("MessageSent")
                        .HasColumnType("timestamp without time zone");

                    b.Property<DateTime?>("ModifiedDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<Guid>("RecipientId")
                        .HasColumnType("uuid");

                    b.Property<string>("RecipientUsername")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<bool>("SenderDeleted")
                        .HasColumnType("boolean");

                    b.Property<Guid>("SenderId")
                        .HasColumnType("uuid");

                    b.Property<string>("SenderUsername")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<List<string>>("VoiceMsgs")
                        .HasColumnType("text[]");

                    b.HasKey("Id");

                    b.HasIndex("RecipientId");

                    b.HasIndex("SenderId");

                    b.ToTable("Messages");
                });

            modelBuilder.Entity("EvaluationBackend.Entities.Muscle", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime?>("CreationDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<Guid?>("DayId")
                        .HasColumnType("uuid");

                    b.Property<bool>("Deleted")
                        .HasColumnType("boolean");

                    b.Property<Guid?>("ExerciseId")
                        .HasColumnType("uuid");

                    b.Property<string>("Img")
                        .HasColumnType("text");

                    b.Property<DateTime?>("ModifiedDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("DayId");

                    b.ToTable("Muscles");
                });

            modelBuilder.Entity("EvaluationBackend.Entities.Notification", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Body")
                        .HasColumnType("text");

                    b.Property<DateTime?>("CreationDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<bool>("Deleted")
                        .HasColumnType("boolean");

                    b.Property<DateTime?>("ModifiedDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<Guid?>("RefrenceId")
                        .HasColumnType("uuid");

                    b.Property<Guid?>("SenderId")
                        .HasColumnType("uuid");

                    b.Property<string>("Title")
                        .HasColumnType("text");

                    b.Property<int>("Type")
                        .HasColumnType("integer");

                    b.Property<Guid?>("UserId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("SenderId");

                    b.HasIndex("UserId");

                    b.ToTable("Notifications");
                });

            modelBuilder.Entity("EvaluationBackend.Entities.Offer", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime?>("CreationDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<bool>("Deleted")
                        .HasColumnType("boolean");

                    b.Property<string>("Img")
                        .HasColumnType("text");

                    b.Property<DateTime?>("ModifiedDate")
                        .HasColumnType("timestamp without time zone");

                    b.HasKey("Id");

                    b.ToTable("Offers");
                });

            modelBuilder.Entity("EvaluationBackend.Entities.Section", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime?>("CreationDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<bool>("Deleted")
                        .HasColumnType("boolean");

                    b.Property<DateTime?>("ModifiedDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Sections");
                });

            modelBuilder.Entity("EvaluationBackend.Entities.Set", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime?>("CreationDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<bool>("Deleted")
                        .HasColumnType("boolean");

                    b.Property<DateTime?>("ModifiedDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<Guid?>("SetId")
                        .HasColumnType("uuid");

                    b.Property<string>("Sets")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("SetId");

                    b.ToTable("Sets");
                });

            modelBuilder.Entity("EvaluationBackend.Entities.Subscription", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid?>("CourseId")
                        .HasColumnType("uuid");

                    b.Property<DateTime?>("CreationDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<bool>("Deleted")
                        .HasColumnType("boolean");

                    b.Property<DateTime?>("ModifiedDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("text");

                    b.Property<string>("PlayerPhoto")
                        .HasColumnType("text");

                    b.Property<DateTime?>("ResgisterDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<Guid?>("SectionId")
                        .HasColumnType("uuid");

                    b.Property<int?>("Status")
                        .HasColumnType("integer");

                    b.Property<Guid?>("SubInfoId")
                        .HasColumnType("uuid");

                    b.Property<int?>("Type")
                        .HasColumnType("integer");

                    b.Property<Guid?>("UserId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("CourseId");

                    b.HasIndex("SectionId");

                    b.HasIndex("UserId")
                        .IsUnique();

                    b.ToTable("Subscriptions");
                });

            modelBuilder.Entity("EvaluationBackend.Entities.SubscriptionInfo", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<int?>("Age")
                        .HasColumnType("integer");

                    b.Property<DateTime?>("CreationDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<bool>("Deleted")
                        .HasColumnType("boolean");

                    b.Property<string>("Goal")
                        .HasColumnType("text");

                    b.Property<string>("GymAddress")
                        .HasColumnType("text");

                    b.Property<string>("GymName")
                        .HasColumnType("text");

                    b.Property<string>("Height")
                        .HasColumnType("text");

                    b.Property<string>("Hrmon")
                        .HasColumnType("text");

                    b.Property<List<string>>("Imges")
                        .HasColumnType("text[]");

                    b.Property<string>("Injurse")
                        .HasColumnType("text");

                    b.Property<DateTime?>("ModifiedDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<string>("Notes")
                        .HasColumnType("text");

                    b.Property<string>("Since")
                        .HasColumnType("text");

                    b.Property<string>("Sleep")
                        .HasColumnType("text");

                    b.Property<Guid>("SubId")
                        .HasColumnType("uuid");

                    b.Property<string>("Tests")
                        .HasColumnType("text");

                    b.Property<string>("UnWantedFood")
                        .HasColumnType("text");

                    b.Property<bool>("UseHrmon")
                        .HasColumnType("boolean");

                    b.Property<string>("Weight")
                        .HasColumnType("text");

                    b.Property<string>("Work")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("SubId")
                        .IsUnique();

                    b.ToTable("SubscriptionsInfo");
                });

            modelBuilder.Entity("EvaluationBackend.Entities.Connection", b =>
                {
                    b.HasOne("EvaluationBackend.Entities.Group", null)
                        .WithMany("Connections")
                        .HasForeignKey("GroupName");
                });

            modelBuilder.Entity("EvaluationBackend.Entities.Course", b =>
                {
                    b.HasOne("EvaluationBackend.Entities.Section", "SectionName")
                        .WithMany("CourseName")
                        .HasForeignKey("SectionId")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.Navigation("SectionName");
                });

            modelBuilder.Entity("EvaluationBackend.Entities.Day", b =>
                {
                    b.HasOne("EvaluationBackend.Entities.Course", "CourseName")
                        .WithMany("Days")
                        .HasForeignKey("CourseId");

                    b.Navigation("CourseName");
                });

            modelBuilder.Entity("EvaluationBackend.Entities.DayExercise", b =>
                {
                    b.HasOne("EvaluationBackend.Entities.Day", "Day")
                        .WithMany("Exercises")
                        .HasForeignKey("DayId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("EvaluationBackend.Entities.Exercise", "ExerciseName")
                        .WithMany()
                        .HasForeignKey("ExerciseId");

                    b.HasOne("EvaluationBackend.Entities.Muscle", "MuscleName")
                        .WithMany()
                        .HasForeignKey("MuscleId");

                    b.HasOne("EvaluationBackend.Entities.Set", "Sets")
                        .WithMany()
                        .HasForeignKey("SetsId");

                    b.Navigation("Day");

                    b.Navigation("ExerciseName");

                    b.Navigation("MuscleName");

                    b.Navigation("Sets");
                });

            modelBuilder.Entity("EvaluationBackend.Entities.Exercise", b =>
                {
                    b.HasOne("EvaluationBackend.Entities.Exercise", "Exercice")
                        .WithMany()
                        .HasForeignKey("ExerciseId");

                    b.HasOne("EvaluationBackend.Entities.Muscle", "MuscleName")
                        .WithOne("Exercise")
                        .HasForeignKey("EvaluationBackend.Entities.Exercise", "MuscleId");

                    b.Navigation("Exercice");

                    b.Navigation("MuscleName");
                });

            modelBuilder.Entity("EvaluationBackend.Entities.Message", b =>
                {
                    b.HasOne("EvaluationBackend.Entities.AppUser", "Recipient")
                        .WithMany("MessagesReceived")
                        .HasForeignKey("RecipientId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("EvaluationBackend.Entities.AppUser", "Sender")
                        .WithMany("MessagesSent")
                        .HasForeignKey("SenderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Recipient");

                    b.Navigation("Sender");
                });

            modelBuilder.Entity("EvaluationBackend.Entities.Muscle", b =>
                {
                    b.HasOne("EvaluationBackend.Entities.Day", "DayName")
                        .WithMany()
                        .HasForeignKey("DayId");

                    b.Navigation("DayName");
                });

            modelBuilder.Entity("EvaluationBackend.Entities.Notification", b =>
                {
                    b.HasOne("EvaluationBackend.Entities.AppUser", "Sender")
                        .WithMany("NotificationsSent")
                        .HasForeignKey("SenderId")
                        .OnDelete(DeleteBehavior.NoAction);

                    b.HasOne("EvaluationBackend.Entities.AppUser", "User")
                        .WithMany("NotificationsReceived")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.NoAction);

                    b.Navigation("Sender");

                    b.Navigation("User");
                });

            modelBuilder.Entity("EvaluationBackend.Entities.Set", b =>
                {
                    b.HasOne("EvaluationBackend.Entities.Set", "ExerciseSet")
                        .WithMany()
                        .HasForeignKey("SetId");

                    b.Navigation("ExerciseSet");
                });

            modelBuilder.Entity("EvaluationBackend.Entities.Subscription", b =>
                {
                    b.HasOne("EvaluationBackend.Entities.Course", "CourseName")
                        .WithMany("Subs")
                        .HasForeignKey("CourseId");

                    b.HasOne("EvaluationBackend.Entities.Section", "SectionName")
                        .WithMany("Subscriptions")
                        .HasForeignKey("SectionId")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.HasOne("EvaluationBackend.Entities.AppUser", "User")
                        .WithOne("Sub")
                        .HasForeignKey("EvaluationBackend.Entities.Subscription", "UserId");

                    b.Navigation("CourseName");

                    b.Navigation("SectionName");

                    b.Navigation("User");
                });

            modelBuilder.Entity("EvaluationBackend.Entities.SubscriptionInfo", b =>
                {
                    b.HasOne("EvaluationBackend.Entities.Subscription", "Sub")
                        .WithOne("SubInfo")
                        .HasForeignKey("EvaluationBackend.Entities.SubscriptionInfo", "SubId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Sub");
                });

            modelBuilder.Entity("EvaluationBackend.Entities.AppUser", b =>
                {
                    b.Navigation("MessagesReceived");

                    b.Navigation("MessagesSent");

                    b.Navigation("NotificationsReceived");

                    b.Navigation("NotificationsSent");

                    b.Navigation("Sub");
                });

            modelBuilder.Entity("EvaluationBackend.Entities.Course", b =>
                {
                    b.Navigation("Days");

                    b.Navigation("Subs");
                });

            modelBuilder.Entity("EvaluationBackend.Entities.Day", b =>
                {
                    b.Navigation("Exercises");
                });

            modelBuilder.Entity("EvaluationBackend.Entities.Group", b =>
                {
                    b.Navigation("Connections");
                });

            modelBuilder.Entity("EvaluationBackend.Entities.Muscle", b =>
                {
                    b.Navigation("Exercise");
                });

            modelBuilder.Entity("EvaluationBackend.Entities.Section", b =>
                {
                    b.Navigation("CourseName");

                    b.Navigation("Subscriptions");
                });

            modelBuilder.Entity("EvaluationBackend.Entities.Subscription", b =>
                {
                    b.Navigation("SubInfo");
                });
#pragma warning restore 612, 618
        }
    }
}
