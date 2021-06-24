using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEditor;
using UnityEngine;
/// <summary>
/// 提交工具 
/// 1 必须在环境变量中 添加svn 的路径，保证svn 命令可执行
/// 2 点击刷新列表 调用 svn status 获取项目路径的变更状态
/// 
/// </summary>
public class EditorSVNTools : EditorWindow
{
    public List<SVNFileInfo> AllFiles = new List<SVNFileInfo>();
    /// <summary>
    /// 筛选列表
    /// </summary>
    public List<SVNFileInfo> FileInfosFilters = new List<SVNFileInfo>();

    private Vector2 _viewSize = Vector2.zero;

    private string _strCommitInfo = "更新";
    private bool _isAutoSelectScript = true;
    private bool _isAutoRefresh = true;
    private bool _isReverseInput = false;
    private bool _isRevertFilter = false;
    private List<string> _saveConfigs = new List<string>();
    private string _filterInfo = string.Empty;
    private string[] _filterInfos;
    private EnumSVNFileState _filterType = EnumSVNFileState.All;

    /// <summary>
    /// 输出信息
    /// </summary>
    private string _logInfo = "日志信息";
    void OnEnable()
    {
        if (_isAutoRefresh)
        {
            RefreshFileInfos();
        }
    }
    void OnGUI()
    {

        if (DrawDragContent())
        {
            return;
        }

        DrawTitle();

        DrawContent();

        DrawBottom();



    }

    /// <summary>
    /// 聚焦
    /// </summary>
    void OnFocus()
    {

    }

    #region  拖动区域功能逻辑 

    private string _lastFileName;
    private Object _lastFileObj;
    private Rect _rectDrag;
    private bool _isDraging;
    private bool DrawDragContent()
    {
        GUILayout.Label(" 将文件拖入下方空白区域");
        _rectDrag = EditorGUILayout.GetControlRect(GUILayout.Width(500), GUILayout.Height(50));
        bool isDrag = false;
        if (Event.current.type == EventType.DragUpdated)
        {
            DragAndDrop.visualMode = DragAndDropVisualMode.Move;
            isDrag = true;
        }
        else if (Event.current.type == EventType.DragExited)
        {
            isDrag = false;
            _isDraging = false;
            ResetLastSelect();
        }
        //第一次拖动
        if (isDrag != _isDraging)
        {
            _isDraging = isDrag;
            //改变鼠标的外表  
            if (DragAndDrop.objectReferences != null && DragAndDrop.objectReferences.Length > 0)
            {
                return DragObjectInfos(DragAndDrop.objectReferences, _isAutoSelectScript);
            }
            if (DragAndDrop.paths != null && DragAndDrop.paths.Length > 0)
            {
                if (_lastFileName != DragAndDrop.paths[0])
                {
                    for (int cnt = 0; cnt < DragAndDrop.paths.Length; cnt++)
                    {
                        Object obj = AssetDatabase.LoadAssetAtPath(DragAndDrop.paths[cnt], typeof(Object));
                        DragObjectInfo(obj, _isAutoSelectScript);
                    }
                    SortFileInfosFilters();
                    SaveMapInfo();
                    _lastFileName = DragAndDrop.paths[0];
                    return true;
                }
            }
        }
        return false;
    }

