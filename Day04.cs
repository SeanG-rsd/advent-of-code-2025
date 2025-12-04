namespace AdventOfCode
{
    public class Day04
    {
        public static void Main(string[] args)
        {
            var answer = PrintingDepartment();
            Console.WriteLine(answer);
        }

        public static int RemoveRolls(List<char[]> lines)
        {
            int rolls = 0;
            List<(int y, int x)> coords = new();
            for (int y = 0; y < lines.Count; y++)
            {
                for (int x = 0; x < lines[y].Length; x++)
                {
                    if (lines[y][x] == '.') continue;
                    int count = 0;
                    for (int j = -1; j <= 1; j++)
                    {
                        for (int i = -1; i <= 1; i++)
                        {
                            if (i == 0 && j == 0) continue;

                            int newY = y + j;
                            int newX = x + i;

                            if (newY < 0 || newY >= lines.Count) continue;
                            if (newX < 0 || newX >= lines[y].Length) continue;

                            if (lines[newY][newX] == '@') count++;
                        }
                    }

                    if (count < 4)
                    {
                        rolls++;
                        coords.Add((y, x));
                    }
                }
            }

            foreach (var c in coords)
            {
                lines[c.y][c.x] = '.';
            }

            return rolls;
        }

        public static int PrintingDepartment()
        {
            int total = 0;
            using var reader = new StreamReader("./day04.txt");

            List<char[]> lines = new();

            while (!reader.EndOfStream)
            {
                string line = reader.ReadLine()!;

                lines.Add(line.ToCharArray());
            }

            int rolls = 0;
            do
            {
                rolls = RemoveRolls(lines);
                total += rolls;
            } while (rolls > 0);

            return total;
        }
    }
}