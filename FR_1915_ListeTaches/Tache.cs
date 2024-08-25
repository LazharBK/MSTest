using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

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

        public string LigneCSV(IFormatProvider format)
        {
            return string.Format(
                "{0};{1};{2};{3};{4}",
                Titre,
                Debut.ToString("d", format),
                Duree.ToString("c", format),
                (Duree - ResteAFaire).ToString("c", format),
                ResteAFaire.ToString("c", format)
            );
        }

        public DateTime FinEstimee
        {
            get
            {
                decimal coefAFaire = Duree.Ticks / (decimal)ResteAFaire.Ticks;
                decimal ticksFin = (DateTime.Now.Ticks * coefAFaire - Debut.Ticks) / (coefAFaire - 1m);

                return new DateTime((long)ticksFin);
            }
        }

        private List<IProgress<TimeSpan>> suivis = new List<IProgress<TimeSpan>>();

        public void AjouterSuiviProgression(IProgress<TimeSpan> suiviProgression)
        {
            suivis.Add(suiviProgression);
        }

        public void ExporterCSV(Stream sortie, IFormatProvider format)
        {
            using (TextWriter writer = new StreamWriter(sortie, Encoding.UTF8, 1024, true))
            {
                writer.WriteLine(LigneCSV(format));
            }
        }
    }
}