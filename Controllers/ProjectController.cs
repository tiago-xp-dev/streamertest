using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using SS_API.Enums;
using SS_API.Model;
using SS_API.Services;

namespace SS_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectController : ControllerBase
    {
        ProjectServices services;

        public ProjectController()
        {
            services = new ProjectServices();
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
                Project result = services.GetProjectById(id);

                if (result == null)
                    return NoContent();

                return Ok(result);
            }
            catch (Exception e)
            {
                return Problem(
                    title: "Não foi possível Ler o Projeto devido a um erro interno, tente novamente mais tarde!",
                    type: e.HResult.ToString());
            }
        }

        [HttpGet("[action]/{id}")]
        public ActionResult<List<Project>> GetByCourse(int id)
        {
            try
            {
                List<Project> results = services.GetProjectByCourse(id);

                if (results.Count == 0)
                    return NoContent();

                return Ok(results);
            }
            catch (Exception e)
            {
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

                int addedProjectId = services.InsertProject(project);
                return Created("project", addedProjectId);
            }
            catch (Exception e)
            {
                return Problem(
                    title: "Não foi possível inserir o Projeto devido a um erro interno, tente novamente mais tarde!",
                    type: e.HResult.ToString());
            }
        }

        [HttpDelete("[action]/{id}")]
        public ActionResult<bool> Delete(int id)
        {
            try
            {
                Project projectToDelete = services.GetProjectById(id);

                if (projectToDelete == null)
                    return Ok(true);

                bool deleteState = services.DeleteProject(projectToDelete);

                return Accepted(deleteState);
            }
            catch (Exception e)
            {
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
                bool updateState = services.UpdateProject(project);

                return Ok(updateState);
            }
            catch (Exception e)
            {
                return Problem(
                    title: "Não foi possível atualizar o Projeto devido a um erro interno, tente novamente mais tarde!",
                    type: e.HResult.ToString());
            }
        }
    }
}