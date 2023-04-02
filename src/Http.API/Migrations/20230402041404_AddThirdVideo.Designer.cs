﻿// <auto-generated />
using System;
using EntityFramework;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Http.API.Migrations
{
    [DbContext(typeof(CommandDbContext))]
    [Migration("20230402041404_AddThirdVideo")]
    partial class AddThirdVideo
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("BlogTags", b =>
                {
                    b.Property<Guid>("BlogsId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("TagsId")
                        .HasColumnType("uuid");

                    b.HasKey("BlogsId", "TagsId");

                    b.HasIndex("TagsId");

                    b.ToTable("BlogTags");
                });

            modelBuilder.Entity("Core.Models.EntityBase", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTimeOffset>("CreatedTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

                    b.Property<DateTimeOffset>("UpdatedTime")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.ToTable((string)null);

                    b.UseTpcMappingStrategy();
                });

            modelBuilder.Entity("SystemRoleSystemUser", b =>
                {
                    b.Property<Guid>("SystemRolesId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("UsersId")
                        .HasColumnType("uuid");

                    b.HasKey("SystemRolesId", "UsersId");

                    b.HasIndex("UsersId");

                    b.ToTable("SystemRoleSystemUser");
                });

            modelBuilder.Entity("Core.Entities.CMS.Blog", b =>
                {
                    b.HasBaseType("Core.Models.EntityBase");

                    b.Property<string>("Authors")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("character varying(200)");

                    b.Property<int>("BlogType")
                        .HasColumnType("integer");

                    b.Property<Guid>("CatalogId")
                        .HasColumnType("uuid");

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasMaxLength(10000)
                        .HasColumnType("character varying(10000)");

                    b.Property<string>("Description")
                        .HasMaxLength(300)
                        .HasColumnType("character varying(300)");

                    b.Property<bool>("IsAudit")
                        .HasColumnType("boolean");

                    b.Property<bool>("IsOriginal")
                        .HasColumnType("boolean");

                    b.Property<bool>("IsPublic")
                        .HasColumnType("boolean");

                    b.Property<int>("LanguageType")
                        .HasColumnType("integer");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<string>("TranslateContent")
                        .HasMaxLength(12000)
                        .HasColumnType("character varying(12000)");

                    b.Property<string>("TranslateTitle")
                        .HasMaxLength(200)
                        .HasColumnType("character varying(200)");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.Property<int>("ViewCount")
                        .HasColumnType("integer");

                    b.HasIndex("Authors");

                    b.HasIndex("CatalogId");

                    b.HasIndex("CreatedTime");

                    b.HasIndex("IsOriginal");

                    b.HasIndex("IsPublic");

                    b.HasIndex("LanguageType");

                    b.HasIndex("Title");

                    b.HasIndex("UserId");

                    b.ToTable("Blogs");
                });

            modelBuilder.Entity("Core.Entities.CMS.Catalog", b =>
                {
                    b.HasBaseType("Core.Models.EntityBase");

                    b.Property<short>("Level")
                        .HasColumnType("smallint");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.Property<Guid?>("ParentId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.HasIndex("Level");

                    b.HasIndex("Name");

                    b.HasIndex("ParentId");

                    b.HasIndex("UserId");

                    b.ToTable("Catalogs");
                });

            modelBuilder.Entity("Core.Entities.CMS.NewsTags", b =>
                {
                    b.HasBaseType("Core.Models.EntityBase");

                    b.Property<string>("Color")
                        .HasMaxLength(20)
                        .HasColumnType("character varying(20)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(40)
                        .HasColumnType("character varying(40)");

                    b.Property<Guid>("ThirdNewsId")
                        .HasColumnType("uuid");

                    b.HasIndex("ThirdNewsId");

                    b.ToTable("NewsTags");
                });

            modelBuilder.Entity("Core.Entities.CMS.Tags", b =>
                {
                    b.HasBaseType("Core.Models.EntityBase");

                    b.Property<string>("Color")
                        .HasMaxLength(20)
                        .HasColumnType("character varying(20)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.HasIndex("Color");

                    b.HasIndex("Name");

                    b.HasIndex("UserId");

                    b.ToTable("Tags");
                });

            modelBuilder.Entity("Core.Entities.CMS.ThirdNews", b =>
                {
                    b.HasBaseType("Core.Models.EntityBase");

                    b.Property<string>("AuthorAvatar")
                        .HasMaxLength(300)
                        .HasColumnType("character varying(300)");

                    b.Property<string>("AuthorName")
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<string>("Category")
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.Property<string>("Content")
                        .HasMaxLength(8000)
                        .HasColumnType("character varying(8000)");

                    b.Property<DateTimeOffset?>("DatePublished")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Description")
                        .HasMaxLength(5000)
                        .HasColumnType("character varying(5000)");

                    b.Property<string>("IdentityId")
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.Property<int>("NewsStatus")
                        .HasColumnType("integer");

                    b.Property<int>("NewsType")
                        .HasColumnType("integer");

                    b.Property<string>("Provider")
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.Property<int>("TechType")
                        .HasColumnType("integer");

                    b.Property<string>("ThumbnailUrl")
                        .HasMaxLength(300)
                        .HasColumnType("character varying(300)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("character varying(200)");

                    b.Property<int>("Type")
                        .HasColumnType("integer");

                    b.Property<string>("Url")
                        .HasMaxLength(300)
                        .HasColumnType("character varying(300)");

                    b.HasIndex("AuthorName");

                    b.HasIndex("Category");

                    b.HasIndex("NewsType");

                    b.HasIndex("Provider");

                    b.HasIndex("Title");

                    b.ToTable("ThirdNews");
                });

            modelBuilder.Entity("Core.Entities.CMS.ThirdVideo", b =>
                {
                    b.HasBaseType("Core.Models.EntityBase");

                    b.Property<string>("Description")
                        .HasMaxLength(500)
                        .HasColumnType("character varying(500)");

                    b.Property<string>("OriginalUrl")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("character varying(200)");

                    b.Property<string>("Source")
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.Property<string>("ThumbnailUrl")
                        .HasMaxLength(200)
                        .HasColumnType("character varying(200)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(120)
                        .HasColumnType("character varying(120)");

                    b.HasIndex("CreatedTime");

                    b.HasIndex("Title");

                    b.ToTable("ThirdVideos");
                });

            modelBuilder.Entity("Core.Entities.EntityDesign.EntityLibrary", b =>
                {
                    b.HasBaseType("Core.Models.EntityBase");

                    b.Property<string>("Description")
                        .HasMaxLength(200)
                        .HasColumnType("character varying(200)");

                    b.Property<bool>("IsPublic")
                        .HasColumnType("boolean");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(60)
                        .HasColumnType("character varying(60)");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.HasIndex("IsPublic");

                    b.HasIndex("Name");

                    b.HasIndex("UserId");

                    b.ToTable("EntityLibraries");
                });

            modelBuilder.Entity("Core.Entities.EntityDesign.EntityMember", b =>
                {
                    b.HasBaseType("Core.Models.EntityBase");

                    b.Property<int>("AccessModifier")
                        .HasColumnType("integer");

                    b.Property<bool>("CanSet")
                        .HasColumnType("boolean");

                    b.Property<string>("Comment")
                        .IsRequired()
                        .HasMaxLength(300)
                        .HasColumnType("character varying(300)");

                    b.Property<string>("DefaultValue")
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<int?>("DictionaryKeyType")
                        .HasColumnType("integer");

                    b.Property<int?>("DictionaryValueType")
                        .HasColumnType("integer");

                    b.Property<Guid>("EntityModelId")
                        .HasColumnType("uuid");

                    b.Property<bool>("IsEnum")
                        .HasColumnType("boolean");

                    b.Property<bool>("IsList")
                        .HasColumnType("boolean");

                    b.Property<bool>("IsObject")
                        .HasColumnType("boolean");

                    b.Property<bool>("IsRequired")
                        .HasColumnType("boolean");

                    b.Property<int>("MemberType")
                        .HasColumnType("integer");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(60)
                        .HasColumnType("character varying(60)");

                    b.Property<bool>("NeedInit")
                        .HasColumnType("boolean");

                    b.Property<Guid?>("ObjectTypeId")
                        .HasColumnType("uuid");

                    b.HasIndex("AccessModifier");

                    b.HasIndex("EntityModelId");

                    b.HasIndex("Name");

                    b.HasIndex("ObjectTypeId")
                        .IsUnique();

                    b.ToTable("EntityMembers");
                });

            modelBuilder.Entity("Core.Entities.EntityDesign.EntityMemberConstraint", b =>
                {
                    b.HasBaseType("Core.Models.EntityBase");

                    b.Property<Guid>("EntityMemberId")
                        .HasColumnType("uuid");

                    b.Property<int?>("Length")
                        .HasColumnType("integer");

                    b.Property<long?>("Max")
                        .HasColumnType("bigint");

                    b.Property<int?>("MaxLength")
                        .HasColumnType("integer");

                    b.Property<int?>("Min")
                        .HasColumnType("integer");

                    b.Property<int?>("MinLength")
                        .HasColumnType("integer");

                    b.HasIndex("EntityMemberId")
                        .IsUnique();

                    b.ToTable("EntityMemberConstraints");
                });

            modelBuilder.Entity("Core.Entities.EntityDesign.EntityModel", b =>
                {
                    b.HasBaseType("Core.Models.EntityBase");

                    b.Property<int>("AccessModifier")
                        .HasColumnType("integer");

                    b.Property<string>("CodeContent")
                        .HasMaxLength(8000)
                        .HasColumnType("character varying(8000)");

                    b.Property<int>("CodeLanguage")
                        .HasColumnType("integer");

                    b.Property<string>("Comment")
                        .IsRequired()
                        .HasMaxLength(300)
                        .HasColumnType("character varying(300)");

                    b.Property<Guid>("EntityLibraryId")
                        .HasColumnType("uuid");

                    b.Property<string>("LanguageVersion")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(60)
                        .HasColumnType("character varying(60)");

                    b.Property<Guid?>("ParentEntityId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.HasIndex("AccessModifier");

                    b.HasIndex("CodeLanguage");

                    b.HasIndex("EntityLibraryId");

                    b.HasIndex("Name");

                    b.HasIndex("ParentEntityId");

                    b.HasIndex("UserId");

                    b.ToTable("EntityModels");
                });

            modelBuilder.Entity("Core.Entities.SystemRole", b =>
                {
                    b.HasBaseType("Core.Models.EntityBase");

                    b.Property<string>("Icon")
                        .HasMaxLength(30)
                        .HasColumnType("character varying(30)");

                    b.Property<bool>("IsSystem")
                        .HasColumnType("boolean");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("character varying(30)");

                    b.Property<string>("NameValue")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasIndex("Name");

                    b.ToTable("SystemRoles");
                });

            modelBuilder.Entity("Core.Entities.SystemUser", b =>
                {
                    b.HasBaseType("Core.Models.EntityBase");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("integer");

                    b.Property<string>("Avatar")
                        .HasMaxLength(200)
                        .HasColumnType("character varying(200)");

                    b.Property<string>("Email")
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("boolean");

                    b.Property<DateTimeOffset?>("LastLoginTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("boolean");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("PasswordHash")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<string>("PasswordSalt")
                        .IsRequired()
                        .HasMaxLength(60)
                        .HasColumnType("character varying(60)");

                    b.Property<string>("PhoneNumber")
                        .HasMaxLength(20)
                        .HasColumnType("character varying(20)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("boolean");

                    b.Property<string>("RealName")
                        .HasMaxLength(30)
                        .HasColumnType("character varying(30)");

                    b.Property<int>("RetryCount")
                        .HasColumnType("integer");

                    b.Property<int>("Sex")
                        .HasColumnType("integer");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("boolean");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("character varying(30)");

                    b.HasIndex("CreatedTime");

                    b.HasIndex("Email");

                    b.HasIndex("IsDeleted");

                    b.HasIndex("PhoneNumber");

                    b.HasIndex("UserName");

                    b.ToTable("SystemUsers");
                });

            modelBuilder.Entity("Core.Entities.User", b =>
                {
                    b.HasBaseType("Core.Models.EntityBase");

                    b.Property<string>("Email")
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("boolean");

                    b.Property<string>("PasswordHash")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<string>("PasswordSalt")
                        .IsRequired()
                        .HasMaxLength(60)
                        .HasColumnType("character varying(60)");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasMaxLength(40)
                        .HasColumnType("character varying(40)");

                    b.Property<int>("UserType")
                        .HasColumnType("integer");

                    b.HasIndex("UserName")
                        .IsUnique();

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Core.Entities.WebConfig", b =>
                {
                    b.HasBaseType("Core.Models.EntityBase");

                    b.Property<string>("Description")
                        .HasMaxLength(300)
                        .HasColumnType("character varying(300)");

                    b.Property<string>("GroupName")
                        .HasColumnType("text");

                    b.Property<bool>("IsSystem")
                        .HasColumnType("boolean");

                    b.Property<string>("Key")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<bool>("Valid")
                        .HasColumnType("boolean");

                    b.Property<string>("Value")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.ToTable("WebConfigs");
                });

            modelBuilder.Entity("BlogTags", b =>
                {
                    b.HasOne("Core.Entities.CMS.Blog", null)
                        .WithMany()
                        .HasForeignKey("BlogsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Core.Entities.CMS.Tags", null)
                        .WithMany()
                        .HasForeignKey("TagsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("SystemRoleSystemUser", b =>
                {
                    b.HasOne("Core.Entities.SystemRole", null)
                        .WithMany()
                        .HasForeignKey("SystemRolesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Core.Entities.SystemUser", null)
                        .WithMany()
                        .HasForeignKey("UsersId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Core.Entities.CMS.Blog", b =>
                {
                    b.HasOne("Core.Entities.CMS.Catalog", "Catalog")
                        .WithMany("Blogs")
                        .HasForeignKey("CatalogId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Core.Entities.User", "User")
                        .WithMany("Blogs")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Catalog");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Core.Entities.CMS.Catalog", b =>
                {
                    b.HasOne("Core.Entities.CMS.Catalog", "Parent")
                        .WithMany("Children")
                        .HasForeignKey("ParentId");

                    b.HasOne("Core.Entities.User", "User")
                        .WithMany("Catalogs")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Parent");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Core.Entities.CMS.NewsTags", b =>
                {
                    b.HasOne("Core.Entities.CMS.ThirdNews", "ThirdNews")
                        .WithMany("NewsTags")
                        .HasForeignKey("ThirdNewsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ThirdNews");
                });

            modelBuilder.Entity("Core.Entities.CMS.Tags", b =>
                {
                    b.HasOne("Core.Entities.User", "User")
                        .WithMany("Tags")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("Core.Entities.EntityDesign.EntityLibrary", b =>
                {
                    b.HasOne("Core.Entities.User", "User")
                        .WithMany("EntityLibraries")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("Core.Entities.EntityDesign.EntityMember", b =>
                {
                    b.HasOne("Core.Entities.EntityDesign.EntityModel", "EntityModel")
                        .WithMany("EntityMembers")
                        .HasForeignKey("EntityModelId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Core.Entities.EntityDesign.EntityModel", "ObjectType")
                        .WithOne()
                        .HasForeignKey("Core.Entities.EntityDesign.EntityMember", "ObjectTypeId");

                    b.Navigation("EntityModel");

                    b.Navigation("ObjectType");
                });

            modelBuilder.Entity("Core.Entities.EntityDesign.EntityMemberConstraint", b =>
                {
                    b.HasOne("Core.Entities.EntityDesign.EntityMember", "EntityMember")
                        .WithOne("Constraint")
                        .HasForeignKey("Core.Entities.EntityDesign.EntityMemberConstraint", "EntityMemberId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("EntityMember");
                });

            modelBuilder.Entity("Core.Entities.EntityDesign.EntityModel", b =>
                {
                    b.HasOne("Core.Entities.EntityDesign.EntityLibrary", "EntityLibrary")
                        .WithMany("EntityModels")
                        .HasForeignKey("EntityLibraryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Core.Entities.EntityDesign.EntityModel", "ParentEntity")
                        .WithMany("ChildrenEntities")
                        .HasForeignKey("ParentEntityId");

                    b.HasOne("Core.Entities.User", "User")
                        .WithMany("EntityModels")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("EntityLibrary");

                    b.Navigation("ParentEntity");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Core.Entities.CMS.Catalog", b =>
                {
                    b.Navigation("Blogs");

                    b.Navigation("Children");
                });

            modelBuilder.Entity("Core.Entities.CMS.ThirdNews", b =>
                {
                    b.Navigation("NewsTags");
                });

            modelBuilder.Entity("Core.Entities.EntityDesign.EntityLibrary", b =>
                {
                    b.Navigation("EntityModels");
                });

            modelBuilder.Entity("Core.Entities.EntityDesign.EntityMember", b =>
                {
                    b.Navigation("Constraint");
                });

            modelBuilder.Entity("Core.Entities.EntityDesign.EntityModel", b =>
                {
                    b.Navigation("ChildrenEntities");

                    b.Navigation("EntityMembers");
                });

            modelBuilder.Entity("Core.Entities.User", b =>
                {
                    b.Navigation("Blogs");

                    b.Navigation("Catalogs");

                    b.Navigation("EntityLibraries");

                    b.Navigation("EntityModels");

                    b.Navigation("Tags");
                });
#pragma warning restore 612, 618
        }
    }
}
