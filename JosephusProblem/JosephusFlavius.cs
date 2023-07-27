using System;
using System.Collections.Generic;

namespace JosephusProblem
{
    /// <summary>
    /// Providing functionality for the Josephus Flavius problem.
    /// <see>You can find more details on this problem in Wikipedia - https://en.wikipedia.org/wiki/Josephus_problem</see>.
    /// </summary>
    public static class JosephusFlavius
    {
        /// <summary>
        /// Returns the iterator that generates a list of persons that are crossed out.
        /// </summary>
        /// <param name="count">Count of the persons in circle.</param>
        /// <param name="crossedOut">The number of the person to be crossed out.</param>
        /// <returns>Returns the iterator that generates a list of persons that are crossed out.</returns>
        /// <exception cref="ArgumentException"><paramref name="count"/>is less than 1.</exception>
        /// <exception cref="ArgumentException"><paramref name="crossedOut"/> is less than 1.</exception>
        public static IEnumerable<int> GetCrossedOutPersons(int count, int crossedOut)
        {
            ValidateInputParameters(count, crossedOut);

            return GetCrossedOutPersonsIterator(count, crossedOut);
        }

        private static void ValidateInputParameters(int count, int crossedOut)
        {
            if (count < 1)
            {
                throw new ArgumentException("Count must be greater than or equal to 1.", nameof(count));
            }

            if (crossedOut < 1)
            {
                throw new ArgumentException("Crossed out must be greater than 0.", nameof(crossedOut));
            }
        }

        private static IEnumerable<int> GetCrossedOutPersonsIterator(int count, int crossedOut)
        {
            Queue<int> persons = new Queue<int>(count);
            for (int i = 1; i <= count; i++)
            {
                persons.Enqueue(i);
            }

            while (persons.Count > 1)
            {
                for (int j = 0; j < crossedOut - 1; j++)
                {
                    int personToMove = persons.Dequeue();
                    persons.Enqueue(personToMove);
                }

                int crossedOutPerson = persons.Dequeue();
                yield return crossedOutPerson;
            }
        }

        /// <summary>
        /// Returns order number of survivor.
        /// </summary>
        /// <param name="count">Count of the persons in circle.</param>
        /// <param name="crossedOut">The number of the person to be crossed out.</param>
        /// <returns>The order number of the last survivor.</returns>
        /// <exception cref="ArgumentException"><paramref name="count"/>is less than 1.</exception>
        /// <exception cref="ArgumentException"><paramref name="crossedOut"/> is less than 1.</exception>
#pragma warning disable SA1202
        public static int GetSurvivor(int count, int crossedOut)
#pragma warning restore SA1202
        {
            if (count < 1)
            {
                throw new ArgumentException("Count of persons in the circle must be greater than 0.", nameof(count));
            }

            if (crossedOut < 1)
            {
                throw new ArgumentException("The number of the person to be crossed out must be greater than 0.", nameof(crossedOut));
            }

            int survivor = 0;
            for (int i = 2; i <= count; i++)
            {
                survivor = (survivor + crossedOut) % i;
            }

            return survivor + 1;
        }
    }
}
