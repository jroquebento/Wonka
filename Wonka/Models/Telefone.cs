﻿namespace Wonka.Models
{
    public class Telefone
    {
        public int Id { get; set; }
        public int IdPessoa { get; set; }
        public string Tipo { get; set; }
        public string DDD { get; set; }
        public string Numero { get; set; }
    }
}
