namespace raft
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string[] firstLine = Console.ReadLine().Split(' ');
            int n = int.Parse(firstLine[0]);
            int k = int.Parse(firstLine[1]);

            string[] secondLine = Console.ReadLine().Split(' ');
            long[] weights = new long[n];
            long sumWeights = 0;
            long maxWeight = 0;

            for(int i = 0; i < n; i ++)
            {
                weights[i] = long.Parse(secondLine[i]);
                sumWeights += weights[i];
                if(maxWeight < weights[i])
                {
                    maxWeight = weights[i];
                }
            }
            Array.Sort(weights);
            Array.Reverse(weights);


            long left = maxWeight;
            long right = sumWeights;
            long bestCapacity = right;

            while (left <= right)
            {
                long mid = left + (right - left) / 2;

                if (CanLoad(weights, n, k, mid))
                {
                    bestCapacity = mid;
                    right = mid - 1;
                }
                else
                {
                    left = mid + 1;
                }
            }
            Console.WriteLine(bestCapacity);
        }

        static bool CanLoad(long[] weights, int n, int k, long capacity)
        {
            bool[] used = new bool[n];
            int transportCount = 0;

            for(int trip = 0; trip < k; trip++)
            {
                long remainingSpace = capacity;
                for(int i = 0; i < n; i++)
                {
                    if(!used[i] && weights[i] <= remainingSpace)
                    {
                        used[i] = true;
                        remainingSpace -= weights[i];
                        transportCount++;
                    }
                }
                if (transportCount == n)
                    return true;
            }
            return transportCount == n;
        }
    }
}
