using System.ComponentModel.DataAnnotations;

namespace SS_API.Model
{
    /// <summary>
    /// Refere-se a uma instância de um Curso.
    /// </summary>
    public class Course
    {
        /// <summary>
        /// Identificador primário do Curso no Banco de Dados.
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Nome do Curso.
        /// </summary>
        public string Name { get; set; }
    }
}