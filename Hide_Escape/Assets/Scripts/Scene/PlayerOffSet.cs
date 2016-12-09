using UnityEngine;
using System.Collections;

public class PlayerOffSet : MonoBehaviour {

    static MoveSceneFloor.TriggerType type;     //  移動前のフロアのトリガー保存用
    
	// Use this for initialization
	void Start () {
        switch(type)
        {
            case MoveSceneFloor.TriggerType.NON:
                break;
            case MoveSceneFloor.TriggerType.NEXT_TRIGGER:
                NextTriggerOffSet();
                break;
            case MoveSceneFloor.TriggerType.RETURN_TRIGGER:
                ReturnTriggerOffSet();
                break;

        }
	
	}
	
	// Update is called once per frame
	void Update () {

        //  トリガータイプが何か指定されたらtypeにその値を指定する
	    if(GetComponent<MoveSceneFloor>().Type != MoveSceneFloor.TriggerType.NON)
        {
            type = GetComponent<MoveSceneFloor>().Type;
        }
	}

    //  次のシーンに行くときのトリガーでのプレイヤーのセット
    private void NextTriggerOffSet()
    {
        //  現在のシーンの名前でプレイヤーの設置場所を決める
        switch(Application.loadedLevelName)
        {
            case "First_Floor":
                SetPosition(new Vector3(0.06f, 1.07f, 3.36f));
                break;
            case "Second_Floor":
                SetPosition(new Vector3(4.46f, 1.07f, -9.49f));
                break;
            case "Thard_Floor":
                SetPosition(new Vector3(4.07f, 1.22f, -2.99f));
                break;

        }
    }

    //  前のシーンに戻るときのトリガーでのプレイヤーのセット
    private void ReturnTriggerOffSet()
    {
        //  現在のシーンの名前でプレイヤーの設置場所を決める
        switch (Application.loadedLevelName)
        {
            case "First_Floor":
                SetPosition(new Vector3(0.06f, 1.07f, -9.45f));
                break;
            case "Second_Floor":
                SetPosition(new Vector3(15.02f, 1.07f, -7.52f));
                break;
            case "Thard_Floor":
                SetPosition(new Vector3(-6.17f, 1.22f, 2.76f));
                break;

        }
    }

    private void SetPosition(Vector3 pos)
    {
        GetComponent<P_Player>().transform.position = pos;
    }
}
