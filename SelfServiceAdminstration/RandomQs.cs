using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections;

namespace SelfServiceAdminstration
{
    public class RandomQs
    {

        //static string[] T = { "One  ", "two ", "three ", "four ", "five ", " Six  " };
        public ArrayList PickRandom(
            ArrayList[] values, int num_values)
        {
            ArrayList list = new ArrayList();
            // The Random object this method uses.
            Random Rand = null;

            // Return num_items random values.
            // Create the Random object if it doesn't exist.
            if (Rand == null) Rand = new Random();

            // Don't exceed the array's length.
            if (num_values >= values.Length)
                num_values = values.Length - 1;

            // Make an array of indexes 0 through values.Length - 1.
            int[] indexes =
                Enumerable.Range(0, values.Length).ToArray();

            // Build the return list.
            //List<T> results = new List<T>();

            // Randomize the first num_values indexes.
            for (int i = 0; i < num_values; i++)
            {
                // Pick a random entry between i and values.Length - 1.
                int j = Rand.Next(i, values.Length);

                // Swap the values.
                int temp = indexes[i];
                indexes[i] = indexes[j];
                indexes[j] = temp;

                // Save the ith value.
               //.Add(values[1]);

                list.Add(values[indexes[i]]);
            }

            // Return the selected items.
            return list;
        }

    }
}