using UnityEngine;
using System.Collections;

public class PopUp : MonoBehaviour
{
    /*仕様
     *
     * SetTextで設定されたテキストを表示するクラスです
     * 表示する際はActivate()を
     * 非表示にする際はDeactivate()を　呼び出してください
     * 
     * textは新しいものを設定するたびに　初期化を行い　その後設定します
    */

    private string text; //これに代入された文字列を表示

    public Vector2 Window_Senter;
    void OnGUI()
    {

       // SetText("qawsedrftgyhuj");
       //GUI.backgroundColor = Color.clear;
        GUI.Box(new Rect(Window_Senter.x/4, Window_Senter.y/2,text.Length*12, 25), " "+text);
    }
    void Update()
    {
        //ウィンドウサイズ更新
        Window_Senter.x = Screen.width ;
        Window_Senter.y = Screen.height ;

    }

    public bool SetText(string txt)
    {
       // text.Remove(0);
        text = txt;
        return true;
    }
    public void Removetext() {
        text.Remove(0);
    }

    public void Activate() {
        this.enabled = true; ;
    }
    public void Deactivate() {
        this.enabled = false;
    }

}
