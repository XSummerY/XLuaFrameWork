using System.Collections;
using System.Collections.Generic;
public enum GameMode
{
    EditorMode,
    PackgeBundle,
    UpdateMode,
}
public class AppConst
{
    public const string BundleExtension = ".ab";
    public const string FileListName = "filelist.txt";
    public static GameMode GameMode = GameMode.EditorMode;
}
