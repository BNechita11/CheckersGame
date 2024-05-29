using CheckersMAP.Models;
using CheckersMAP.Services;
using CheckersMAP.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class PlayerTurnViewModels : NotifyPropertyChanges
{
    private GameLogic gameLogic;
    private PlayerTurn playerTurn;
    private string playerText;


    public PlayerTurnViewModels(GameLogic gameLogic, PlayerTurn playerTurn)
    {
        this.gameLogic = gameLogic;
        this.playerTurn = playerTurn;
    }

    public string PlayerText
    {
        get { return playerText; }
        set
        {
            playerText = value;
            NotifyPropertyChanged("PlayerText"); 
        }
    }

}