    private bool DragObjectInfos(Object[] objs, bool isSelectScript)
    {
        if (_lastFileObj != objs[0])
        {
            string strLog = "\n";
            string strSucceed = "\n";
            bool res = false;
            for (int cnt = 0; cnt < objs.Length; cnt++)
            {
                if (DragObjectInfo(objs[cnt], isSelectScript))
                {
                    res = true;
                    strSucceed = strSucceed + objs[cnt].name + "\n";
                }
                else
                {
                    strLog = strLog + objs[cnt].name + "\n";
                }
            }
            if (res)
            {
                SetLogInfo(" Change File Info  " + strSucceed);
                RefreshFilterList();
                SaveMapInfo();
            }
            else
            {
                SetLogInfo("Not Change Files " + strLog);
            }
            _lastFileObj = DragAndDrop.objectReferences[0];
            return true;
        }
        return false;
    }
    private bool DragObjectInfo(Object obj, bool isSelectScript)
    {
        bool res = false;
        Object parentObject = PrefabUtility.GetPrefabParent(obj);
        string path;
        if (parentObject != null)
        {
            path = AssetDatabase.GetAssetPath(parentObject);
        }
        else
        {
            path = AssetDatabase.GetAssetPath(obj);
        }
        SVNFileInfo info = GetFileDataByName(path);
        if (info != null)
        {
            info.SetIsSelect(true);
            res = true;
        }

        if (isSelectScript)
        {
            List<SVNFileInfo> references = DragObjectReferenceInfo(obj);
            if (references != null && references.Count > 0)
            {
                for (int cnt = 0; cnt < references.Count; cnt++)
                {
                    references[cnt].SetIsSelect(true);
                    res = true;
                }
            }
        }
        return res;
    }
    private List<SVNFileInfo> DragObjectReferenceInfo(Object obj)
    {
        GameObject trans = obj as GameObject;
        if (trans == null)
        {
            return null;
        }
        Component[] coms = trans.GetComponents<Component>();
        if (coms == null)
        {
            return null;
        }
        List<SVNFileInfo> list = new List<SVNFileInfo>();
        for (int cnt = 0; cnt < coms.Length; cnt++)
        {
            string strName = coms[cnt].GetType().Name + ".cs";
            SVNFileInfo svnInfo = GetFileDataByContainName(strName);
            if (svnInfo != null)
            {
                list.Add(svnInfo);
            }
        }
        return list;
    }


    private void ResetLastSelect()
    {
        _lastFileName = string.Empty;
        _lastFileObj = null;
    }
    #endregion

    private void DrawTitle()
    {
        EditorGUILayout.BeginHorizontal();
        GUILayout.Label("筛选：", GUILayout.Width(35));
        string strfilter = EditorGUILayout.TextField(_filterInfo);
        if (strfilter != _filterInfo)
        {
            _filterInfo = strfilter;
            _filterInfos = MFrameWork.Utility.Xml.ParseString<string>(strfilter, MFrameWork.Utility.Xml.SplitAsterisk);
            RefreshFilterList();
        }
        ////反向选择
        //GUILayout.Label("反转：", GUILayout.Width(35));
        //bool isReverseInput = EditorGUILayout.Toggle(_isReverseInput);
        //if (_isReverseInput != isReverseInput)
        //{
        //    _isReverseInput = isReverseInput;
        //    RefreshFilterList();
        //}
        GUILayout.Label("筛选类型:", GUILayout.Width(55));
        EnumSVNFileState type = (EnumSVNFileState)EditorGUILayout.EnumPopup(_filterType);
        if (type != _filterType)
        {
            _filterType = type;
            RefreshFilterList();
        }
        //GUILayout.Label("反转：", GUILayout.Width(35));
        //bool isRevertFilter = EditorGUILayout.Toggle(_isRevertFilter);
        //if (_isRevertFilter != isRevertFilter)
        //{
        //    _isRevertFilter = isRevertFilter;
        //    RefreshFilterList();
        //}

        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button("T", GUI.skin.label, GUILayout.Width(15)))
        {
            SortListByValue();
        }
        GUILayout.Label(FileInfosFilters.Count + "/" + AllFiles.Count);
        GUILayout.Label("自動選中脚本 ：", GUILayout.Width(80));
        _isAutoSelectScript = EditorGUILayout.Toggle(_isAutoSelectScript);
        GUILayout.Label("自動刷新 ：", GUILayout.Width(50));
        bool isAutoRefresh = EditorGUILayout.Toggle(_isAutoRefresh);
        if (isAutoRefresh != _isAutoRefresh)
        {
            string saveValue = isAutoRefresh ? "1" : "0";
            EditorConfitTools.SaveConfigByType(EnumLoadSettingPath.SVNConfigTools, saveValue);
            _isAutoRefresh = isAutoRefresh;
        }
        if (GUILayout.Button("Sort", GUILayout.Width(38)))
        {
            SortFileInfosFilters();

        }
        if (GUILayout.Button("CLS", GUILayout.Width(38)))
        {
            for (int cnt = 0; cnt < AllFiles.Count; cnt++)
            {
                AllFiles[cnt].SetIsSelect(false);
            }
            RefreshFilterList();
        }

