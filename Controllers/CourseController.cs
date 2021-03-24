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
        /// <summary>
        /// Serviços do modelo Course.
        /// </summary>
        private CourseServices _services;

        /// <summary>
        /// Serviços de Logging.
        /// </summary>
        private ILogger _logger;

        public CourseController(ILoggerFactory logger)
        {
            _services = new CourseServices();
            _logger = logger.CreateLogger("CourseLogger");
        }

        /// <summary>
        /// Realiza a leitura de um Curso correspondente ao identificador <paramref name="id"/> fornecido.
        /// </summary>
        /// <param name="id">Identificador do Curso a ser lido.</param>
        /// <response code="200">Retorna o Curso encontrado.</response>
        /// <response code="204">Retorna vazio caso nada seja encontrado.</response> 
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

        /// <summary>
        /// Realiza a leitura de TODOS os Cursos.
        /// </summary> 
        /// <response code="200">Retorna uma lista genérica contendo os Cursos encontrados.</response>
        /// <response code="204">Retorna vazio caso nada seja encontrado.</response>
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

        /// <summary>
        /// Cria um novo Curso com o <paramref name="course"/> fornecido.
        /// </summary>
        /// <param name="course">O Objeto do Curso a ser criado (Deve ser passado através do Body).</param>
        /// <response code="201">Retorna um int referente a identificação do Curso Inserido.</response>
        [HttpPost("[action]")]
        public ActionResult<int> Create([FromBody] Course course)
        {
            try
            {
                int addedCourseId = _services.InsertCourse(course);
                return Created("course", addedCourseId);
            }
            catch (Exception e)
            {
                _logger.LogError($"Ocorreu uma exceção ao Inserir um Curso!\n" +
                                 $"Parâmetros: {{name:{course.Name}}}\n" +
                                 $"Exceção: {e.ToString()}");

                return Problem(
                    title: "Não foi possível Inserir o Curso devido a um erro interno, tente novamente mais tarde!",
                    type: e.HResult.ToString());
            }
        }

        /// <summary>
        /// Deleta um Curso e todos os projetos correspondentes com base no Identificador <paramref name="id"/> fornecido.
        /// </summary>
        /// <param name="id">Identificador do Curso a ser deletado.</param>
        /// <response code="200">Retorna um boolean(true) pois este Projeto já foi Deletado.</response>
        /// <response code="202">Retorna um boolean representando o Status do Delete: Sucesso(true) ou Fracasso(false)</response>
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

        /// <summary>
        /// Atualiza um Curso com os dados fornecidos dentro do Objeto do tipo Course.
        /// </summary>
        /// <param name="course">Curso a ser atualizado (Deve ser passado através do Body).</param>
        /// <response code="200">Retorna um boolean representando o Status da Update: Sucesso(true) ou Fracasso(false)</response>
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