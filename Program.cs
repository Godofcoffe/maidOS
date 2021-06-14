using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace MaidOS
{
    class Program
    {
        enum Opcao { quit, cache, clean_disk, repare_system, help };

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

            Console.WriteLine("...............................................MaidOS...............................................");
            Console.WriteLine($"                                                                                              {versao}");
            Console.WriteLine("Autor: Godofcoffe\n\n\n");
            // Console.WriteLine("> Há uma nova versão disponivel <");
            Console.Write("Oque eu posso fazer por você hoje (usuario)?\n\n");
            Console.WriteLine("[ 1 ] Limpar cache de Apps e arquivos temporários");
            Console.WriteLine("[ 2 ] Escanear e reparar arquivos do sistema operacional");
            Console.WriteLine("[ 3 ] Verificação de disco");
            Console.WriteLine("[ 4 ] Ajuda");
            Console.WriteLine("[ 0 ] Sair");
            Console.Write("Opção > ");
            int index = int.Parse(Console.ReadLine());
            Opcao opcao = (Opcao)index;
            switch (opcao)
            {
                case Opcao.cache:
                    break;
                case Opcao.clean_disk:
                    break;
                case Opcao.repare_system:
                    break;
                case Opcao.quit:
                    break;
                case Opcao.help:
                    break;
                default:
                    Console.WriteLine("Essa opção não existe!");
                    break;
            }

        }
        static void Maid(string[] dir)
        {
            foreach(string d in dir)
            {
                string[] paths = Directory.GetDirectories(d);
                string[] arqs = Directory.GetFiles(d);

                foreach(string path in paths)
                {
                    Console.WriteLine($"Apagando o diretoio: {path}");
                    Directory.Delete(path, true);
                }
                foreach(string arq in arqs)
                {
                    Console.WriteLine($"Apagando o arquivo: {arq}");
                    Directory.Delete(arq);
                }
            }
        }
    }
}
