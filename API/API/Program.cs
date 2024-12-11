using API.modelos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<AppDataContext>();

builder.Services.AddCors(options =>
    options.AddPolicy("Acesso Total",
        configs => configs
            .AllowAnyOrigin()
            .AllowAnyHeader()
            .AllowAnyMethod())
);

var app = builder.Build();

app.MapGet("/", () => "Recuperação!");

app.MapPost("/cadastrar/aluno", ([FromBody] Aluno aluno, [FromServices] AppDataContext ctx) =>
{
    ctx.Alunos.Add(aluno);
    ctx.SaveChanges();
    return Results.Created("/cadastrar/aluno", aluno); 
});

app.MapPost("/cadastrar/imc", ([FromBody] Imc imc, [FromServices] AppDataContext ctx) =>
{
    var aluno = ctx.Alunos.Find(imc.AlunoId);
    if (aluno == null)
    {
        return Results.NotFound("Aluno não encontrado.");
    }

    ctx.Imcs.Add(imc);
    imc.Aluno = aluno; 
    ctx.SaveChanges();
    return Results.Created("/cadastrar/imc", imc); 
});

app.MapPut("/imc/alterar/{id}", ([FromServices] AppDataContext ctx, [FromRoute] string id, [FromBody] Imc imcAlterado) =>
{
    var imc = ctx.Imcs.Find(id);
    if (imc == null)
    {
        return Results.NotFound("IMC não encontrado.");
    }

    imc.Peso = imcAlterado.Peso;
    imc.Altura = imcAlterado.Altura;
    ctx.Imcs.Update(imc);
    ctx.SaveChanges();
    return Results.Ok(imc); 
});

app.MapGet("/listar/imcs", ([FromServices] AppDataContext ctx) =>
{
    var imcs = ctx.Imcs.ToList();
    return Results.Ok(imcs);
});

app.MapGet("/listar/imcs/{alunoId}", ([FromServices] AppDataContext ctx, [FromRoute] int alunoId) =>
{
    var imcs = ctx.Imcs.Where(i => i.AlunoId == alunoId).ToList(); 
    if (!imcs.Any())
    {
        return Results.NotFound($"Nenhum IMC encontrado para o aluno com ID {alunoId}.");
    }
    return Results.Ok(imcs); 
});


app.UseCors("Acesso Total");

app.Run();
