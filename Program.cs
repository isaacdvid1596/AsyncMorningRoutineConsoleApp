using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;


/*NOT ASYNC*/

namespace ConsoleApp1
{

    class Coffee
    {

    }

    class Egg
    {

    }

    class Bacon
    {

    }

    class Toast
    {

    }

    /*CAMBIAR TODO DE void → TASK*/

    /*recomendado cambiar nombre de metodos conteniendo async (FryEggsAsync) que hueva*/

    class Program
    {

        static async Task Main(string[] args)
        {
            var stopwatch = new Stopwatch();
            stopwatch.Start();

            var cup = PourCoffee();
            Console.WriteLine("taza de cafe lista");

            /*storing tasks inside of vars*/
            var eggsTask = FryEggs(2);
            var baconTask = FryBacon(2);
            var toastTask = MakeToasts(2);

            var tasks = new List<Task> {eggsTask,baconTask,toastTask};

            while (tasks.Count > 0)
            {
                var task = await Task.WhenAny(tasks);

                if (task == eggsTask)
                {
                    Console.WriteLine("Huevos listos");
                }
                else if (task == baconTask)
                {
                    Console.WriteLine("Bacon ready!");
                }
                else if (task == toastTask)
                {
                    Console.WriteLine("Toasts ready!");
                }

                tasks.Remove(task);
            }

            await Task.WhenAll(eggsTask, baconTask, toastTask);
            //await toastTask;

            //var toasts = await toastsTask;
            //ApplyButter(toasts);
            //ApplyJam(toasts);
            //Console.WriteLine("Toasts ready!");

            //var eggs = await eggsTask;

            //var bacon = await baconTask;



            stopwatch.Stop();
            Console.WriteLine($"Time elapsed {stopwatch.ElapsedMilliseconds}");
        }

        private static async Task<Toast> MakeToasts(int t)
        {
            var toasts = await ToastBread(2);

            ApplyButter(toasts);
            ApplyJam(toasts);
            return toasts;
        }

        /*no esperan asi que dejar void*/
        private static void ApplyJam(Toast toasts) => Console.WriteLine("Adding Butter");

        private static void ApplyButter(Toast toasts) => Console.WriteLine("Adding Jam");

        private static async Task<Toast> ToastBread(int t)
        {
            for (int i = 0; i < t; i++)
            {
                Console.WriteLine($"placing {t} slices of bread in the toaster");
            }

            Console.WriteLine("Starting to toast");
            await Task.Delay(300);
            Console.WriteLine("Placing on the dish");
            return new Toast();
        }

        private static async Task<Bacon> FryBacon(int b)
        {
            Console.WriteLine($"placing {b} bacon strips on the fryer");
            Console.WriteLine($"cooking one side");
            await Task.Delay(300);
            for (int i = 0; i < 1; i++)
            {
                Console.WriteLine("Flipping the bacon");
            }
            Console.WriteLine("Cooking the other side");
            await Task.Delay(300);
            Console.WriteLine("Placing it on dish");
            return new Bacon();
        }

        private static async Task<Egg> FryEggs(int e)
        {
            Console.WriteLine("Heating fryer");
            await Task.Delay(300);
            Console.WriteLine($"breaking {e} eggs");
            Console.WriteLine($"cooking the eggs");
            await Task.Delay(300);
            Console.WriteLine("Placing it on dish");
            return new Egg();
        }

        private static Coffee PourCoffee()
        {
            Console.WriteLine("Sirviendo el cafe");
            return new Coffee();
        }
    }
}
