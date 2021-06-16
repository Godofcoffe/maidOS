using System;
using System.IO;
using System.Diagnostics;
using System.Management;

namespace MaidOS
{
    class Program
    {
        enum Opcao { quit, cache, clean_disk, repare_system }
        enum Opcao2 { quit, dirs_cache, Winupdate, dns}

        static void Main(string[] args)
        {
            string diretorioAtual = Directory.GetCurrentDirectory();
            string versao = "0.0.0";
            string[] diretorios = { @"C:\Windows\Temp",
            @"C:\Users\{self.usr}\AppData\Local\Temp",
            @"C:\Windows\Prefetch",
            @"C:\Users\{self.usr}\Recent",
            @"C:\Users\{self.usr}\AppData\Local\Google\Chrome\User Data\Default\Cache",
            @"C:\Windows\SoftwareDistribution\Download",
            @"C:\Windows\SoftwareDistribution\Download"};

            Console.SetWindowSize(80, 20);

            inicio:
            Console.Clear();
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
                    SubInicio:
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
                            goto inicio;

                        case Opcao2.dirs_cache:
                            int tot = QuantArquivos(diretorios);
                            if (tot > 0)
                            {
                                Console.WriteLine($"Há um total de {tot} de arquivo e pastas.");
                                Console.Write("Deseja continuar? [s/n]: ");
                                char verificacao = Console.ReadLine().ToLower()[0];
                                if (verificacao == 's')
                                {
                                    Maid(diretorios);
                                }
                                else
                                {
                                    goto SubInicio;
                                }
                            }
                            else
                            {
                                Console.WriteLine("Não pude calcular quantos arquivos estão no cache.");
                            }
                            break;

                        case Opcao2.Winupdate:
                            Console.WriteLine("Ainda em desenvolvimento...");
                            break;

                        case Opcao2.dns:
                            LimparDns();
                            break;
                    }
                    break;

                case Opcao.clean_disk:
                    chkdsk();
                    break;

                case Opcao.repare_system:
                    Reparos();
                    break;

                case Opcao.quit:
                    Console.WriteLine("Saindo...");
                    break;

                default:
                    Console.WriteLine("Essa opção não existe!");
                    break;
            }

        }

        static int QuantArquivos(string[] dir)
        {
            string[] paths = { };
            string[] arqs = { };
            foreach (string d in dir)
            {
                try
                {
                    paths = Directory.GetDirectories(d);
                    arqs = Directory.GetFiles(d);
                }
                catch (UnauthorizedAccessException)
                {
                    Console.WriteLine("Não tenho permissão para acessar esse diretório/arquivo.");
                }
            }
            int tamanho_paths = paths.Length;
            int tamanho_arqs = arqs.Length;
            return tamanho_arqs + tamanho_paths;
        }

        static void Maid(string[] dir)
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

        static void LimparDns()
        {
            try
            {
                Process.Start(@"..\..\Dns.bat");
            }
            catch
            {
                Console.WriteLine("Um erro ocorreu ao executar o comando!");
            }
        }

        static string GetPlataforma()
        {
            string os = "";
            using (ManagementObjectSearcher searcher = new ManagementObjectSearcher("SELECT * FROM Win32_OperatingSystem"))
            {
                ManagementObjectCollection information = searcher.Get();
                if (information != null)
                {
                    foreach (ManagementObject obj in information)
                    {
                        os = obj["Caption"].ToString() + " - " + obj["OSArchitecture"].ToString();
                    }
                }
                os = os.Replace("NT 5.1.2600", "XP");
                os = os.Replace("NT 5.2.3790", "Server 2003");
                return os;
            }
        }

        static void Reparos()
        {
            string plat = GetPlataforma();
            if (plat.IndexOf("Windows 7") != -1)
            {
                Console.WriteLine("Executando sfc:");
                Process.Start(@"..\..\SFC.bat");
            }
            else if (plat.IndexOf("Windows 10") != -1 || plat.IndexOf("Windows 8") != -1 || plat.IndexOf("Windows 8.1") != -1)
            {
                Console.WriteLine("Executando DISM.exe");
                Process.Start(@"..\..\DISM.bat");
                Process.Start(@"..\..\SFC.bat");
            }
            else
            {
                Console.WriteLine("Não há suporte para esse sistema operacional!");
            }
        }

        static void chkdsk()
        {
            Process.Start(@"..\..\chkdsk.bat");
        }
    }
}
