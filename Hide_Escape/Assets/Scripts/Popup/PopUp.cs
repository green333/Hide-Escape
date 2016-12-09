using UnityEngine;
using System.Collections;

public class PopUp : MonoBehaviour
{


    public Vector2 RenderPos; //この値を元に描画
    public string text; //これに代入された文字列を表示


    void OnGUI()
    {
        GUI.Box(new Rect(10, 10, 100, 90), "TEST!!!!!!!!!!!!!!!!!!!");
    }


}
