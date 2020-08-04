using Cinemachine;
using HedgehogTeam.EasyTouch;
using MFrameWork;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CameraManager : MonoSingleton<CameraManager>, IUpdate
{
    //public Transform ParentNode;
    public Transform TargetControl;
    public CinemachineVirtualCamera CameraWorld;
    public Camera MainCamera { get { return _mainCamera; } }
    private Camera _mainCamera;
    public float MaxZoom = 70;
    public float MinZoom = 20;
    public float MaxAngle = 90;
    public float MinAngle = 30;

    public float MaxSpeed = 30;
    public float MinSpeed = 1;
    public float CurSpeed = 30;
    public int Priority { get { return 10; } }
    private Vector3 _tempTargetPos;
    private PrototypeMap _data;
    public void Awake()
    {
        _data = MapManager.Instance.MapData;
        RefreshTargetPos(_data.CameraPosition);
        RegisterInterfaceManager.RegisteUpdate(this);
        EasyTouch.On_Pinch += On_Pinch;
        //加载相机控制
        EventLoadFinish();

    }


    private void On_Pinch(Gesture gesture)
    {
        //Log.Info(gesture.pickedObject + "  " + gesture.deltaPinch);
        SetWorldCameraFieldOfView(GetWorldCameraFieldOfView() + gesture.deltaPinch * Time.deltaTime);
    }

    public void RefreshTargetPos(Vector3 position)
    {
        if (TargetControl == null)
        {
            _tempTargetPos = position;
            return;
        }
        if (position.x < _data.GetDragLimit()[0])
        {
            position.x = _data.GetDragLimit()[0];
        }
        else if (position.x > _data.GetDragLimit()[1])
        {
            position.x = _data.GetDragLimit()[1];
        }
        if (position.z < _data.GetDragLimit()[2])
        {
            position.z = _data.GetDragLimit()[2];
        }
        else if (position.z > _data.GetDragLimit()[3])
        {
            position.z = _data.GetDragLimit()[3];
        }
        TargetControl.transform.position = position;

    }

    private void EventLoadFinish()
    {
        GameObject prefab = ResLoadHelper.LoadAsset<GameObject>(AssetsName.WORLD_CM);
        GameObject obj = NGUITools.AddChild(gameObject, prefab);

        TargetControl = new GameObject("Drag").transform;
        TargetControl.transform.position = _tempTargetPos;
        TargetControl.transform.SetParent(transform);

        CameraWorld = obj.GetComponentInChildren<CinemachineVirtualCamera>();
        _mainCamera = obj.GetComponentInChildren<Camera>();
        CameraWorld.Follow = TargetControl;


        SetCameraAngle(_data.CameraAngle);
        SetWorldCameraFieldOfView(_data.CameraFieldView);
        SetCameraSpeed(10);


    }

    public float GetWorldCameraFieldOfView()
    {
        if (CameraWorld == null)
        {
            return 0;
        }
        return CameraWorld.m_Lens.FieldOfView;
    }
    public void SetWorldCameraFieldOfView(float value)
    {
        if (CameraWorld == null)
        {
            return;
        }
        CameraWorld.m_Lens.FieldOfView = value;
    }


    public void SetCameraAngle(Vector3 value)
    {
        if (CameraWorld == null)
        {
            return;
        }
        CameraWorld.transform.localRotation = Quaternion.Euler(value);
    }

    public void SetCameraSpeed(float value)
    {
        CurSpeed = value;
    }
    public float GetCameraAngle()
    {
        if (CameraWorld == null)
        {
            return 0;
        }
        return CameraWorld.transform.eulerAngles.x;
    }
    public void On_Update(float elapseSeconds, float realElapseSeconds)
    {
        if (MainCamera == null)
        {
            return;
        }
        Gesture current = EasyTouch.current;
        if (current == null)
        {
            return;
        }
        if (current.type == EasyTouch.EvtType.None)
        {
            return;
        }
        if (current.type == EasyTouch.EvtType.On_Swipe && current.touchCount == 1)
        {
            //RefreshTargetPos(Vector3.left * CurSpeed * current.deltaPosition.x / Screen.width);
            //RefreshTargetPos(Vector3.back * CurSpeed * current.deltaPosition.y / Screen.height);
        }
        if (current.type == EasyTouch.EvtType.On_Drag && current.touchCount == 1)
        {
            Vector3 targetPos = TargetControl.position;
            float x = CurSpeed * current.deltaPosition.x / Screen.width;
            float y = CurSpeed * current.deltaPosition.y / Screen.height;
            RefreshTargetPos(new Vector3(targetPos.x - x, targetPos.y, targetPos.z - y));
        }

        // Twist
        if (current.type == EasyTouch.EvtType.On_Twist)
        {
            TargetControl.Rotate(Vector3.up * current.twistAngle);
        }
        if (current.type == EasyTouch.EvtType.On_SimpleTap)
        {
            // RefreshTargetPos(current.position);
        }


        // Log.Debug(current.type);

    }
    private void OnDestroy()
    {
        EasyTouch.On_Pinch -= On_Pinch;
        RegisterInterfaceManager.UnRegisteUpdate(this);
    }



}
