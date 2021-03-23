using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using SS_API.Model;
using SS_API.Services;

namespace SS_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CourseController : ControllerBase
    {
        CourseServices services;

        public CourseController()
        {
            services = new CourseServices();
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
                Course result = services.GetCourseById(id);

                if (result == null)
                    return NoContent();

                return Ok(result);
            }
            catch (Exception e)
            {
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
                List<Course> results = services.GetAllCourses();

                if (results.Count == 0)
                    return NoContent();

                return Ok(results);
            }
            catch (Exception e)
            {
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
                Course course= new Course()
                {
                    Name = name,                   
                };
                
                int addedCourseId = services.InsertCourse(course);
                return Created("course", addedCourseId);
            }
            catch (Exception e)
            {
                return Problem(
                    title: "Não foi possível inserir o Curso devido a um erro interno, tente novamente mais tarde!",
                    type: e.HResult.ToString());
            }
        }

        [HttpDelete("[action]/{id}")]
        public ActionResult<bool> Delete(int id)
        {
            try
            {
                Course courseToDelete = services.GetCourseById(id);

                if (courseToDelete == null)
                    return Ok(true);

                bool deleteState = services.DeleteCourse(courseToDelete);

                return Accepted(deleteState);
            }
            catch (Exception e)
            {
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
                bool updateState = services.UpdateCourse(course);

                return Ok(updateState);
            }
            catch (Exception e)
            {
                return Problem(
                    title: "Não foi possível atualizar o Curso devido a um erro interno, tente novamente mais tarde!",
                    type: e.HResult.ToString());
            }
        }
    }
}