using CheckersMAP.Enums;
using CheckersMAP.ViewModels;
using CheckersMAP.Enums;
using CheckersMAP.ViewModels;
using System;

public class PlayerTurn : NotifyPropertyChanges
{
    public int RemainingPieces { get; set; }
    private PieceColor color;
    private string text;
    private PlayerTurnViewModels playerTurnViewModels; 

    public PlayerTurn(PieceColor color)
    {
        this.color = color;
        SetTurnText();
    }

    // Metodă pentru a seta textul în funcție de culoare
    private void SetTurnText()
    {
        if (color == PieceColor.Red)
        {
            text = "RED";
        }
        else if (color == PieceColor.White)
        {
            text = "WHITE";
        }
        else
        {
            text = null; 
        }
        if (playerTurnViewModels != null)
            playerTurnViewModels.PlayerText = text;
    }


    public PieceColor PlayerColor
    {
        get { return color; }
        set
        {
            color = value;
            SetTurnText();
            NotifyPropertyChanged("PlayerColor");
        }
    }

    public string TurnPlayer
    {
        get { return text; }
        set
        {
            text = value;
            NotifyPropertyChanged("TurnPlayer");
        }
    }
}
