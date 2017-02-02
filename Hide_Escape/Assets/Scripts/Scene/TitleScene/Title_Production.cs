using UnityEngine;
using System.Collections;

using UnityEngine.UI;

//------------------------------------------------------------

//  タイトルの演出

//------------------------------------------------------------
public class Title_Production : Scene {

    [SerializeField]
    private Image[] image;     //画像
    
    [SerializeField]
    private RGB_Color[] color = new RGB_Color[2];    //カラー(Red,Green,Blue,Alpha)

    [SerializeField]
    private string filename;   //切り替え先のシーン

    [SerializeField]
    //private float alpha_lower;  //不透明度を減らす

    private enum Title
    {
        WAIT = 0,       //ボタン押さない限り待機
        PRESS_ENTER,    //ゲームスタート演出
        GAME_SCREEN,    //ゲーム画面へ
    }

    private Title transition;   //ゲーム遷移

    [SerializeField]
    private Image game_start_fade;

    //---------------------------

    //  初期化

    //---------------------------
    public override void Start () {
        game_start_fade.color = new Color(0, 0, 0, 0);
    }


    //---------------------------

    //  更新

    //---------------------------
    public override void Update () {

        switch (transition)
        {
            case Title.WAIT:
                Press_Enter();
                break;

            case Title.PRESS_ENTER:
                Title_Process();
                    End_Fade();
                break;

            case Title.GAME_SCREEN:
                Game_Screen();
                break;
        }

	}


    //---------------------------

    //  キーを押すための関数

    //---------------------------
    void Press_Enter()
    {
        if (Input.GetButtonDown("Botton_B") || Input.GetKeyDown(KeyCode.Space) || Input.GetButtonDown("Start Botton"))
        {
            transition = Title.PRESS_ENTER;
            AudioManager.Instance.PlaySE("Discovery02");
        }
    }

    void End_Fade()
    {
        game_start_fade.color = new Color(color[1].red, color[1].green, color[1].blue, color[1].alpha);

        
        if(color[1].alpha > 1)
        {
            transition = Title.GAME_SCREEN;
        }
        else
        {
            color[1].alpha += 0.01f;
        }
    }

    //---------------------------

    //  キーを押した後の処理

    //---------------------------
    void Title_Process()
    {

        for (int i = 0; i < image.Length; i++)
        {
            image[i].color = new Color(color[0].red, color[0].green, color[0].blue, color[0].alpha);
        }


        if (color[0].alpha < 0)
        {
            //transition = Title.GAME_SCREEN;
        }
        else
        {
            color[0].alpha -= 0.01f;
        }

    }


    //---------------------------

    //  演出が終わった後のシーン切り替え

    //---------------------------
    void Game_Screen()
    {
        ChangeScene(filename);
    }




}
