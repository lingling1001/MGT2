using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class EditorCopyComponentPath
{
    public EditorCopyComponentPath()
    {

    }
    public EditorCopyComponentPath(int type)
    {

        CopyText(GetComponent(type));
    }


    public static void CopyText(string str)
    {
        if (!string.IsNullOrEmpty(str))
        {
            TextEditor text = new TextEditor();
            text.text = str;
            text.OnFocus();
            text.Copy();
        }
    }

    public void CopyAllComponent(GameObject obj)
    {
        if (obj == null)
        {
            return;
        }



    }
    private string GetComponent(int type)
    {
        GameObject obj = Selection.activeGameObject;
        if (obj != null)
        {
            string component = string.Empty;
            string path = GenerateUINodePath(obj);
            string content = "this.Find<{0}>(\"" + path + "\");";
            string prefix = string.Empty;
            string fileName = string.Empty;
            string suffix = string.Empty;

            if (type == 2)
            {
                fileName = GetFieldName(obj.name);
            }
            if (obj.GetComponent<Button>() != null)
            {
                component = "Button";
                if (type == 2)
                {
                    prefix = "_btn";
                }
            }
            else if (obj.GetComponent<Text>() != null)
            {
                component = "Text";
                if (type == 2)
                {
                    prefix = "_txt";
                }
            }
            else if (obj.GetComponent<Image>() != null)
            {
                component = "Image";
                if (type == 2)
                {
                    prefix = "_img";
                }
            }
            //else if (obj.name.ToLower().Contains("object"))
            //{
            //    content = "this.Find(this,\"" + path + "\");";
            //    component = "GameObject";
            //    prefix = "_obj";
            //}
            //else if (obj.name.ToLower().Contains("event"))
            //{
            //    content = "ToolGameObject.FindChild(gameObject,\"" + path + "\");";
            //    component = "UIButton";
            //}
            prefix = "_";
            if (type == 2)
            {
                content = "private  " + component + " " + prefix + fileName + ";\n" + prefix + fileName + "=" + content + "\n";
                switch (component)
                {
                    case "Button":
                        suffix = prefix + fileName + ".RegistEvent(EventClick" +
                                 fileName + ");";
                        break;
                }
                content = content + suffix;
            }
            return string.Format(content, component);
        }
        return string.Empty;
    }

    private string GetFieldName(string name)
    {
        string[] strs = name.Split(new char[] { '_' }, StringSplitOptions.RemoveEmptyEntries);
        if (strs.Length == 2)
        {
            return strs[0];
        }
        return name;
    }

    private string GenerateUINodePath(GameObject obj)
    {
        if (obj == null) return "";
        string path = obj.name;

        while (obj.transform.parent != null && isContinue(obj.transform.parent.gameObject))
        {
            obj = obj.transform.parent.gameObject;
            //path = obj.name + "\\" + path;
            path = obj.name + "/" + path;
        }
        return path;
    }
    private bool isContinue(GameObject go)
    {

        Transform parent = go.transform.parent;
        if (parent != null)
        {
            if (parent.gameObject.GetComponent<Canvas>() != null ||
                parent.gameObject.GetComponent<Camera>() != null)
            {
                return false;
            }
        }
        return true;
    }
}
