using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using SS_API.Data;
using SS_API.Model;

namespace SS_API.Services
{
    public class CourseServices
    {
        private readonly static IConfigurationBuilder _builder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
        private readonly static IConfigurationRoot _configuration = _builder.Build();
        private DbContextOptions<StreamerContext> _options;

        public CourseServices()
        {
            var connectionString = _configuration.GetConnectionString("DefaultConnection");

            var contextOptions = new DbContextOptionsBuilder<StreamerContext>()
                .UseSqlite(connectionString).Options;

            _options = contextOptions;
        }

        public int InsertCourse(Course course)
        {
            using (var db = new StreamerContext(_options))
            {
                Course addedCourse = db.Courses.Add(course).Entity;
                db.SaveChanges();

                return addedCourse.Id;
            }
        }

        public List<Course> GetAllCourses()
        {
            using (var db = new StreamerContext(_options))
            {
                List<Course> results = db.Courses.AsNoTracking().ToList();

                return results;
            }
        }

        public Course GetCourseById(int id)
        {
            using (var db = new StreamerContext(_options))
            {
                Course course = db.Courses
                .AsNoTracking()
                .FirstOrDefault(
                    course =>
                    course.Id == id
                );

                return course;
            }
        }

        public bool UpdateCourse(Course course)
        {
            bool updateState;

            using (var db = new StreamerContext(_options))
            {
                try
                {
                    db.Courses.Update(course);
                    db.SaveChanges();
                    updateState = true;
                }
                catch
                {
                    updateState = false;
                }
            }

            return updateState;
        }

        public bool DeleteCourse(Course course)
        {
            bool deleteState;

            using (var db = new StreamerContext(_options))
            {
                try
                {
                    db.Courses.Remove(course);
                    db.SaveChanges();
                    deleteState = true;
                }
                catch
                {
                    deleteState = false;
                }
            }

            return deleteState;
        }
    }
}