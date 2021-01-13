using UnityEditor;
using UnityEngine;
using System.IO;
using UnityEditor.SceneManagement;

public class EasyEditor : Editor
{
    [MenuItem("Custom/GotoMain")]
    public static void GotoSetup()
    {
        EditorSceneManager.OpenScene(Application.dataPath + "/Scenes/Main.unity");
    }

    [MenuItem("Custom/GotoUIEditor")]
    public static void GotoUIEditor()
    {
        EditorSceneManager.OpenScene(Application.dataPath + "/Scenes/UIEditor.unity");
    }
    [MenuItem("Custom/GotoTestScene")]
    public static void GotoTestScene()
    {
        EditorSceneManager.OpenScene(Application.dataPath + "/Scenes/TestScene.unity");
    }
    [MenuItem("Custom/GoHome")]
    public static void GotoHome()
    {
        EditorSceneManager.OpenScene(Application.dataPath + "/Scenes/Home.unity");
    }
    [MenuItem("Custom/GotoMap1")]
    public static void GotoMap1()
    {
        EditorSceneManager.OpenScene(Application.dataPath + "/Scenes/Map1.unity");
    }
    [MenuItem("Custom/GotoMap2")]
    public static void GotoMap2()
    {
        EditorSceneManager.OpenScene(Application.dataPath + "/Scenes/Map2.unity");
    }
    //把配置文件放入Resources目录下
    [MenuItem("Custom/ConfigToResources")]
    public static void ConfigToResources()
    {
        Debug.Log("还未设置配置文件");
        /*//找到目标路径和源路径

        var srcPath = Application.dataPath + "/../Config/";
        var dstPath = Application.dataPath + "/Resources/Config/";
        //递归清空目录
        if (Directory.Exists(dstPath))
        {
            Directory.Delete(dstPath, true);
            Directory.CreateDirectory(dstPath);
        }
        else
        {
            Directory.CreateDirectory(dstPath);
        }
        
        
        //把源路径内的所有文件，复制到目标路径，并添加扩展名
        foreach(var filePath in Directory.GetFiles(srcPath))
        {
            var fileName = filePath.Substring(filePath.LastIndexOf('/') + 1);
            File.Copy(filePath, dstPath+fileName + ".bytes",true);
        }
        //强制刷新引擎客户端
        AssetDatabase.Refresh();

        Debug.Log("配置文件复制完毕！");*/
    }
}
