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
    private RGB_Color color;    //カラー(Red,Green,Blue,Alpha)

    [SerializeField]
    private string filename;   //切り替え先のシーン

    [SerializeField]
    private float alpha_lower;  //不透明度を減らす

    private enum Title
    {
        WAIT = 0,       //ボタン押さない限り待機
        PRESS_ENTER,    //ゲームスタート演出
        GAME_SCREEN,    //ゲーム画面へ
    }

    private Title transition;   //ゲーム遷移


    //---------------------------

    //  初期化

    //---------------------------
    public override void Start () {
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
        if (Input.GetButtonDown("Botton_A"))
        {
            transition = Title.PRESS_ENTER;
        }
    }


    //---------------------------

    //  キーを押した後の処理

    //---------------------------
    void Title_Process()
    {

        for (int i = 0; i < image.Length; i++)
        {
            image[i].color = new Color(color.red, color.green, color.blue, color.alpha);
        }


        color.alpha -= 0.01f;


        if (color.alpha < 0)
        {
            transition = Title.GAME_SCREEN;
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
