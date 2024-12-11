using System.Text;

namespace BaD2024_Puzzle
{
    internal static class Program
    {
        internal static void GetFragmentChain(string filePath) 
        {
            var fragments = File.ReadAllLines(filePath).Where(line => !string.IsNullOrWhiteSpace(line)).ToList();

            // Змінні для збереження результату
            StringBuilder bestSequence = new();
            List<string> bestChain = [];

            //Визначення стартового фрагменту послідовності
            var startFragment = fragments[0];
            fragments.RemoveAt(0);
            List<string> remainingFragments = new(fragments);

            //Пошук ланцюга
            FindLongestChain(remainingFragments, [startFragment]);

            // Формування послідовності
            for (int i = 0; i < bestChain.Count; i++)
            {
                if (i != 0) bestSequence.Append(bestChain[i][2..]);
                else bestSequence.Append(bestChain[i]);
            }
            //Вивід результатів
            Console.WriteLine("Найдовша послідовність:");
            Console.WriteLine(bestSequence);
            Console.WriteLine("Довжина найдовшої послідовності:");
            Console.WriteLine(bestSequence.Length);
            Console.WriteLine("Ланцюжок фрагментів:");
            Console.WriteLine(string.Join(" -> ", bestChain));
            Console.ReadKey();



            List<string> FindLongestChain(List<string> remainingFragments, List<string> currentChain)
            {
                if (remainingFragments.Count == 0)
                {
                    bestChain.AddRange(currentChain);
                    return bestChain;
                }
                else
                {
                    for (int i = 0; i < remainingFragments.Count; i++)
                    {
                        //Якщо останні дві цифри ланцюга
                        if (remainingFragments[i].StartsWith(currentChain[^1][^2..]))
                        {
                            currentChain.Add(remainingFragments[i]);
                            fragments.Remove(remainingFragments[i]);
                        }
                        //Якщо перші дві цифри ланцюга
                        else if (currentChain[0].StartsWith(remainingFragments[i][^2..]))
                        {
                            currentChain.Insert(0, remainingFragments[i]);
                            fragments.Remove(remainingFragments[i]);
                        }
                    }
                    remainingFragments.Clear();
                }
                return FindLongestChain(fragments, currentChain);
            }
        }
    }
}