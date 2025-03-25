namespace Person.Models;

public class PersonModel
{
  public PersonModel(string name)
  {
    Name = name;
    Id = Guid.NewGuid();
  }


  /* guid tem id com sequência longa de caracteres
    mas exige um pouco mais de processamento justamente
    pelo seu tamanho, portanto, para banco de dados grandes
    isso pode gerar um problema de desempenho.

    init: só funciona quando tiver construtor, e só posso 
    alterar ele uma vez.
  */
  public Guid Id { get; init; }

  /* Se nullable estivar ativado: todas as variáveis, propriedades, etc,
  que são passíveis de nulo, e eu não especificar isso, ele vai da wwarning.
  
  sol:
    - public string Name { get; set; } = String.Empty; // declaro vazia
    - public string? Name { get; set; } // uso marcador de nulo "?" dizendo que é nulo

  private set:   
    a única parte da aplicação responsável por cuidar das propriedades que estão no modelo,
    é o próprio modelo.
  */
  public string Name { get; private set; }

  public void ChangeName(string name)
  {
    Name = name;
  }

  public void SetInactive()
  {
    Name = "Desativado";
  }
}