using System;
using System.Collections.Generic;

class Book{
    //Tipos abstratos de dados
    public string Name{get; set;}
    /*O tipo decimal tem mais precisão e um intervalo pequeno, o que torna apropriado para cálculos financeiros e monetários. Porém, nesse sistema não será realizado cálculos matemáticos com o valor, então utilizarei o float, pois ocupa menos espaço na memória e supri a necessidade para esse sistema*/
    public float Price{get; set;}
    public string Author{get; set;}
    public string Gender{get; set;}
    public string PublishingCompany{get; set;}
    public DateTime DateRegister{get; set;}
    public int QuantityInStock{get; set;} = 0;
    

    //Construtor para criação de novos produtos
    public Book(string name, float price, string author, string gender, string publishingCompany, DateTime dateRegister){
        Name = name;
        Price = price;
        Author = author;
        Gender = gender;
        PublishingCompany = publishingCompany;
        DateRegister = dateRegister;
    }
}

class Program{
    /*Variável estática da classe "Program", é compartilhada entre todas as intâncias da classe em que foi declarada. Esta variável é uma coleção genérica uqe pode armazenar os livros em estoque. Como ela está declarada na classe "Program", as instâncias dessa classe compartilharão a mesma lista de produtos em estoque. Essa abordagem armazena dados compartilhados entre diferentes partes do programa*/
    static List<Book> stock = new List<Book>();

    //Função Main com estrutura de decisão e chamada de funções
    static void Main(){
        Console.WriteLine("CONTROLE DE ESTOQUE - LIVRARIA\n");
        int choice;
        do{
            //Chamada de função
            ShowMenu();

            //A variável recebe o retorno da função, que mostra ao usuário uma mensagem e retorna o dado de entrada convertido para o tipo primitivo int
            choice = ReceiveOption();

            switch(choice){
                case 0:
                    Console.WriteLine("Programa encerrado!");
                    break;
                case 1:
                    AddBook();
                    break;
                case 2:
                    ListBooks();
                    break;
                case 3:
                    RemoveBook();
                    break;
                case 4:
                    AddStock();
                    break;
                case 5:
                    RemoveStock();
                    break;
            }
        } while(choice != 0);
    }

    //Função que exibe o menu
    static void ShowMenu(){
        Console.WriteLine("MENU DE OPÇÕES");
        Console.WriteLine("[1] Novo");
        Console.WriteLine("[2] Listar produtos");
        Console.WriteLine("[3] Remover produtos");
        Console.WriteLine("[4] Entrada de estoque");
        Console.WriteLine("[5] Saída de estoque");
        Console.WriteLine("[0] Sair");
    }

    //Função que recebe e retorna a escolha do usuário 
    static int ReceiveOption(){
        Console.Write("Escolha a opção: ");
        return Convert.ToInt32(Console.ReadLine());
    }

    //Função que adiciona o livro em estoque
    static void AddBook(){
        Console.WriteLine("ADICIONAR LIVRO");

        Console.Write("Informe o nome do Livro: ");
        string name = Console.ReadLine();

        Console.Write("Informe o preço: ");
        float price = float.Parse(Console.ReadLine());

        Console.Write("Informe o nome do autor(a): ");
        string author = Console.ReadLine();

        Console.Write("Informe o gênero: ");
        string gender = Console.ReadLine();

        Console.Write("Informe a editora: ");
        string publishingCompany = Console.ReadLine();

        /*Instância da classe "Book" é criada e seu construtor é usado para inicializar os atributos do novo produto. Sintaxe abaixo:
        NomeClasse nomeEstancia = new NomeConstrutor(argumentos)*/
        Book newBook = new Book(name, price, author, gender, publishingCompany, DateTime.Now);

        /*Adiciona o objeto "newBook" à lista de estoque, sendo esta uma coleção que armazena objetos da classe "Book", logo a lista contém uma referência do produto criado*/
        stock.Add(newBook);
        Console.WriteLine("Livro adicionado com sucesso!");
    }

    //Função que remove os livros de estoque
    static void RemoveBook(){
        Console.WriteLine("REMOVER LIVRO");

        //Chamada da função que lista os livros em estoque
        ListBooks();

        Console.Write("Digite o nome do livro para a ser removido: ");
        string nameBookRemove = Console.ReadLine();

        /*stock é a lista que contém os livros em estoque, onde a busca será realizada.
        .Find é o método da classe List<Book> que procura o primeiro elementoque satisfaça a condição especificada
        book.Nome.Equals(nameBookRemove, StringComparison.OrdinalIgnoreCase) é uma expressão lambda que define a condição para busca, ela compara o nome do livro da lista com o nome do livro que foi digitado pelo usuário sem levar em consideração a case da frase
        */
        Book bookRemove  = stock.Find(book => book.Name.Equals(nameBookRemove, StringComparison.OrdinalIgnoreCase));

        if(bookRemove != null){
            stock.Remove(bookRemove);
            Console.WriteLine("Livro removido com sucesso!");
        }
        else{
            Console.WriteLine("Livro não encontrado!");
        }
    }

    //Função que lista os livros em estoque
    static void ListBooks(){
        Console.WriteLine("LISTA DE LIVROS");
        foreach(var book in stock){
            Console.WriteLine($"{book.Name} - {book.PublishingCompany} - (${book.Price}) - {book.QuantityInStock}");
        }
    }

    //Função que adiciona quantidade de livros do estoque
    static void AddStock(){
        Console.WriteLine("ADICIONAR LIVRO EM ESTOQUE");

        //Chamada da função que lista os livros em estoque
        ListBooks();

        Console.Write("Digite o nome do livro para entrada de estoque: ");
        string entryStock = Console.ReadLine();
        Book bookEntryStock = stock.Find(book => book.Name.Equals(entryStock, StringComparison.OrdinalIgnoreCase));
        if(bookEntryStock != null){
            Console.Write("Quantidade de entrada: ");
            int entryQuantity = Convert.ToInt32(Console.ReadLine());
            bookEntryStock.QuantityInStock += entryQuantity;
            Console.WriteLine("Entrada realizada com sucesso!");
        }else{
            Console.WriteLine("Produto não encontrado.\n");
        }
    }

    //Função que remove quantidade de livros do estoque
    static void RemoveStock(){
        Console.WriteLine("REMOVER LIVRO DE ESTOQUE");
        ListBooks();

        Console.Write("Digite o nome do livro: ");
        string outputBookName = Console.ReadLine();

        Book outputBook = stock.Find(book => book.Name.Equals(outputBookName, StringComparison.OrdinalIgnoreCase));

        if(outputBook != null){
            Console.Write("Digite a quantidade de saída: ");
            int outputQuantity = Convert.ToInt32(Console.ReadLine());
            
            if(outputQuantity <= outputBook.QuantityInStock){
                outputBook.QuantityInStock  -= outputQuantity;
                Console.WriteLine("Quantidade removida!");
            }
            else{
                Console.WriteLine("Quantidade de saída maior que a quantidade em estoque. Operação não realizada.");
            }
        }
        else{
            Console.WriteLine("Produto não encontrado!");
        }
    }
}
