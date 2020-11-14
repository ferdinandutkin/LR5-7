using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cactus
{

    [Serializable]
    public class Army : IEnumerable<IThinkable>
    {

        private bool TypeCheck(IThinkable value) => value is Transformer or Person;

        private List<IThinkable> Container = new List<IThinkable>();
        public Army() { }

        public Army(IEnumerable<IThinkable> enumerable) => enumerable.ToList().ForEach(Add);
        public Army(params IThinkable[] values) => Array.ForEach(values, Add);
        public void Add(IThinkable value)
        {
            if (TypeCheck(value))
            {
                Container.Add(value);
            }
            else throw new WrongUnitTypeException();
        }


        public int IndexOf(IThinkable value) => TypeCheck(value) ? Container.IndexOf(value) : -1;
       
        public bool Remove(IThinkable value) => TypeCheck(value) ? Container.Remove(value) : false;
       

        public void RemoveAt(int index) => Container.RemoveAt(index);
    
        public IThinkable this[int index]
        {
            get => Container[index];
            set
            {
                if (TypeCheck(value))
                {
                    Container[index] = value;
                }
                else throw new WrongUnitTypeException();
            }
        }
        public IEnumerator<IThinkable> GetEnumerator() => ((IEnumerable<IThinkable>)Container).GetEnumerator();
 

        IEnumerator IEnumerable.GetEnumerator() => Container.GetEnumerator();

        public override string ToString()
        {
            var sb = new StringBuilder();
            Container.ForEach(
                el =>
                sb.Append(
                     el switch
                     {
                         Person p => $"Имя: {p.Name}, Дата рождения: {p.Birhday.ToString("d")}",
                         Transformer t => $"Название: {t.Name}, Дата создания: {t.CreationDate.ToString("d")}, Мощность: {t.PowerLevel}",
                         _ => "",
                     }
                     + Environment.NewLine
                 )
            );
            return sb.ToString();            

        }

        public static IThinkable ParseUnit(string s)
        {
            if (s.IndexOf("Имя") != -1)
            {
                return Person.Parse(s);
            }
            else if (s.IndexOf("Название") != -1)
            {
                return Transformer.Parse(s);
            }
            else throw new UnitParsingException();
        }
        

   



       

    }
}
