using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


public enum PlayModeState
{
    Stopped,
    Playing,
    Paused
}

[InitializeOnLoad]
public class EditorPlayMode
{
    private static PlayModeState _currentState = PlayModeState.Stopped;

    static EditorPlayMode()
    {
        EditorApplication.playModeStateChanged += OnUnityPlayModeChanged;
        EditorApplication.pauseStateChanged += OnUnityPauseStateChange;


    }

    private static void OnUnityPauseStateChange(PauseState obj)
    {
        if (GameManager.InstanceIsNull())
        {
            return;
        }

        GameTimeManager timeMgr = GameManager.Instance.GetMgr<GameTimeManager>();
        if (timeMgr == null)
        {
            return;
        }
        timeMgr.SetPauseState(obj == PauseState.Paused);

    }

    public static event Action<PlayModeState, PlayModeState> PlayModeChanged;

    public static void Play()
    {
        EditorApplication.isPlaying = true;
    }

    public static void Pause()
    {
        EditorApplication.isPaused = true;
    }

    public static void Stop()
    {
        EditorApplication.isPlaying = false;
    }


    private static void OnPlayModeChanged(PlayModeState currentState, PlayModeState changedState)
    {
        if (PlayModeChanged != null)
            PlayModeChanged(currentState, changedState);
    }

    private static void OnUnityPlayModeChanged(PlayModeStateChange state)
    {

        var changedState = PlayModeState.Stopped;
        switch (_currentState)
        {
            case PlayModeState.Stopped:
                if (EditorApplication.isPlayingOrWillChangePlaymode)
                {
                    changedState = PlayModeState.Playing;
                }
                break;
            case PlayModeState.Playing:
                if (EditorApplication.isPaused)
                {
                    changedState = PlayModeState.Paused;
                }
                else
                {
                    changedState = PlayModeState.Stopped;
                }
                break;
            case PlayModeState.Paused:
                if (EditorApplication.isPlayingOrWillChangePlaymode)
                {
                    changedState = PlayModeState.Playing;
                }
                else
                {
                    changedState = PlayModeState.Stopped;
                }
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }

        // Fire PlayModeChanged event.
        OnPlayModeChanged(_currentState, changedState);

        // Set current state.
        _currentState = changedState;
    }

}
