using System;
using System.Collections.Generic;
using System.IO;
namespace pyramidNumbers
{
    public class Program
    {
        public static int lowestValue;//This is for keeping the value that the data array doesn't contain

        public static int finalValue;

        public static void Main(string[] args)
        {
            Console.WriteLine("Target Location ?");
            string targetLocation = Console.ReadLine();
            FileStream fs = new FileStream(targetLocation, FileMode.OpenOrCreate);
            int[] data = numberSplitter(fs);
            fs.Flush();
            fs.Close();
            lowestValue = data[0];

            for (int i = 1; i < data.Length; i++)
            {
                if (lowestValue > data[i])
                {
                    lowestValue = data[i];
                }
            }
            for (int i = 0; i < data.Length; i++)
            {
                isPrime(data, i);
            }

            maxValue(data, 1, 0, 0, 0);

            Console.WriteLine("Final Value is : " + finalValue);

        }
        public static void isPrime(int[] data, int index)//For making prime numbers out of scope of the data
        {
            bool prime = true;

            for (int i = 2; i < data[index] / 2; i++)
            {
                if (data[index] % i == 0)
                {
                    prime = false;
                    break;
                }
            }

            if (prime && data[index] != 1)
            {
                data[index] = lowestValue - 1;
            }
        }

        public static void maxValue(int[] data, int step, int totalNumber, int index, int tempValue)//index keeps the index of the pyramid's current step
        {
            totalNumber += step;

            if (totalNumber - step + index < data.Length)//Assures it is not out of bound
            {
                if (data[totalNumber - step + index] != lowestValue - 1)//Assures that the value is not prime
                {
                    tempValue += data[totalNumber - step + index];

                    if (data.Length > totalNumber)//This controls if there is more step
                    {
                        step++;
                        maxValue(data, step, totalNumber, index, tempValue);
                        index++;
                        maxValue(data, step, totalNumber, index, tempValue);
                    }
                    else
                    {
                        if (tempValue > finalValue)
                        {
                            finalValue = tempValue;
                        }
                    }
                }
            }
        }

        public static int[] numberSplitter(FileStream fs)//For finding numbers in a file
        {
            StreamReader sr = new StreamReader(fs);
            string text = sr.ReadToEnd();

            List<int> intList = new List<int>();

            for (int i = 0; i < text.Length; i++)
            {
                if (text[i] != ' ' && text[i] != '\n')
                {
                    string temp = "";
                    while (i < text.Length && text[i] != ' ' && text[i] != '\n')
                    {
                        temp += text[i];
                        i++;
                    }

                    intList.Add(Convert.ToInt32(temp));
                    i--;
                }
            }

            for (int i = 0; i < intList.Count; i++)
            {
                Console.WriteLine(intList[i]);
            }

            return intList.ToArray();
        }

    }


}
