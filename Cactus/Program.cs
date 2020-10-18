using Cactus;
using System;
using System.Linq;







namespace LR5
{
    class Program
    {
        static Army GenerateArmy()
        {
            var army = new Army();

            for (int i = 0; i < 45; i++)
            {
                army.Add((i % 2 == 0) ?
                    new Person("Солдат " + (i / 2 + 1), RandomDay.Get(new DateTime(1998, 10, 5), new DateTime(2005, 4, 3))) as IThinkable
                    :
                    new Transformer(new Random().Next(10), RandomDay.Get(new DateTime(2005, 10, 5), DateTime.Today)) { Name = "Трансформер " + (i + 1) / 2 }
                    );

            }

            return army;
        }

        public static void LR5()
        {
            var a = new Automobile("3334");
            var t = new Transformer();

            t.Start();
            a.Beep();

            Vehicle[] arr = { a, t };

            var arr1 = new[] { arr[0] as IControllable, arr[1] };

            Printer.IAmPrinting(arr1);

            Array.ForEach(arr1, el => el.TurnLeft());

            Printer.PrintMethods(arr1);



            var p = new Person("Ванька");

            if (p is IThinkable ip)
            {

                Array.ForEach(
                    new[] { ip, arr[1] as IThinkable },
                    Console.WriteLine);
            }

            p.Exist();
            (p as IExistable).Exist();

        }
        static void Main()
        {
            Date date;
            date = new Date() {Day = 3 };
            Date.Time time = Date.Time.Evening;

            Army army = GenerateArmy();

            Console.WriteLine(army);


            string fileName = @"C:\Users\ferdinand\Desktop\godbl\to.txt";

           
           
            ArmyController.ToFile(army, fileName);
           

            Console.WriteLine();

            Army army1;

            ArmyController.FromFile(out army1, fileName);


            army1.Add(Person.Parse("Имя: ЕгорФрост, Дата рождения: 22.08.2001"));

            army1.Add(Transformer.Parse("Название: Игорь, Дата создания: 19.08.1980, Мощность: 15"));

            Console.WriteLine(army1);

            

            Console.WriteLine("Трансформеры с уровнем мощи 3: ");
            ArmyController.SpecifiedPowerTransformerNames(army1, 3).ToList().ForEach(Console.WriteLine);

           

            Console.WriteLine("Количество боевых единиц: ");
            Console.WriteLine(ArmyController.UnitCount(army1));

            var s = ArmyController.ToJson(army1);
            Console.WriteLine(s);
            var army2 = ArmyController.FromJson(s);

            Console.WriteLine(army2);


        }
    }
}
