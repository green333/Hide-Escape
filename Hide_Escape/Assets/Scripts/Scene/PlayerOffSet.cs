using UnityEngine;
using System.Collections;

public class PlayerOffSet : MonoBehaviour {

    static MoveSceneFloor.TriggerType type;     //  移動前のフロアのトリガー保存用
    
    public int flg = 0;

    public string a;

	// Use this for initialization
	void Start () {
        switch(type)
        {
            case MoveSceneFloor.TriggerType.NON:
                break;
            case MoveSceneFloor.TriggerType.NEXT_TRIGGER:
                NextTriggerOffSet();
                flg = 1;
                break;
            case MoveSceneFloor.TriggerType.RETURN_TRIGGER:
                ReturnTriggerOffSet();
                flg = 2;
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
        a = "いいいいい";
        //  現在のシーンの名前でプレイヤーの設置場所を決める
        switch(Application.loadedLevelName)
        {
            case "First_Floor":
                SetPosition(new Vector3(0.06f, 1.07f, 3.36f));
                a = Application.loadedLevelName;
                break;
            case "Second_Floor":
                SetPosition(new Vector3(4.46f, 1.07f, -9.49f));
                a = Application.loadedLevelName;
                break;
            case "Third_Floor":
                SetPosition(new Vector3(4.07f, 1.22f, -2.99f));
                a = Application.loadedLevelName;
                break;
            case "Return_Second_Floor":
                SetPosition(new Vector3(-15.0f, 1.07f, 0.18f));
                a = Application.loadedLevelName;
                break;
            case "Return_First_Floor":
                SetPosition(new Vector3(12.22f, 1.07f, -1.45f));
                a = Application.loadedLevelName;
                break;
        }
    }

    //  前のシーンに戻るときのトリガーでのプレイヤーのセット
    private void ReturnTriggerOffSet()
    {
        a = "ああああ";
        //  現在のシーンの名前でプレイヤーの設置場所を決める
        switch (Application.loadedLevelName)
        {
            case "First_Floor":
                SetPosition(new Vector3(0.06f, 1.07f, -9.45f));
                a = Application.loadedLevelName;
                break;
            case "Second_Floor":
                SetPosition(new Vector3(15.02f, 1.07f, -7.52f));
                a = Application.loadedLevelName;
                break;
            case "Third_Floor":
                SetPosition(new Vector3(-6.17f, 1.22f, 2.76f));
                a = Application.loadedLevelName;
                break;
            case "Return_Second_Floor":
                SetPosition(new Vector3(15.73f, 1.07f, 3.87f));
                a = Application.loadedLevelName;
                break;
            case "Return_First_Floor":
                SetPosition(new Vector3(15.73f, 1.07f, 3.87f));
                a = Application.loadedLevelName;
                break;
        }
    }

    private void SetPosition(Vector3 pos)
    {
        transform.position = pos;
    }
}
