namespace AdventOfCode
{
    public class Day07
    {
        static HashSet<(int line, int x)> splits = new();
        static Dictionary<(int line, int x), long> cache = new();
        public static void Main(string[] args)
        {
            var answer = Laboratories();
            Console.WriteLine(answer);
        }

        public static long Laboratories()
        {
            using var reader = new StreamReader("./day07.txt");

            List<char[]> lines = new();

            while(!reader.EndOfStream)
            {
                lines.Add(reader.ReadLine()!.ToCharArray());
            }

            int width = lines[0].Length;
            int start = -1;
            for (int i = 0; i < lines[0].Length; i++) if (lines[0][i] == 'S')
                {
                    start = i;
                    break;
                }

            return Split2(lines, 0, start);
        }

        public static int Split(List<char[]> lines, int line, int i)
        {
            if (line >= lines.Count) return 0;
            if (i < 0 || i >= lines[0].Length) return 0;
            if (splits.Contains((line, i))) return 0;
            splits.Add((line, i));
            
            if (lines[line][i] == '.') return Split(lines, line + 1, i);
            else
            {
                return 1 + Split(lines, line, i + 1) + Split(lines, line, i - 1);
            }            
        }

        public static long Split2(List<char[]> lines, int line, int i)
        {
            if (line >= lines.Count - 1) return 1;
            if (cache.ContainsKey((line, i))) return cache[(line, i)];
            
            if (lines[line][i] == '.' || lines[line][i] == 'S') return Split2(lines, line + 2, i);
            else
            {
                return cache[(line, i)] = Split2(lines, line, i + 1) + Split2(lines, line, i - 1);
            }            
        }
    }
}