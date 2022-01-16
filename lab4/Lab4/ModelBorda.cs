using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lab4
{
    class ModelBorda
    {
        string[] feed = { "RoyalConin", "Grandorf", "Pedigree", "GO!", "Monge" };

        List<int[]> voitersResalts;
        //кол-во экспертов
        int expert;
        //кол-во оценок
        int mark;
        public ModelBorda(List<int[]> VoitersResalts)
        {
            voitersResalts = VoitersResalts;
            expert = voitersResalts.Count;
            mark = voitersResalts[0].Length;
        }

        public void CalculateResult()
        {
            var markExpert = new int[mark];
            for (int indexpert = 0; indexpert < expert; indexpert++)
            {
                for (int indmark = 0; indmark < mark; indmark++)
                {
                    //Определяет index и отнимает от него кол-во элементов -1 для выставления оценки и помещает
                    // в массив оценок
                    markExpert[voitersResalts[indexpert][indmark] - 1] += Math.Abs(indmark - (mark - 1));
                }
            }
            Array.Sort(markExpert);
            for (int i = 0; i < markExpert.Length; i++)
            {
                Console.WriteLine("Альтернатива '" + feed[i] + "' сумма очков равна " + markExpert[i]);
            }
            int maxmark = markExpert.Max<int>();
            int resualt = Array.IndexOf(markExpert, maxmark);
            Console.WriteLine("Выигрывает альтернатива '" + feed[resualt] + "' с суммой очков равной " + markExpert[resualt]);

        }
    }
}
