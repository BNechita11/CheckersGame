using CheckersMAP.Models;
using CheckersMAP.Services;
using CheckersMAP.ViewModels;
using CheckersMAP.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using CheckersMAP.Enums;

namespace CheckersMAP.ViewModels
{
    public class GameSquareViewModels : NotifyPropertyChanges
    {
        private GameLogic gameLogic;
        private GameSquare genericSquare;
        private ICommand clickPieceCommand;
        private ICommand movePieceCommand;

        public GameSquareViewModels(GameSquare square, GameLogic gameLogic)
        {
            genericSquare = square;
            this.gameLogic = gameLogic;
        }

        public GameSquare GenericSquare
        {
            get
            {
                return genericSquare;
            }
            set
            {
                genericSquare = value;
                NotifyPropertyChanged("GenericSquare");
            }
        }

        public ICommand ClickPieceCommand
        {
            get
            {
                if (clickPieceCommand == null)
                {
                    clickPieceCommand = new RelayCommand<GameSquare>(gameLogic.ClickPiece);
                }
                return clickPieceCommand;
            }
        }

        public ICommand MovePieceCommand
        {
            get
            {
                if (movePieceCommand == null)
                {
                    movePieceCommand = new RelayCommand<GameSquare>(gameLogic.MovePiece);
                }
                return movePieceCommand;
            }
        }
    }
}