using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;
public class UICameraControl : MonoBehaviour
{

    public Slider SliderZoom;
    public Text TexZoom;
    public Slider SliderAngle;
    public Text TexAngle;
    public Slider SliderSpeed;
    public Text TexSpeed;
    private void Awake()
    {
        Invoke("EventDelaySetData", 0.1f);

    }
    private void EventDelaySetData()
    {
        SliderZoom.maxValue = CameraManager.Instance.MaxZoom;
        SliderZoom.minValue = CameraManager.Instance.MinZoom;
        SliderZoom.value = CameraManager.Instance.GetWorldCameraFieldOfView();

        SliderAngle.maxValue = CameraManager.Instance.MaxAngle;
        SliderAngle.minValue = CameraManager.Instance.MinAngle;
        SliderAngle.value = CameraManager.Instance.GetCameraAngle();

        SliderSpeed.maxValue = CameraManager.Instance.MaxSpeed;
        SliderSpeed.minValue = CameraManager.Instance.MinSpeed;
        SliderSpeed.value = CameraManager.Instance.CurSpeed;

        RefreshAngle();

        RefreshZoom();

        RefreshSpeed();

        SliderZoom.OnValueChangedAsObservable().Subscribe(x =>
        {
            CameraManager.Instance.SetWorldCameraFieldOfView(x);
            RefreshZoom();
        });

        SliderAngle.OnValueChangedAsObservable().Subscribe(x =>
        {
            CameraManager.Instance.SetCameraAngle(new Vector3(x, 0, 0));
            RefreshAngle();
        });

        SliderSpeed.OnValueChangedAsObservable().Subscribe(x =>
        {
            CameraManager.Instance.SetCameraSpeed(x);
            RefreshSpeed();
        });
    }

    private void RefreshAngle()
    {
        TexAngle.text = SliderAngle.value.ToString();
    }
    private void RefreshZoom()
    {
        TexZoom.text = SliderZoom.value.ToString();
    }
    private void RefreshSpeed()
    {
        TexSpeed.text = SliderSpeed.value.ToString();
    }
}
