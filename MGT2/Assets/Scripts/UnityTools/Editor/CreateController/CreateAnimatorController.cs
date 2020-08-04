using UnityEngine;
using System.Collections;
using UnityEditor;
using UnityEditor.Animations;//5.0改变 UnityEditorInternal;并不能用了。
using System.IO;
using System.Collections.Generic;

public class CreateAnimatorController : Editor
{
    [MenuItem("MGTools/创建Controller")]
    static void DoCreateAnimationAssets()
    {
        string strPath = GetSelectedPathOrFallback();
        //创建Controller
        AnimatorController animatorController = AnimatorController.CreateAnimatorControllerAtPath(strPath + "/animation.controller");
        //得到它的Layer
        AnimatorControllerLayer layer = animatorController.layers[0];

        List<string> paths = new List<string>();
        EditorCommonObject.GetObjectDirFiles(strPath, paths);
        for (int i = 0; i < paths.Count; i++)
        {
            AnimationClip obj = AssetDatabase.LoadAssetAtPath(paths[i], typeof(AnimationClip)) as AnimationClip;
            if (obj != null)
            {

                //AnimationClipSettings clipSetting = AnimationUtility.GetAnimationClipSettings(obj);
                //clipSetting.loopTime = true;
                //AnimationUtility.SetAnimationClipSettings(obj, clipSetting);

                string strName = paths[i].Substring(paths[i].IndexOf(".fbx") - 10);
                strName = strName.Substring(strName.LastIndexOf("_") + 1).Replace(".fbx", "");
                AddStateTransition(obj, layer, strName);
            }


        }
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
    }
    private static void AddStateTransition(AnimationClip newClip, AnimatorControllerLayer layer, string strName)
    {
        //根据动画文件读取它的AnimationClip对象
        if (newClip == null)
        {
            return;
        }

        AnimatorStateMachine sm = layer.stateMachine;
        ////取出动画名子 添加到state里面
        AnimatorState state = sm.AddState(strName);
        //5.0改变
        state.motion = newClip;

        Debug.Log(state.motion + "  " + strName);
        //把state添加在layer里面

        AnimatorStateTransition trans = sm.AddAnyStateTransition(state);

        AnimatorStateTransition exTrans = state.AddExitTransition();

        exTrans.hasExitTime = true;


    }


    public static string GetSelectedPathOrFallback()
    {
        string path = "Assets";
        foreach (UnityEngine.Object obj in Selection.GetFiltered(typeof(UnityEngine.Object), SelectionMode.Assets))
        {
            path = AssetDatabase.GetAssetPath(obj);
            if (!string.IsNullOrEmpty(path) && File.Exists(path))
            {
                //如果是目录获得目录名，如果是文件获得文件所在的目录名
                path = Path.GetDirectoryName(path);
                break;
            }
        }

        return path;
    }

}