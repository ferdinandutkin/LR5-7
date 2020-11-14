using Cactus;
using System;
using Newtonsoft.Json;

public interface IControllable //Управление авто
{
    void TurnLeft();
    void TurnRight();

    void TurnBack();
}

struct Date
{
    public enum Time : byte
    {
        Morning, Afternoon, Evening
    };


    public int Day, Year, Month;
}






public interface IThinkable //разумное существо
{
    public void Think() => Console.WriteLine("ХММММ я думаю");
}



public abstract class Vehicle : IControllable //транспортное средство
{
    public abstract void Start();
    public abstract void Stop();
    public abstract void TurnBack();
    public abstract void TurnLeft();
    public abstract void TurnRight();
}


public class Automobile : Vehicle //машина 
{

    protected string className = "Автомобиль";

    public string RegistrationNumber { get; protected set; }

    protected bool isMoving = false;
    public void Beep() => Console.WriteLine("Бииип");

    public Automobile(string registrationNumber) => RegistrationNumber = registrationNumber;

    public Automobile() => RegistrationNumber = "12456789";


    public override void Start()
    {
        if (!isMoving)
        {
            Console.WriteLine(className + " начал движение");
            isMoving = true;
        }

    }

    public override void Stop()
    {
        if (isMoving)
        {
            Console.WriteLine(className + " закончил движение");
            isMoving = false;
        }
    }

    public override void TurnBack() => Console.WriteLine(className + " развернулся");


    public override void TurnLeft() => Console.WriteLine(className + " повернул налево");

    public override void TurnRight() => Console.WriteLine(className + " повернул направо");



    public virtual void Wroom() => Engine.Wroom();



    static class Engine //Двигатель
    {
        public static void Wroom() => Console.WriteLine("Двигатель делает бррр");

    }


    public override bool Equals(object obj)
    {
        if (obj is Automobile auto)
        {
            return auto.RegistrationNumber == RegistrationNumber;
        }
        else return false;
    }
    public override string ToString() => $"{className} с номером {RegistrationNumber} (сейчас {(isMoving ? "" : "не")} в движении)";


    public override int GetHashCode() => RegistrationNumber.GetHashCode(); 

}


public sealed class Transformer : Automobile, IThinkable
{
    private bool isTransformed = false;

    public string Name { get; set; } = string.Empty;
    public DateTime CreationDate { get; private set; } = RandomDay.Get(new DateTime(2000, 10, 11), DateTime.Today);
    public int PowerLevel { get;  set; } = 5;


    public void Transform()
    {
        if (isTransformed)
        {
            Console.WriteLine("Трасформер трансформировался обратно");
            isTransformed = false;
        }
        if (!isTransformed)
        {
            Console.WriteLine("Трасформер трансформировался");
            isTransformed = true;
        }
    }


    public Transformer() => className = "Трансформер";


    public Transformer(int powerLevel) : this() => PowerLevel = powerLevel;

    public Transformer(int powerLevel, DateTime creationDate) : this(powerLevel) => CreationDate = creationDate;

    public override void Wroom() => Console.WriteLine("Трансформер делает врум");
    public void Think() => Console.WriteLine("Думаю о нелегкой судьбе трансформеров в современном мире");
    public override string ToString() => $"Трансформер (сейчас {(isMoving ? "" : "не")} в движении)";




    //  $"Название: {t.Name}, Дата создания: {t.CreationDate.ToString("d")}, Мощность: {t.PowerLevel}",
    public static Transformer Parse(string s)
    {
        var tokens = s.Split(", ", StringSplitOptions.RemoveEmptyEntries);

        if (tokens.Length != 3)
        {
            throw new TransformerParsingException();
        }


        return new Transformer(int.Parse(tokens[2].Split(": ", StringSplitOptions.RemoveEmptyEntries)[1]),
            DateTime.Parse(tokens[1].Split(": ", StringSplitOptions.RemoveEmptyEntries)[1]))
        { Name = tokens[0].Split(": ", StringSplitOptions.RemoveEmptyEntries)[1] };

    }
} //трансформер






public abstract class Existing
{
    public abstract void Exist();
}

public interface IExistable
{
    public abstract void Exist();
}


public class Person : Existing, IThinkable, IExistable//человек
{

    public Person() => Name = string.Empty;
    public Person(string name) => Name = name;
    public Person(string name, DateTime birthday) : this(name) => Birhday = birthday;
    public string Name { get; set; }

    public DateTime Birhday { get; private set; } = RandomDay.Get(new DateTime(2000, 10, 11), DateTime.Today);

    [JsonIgnore]
    public string Phrase { get; set; } = "Здрасьте";
    public void Say() => Console.WriteLine(Phrase);

    void IExistable.Exist() => Console.WriteLine("Я мыслю, следовательно, существую как объект класса, реализующего интерфейс");

    public override string ToString() => $"Человек по имени {Name} с днем рождения {Birhday.ToString("d")})";

    public override void Exist() => Console.WriteLine("Я мыслю, следовательно, существую как объект класса-наследника");



    //$"{{Имя: {p.Name}, Дата рождения: {p.Birhday.ToString("d")}}}",
    public static Person Parse(string s)
    {
        var tokens = s.Split(", ", StringSplitOptions.RemoveEmptyEntries);

        if (tokens.Length != 2)
        {
            throw new PersonParsingException();
        }

        return new Person(tokens[0].Split(": ", StringSplitOptions.RemoveEmptyEntries)[1]
            , DateTime.Parse(tokens[1].Split(": ", StringSplitOptions.RemoveEmptyEntries)[1]));

    }
}

public class NotAUnit : IThinkable { }