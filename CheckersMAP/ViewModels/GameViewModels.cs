using System.Collections.ObjectModel;
using CheckersMAP.Enums;
using CheckersMAP.Models;
using CheckersMAP.Services;
using System.ComponentModel;

namespace CheckersMAP.ViewModels
{
    public class GameViewModels : INotifyPropertyChanged
    {
        private GameLogic gameLogic;
        public ObservableCollection<ObservableCollection<GameSquareViewModels>> Board { get; set; }
        public FileMenuOptions Interactions { get; set; }
        public WinnerViewModels WinnerViewModels { get; set; }
        public PlayerTurnViewModels PlayerTurnViewModels { get; set; }
        public string RED_PIECE { get; set; }
        public string WHITE_PIECE { get; set; }

        public GameLogic Logic
        {
            get { return gameLogic; }
            set
            {
                gameLogic = value;
                NotifyPropertyChanged("Logic");
            }
        }
        public GameViewModels()
        {
           // gameLogic = new GameLogic(); // Inițializați corect instanța gameLogic

            ObservableCollection<ObservableCollection<GameSquare>> board = Utility.initBoard();
            PlayerTurn playerTurn = new PlayerTurn(PieceColor.Red);
            WinnerViewModels winner = Utility.getScore(gameLogic);
            Logic = new GameLogic(board, playerTurn, winner);
            PlayerTurnViewModels = new PlayerTurnViewModels(Logic, playerTurn);
            WinnerViewModels = winner;
            Board = CellBoardToCellViewModelsBoard(board);
            Interactions = new FileMenuOptions(Logic);
            RED_PIECE = Utility.redPiece;
            WHITE_PIECE = Utility.whitePiece;
        }


        // Proprietate pentru afișarea numărului de piese rămase pentru jucătorul alb
        public int WhitePlayerRemainingPieces
        {
            get { return gameLogic.WhitePlayer.RemainingPieces; }
        }

        // Proprietate pentru afișarea numărului de piese rămase pentru jucătorul roșu
        public int RedPlayerRemainingPieces
        {
            get { return gameLogic.RedPlayer.RemainingPieces; }
        }

        private ObservableCollection<ObservableCollection<GameSquareViewModels>> CellBoardToCellViewModelsBoard(ObservableCollection<ObservableCollection<GameSquare>> board)
        {
            ObservableCollection<ObservableCollection<GameSquareViewModels>> result = new ObservableCollection<ObservableCollection<GameSquareViewModels>>();
            for (int i = 0; i < board.Count; i++)
            {
                ObservableCollection<GameSquareViewModels> line = new ObservableCollection<GameSquareViewModels>();
                for (int j = 0; j < board[i].Count; j++)
                {
                    GameSquare c = board[i][j];
                    GameSquareViewModels cellVM = new GameSquareViewModels(c, Logic);
                    line.Add(cellVM);
                }
                result.Add(line);
            }
            return result;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void NotifyPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
