using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace testirovanieVakansiaPegasis
{


    public class A { private A() { Console.WriteLine("A"); } }

    static class S
    {
        static S() { }
    }

    class W
    {
        protected static void Ast()
        {

        }
    }


    class Program
    {


        class Hotel
        {
            public string Name { get; set; }
            public string Category { get; set; }
        }

        static void Main(string[] args)
        {
            var hotels = new List<Hotel>
            {
                new Hotel{Name = "Hotel A", Category = "3*"},
                new Hotel{Name = "Hotel B", Category = "4*"},
                new Hotel{Name = "Hotel C", Category = "3*"},
                new Hotel{Name = "Hotel D", Category = "5*"},
                new Hotel{Name = "Hotel E", Category = "5*"},
                new Hotel{Name = "Hotel F", Category = "3*"}
            };


            var result = CountHotelsByCategory(hotels);


            foreach(var item in result)
            {
                Console.WriteLine($"{item.category}: {item.count}");
            }

            Console.ReadLine();
        }

        static List<(string category, int count)> CountHotelsByCategory(List<Hotel> Hotels)
        {
            List < (string category, int count) > retunVal = new List<(string category, int count)>();
            Hotels.ForEach(i =>
            {
                if(retunVal.Select(j => j.category).Contains(i.Category))
                {
                    int indx = retunVal.IndexOf(retunVal.Single(j => j.category == i.Category));
                    var touple = retunVal[indx];
                    touple.count++;
                    retunVal.RemoveAt(indx);
                    retunVal.Add(touple);
                }
                else
                {
                    retunVal.Add((i.Category, 1));
                }
            });

            return retunVal;
        }

        static public bool EqualCount<T>(IEnumerable<T> enumirable1, IEnumerable<T> enumirable2, out int count)
        {
            if(enumirable1.Count() == enumirable2.Count())
            {
                count = enumirable1.Count();
                return true;
            }
            else
            {
                count = default;
                return false;
            }

        }

        static public string CreateTextSummary(string text, int maxLength = 100)
        {
            string str = text;

            str.AsParallel().ForAll(i =>
            {
                i = i == '\n' ? ' ' : i;
            });


            try
            {
                str.Substring(0, maxLength);
            }
            catch (Exception e)
            {
                return e.Message;
            }

            return str.Substring(0, maxLength);
        } 

    }



}
