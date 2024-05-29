using CheckersMAP.Enums;
using CheckersMAP.Models;
using CheckersMAP.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckersMAP.Models
{
    public class GamePiece : INotifyPropertyChanged
    {
        private PieceColor color;
        private PieceType type;
        private string texture;
        private GameSquare square;

        public event PropertyChangedEventHandler PropertyChanged;

        public GamePiece(PieceColor color)
        {
            this.color = color;
            type = PieceType.Regular;
            if (color.Equals(PieceColor.Red))
            {
                texture = Utility.redPiece;
            }
            else
            {
                texture = Utility.whitePiece;
            }
        }

        public GamePiece(PieceColor color, PieceType type)
        {
            this.color = color;
            this.type = type;
            if (color.Equals(PieceColor.Red))
            {
                texture = Utility.redPiece;
            }
            else
            {
                texture = Utility.whitePiece;
            }
            if (type == PieceType.King && color.Equals(PieceColor.Red))
            {
                texture = Utility.redKingPiece;
            }
            if (type == PieceType.King && color.Equals(PieceColor.White))
            {
                texture = Utility.whiteKingPiece;
            }
        }

        public PieceColor Color
        {
            get
            {
                return color;
            }
        }

        public PieceType Type
        {
            get
            {
                return type;
            }
            set
            {
                type = value;
                NotifyPropertyChanged("Type");
            }
        }

        public string Texture
        {
            get
            {
                return texture;
            }
            set
            {
                texture = value;
                NotifyPropertyChanged("Texture");
            }
        }

        public GameSquare Square
        {
            get
            {
                return square;
            }
            set
            {
                square = value;
                NotifyPropertyChanged("Square");
            }
        }

        protected void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}