using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Diagnostics;

namespace MaidOS
{
    class Program
    {
        enum Opcao { quit, cache, clean_disk, repare_system }
        enum Opcao2 { quit, dirs_cache, Winupdate, dns}

        static void Main(string[] args)
        {
            string versao = "0.0.0";
            string[] diretorios = { @"C:\Windows\Temp",
            @"C:\Users\{self.usr}\AppData\Local\Temp",
            @"C:\Windows\Prefetch",
            @"C:\Users\{self.usr}\Recent",
            @"C:\Users\{self.usr}\AppData\Local\Google\Chrome\User Data\Default\Cache",
            @"C:\Windows\SoftwareDistribution\Download",
            @"C:\Windows\SoftwareDistribution\Download"};

            Console.SetWindowSize(80, 20);

            Console.WriteLine(".....................................MaidOS.....................................");
            Console.WriteLine($"                                                                         {versao}");
            Console.WriteLine("Autor: Godofcoffe\n\n");
            // Console.WriteLine("> Há uma nova versão disponivel <");
            Console.WriteLine("Oque eu posso fazer por você hoje (usuario)?");
            Console.WriteLine("Digite '?' para mostrar o menu de ajuda.\n");
            Console.WriteLine("[ 1 ] Limpar cache de Apps e arquivos temporários");
            Console.WriteLine("[ 2 ] Escanear e reparar arquivos do sistema operacional");
            Console.WriteLine("[ 3 ] Verificação de disco");
            Console.WriteLine("[ 0 ] Sair\n");
            Console.Write("Opção > ");
            string entrada = Console.ReadLine();
            Console.Clear();
            Opcao opcao_selecionada = 0;
            
            if (char.Parse(entrada) == '?')
            {
                Console.WriteLine("menu de ajuda...");
            }
            else
            {
                int index = int.Parse(entrada);
                opcao_selecionada = (Opcao)index;
            }
            
            switch (opcao_selecionada)
            {
                case Opcao.cache:
                    Console.WriteLine("[ 1 ] Somente diretórios cache");
                    Console.WriteLine("[ 2 ] Somente diretório cache de Windows Update");
                    Console.WriteLine("[ 3 ] Somente cache DNS");
                    Console.WriteLine("[ 4 ] Limpeza Completa");
                    Console.WriteLine("[ 0 ] Cancelar\n");
                    Console.Write("Opção > ");
                    int index2 = int.Parse(Console.ReadLine());
                    Opcao2 opcao_selecionada2 = (Opcao2)index2;

                    switch (opcao_selecionada2)
                    {
                        case Opcao2.quit:
                            Console.WriteLine("saindo...");
                            break;

                        case Opcao2.dirs_cache:
                            Console.WriteLine("cache");
                            break;

                        case Opcao2.Winupdate:
                            Console.WriteLine("cache update");
                            break;

                        case Opcao2.dns:
                            Console.WriteLine("dns");
                            break;

                    }
                    break;

                case Opcao.clean_disk:
                    Console.WriteLine("limpando disco...");
                    break;

                case Opcao.repare_system:
                    Console.WriteLine("reparando...");
                    break;

                case Opcao.quit:
                    Console.WriteLine("saindo...");
                    break;

                default:
                    Console.WriteLine("Essa opção não existe!");
                    break;
            }

        }

        int carregarTamanho(string[] dir)
        {
            string[] paths = { };
            string[] arqs = { };
            foreach(string d in dir)
            {
                paths = Directory.GetDirectories(d);
                arqs = Directory.GetFiles(d);
            }
            int tamanho_paths = paths.Length;
            int tamanho_arqs = arqs.Length;
            return tamanho_arqs + tamanho_paths;
        }

        void Maid(string[] dir)
        {
            foreach(string d in dir)
            {
                string[] paths = Directory.GetDirectories(d);
                string[] arqs = Directory.GetFiles(d);

                foreach(string path in paths)
                {
                    Console.WriteLine($"Apagando o diretório: {path}");
                    Directory.Delete(path, true);
                }
                foreach(string arq in arqs)
                {
                    Console.WriteLine($"Apagando o arquivo: {arq}");
                    Directory.Delete(arq);
                }
            }
        }

        void Limpar_dns()
        {
            string diretorioAtual = Directory.GetCurrentDirectory();
            try
            {
                Process.Start("");
            }
            catch
            {
                Console.WriteLine("Um erro ocorreu ao executar o comando!");
            }
        }
    }
}
