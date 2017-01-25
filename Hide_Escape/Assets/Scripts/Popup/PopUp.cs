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

    public  string text; //これに代入された文字列を表示

    public Vector2 Window_Senter;

    private int Timer;

    public bool TimeEraseMode = false;


    GUIStyle customGuiStyle;


    void OnGUI()
    {

       // SetText("qawsedrftgyhuj");
       //GUI.backgroundColor = Color.clear;
        GUI.Box(new Rect(Window_Senter.x-text.Length*2, Window_Senter.y,text.Length*12, 25), " "+text);
       // GUI.Box(new Rect(Window_Senter.x , Window_Senter.y / 2, text.Length * 12, 25), " " + text,customGuiStyle);


    }
    void Update()
    {
        //ウィンドウサイズ更新
        Window_Senter.x = Screen.width/2 ;
        Window_Senter.y = Screen.height/2 ;



        if (TimeEraseMode)
        {

            if (Timer < 0)
            {
                Deactivate();

            }
            else Timer--;

        }

    }

    public bool SetText(string txt)
    {
        
       // text.Remove(0);
        text = txt;

      //  SetTextSize(1);
        return true;
    }

    public void SetTextSize(int size) {
        customGuiStyle.fontSize = size;
        return;
    }


    
    public void Removetext() {
        text.Remove(0);
    }

    public void Activate() {
        this.enabled = true; ;
    }

    public void Activate(int EraseTime)
    {
        Timer = EraseTime;
        TimeEraseMode = true;
        this.enabled = true;
    }

    public void Deactivate() {
        this.enabled = false;
        TimeEraseMode = false; ;
    }

}
