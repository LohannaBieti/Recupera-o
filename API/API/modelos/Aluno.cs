using System;

namespace API.modelos;

public class Aluno
{
    public string AlunoId { get; set; } 
    public string? Nome { get; set; }
    public string? Sobrenome { get; set; }
    public DateTime CriadoEm { get; set; } = DateTime.Now;
    
    
}