using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;

//================================================================================================================
//  @brief  読み込むデータと生成するオブジェクトまとめたやつ
//================================================================================================================
public class ReadInfo
{
    private Vector3 _pos = new Vector3(0.0f, 0.0f, 0.0f);
    private Vector3 _angle = new Vector3(0.0f, 0.0f, 0.0f);
    private Vector3 _size = new Vector3(0.0f, 0.0f, 0.0f);

    private GameObject _obj = null;
    private Animator _animator = null;
    private Collider _collider = null;

    private string _keyFlag = null;

    public Vector3 pos { get { return _pos; } set { _pos = value; } }
    public Vector3 angle { get { return _angle; } set { _angle = value; } }
    public Vector3 size { get { return _size; } set { _size = value; } }

    public GameObject obj { get { return _obj; } set { _obj = value; } }
    public Animator animator { get { return _animator; } set { _animator = value; } }
    public Collider collider { get { return _collider; } set { _collider = value; } }

    public string keyFlag { get { return _keyFlag; } set { _keyFlag = value; } }

    public NavMeshObstacle nav;
}

//================================================================================================================
//  @brief  ドアと鍵の処理
//  @note   csvデータを読み込んで生成する
//================================================================================================================
public class DoorManager : MonoBehaviour
{
    private char[] KUGIRI = { ',', '\n' };  //  区切り文字
    private const int ELEMENT = 9;          //  読み込む要素の数
    private const int CAPACITY = 10;        //  キャパシティ
    public TextAsset DoorFile;              //  ドアの情報のファイル
    public TextAsset KeyFile;               //  鍵の情報のファイル
    private bool _isOpen;                   //  ドアを開けるフラグ

    private List<ReadInfo> door = new List<ReadInfo>(CAPACITY);
    private List<ReadInfo> key = new List<ReadInfo>(CAPACITY);

    public bool IsOpen
    {
        get { return _isOpen; }
    }

    public List<ReadInfo> Key
    {
        get { return key; }
    }

    //----------------------------------------------------------------------------
    //  @brief  初期化
    //----------------------------------------------------------------------------
    private void Start()
    {
        SetIsOpenDoor(false);
        ReadFile(ref DoorFile, ref door);
        ReadFile(ref KeyFile, ref key);
        GenerateDoor();
        GenerateKey();

        LoadActiveKeyData("First_Floor");
        LoadActiveKeyData("Second_Floor");
        LoadActiveKeyData("Third_Floor");
        LoadActiveKeyData("Return_Second_Floor");
        LoadActiveKeyData("Return_First_Floor");
    }

    //----------------------------------------------------------------------------
    //  @brief  更新
    //----------------------------------------------------------------------------
    private void Update()
    {
        //  デバッグ用にPキーでシーン移動させてます
        if (Input.GetKeyDown(KeyCode.P))
        {
            SaveActiveKeyData();
            Application.LoadLevel("Second_Floor");
            return;
        }

        OpenDoor();
    }

    //----------------------------------------------------------------------------
    //  @brief  アプリケーションを終了するときに呼ばれる
    //----------------------------------------------------------------------------
    private void OnApplicationQuit()
    {
        ClearActiveKeyData();
    }

    //----------------------------------------------------------------------------
    //  @brief  キーの取得情報をロードする
    //  @param[in]  sceneName シーンの名前
    //----------------------------------------------------------------------------
    private void LoadActiveKeyData(string sceneName)
    {
        for (int i = 0; i < key.Count; i++)
        {
            if (Application.loadedLevelName != sceneName) { return; }

            string load = PlayerPrefs.GetString(sceneName + i);
            if (load == "TRUE")
            {
                key[i].obj.SetActive(true);
            }
            else if (load == "FALSE")
            {
                key[i].obj.SetActive(false);
            }
        }
    }

    //----------------------------------------------------------------------------
    //  @brief  キーの取得情報をセーブする
    //----------------------------------------------------------------------------
    public void SaveActiveKeyData()
    {
        for (int i = 0; i < key.Count; i++)
        {
            SaveActiveKeyData(i, "First_Floor");
            SaveActiveKeyData(i, "Second_Floor");
            SaveActiveKeyData(i, "Third_Floor");
            SaveActiveKeyData(i, "Return_Second_Floor");
            SaveActiveKeyData(i, "Return_First_Floor");
        }
    }
    private void SaveActiveKeyData(int n, string sceneName)
    {
        if (Application.loadedLevelName != sceneName) { return; }

        if (key[n].obj.activeInHierarchy)
        {
            PlayerPrefs.SetString(sceneName + n, "TRUE");
        } else {
            PlayerPrefs.SetString(sceneName + n, "FALSE");
        }
    }

