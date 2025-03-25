using Person.Models;
using Person.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http.HttpResults;


namespace Person.Routes;


// Static pois não vou instanciar
public static class PersonRoute
{
  //Classe static, etnão todos os membros são satatic
  public static void PersonRoutes(this WebApplication app)
  {
    // no navegador fica /person
    //app.MapGet("person", () => new PersonModel(name:"Filipi"));

    var route = app.MapGroup(prefix: "person");

    // async : requisição assíncrona
    // Adiciona no bd
    app.MapPost("", async (PersonRequest req, PersonContext context) =>
    {
      var person = new PersonModel(req.name);
      await context.AddAsync(person);
      await context.SaveChangesAsync();
    }
    );

    route.MapGet("", async (PersonContext context) =>
    {
      var people = await context.People.ToListAsync();

      /* Posso retornar:
          Results.BadRequest(people);
          Results.NotFound(people);
          Results.NoContent(people);
          Posso adicionar se o conteúdo de "people" for vazio, retorna "NoContent", etc.
      */
      return Results.Ok(people);
    });

    route.MapPut("{id:guid}", async (Guid id, PersonRequest req, PersonContext context) =>
        {
          var person = await context.People.FirstOrDefaultAsync(person => person.Id == id);

          if (person == null)
            return Results.NotFound();

          person.ChangeName(req.name);
          await context.SaveChangesAsync();

          return Results.Ok(person);
        });

    route.MapDelete("{id:guid}",
      async (Guid id, PersonContext context) =>
    {
      var person = await context.People.FirstOrDefaultAsync(person => person.Id == id);
      if (person == null)
        return Results.NotFound();


      person.SetInactive();
      await context.SaveChangesAsync();
      return Results.Ok(person);
    });

  }
}