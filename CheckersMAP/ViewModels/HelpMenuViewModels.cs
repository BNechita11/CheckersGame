using System.ComponentModel;
using System.Windows;

namespace CheckersMAP.ViewModels
{
    public class HelpMenuViewModels : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        // Constructor privat pentru a implementa Singleton pattern
        private HelpMenuViewModels() { }

        // Singleton pattern
        private static HelpMenuViewModels instance;
        public static HelpMenuViewModels Instance
        {
            get
            {
                if (instance == null)
                    instance = new HelpMenuViewModels();
                return instance;
            }
        }

        // Metoda pentru afișarea informațiilor despre programator și joc

        public string GetAboutInformation()
        {
            return "Numele Creatorului:Bianca Nechita\nAdresa de Email: andreeanechita@studentunitbv.ro\nGrupa: 10LF223 \nDescriere: Acesta este un joc de dame simplu și distractiv!";
        }
    }
}
