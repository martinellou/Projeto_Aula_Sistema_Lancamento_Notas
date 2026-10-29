using System;

class Aluno
{
    public string Nome { get; set; } = "";
    public int Idade { get; set; } = 0;
}

class Program
{
    const int QuantidadeMaximaAlunos = 3;
    const int QuantidadeDisciplinas = 3;

    static string[] disciplinas =
    {
        "Lógica de Programação",
        "Banco de Dados",
        "Programação em C#"
    };

    static Aluno[] alunos = new Aluno[QuantidadeMaximaAlunos];
    static double[,] notas = new double[QuantidadeMaximaAlunos, QuantidadeDisciplinas];
    static bool[] notasLancadas = new bool[QuantidadeMaximaAlunos];
    static int quantidadeAlunos = 0;

    static void Main()
    {
        InicializarAlunos();

        int opcao;

        do
        {
            Console.Clear();
            ExibirMenu();

            Console.Write("Digite a opção desejada: ");
            while (!int.TryParse(Console.ReadLine(), out opcao))
            {
                Console.Write("Opção inválida! Digite novamente: ");
            }
            Console.WriteLine();

            switch (opcao)
            {
                case 1:
                    CadastrarAluno();
                    break;
                case 2:
                    LancarNotas();
                    break;
                case 3:
                    ConsultarResultado();
                    break;
                case 0:
                    Console.WriteLine("Programa Encerrado...");
                    break;
                default:
                    Console.WriteLine("Opção inválida");
                    break;
            }

            if (opcao != 0)
            {
                Console.WriteLine("\nPressione qualquer tecla para continuar...");
                Console.ReadKey();
            }

        } while (opcao != 0);
    }

    static void InicializarAlunos()
    {
        for (int i = 0; i < alunos.Length; i++)
        {
            alunos[i] = new Aluno();
        }
    }

    static void ExibirMenu()
    {
        Console.WriteLine("=====================================");
        Console.WriteLine("   Sistema de Alunos e Notas em C#   ");
        Console.WriteLine("=====================================");
        Console.WriteLine("1 - Cadastrar aluno");
        Console.WriteLine("2 - Lançar notas");
        Console.WriteLine("3 - Consultar Resultado");
        Console.WriteLine("0 - Sair");
        Console.WriteLine("=====================================");
    }

    static void CadastrarAluno()
    {
        Console.WriteLine("=== Cadastro de Aluno ===");

        if (quantidadeAlunos >= QuantidadeMaximaAlunos)
        {
            Console.WriteLine("Limite de alunos atingido!");
            return;
        }

        Console.Write("Entre com o nome do aluno: ");
        string nome = Console.ReadLine() ?? "";

        Console.Write("Entre com a idade do aluno: ");
        int idade;
        while (!int.TryParse(Console.ReadLine(), out idade) || idade <= 0)
        {
            Console.Write("Idade inválida, digite novamente: ");
        }

        if (idade >= 18)
        {
            Console.WriteLine("O aluno é maior de idade");
        }
        else
        {
            Console.WriteLine("O aluno é menor de idade");
        }

        char resposta;
        do
        {
            Console.Write("\nConfirma cadastro? S/N: ");
            resposta = char.ToUpper(Console.ReadKey().KeyChar);
            Console.WriteLine();
        } while (resposta != 'S' && resposta != 'N');

        if (resposta == 'S')
        {
            int posicao = quantidadeAlunos;
            alunos[posicao].Nome = nome;
            alunos[posicao].Idade = idade;
            quantidadeAlunos++;

            Console.WriteLine("\nAluno cadastrado com sucesso");
            Console.WriteLine($"Código do aluno: {posicao + 1}");
        }
        else
        {
            Console.WriteLine("Cadastro cancelado...");
        }
    }

    static void LancarNotas()
    {
        Console.WriteLine("=== Lançamento de Notas ===");

        if (quantidadeAlunos == 0)
        {
            Console.WriteLine("Nenhum aluno cadastrado");
            return;
        }

        Console.WriteLine("Alunos Cadastrados até o momento:");
        for (int i = 0; i < quantidadeAlunos; i++)
        {
            Console.WriteLine($"{i + 1} - {alunos[i].Nome}");
        }

        int codigo;
        Console.Write("\nDigite o código do aluno que deseja lançar nota: ");
        while (!int.TryParse(Console.ReadLine(), out codigo) || codigo < 1 || codigo > quantidadeAlunos)
        {
            Console.Write("Código inválido! Digite novamente: ");
        }

        int posicao = codigo - 1;
        Console.WriteLine($"\nAluno: {alunos[posicao].Nome}\n");

        for (int i = 0; i < disciplinas.Length; i++)
        {
            double nota;
            Console.Write($"Nota da disciplina [{disciplinas[i]}]: ");

            while (!double.TryParse(Console.ReadLine(), out nota) || nota < 0 || nota > 10)
            {
                Console.Write("Nota inválida! Digite um valor entre 0 e 10: ");
            }

            notas[posicao, i] = nota;
        }

        notasLancadas[posicao] = true;
        Console.WriteLine("\nNotas lançadas com sucesso");
    }

    static void ConsultarResultado()
    {
        Console.WriteLine("=== Consulta de Resultados ===");

        if (quantidadeAlunos == 0)
        {
            Console.WriteLine("Nenhum aluno cadastrado.");
            return;
        }

        for (int i = 0; i < quantidadeAlunos; i++)
        {
            Console.WriteLine($"\nAluno: {alunos[i].Nome} (Idade: {alunos[i].Idade})");

            if (!notasLancadas[i])
            {
                Console.WriteLine("  Notas ainda não foram lançadas.");
                continue;
            }

            double soma = 0;
            for (int j = 0; j < disciplinas.Length; j++)
            {
                Console.WriteLine($"  {disciplinas[j]}: {notas[i, j]:F1}");
                soma += notas[i, j];
            }

            double media = soma / disciplinas.Length;
            string status = media >= 7.0 ? "Aprovado" : (media >= 5.0 ? "Recuperação" : "Reprovado");

            Console.WriteLine($"  Média: {media:F1} | Status: {status}");
        }
    }
}