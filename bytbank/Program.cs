using System;
using System.Runtime.CompilerServices;
using System.Xml;

namespace MyApp // Note: actual namespace depends on the project name.
{
    internal class Program {

        static void ShowMenu() {
            Console.WriteLine("1 - Inserir novo usuário");
            Console.WriteLine("2 - Deletar um usuário");
            Console.WriteLine("3 - Listar todas as contas registradas");
            Console.WriteLine("4 - Detalhes de um usuário");
            Console.WriteLine("5 - Quantia armazenada no banco");
            Console.WriteLine("6 - Manipular a conta");
            Console.WriteLine("0 - Sair do programa");
            Console.WriteLine("Digite a operação desejada: ");

        }
        static void RegistrarNovosUsuarios(List<string> cpfs, List<string> titulares, List<string> senhas, List<double> saldos) {
            Console.Write("Digite o CPF: ");
            cpfs.Add(Console.ReadLine());
            Console.Write("Digite o Nome: ");
            titulares.Add(Console.ReadLine());
            Console.Write("Digite uma Senha: ");
            senhas.Add(Console.ReadLine());
            saldos.Add(0);
        }

        static void DeletarUsuario(List<string> cpfs, List<string> titulares, List<string> senhas, List<double> saldos) {
            Console.Write("Digite o CPF: ");
            string cpfParaDeletar = Console.ReadLine();
            int indexParaDeletar = cpfs.FindIndex(d => d == cpfParaDeletar);

            if(indexParaDeletar == -1) {
                Console.WriteLine("Não foi possível deletar esta Conta");
                Console.WriteLine("MOTIVO: Conta não encontrada.");
            }

            cpfs.Remove(cpfParaDeletar);
            titulares.RemoveAt(indexParaDeletar);
            senhas.RemoveAt(indexParaDeletar);
            saldos.RemoveAt(indexParaDeletar);

            Console.WriteLine("Conta deletada com Sucesso!!!");
        }

        static void ListarTodasAsContas(List<string> cpfs, List<string> titulares, List<double> saldos) {
            for(int i = 0; i < cpfs.Count; i++) {
                ApresentaConta(i, cpfs, titulares, saldos);
            }
        }

        static void ApresentarUsuario(List<string> cpfs, List<string> titulares, List<double> saldos) {
            Console.Write("Digite o CPF: ");
            string cpfParaApresentar = Console.ReadLine();
            int indexParaApresentar = cpfs.FindIndex(cpf => cpf == cpfParaApresentar);

            if(indexParaApresentar == -1) {
                Console.WriteLine("Não foi possível apresentar esta Conta");
                Console.WriteLine("MOTIVO: Conta não encontrada.");
            }

            ApresentaConta(indexParaApresentar, cpfs, titulares, saldos);
        }

        static void ApresentarValorAcumulado(List<double> saldos) {
            Console.WriteLine($"Total acumulado no banco: R${saldos.Sum().ToString("F2")}");
            // return saldos.Sum();  //Aggregate(0.0, (x, y) => x + y);
        }

        static void ApresentaConta(int index, List<string> cpfs, List<string> titulares, List<double> saldos) {
            Console.WriteLine($"CPF = {cpfs[index]} | Titular = {titulares[index]} | Saldo = R${saldos[index]:F2}");
        }

