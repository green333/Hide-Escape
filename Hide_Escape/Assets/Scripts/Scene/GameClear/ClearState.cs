using UnityEngine;
using System.Collections;

using UnityEngine.UI;

public class ClearState : MonoBehaviour {


    enum Clear_State
    {
        START_FADE = 0,
        WAKE_UP,           //ホワイトアウトして目覚める
        EYES_LOOK_SIDEWAYS,    //目線が横を向く
        TARGET_LOOP_ZOOM_IN,   //ターゲットに寄る
        CLEAR_TEXT_IND,                   //クリアテキスト表示
        NEXT_TITLE,            //タイトルへ
    }

    [SerializeField]
    private Clear_State state;         //クリアステート

    private float angle;    //回転

    private float spd = 30.0f;   //回転速度

    private float moved;  //移動値


    [SerializeField]
    private GameObject clear_text_obj = null;   //クリアテキストのオブジェクト

    [SerializeField]
    private Image fade;     //フェード

    [SerializeField]
    private RGB_Color color;

    void Awake()
    {
        fade.color = new Color(color.red, color.green, color.blue, color.alpha);
    }

    //-------------------------------------------

    //  初期化

    //-------------------------------------------
    void Start () {

    }



    //-------------------------------------------

    //  更新

    //-------------------------------------------
    void Update () {


        switch(state)
        {
            case Clear_State.START_FADE:
                Invoke("Wake_Fade", 2);
                break;

            case Clear_State.WAKE_UP:
                WakeUp();
                break;

            case Clear_State.EYES_LOOK_SIDEWAYS:
                Look_Sideway();
                break;

            case Clear_State.TARGET_LOOP_ZOOM_IN:
                Zoom_In();
                break;

            case Clear_State.CLEAR_TEXT_IND:
                Text_Ind();
                break;

            case Clear_State.NEXT_TITLE:
                Next_Scene();
                break;
        }


    }

    void Wake_Fade()
    {
        state = Clear_State.WAKE_UP;
    }


    //-------------------------------------------

    //  ステートをセット

    //-------------------------------------------
    void SetState(Clear_State c_state)
    {
        state = c_state;
    }


    void Zoom_State()
    {
        SetState(Clear_State.TARGET_LOOP_ZOOM_IN);
    }



    //-------------------------------------------

    //  起き上がる処理

    //-------------------------------------------
    void WakeUp()
    {

        transform.eulerAngles = new Vector3(transform.rotation.x, 
                                            transform.rotation.y,
                                            transform.rotation.z + angle * spd);

       
        if (transform.eulerAngles.z > 90)
        {
            angle = 0;
            SetState(Clear_State.EYES_LOOK_SIDEWAYS);
        }
        else
        {
            angle += Time.deltaTime;
        }
    }


    //-------------------------------------------

    //  横を向く

    //-------------------------------------------
    void Look_Sideway()
    {
        transform.eulerAngles = new Vector3(transform.rotation.x,
                                            transform.rotation.y + angle * spd,
                                            transform.rotation.z + 90);

        if(transform.eulerAngles.y > 90)
        {
            Invoke("Zoom_State", 1);
            //SetState(Clear_State.TARGET_LOOP_ZOOM_IN);
        }
        else
        {
            angle += Time.deltaTime;
        }
    }


    //-------------------------------------------

    //  注目する（そのオブジェクトに寄る
    
    //-------------------------------------------
    void Zoom_In()
    {

        transform.position += new Vector3(0, 0, moved);

        if(transform.position.z > -1.77929)
        {
            SetState(Clear_State.CLEAR_TEXT_IND);
        }
        else
        {
            moved = Time.deltaTime;
        }
    }

    float alpha = 1.0f;
    private bool oneshot = false;

    //-----------------------------------

    //  クリア演出終了

    //-----------------------------------
    void Text_Ind()
    {
        clear_text_obj.GetComponent<MeshRenderer>().material.color = new Color(1.0f, 1.0f, 1.0f, alpha);

        if(alpha > 0)
        {
            alpha -= 0.05f;
        }

        Invoke("Fade",1);
    }

    void Fade()
    {
        color.alpha += 0.01f;
        fade.color = new Color(color.red, color.green, color.blue, color.alpha);

        if(color.alpha > 1)
        {
            SetState(Clear_State.NEXT_TITLE);
        }
    }

    void Next_Scene()
    {
        Application.LoadLevel("Title");
    }
}
