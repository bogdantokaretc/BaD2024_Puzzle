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
            List<string> bestChain = [], tempBestChain = [];
            //Колекція для ітерацій та змін
            List<string> currentFragments = new(fragments);

            for (int i = 0; i < fragments.Count; i++)
            {
                //Визначення стартового фрагменту послідовності
                var startFragment = currentFragments[i];
                currentFragments.RemoveAt(i);
                List<string> remainingFragments = new(currentFragments);

                //Пошук ланцюга
                FindLongestChain(remainingFragments, [startFragment]);
                currentFragments.AddRange(fragments);

                if (tempBestChain.Count>=bestChain.Count)
                {
                    bestChain.Clear();
                    bestChain.AddRange(tempBestChain);
                }
            }
            
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
                    tempBestChain.AddRange(currentChain);
                    return tempBestChain;
                }
                else
                {
                    for (int i = 0; i < remainingFragments.Count; i++)
                    {
                        //Якщо останні дві цифри ланцюга
                        if (remainingFragments[i].StartsWith(currentChain[^1][^2..]))
                        {
                            currentChain.Add(remainingFragments[i]);
                            currentFragments.Remove(remainingFragments[i]);
                        }
                        //Якщо перші дві цифри ланцюга
                        else if (currentChain[0].StartsWith(remainingFragments[i][^2..]))
                        {
                            currentChain.Insert(0, remainingFragments[i]);
                            currentFragments.Remove(remainingFragments[i]);
                        }
                    }
                    tempBestChain.Clear();
                    remainingFragments.Clear();
                }
                return FindLongestChain(currentFragments, currentChain);
            }
        }
    }
}