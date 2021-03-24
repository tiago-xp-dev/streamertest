using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SS_API.Model;
using SS_API.Services;

namespace SS_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CourseController : ControllerBase
    {
        private CourseServices _services;
        private ILogger _logger;

        public CourseController(ILoggerFactory logger)
        {
            _services = new CourseServices();
            _logger = logger.CreateLogger("CourseLogger");
        }

        /// <summary>
        /// Teste de documentação
        /// </summary>
        /// <param name="id">Teste de parâmetro</param>
        /// <returns>Teste de retorno</returns>
        [HttpGet("[action]/{id}")]
        public ActionResult<Course> GetById(int id)
        {
            try
            {
                Course result = _services.GetCourseById(id);

                if (result == null)
                    return NoContent();

                return Ok(result);
            }
            catch (Exception e)
            {
                _logger.LogError($"Ocorreu uma exceção ao Ler um Curso!\n" +
                                 $"Parâmetros: {{id:{id}}}\n" +
                                 $"Exceção: {e.ToString()}");

                return Problem(
                    title: "Não foi possível Ler o Curso devido a um erro interno, tente novamente mais tarde!",
                    type: e.HResult.ToString());
            }
        }

        [HttpGet("[action]")]
        public ActionResult<List<Course>> GetAllCourses()
        {
            try
            {
                List<Course> results = _services.GetAllCourses();
                
                if (results.Count == 0)
                    return NoContent();

                return Ok(results);
            }
            catch (Exception e)
            {
                _logger.LogError("Ocorreu uma exceção ao Ler todos os Cursos!\n" +
                                 "Exceção: " + e.ToString());
                return Problem(
                    title: "Não foi possível Ler os Cursos devido a um erro interno, tente novamente mais tarde!",
                    type: e.HResult.ToString());
            }
        }

        [HttpPost("[action]/{name}")]
        public ActionResult<int> Create(string name)
        {
            try
            {
                Course course = new Course()
                {
                    Name = name,
                };

                int addedCourseId = _services.InsertCourse(course);
                return Created("course", addedCourseId);
            }
            catch (Exception e)
            {
                _logger.LogError($"Ocorreu uma exceção ao Inserir um Curso!\n" +
                                 $"Parâmetros: {{name:{name}}}\n" +
                                 $"Exceção: {e.ToString()}");

                return Problem(
                    title: "Não foi possível Inserir o Curso devido a um erro interno, tente novamente mais tarde!",
                    type: e.HResult.ToString());
            }
        }

        [HttpDelete("[action]/{id}")]
        public ActionResult<bool> Delete(int id)
        {
            try
            {
                Course courseToDelete = _services.GetCourseById(id);

                if (courseToDelete == null)
                    return Ok(true);

                bool deleteState = _services.DeleteCourse(courseToDelete);

                return Accepted(deleteState);
            }
            catch (Exception e)
            {
                _logger.LogError($"Ocorreu uma exceção ao Deletar um Curso!\n" +
                                 $"Parâmetros: {{id:{id}}}\n" +
                                 $"Exceção: {e.ToString()}");

                return Problem(
                    title: "Não foi possível Deletar o Curso devido a um erro interno, tente novamente mais tarde!",
                    type: e.HResult.ToString());
            }
        }

        [HttpPut("[action]")]
        public ActionResult<bool> Update([FromBody] Course course)
        {
            try
            {
                bool updateState = _services.UpdateCourse(course);

                return Ok(updateState);
            }
            catch (Exception e)
            {
                _logger.LogError($"Ocorreu uma exceção ao Atualizar um Curso!\n" +
                                 $"Parâmetros: {{Course.Id:{course.Id}}} / {{Course.Name:{course.Name}}}\n" +
                                 $"Exceção: {e.ToString()}");

                return Problem(
                    title: "Não foi possível Atualizar o Curso devido a um erro interno, tente novamente mais tarde!",
                    type: e.HResult.ToString());
            }
        }
    }
}