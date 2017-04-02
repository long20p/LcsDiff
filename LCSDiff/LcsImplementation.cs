using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LCSDiff
{
    public class LcsImplementation
    {
        private LcsResult[,] weights;
        private string[] source;
        private string[] target;

        public LcsImplementation(string[] source, string[] target)
        {
            this.source = source;
            this.target = target;
            this.weights = new LcsResult[source.Length, target.Length];
        }

        public void InitializeWeights()
        {
            Console.Write($"{" ",10}|");
            for (int i = 0; i < target.Length; i++)
            {
                Console.Write($"{target[i],10}|");
            }

            Console.WriteLine();

            for (int i = 0; i < source.Length; i++)
            {
                for (int j = 0; j < target.Length; j++)
                {
                    if (j == 0)
                    {
                        Console.Write($"{source[i],10}|");
                    }

                    if (source[i].Equals(target[j], StringComparison.OrdinalIgnoreCase))
                    {
                        weights[i, j] = new LcsResult();
                        weights[i, j].Length = i == 0 || j == 0 ? 1 : 1 + weights[i - 1, j - 1].Length;
                        weights[i, j].StartIndexInSource = i - weights[i, j].Length + 1;
                        weights[i, j].StartIndexInTarget = j - weights[i, j].Length + 1;
                        Console.BackgroundColor = ConsoleColor.DarkBlue;
                        Console.Write($"{$"{weights[i, j].StartIndexInSource},{weights[i, j].StartIndexInTarget},{weights[i, j].Length}",10}|");
                        Console.ResetColor();
                    }
                    else
                    {
                        weights[i, j] = new LcsResult();
                        Console.Write($"{weights[i, j].Length,10}|");
                    }
                }
                Console.WriteLine();
            }
        }

        public void GetChangeHistory()
        {
            var results = GetChangeResults(0, source.Length, 0, target.Length).ToList();
            foreach (var changeRecord in results)
            {
                Console.WriteLine($"{changeRecord.Type} {changeRecord.Length}");
            }
        }

        public IEnumerable<ChangeRecord> GetChangeResults(int sourceStartIndex, int sourceEndIndex,
            int targetStartIndex, int targetEndIndex)
        {
            if (sourceStartIndex > sourceEndIndex || targetStartIndex > targetEndIndex)
            {
                yield break;
            }

            var maxLcs = new LcsResult();

            for (int i = sourceStartIndex; i < sourceEndIndex; i++)
            {
                for (int j = targetStartIndex; j < targetEndIndex; j++)
                {
                    if (weights[i, j].Length > maxLcs.Length)
                    {
                        maxLcs = weights[i, j];
                    }
                }
            }

            if (maxLcs.Length > 0)
            {

                var before = GetChangeResults(sourceStartIndex, maxLcs.StartIndexInSource, targetStartIndex,
                    maxLcs.StartIndexInTarget);
                foreach (var lcsResult in before)
                {
                    yield return lcsResult;
                }

                yield return new ChangeRecord(ChangeType.Copy, maxLcs.Length);

                var after = GetChangeResults(maxLcs.StartIndexInSource + maxLcs.Length, sourceEndIndex,
                    maxLcs.StartIndexInTarget + maxLcs.Length, targetEndIndex);
                foreach (var lcsResult in after)
                {
                    yield return lcsResult;
                }

                yield break;
            }

            if (sourceStartIndex < sourceEndIndex)
            {
                yield return new ChangeRecord(ChangeType.Delete, sourceEndIndex - sourceStartIndex);
            }

            if (targetStartIndex < targetEndIndex)
            {
                yield return new ChangeRecord(ChangeType.Insert, targetEndIndex - targetStartIndex);
            }
        }
    }
}
