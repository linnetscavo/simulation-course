using System;
using System.Collections.Generic;

namespace Lab4_RandomGenerator
{

    public class MultiplicativeCongruentialGenerator
    {
        private ulong x;
        private const ulong M = 9223372036854775808UL;   // 2^63
        private const ulong Beta = 4294967299UL;          

        public MultiplicativeCongruentialGenerator(ulong seed = 4294967299)
        {
            x = seed;
        }

        public double NextDouble()
        {
            x = (Beta * x) % M;
            return (double)x / M;
        }

        public List<double> Generate(int count)
        {
            List<double> result = new List<double>(count);
            for (int i = 0; i < count; i++)
            {
                result.Add(NextDouble());
            }
            return result;
        }
    }

    public static class StatisticsCalculator
    {
        public static (double mean, double variance) Calculate(List<double> values)
        {
            int n = values.Count;

            double sum = 0;
            foreach (double value in values)
            {
                sum += value;
            }
            double mean = sum / n;

            double sumSquaredDiff = 0;
            foreach (double value in values)
            {
                double diff = value - mean;
                sumSquaredDiff += diff * diff;
            }
            double variance = sumSquaredDiff / (n - 1);

            return (mean, variance);
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            const int sampleSize = 100000;

            double theoreticalMean = 0.5;
            double theoreticalVariance = 1.0 / 12.0;

            MultiplicativeCongruentialGenerator myGenerator = new MultiplicativeCongruentialGenerator();
            List<double> mySample = myGenerator.Generate(sampleSize);
            Console.WriteLine("\nПервые 10 чисел (мой генератор):");
            for (int i = 0; i < 10; i++)
            {
                Console.WriteLine($"  [{i + 1}] {mySample[i]:F10}");
            }
            var (myMean, myVariance) = StatisticsCalculator.Calculate(mySample);

            Random builtInRandom = new Random();
            List<double> builtInSample = new List<double>(sampleSize);
            for (int i = 0; i < sampleSize; i++)
            {
                builtInSample.Add(builtInRandom.NextDouble());
            }
            Console.WriteLine("\nПервые 10 чисел (встроенный Random):");
            for (int i = 0; i < 10; i++)
            {
                Console.WriteLine($"  [{i + 1}] {builtInSample[i]:F10}");
            }
            var (builtInMean, builtInVariance) = StatisticsCalculator.Calculate(builtInSample);



            Console.WriteLine($"Размер выборки: {sampleSize}\n");

            Console.WriteLine("ТЕОРЕТИЧЕСКИЕ ЗНАЧЕНИЯ (для равномерного распределения [0,1]):");
            Console.WriteLine($"  Среднее:     {theoreticalMean:F5}");
            Console.WriteLine($"  Дисперсия:   {theoreticalVariance:F5}\n");

            Console.WriteLine("МОЙ ГЕНЕРАТОР (мультипликативный конгруэнтный метод):");
            Console.WriteLine($"  Среднее:     {myMean:F5}");
            Console.WriteLine($"  Дисперсия:   {myVariance:F5}\n");


            Console.WriteLine("ВСТРОЕННЫЙ ГЕНЕРАТОР (Random):");
            Console.WriteLine($"  Среднее:     {builtInMean:F5}");
            Console.WriteLine($"  Дисперсия:   {builtInVariance:F5}\n");

            Console.WriteLine("Нажмите любую клавишу для выхода...");
            Console.ReadKey();
        }
    }
}