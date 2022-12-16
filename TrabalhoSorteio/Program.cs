using System;
using TrabalhoSorteio;
using System.Globalization;
using System.Collections;

namespace TrabalhoSorteio
{
    class Program
    {
        static ArrayList moradoresA = new ArrayList();
        static ArrayList moradoresB = new ArrayList();
        static Queue esperaA = new Queue();
        static Queue esperaB = new Queue();

        static int numMoradoresA = 0, numMoradoresB = 0, maxMoradoresA, maxMoradoresB, maxEspera, filaEsperaA = 0, filaEsperaB = 0;
        static double umSalario = 1213, tresSalarios = 3639;
        static bool sucessoExcluir = false;
        static void Main(string[] args)
        {
            Console.WriteLine("--Bem Vindo--\n");
            MaxFaixa1(); MaxFaixa2(); MaxFila();
            for (int i = 0; i >= 0; i++)
            {
                Console.WriteLine("\n1 - Cadastrar morador" +
            "\n2 - Imprimir lista de moradores cadastrados" +
            "\n      2.1 - Listagem simples (apenas CPF e nome do morador)" +
            "\n      2.2 - Listagem completa(todos os dados)" +
            "\n3 - Imprimir fila de espera" +
            "\n4 - Pesquisar morador" +
            "\n5 - Desistencia/Exclusão" +
            "\n6 - Sorteio" +
            "\n7 - Editar Parâmetros" +
            "\n8 - Media Salarial" +
            "\n9 - Sair");
                int opcao = int.Parse(Console.ReadLine());
                switch (opcao)
                {
                    case 1: CadastroMorador(); break;
                    case 2: ImprimirCadastrados(); break;
                    case 3: ImprimirEspera(); break;
                    case 4: PesquisarMorador(); break;
                    case 5: ExcluirMorador(); break;
                    case 6: Sorteio(); break;
                    case 7: Parametros(); break;
                    case 8: Media(); break;
                    case 9: Console.WriteLine("Fechando programa..."); i = -100; break;
                    default: Console.WriteLine("Código Incorreto! Digite novamente"); break;
                }
            }
        }
        static void Media()
        {
            Console.WriteLine("\nMÉDIA SALARIAL");
            Console.WriteLine("1 - Média da FAIXA 1\n2 - Média da FAIXA 2\n3 - Média de ambas");
            int opcao = int.Parse(Console.ReadLine());
            if (opcao == 1)
                CalcularMedia(moradoresA, 1);
            else if (opcao == 2)
                CalcularMedia(moradoresB, 2);
            else if (opcao == 3)
                CalcularMediaTotal();
        }
        static void CalcularMediaTotal()
        {
            double media = moradoresA.Count + moradoresB.Count;
            double total = 0;
            foreach (Object m in moradoresA)
                total += ((Morador)m).Renda;
            foreach (Object m in moradoresB)
                total += ((Morador)m).Renda;
            media = total / media;
            Console.WriteLine("\nA média salarial de todos os moradores é de R$" + media.ToString("F2", CultureInfo.InvariantCulture));
        }
        static void CalcularMedia(ArrayList lista, int i)
        {
            double media = lista.Count;
            double total = 0;
            foreach (Object m in lista)
            {
                total += ((Morador)m).Renda;
            }
            media = total / media;
            Console.WriteLine("\nA média salarial dos moradores da FAIXA " + i + " é de R$" + media.ToString("F2", CultureInfo.InvariantCulture));
        }

        static void Parametros()
        {
            Console.WriteLine("\nEditar Parametros:");
            Console.WriteLine("1 - Editar maximo de moradores na FAIXA 1 " +
                "\n2 - Editar maximo de moradores na FAIXA 2" +
                "\n3 - Editar maximo de moradores na fila de espera" +
                "\n4 - Editar salario minimo" +
                "\n5 - Menu principal");
            int opcao = int.Parse(Console.ReadLine());
            switch (opcao)
            {
                case 1:
                    MaxFaixa1();
                    break;
                case 2:
                    MaxFaixa2();
                    break;
                case 3:
                    MaxFila();
                    break;
                case 4:
                    SalarioMinimo();
                    break;
                default:
                    break;
            }

        }
        static void SalarioMinimo()
        {
            Console.Write("Qual o novo valor do salário mínimo? ");
            umSalario = double.Parse(Console.ReadLine());
            tresSalarios = umSalario * 3;
        }
        static void MaxFila()
        {
            Console.Write("Qual será o máximo de moradores em cada fila de espera? ");
            maxEspera = int.Parse(Console.ReadLine());
        }
        static void MaxFaixa1()
        {
            Console.Write("Qual será o máximo de moradores na faixa 1? ");
            maxMoradoresA = int.Parse(Console.ReadLine());
        }
        static void MaxFaixa2()
        {
            Console.Write("Qual será o máximo de moradores na faixa 2? ");
            maxMoradoresB = int.Parse(Console.ReadLine());
        }

