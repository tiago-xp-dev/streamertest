using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using SS_API.Data;
using SS_API.Model;
using System.Linq;
using System.IO;
using System;
using System.Collections.Generic;

namespace SS_API.Services
{
    public class ProjectServices
    {
        private readonly static IConfigurationBuilder _builder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
        private readonly static IConfigurationRoot _configuration = _builder.Build();
        private DbContextOptions<StreamerContext> _options;

        public ProjectServices()
        {
            var connectionString = _configuration.GetConnectionString("DefaultConnection");

            var contextOptions = new DbContextOptionsBuilder<StreamerContext>()
            .UseSqlite(connectionString).Options;

            _options = contextOptions;
        }

        public async Task<int> InsertProject(Project project)
        {
            using (var db = new StreamerContext(_options))
            {
                Project addedProject = db.Projects.Add(project).Entity;
                db.SaveChanges();

                return addedProject.Id;
            }
        }

        public Project GetProjectById(int id)
        {
            using (var db = new StreamerContext(_options))
            {
                Project foundProject = db.Projects
                .AsNoTracking()
                .FirstOrDefault(
                    proj =>
                    proj.Id == id
                );

                foundProject.Course = db.Courses
                .AsNoTracking()
                .FirstOrDefault(
                    course =>
                    course.Id == foundProject.CourseId
                );

                return foundProject;
            }
        }

        public List<Project> GetProjectByCourse(int id)
        {
            using (var db = new StreamerContext(_options))
            {
                List<Project> foundProjects = db.Projects
                .AsNoTracking()
                .Where(
                proj =>
                    proj.CourseId == id
                ).ToList();

                if (foundProjects.Count > 0)
                {
                    foundProjects.ForEach(
                    proj =>
                        proj.Course = db.Courses
                        .AsNoTracking()
                        .FirstOrDefault(
                        course =>
                            course.Id == proj.CourseId
                        )
                    );
                }

                return foundProjects;
            }
        }

        public bool UpdateProject(Project project)
        {
            bool updateState;

            using (var db = new StreamerContext(_options))
            {
                try
                {
                    db.Update(project);
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

        public bool DeleteProject(Project project)
        {
            bool deleteState;

            using (var db = new StreamerContext(_options))
            {
                try
                {
                    db.Projects.Remove(project);
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