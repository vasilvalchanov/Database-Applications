namespace StudentSystem.Data.Migrations
{
    using System;
    using System.CodeDom;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    using StudentSystem.Models;

    internal sealed class Configuration : DbMigrationsConfiguration<StudentSystemContext>
    {
        public Configuration()
        {
            this.AutomaticMigrationDataLossAllowed = false;
            this.AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(StudentSystemContext context)
        {
            if (!context.Students.Any() || !context.Courses.Any() || !context.Resources.Any() || !context.Homeworks.Any())
            {
                var studentsToSeed = new HashSet<Student>();
                var coursesToSeed = new HashSet<Course>();
                var resourcesToSeed = new HashSet<Resource>();
                var homeworksToSeed = new HashSet<Homework>();

                var student1 = new Student()
                {
                    Name = "Ivan Ivanov",
                    BirthDay = new DateTime(1989, 11, 05),
                    RegisteredOn = new DateTime(2016, 01, 10),
                    PhoneNumber = "0888987456"
                };

                var student2 = new Student()
                {
                    Name = "Petar Todorov",
                    BirthDay = new DateTime(1996, 12, 12),
                    RegisteredOn = new DateTime(2015, 07, 12),
                };

                var student3 = new Student()
                {
                    Name = "Baba Gicka",
                    BirthDay = new DateTime(1945, 01, 02),
                    RegisteredOn = new DateTime(2016, 02, 02),
                    PhoneNumber = "0888888888"
                };

                var student4 = new Student()
                {
                    Name = "Parlapun Shibekov",
                    BirthDay = new DateTime(1990, 03, 11),
                    RegisteredOn = new DateTime(2015, 12, 02),
                };

                var student5 = new Student()
                {
                    Name = "Temelko Ivanov",
                    BirthDay = new DateTime(1992, 05, 22),
                    RegisteredOn = new DateTime(2015, 03, 25),
                    PhoneNumber = "0898123654"
                };

                var course1 = new Course()
                {
                    Name = "C# OOP",
                    StartDate = new DateTime(2016, 03, 10),
                    EndDate = new DateTime(2016, 04, 29),
                    Price = 50
                };

                var course2 = new Course()
                {
                    Name = "C# HQC",
                    StartDate = new DateTime(2016, 05, 01),
                    EndDate = new DateTime(2016, 06, 15),
                    Price = 50
                };

                var course3 = new Course()
                {
                    Name = "C# Databases",
                    StartDate = new DateTime(2016, 06, 16),
                    EndDate = new DateTime(2016, 06, 17),
                    Price = 70
                };

                var course4 = new Course()
                {
                    Name = "C# Database Applications",
                    StartDate = new DateTime(2016, 03, 18),
                    EndDate = new DateTime(2016, 08, 30),
                    Price = 70
                };

                var resource1 = new Resource()
                {
                    Name = "Inheritance",
                    ResourceType = ResourceType.Video,
                    Course = course1,
                    Url = "some Url"
                };

                var resource2 = new Resource()
                {
                    Name = "Inheritance",
                    ResourceType = ResourceType.Presentation,
                    Course = course1,
                    Url = "some Url"
                };

                var resource3 = new Resource()
                {
                    Name = "EntityFrameworkCodeFirst",
                    ResourceType = ResourceType.Presentation,
                    Course = course4,
                    Url = "some Url"
                };

                var resource4 = new Resource()
                {
                    Name = "EntityFrameworkCodeFirst",
                    ResourceType = ResourceType.Video,
                    Course = course4,
                    Url = "some Url"
                };

                var resource5 = new Resource()
                {
                    Name = "TransactSQL",
                    ResourceType = ResourceType.Video,
                    Course = course3,
                    Url = "some Url"
                };

                var resource6 = new Resource()
                {
                    Name = "TransactSQL",
                    ResourceType = ResourceType.Document,
                    Course = course3,
                    Url = "some Url"
                };

                var resource7 = new Resource()
                {
                    Name = "Unit Testing",
                    ResourceType = ResourceType.Video,
                    Course = course2,
                    Url = "some Url"
                };

                var unitTestingHomework = new Homework()
                {
                    Content = "Some Content",
                    ContentType = ContentType.ApplicationZIP,
                    Course = course2,
                    Student = student2,
                    SubmissionDate = new DateTime(2016, 05, 10)
                };

                var mockingHomework = new Homework()
                {
                    Content = "Some Content",
                    ContentType = ContentType.ApplicationZIP,
                    Course = course2,
                    Student = student1,
                    SubmissionDate = new DateTime(2016, 05, 15)
                };

                var transactSQLHomework = new Homework()
                {
                    Content = "Some Content",
                    ContentType = ContentType.ApplicationPDF,
                    Course = course3,
                    Student = student4,
                    SubmissionDate = new DateTime(2016, 06, 21)
                };

                student1.Courses.Add(course2);
                course2.Students.Add(student1);
                student2.Courses.Add(course2);
                course2.Students.Add(student2);
                student3.Courses.Add(course3);
                course3.Students.Add(student3);
                student4.Courses.Add(course3);
                course3.Students.Add(student4);
                student5.Courses.Add(course1);
                course1.Students.Add(student5);

                studentsToSeed.Add(student1);
                studentsToSeed.Add(student2);
                studentsToSeed.Add(student3);
                studentsToSeed.Add(student4);
                studentsToSeed.Add(student5);

                coursesToSeed.Add(course1);
                coursesToSeed.Add(course2);
                coursesToSeed.Add(course3);
                coursesToSeed.Add(course4);

                homeworksToSeed.Add(transactSQLHomework);
                homeworksToSeed.Add(mockingHomework);
                homeworksToSeed.Add(unitTestingHomework);

                resourcesToSeed.Add(resource1);
                resourcesToSeed.Add(resource2);
                resourcesToSeed.Add(resource3);
                resourcesToSeed.Add(resource4);
                resourcesToSeed.Add(resource5);
                resourcesToSeed.Add(resource6);
                resourcesToSeed.Add(resource7);

                foreach (var student in studentsToSeed)
                {
                    context.Students.AddOrUpdate(student);
                }

                foreach (var course in coursesToSeed)
                {
                    context.Courses.AddOrUpdate(course);
                }

                foreach (var homework in homeworksToSeed)
                {
                    context.Homeworks.AddOrUpdate(homework);
                }

                foreach (var resource in resourcesToSeed)
                {
                    context.Resources.AddOrUpdate(resource);
                }

                context.SaveChanges();

            }
        }
    }
}
