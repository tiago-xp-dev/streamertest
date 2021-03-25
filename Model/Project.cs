using System.ComponentModel.DataAnnotations;
using SS_API.Enums;

namespace SS_API.Model
{
    /// <summary>
    /// Refere-se a uma instância de um Projeto que é relácionado a um Curso.
    /// </summary>
    public class Project
    {
        /// <summary>
        /// Identificador primário do Projeto no Banco de Dados.
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Diretório no qual a imagem deste projeto está armazenada. 
        /// </summary>
        public string Image { get; set; }

        /// <summary>
        /// Nome do Projeto.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Razão do Projeto.
        /// </summary>
        public string Why { get; set; }

        /// <summary>
        /// O que será realizado no Projeto.
        /// </summary>
        public string WhatWillWeDo { get; set; }

        /// <summary>
        /// O que é o Projeto.
        /// </summary>
        public string What { get; set; }

        /// <summary>
        /// Status do Projeto.
        /// </summary>
        public ProjectStatus ProjectStatus { get; set; }

        /// <summary>
        /// Curso no qual este Projeto pertence.
        /// </summary>
        public Course Course { get; set; }

        /// <summary>
        /// Identificador primário do Curso deste Projeto no Banco de Dados.
        /// </summary>
        public int CourseId { get; set; }
    }
}