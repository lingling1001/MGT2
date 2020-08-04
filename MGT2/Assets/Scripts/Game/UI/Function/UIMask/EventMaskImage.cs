using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EventMaskImage : MonoBehaviour
{
    private Image _imgMask;

    private void Awake()
    {
        _imgMask = transform.FindChild<Image>("UIRoot/EventMaskImage/imgMask");

    }
}
