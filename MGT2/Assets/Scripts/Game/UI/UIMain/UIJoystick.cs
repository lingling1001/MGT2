using HedgehogTeam.EasyTouch;
using MFrameWork;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public partial class UIJoystick : BaseUI
{
    private ETCJoystick _ectJoystick;
    private AssemblyCache _assemblyCacheView;
    private CameraManager _cameraManager;


    public override void OnInit()
    {
        EasyTouch.On_Swipe += EventOnSwipe;

        _ectJoystick = ObjUI.GetComponentInChildren<ETCJoystick>();
        _cameraManager = GameManager.QGetOrAddMgr<CameraManager>();

        GetBindComponents(ObjUI);
        RefreshContent();

    }

    private void EventOnSwipe(Gesture gesture)
    {
        //_cameraManager.ResetCameraFollow();
        //Vector3 deltaPosition = gesture.deltaPosition;
        //deltaPosition.Normalize();

        //Vector3 target = new Vector3(deltaPosition.x * 0.1f, 0, deltaPosition.y * 0.1f);

        //_cameraManager.SetFollowPosition(_cameraManager.FollowPosition - target);

        //Log.Info("{0}  {1}  ", target, deltaPosition);
    }
    private void RefreshContent()
    {




    }


    public override void OnRelease()
    {
        EasyTouch.On_Swipe -= EventOnSwipe;

        base.OnRelease();
    }


}

