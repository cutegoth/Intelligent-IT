using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lab4
{
    class ModelKondorse
    {
        string[] feed = { "RoyalConin", "Grandorf", "Pedigree", "GO!", "Monge" };

        public List<int[]> votesResults;

        public ModelKondorse(List<int[]> votesResults)
        {
            this.votesResults = votesResults;
        }


        public void CalculateResultClearWinner()
        {
            int[] result = new int[votesResults.Count];

            for (int i = 0; i < votesResults.Count; i++)
            {
                for (int j = i + 1; j < votesResults.Count; j++)
                {
                    int iWins = 0;
                    int jWins = 0;
                    foreach (var vote in votesResults) //перебор списка приоритетов
                    {
                        if (Array.IndexOf(vote, i + 1) < Array.IndexOf(vote, j + 1)) //если альтернатива i лучше j
                            iWins++;
                        else if (Array.IndexOf(vote, i + 1) != Array.IndexOf(vote, j + 1)) //если альтернатива j лучше i
                            jWins++;
                    }

                    if (iWins > jWins) //если альтернативу i выбрало больше людей
                    {
                        result[i]++;
                        Console.WriteLine($"Альтернатива '{feed[i]}' ={iWins} > альтернатива '{feed[j]}' = {jWins}");
                    }
                    else if (iWins != jWins) //если альтернативу j выбрало больше людей
                    {
                        result[j]++;
                        Console.WriteLine($"Альтернатива '{feed[j]}' ={jWins} > альтернатива '{feed[i]}' = {iWins}");
                    }
                    j++;
                }
            }
            //подсчет результата
            var a = result.Max();
            var res = Array.IndexOf(result, a);
            var res2 = Array.LastIndexOf(result, a);
            if (res == res2)
                Console.WriteLine($"Альтернатива {feed[res]} выиграла");
            else
                Console.WriteLine($"Парадокс Кондорсе! Альтернативы {feed[res]} {feed[res2]} выиграли");

        }

        public void CalculateResultKopland()
        {
            int[] result = new int[votesResults.Count];

            for (int i = 0; i < votesResults.Count; i++)
            {
                for (int j = i + 1; j < votesResults.Count; j++)
                {
                    int iWins = 0;
                    int jWins = 0;
                    foreach (var vote in votesResults) //перебор списка приоритетов
                    {
                        if (Array.IndexOf(vote, i + 1) < Array.IndexOf(vote, j + 1)) //если альтернатива i лучше j
                            iWins++;
                        else if (Array.IndexOf(vote, i + 1) != Array.IndexOf(vote, j + 1)) //если альтернатива j лучше i
                            jWins++;
                    }
                    if (iWins > jWins) //если альтернативу i выбрало больше людей
                    {
                        result[i] += 1;
                        result[j] -= 1;
                        Console.WriteLine($"Альтернатива '{feed[i]}' = {iWins} итого {result[i]}> альтернатива '{feed[j]}' = {jWins} итого {result[j]}");
                    }
                    else if (iWins != jWins) //если альтернативу j выбрало больше людей
                    {
                        result[j] += 1;
                        result[i] -= 1;
                        Console.WriteLine($"Альтернатива '{feed[j]}' = {jWins} итого {result[j]} > альтернатива '{feed[i]}' = {iWins} итого {result[i]}");
                    }
                    else //если за альтернативу k равносильна i
                        Console.WriteLine($"Альтернатива '{feed[j]}' = {jWins} итого {result[j]} = альтернатива '{feed[i]}' = {iWins} итого {result[i]}");
                }
            }
            //подсчет результата
            var a = result.Max();
            var res = Array.IndexOf(result, a);
            var res2 = Array.LastIndexOf(result, a);
            if (res == res2)
                Console.WriteLine($"Альтернатива {feed[res]} выиграла");
            else
                Console.WriteLine($"Парадокс Кондорсе! Альтернативы {feed[res]} {feed[res2]} выиграли");

        }

        public void CalculateResultSimpson()
        {
            List<int> mins = new List<int>();
            for (int i = 0; i < votesResults.Count; i++)
            {
                List<int> rate = new List<int>();
                for (int j = 0; j < votesResults[0].Length; j++)
                {
                    int iWins = 0;
                    int jWins = 0;

                    if (i != j)
                    {
                        foreach (var vote in votesResults) //перебор списка приоритетов
                        {
                            if (Array.IndexOf(vote, i + 1) < Array.IndexOf(vote, j + 1)) //если альтернатива i лучше j
                                iWins++;
                            else if (Array.IndexOf(vote, i + 1) != Array.IndexOf(vote, j + 1)) //если альтернатива j лучше i
                                jWins++;
                        }
                        rate.Add(iWins); //создаем оценочный лист для сравнения i-ой альтернативы с остальными
                    }
                }
                if (rate.Count != 0)
                {
                    int m = rate.Min(); //ищем минимумы
                    mins.Add(m);
                }
            }
            int itog = mins.IndexOf(mins.Max()) + 1; // находим максимум в списке минимумов
            Console.WriteLine("Выигрывает альтернатива " + feed[itog] + " с результатом " + mins.Max());
        }

    }
}
