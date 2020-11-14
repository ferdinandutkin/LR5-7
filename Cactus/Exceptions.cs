using System;

class LR7Exception : Exception
{
    static int count;
    readonly int id;

    public LR7Exception() { count++; id = count; }
    public LR7Exception(string message) : base(message) { count++; id = count; }

    public LR7Exception(string message, Exception inner)
       : base(message, inner)
    {
        count++; id = count;
    }

    public override string ToString()
    {
        return base.ToString() + $" ID: {id}";
    }
}



class WrongUnitTypeException : LR7Exception
{
    public WrongUnitTypeException() { }
    public WrongUnitTypeException(string message) : base(message) { }

    public WrongUnitTypeException(string message, Exception inner)
       : base(message, inner)
    {
    }

}


class UnitParsingException : LR7Exception
{
    public UnitParsingException() { }
    public UnitParsingException(string message) : base(message) { }

    public UnitParsingException(string message, Exception inner)
       : base(message, inner)
    {
    }

}



class PersonParsingException : UnitParsingException
{
    public PersonParsingException() { }

    public PersonParsingException(string message) : base(message) { }

    public PersonParsingException(string message, Exception inner)
       : base(message, inner)
    {
    }

}

class TransformerParsingException : UnitParsingException
{
    public TransformerParsingException() { }

    public TransformerParsingException(string message) : base(message) { }

    public TransformerParsingException(string message, Exception inner)
       : base(message, inner)
    {
    }

}
