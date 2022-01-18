using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Express.BusinessLayer
{
    public class MethodLibrary
    {
        public string RandomReferenceGenerator()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(RandomString(2, true));
            builder.Append(RandomNumber(10000, 99999));
            builder.Append(RandomString(2, true));
            return builder.ToString();
        }// Generate a random number between two numbers  
        public int RandomNumber(int min, int max)
        {
            Random random = new Random();
            return random.Next(min, max);
        }// Generate a random string with a given size  
        public string RandomString(int size, bool upper)
        {
            StringBuilder builder = new StringBuilder();
            Random random = new Random();
            char ch;
            for (int i = 0; i < size; i++)
            {
                ch = Convert.ToChar(Convert.ToInt32(Math.Floor(26 * random.NextDouble() + 65)));
                builder.Append(ch);
            }
            if (upper)
                return builder.ToString().ToUpper();
            return builder.ToString();
        }
    }
}
