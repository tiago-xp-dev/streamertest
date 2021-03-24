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
        private ProjectServices _services;
        private ILogger _logger;


        public ProjectController(ILoggerFactory logger)
        {
            _services = new ProjectServices();
            _logger = logger.CreateLogger("ProjectLogger");
        }

        /// <summary>
        /// Teste de documentação
        /// </summary>
        /// <param name="id">Teste de parâmetro</param>
        /// <returns>Teste de retorno</returns>
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

        [HttpGet("[action]/{id}")]
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

        [HttpPost("[action]/{name}/{courseId}")]
        public ActionResult<int> Create(string name, string image, string why, string what, string whatWillWeDo, int projectStatus, int courseId)
        {
            try
            {
                Project project = new Project()
                {
                    Name = name,
                    Image = image,
                    Why = why,
                    What = what,
                    WhatWillWeDo = whatWillWeDo,
                    ProjectStatus = (ProjectStatus)projectStatus,
                    CourseId = courseId
                };

                int addedProjectId = _services.InsertProject(project);
                return Created("project", addedProjectId);
            }
            catch (Exception e)
            {
                _logger.LogError($"Ocorreu uma exceção ao Inserir um Projeto!\n" +
                                 $"Parâmetros: {{image:{image}}} / " +
                                 $"{{name:{name}}} / {{why:{why}}} / " +
                                 $"{{whatWillWeDo:{whatWillWeDo}}} / {{what:{what}}} / " +
                                 $"{{projectStatus:{projectStatus}}} / {{courseId:{courseId}}}\n" +
                                 $"Exceção: {e.ToString()}");

                return Problem(
                    title: "Não foi possível Inserir o Projeto devido a um erro interno, tente novamente mais tarde!",
                    type: e.HResult.ToString());
            }
        }

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