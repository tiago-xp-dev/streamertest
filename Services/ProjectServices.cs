using System.Linq;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using SS_API.Data;
using SS_API.Model;
using SS_API.Utils;

namespace SS_API.Services
{
    /// <summary>
    /// Serviços para a relização da interação entre a tabela de Projetos e o código.
    /// </summary>
    public class ProjectServices
    {
        /// <summary>
        /// Opções do Contexto do Banco de Dados.
        /// </summary>
        private DbContextOptions<StreamerContext> _options;

        /// <summary>
        /// Configura e instancia um novo <see cref="ProjectServices"/>.
        /// </summary>
        public ProjectServices()
        {
            ConfigurationUtils ConfigUtils = new ConfigurationUtils();

            var connectionString = ConfigUtils.Configuration
            .GetConnectionString("DefaultConnection");

            var contextOptions = new DbContextOptionsBuilder<StreamerContext>()
            .UseSqlite(connectionString).Options;

            _options = contextOptions;
        }

        /// <summary>
        /// Insere um novo Projeto ao banco de dados com base no <seealso cref="Project"/>> fornecido.
        /// </summary>
        /// <param name="project">O Projeto a ser inserido no banco de dados.</param>
        /// <returns><see cref="int"/> Identificador primário do Projeto inserido.</returns>
        public int InsertProject(Project project)
        {
            using (var db = new StreamerContext(_options))
            {
                Project addedProject = db.Projects.Add(project).Entity;
                db.SaveChanges();

                return addedProject.Id;
            }
        }

        /// <summary>
        /// Lê um Projeto existente no banco de dados com base no identificador primário <see cref="int"/> fornecido. 
        /// </summary>
        /// <param name="id">O Identificador primário do Projeto a ser lido.</param>
        /// <returns><see cref="Project"/>Projeto lido.</returns>
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

        /// <summary>
        /// Lê os Projetos existentes no banco de dados com base no identificador primário <see cref="int"/> de um Curso fornecido. 
        /// </summary>
        /// <param name="id">O Identificador primário do curso a ler os Projetos.</param>
        /// <returns><see cref="List{Project}"/> Lista genérica contendo todos os Projetos do Curso.</returns>
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

        /// <summary>
        /// Atualiza um Projeto existente no banco de dados com base no <see cref="Project"/> fornecido. 
        /// </summary>
        /// <param name="project">O Projeto a ser deletado.</param>
        /// <returns><see cref="bool"/>Indicando se o Projeto foi(1) ou não(0) atualizado.</returns>
        public bool UpdateProject(Project project)
        {
            bool updateState;

            using (var db = new StreamerContext(_options))
            {
                try
                {
                    db.Projects.Update(project);
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

        /// <summary>
        /// Deleta um Projeto existente no banco de dados com base no <see cref="Project"/> fornecido. 
        /// </summary>
        /// <param name="project">O Projeto a ser deletado.</param>
        /// <returns><see cref="bool"/>Indicando se o Projeto foi(1) ou não(0) deletado.</returns>
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