using System;
using System.Collections.Generic;
using pr1.Base;
using pr1.Models;

namespace pr1;






    internal class Program
    {
        public static void Main()
        {
            List<Technologia> Shop = new List<Technologia>();
            
            Phone SmartPhone = new Phone(1,"PocoLocoX8",15000,"Androeed");
            Computer PersonalComputer = new Computer(12, "KOMP", 60000, 15.3);
            Laptop Notebook = new Laptop(123, "MacbookAir", 45000, true);
            

            Shop.Add(SmartPhone);
            Shop.Add(PersonalComputer);
            Shop.Add(Notebook);
            

            for (int j = 0; j < Shop.Count; j++)
            {
                Shop[j].GetDescription();
            }
        }
    }
