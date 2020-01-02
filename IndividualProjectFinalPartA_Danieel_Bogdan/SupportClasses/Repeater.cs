using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IndividualProjectFinalPartA_Danieel_Bogdan
{
    class Repeater
    {
        public static bool RepeatTheProgram(string choice)
        {
            int counter = 0;
            bool Active = counter == 0;

            switch (choice.ToUpper())
            {
                case "Y":
                    Active = counter == 0;
                    return Active;

                case "N":
                    Active = counter == 1;
                    return Active;

                default:
                    Active = counter == 1;
                    Console.WriteLine("There is no such option. The programm will Terminate");
                    Console.WriteLine("No Wait.......I....L.....");
                    return Active;
            }

        }
    }
}
