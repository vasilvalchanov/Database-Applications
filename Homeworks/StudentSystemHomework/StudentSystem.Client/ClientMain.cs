using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentSystem.Client
{
    using StudentSystem.Data;
    using StudentSystem.Models;

    class ClientMain
    {
        static void Main(string[] args)
        {
            var studentContext = new StudentSystemContext();
            
            //PrintAllStudentsWithTheirHomeworks(studentContext);
            //PrintAllCoursesWithTheirResources(studentContext);
        
            //var oopCourse = studentContext.Courses.FirstOrDefault(c => c.Name == "C# OOP");
            //oopCourse.Resources.Add(new Resource()
            //                            {
            //                                Name = "Inheritance Lab",
            //                                ResourceType = ResourceType.Document,
            //                                Url = "www.daimilaba.com"
            //                            });

            //oopCourse.Resources.Add(new Resource()
            //{
            //    Name = "Inheritance Lab Solution",
            //    ResourceType = ResourceType.Video,
            //    Url = "www.daimilaba.com"
            //});

            //oopCourse.Resources.Add(new Resource()
            //{
            //    Name = "Sample Exam",
            //    ResourceType = ResourceType.Document,
            //    Url = "www.exam.com"
            //});

            //oopCourse.Resources.Add(new Resource()
            //{
            //    Name = "Teamwork assignment",
            //    ResourceType = ResourceType.Document,
            //    Url = "www.team.com"
            //});

            //studentContext.SaveChanges();

            //PrintAllCoursesWithMoreThanFiveResources(studentContext);

            //PrintAllActiveCoursesOnDate(studentContext, new DateTime(2016, 03, 15));


            var student = studentContext.Students.FirstOrDefault(s => s.Name == "Baba Gicka");
            var course = studentContext.Courses.FirstOrDefault(c => c.Name == "C# OOP");
            student.Courses.Add(course);
            studentContext.SaveChanges();

            PrintStudentsCourcesAndTaxes(studentContext);


        }

        private static void PrintAllStudentsWithTheirHomeworks(StudentSystemContext context)
        {
            var students =
                context.Students.Select(
                    s =>
                    new
                        {
                            Name = s.Name,
                            Homeworks = s.Homeworks.Select(h => new { Content = h.Content, ContentType = h.ContentType })
                        });

            foreach (var student in students)
            {
                Console.WriteLine(student.Name);
                Console.WriteLine("--Homeworks:");

                foreach (var homework in student.Homeworks)
                {
                    Console.WriteLine("----{0} - {1}", homework.Content, homework.ContentType);
                }
            }
        }

        private static void PrintAllCoursesWithTheirResources(StudentSystemContext context)
        {
            var courses =
                context.Courses.OrderBy(c => c.StartDate)
                    .ThenByDescending(c => c.EndDate)
                    .Select(c => new { c.Name, c.Description, c.Resources });

            foreach (var course in courses)
            {
                Console.WriteLine("Course Name: {0}", course.Name);
                Console.WriteLine("Description: {0}", course.Description ?? "No Description");
                Console.WriteLine("Resources:");

                foreach (var resource in course.Resources)
                {
                    Console.WriteLine("--{0}", resource.Name);
                    Console.WriteLine("--{0}", resource.ResourceType);
                    Console.WriteLine("--{0}", resource.Url);
                    Console.WriteLine();
                }

                Console.WriteLine();
            }
        }

        private static void PrintAllCoursesWithMoreThanFiveResources(StudentSystemContext context)
        {
            var courses =
                context.Courses.Where(c => c.Resources.Count > 5)
                    .OrderByDescending(c => c.Resources.Count)
                    .ThenByDescending(c => c.StartDate)
                    .Select(c => new { CourseName = c.Name, ResourceCount = c.Resources.Count });

            foreach (var course in courses)
            {
                Console.WriteLine("Course: {0}", course.CourseName);
                Console.WriteLine("Resources: {0}", course.ResourceCount);
            }
        }

        private static void PrintAllActiveCoursesOnDate(StudentSystemContext context, DateTime date)
        {

            var courses =
                context.Courses.Where(c => c.StartDate <= date && c.EndDate >= date).ToList()
                    .Select(
                        c =>
                        new
                            {
                                CourseName = c.Name,
                                StartDate = c.StartDate,
                                EndDate = c.EndDate,
                                Duration = c.EndDate - c.StartDate,
                                StudentsCount = c.Students.Count
                            })
                    .OrderByDescending(c => c.StudentsCount)
                    .ThenByDescending(c => c.Duration);


            if (courses.Any())
            {
                foreach (var course in courses)
                {
                    Console.WriteLine("Course Name: {0}", course.CourseName);
                    Console.WriteLine("Start on: {0}", course.StartDate);
                    Console.WriteLine("End on: {0}", course.EndDate);
                    Console.WriteLine("Duration in days: {0}", course.Duration.Days);
                    Console.WriteLine("Number of enrolled students: {0}", course.StudentsCount);
                }
            }
            else
            {
                Console.WriteLine("There is not active course on this date");
            }
        }

        private static void PrintStudentsCourcesAndTaxes(StudentSystemContext context)
        {
            var result =
                context.Students.Select(
                    s =>
                    new
                        {
                            StudentName = s.Name,
                            NumberOfEnrolledCources = s.Courses.Count,
                            TotalPricesOfAllEnrolledCourses = s.Courses.Sum(c => c.Price),
                            AveragePriceOfCourse = s.Courses.Average(c => c.Price)
                        })
                    .OrderByDescending(s => s.TotalPricesOfAllEnrolledCourses)
                    .ThenBy(s => s.StudentName);

            foreach (var student in result)
            {
                Console.WriteLine("Student: {0}", student.StudentName);
                Console.WriteLine("Number of enrolled courses: {0}", student.NumberOfEnrolledCources);
                Console.WriteLine("Total price of all courses: {0:f2}", student.TotalPricesOfAllEnrolledCourses);
                Console.WriteLine("Average price per course: {0:f2}", student.AveragePriceOfCourse);
            }
        }
    }
}
