using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kusogaki
{ 
    public class TimeOfCrime : EventArgs
    {
        public DateTime Time
        {
            get; set;
        }
    }

    public class Kusogaki
    {
        public event CriminalHandler Criminal;
        public TimeOfCrime e = null;
        public delegate void CriminalHandler(Kusogaki k, TimeOfCrime e);
        public void Observe()
        {
         while (true)
        {
                Console.WriteLine("#[Kusogaki] クソガキの行動を報告してください。");
                var action = Console.ReadLine();
                if (action == "万引き") {
                    if (Criminal != null)
                    {
                        Console.WriteLine($"#[Kusogaki] クソガキは {action} した。そらあかんやろ。通報や。");
                        TimeOfCrime e = new TimeOfCrime();
                        e.Time = DateTime.Now;
                        Criminal(this, e);
                    }
                } else
                {
                    Console.WriteLine($"#[Kusogaki] クソガキは {action} したけどまあいいやろ。");
                }
        }

        }
    }

    public class Police
    {
        public void Subscribe(Kusogaki k)
        {
            k.Criminal += new Kusogaki.CriminalHandler(Arrest);
            k.Criminal += new Kusogaki.CriminalHandler(Imprison);
        }
        private void Arrest(Kusogaki k, TimeOfCrime e)
        {
            Console.WriteLine($"#[Police] クソガキを連行します。犯行時間 {e.Time}");
        }
        private void Imprison(Kusogaki k, TimeOfCrime e)
        {
            Console.WriteLine($"#[Police] 懲役３年。お勤め頑張るんだぞ。");
        }
        
    }

    class Program
    {
        static void Main(string[] args)
        {
            var kusogaki = new Kusogaki();
            var police = new Police();
            police.Subscribe(kusogaki);
            kusogaki.Observe();
        }
    }
}