        static void Sorteio()
        {
            Console.Write("\nQuantos moradores serão sorteados na faixa 1?");
            int numSorteioA = int.Parse(Console.ReadLine());
            Console.Write("\nQuantos moradores serão sorteados na faixa 2?");
            int numSorteioB = int.Parse(Console.ReadLine());
            Console.WriteLine("------SORTEIO FAIXA 1------");
            Sortear(numSorteioA, moradoresA);
            Console.WriteLine("------SORTEIO FAIXA 2------");
            Sortear(numSorteioB, moradoresB);
        }
        static void Sortear(int numSorteio, ArrayList moradores)
        {
            Random r = new Random();
            for (int i = 1; i <= numSorteio; i++)
            {

                Console.WriteLine("\n------" + i + "° SORTEADO------");
                int num = r.Next(moradores.Count);
                ImprimirListaConfirmacao(num, moradores);
            }
        }

        static void ExcluirMorador()
        {
            bool encontrado = false;
            Console.WriteLine();
            Console.Write("Digite o CPF do morador: ");
            long tempCPF = long.Parse(Console.ReadLine());
            if (encontrado == false)
            {
                for (int i = 0; i < moradoresA.Count; i++)
                {
                    if (((Morador)moradoresA[i]).Cpf == tempCPF)
                    {
                        ImprimirListaConfirmacao(i, moradoresA);
                        encontrado = true;
                        Excluir(i, moradoresA, esperaA);
                        if (sucessoExcluir == true)
                            filaEsperaA--;
                    }
                }
            }
            if (encontrado == false)
            {
                for (int i = 0; i < moradoresB.Count; i++)
                {
                    if (((Morador)moradoresB[i]).Cpf == tempCPF)
                    {
                        ImprimirListaConfirmacao(i, moradoresB);
                        encontrado = true;
                        Excluir(i, moradoresB, esperaB);
                        if (sucessoExcluir == true)
                            filaEsperaB--;
                    }
                }
            }
        }
        static void Excluir(int i, ArrayList lista, Queue espera)
        {
            Console.WriteLine("Você realmente deseja desistir/excluir?\n1 - SIM | 2 - NÃO");
            int opcao = int.Parse(Console.ReadLine());
            if (opcao == 1)
            {
                lista.RemoveAt(i);
                Console.WriteLine("Deletado com sucesso.");
                sucessoExcluir = true;
                if (espera.Count > 0){ 
                Dequeue(lista, espera);}
                Console.WriteLine();
            }
            else
                Console.WriteLine();
        }
        static void ImprimirListaConfirmacao(int i, ArrayList lista)
        {
            Console.WriteLine("CPF " + ((Morador)lista[i]).Cpf + " - Nome " + ((Morador)lista[i]).Nome);
            Console.WriteLine("Qtde. Dependentes " + ((Morador)lista[i]).Dependentes + " - Renda Familiar " + ((Morador)lista[i]).Renda.ToString("F2", CultureInfo.InvariantCulture));
            Console.WriteLine("Telefone: " + ((Morador)lista[i]).Telefone);
            Console.WriteLine(((Morador)lista[i]).Endereco);
        }
        static void Dequeue(ArrayList lista, Queue espera)
        {
            lista.Add(espera.Dequeue());
        }

