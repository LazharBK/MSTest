using System;

namespace FR_1915_ListeTaches
{
    class Program
    {
        static void Main(string[] args)
        {
            Tache t = new Tache("Tâche A", DateTime.Now-TimeSpan.FromDays(2), TimeSpan.FromDays(3));

            t.Effectuer(TimeSpan.FromDays(2));
            Console.WriteLine($"La tâche '{ t.Titre }'");
            Console.WriteLine($"- A faire/Total : { t.ResteAFaire } / { t.Duree }");
            Console.WriteLine($"- Début         : { t.Debut }");
        }
    }
}
