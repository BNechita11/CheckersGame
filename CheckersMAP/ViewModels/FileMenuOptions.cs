using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using CheckersMAP.Commands;
using CheckersMAP.Services;
using CheckersMAP.ViewModels;
using CheckersMAP.Enums;

namespace CheckersMAP.ViewModels
{
    public class FileMenuOptions : NotifyPropertyChanges
    {
        private GameLogic gameLogic;
        private ICommand resetCommand;
        private ICommand saveCommand;
        private ICommand statisticsCommand;
        private ICommand loadCommand;

        public FileMenuOptions(GameLogic gameLogic)
        {
            this.gameLogic = gameLogic;
        }

        public ICommand ResetCommand
        {
            get
            {
                if (resetCommand == null)
                {
                    resetCommand = new NonGenericCommand(gameLogic.ResetGame);
                }
                return resetCommand;
            }
        }

        public ICommand SaveCommand
        {
            get
            {
                if (saveCommand == null)
                {
                    saveCommand = new NonGenericCommand(gameLogic.SaveGame);
                }
                return saveCommand;
            }
        }

        public ICommand LoadCommand
        {
            get
            {
                if (loadCommand == null)
                {
                    loadCommand = new NonGenericCommand(gameLogic.LoadGame);
                }
                return loadCommand;
            }
        }

        public ICommand StatisticsCommand
        {
            get
            {
                if (statisticsCommand == null)
                {
                    statisticsCommand = new NonGenericCommand(gameLogic.Statistics);
                }
                return statisticsCommand;
            }
        }
    }
}