        static void PesquisarMorador()
        {

            Console.WriteLine();
            Console.Write("Digite o CPF do morador: ");
            long tempCPF = long.Parse(Console.ReadLine());
            ImprimirPesquisa(tempCPF);
        }
        static void ImprimirPesquisa(long tempCPF)
        {
            bool encontrado = false;
            Console.WriteLine("Pesquisando na faixa 1...");
            foreach (Object m in moradoresA)
            {
                if (((Morador)m).Cpf == tempCPF)
                {
                    Console.WriteLine("CPF " + ((Morador)m).Cpf + " - Nome " + ((Morador)m).Nome);
                    Console.WriteLine("Qtde. Dependentes " + ((Morador)m).Dependentes + " - Renda Familiar " + ((Morador)m).Renda.ToString("F2", CultureInfo.InvariantCulture));
                    Console.WriteLine("Telefone: " + ((Morador)m).Telefone);
                    Console.WriteLine(((Morador)m).Endereco);
                    encontrado = true;
                }
            }
            if (encontrado == false)
            {
                Console.WriteLine("\nPesquisando na faixa 2...");
                foreach (Object m in moradoresB)
                {
                    if (((Morador)m).Cpf == tempCPF)
                    {
                        Console.WriteLine("CPF " + ((Morador)m).Cpf + " - Nome " + ((Morador)m).Nome);
                        Console.WriteLine("Qtde. Dependentes " + ((Morador)m).Dependentes + " - Renda Familiar " + ((Morador)m).Renda.ToString("F2", CultureInfo.InvariantCulture));
                        Console.WriteLine("Telefone: " + ((Morador)m).Telefone);
                        Console.WriteLine(((Morador)m).Endereco);
                        encontrado = true;
                    }
                }
            }
            if (encontrado == false)
            {
                Console.WriteLine("\nPesquisando na fila de espera da faixa 1...");
                foreach (Object m in esperaA)
                {
                    if (((Morador)m).Cpf == tempCPF)
                    {
                        Console.WriteLine("CPF " + ((Morador)m).Cpf + " - Nome " + ((Morador)m).Nome);
                        Console.WriteLine("Qtde. Dependentes " + ((Morador)m).Dependentes + " - Renda Familiar " + ((Morador)m).Renda.ToString("F2", CultureInfo.InvariantCulture));
                        Console.WriteLine("Telefone: " + ((Morador)m).Telefone);
                        Console.WriteLine(((Morador)m).Endereco);
                        encontrado = true;
                    }
                }
            }
            if (encontrado == false)
            {
                Console.WriteLine("\nPesquisando na fila de espera da faixa 2...");
                foreach (Object m in esperaB)
                {
                    if (((Morador)m).Cpf == tempCPF)
                    {
                        Console.WriteLine("CPF " + ((Morador)m).Cpf + " - Nome " + ((Morador)m).Nome);
                        Console.WriteLine("Qtde. Dependentes " + ((Morador)m).Dependentes + " - Renda Familiar " + ((Morador)m).Renda.ToString("F2", CultureInfo.InvariantCulture));
                        Console.WriteLine("Telefone: " + ((Morador)m).Telefone);
                        Console.WriteLine(((Morador)m).Endereco);
                        encontrado = true;
                    }
                }
            }
            if (encontrado == false)
                Console.WriteLine("\nNão foi encontrado nenhum morador com esse CPF\n");
        }

        static void ImprimirEspera()
        {
            Console.WriteLine();
            Console.WriteLine("FILA DE ESPERA (PÁGINA 1)");
            Console.WriteLine("================================");
            Console.WriteLine("FAIXA 1");
            foreach (Object m in esperaA)
            {
                Console.WriteLine("CPF " + ((Morador)m).Cpf + " - Nome " + ((Morador)m).Nome + " - Renda Familiar " + ((Morador)m).Renda.ToString("F2", CultureInfo.InvariantCulture));
            }
            Console.WriteLine();
            Console.WriteLine("FILA DE ESPERA (PÁGINA 2)");
            Console.WriteLine("================================");
            Console.WriteLine("FAIXA 2");
            foreach (Object m in esperaB)
            {
                Console.WriteLine("CPF " + ((Morador)m).Cpf + " - Nome " + ((Morador)m).Nome + " - Renda Familiar " + ((Morador)m).Renda.ToString("F2", CultureInfo.InvariantCulture));
            }
            Console.WriteLine("================================");
            Console.WriteLine();

        }

