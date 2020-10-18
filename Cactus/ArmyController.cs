

using System.Collections.Generic;
using System.IO;
using System.Linq;
using System;
 
using Newtonsoft.Json;

namespace Cactus
{
    static class ArmyController
    {
        public static IEnumerable<string> SpecifiedPowerTransformerNames(Army army, int powerLevel) =>
            from unit in army
            where unit is Transformer && (unit as Transformer).PowerLevel == powerLevel
            select (unit as Transformer).Name;


        public static int UnitCount(Army army) => army.Count();


        public static void FromJsonFile(out Army army, string fileName) => army = FromJson(File.ReadAllText(fileName));

        public static void ToJsonFile(Army army, string fileName) => File.WriteAllText(fileName, ToJson(army));



        public static void FromFile(out Army army, string fileName)
        {
            army = new Army();

            foreach (var line in File.ReadAllLines(fileName))
            {
                army.Add(Army.ParseUnit(line));
            }

        }


        public static void ToFile(Army army, string fileName) => File.WriteAllText(fileName, army.ToString());




        public static string ToJson(Army army) =>
            JsonConvert.SerializeObject(army, new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.Auto });



        public static Army FromJson(string json) =>
            JsonConvert.DeserializeObject<Army>(json, new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.Auto });

    }

}
