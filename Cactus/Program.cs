using Cactus;
using System;
using System.Diagnostics;
using System.Linq;







namespace LR5
{
    class Program
    {
     
        public static void LR5()
        {
            var a = new Automobile("3334");
            var t = new Transformer();

            t.Start();
            a.Beep();

            a.Wroom();
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


        public static void LR6()
        {

            Army GenerateArmy()
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

        static void Main()
        {

            var logger = new Logger();
            //Debug.Assert(false, "А мог бы true поставить");
            var exceptionalDelegates = new Action[]
            {
                () => _ = int.Parse("9999999999999999999999999999999"),
                () => Person.Parse("Блаблабла"),
                () =>  _ = new int[1] {1}[9],
                () => Transformer.Parse("Это точно не трансформер"),
                () => _ = 42 / Convert.ToInt32(0),
                () => new System.IO.StreamWriter(@"C:\ПапкиНеСуществует\ФайлаТоже.lul"),
                () => new Army().Add(new NotAUnit()),
            };


            try
            {
                foreach (var action in exceptionalDelegates)
                {
                    try
                    {
                        action();
                         
                    }
                    catch (Exception e)
                    {
                        logger.Log(e);
                        //trow;
                    }
                }
            }
            finally
            {

                logger.Log("Прощай мир");
           
            }
          
        }
    }
}
