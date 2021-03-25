using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using SS_API.Data;
using SS_API.Model;
using SS_API.Utils;

namespace SS_API.Services
{
    /// <summary>
    /// Serviços para a relização da interação entre a tabela de Cursos e o código.
    /// </summary>
    public class CourseServices
    {
        /// <summary>
        /// Opções do Contexto do Banco de Dados.
        /// </summary>
        private DbContextOptions<StreamerContext> _options;

        /// <summary>
        /// Configura e instancia um novo <see cref="CourseServices"/>.
        /// </summary>
        public CourseServices()
        {
            ConfigurationUtils ConfigUtils = new ConfigurationUtils();

            var connectionString = ConfigUtils.Configuration
            .GetConnectionString("DefaultConnection");

            var contextOptions = new DbContextOptionsBuilder<StreamerContext>()
            .UseSqlite(connectionString).Options;

            _options = contextOptions;
        }

        /// <summary>
        /// Insere um novo Curso ao banco de dados com base no <seealso cref="Course"/>> fornecido.
        /// </summary>
        /// <param name="course">O Curso a ser inserido no banco de dados.</param>
        /// <returns><see cref="int"/> Identificador primário do Curso inserido.</returns>
        public int InsertCourse(Course course)
        {
            using (var db = new StreamerContext(_options))
            {
                Course addedCourse = db.Courses.Add(course).Entity;
                db.SaveChanges();

                return addedCourse.Id;
            }
        }

        /// <summary>
        /// Lê todos os Cursos existentes no banco de dados. 
        /// </summary>
        /// <returns><see cref="List{Course}"/> Lista genérica contendo todos os Cursos.</returns>
        public List<Course> GetAllCourses()
        {
            using (var db = new StreamerContext(_options))
            {
                List<Course> results = db.Courses.AsNoTracking().ToList();

                return results;
            }
        }

        /// <summary>
        /// Lê um Curso existente no banco de dados com base no identificador primário <see cref="int"/> fornecido. 
        /// </summary>
        /// <param name="id">O Identificador primário do curso a ser lido.</param>
        /// <returns><see cref="Course"/>Curso lido.</returns>
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

        /// <summary>
        /// Atualiza um Curso existente no banco de dados com base no <see cref="Course"/> fornecido. 
        /// </summary>
        /// <param name="course">O Curso a ser atualizado.</param>
        /// <returns><see cref="bool"/>Indicando se o curso foi(1) ou não(0) atualizado.</returns>
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

        /// <summary>
        /// Deleta um Curso existente no banco de dados com base no <see cref="Course"/> fornecido. 
        /// </summary>
        /// <param name="course">O Curso a ser deletado.</param>
        /// <returns><see cref="bool"/>Indicando se o curso foi(1) ou não(0) deletado.</returns>
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