using System;

namespace API.modelos;

public class Imc
{
    public string ImcId { get; set; } = Guid.NewGuid().ToString();
    public double? Altura { get; set; }
    public double? Peso { get; set; }
    public string? classificação { get; set; }
    public string AlunoId { get; set; }
    public Aluno? Aluno { get; set; }
    public DateTime CriadoEm { get; set; } = DateTime.Now;
}