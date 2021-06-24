using MFrameWork;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainGame : MonoBehaviour
{
    public static MainGame Instance { get { return _instance; } }
    private static MainGame _instance;
    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(this);
        }

    }

    private void Start()
    {

        GameStateManager.Instance.OnInit();

    }
    private void Update()
    {
        RegisterInterfaceManager.Update(Time.deltaTime, Time.unscaledDeltaTime);
    }

    private void OnDestroy()
    {
        GameStateManager.Instance.OnRelease();
    }

}
