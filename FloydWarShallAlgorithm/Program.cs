using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace FloydWarShallAlgorithm
{
    class Program
    {
        public static int n;
        public static int xp;
        public static int kt;
        public static int[,] matrix;
        public static int[,] dist;
        public static int[,] path;
        public static int ts;

        public static void Input()
        {
            StreamReader sr = new StreamReader("graph.inp");
            string[] chuoi = sr.ReadLine().Trim().Split(' ');
            n = int.Parse(chuoi[0]);
            xp = int.Parse(chuoi[1]);
            kt = int.Parse(chuoi[2]);
            ts = 10000;

            Create();
            for (int i = 0; i < n; i++)
            {
                matrix[i, i] = 0;
                string[] s = sr.ReadLine().Trim().Split(' ');
                for (int j = 0; j < s.Count(); j++)
                {
                    matrix[i, int.Parse(s[j]) - 1] = int.Parse(s[j + 1]);
                    ts += int.Parse(s[j + 1]);
                    j++;
                }
            }
            CreateOtherMatrix();
            sr.Close();
        }
        public static void OutputMatrix(int[,] a)
        {
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    if (a[i, j] == ts)
                        Console.Write(" . ");
                    else if (a[i, j] < ts && (a[i, j] > 9 || a[i, j] < 0))
                        Console.Write(a[i, j] + " ");
                    else
                        Console.Write(" " + a[i, j] + " ");
                }
                Console.WriteLine();
            }
            Console.WriteLine();
        }
        public static void Create()
        {
            matrix = new int[n, n];
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    matrix[i, j] = ts;
                }
            }
        }
        public static void CreateOtherMatrix()
        {
            dist = new int[n, n];
            path = new int[n, n];
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    path[i, j] = -1;
                    if (matrix[i, j] == 10000)
                        matrix[i, j] = ts;
                    dist[i, j] = matrix[i, j];
                }
            }
        }
        public static void FloydWarShall()
        {
            for (int k = 0; k < n; k++)
            {
                for (int i = 0; i < n; i++)
                {
                    for (int j = 0; j < n; j++)
                    {
                        if (dist[i, j] > dist[k, j] + dist[i, k])
                        {
                            dist[i, j] = dist[k, j] + dist[i, k];
                            path[i, j] = k + 1;
                        }
                    }
                }
            }
        }
        public static Stack<int> st;
        public static Queue<int> road;
        public static void FindShortestRoad()
        {
            st = new Stack<int>();
            road = new Queue<int>();
            int start = xp - 1;  // 1 - 1 = 0
            int finish = kt - 1; // 7 - 1 = 6
            st.Push(kt);
            while (path[start, finish] != -1)
            {
                st.Push(path[start, finish]);
                finish = path[start, finish] - 1;
            }
            start = xp - 1;
            road.Enqueue(xp);
            while(st.Count != 0)
            {
                finish = st.Pop();//2  //5  //6  //7
                if(path[start, finish - 1] == -1)//[0,1]  //[1,4]   //[4,5]  //[5,6]
                {
                    road.Enqueue(finish);//2  //5  //6
                    start = finish - 1;//1  //4   //5
                }
                else // nếu tìm tháy 1 đỉnh nằm giữa
                {
                    road.Enqueue(path[start, finish - 1]); // thêm đỉnh đó vào
                    start = path[start, finish - 1] - 1;
                    st.Push(finish); // gán lại phần tử vào stack
                }
            }
            while(road.Count != 0)
                Console.Write(road.Dequeue() + " ");
            Console.WriteLine();
        }
        
        static void Main(string[] args)
        {
            Input();
            Console.WriteLine("Matrix : ");
            OutputMatrix(matrix);
            FloydWarShall();
            Console.WriteLine("Dist : ");
            OutputMatrix(dist);
            Console.WriteLine("Path : ");
            OutputMatrix(path);
            Console.WriteLine("Road : ");
            FindShortestRoad();
            Console.WriteLine();
        }
    }
}
