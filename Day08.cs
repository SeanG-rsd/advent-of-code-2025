namespace AdventOfCode
{
    public class Day08
    {
        public static void Main(string[] args)
        {
            var answer = Playground();
            Console.WriteLine(answer);
        }

        public class Box
        {
            public long x;
            public long y;
            public long z;

            public override string ToString()
            {
                return $"({x},{y},{z})";
            }
        }

        public class Circuit
        {
            public List<(Box f, Box t)> connections = new();
            public HashSet<Box> boxes = new();
        }

        public static int Playground()
        {
            List<Box> boxes = new();

            string[] text = File.ReadAllLines("./day08.txt");

            foreach (string s in text)
            {
                string[] data = s.Split(',');
                boxes.Add(new Box() { x = long.Parse(data[0]), y = long.Parse(data[1]), z = long.Parse(data[2]) });
            }

            List<Circuit> circuits = new();

            List<(Box f, Box t)> invalid = new();

            for (int i = 0; i < 1000; i++)
            {
                var shortest = (new Box(), new Box());
                double shortestLen = double.MaxValue;

                for (int x = 0; x < boxes.Count; x++)
                {
                    for (int y = 0; y < boxes.Count; y++)
                    {
                        if (x == y) continue;
                        bool valid = true;
                        foreach (var c in circuits)
                        {
                            if (c.connections.Contains((boxes[x], boxes[y])) || c.connections.Contains((boxes[y], boxes[x])))
                            {
                                valid = false;
                                break;
                            }
                            if (c.boxes.Contains(boxes[x]) && c.boxes.Contains(boxes[y]))
                            {
                                valid = false;
                                break;
                            }
                        }
                        if (!valid) continue;
                        //if (invalid.Contains((boxes[x], boxes[y])) || invalid.Contains((boxes[y], boxes[x]))) continue;

                        //double j = Math.Pow(Math.Abs(boxes[x].x - boxes[y].x), 2);

                        double dist = Math.Sqrt(Math.Pow(Math.Abs(boxes[x].x - boxes[y].x), 2) + Math.Pow(Math.Abs(boxes[x].y - boxes[y].y), 2) + Math.Pow(Math.Abs(boxes[x].z - boxes[y].z), 2));
                        if (dist < shortestLen)
                        {
                            shortestLen = dist;
                            shortest = (boxes[x], boxes[y]);
                        }
                    }
                }

                Console.WriteLine($"{shortest.Item1.ToString()} : {shortest.Item2.ToString()}");
                bool contains = false;
                // foreach (var cir in circuits)
                // {
                //     if (cir.boxes.Contains(shortest.Item1) && cir.boxes.Contains(shortest.Item2))
                //     {
                //         Console.WriteLine("here");
                //         invalid.Add(shortest);
                //         contains = true;
                //     }
                //     if (contains) break;
                // }
                // if (contains) continue;
                
                foreach (var cir in circuits)
                {
                    foreach (var con in cir.connections)
                    {
                        if (con.f == shortest.Item1 || con.f == shortest.Item2 || con.t == shortest.Item1 || con.t == shortest.Item2)
                        {
                            contains = true;
                            cir.connections.Add(shortest);
                            cir.boxes.Add(shortest.Item1);
                            cir.boxes.Add(shortest.Item2);
                            break;
                        }
                    }
                    if (contains) break;
                }
                if (contains) continue;
                circuits.Add(new Circuit() { connections = new() { shortest }, boxes = new() { shortest.Item1, shortest.Item2 } });
            }

            List<int> lens = new();

            foreach (var cir in circuits)
            {
                HashSet<Box> len = new();
                Console.WriteLine();
                foreach (var con in cir.connections)
                {
                    len.Add(con.f);
                    len.Add(con.t);
                    Console.WriteLine($"{con.f.ToString()} : {con.t.ToString()}");
                }
                Console.WriteLine(len.Count);
                lens.Add(len.Count);
            }

            lens.Sort();

            return lens[lens.Count - 1] * lens[lens.Count - 2] * lens[lens.Count - 3];
        }
    }
}