using System;

namespace API.modelos;

    public class Imc
{
    public int Id { get; set; }
    public float Peso { get; set; }
    public float Altura { get; set; }
    public float ValorImc { get; set; }
    public int AlunoId { get; set; } 
    public Aluno Aluno { get; set; } 
}