        EditorGUILayout.EndHorizontal();


    }

    private void DrawContent()
    {
        _isLeftShift = Event.current.shift;
        _viewSize = EditorGUILayout.BeginScrollView(_viewSize, GUILayout.Height(350));
        for (int cnt = 0; cnt < FileInfosFilters.Count; cnt++)
        {
            GUILayout.BeginHorizontal();

            SVNFileInfo fileData = FileInfosFilters[cnt];
            string strType = fileData.Flag;

            if (fileData.State == EnumSVNFileState.Add)
            {
                GUI.color = Color.yellow;
            }
            else
            {
                GUI.color = Color.cyan;
            }
            if (GUILayout.Button(strType, GUI.skin.label, GUILayout.Width(16)))
            {
                CheckClickShift(cnt);
            }
            GUI.color = fileData.IsSelect ? Color.yellow : Color.white;
            SetDataSelect(fileData, EditorGUILayout.Toggle(fileData.IsSelect, GUILayout.Width(20)));
            if (GUILayout.Button(fileData.ToString(), GUI.skin.label, GUILayout.Width(500)))
            {
                CheckClickDouble(cnt);
                //SetDataSelect(fileData, !fileData.IsSelect);
            }
            GUILayout.EndHorizontal();
        }
        GUI.color = Color.white;
        EditorGUILayout.EndScrollView();
    }
    private bool _isLeftShift = false;
    private int _shiftIdx = 0;
    private void CheckClickShift(int idx)
    {
        if (!_isLeftShift)
        {
            _shiftIdx = idx;
            return;
        }
        if (_shiftIdx == idx)
        {
            return;
        }
        List<SVNFileInfo> list = null;
        if (_shiftIdx > idx)
        {
            list = this.FileInfosFilters.FindAll(item => item.Index < _shiftIdx && item.Index > idx);
        }
        else
        {
            list = this.FileInfosFilters.FindAll(item => item.Index > _shiftIdx && item.Index < idx);
        }
        for (int cnt = 0; cnt < list.Count; cnt++)
        {
            if (list[cnt].IsMetaFile)
            {
                SetLogInfo(" .meta 不支持 多选。 ");
                continue;
            }
            SetDataSelect(list[cnt], !list[cnt].IsSelect);
        }

    }

    private float _lastTime;
    private int _lastClickIdx;

    private void CheckClickDouble(int cnt)
    {
        Debug.LogError(" CheckClickDouble  " + cnt);
        if (_lastClickIdx != cnt)
        {
            _lastClickIdx = cnt;
            _lastTime = Time.realtimeSinceStartup;
            return;
        }
        Debug.LogError(" CheckClickDouble111  " + (Time.realtimeSinceStartup - _lastTime));

        if (Time.realtimeSinceStartup - _lastTime < 0.3f)
        {
            SVNFileInfo fileInfo = FileInfosFilters[cnt];
            if (fileInfo.Object == null)
            {
                fileInfo.Object = AssetDatabase.LoadAssetAtPath(fileInfo.Name, typeof(Object));
            }
            Debug.LogError(" double  is  " + fileInfo.Object);
            SetDataSelect(fileInfo, !fileInfo.IsSelect);
            Selection.activeObject = fileInfo.Object;
        }
        _lastTime = Time.realtimeSinceStartup;

    }

    private void DrawBottom()
    {

        GUILayout.BeginHorizontal();
        GUILayout.Label("提交记录信息 ：", GUILayout.Width(80));
        _strCommitInfo = GUILayout.TextField(_strCommitInfo, GUILayout.Width(200));
        GUILayout.EndHorizontal();


        GUILayout.BeginHorizontal();
        if (GUILayout.Button("保存临时记录"))
        {
            SaveMapInfo();
        }
        if (GUILayout.Button("清空记录"))
        {
            ClearMapInfo();
        }
        if (GUILayout.Button("刷新列表"))
        {
            SaveMapInfo();
            RefreshFileInfos();
        }
        if (GUILayout.Button("添加文件"))
        {
            FileInfoAddToSVN();
        }
        if (GUILayout.Button("提交选中文件"))
        {
            FileInfoCommitToSVN();
        }

        GUILayout.EndHorizontal();


        GUILayout.BeginHorizontal();
        if (GUILayout.Button("CLS", GUILayout.Width(50)))
        {
            _logInfo = string.Empty;
        }
        GUI.color = Color.red;
        GUILayout.Label(_logInfo);

        GUILayout.EndHorizontal();

    }
    private void InitialConfit()
    {
        string strConfit = EditorConfitTools.GetSavePath(EnumLoadSettingPath.SVNConfigTools);
        if (string.IsNullOrEmpty(strConfit))
        {
            _isAutoRefresh = false;
        }
        else
        {
            _isAutoRefresh = strConfit == "1";
        }
    }

    /// <summary>
    /// 刷新 全部信息 
    /// </summary>
    private void RefreshFileInfos()
    {
        //初始化配置
        InitialConfit();
        //读取所有变更文件信息
        InitialAllFiles();
        //加载保存路径信息
        LoadMapInfo();
        //刷新筛选列表
        RefreshFilterList();

    }
    private void SetLogInfo(string strLog)
    {
        _logInfo = strLog;
    }


    private void InitialAllFiles()
    {
        //string str = ProcessCommandTools.ProcessCommand2("cmd.exe", "/c svn status Assets", false);
        string str = EditorCommandTools.ProcessCommand2("svn", " status Assets", false);

        FileInfosFilters.Clear();

        ReleaseFileInfo(AllFiles);

        string[] pathFileInfos = MFrameWork.Utility.Xml.ParseString<string>(str, new char[] { '\n' });
        for (int cnt = 0; cnt < pathFileInfos.Length; cnt++)
        {
            //"?""       "
            string strInfo = pathFileInfos[cnt];
            if (string.IsNullOrEmpty(strInfo))
            {
                continue;
            }
            string[] paths = strInfo.Split(new string[] { "       " }, System.StringSplitOptions.RemoveEmptyEntries);
            if (paths == null || paths.Length != 2)
            {
                continue;
            }
            string strName = paths[1].Trim().Replace("\\", "/");
            if (string.IsNullOrEmpty(strName))
            {
                Debug.LogError("  File Info Error " + paths[1]);
                continue;
            }
            AddSVNFileInfo(strName, paths[0]);
            //if (paths[0] == "?")
            //{
            //    List<string> files = new List<string>();
            //    GetDirectorFiles(strName, files);
            //    for (int i = 0; i < files.Count; i++)
            //    {
            //        AddSVNFileInfo(files[i], paths[0]);
            //    }
            //}
        }
    }
    private void AddSVNFileInfo(string path, string flag)
    {
        if (string.IsNullOrEmpty(path))
        {
            Debug.LogError(" Is null " + flag);
            return;
        }
        SVNFileInfo tempInfo = CreateFileInfos();
        tempInfo.SetName(path, flag);
        AllFiles.Add(tempInfo);
    }
    private void GetDirectorFiles(string path, List<string> listPaths)
    {
        if (Directory.Exists(path))
        {
            string[] diArr = Directory.GetDirectories(path);
            if (diArr.Length > 0)
            {
                for (int cnt = 0; cnt < diArr.Length; cnt++)
                {
                    string strNewPath = diArr[cnt].Replace("\\", "/");
                    listPaths.Add(strNewPath);
                    GetDirectorFiles(strNewPath, listPaths);
                }
            }
            string[] files = Directory.GetFiles(path);
            if (files.Length > 0)
            {
                for (int cnt = 0; cnt < files.Length; cnt++)
                {
                    string strNewPath = files[cnt].Replace("\\", "/");
                    listPaths.Add(strNewPath);
                }
            }
        }
        //else
        //{
        //    listPaths.Add(path);
        //}
    }

    private SVNFileInfo GetFileDataByName(string name)
    {
        return AllFiles.Find(item => item.Name == name);
    }
    private SVNFileInfo GetFileDataByContainName(string name)
    {
        return AllFiles.Find(item => item.Name.Contains(name));
    }


    private void RefreshFilterList(int sortType = 1)
    {
        FileInfosFilters.Clear();
        for (int cnt = 0; cnt < AllFiles.Count; cnt++)
        {
            if (IsValidFilter(AllFiles[cnt]))
            {
                FileInfosFilters.Add(AllFiles[cnt]);
            }
        }
        SortFileInfosFilters(sortType);

    }
    private void SortFileInfosFilters(int sortType = 1)
    {
        if (sortType == 1)
        {
            FileInfosFilters.Sort(SortData);

        }
        else if (sortType == 2)
        {
            EnumSVNFileState state = (EnumSVNFileState)_tempClickIndex;
            for (int cnt = 0; cnt < FileInfosFilters.Count; cnt++)
            {
                if (FileInfosFilters[cnt].State == state)
                {
                    FileInfosFilters[cnt].SetSortValue(FileInfosFilters[cnt].SortValue + 10);
                }
            }
            FileInfosFilters.Sort(SortDataValue);
            _tempClickIndex++;
            if (_tempClickIndex > 3)
            {
                ResetSortListValue();
            }
        }

        for (int cnt = 0; cnt < FileInfosFilters.Count; cnt++)
        {
            FileInfosFilters[cnt].Index = cnt;
        }
    }



    private int _tempClickIndex = 0;
    private void SortListByValue()
    {
        SortFileInfosFilters(2);
        SaveMapInfo();
    }
    /// <summary>
    /// 重置sort value
    /// </summary>
    private void ResetSortListValue()
    {
        for (int cnt = 0; cnt < AllFiles.Count; cnt++)
        {
            AllFiles[cnt].ResetSortValue();
        }
        _tempClickIndex = 0;
    }

    private void SetDataSelect(SVNFileInfo data, bool isSelect)
    {
        if (isSelect != data.IsSelect)
        {
            data.SetIsSelect(isSelect);
            string metaName = data.Name + ".meta";
            SVNFileInfo metaInfo = AllFiles.Find(item => item.Name == metaName);
            if (metaInfo != null)
            {
                metaInfo.SetIsSelect(isSelect);
            }
        }

    }
    private int SortData(SVNFileInfo x, SVNFileInfo y)
    {
        if (x.IsSelect != y.IsSelect)
        {
            return y.IsSelect.CompareTo(x.IsSelect);
        }
        return x.Name.CompareTo(y.Name);
    }
    private int SortDataValue(SVNFileInfo x, SVNFileInfo y)
    {
        if (x.IsSelect != y.IsSelect)
        {
            return y.IsSelect.CompareTo(x.IsSelect);
        }
        if (y.SortValue != x.SortValue)
        {
            return y.SortValue.CompareTo(x.SortValue);
        }
        return SortData(x, y);
    }
    /// <summary>
    /// 筛选
    /// </summary>
    private bool IsValidFilter(SVNFileInfo data)
    {
        bool isSelect = false;
        if (string.IsNullOrEmpty(_filterInfo.Trim()))
        {
            isSelect = true;
        }
        else if (_filterInfos != null && _filterInfos.Length > 1)
        {
            for (int cnt = 0; cnt < _filterInfos.Length; cnt++)
            {
                if (string.IsNullOrEmpty(_filterInfos[cnt]))
                {
                    continue;
                }
                if (data.Name.ToLower().Contains(_filterInfos[cnt].ToLower()))
                {
                    isSelect = true;
                }
            }
        }
        else if (data.Name.ToLower().Contains(_filterInfo.ToLower()))
        {
            isSelect = true;
        }
        if (_isReverseInput && isSelect)//反转 isSelect=true 返回false
        {
            return false;
        }
        if (!_isReverseInput && !isSelect)
        {
            return false;
        }
        isSelect = false;
        if (_filterType == EnumSVNFileState.All)
        {
            isSelect = true;
        }
        else if (_filterType == EnumSVNFileState.Select)
        {
            isSelect = data.IsSelect;
        }
        else if (_filterType == data.State)
        {
            isSelect = true;
        }
        return _isRevertFilter ? !isSelect : isSelect;
    }

    private bool FileInfoAddToSVN()
    {
        StringBuilder sbAdd = new StringBuilder();
        sbAdd.Append("/c svn add  ");
        bool hasAdd = false;
        for (int cnt = 0; cnt < FileInfosFilters.Count; cnt++)
        {
            if (FileInfosFilters[cnt].IsSelect)
            {
                if (FileInfosFilters[cnt].State == EnumSVNFileState.None)
                {
                    sbAdd.Append(FileInfosFilters[cnt].Name);
                    sbAdd.Append(" ");
                    hasAdd = true;
                }
            }
        }
        if (hasAdd)
        {
            Debug.Log(sbAdd.ToString());
            EditorCommandTools.ProcessCommand("cmd.exe", sbAdd.ToString());
        }
        return hasAdd;
    }
    private void FileInfoCommitToSVN()
    {
        if (_strCommitInfo.Length < 5)
        {
            Debug.LogError(" 提交记录信息太短 ");
            return;
        }
        bool hasAdd = FileInfoAddToSVN();
        StringBuilder sbMod = new StringBuilder();
        sbMod.Append("/K svn ci -m ");
        sbMod.Append("\"");
        sbMod.Append(_strCommitInfo);
        sbMod.Append("\" ");
        bool hasContent = false;
        for (int cnt = 0; cnt < FileInfosFilters.Count; cnt++)
        {
            if (FileInfosFilters[cnt].IsSelect)
            {
                sbMod.Append(FileInfosFilters[cnt].Name);
                sbMod.Append(" ");
                hasContent = true;
            }
        }
        if (hasContent || hasAdd)
        {
            Debug.Log(sbMod.ToString());
            EditorCommandTools.ProcessCommand("cmd.exe", sbMod.ToString());

            RefreshFileInfos();

        }
        else
        {
            SetLogInfo(" 未选择文件。");
        }
    }


    private List<SVNFileInfo> _cacheFiles = new List<SVNFileInfo>();
    private SVNFileInfo CreateFileInfos()
    {
        SVNFileInfo data = null;
        if (_cacheFiles.Count > 0)
        {
            data = _cacheFiles[0];
            data.Object = null;
            _cacheFiles.RemoveAt(0);
        }
        return new SVNFileInfo();
    }
    private void ReleaseFileInfo(List<SVNFileInfo> list)
    {
        _cacheFiles.AddRange(list);
        list.Clear();
    }


    private Dictionary<string, string> _mapSaveHash = new Dictionary<string, string>();
    private void LoadMapInfo()
    {
        LoadLocalMapHelper.InitialMap(GetSavePath(), ref _mapSaveHash);
        for (int cnt = 0; cnt < AllFiles.Count; cnt++)
        {
            if (_mapSaveHash.ContainsKey(AllFiles[cnt].Name))
            {
                AllFiles[cnt].SetIsSelect(true);
            }
        }
    }
    private void SaveMapInfo()
    {
        _mapSaveHash.Clear();
        for (int cnt = 0; cnt < AllFiles.Count; cnt++)
        {
            if (AllFiles[cnt].IsSelect)
            {
                _mapSaveHash.Add(AllFiles[cnt].Name, "1");
            }
        }
        LoadLocalMapHelper.SaveFile(GetSavePath(), _mapSaveHash);
    }
    private void ClearMapInfo()
    {
        for (int cnt = 0; cnt < AllFiles.Count; cnt++)
        {
            AllFiles[cnt].SetIsSelect(false);
        }
        _mapSaveHash.Clear();
        LoadLocalMapHelper.SaveFile(GetSavePath(), _mapSaveHash);
    }

    public static string GetSavePath()
    {
        return EditorConfitTools.GetSavePath(EnumLoadSettingPath.SVNTempSave) + "/SVNData.txt";
    }

    [MenuItem("MGTools/SVNTools")]
    private static void CopyAllComponent()
    {
        //创建窗口
        EditorWindow ew = EditorWindow.GetWindow(typeof(EditorSVNTools), false, "EditorSVNTools", true);
        EditorSVNTools myWindow = (EditorSVNTools)ew;
        myWindow.Show();//展示
    }
}
public enum EnumSVNFileState
{
    None = 0,
    Add,
    Mod,
    Del,
    Select,
    All,
}


