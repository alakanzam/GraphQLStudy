using System.Linq;
using GraphQlStudy.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace GraphQlStudy.Models.Contexts
{
    public class RelationalDbContext : DbContext
    {
        #region Properties

        /// <summary>
        /// List of study.
        /// </summary>
        public virtual DbSet<Student> Students { get; set; }

        public virtual DbSet<Class> Classes { get; set; }

        public virtual DbSet<StudentInClass> StudentInClasses { get; set; }

        #endregion

        #region Methods

        /// <summary>
        /// <inheritdoc />
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            AddStudentTable(modelBuilder);
            AddClassTable(modelBuilder);
            AddStudentInClassTable(modelBuilder);

            // This is for remove pluralization naming convention in database defined by Entity Framework.
            foreach (var entity in modelBuilder.Model.GetEntityTypes())
                entity.Relational().TableName = entity.DisplayName();

            // Disable cascade delete.
            foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
                relationship.DeleteBehavior = DeleteBehavior.Restrict;
        }

        /// <summary>
        /// Add student table,
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected virtual void AddStudentTable(ModelBuilder modelBuilder)
        {
            var student = modelBuilder.Entity<Student>();
            student.HasKey(x => x.Id);
            student.Property(x => x.Id).UseSqlServerIdentityColumn();

            student.Property(x => x.FullName).IsRequired();
            student.Property(x => x.Photo).IsRequired();
        }

        /// <summary>
        /// Add class table.
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected virtual void AddClassTable(ModelBuilder modelBuilder)
        {
            var oClass = modelBuilder.Entity<Class>();
            oClass.HasKey(x => x.Id);
            oClass.Property(x => x.Id).UseSqlServerIdentityColumn();

            oClass.Property(x => x.Name).IsRequired();
        }

        /// <summary>
        /// Add StudentInClass table.
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected virtual void AddStudentInClassTable(ModelBuilder modelBuilder)
        {
            var studentInClass = modelBuilder.Entity<StudentInClass>();
            studentInClass.HasKey(x => new { x.ClassId, x.StudentId });

            studentInClass.HasOne(x => x.Student).WithMany(x => x.StudentsInClasses)
                .HasForeignKey(x => x.StudentId);
            studentInClass.HasOne(x => x.Class).WithMany(x => x.StudentsInClasses)
                .HasForeignKey(x => x.ClassId);
        }

        #endregion

        #region Constructor

        public RelationalDbContext(DbContextOptions dbContextOptions) : base(dbContextOptions)
        {

        }

        #endregion

    }
}