        static void ImprimirCadastrados()
        {
            Console.WriteLine();
            Console.WriteLine("Imprimir listagem completa ou apenas CPF e nome?");
            Console.WriteLine("1 - Completo\n2 - Apenas CPF e nome");
            int opcao = int.Parse(Console.ReadLine());
            switch (opcao)
            {
                case 1:
                    PrintCompleto();
                    break;
                case 2:
                    PrintIncompleto();
                    break;
            }

        }
        static void PrintIncompleto()
        {
            Console.WriteLine();
            Console.WriteLine("LISTAGEM DE MORADORES (PÁGINA 1)");
            Console.WriteLine("================================");
            Console.WriteLine("FAIXA 1");
            foreach (Object m in moradoresA)
            {
                Console.WriteLine("CPF " + ((Morador)m).Cpf + " - Nome " + ((Morador)m).Nome + " - Renda Familiar " + ((Morador)m).Renda.ToString("F2", CultureInfo.InvariantCulture));
            }
            Console.WriteLine();
            Console.WriteLine("LISTAGEM DE MORADORES (PÁGINA 2)");
            Console.WriteLine("================================");
            Console.WriteLine("FAIXA 2");
            foreach (Object m in moradoresB)
            {
                Console.WriteLine("CPF " + ((Morador)m).Cpf + " - Nome " + ((Morador)m).Nome + " - Renda Familiar " + ((Morador)m).Renda.ToString("F2", CultureInfo.InvariantCulture));
            }
            Console.WriteLine();
            Console.WriteLine("================================");
        }
        static void PrintCompleto()
        {
            Console.WriteLine();
            Console.WriteLine("LISTAGEM DE MORADORES (PÁGINA 1)");
            Console.WriteLine("================================");
            Console.WriteLine("FAIXA 1");
            CompletoMoradores(moradoresA);
            Console.WriteLine();
            Console.WriteLine("LISTAGEM DE MORADORES (PÁGINA 2)");
            Console.WriteLine("================================");
            Console.WriteLine("FAIXA 2");
            CompletoMoradores(moradoresB);
        }
        static void CompletoMoradores(ArrayList listaMoradores)
        {
            for (int i = 0; i < listaMoradores.Count; i++)
            {
                int zerar = 0;
                if (i < listaMoradores.Count)
                {
                    Console.WriteLine("\nCPF " + ((Morador)listaMoradores[i]).Cpf + " - Nome " + ((Morador)listaMoradores[i]).Nome);
                    Console.WriteLine("Qtde. Dependentes " + ((Morador)listaMoradores[i]).Dependentes + " - Renda Familiar " + ((Morador)listaMoradores[i]).Renda.ToString("F2", CultureInfo.InvariantCulture));
                    Console.WriteLine("Telefone: " + ((Morador)listaMoradores[i]).Telefone);
                    Console.WriteLine(((Morador)listaMoradores[i]).Endereco);
                    i++; zerar++;
                }
                if (i < listaMoradores.Count)
                {
                    Console.WriteLine("\nCPF " + ((Morador)listaMoradores[i]).Cpf + " - Nome " + ((Morador)listaMoradores[i]).Nome);
                    Console.WriteLine("Qtde. Dependentes " + ((Morador)listaMoradores[i]).Dependentes + " - Renda Familiar " + ((Morador)listaMoradores[i]).Renda.ToString("F2", CultureInfo.InvariantCulture));
                    Console.WriteLine("Telefone: " + ((Morador)listaMoradores[i]).Telefone);
                    Console.WriteLine(((Morador)listaMoradores[i]).Endereco);
                    i++; zerar++;
                }
                if (i < listaMoradores.Count)
                {
                    Console.WriteLine("\nCPF " + ((Morador)listaMoradores[i]).Cpf + " - Nome " + ((Morador)listaMoradores[i]).Nome);
                    Console.WriteLine("Qtde. Dependentes " + ((Morador)listaMoradores[i]).Dependentes + " - Renda Familiar " + ((Morador)listaMoradores[i]).Renda.ToString("F2", CultureInfo.InvariantCulture));
                    Console.WriteLine("Telefone: " + ((Morador)listaMoradores[i]).Telefone);
                    Console.WriteLine(((Morador)listaMoradores[i]).Endereco);
                    zerar++;
                }
                if (zerar == 3)
                {
                    Console.WriteLine("\nImprimir mais três? 1 - SIM | 2 - NÃO");
                    int opcao = int.Parse(Console.ReadLine());
                    if (opcao == 1)
                    {
                        Console.WriteLine("\nImprimindo os proximos...");
                    }
                    else
                    {
                        i = listaMoradores.Count;
                    }
                }
            }
        }
        /*static void PrintCompleto()
        {
            Console.WriteLine();
            Console.WriteLine("LISTAGEM DE MORADORES (PÁGINA 1)");
            Console.WriteLine("================================");
            Console.WriteLine("FAIXA 1");
            CompletoMoradoresA();
            Console.WriteLine();
        }
        static void CompletoMoradoresA()
        {
            int count = 0;
            foreach (Object m in moradoresA)
            {

                Console.WriteLine("CPF " + ((Morador)m).Cpf + " - Nome " + ((Morador)m).Nome);
                Console.WriteLine("Qtde. Dependentes " + ((Morador)m).Dependentes + " - Renda Familiar " + ((Morador)m).Renda.ToString("F2", CultureInfo.InvariantCulture));
                Console.WriteLine("Telefone: " + ((Morador)m).Telefone);
                Console.WriteLine(((Morador)m).Endereco);
                count++;
                Console.WriteLine();
                if (count <= moradoresA.Count - 1)
                {
                    Console.WriteLine("Imprimir proximo morador da faixa 1?");
                    Console.WriteLine("1 - SIM | 2 - NÃO");
                    int opcao = int.Parse(Console.ReadLine());
                    if (opcao == 2)
                    {
                        CompletoMoradoresB();
                    }
                }
                Console.WriteLine();
            }
            CompletoMoradoresB();
        }
        static void CompletoMoradoresB()
        {
            int count = 0;
            Console.WriteLine("\nLISTAGEM DE MORADORES (PÁGINA 2)");
            Console.WriteLine("================================");
            Console.WriteLine("FAIXA 2");
            foreach (Object m in moradoresB)
            {
                Console.WriteLine("CPF " + ((Morador)m).Cpf + " - Nome " + ((Morador)m).Nome);
                Console.WriteLine("Qtde. Dependentes " + ((Morador)m).Dependentes + " - Renda Familiar " + ((Morador)m).Renda.ToString("F2", CultureInfo.InvariantCulture));
                Console.WriteLine("Telefone: " + ((Morador)m).Telefone);
                Console.WriteLine(((Morador)m).Endereco);
                count++;
                Console.WriteLine();
                Console.WriteLine("Imprimir proximo morador da faixa 2?");
                Console.WriteLine("1 - SIM | 2 - NÃO");
                int opcao = int.Parse(Console.ReadLine());
                if (opcao == 2)
                {
                    return;
                }
            }
            Console.WriteLine();
            Console.WriteLine("NÃO HÁ MAIS MORADORES");
            Console.WriteLine("================================");
        }*/

