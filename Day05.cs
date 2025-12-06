namespace AdventOfCode
{
    public class Day05
    {
        // public static void Main(string[] args)
        // {
        //     var answer = Cafeteria2();
        //     Console.WriteLine(answer);
        // }

        public static int Cafeteria()
        {
            int fresh = 0;

            using var reader = new StreamReader("./day05.txt");
            Dictionary<long, long> freshRanges = new();

            while (!reader.EndOfStream)
            {
                string line = reader.ReadLine()!;
                if (line == "") break;

                string[] range = line.Split('-');
                long s = long.Parse(range[0]);
                long e = long.Parse(range[1]);

                if (!freshRanges.TryAdd(s, e))                 {
                    freshRanges[s] = Math.Max(e, freshRanges[s]);
                }
            }

            while (!reader.EndOfStream)
            {
                string line = reader.ReadLine()!;

                long id = long.Parse(line);
                bool found = false;
                foreach (var key in freshRanges.Keys)
                {
                    if (id >= key && id <= freshRanges[key]) found = true;
                }
                if (found) fresh++;
            }

            return fresh;
        }

        public static long Cafeteria2()
        {
            long fresh = 0;

            using var reader = new StreamReader("./day05.txt");
            List<(long s, long e)> freshRanges = new();

            while (!reader.EndOfStream)
            {
                string line = reader.ReadLine()!;
                if (line == "") break;

                string[] range = line.Split('-');
                long s = long.Parse(range[0]);
                long e = long.Parse(range[1]);

                freshRanges.Add((s, e));
            }

            while (!reader.EndOfStream)
            {
                string line = reader.ReadLine()!;
            }

            freshRanges = freshRanges.OrderBy(a => a.s).ThenBy(a => a.e).ToList();

            Console.WriteLine(string.Join(", ", freshRanges));

            List<(long s, long e)> merged = new();

            long start = freshRanges[0].s;
            long end = freshRanges[0].e;
            for (int i = 0; i < freshRanges.Count; i++)
            {
                var (s, e) = freshRanges[i];
                Console.WriteLine((s, e));
                if (s <= end)
                {
                    end = Math.Max(e, end);
                    Console.WriteLine(e);
                } else
                {
                    merged.Add((start, end));
                    start = s;
                    end = e;
                }
            }
            merged.Add((start, end));

            Console.WriteLine(string.Join(", ", merged));

            foreach (var (s, e) in merged)
            {
                fresh += e - s;
            }

            return fresh + merged.Count;
        }
    }
}