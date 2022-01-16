using System;
using System.Collections.Generic;

namespace Lab4
{
    class Program
    {
        static void Main(string[] args)
        {
        
            var votesResult = new List<int[]>();
            string[] feed = { "RoyalConin", "Grandorf", "Pedigree", "GO!", "Monge" };
            string[] voters = { "Ivan", "Stepan", "Maria", "Egor", "Liza", "Danil" };
            Console.WriteLine("Кол-во рассматриваемых кормов (не более 5):");
            int altCount = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine();
            Console.WriteLine("Кол-во голосующих:");
            int votersCount = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine();
            Console.WriteLine("Приоритеты голосующих");
            Console.WriteLine();
            for (int i = 0; i < altCount; i++)
            {
                Console.WriteLine($"Приоритеты для корма '{feed[i]}'");

                int[] result = new int[votersCount];

                for (int j = 0; j < votersCount; j++)
                {
                    if (j < voters.Length)
                        Console.WriteLine($"Введите приоритет голосующего '{voters[j]}' (от 1 до {votersCount})");
                    else
                        Console.WriteLine($"Введите приоритет голосующего {j - voters.Length + 1}' (от 1 до {votersCount})"); 

                    result[j] = Convert.ToInt32(Console.ReadLine());

                }
                votesResult.Add(result);
                Console.WriteLine();
            }
            Console.WriteLine("Относительно большинства");
            RelMajority relMajority = new RelMajority(votesResult);
            relMajority.CalculateResult();
            ModelKondorse modelKondorse = new ModelKondorse(votesResult);
            Console.WriteLine();
            Console.WriteLine("Явный победитель");
            modelKondorse.CalculateResultClearWinner();
            Console.WriteLine();
            Console.WriteLine("Правило Копленда");
            modelKondorse.CalculateResultKopland();
            Console.WriteLine();
            Console.WriteLine("Правило Симпсона");
            modelKondorse.CalculateResultSimpson();
            Console.WriteLine();
            Console.WriteLine("Модель Борда");
            ModelBorda modelBorda = new ModelBorda(votesResult);
            modelBorda.CalculateResult();
            Console.WriteLine();
            Console.ReadLine();
        }
    }
}
