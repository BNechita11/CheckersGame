using CheckersMAP.Enums;
using CheckersMAP.Models;
using CheckersMAP.Commands;
using CheckersMAP.ViewModels;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace CheckersMAP.Services
{
    public class Utility
    {
        #region constValues
       
        public const string whiteSquare = "C:/Users/Nechita Bianca/Desktop/AN2/sem2/MAP/CheckersMAP/CheckersMAP/Resources/whiteSquare.png";
        public const string pinkSquare = "C:/Users/Nechita Bianca/Desktop/AN2/sem2/MAP/CheckersMAP/CheckersMAP/Resources/pinkSquare.jpg";
        public const string redPiece = "C:/Users/Nechita Bianca/Desktop/AN2/sem2/MAP/CheckersMAP/CheckersMAP/Resources/pieceRed.png";
        public const string whitePiece = "C:/Users/Nechita Bianca/Desktop/AN2/sem2/MAP/CheckersMAP/CheckersMAP/Resources/pieceWhite.png";
        public const string hintSquare = "C:/Users/Nechita Bianca/Desktop/AN2/sem2/MAP/CheckersMAP/CheckersMAP/Resources/hintSquare.jpg";
        public const string redKingPiece = "C:/Users/Nechita Bianca/Desktop/AN2/sem2/MAP/CheckersMAP/CheckersMAP/Resources/redKing.png";
        public const string whiteKingPiece = "C:/Users/Nechita Bianca/Desktop/AN2/sem2/MAP/CheckersMAP/CheckersMAP/Resources/whiteKing.png";
        public const string SQUARE_HIGHLIGHT = "NULL";
        


        public const char NO_PIECE = 'N';
        public const char WHITE_PIECE = 'W';
        public const char RED_PIECE = 'R';
        public const char RED_KING = 'K';
        public const char WHITE_KING = 'L';
        public const char WHITE_TURN = '2';
        public const char RED_TURN = '1';
        public const char EXTRA_MOVE = 'H';
        public const char EXTRA_PATH = 'E';
        


        public const int boardSize = 8;
        #endregion
        #region staticValues
        public static GameSquare CurrentSquare { get; set; }
        private static Dictionary<GameSquare, GameSquare> currentNeighbours = new Dictionary<GameSquare, GameSquare>();

        private static PlayerTurn turn = new PlayerTurn(PieceColor.Red);
        private static bool extraMove = false;
        private static bool extraPath = false;
        private static int collectedRedPieces = 0;
        private static int collectedWhitePieces = 0;
        #endregion

        public static Dictionary<GameSquare, GameSquare> CurrentNeighbours
        {
            get
            {
                return currentNeighbours;
            }
            set
            {
                currentNeighbours = value;
            }
        }

        public static PlayerTurn Turn
        {
            get
            {
                return turn;
            }
            set
            {
                turn = value;
            }
        }

        public static bool ExtraMove
        {
            get
            {
                return extraMove;
            }
            set
            {
                extraMove = value;
            }
        }

        public static bool ExtraPath
        {
            get
            {
                return extraPath;
            }
            set
            {
                extraPath = value;
            }
        }

        public static int CollectedWhitePieces
        {
            get { return collectedWhitePieces; }
            set { collectedWhitePieces = value; }
        }

        public static int CollectedRedPieces
        {
            get { return collectedRedPieces; }
            set { collectedRedPieces = value; }
        }


        #region UtilityMethods
        public static ObservableCollection<ObservableCollection<GameSquare>> initBoard()
        {
            ObservableCollection<ObservableCollection<GameSquare>> board = new ObservableCollection<ObservableCollection<GameSquare>>();
            const int boardSize = 8;

            for (int row = 0; row < boardSize; ++row)
            {
                board.Add(new ObservableCollection<GameSquare>());
                for (int column = 0; column < boardSize; ++column)
                {
                    if ((row + column) % 2 == 0)
                    {
                        board[row].Add(new GameSquare(row, column, SquareShade.PinkShade, null));
                    }
                    else if (row < 3)
                    {
                        board[row].Add(new GameSquare(row, column, SquareShade.WhiteShade, new GamePiece(PieceColor.White)));
                    }
                    else if (row > 4)
                    {
                        board[row].Add(new GameSquare(row, column, SquareShade.WhiteShade, new GamePiece(PieceColor.Red)));
                    }
                    else
                    {
                        board[row].Add(new GameSquare(row, column, SquareShade.WhiteShade, null));
                    }
                }
            }

            return board;
        }

        public static void ResetGameBoard(ObservableCollection<ObservableCollection<GameSquare>> squares)
        {
            for (int index1 = 0; index1 < boardSize; index1++)
            {
                for (int index2 = 0; index2 < boardSize; index2++)
                {
                    if ((index1 + index2) % 2 == 0)
                    {
                        squares[index1][index2].Piece = null;
                    }
                    else
                        if (index1 < 3)
                    {
                        squares[index1][index2].Piece = new GamePiece(PieceColor.White);
                        squares[index1][index2].Piece.Square = squares[index1][index2];
                        //pieces
                    }
                    else
                        if (index1 > 4)
                    {
                        squares[index1][index2].Piece = new GamePiece(PieceColor.Red);
                        squares[index1][index2].Piece.Square = squares[index1][index2];
                        //pieces
                    }
                    else
                    {
                        squares[index1][index2].Piece = null;
                    }
                }
            }
        }
        public static bool isInBounds(int row, int column)
        {
            return row >= 0 && column >= 0 && row < boardSize && column < boardSize;
        }

        public static void initializeNeighboursToBeChecked(GameSquare square, HashSet<Tuple<int, int>> neighboursToCheck)
        {
            if (square.Piece.Type == PieceType.King)
            {
                neighboursToCheck.Add(new Tuple<int, int>(-1, -1));
                neighboursToCheck.Add(new Tuple<int, int>(-1, 1));
                neighboursToCheck.Add(new Tuple<int, int>(1, -1));
                neighboursToCheck.Add(new Tuple<int, int>(1, 1));
            }
            else if (square.Piece.Color == PieceColor.Red)
            {
                neighboursToCheck.Add(new Tuple<int, int>(-1, -1));
                neighboursToCheck.Add(new Tuple<int, int>(-1, 1));
            }
            else
            {
                neighboursToCheck.Add(new Tuple<int, int>(1, -1));
                neighboursToCheck.Add(new Tuple<int, int>(1, 1));
            }
        }
        #endregion
        #region Serialization
        public static void LoadGame(ObservableCollection<ObservableCollection<GameSquare>> squares)
        {
            OpenFileDialog openDialog = new OpenFileDialog();
            bool? answer = openDialog.ShowDialog();

            if (answer == true)
            {
                string path = openDialog.FileName;
                using (var reader = new StreamReader(path))
                {
                    string text;
                    //current
                    if (CurrentSquare != null)
                    {
                        CurrentSquare.Texture = whiteSquare;
                    }
                    text = reader.ReadLine();
                    if (text != NO_PIECE.ToString())
                    {
                        CurrentSquare = squares[(int)char.GetNumericValue(text[0])][(int)char.GetNumericValue(text[1])];
                        CurrentSquare.Texture = SQUARE_HIGHLIGHT;
                    }
                    else
                    {
                        CurrentSquare = null;
                    }

                    text = reader.ReadLine();

                    if (text != NO_PIECE.ToString())
                    {
                        ExtraMove = true;
                    }
                    else
                    {
                        ExtraMove = false;
                    }

                    text = reader.ReadLine();
                    if (text != NO_PIECE.ToString())
                    {
                        ExtraPath = true;
                    }
                    else
                    {
                        ExtraPath = false;
                    }
                    //to_do_multi_JUMP
                    text = reader.ReadLine();
                    if (text == RED_TURN.ToString())
                    {
                        Turn.PlayerColor = PieceColor.Red;
                        Turn.TurnPlayer = redPiece;
                    }
                    else
                    {
                        Turn.PlayerColor = PieceColor.White;
                        Turn.TurnPlayer = redPiece;
                    }
                    //board
                    for (int index1 = 0; index1 < boardSize; index1++)
                    {
                        text = reader.ReadLine();
                        for (int index2 = 0; index2 < boardSize; index2++)
                        {
                            squares[index1][index2].LegalSquareSymbol = null;
                            switch (text[index2])
                            {
                                case NO_PIECE:
                                    squares[index1][index2].Piece = null;
                                    break;
                                case RED_PIECE:
                                    squares[index1][index2].Piece = new GamePiece(PieceColor.Red, PieceType.Regular);
                                    squares[index1][index2].Piece.Square = squares[index1][index2];
                                    break;
                                case RED_KING:
                                    squares[index1][index2].Piece = new GamePiece(PieceColor.Red, PieceType.King);
                                    squares[index1][index2].Piece.Square = squares[index1][index2];
                                    break;
                                case WHITE_PIECE:
                                    squares[index1][index2].Piece = new GamePiece(PieceColor.White, PieceType.Regular);
                                    squares[index1][index2].Piece.Square = squares[index1][index2];
                                    break;
                                case WHITE_KING:
                                    squares[index1][index2].Piece = new GamePiece(PieceColor.White, PieceType.King);
                                    squares[index1][index2].Piece.Square = squares[index1][index2];
                                    break;
                            }
                        }
                    }

                    foreach (var square in CurrentNeighbours.Keys)
                    {
                        square.LegalSquareSymbol = null;
                    }

                    CurrentNeighbours.Clear();

                    do
                    {
                        text = reader.ReadLine();
                        if (text == "-")
                        {
                            if (text.Length == 1)
                            {
                                break;
                            }
                            CurrentNeighbours.Add(squares[(int)char.GetNumericValue(text[0])][(int)char.GetNumericValue(text[1])], null);
                        }
                        else
                        {
                            CurrentNeighbours.Add(squares[(int)char.GetNumericValue(text[0])][(int)char.GetNumericValue(text[1])],
                                squares[(int)char.GetNumericValue(text[2])][(int)char.GetNumericValue(text[3])]);
                            
                        }
                    } while (text != "-");

                }
            }
        }

        public static void SaveGame(ObservableCollection<ObservableCollection<GameSquare>> squares)
        {
            SaveFileDialog saveDialog = new SaveFileDialog();
            bool? answer = saveDialog.ShowDialog();
            if (answer == true)
            {
                var path = saveDialog.FileName;
                using (var writer = new StreamWriter(path))
                {
                    //current
                    if (CurrentSquare != null)
                    {
                        writer.Write(CurrentSquare.Row.ToString() + CurrentSquare.Column.ToString());
                    }
                    else
                    {
                        writer.Write(NO_PIECE);
                    }
                    writer.WriteLine();
                    if (ExtraMove)
                    {
                        writer.Write(EXTRA_MOVE);
                    }
                    else
                    {
                        writer.Write(NO_PIECE);
                    }
                    writer.WriteLine();
                    if (ExtraPath)
                    {
                        writer.Write(ExtraPath);
                    }
                    else
                    {
                        writer.Write(NO_PIECE);
                    }
                    writer.WriteLine();
                    //TO_DO_MULTI_JUMP
                    if (Turn.PlayerColor.Equals(PieceColor.Red))
                    {
                        writer.Write(RED_TURN);
                    }
                    else
                    {
                        writer.Write(WHITE_TURN);
                    }
                    writer.WriteLine();
                    //board
                    foreach (var line in squares)
                    {
                        foreach (var square in line)
                        {
                            if (square.Piece == null)
                            {
                                writer.Write(NO_PIECE);
                            }
                            else if (square.Piece.Color.Equals(PieceColor.Red) && square.Piece.Type == PieceType.Regular)
                            {
                                writer.Write(RED_PIECE);
                            }
                            else if (square.Piece.Color.Equals(PieceColor.White) && square.Piece.Type == PieceType.Regular)
                            {
                                writer.Write(WHITE_PIECE);
                            }
                            else if (square.Piece.Color.Equals(PieceColor.White) && square.Piece.Type == PieceType.King)
                            {
                                writer.Write(WHITE_KING);
                            }
                            else if (square.Piece.Color.Equals(PieceColor.Red) && square.Piece.Type == PieceType.King)
                            {
                                writer.Write(RED_KING);
                            }
                            else
                            {
                                // Nu face nimic pentru ca nu e niciun tip de piesa
                            }
                        }
                        writer.WriteLine();
                    }

                    foreach (var square in CurrentNeighbours.Keys)
                    {
                        if (CurrentNeighbours[square] == null)
                        {
                            writer.Write(square.Row.ToString() + square.Column.ToString() + NO_PIECE);
                        }
                        else
                        {
                            writer.Write(square.Row.ToString() + square.Column.ToString() + CurrentNeighbours[square].Row.ToString() + CurrentNeighbours[square].Column.ToString());
                        }
                        writer.WriteLine();
                    }
                    writer.Write("-\n");
                }
            }
        }

        public static void ResetGame(ObservableCollection<ObservableCollection<GameSquare>> squares)
        {
            foreach (var square in CurrentNeighbours.Keys)
            {
                square.LegalSquareSymbol = null;
            }

            if (CurrentSquare != null)
            {
                CurrentSquare.Texture = whiteSquare;
            }

            currentNeighbours.Clear();
            CurrentSquare = null;
            ExtraMove = false;
            ExtraPath = false;
            CollectedWhitePieces = 0;
            CollectedRedPieces = 0;
            Turn.PlayerColor = PieceColor.Red;
            //texture add
            ResetGameBoard(squares);
        }
        #endregion

        #region Credits
        public static void Statistics()
        {
            string PATH = "C:/Users/Nechita Bianca/Desktop/AN2/sem2/MAP/CheckersMAP/CheckersMAP/Resources/statisticiJoc.txt";

            using (var reader = new StreamReader(PATH))
            {
                MessageBox.Show(reader.ReadToEnd(), "Statistici", MessageBoxButton.OKCancel);
            }
        }
        #endregion

        #region textFileHandling
        public static void writeScore(int r, int w)
        {
            string PATH = "C:/Users/Nechita Bianca/Desktop/AN2/sem2/MAP/CheckersMAP/CheckersMAP/Resources/winnerText.txt";
            using (var writer = new StreamWriter(PATH))
            {
                writer.WriteLine(r + "," + w);
            }
        }

        public static WinnerViewModels getScore(GameLogic gameLogic)
        {
            WinnerClass winner = new WinnerClass(0, 0);
            WinnerViewModels aux = new WinnerViewModels(gameLogic, winner);
            string PATH = "C:/Users/Nechita Bianca/Desktop/AN2/sem2/MAP/CheckersMAP/CheckersMAP/Resources/winnerText.txt";
            using (var reader = new StreamReader(PATH))
            {
                string line = reader.ReadLine();
                var splitted = line.Split(',');
                aux.WinnerPlayer.RedWins = int.Parse(splitted[0]);
                aux.WinnerPlayer.WhiteWins = int.Parse(splitted[1]);
            }
            return aux;
        }

        #endregion
    }
}