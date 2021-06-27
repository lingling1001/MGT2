using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoText : MonoBehaviour
{
    public int TextId;
    private TMPro.TMP_Text _text;
    private void Awake()
    {
        _text = gameObject.GetComponent<TMPro.TMP_Text>();
    }

}
