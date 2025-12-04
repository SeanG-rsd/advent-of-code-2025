namespace AdventOfCode
{
    public class Day02
    {
        // public static void Main(string[] args)
        // {
        //     long answer = GiftShop();
        //     Console.WriteLine(answer);
        // }

        public static bool IsInvalid(string s)
        {
            if (s.Length % 2 == 1) return false;
            //Console.WriteLine(s);
            string first = s.Substring(0, s.Length / 2);
            string second = s.Substring(s.Length / 2);

            return first == second;
        }

        public static bool IsInvalid2(string s)
        {
            for (int i = 1; i <= s.Length / 2; i++)
            {
                if (s.Length % i != 0) continue;
                string check = s.Substring(0, i);
                bool valid = false;
                for (int j = 0; j < s.Length; j += i)
                {
                    string curr = s.Substring(j, i);
                    if (check != curr) valid = true;
                }
                if (!valid) return true;
            }

            return false;
        }

        public static long GiftShop()
        {
            long total = 0;
            using var reader = new StreamReader("./day02.txt");
            string line = reader.ReadLine()!;
            string[] ranges = line.Split(',');

            foreach (string r in ranges)
            {
                string[] data = r.Split('-');

                long s = long.Parse(data[0]);
                long e = long.Parse(data[1]);

                for (long i = s; i <= e; i++)
                {
                    if (i < 10) continue;
                    if (IsInvalid2(i.ToString()))
                    {
                        Console.WriteLine($"Invalid: {i}");
                        total += i;
                    }
                }
            }

            return total;
        }
    }
}