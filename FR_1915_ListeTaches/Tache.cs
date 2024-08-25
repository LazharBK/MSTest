using System;
using System.Collections.Generic;
using System.Linq;

namespace FR_1915_ListeTaches
{
    public class Tache
    {      
        public Tache(string titre, DateTime debut, TimeSpan charge)
        {
            Titre = titre;
            Debut = debut;
            Duree =
            ResteAFaire = charge;
        }

        public string   Titre { get; private set; }
        public DateTime Debut { get; private set; }
        public TimeSpan Duree { get; private set; }
        public TimeSpan ResteAFaire { get; private set; }
        public bool     EstTerminee { get => ResteAFaire == TimeSpan.Zero; }

        public void Effectuer(TimeSpan duree)
        {
            if (duree <= TimeSpan.Zero || duree > ResteAFaire)
            {
                throw new ArgumentOutOfRangeException(nameof(duree), "La durée doit être strictement positive et ne peut excéder le temps restant");
            }
            ResteAFaire -= duree;
        }
        
        public static IEnumerable<Tache> FiltrerTerminees(IEnumerable<Tache> listeTaches)
        {
            return from tache in listeTaches
                   where tache.EstTerminee
                   select tache;                   
        }
    }
}