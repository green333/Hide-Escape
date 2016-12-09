using UnityEngine;
using System.Collections;

public class FirstFloorScene : Scene
{

    public string nextSceneName = "";   //  次のシーンの名前

    public Screenfade fade;             //  フェード
    public int fadeSpeed;               //  フェードの速さ

    // Use this for initialization
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(fade.IsEnd())
        {
            ChangeScene(nextSceneName);
        }
    }

    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            fade.SetFade(Screenfade.FadeType.Type.OUT, fadeSpeed);
        }
    }
}
