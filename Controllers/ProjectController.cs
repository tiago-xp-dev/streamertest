using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SS_API.Enums;
using SS_API.Model;
using SS_API.Services;

namespace SS_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectController : ControllerBase
    {
        /// <summary>
        /// Serviços do modelo Project.
        /// </summary>
        private ProjectServices _services;

        /// <summary>
        /// Serviços de Logging.
        /// </summary>
        private ILogger _logger;


        public ProjectController(ILoggerFactory logger)
        {
            _services = new ProjectServices();
            _logger = logger.CreateLogger("ProjectLogger");
        }

        /// <summary>
        /// Realiza a leitura e um Projeto correspondente ao identificador <paramref name="id"/> fornecido.
        /// </summary>
        /// <param name="id">Identificador do Projeto a ser lido.</param>
        /// <response code="200">Retorna o Projeto encontrado.</response>
        /// <response code="204">Retorna vazio caso nada seja encontrado.</response> 
        [HttpGet("[action]/{id}")]
        public ActionResult<Project> GetById(int id)
        {
            try
            {
                Project result = _services.GetProjectById(id);

                if (result == null)
                    return NoContent();

                return Ok(result);
            }
            catch (Exception e)
            {
                _logger.LogError($"Ocorreu uma exceção ao Ler um Projeto!\n" +
                                 $"Parâmetros: {{id:{id}}}\n" +
                                 $"Exceção: {e.ToString()}");

                return Problem(
                    title: "Não foi possível Ler o Projeto devido a um erro interno, tente novamente mais tarde!",
                    type: e.HResult.ToString());
            }
        }

        /// <summary>
        /// Realiza a leitura de vários Projetos correspondentes ao identificador de um Curso <paramref name="id"/> fornecido.
        /// </summary>
        /// <param name="id">Identificador do Projeto a ser lido.</param> 
        /// <response code="200">Retorna uma lista genérica contendo os Projetos encontrados.</response>
        /// <response code="204">Retorna vazio caso nada seja encontrado.</response>
        [HttpGet("[action]/{courseId}")]
        public ActionResult<List<Project>> GetByCourse(int courseId)
        {
            try
            {
                List<Project> results = _services.GetProjectByCourse(courseId);

                if (results.Count == 0)
                    return NoContent();

                return Ok(results);
            }
            catch (Exception e)
            {
                _logger.LogError($"Ocorreu uma exceção ao Ler os Projetos de um Curso!\n" +
                                 $"Parâmetros: {{courseId:{courseId}}}\n" +
                                 $"Exceção: {e.ToString()}");

                return Problem(
                    title: "Não foi possível Ler os Projetos devido a um erro interno, tente novamente mais tarde!",
                    type: e.HResult.ToString());
            }
        }

        /// <summary>
        /// Cria um novo Projeto com base no <paramref name="project"/> fornecido.
        /// </summary>
        /// <param name="project">Objeto do Projeto a ser criado.</param>
        /// <response code="201">Retorna um int referente a identificação do Projeto Inserido.</response>
        [HttpPost("[action]")]
        public ActionResult<int> Create([FromBody] Project project)
        {
            try
            {
                project.Course = null;

                int addedProjectId = _services.InsertProject(project);
                return Created("project", addedProjectId);
            }
            catch (Exception e)
            {
                _logger.LogError($"Ocorreu uma exceção ao Inserir um Projeto!\n" +
                                 $"Parâmetros: {{image:{project.Image}}} / " +
                                 $"{{name:{project.Name}}} / {{why:{project.Why}}} / " +
                                 $"{{whatWillWeDo:{project.WhatWillWeDo}}} / {{what:{project.What}}} / " +
                                 $"{{projectStatus:{project.ProjectStatus}}} / {{courseId:{project.CourseId}}}\n" +
                                 $"Exceção: {e.ToString()}");

                return Problem(
                    title: "Não foi possível Inserir o Projeto devido a um erro interno, tente novamente mais tarde!",
                    type: e.HResult.ToString());
            }
        }

        /// <summary>
        /// Deleta um Projeto com base no Identificador <paramref name="id"/> fornecido.
        /// </summary>
        /// <param name="id">Identificador do Projeto a ser deletado.</param>
        /// <response code="200">Retorna um boolean(true) pois este Projeto já foi Deletado.</response>
        /// <response code="202">Retorna um boolean representando o Status do Delete: Sucesso(true) ou Fracasso(false)</response>
        [HttpDelete("[action]/{id}")]
        public ActionResult<bool> Delete(int id)
        {
            try
            {
                Project projectToDelete = _services.GetProjectById(id);

                if (projectToDelete == null)
                    return Ok(true);

                bool deleteState = _services.DeleteProject(projectToDelete);

                return Accepted(deleteState);
            }
            catch (Exception e)
            {
                _logger.LogError($"Ocorreu uma exceção ao Deletar um Projeto!\n" +
                                 $"Parâmetros: {{id:{id}}}\n" +
                                 $"Exceção: {e.ToString()}");

                return Problem(
                    title: "Não foi possível Deletar o Projeto devido a um erro interno, tente novamente mais tarde!",
                    type: e.HResult.ToString());
            }
        }

        /// <summary>
        /// Atualiza um Projeto com os dados fornecidos dentro do Objeto do tipo Project.
        /// </summary>
        /// <param name="project">Project a ser atualizado (Deve ser passado através do Body).</param>
        /// <response code="200">Retorna um boolean representando o Status da Atualização: Sucesso(true) ou Fracasso(false)</response>
        [HttpPut("[action]")]
        public ActionResult<bool> Update([FromBody] Project project)
        {
            try
            {
                bool updateState = _services.UpdateProject(project);

                return Ok(updateState);
            }
            catch (Exception e)
            {
                _logger.LogError($"Ocorreu uma exceção ao Atualizar um Projeto!\n" +
                                 $"Parâmetros: {{id:{project.Id}}} / {{image:{project.Image}}} / " +
                                 $"{{name:{project.Name}}} / {{why:{project.Why}}} / " +
                                 $"{{whatWillWeDo:{project.WhatWillWeDo}}} / {{what:{project.What}}} / " +
                                 $"{{projectStatus:{project.ProjectStatus}}} / {{courseId:{project.CourseId}}}\n" +
                                 $"Exceção: {e.ToString()}");

                return Problem(
                    title: "Não foi possível Atualizar o Projeto devido a um erro interno, tente novamente mais tarde!",
                    type: e.HResult.ToString());
            }
        }
    }
}