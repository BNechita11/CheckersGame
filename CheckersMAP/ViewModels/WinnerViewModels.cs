using CheckersMAP.Models;
using CheckersMAP.Services;
using System;

namespace CheckersMAP.ViewModels
{
    public class WinnerViewModels : NotifyPropertyChanges
    {
        private GameLogic gameLogic;
        private WinnerClass winner;

        public WinnerViewModels(GameLogic gameLogic, WinnerClass winner)
        {
            this.gameLogic = gameLogic;
            this.winner = winner;
        }

        public WinnerClass WinnerPlayer
        {
            get { return winner; }
            set
            {
                winner = value;
                NotifyPropertyChanged("WinnerPlayer");
            }
        }
    }
}
