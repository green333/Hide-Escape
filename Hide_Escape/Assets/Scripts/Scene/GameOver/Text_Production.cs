using UnityEngine;
using System.Collections;

using UnityEngine.UI;


[System.Serializable]
class RGB_Color
{
    public float red = 0;   //赤色

    public float green = 0; //緑色

    public float blue = 0;  //青色

    public float alpha = 0; //不透明度

}



public class Text_Production : MonoBehaviour {

    [SerializeField]
    private Image[] text_pro;    //演出用のテキスト

    [SerializeField]
    private Image[] select_on;    //選択中

    private int now_text = 0;    //現在のテキスト

    enum Pro_Status
    {
        ONE_TEXT_ALPHA_UP = 0,   //一文字ずつテキストの不透明度上げる
        ALL_TEXT_ALPHA_DOWN,     //すべてのテキストの不透明度を下げる   
        ALL_TEXT_ALPHA_UP,       //すべてのテキストの不透明度を上げる
        ALL_TEXT_UP,             //すべてのテキストの位置を上げる
        OPERABLE,                //操作可能になる
    }

    enum State_Step
    {
        MOVED = 0,     //移動のみ
        COLOR_INV,     //コンティニューや終了を表示
    }

    enum Command
    {
        CONTINUE = 0,     //コンティニュー
        END,              //ゲーム終了
    }

    [SerializeField]
    private Pro_Status pro;      //演出

    private Command com;

    private State_Step fin_step;      //最後の演出ステップ（移動 -> 表示)


    [SerializeField]
    private RGB_Color color;     //色（赤、緑、青、不透明度）

    [SerializeField]
    private float alpha_spd;   //不透明度を下げる速度



    bool enterflg;


    [SerializeField]
    float up_spd;

    private Vector2 pos;

    private Vector2[] p;

    public const int MAX = 500;





    void Awake()
    {
        for (int i = 0; i < select_on.Length; i++)
        {
            select_on[i].enabled = false;
        }
    }

    //-------------------------------------

    //  初期化

    //-------------------------------------
    void Start() {
        for (int i = 0; i < text_pro.Length; i++)
        {
            text_pro[i].color = new Color(0.0f, 0.0f, 0.0f, 0.0f);
        }



    }

    //-------------------------------------

    //  更新

    //-------------------------------------
    void Update() {

        switch (pro)
        {
            case Pro_Status.ONE_TEXT_ALPHA_UP:
                One_Text_Pro();                          //一つづつのテキスト演出
                break;

            case Pro_Status.ALL_TEXT_ALPHA_DOWN:
                All_Text_Lower();                       //全体でのテキスト演出
                break;

            case Pro_Status.ALL_TEXT_ALPHA_UP:
                All_Text_Higher();
                break;

            case Pro_Status.ALL_TEXT_UP:
                All_Text_FinPro();
                break;

            case Pro_Status.OPERABLE:
                Operable();
                break;

        }

    }


    //---------------------------------

    //  操作（コンティニューor終了)

    //---------------------------------
    void Operable()
    {


        //if (Input.GetKeyDown(KeyCode.LeftArrow) && !enterflg && com == Command.END)
        //{
        //    Indication(Command.CONTINUE,true);
        //    com = Command.CONTINUE;
        //}

        //if (Input.GetKeyDown(KeyCode.RightArrow) && !enterflg && com == Command.CONTINUE)
        //{
        //    Indication(Command.END,true);
        //    com = Command.END;
        //}

        float X = Input.GetAxis("Left Joystick Horizontal");
        Debug.Log(X);

        if (X < -0.5f && !enterflg && com == Command.END)
        {
            Indication(Command.CONTINUE, true);
            com = Command.CONTINUE;
        }

        if(X > 0.5f && !enterflg && com == Command.CONTINUE)
        {
            Indication(Command.END, true);
            com = Command.END;
        }

        //if (Input.GetKeyDown(KeyCode.KeypadEnter))
        if (Input.GetButtonUp("Botton_A"))
        {
            AudioManager.Instance.PlaySE("Decision");
            enterflg = true;
        }

        Decision(enterflg);    //決定後
    }

    void Decision(bool decision)
    {
        if (decision)
        {
            switch (com)
            {
                case Command.CONTINUE:
                    Application.LoadLevel(GetComponent<MoveSceneFloor>().SceneName);
                    break;

                case Command.END:
                    Application.LoadLevel("Title");
                    break;
            }
        }
    }

    //---------------------------------

    //  表示するイメージの選択

    //  引数：表示するコマンドを入れる

    //---------------------------------
    void Indication(Command com,bool se_ring)
    {
        if (se_ring)
        {
            AudioManager.Instance.PlaySE("Select");
        }

        switch (com)
        {
            case Command.CONTINUE:
                select_on[(int)Command.END].enabled = false;
                select_on[(int)Command.CONTINUE].enabled = true;
                break;

            case Command.END:
                select_on[(int)Command.CONTINUE].enabled = false;
                select_on[(int)Command.END].enabled = true;
                break;
        }

    }


    //---------------------------------

    //  次のテキストへ

    //---------------------------------
    void One_Text_Pro()
    {
        if (now_text < text_pro.Length - 2)
        {
            Text_Alpha_Lower(now_text);
        }
        else
        {
            color.alpha = 1.0f;
            pro = Pro_Status.ALL_TEXT_ALPHA_DOWN;
        }
    }

    //-------------------------------------

    //  テキストの不透明度を下げる

    //-------------------------------------
    void Text_Alpha_Lower(int text)
    {
        if (text_pro[text].color.a > 1.0f)
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

    //  全体のテキストでの演出

    //---------------------------------
    void All_Text_Lower()
    {
        TextColor();


        if (color.alpha > 0.5f)
        {
            color.alpha -= alpha_spd;
        }
        else
        {
            pro = Pro_Status.ALL_TEXT_ALPHA_UP;
        }

    }

    void TextColor()
    {
        for (int i = 0; i < text_pro.Length - 2; i++)
        {
            text_pro[i].color = new Color(color.red, color.green, color.blue, color.alpha);
        }
    }

    //---------------------------------

    //  次のテキストへ

    //---------------------------------
    void All_Text_Higher()
    {
        TextColor();

        color.alpha += alpha_spd * 0.2f;

        if(color.alpha  > 1)
        {
            pro = Pro_Status.ALL_TEXT_UP;
        }

    }


    void All_Text_FinPro()
    {
        switch (fin_step)
        {
            case State_Step.MOVED:              //移動
                pos = transform.position;

                pos.y += up_spd;

                transform.position = pos;

                if (pos.y > MAX)
                {
                    fin_step = State_Step.COLOR_INV;
                }
                break;

            case State_Step.COLOR_INV:         //移動が終われば表示
                color.alpha += alpha_spd;

                for (int i = text_pro.Length - 2; i < text_pro.Length ; i++)
                {
                    text_pro[i].color = new Color(color.red, color.green, color.blue, color.alpha);

                    if(text_pro[i].color.a > 1)
                    {
                        Indication(Command.CONTINUE,false);
                        pro = Pro_Status.OPERABLE;
                    }
                }
                break;
        }
    }


}

