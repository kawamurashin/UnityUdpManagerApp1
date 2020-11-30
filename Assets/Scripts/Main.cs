using Controller;
using UnityEngine;
public class Main : MonoBehaviour
{
    public static string AppType = "App1";
    public static int ReceivePort = 10011;
    public static int SendPort = 10012;
    /*
    もう一つのアプリ用
    public static string AppType = "App2";
    public static int ReceivePort = 10012;
    public static int SendPort = 10011;
    */
    public void Start()
    {
        var obj = new GameObject("ControllerManager");
        obj.AddComponent<ControllerManager>();
        obj.transform.parent = transform;
    }
}
