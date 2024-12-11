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

app.MapPost("/cadastrar/aluno" , ([FromBody] Aluno aluno, [FromServices] AppDataContext ctx) =>
{
    ctx.Alunos?.Add(aluno);
    ctx.SaveChanges();
    return Results.Created("", aluno);
});

app.MapPost("/cadastrar/imc" , ([FromBody] Imc imc, [FromServices] AppDataContext ctx) =>
{
    var Aluno = ctx.Alunos?.Find(imc.AlunoId);
    if(Aluno == null){
        return Results.NotFound();
    }

    ctx.Imc?.Add(imc);
    imc.Aluno = Aluno;
    ctx.SaveChanges();
    return Results.Created("", imc);
});

app.MapPut("/imc/alterar/{id}", ([FromServices] AppDataContext ctx, [FromRoute] string id) =>
{
    Imc? imc = ctx.Imcs.Find(id);
    if (imc is null)
    {
        return Results.NotFound(" não encontrada");
    }

    ctx.Imcs.Update(imc);
    ctx.SaveChanges();
    return Results.Ok(ctx.Imcs.ToList());
});


app.MapGet("/listar/imcs", ([FromServices] AppDataContext ctx) => 
{
    return Results.Ok(ctx.Imcs?.ToList());
});

app.UseCors("Acesso Total");

app.Run();
