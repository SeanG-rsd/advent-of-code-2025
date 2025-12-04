using System.Diagnostics;

namespace AdventOfCode
{
    public class Day03
    {
        public static void Main(string[] args)
        {
            var answer = Lobby();
            Console.WriteLine(answer);
        }

        public static int GetHighestForBattery(string b)
        {
            char highFirst = '0';
            char highSecond = '0';

            for (int i = 0; i < b.Length - 1; i++)
            {
                char second = '0';
                for (int j = i + 1; j < b.Length; j++)
                {
                    if (b[j] > second) second = b[j];
                }

                if (b[i] > highFirst)
                {
                    highFirst = b[i];
                    highSecond = second;
                } else if (b[i] == highFirst)
                {
                    if (second > highSecond) highSecond = second;
                }
            }
            Console.WriteLine(highFirst.ToString() + highSecond.ToString());

            return int.Parse(highFirst.ToString() + highSecond.ToString());
        }

        public static long GetHighestForBattery2(string b, int c)
        {
            string best = "";
            int start = 0;
            
            for (int i = 0; i < c; i++)
            {
                //Console.WriteLine($"{best}: {i}");
                char h = '0';
                int idx = -1;
                for (int j = start; j <= b.Length - c + i; j++)
                {
                    if (b[j] > h)
                    {
                        h = b[j];
                        idx = j + 1;
                    }
                }
                best += h;
                start = idx;
            }
            //Console.WriteLine(best);
            
            return long.Parse(best);
        }

        public static long Lobby()
        {
            long joltage = 0;

            using var reader = new StreamReader("./day03.txt");

            while (!reader.EndOfStream)
            {
                string line = reader.ReadLine()!;

                joltage += GetHighestForBattery2(line, 12);
            }

            return joltage;
        }
    }
}