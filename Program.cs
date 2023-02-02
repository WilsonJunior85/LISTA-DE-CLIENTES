


using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;


namespace cursocsharp
{
    
    class Program
    {
    [System.Serializable]
   //Criando  variáveis     
        struct Cliente{
 public string nome;
 public string email;
 public string cpf;
 }

//Criando uma lista nova de clientes
 static List<Cliente> clientes = new List<Cliente>();

 //Criando o Menu de Clintes
 enum Menu { Listagem = 1, Adicionar = 2, Remover = 3, Sair = 4}


         static void Main(string[] args) 
        {

  Carregar();
  bool escolheuSair = false;
  while(!escolheuSair){
  Console.WriteLine("Bem vindo(a) ao nosso Sistema de Clientes");
  Console.WriteLine("1-Listagem\n2-Adicionar\n3-Remover\n4-Sair");
  int intOperacao = int.Parse(Console.ReadLine());
  Menu opcao = (Menu)intOperacao;


  switch (opcao){
   case Menu.Listagem:
   Listagem();
    break;
   case Menu.Adicionar:
    Adicionar();
    break;
   case Menu.Remover:
   Remover();
    break;
   case Menu.Sair:
    escolheuSair = true;
    break;
  }

Console.Clear();

  }
        }

// Adicionar Clientes
static void Adicionar(){
   Cliente cliente = new Cliente();
   Console.WriteLine("Cadastro de cliente: ");
   Console.WriteLine("Nome dos cliente: ");
   cliente.nome = Console.ReadLine();
   Console.WriteLine("Email do cliente: ");
   cliente.email = Console.ReadLine();
   Console.WriteLine("CPF do cliente: ");
   cliente.cpf = Console.ReadLine();

   clientes.Add(cliente);
   Salvar(); // Salvando automaticamente os elemntos na lista.
   Console.WriteLine("Cadastro concluído com sucesso, aperte enter para sair.");
   Console.ReadLine();
  }

// Listagem de Clientes
static void Listagem(){

    if(clientes.Count > 0){
    Console.WriteLine("Lista de clientes: ");
int i = 0;
    foreach(Cliente cliente in clientes){

        Console.WriteLine($"ID: {i}");
        Console.WriteLine($"Nome: {cliente.nome}");
        Console.WriteLine($"Email: {cliente.email}");
        Console.WriteLine($"CPF: {cliente.cpf}");
        Console.WriteLine("-------------------------------------");
        i++;
        
    }

    }
else{

    Console.WriteLine("Nenhum cliente cadastrado.");
}

    Console.WriteLine("Aperte enter para sair.");
    Console.ReadLine();
}

// Criando uma função de remover
static void Remover(){
//Primeiro iremos chamar a função "Listagem" pra dentro da função "Remover"    
Listagem();
//Depois iremos escrever na tela, solicitando o ID que o cliente deseja remover.
Console.WriteLine("Digite o ID do cliente que você quer remover: ");
// Depois que digitar o ID, vai ser guardado nessa variável(não pode esquecer de converter para int.Parse) abaixo:
int id = int.Parse(Console.ReadLine());

// Iremos verificar a entrada do usuário
// Primeiro ela tenm que ser positivo (id > 0)
// Ela tem que ser menor o tamanho da lista ( Clientes.Count)
if(id >= 0 && id < clientes.Count){

// Se o id for válido, iremos remover um elemento de uma lista.
//Depois iremos chamar a função "Salvar"
clientes.RemoveAt(id);
Salvar();

}else{

// Se o id não for válido, colocar uma mensagem dizendo pra tentar novamente.
Console.WriteLine("ID digitado é inválido, tente novamente!");
Console.ReadLine();
}

}


//SALVAR OS CLIENTES EM UM ARQUIVO
static void Salvar(){
//Criando uma FileStream para poder manipular os arquivos, ele vai abrir o arquivo
// E se o arquivo não existir, ele vai criar um novo com o FileMode.OpenOrCreate.
FileStream stream = new FileStream("cliente.dat", FileMode.OpenOrCreate);
//Salvando os dados em formato binário no arquivo.
BinaryFormatter enconder =  new BinaryFormatter();
//Serializar passando a stream dentro do arquivo a lista de clientes.
enconder.Serialize(stream, clientes);

//Fechando a stream 
stream.Close();
}

//Crindo a função de leitura( de carregamento), que vai ser executada assim que o progama iniciar.
static void Carregar(){
FileStream stream = new FileStream("cliente.dat", FileMode.OpenOrCreate);
try
{
    
BinaryFormatter enconder =  new BinaryFormatter();
clientes = (List<Cliente>)enconder.Deserialize(stream);
//Se clientes for null(não tiver nada), eu vou criar uma nova lista de clientes.
if(clientes == null){
    clientes = new List<Cliente>();
}

}
catch(System.Exception)
{
    clientes = new List<Cliente>();
    
}
           stream.Close();

       }
     }

}

         