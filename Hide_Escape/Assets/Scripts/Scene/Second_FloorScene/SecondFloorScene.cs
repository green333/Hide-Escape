using UnityEngine;
using System.Collections;

public class SecondFloorScene : Scene {

    public string nextSceneName = "";   //  次のシーンの名前
    public string previousSceneName = "";  //  前のシーンの名前

    public Screenfade fade;             //  フェード
    public int fadeSpeed;               //  フェードの速さ

    enum TriggerType
    {
        NON = 0,            //  なんのトリガーも引いてない
        RETURN_TRIGGER,     //  前のステージに戻る
        NEXT_TRIGGER,       //  次のステージに進む
        TRIGGER_MAX,        //  トリガー最大値
    }

    private TriggerType type;

    // Use this for initialization
    void Start()
    {
        type = TriggerType.NON;
    }

    // Update is called once per frame
    void Update()
    {
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
        }
    }

    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == "NextTrigger")
        {
            fade.SetFade(Screenfade.FadeType.Type.OUT, fadeSpeed);
            type = TriggerType.NEXT_TRIGGER;
        }
        else if(collider.gameObject.tag == "ReturnTrigger")
        {
            fade.SetFade(Screenfade.FadeType.Type.OUT, fadeSpeed);
            type = TriggerType.RETURN_TRIGGER;
        }
    }

}
