/*  
To improve your programming skills, you've decided to enroll in computer school, and you're starting out with a fascinating course on algorithms and data structures!
Your instructor's marking philosophy is that they care most about consistency, so they'll be judging your performance according to your median mark in the course (based on all assignments and tests).
You'd like to be aware of your current standing in the course, so you're hoping to write an algorithm that can recalculate your median grade every time you enter a new mark (ie: every time you receive a graded test or assignment).
Given scores, an array of integers representing all test and assignment scores, your task is to return an array of integers where output[i] represents the median grade after all marks up to (and including) scores[i] have been entered. Your instructor is a generous marker, so they always round the median up to the nearest integer.

EXAMPLE

    For scores = [100, 20, 50, 70, 45] the output should be medianScores(scores) = [100, 60, 50, 60, 50].

    After each score is entered, the median is recalculated as follows:
        For [100], the median is 100 since it's the only element.
        For [20, 100], the median is (20 + 100)/2 = 60 since there's an even number of elements.
        For [20, 50, 100], the median is 50 (middle element).
        For [20, 50, 70, 100], the median is (50 + 70)/2 = 60 (mean of the two middle elements).
        For [20, 45, 50, 70, 100], the median is 50 again (middle element).
*/
using System;
using System.Collections.Generic;

namespace medianScores
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] medianScores(int[] scores) 
            {
                var medians = new List<int>();    
                var tmpScores = new List<int>();
                
                // Mediana dla jednej liczby jest równa tej liczbie
                medians.Add(scores[0]);
                // Dodajemy pierwszy element do listy
                tmpScores.Add(scores[0]);
                
                
                for (int i = 1; i < scores.Length; i++)
                {
                    var score = scores[i];
                    // Lista będzie konstruowana tak, że elementy w niej będą poukładane rosnąco. W tym przypadku możemy zastosować wyszukiwanie binarne, bo właśnie mamy posortowaną kolekcję
                    var index = tmpScores.BinarySearch(score);

                    tmpScores.Insert(index < 0 ? ~index : index, score);
                    
                    var count = tmpScores.Count;
                    
                    if ((tmpScores.Count & 1) == 1)
                    {
                        medians.Add(tmpScores[count / 2]);
                    }
                    else 
                    {
                        var median = Math.Ceiling((tmpScores[count / 2 - 1] + tmpScores[count / 2]) / 2F);
                        medians.Add((int)median);
                    }        
                }
                
                return medians.ToArray();
            }

            var scoresTest = new int[] {100, 20, 50, 70, 45};
            var answer = medianScores(scoresTest);

            foreach (var median in answer)
            {
                Console.Write(median + " ");
            }
            Console.WriteLine();
        }
    }
}
