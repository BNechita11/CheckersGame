using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Documents;
using CheckersMAP.Enums;
using CheckersMAP.Models;
using CheckersMAP.Services;
using CheckersMAP.ViewModels;


namespace CheckersMAP.Services
{
    public class GameLogic
    {
        public Player RedPlayer { get; set; }
        public Player WhitePlayer { get; set; }

        private ObservableCollection<ObservableCollection<GameSquare>> board;
        private PlayerTurn playerTurn;
        private WinnerViewModels winner;
        public GameLogic(ObservableCollection<ObservableCollection<GameSquare>> board, PlayerTurn playerTurn, WinnerViewModels winner)
        {
            this.board = board;
            this.playerTurn = playerTurn;
            this.winner = winner;
            var score = Utility.getScore(this);
            this.winner.WinnerPlayer.RedWins = score.WinnerPlayer.RedWins;
            this.winner.WinnerPlayer.WhiteWins = score.WinnerPlayer.WhiteWins;

        }

        #region Logics
        private void SwitchTurns(GameSquare square)
        {
            if (square.Piece.Color == PieceColor.Red)
            {
                Utility.Turn.PlayerColor = PieceColor.White;
                playerTurn.PlayerColor = PieceColor.White; // Actualizează culoarea jucătorului alb
            }
            else
            {
                Utility.Turn.PlayerColor = PieceColor.Red;
                playerTurn.PlayerColor = PieceColor.Red; // Actualizează culoarea jucătorului rosu
            }
        }

        private void FindNeighbours(GameSquare square)
        {
            var neighboursToCheck = new HashSet<Tuple<int, int>>();

            Utility.initializeNeighboursToBeChecked(square, neighboursToCheck);

            foreach (Tuple<int, int> neighbour in neighboursToCheck)
            {
                if (Utility.isInBounds(square.Row + neighbour.Item1, square.Column + neighbour.Item2))  
                {
                    if (board[square.Row + neighbour.Item1][square.Column + neighbour.Item2].Piece == null)
                    {
                        if (!Utility.ExtraMove)
                        {
                            Utility.CurrentNeighbours.Add(board[square.Row + neighbour.Item1][square.Column + neighbour.Item2], null);
                        }
                    }
                    else if (Utility.isInBounds(square.Row + neighbour.Item1 * 2, square.Column + neighbour.Item2 * 2) &&
                        board[square.Row + neighbour.Item1][square.Column + neighbour.Item2].Piece.Color != square.Piece.Color &&
                        board[square.Row + neighbour.Item1 * 2][square.Column + neighbour.Item2 * 2].Piece == null)
                    {
                        Utility.CurrentNeighbours.Add(board[square.Row + neighbour.Item1 * 2][square.Column + neighbour.Item2 * 2], board[square.Row + neighbour.Item1][square.Column + neighbour.Item2]);
                        Utility.ExtraPath = true; //cale suplimentara pentru o captura multipla
                    }
                }
            }
        }

        private void DisplayRegularMoves(GameSquare square)
        {
            if (Utility.CurrentSquare != square)
            {
                if (Utility.CurrentSquare != null)
                {
                    board[Utility.CurrentSquare.Row][Utility.CurrentSquare.Column].Texture = Utility.whiteSquare;

                    foreach (GameSquare selectedSquare in Utility.CurrentNeighbours.Keys)
                    {
                        selectedSquare.LegalSquareSymbol = null;
                    }
                    Utility.CurrentNeighbours.Clear();
                }

                FindNeighbours(square);

                if (Utility.ExtraMove && !Utility.ExtraPath)
                {
                    Utility.ExtraMove = false;
                    SwitchTurns(square);
                }
                else
                {

                    foreach (GameSquare neighbour in Utility.CurrentNeighbours.Keys)
                    {
                        board[neighbour.Row][neighbour.Column].LegalSquareSymbol = Utility.hintSquare;
                    }

                    Utility.CurrentSquare = square;
                    Utility.ExtraPath = false;
                }
            }
            else
            {
                board[square.Row][square.Column].Texture = Utility.whiteSquare;

                foreach (GameSquare selectedSquare in Utility.CurrentNeighbours.Keys)
                {
                    selectedSquare.LegalSquareSymbol = null;
                }
                Utility.CurrentNeighbours.Clear();
                Utility.CurrentSquare = null;
            }
        }
        #endregion
        #region ClickCommands

