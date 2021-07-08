using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public partial class UIEnterGame : BaseUI
{
    private string _strRoleName;
    private SaveEntityManager _saveEntityManager;
    public override void OnInit()
    {

        GetBindComponents(ObjUI);
        EventHelper.RegistEvent(m_Btn_Enter, EventClickEnter);
        EventHelper.RegistEvent(m_Btn_Load, EventClickLoad);
        RefreshContent();

    }
    private void RefreshContent()
    {
        _saveEntityManager = GameManager<SaveEntityManager>.QGetOrAddMgr();
        m_Btn_Load.interactable = _saveEntityManager.HasSaveGame();

    }
    /// <summary>
    /// Ω¯»Î”Œœ∑
    /// </summary>
    private void EventClickEnter(Button btn)
    {
        GameManager<WorldManager>.QGetOrAddMgr().SetEnterType(EnumEnterType.NewGame);
        GameStateManager.Instance.ChangeState(FsmManagerGame.GAME_STATE_START);
    }

    private void EventClickLoad(Button btn)
    {
        GameManager<WorldManager>.QGetOrAddMgr().SetEnterType(EnumEnterType.Load);
        GameStateManager.Instance.ChangeState(FsmManagerGame.GAME_STATE_START);
    }


}
