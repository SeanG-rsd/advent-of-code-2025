using System.Security.Cryptography.X509Certificates;

namespace AdventOfCode
{
    public class Day06
    {
        // public static void Main(string[] args)
        // {
        //     var answer = TrashCompactor2();
        //     Console.WriteLine(answer);
        // }

        public static long TrashCompactor()
        {
            using var reader = new StreamReader("./day06.txt");

            List<string> lines = new();

            while (!reader.EndOfStream) lines.Add(reader.ReadLine()!);

            string[] oper = lines[lines.Count - 1].Split(' ', StringSplitOptions.RemoveEmptyEntries);

            lines.RemoveAt(lines.Count - 1);

            long[] totals = new long[oper.Length];

            string[] start = lines[0].Split(' ', StringSplitOptions.RemoveEmptyEntries);
            for (int i = 0; i < start.Length; i++)
            {
                Console.WriteLine($"[{start[i]}]");
                totals[i] = long.Parse(start[i]);
            }

            for (int i = 1; i < lines.Count; i++)
            {
                string[] next = lines[i].Split(' ', StringSplitOptions.RemoveEmptyEntries);

                for (int j = 0; j < next.Length; j++)
                {
                    if (oper[j] == "+")
                    {
                        totals[j] += long.Parse(next[j]);
                    }
                    else if (oper[j] == "*")
                    {
                        totals[j] *= long.Parse(next[j]);
                    }
                }
            }

            long total = 0;
            foreach (long t in totals) total += t;

            return total;
        }

        public static long TrashCompactor2()
        {
            using var reader = new StreamReader("./day06.txt");

            List<string> unfor = new();
            List<string[]> lines = new();

            while (!reader.EndOfStream)
            {
                string s = reader.ReadLine()!;

                unfor.Add(s);
                lines.Add(s.Split(' ', StringSplitOptions.RemoveEmptyEntries));
            }

            string[] oper = lines[lines.Count - 1];

            lines.RemoveAt(lines.Count - 1);

            (int min, int max)[] minMax = new (int, int)[oper.Length];

            for (int j = 0; j < oper.Length; j++)
            {
                string s = "";
                int max = -1;
                int min = int.MaxValue;
                for (int i = 0; i < lines.Count; i++)
                {
                    max = Math.Max(lines[i][j].Length, max);
                    min = Math.Min(lines[i][j].Length, min);
                    //Console.WriteLine($"[{lines[i][j][^1]}]");
                    s += lines[i][j][^1];
                }
                minMax[j] = (min, max);
                //totals[j] = long.Parse(s);
            }

            for (int x = 0; x < unfor.Count - 1; x++)
            {
                int idx = 0;
                for (int i = 0; i < minMax.Length; i++)
                {
                    for (int j = 0; j < minMax[i].max; j++)
                    {
                        if (unfor[x][idx] == ' ')
                        {
                            lines[x][i] = lines[x][i].Insert(j, "0");
                        }
                        idx++;
                    }
                    idx++;
                }
            }

            long[] totals = new long[oper.Length];

            for (int j = 0; j < oper.Length; j++)
            {
                string s = "";
                for (int i = 0; i < lines.Count; i++)
                {
                    s += lines[i][j][^1];
                }
                totals[j] = long.Parse(s.Trim('0'));
                Console.WriteLine(totals[j]);
            }

            for (int j = 0; j < oper.Length; j++)
            {
                for (int l = 1; l < minMax[j].max; l++)
                {
                    string s = "";
                    for (int i = 0; i < lines.Count; i++)
                    {
                        if (l >= lines[i][j].Length) continue;
                        s += lines[i][j][lines[i][j].Length - 1 - l];
                    }

                    s = s.Trim('0');
                    if (oper[j] == "+")
                    {
                        Console.WriteLine($"+{s}");
                        totals[j] += long.Parse(s);
                    }
                    else if (oper[j] == "*")
                    {
                        Console.WriteLine($"*{s}");
                        totals[j] *= long.Parse(s);
                    }
                }

            }

            long total = 0;
            foreach (long t in totals) total += t;

            return total;
        }
    }
}