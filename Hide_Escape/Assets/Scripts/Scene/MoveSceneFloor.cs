using UnityEngine;
using System.Collections;

public class MoveSceneFloor : Scene {

    public string nextSceneName = "";   //  次のシーンの名前
    public string previousSceneName = "";  //  前のシーンの名前

    public Screenfade fade;             //  フェード
    public int fadeSpeed;               //  フェードの速さ

    public enum TriggerType
    {
        NON = 0,            //  なんのトリガーも引いてない
        RETURN_TRIGGER,     //  前のステージに戻る
        NEXT_TRIGGER,       //  次のステージに進む
        GAMEOVER_TRIGGER,   //  ゲームオーバー
        GAMECLERE_TRIGGER,  //  ゲームクリア
        TRIGGER_MAX,        //  トリガー最大値
    }

    private TriggerType type;

    static string sceneName;

    public string SceneName
    {
        get { return sceneName; }
    }

    public TriggerType Type
    {
        get { return type; }
    }

    // Use this for initialization
    void Start()
    {
        type = TriggerType.NON;
    }

    // Update is called once per frame
    void Update()
    {
        //  フラグチェック
        FlgChack();

        //  フェードが終了したらシーン移動
        if (fade.IsEnd())
        {
            TriggerChack();
        }
    }

    //  どのシーンに飛ぶかチェック
    void TriggerChack()
    {
        switch (type)
        {
            case TriggerType.RETURN_TRIGGER:
                ChangeScene(previousSceneName);
                break;
            case TriggerType.NEXT_TRIGGER:
                ChangeScene(nextSceneName);
                break;

            case TriggerType.GAMEOVER_TRIGGER:
                sceneName = Application.loadedLevelName;
                ChangeScene("GameOver");
                break;
                
            case TriggerType.GAMECLERE_TRIGGER:
                ChangeScene("GameClear");
                break;
        }
    }

    //  どのフラグが立っているかチェック
    void FlgChack()
    {

        if (GetComponent<P_Player>().IS_NEXTSTAGE)
        {

            fade.SetFade(Screenfade.FadeType.Type.OUT, fadeSpeed);
            type = TriggerType.NEXT_TRIGGER;
            GetComponent<DoorManager>().SaveActiveKeyData();
            GetComponent<P_Player>().IS_NEXTSTAGE = false;
        }
        else if (GetComponent<P_Player>().IS_PREVSTAGE)
        {
            fade.SetFade(Screenfade.FadeType.Type.OUT, fadeSpeed);
            type = TriggerType.RETURN_TRIGGER;
            GetComponent<P_Player>().IS_PREVSTAGE = false;
        }
        else if(GetComponent<P_Player>().IS_GAMEOVER)
        {
            fade.SetFade(Screenfade.FadeType.Type.OUT, fadeSpeed);
            type = TriggerType.GAMEOVER_TRIGGER;
            GetComponent<P_Player>().IS_GAMEOVER = false;
        }
        else if(GetComponent<P_Player>().IS_GAMECLEAR)
        {
            fade.SetFade(Screenfade.FadeType.Type.OUT, fadeSpeed, 1.0f, 1.0f, 1.0f);
            type = TriggerType.GAMECLERE_TRIGGER;
            GetComponent<P_Player>().IS_GAMECLEAR = false;

        }
    }

    //void OnTriggerEnter(Collider collider)
    //{
    //    if (collider.gameObject.tag == "NextTrigger")
    //    {
    //        fade.SetFade(Screenfade.FadeType.Type.OUT, fadeSpeed);
    //        type = TriggerType.NEXT_TRIGGER;
    //    }
    //    else if (collider.gameObject.tag == "ReturnTrigger")
    //    {
    //        fade.SetFade(Screenfade.FadeType.Type.OUT, fadeSpeed);
    //        type = TriggerType.RETURN_TRIGGER;
    //    }
    //}
}