        public void ResetGame()
        {
            Utility.ResetGame(board);
        }

        public void SaveGame()
        {
            Utility.SaveGame(board);
        }

        public void LoadGame()
        {
            Utility.LoadGame(board);
            playerTurn.TurnPlayer = Utility.Turn.TurnPlayer;
        }

        public void Statistics()
        {
            Utility.Statistics();
        }
        public void ClickPiece(GameSquare square)
        {
            if ((Utility.Turn.PlayerColor == PieceColor.Red && square.Piece.Color == PieceColor.Red ||
                Utility.Turn.PlayerColor == PieceColor.White && square.Piece.Color == PieceColor.White) &&
                !Utility.ExtraMove)
            {
                DisplayRegularMoves(square);
            }
        }

        public void MovePiece(GameSquare square)
        {
            square.Piece = Utility.CurrentSquare.Piece;
            square.Piece.Square = square;

            if (Utility.CurrentNeighbours[square] != null)
            {
                Utility.CurrentNeighbours[square].Piece = null;
                Utility.ExtraMove = true;
            }
            else
            {
                Utility.ExtraMove = false;
                SwitchTurns(Utility.CurrentSquare);
            }

            board[Utility.CurrentSquare.Row][Utility.CurrentSquare.Column].Texture = Utility.whiteSquare;

            foreach (GameSquare selectedSquare in Utility.CurrentNeighbours.Keys)
            {
                selectedSquare.LegalSquareSymbol = null;
            }
            Utility.CurrentNeighbours.Clear();
            Utility.CurrentSquare.Piece = null;
            Utility.CurrentSquare = null;

            if (square.Piece.Type == PieceType.Regular)
            {
                if (square.Row == 0 && square.Piece.Color == PieceColor.Red)
                {
                    square.Piece.Type = PieceType.King;
                    square.Piece.Texture = Utility.redKingPiece;
                }
                else if (square.Row == board.Count - 1 && square.Piece.Color == PieceColor.White)
                {
                    square.Piece.Type = PieceType.King;
                    square.Piece.Texture = Utility.whiteKingPiece;
                }
            }

            if (Utility.ExtraMove)
            {
                if (playerTurn.TurnPlayer == Utility.redPiece)
                {
                    Utility.CollectedWhitePieces++;
                }
                if (playerTurn.TurnPlayer == Utility.whitePiece)
                {
                    Utility.CollectedRedPieces++;
                }
                DisplayRegularMoves(square);
            }

            if (Utility.CollectedRedPieces == 12 || Utility.CollectedWhitePieces == 12)
            {
                GameOver();
            }
        }

        public void GameOver()
        {
            WinnerViewModels aux = Utility.getScore(this);

            if (Utility.CollectedRedPieces == 12)
            {
                aux.WinnerPlayer.WhiteWins++;
                Utility.writeScore(aux.WinnerPlayer.RedWins, aux.WinnerPlayer.WhiteWins);
                MessageBox.Show("Rosu a castigat! Felicitari!");
            }
            else if (Utility.CollectedWhitePieces == 12)
            {
                aux.WinnerPlayer.RedWins++;
                Utility.writeScore(aux.WinnerPlayer.RedWins, aux.WinnerPlayer.WhiteWins);
                MessageBox.Show("Alb a castigat! Felicitari!");
            }

            winner.WinnerPlayer.RedWins = aux.WinnerPlayer.RedWins;
            winner.WinnerPlayer.WhiteWins = aux.WinnerPlayer.WhiteWins;
            Utility.CollectedRedPieces = 0;
            Utility.CollectedWhitePieces = 0;
            MessageBox.Show("Jocul s-a terminat!");
            Utility.ResetGame(board);
        }

        #endregion 
    }
}