        static void CadastroMorador()
        {
            Console.WriteLine();
            Morador a = new Morador();
            Console.Write("CADASTRO\nCPF: "); a.Cpf = long.Parse(Console.ReadLine());
            Console.Write("Nome completo: "); a.Nome = Console.ReadLine();
            Console.Write("Quantidade de dependentes: "); a.Dependentes = int.Parse(Console.ReadLine());
            Console.Write("Telefone: "); a.Telefone = int.Parse(Console.ReadLine());
            Console.Write("Endereço completo: "); a.Endereco = Console.ReadLine();
            Console.Write("Renda familiar: "); a.Renda = double.Parse(Console.ReadLine(), CultureInfo.InvariantCulture);
            if (a.Renda <= umSalario)
            {
                if (numMoradoresA >= maxMoradoresA)
                {
                    if (filaEsperaA < maxEspera)
                    {
                        Console.WriteLine("Fila cheia, cadastrado na fila de espera.");
                        esperaA.Enqueue(a);
                        filaEsperaA++;
                    }
                    else
                        Console.WriteLine("Fila de espera cheia, cadastro não efetuado.");
                }
                else
                {
                    numMoradoresA++;
                    moradoresA.Add(a);
                    Console.WriteLine("Adicionado na FAIXA 1");
                }
            }
            else if (a.Renda <= tresSalarios)
            {

                if (numMoradoresB >= maxMoradoresB)
                {
                    if (filaEsperaB < maxEspera)
                    {
                        Console.WriteLine("Fila cheia, cadastrado na fila de espera.");
                        esperaA.Enqueue(a);
                        filaEsperaB++;
                    }
                    else
                        Console.WriteLine("Fila de espera cheia, cadastro não efetuado.");
                }
                else
                {
                    numMoradoresB++;
                    moradoresB.Add(a);
                    Console.WriteLine("Adicionado na FAIXA 2");
                }
            }
            else
                Console.WriteLine("Não cadastrado, renda maior que 3 salários mínimos.");
            Console.WriteLine();
        }
    }
}
