
using CreatureCreator.Infrastructure.Services;
using Newtonsoft.Json.Linq;
using System.Linq.Expressions;

namespace Local_Function
{
    class Program
    {
        static void Main(string[] args)
        {
            var classA = new TestClass();
            classA.ListProperty = new List<string>() { "Value A" };

            var classB = new TestClass()
            {
                ListProperty = classA.ListProperty
            };

            classA.ListProperty.Add("Value B");

            Console.WriteLine($"{classA.ListProperty.Count}");
            Console.WriteLine($"{classB.ListProperty.Count}");
        }

        class TestClass
        {
            public List<string> ListProperty { get; set; }
        }

        public static List<T> PredicateTest<T>(Expression<Func<T, bool>> predicate, List<string> data)
        {
            var compiledPredicate = predicate.Compile();
            var result = new List<T>();
            var headers = new List<string>();
            var row = -1;

            foreach (string line in data)
            {
                row++;
                var columns = line.Split(',');
                if (row == 0)
                {
                    foreach (var header in columns)
                    {
                        headers.Add(header.Replace("[", "").Replace("]", "").Replace("_lang", ""));
                    }
                }
                else
                {
                    var lineJsonObject = new JObject();
                    for (int i = 0; i < headers.Count; i++)
                    {

                        lineJsonObject.Add(headers[i], columns[i]);
                    }

                    var lineObject = lineJsonObject.ToObject<T>();
                    if (lineObject == null)
                        continue;

                    var test = compiledPredicate(lineObject);

                    if (!compiledPredicate(lineObject))
                        continue;

                    result.Add(lineObject);
                }
            }
            return result;
        }

        public class Item
        {
            public int Id { get; set; }
            public int Value0 { get; set; }
            public int ParentItemBonusListId { get; set; }
            public int Type { get; set; } // Type 7 is used for Item Modifiers
        }
    }
}





