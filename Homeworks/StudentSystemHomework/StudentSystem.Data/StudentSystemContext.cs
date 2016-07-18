namespace StudentSystem.Data
{
    using System;
    using System.Data.Entity;
    using System.Linq;

    using StudentSystem.Data.Migrations;
    using StudentSystem.Models;

    public class StudentSystemContext : DbContext
    {
        
        public StudentSystemContext()
            : base("name=StudentSystemContext")
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<StudentSystemContext, Configuration>());
        }

        public IDbSet<Student> Students { get; set; }

        public IDbSet<Course> Courses { get; set; }

        public IDbSet<Resource> Resources { get; set; }

        public IDbSet<Homework> Homeworks { get; set; }

        public IDbSet<License> Licenses { get; set; }
    }
}