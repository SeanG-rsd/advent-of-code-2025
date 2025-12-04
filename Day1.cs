namespace AdventOfCode
{
    public class Day1
    {
        // public static void Main(string[] args)
        // {
        //     int password = DialLock2();
        //     Console.WriteLine(password);
        // }

        public static int DialLock()
        {
            int password = 0;
            int curr = 50;

            using var reader = new StreamReader("./day1.txt");

            while (!reader.EndOfStream)
            {
                string line = reader.ReadLine()!;

                char direction = line[0];
                int ticks = int.Parse(line[1..]);

                if (direction == 'L') ticks *= -1;
                curr += ticks;

                curr = curr % 100;
                if (curr < 0)
                {
                    curr = 100 + curr;
                } 

                if (curr == 0) password++;
            }

            return password;
        }

        public static int DialLock2()
        {
            int password = 0;
            int curr = 50;

            using var reader = new StreamReader("./day1.txt");

            while (!reader.EndOfStream)
            {
                string line = reader.ReadLine()!;

                char direction = line[0];
                int ticks = int.Parse(line[1..]);
                int h = ticks / 100;
                ticks %= 100;

                if (direction == 'L') ticks *= -1;
                curr += ticks;
                if ((curr < 0 || curr > 99) && curr - ticks != 0) password++;
                else if (curr == 0) password++;


                curr = curr % 100;
                if (curr < 0)
                {
                    curr = 100 + curr;
                } 

                password += h;
                //Console.WriteLine(password);
            }

            return password;
        }
    }
}
