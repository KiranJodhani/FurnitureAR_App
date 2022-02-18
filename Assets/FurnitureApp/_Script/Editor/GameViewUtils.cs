using UnityEditor;
using System.Reflection;

public class GameViewUtils
{
    static object gameViewSizesInstance;
    static MethodInfo getGroup;
    private static int screenIndex;
    private static int gameViewProfilesCount;

    static GameViewUtils()
    {
        var sizesType = typeof(Editor).Assembly.GetType("UnityEditor.GameViewSizes");
        var singleType = typeof(ScriptableSingleton<>).MakeGenericType(sizesType);
        var instanceProp = singleType.GetProperty("instance");
        getGroup = sizesType.GetMethod("GetGroup");
        gameViewSizesInstance = instanceProp.GetValue(null, null);
    }

    private enum GameViewSizeType
    {
        AspectRatio, FixedResolution
    }

    public static void SetSize(int index)
    {
        var gvWndType = typeof(Editor).Assembly.GetType("UnityEditor.GameView");
        var gvWnd = EditorWindow.GetWindow(gvWndType);

        //var SizeSelectionCallback = gvWndType.GetMethod("SizeSelectionCallback",
        //    BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
        //SizeSelectionCallback.Invoke(gvWnd, new object[] { index, null });

        var selectedSizeIndexProp = gvWndType.GetProperty("selectedSizeIndex",
                BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
        selectedSizeIndexProp.SetValue(gvWnd, index, null);
    }

    private static void GetViewListSize()
    {
        var group = GetGroup(GetCurrentGroupType());
        var getDisplayTexts = group.GetType().GetMethod("GetDisplayTexts");
        gameViewProfilesCount = (getDisplayTexts.Invoke(group, null) as string[]).Length;
    }

    static object GetGroup(GameViewSizeGroupType type)
    {
        return getGroup.Invoke(gameViewSizesInstance, new object[] { (int)type });
    }

    public static GameViewSizeGroupType GetCurrentGroupType()
    {
        var getCurrentGroupTypeProp = gameViewSizesInstance.GetType().GetProperty("currentGroupType");
        return (GameViewSizeGroupType)(int)getCurrentGroupTypeProp.GetValue(gameViewSizesInstance, null);
    }

    [MenuItem("Tools/GameViewSize/Previous %F1")]
    private static void SetPreviousGameViewSize()
    {
        GetViewListSize();
        if (screenIndex - 1 >= 0)
        {
            screenIndex -= 1;
        }
        else
        {
            screenIndex = gameViewProfilesCount - 1;
        }

        SetSize(screenIndex);
    }

    [MenuItem("Tools/GameViewSize/Next  %F2")]
    private static void SetNextGameViewSize()
    {
        GetViewListSize();
        if (screenIndex + 1 < gameViewProfilesCount)
        {
            screenIndex += 1;
        }
        else
        {
            screenIndex = 0;
        }

        SetSize(screenIndex);
    }
}