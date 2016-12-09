using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SoundManager : MonoBehaviour
{
    //SEチャンネル数
    const int SE_CHANNEL = 4;

    //サウンド種別
    enum eType
    {
        Bgm, //BGM
        Se,  //SE
    }

    //シングルトン
    static SoundManager singleton = null;
	public static SoundManager GetInstance()
    {
        return singleton ?? (singleton=new SoundManager());
    }

    //サウンド再生のためのゲームオブジェクト
    GameObject obj = null;
    //サウンドリソース
    AudioSource sourceBGM = null; //BGM
    AudioSource sourceSEDefaulut = null; //SE(デフォルト)
    AudioSource[] sourceSeArray; //SE(チャンネル)
    //BGMにアクセスするためのテーブル
    Dictionary<string, Data> poolBGM = new Dictionary<string, Data>();
    //SEにアクセスするためのテーブル
    Dictionary<string, Data> poolSE = new Dictionary<string, Data>();

    //保持するデータ
    class Data
    {
        //アクセス用キー
        public string Key;
        //リソーズ名
        public string ResName;
        //AudioClip
        public AudioClip Clip;

        //コンストラクタ
        public Data(string key,string res)
        {
            Key = key;
            ResName = "Sound/" + res;
            //AudioClipの取得
            Clip = Resources.Load(ResName) as AudioClip;

        }

    }

    public SoundManager()
    {
        //チャンネル確保
        sourceSeArray = new AudioSource[SE_CHANNEL];
    }

    //AudioSourceを取得する
    AudioSource GetAudioSource(eType type,int channel=-1)
    {
        if(obj==null)
        {
            //GameObjectがなければ作る
            obj = new GameObject("Sound");
            //破壊しないようにする
            GameObject.DontDestroyOnLoad(obj);
            //AudioSourceを作成
            sourceBGM = obj.AddComponent<AudioSource>();
            sourceSEDefaulut = obj.AddComponent<AudioSource>();
            for(int i=0;i<SE_CHANNEL;i++)
            {
                sourceSeArray[i] = obj.AddComponent<AudioSource>();
            }
        }

        if (type == eType.Bgm)
        {
            //BGM
            return sourceBGM;
        }
        else
        {
            //SE
            if(0<=channel && channel<SE_CHANNEL)
            {
                return sourceSeArray[channel];
            }
            else
            {
                return sourceSEDefaulut;
            }
        }
    }

    //サウンドのロード
    // ※Resources/Soundフォルダに配置すること
    public static void LoadBgm(string key,string resName)
    {
        GetInstance()._LoadBgm(key, resName);
    }
    public static void LoadSe(string key,string resName)
    {
        GetInstance()._LoadSe(key, resName);
    }

    void _LoadBgm(string key,string resName)
    {
        if(poolBGM.ContainsKey(key))
        {
            //すでに登録済みなのでいったん消す
            poolBGM.Remove(key);
        }
        poolBGM.Add(key, new Data(key, resName));
    }

    void _LoadSe(string key,string resName)
    {
        if(poolSE.Remove(key))
        {
            //すでに登録済みなのでいったん消す
            poolSE.Remove(key);
        }
        poolSE.Add(key, new Data(key, resName));
    }


    //BGMの再生
    //※事前にLoadBgmでロードしておくこと
    public static bool PlayBgm(string key)
    {
        return GetInstance()._PlayBgm(key);
    }
    bool _PlayBgm(string key)
    {
        if (poolBGM.ContainsKey(key) == false)
        {
            //対応するキーがない
            return false;
        }

        //いったん止める
        _StopBgm();

        //リソースの取得
        var _data = poolBGM[key];

        //再生
        var source = GetAudioSource(eType.Bgm);
        source.loop = true;
        source.clip = _data.Clip;
        source.Play();

        return true;

    }

    //BGMの停止
    public static bool StopBgm()
    {
        return GetInstance()._StopBgm();
    }

    bool _StopBgm()
    {
        GetAudioSource(eType.Bgm).Stop();

        return true;
    }

    // Use this for initialization
	void Start () {
	
	} 
	
	// Update is called once per frame
	void Update () {
	
	}
}
