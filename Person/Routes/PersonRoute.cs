using Person.Models;

namespace Person.Routes;


// Static pois não vou instanciar
public static class PersonRoute
{
  //Classe static, etnão todos os membros são satatic
  public static void PersonRoutes(this WebApplication app)
  {
    // no navegador fica /person
    app.MapGet("person", () => new PersonModel(name:"Filipi"));
  }
}