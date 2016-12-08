using UnityEngine;
using System.Collections;

using UnityEngine.UI;

[System.Serializable]
class RGB_Color
{
    public float red;   //赤色

    public float green; //緑色

    public float blue;  //青色

    public float alpha; //不透明度

}



public class Text_Production : MonoBehaviour {

    [SerializeField]
    private Image[] text_pro;    //演出用のテキスト

    private int now_text = 0;    //現在のテキスト

    enum Pro_Status
    {
        ONE_TEXT_ALPHA_UP = 0,   //一文字ずつテキストの不透明度上げる
        ALL_TEXT_ALPHA_DOWN,     //すべてのテキストの不透明度を下げる
        ALL_TEXT_UP,             //すべてのテキストの不透明度を上げる
        PRO_STATUS_MAX,          //
    }

    private Pro_Status pro;      //演出

    [SerializeField]
    private RGB_Color color;     //色（赤、緑、青、不透明度）

    [SerializeField]
    private float alpha_spd;   //不透明度を下げる速度

    //-------------------------------------

    //  初期化

    //-------------------------------------
    void Start () {
        for(int i = 0; i < text_pro.Length; i++)
        {
            text_pro[i].color = new Color(0.0f, 0.0f, 0.0f, 0.0f);
        }
	
	}

    //-------------------------------------

    //  更新

    //-------------------------------------
    void Update () {

        switch (pro)
        {
            case Pro_Status.ONE_TEXT_ALPHA_UP:
                Next_Text();
                break;

            case Pro_Status.ALL_TEXT_ALPHA_DOWN:
                All_Text_Pro();
                break;

            case Pro_Status.ALL_TEXT_UP:
                break;
        }

	}


    //-------------------------------------

    //  テキストの不透明度を下げる

    //-------------------------------------
    void Text_Alpha_Lower(int text)
    {
        if(text_pro[text].color.a > 1.0f)
        {
            now_text++;
            color.alpha = 0.0f;
        }
        else
        {
            text_pro[text].color = new Color(color.red, color.green, color.blue, color.alpha);
            color.alpha += alpha_spd;
        }
    }

    //---------------------------------

    //  次のテキストへ

    //---------------------------------
    void Next_Text()
    {
        if(now_text < text_pro.Length)
        {
            Text_Alpha_Lower(now_text);
        }
        else
        {
            color.alpha = 1.0f;
            pro = Pro_Status.ALL_TEXT_ALPHA_DOWN;
        }
    }

    int step = 0;
    void All_Text_Pro()
    {
        for(int i = 0; i < text_pro.Length; i++)
        {
            text_pro[i].color = new Color(color.red, color.green, color.blue, color.alpha);
        }


        switch (step)
        {
            case 0:
                if(color.alpha > 0.5f)
                {
                    color.alpha -= alpha_spd;
                }
                else
                {
                    step = 1;  
                }
            break;

            case 1:
                color.alpha += alpha_spd;
                break;

        }
    }

}
