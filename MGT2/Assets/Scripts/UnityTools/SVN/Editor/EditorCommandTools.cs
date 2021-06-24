using System.IO;

public class EditorCommandTools
{

    public static string ProcessCommand2(string command, string argument, bool useShellExecute = true)
    {
        string strOutPut = string.Empty;
        System.Diagnostics.ProcessStartInfo info = new System.Diagnostics.ProcessStartInfo(command);
        info.Arguments = argument;
        //表示是否启动新的窗口来执行这个脚本
        info.CreateNoWindow = false;
        info.ErrorDialog = true;
        //将此属性设置为 false 使您能够重定向输入流、输出流和错误流。
        info.UseShellExecute = useShellExecute;

        if (useShellExecute)
        {
            info.RedirectStandardOutput = false;
            info.RedirectStandardError = false;
            info.RedirectStandardInput = false;
        }
        else
        {
            info.RedirectStandardOutput = true;
            info.RedirectStandardError = true;
            info.RedirectStandardInput = true;
            info.StandardOutputEncoding = System.Text.UTF8Encoding.Default;
            info.StandardErrorEncoding = System.Text.UTF8Encoding.Default;
        }
        // System.Diagnostics.Process process = System.Diagnostics.Process.Start(info);
        System.Diagnostics.Process process = new System.Diagnostics.Process();
        process.StartInfo = info;
        process.Start();
        if (!useShellExecute)
        {
            StreamReader reader = process.StandardOutput;
            string line = reader.ReadLine();
            strOutPut += line + "\n";

            while (!reader.EndOfStream)
            {
                line = reader.ReadLine();
                strOutPut += line + "\n";
            }
        }
        process.WaitForExit();
        process.Close();
        return strOutPut;
    }


    public static string ProcessCommand(string command, string argument, bool useShellExecute = true)
    {
        string strOutPut = string.Empty;
        System.Diagnostics.ProcessStartInfo info = new System.Diagnostics.ProcessStartInfo(command);
        info.Arguments = argument;
        //表示是否启动新的窗口来执行这个脚本
        info.CreateNoWindow = false;
        info.ErrorDialog = true;
        //将此属性设置为 false 使您能够重定向输入流、输出流和错误流。
        info.UseShellExecute = useShellExecute;

        if (useShellExecute)
        {
            info.RedirectStandardOutput = false;
            info.RedirectStandardError = false;
            info.RedirectStandardInput = false;
        }
        else
        {
            info.RedirectStandardOutput = true;
            info.RedirectStandardError = true;
            info.RedirectStandardInput = true;
            info.StandardOutputEncoding = System.Text.UTF8Encoding.Default;
            info.StandardErrorEncoding = System.Text.UTF8Encoding.Default;
        }
        // System.Diagnostics.Process process = System.Diagnostics.Process.Start(info);
        System.Diagnostics.Process process = new System.Diagnostics.Process();
        process.StartInfo = info;
        process.Start();
        if (!useShellExecute)
        {
            strOutPut = process.StandardOutput.ReadToEnd();
        }
        process.WaitForExit();
        process.Close();
        return strOutPut;
    }

}


//private void FileInfoCommitToSVN()
//{

//    string strPath = string.Empty;
//    System.IO.DirectoryInfo parent = System.IO.Directory.GetParent(Application.dataPath);
//    string projectPath = parent.ToString() + "/Assets";
//    if (_strCommitInfo.Length < 5)
//    {
//        Debug.LogError(" 提交记录信息太短 ");
//        return;
//    }

//    bool hasAdd = FileInfoAddToSVN();

//    StringBuilder sbMod = new StringBuilder();

//    sbMod.Append("/K svn ci -m ");
//    sbMod.Append("\"");
//    sbMod.Append(_strCommitInfo);
//    sbMod.Append("\" ");
//    bool hasContent = false;
//    for (int cnt = 0; cnt < FileInfosFilters.Count; cnt++)
//    {
//        if (FileInfosFilters[cnt].IsSelect)
//        {

//            sbMod.Append(FileInfosFilters[cnt].Name);
//            sbMod.Append(" ");
//            hasContent = true;
//        }
//    }
//    if (hasContent || hasAdd)
//    {
//        Debug.LogError(sbMod.ToString());
//        ProcessCommand("cmd.exe", sbMod.ToString());
//    }
//    else
//    {
//        SetLogInfo(" 未选择文件。");
//    }
//}

//private bool FileInfoAddToSVN()
//{
//    StringBuilder sbAdd = new StringBuilder();
//    sbAdd.Append("/c svn add  ");
//    bool hasAdd = false;
//    for (int cnt = 0; cnt < FileInfosFilters.Count; cnt++)
//    {
//        if (FileInfosFilters[cnt].IsSelect)
//        {
//            if (FileInfosFilters[cnt].State == EnumSVNFileState.Add)
//            {
//                sbAdd.Append(FileInfosFilters[cnt].Name);
//                sbAdd.Append(" ");
//                hasAdd = true;
//            }
//        }
//    }
//    if (hasAdd)
//    {
//        Debug.LogError(sbAdd.ToString());
//        ProcessCommand("cmd.exe", sbAdd.ToString());
//    }
//    return hasAdd;
//}

//private bool IsSelectType(EnumSVNFilter state, EnumSVNFilter res)
//{
//    int index = 1 << (int)state;
//    // 获取所有选中的枚举值
//    int eventTypeResult = (int)res;
//    // 按位 与
//    if ((eventTypeResult & index) == index)
//    {
//        return true;
//    }
//    return false;
//}