        // Transações / login e logout
        static void TransacaoDoUsuario(List<string> cpfs, List<string> senhas, List<double> saldos, List<string> titulares) {
            Console.WriteLine();
            Console.WriteLine("***LOGIN***");

            Console.Write("Digite o seu CPF: ");
            string cpfDoUsuario = Console.ReadLine();
            int indexCpfUsuario = cpfs.FindIndex(cpf => cpf == cpfDoUsuario);

            Console.Write("Digite a sua Senha: ");
            string senhaDoUsuario = Console.ReadLine();

            if(indexCpfUsuario == -1 || senhaDoUsuario != senhas[indexCpfUsuario]) {
                Console.WriteLine("CPF ou SENHA incorreta, Tente novamente!!!");
            } else {
                Console.WriteLine($"Bem vindo ao BytBank {titulares[indexCpfUsuario]}");
                Console.WriteLine();
                Console.WriteLine("Qual operação gostaria de Realizar?");

                static void MenuTransacoe() {
                    Console.WriteLine("1 - SAQUE");
                    Console.WriteLine("2 - DEPÓSITO");
                    Console.WriteLine("3 - TRANSFERÊNCIA");
                    Console.WriteLine("0 - SAIR DA CONTA");
                }

                int opcao;
                do {
                    MenuTransacoe();
                    opcao = int.Parse(Console.ReadLine());
                    Console.WriteLine("---------------------");
                    switch(opcao) {
                        case 0:
                            Console.WriteLine("Saindo da conta...");
                            break;
                        case 1:
                            FazerSaque(cpfs, titulares, saldos, indexCpfUsuario);
                            break;
                        case 2:
                            FazerDeposito(cpfs, titulares, saldos, indexCpfUsuario);
                            break;
                        case 3:
                            FazerTransferencia(cpfs, titulares, saldos, indexCpfUsuario);
                            break;

                    }
                    static void FazerDeposito(List<string> cpfs, List<string> titulares, List<double> saldos, int indexCpfUsuario) {
                        Console.WriteLine("-------------------------------------------------");
                        Console.Write("Digite a quantidade a ser depositado: ");
                        double qtdDeposito = double.Parse(Console.ReadLine());
                        saldos[indexCpfUsuario] += qtdDeposito;
                        Console.WriteLine("Deposito Realizado com sucesso!!!");
                        Console.WriteLine();
                        Console.WriteLine("-------------------------------------------------");

                    }

                    static void FazerSaque(List<string> cpfs, List<string> titulares, List<double> saldos, int indexCpfUsuario) {
                        Console.WriteLine("-------------------------------------------------");
                        Console.Write("Digite quantos que você quer sacar: ");
                        double qtdSaque = double.Parse(Console.ReadLine());
                        saldos[indexCpfUsuario] -= qtdSaque;
                        Console.WriteLine($"Você fez um saque de R$ {qtdSaque} Reais");
                        Console.WriteLine();
                        Console.WriteLine("--------------------------------------------------");
                    }

                    static void FazerTransferencia(List<string> cpfs, List<string> titulares, List<double> saldos, int indexCpfUsuario) {
                        Console.WriteLine("--------------------------------------------------");
                        Console.WriteLine("Qual o valor da transferência?");
                        double qtdTransferencia = double.Parse(Console.ReadLine());
                        saldos[indexCpfUsuario] -= qtdTransferencia;

                        Console.Write("Digite qual CPF que vai a transferência: ");
                        string cpfTransferencia = Console.ReadLine();
                        indexCpfUsuario = cpfs.FindIndex(cpf => cpf == cpfTransferencia);
                        saldos[indexCpfUsuario] += qtdTransferencia;
                        Console.WriteLine($"A transferência no valor de R${qtdTransferencia.ToString("F2")} foi Realizada com Sucesso!!!");
                        Console.WriteLine("--------------------------------------------------");

                    }

                } while(opcao != 0);

            }


        }


        static void Main(string[] args) {

            List<string> cpfs = new List<string>();
            List<string> titulares = new List<string>();
            List<string> senhas = new List<string>();
            List<double> saldos = new List<double>();

            int option;
            do {
                ShowMenu();

                option = int.Parse(Console.ReadLine());

                Console.WriteLine("----------------------------------------");

                switch(option) {
                    case 0:
                        Console.WriteLine("O programa será encerrado...");
                        break;
                    case 1:
                        RegistrarNovosUsuarios(cpfs, titulares, senhas, saldos);
                        break;
                    case 2:
                        DeletarUsuario(cpfs, titulares, senhas, saldos);
                        break;

                    case 3:
                        ListarTodasAsContas(cpfs, titulares, saldos);
                        break;
                    case 4:
                        ApresentarUsuario(cpfs, titulares, saldos);
                        break;
                    case 5:
                        ApresentarValorAcumulado(saldos);
                        break;
                    case 6:
                        TransacaoDoUsuario(cpfs, senhas, saldos, titulares);
                        break;

                }
                Console.WriteLine("----------------------------------------");
            } while(option != 0);
        }


    }
}