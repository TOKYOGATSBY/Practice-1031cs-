using System;
using System.Collections.Generic;

class Program
{
    const long INF = (long)1e18;

    static long GetCost(long d, long L1, long L2, long L3, long C1, long C2, long C3)
    {
        if (d <= L1) return C1;
        if (d <= L2) return C2;
        if (d <= L3) return C3;
        return INF;
    }

    static void Main()
    {
        string[] input = Console.ReadLine().Split();
        long L1 = long.Parse(input[0]);
        long L2 = long.Parse(input[1]);
        long L3 = long.Parse(input[2]);
        long C1 = long.Parse(input[3]);
        long C2 = long.Parse(input[4]);
        long C3 = long.Parse(input[5]);

        int N = int.Parse(Console.ReadLine());

        string[] ab = Console.ReadLine().Split();
        int A = int.Parse(ab[0]) - 1;
        int B = int.Parse(ab[1]) - 1;

        long[] pos = new long[N];
        pos[0] = 0;
        for (int i = 1; i < N; i++)
        {
            pos[i] = long.Parse(Console.ReadLine());
        }

        long[] dist = new long[N];
        for (int i = 0; i < N; i++) dist[i] = INF;
        bool[] inQueue = new bool[N];

        Queue<int> queue = new Queue<int>();
        dist[A] = 0;
        queue.Enqueue(A);
        inQueue[A] = true;

        while (queue.Count > 0)
        {
            int u = queue.Dequeue();
            inQueue[u] = false;

            for (int v = u + 1; v < N; v++)
            {
                long d = pos[v] - pos[u];
                if (d > L3) break;
                long ticket = GetCost(d, L1, L2, L3, C1, C2, C3);
                if (dist[v] > dist[u] + ticket)
                {
                    dist[v] = dist[u] + ticket;
                    if (!inQueue[v])
                    {
                        queue.Enqueue(v);
                        inQueue[v] = true;
                    }
                }
            }

            for (int v = u - 1; v >= 0; v--)
            {
                long d = pos[u] - pos[v];
                if (d > L3) break;
                long ticket = GetCost(d, L1, L2, L3, C1, C2, C3);
                if (dist[v] > dist[u] + ticket)
                {
                    dist[v] = dist[u] + ticket;
                    if (!inQueue[v])
                    {
                        queue.Enqueue(v);
                        inQueue[v] = true;
                    }
                }
            }
        }

        Console.WriteLine(dist[B]);
    }
}
