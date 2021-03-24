namespace SS_API.Enums
{
    /// <summary>
    /// Situação atual de um Projeto, esta podendo ser "Em Desenvolvimento" = 0 e "Publicado" = 1.
    /// </summary>
    public enum ProjectStatus : int
    {
        /// <summary>
        /// "Em Desenvolvimento".
        /// </summary>
        UnderDevelopment = 0,
        /// <summary>
        /// "Publicado".
        /// </summary>
        Published = 1
    }
}