    //----------------------------------------------------------------------------
    //  @brief  キーの取得情報を初期化する
    //----------------------------------------------------------------------------
    private void ClearActiveKeyData()
    {
        for (int i = 0; i < key.Count; i++)
        {
            PlayerPrefs.DeleteKey("First_Floor" + i);
            PlayerPrefs.DeleteKey("Second_Floor" + i);
            PlayerPrefs.DeleteKey("Third_Floor" + i);
            PlayerPrefs.DeleteKey("Return_Second_Floor" + i);
            PlayerPrefs.DeleteKey("Return_First_Floor" + i);
        }
    }

    //----------------------------------------------------------------------------
    //  @brief  ドアを開ける
    //----------------------------------------------------------------------------
    private void OpenDoor()
    {
        for (int i = 0; i < key.Count; i++)
        {
            if (key[i].obj.activeInHierarchy) { continue; }

            if (_isOpen
                || Input.GetKeyDown(KeyCode.KeypadEnter) || Input.GetButtonDown("Botton_B"))
            {
                door[i].animator.Play("Open");
                door[i].collider.isTrigger = true;
                door[i].nav.enabled = false;
            }
        }
    }

    //----------------------------------------------------------------------------
    //  @brief  衝突判定
    //  @param[in]  collision   オブジェクトが保持しているコリジョンデータ
    //----------------------------------------------------------------------------
    private void OnCollisionEnter(Collision collision)
    {
        for (int i = 0; i < key.Count; i++)
        {
            if (collision.gameObject.name == "key" + i)
            {
                key[i].obj.SetActive(false);
            }
        }
    }

    //----------------------------------------------------------------------------
    //  @brief      ファイル読み込み
    //  @param[in]  csvFile     CSVFileを管理するクラス
    //  @param[in]  info        読み込むデータのリスト
    //----------------------------------------------------------------------------
    private void ReadFile(ref TextAsset csvFile, ref List<ReadInfo> info)
    {
        string tmp = csvFile.ToString();    //  全データ
        string[] data = tmp.Split(KUGIRI);  //  セルごとのデータ

        for (int i = 0; i < data.Length - 1; i += ELEMENT)
        {
            if (data[i].StartsWith("P") || data[i].StartsWith("S")) { continue; }
            if (i >= data.Length - 1) { break; }

            info.Add(new ReadInfo());

            info[info.Count - 1].pos = new Vector3(
                float.Parse(data[i]),
                float.Parse(data[i + 1]),
                float.Parse(data[i + 2])
                );

            info[info.Count - 1].angle = new Vector3(
                float.Parse(data[i + 3]),
                float.Parse(data[i + 4]),
                float.Parse(data[i + 5])
                );

            info[info.Count - 1].size = new Vector3(
                float.Parse(data[i + 6]),
                float.Parse(data[i + 7]),
                float.Parse(data[i + 8])
                );
        }
    }

    //----------------------------------------------------------------------------
    //  @brief  ドアオブジェクトを生成する
    //----------------------------------------------------------------------------
    private void GenerateDoor()
    {
        for (int i = 0; i < door.Count; i++)
        {
            door[i].obj =
                (GameObject)Instantiate(
                Resources.Load("Prefab/door"),
                door[i].pos,
                Quaternion.identity);

            door[i].animator = door[i].obj.GetComponent<Animator>();
            door[i].collider = door[i].obj.GetComponent<Collider>();
            door[i].nav = door[i].obj.GetComponent<NavMeshObstacle>();

            door[i].obj.name = "door" + i;
            door[i].obj.transform.localEulerAngles = door[i].angle;
            door[i].obj.transform.localScale = door[i].size;
        }

    }
    
    //----------------------------------------------------------------------------
    //  @brief  キーオブジェクトを生成する
    //----------------------------------------------------------------------------
    private void GenerateKey()
    {
        for (int i = 0; i < key.Count; i++)
        {
            key[i].obj =
                (GameObject)Instantiate(
                Resources.Load("Prefab/key"),
                key[i].pos,
                Quaternion.identity);

            key[i].animator = null;
            key[i].collider = key[i].obj.GetComponent<Collider>();

            key[i].obj.name = "key" + i;
            key[i].obj.transform.localEulerAngles = key[i].angle;
            key[i].obj.transform.localScale = key[i].size;
        }
    }

    //----------------------------------------------------------------------------
    //  @brief  ドアを開けるフラグのセッター
    //  @param[in]  value   セットする値(bool)
    //----------------------------------------------------------------------------
    public void SetIsOpenDoor(bool value)
    {
        _isOpen = value;
    }
}
