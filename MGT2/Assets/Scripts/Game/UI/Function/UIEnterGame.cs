using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class UIEnterGame : BaseUI
{
    private Button BtnNewGame;
    private Button BtnLoadGame;
    private Button BtnClose;
    public override void OnInit()
    {
        base.OnInit();
        BtnNewGame = this.Find<Button>("BtnNewGame");
        BtnLoadGame = this.Find<Button>("BtnLoadGame");
        BtnClose = this.Find<Button>("BtnClose");


        BtnNewGame.RegistEvent(EventClickBtnNewGame);
        BtnClose.RegistEvent(EventClickBtnClose);


    }

    private void EventClickBtnClose()
    {
        Application.Quit();
    }

    private void EventClickBtnNewGame()
    {
        GameStateManager.Instance.FsmGameState.ChangeState(FsmManagerGame.GAME_STATE_SELECT_ROLE);
    }
}
