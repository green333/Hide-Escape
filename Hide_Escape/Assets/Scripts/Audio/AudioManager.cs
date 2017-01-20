using UnityEngine;
using System.Collections;

using System.Collections.Generic;

public class AudioManager : Singleton<AudioManager>
{



    public AudioSource SESource;   //
    public AudioSource BGMSource;   //

    [SerializeField]
    private string bgm_name;   //BGMの名前


    private Dictionary<string, AudioClip> _seDic;
    private Dictionary<string, AudioClip> _bgmDic;

    private float volume = 1;                        //音量

    public enum Audio
    {
        BGM = 0,
        SE,
    }


    private void Awake()
    {

        _seDic = new Dictionary<string, AudioClip>();

        object[] seList = Resources.LoadAll("Audio/SE");

        foreach(AudioClip se in seList)
        {
            _seDic[se.name] = se;
        }

        _bgmDic = new Dictionary<string, AudioClip>();

        object[] bgmList = Resources.LoadAll("Audio/BGM");

        foreach(AudioClip bgm in bgmList)
        {
            _bgmDic[bgm.name] = bgm;
        }


        PlayerBGM();
    }

	void Update () {

        if(BGMSource.isPlaying == false)
        {
            PlayerBGM();
        }
	}

    bool Play_End()
    {
        if(BGMSource.isPlaying == false)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    //-------------------------------------------

    //  ボリュームをセット

    //-------------------------------------------
    public void SetVolume(Audio audio, float volume)
    {
        switch (audio)
        {
            case Audio.BGM:
                BGMSource.volume = volume;
                break;

            case Audio.SE:
                SESource.volume = volume;
                break;
        }
    }


    //-------------------------------------------

    //  ピッチのセット

    //-------------------------------------------
    public void SetPitch(Audio audio, float pitch)
    {
        switch (audio)
        {
            case Audio.BGM:
                BGMSource.pitch = pitch;
                break;

            case Audio.SE:
                SESource.pitch = pitch;
                break;
        }

    }

    //-------------------------------------------

    //  ピッチの取得

    //-------------------------------------------
    float GetPitch()
    {
        return BGMSource.pitch;
    }

    //-------------------------------------------

    //  ボリュームを取得

    //-------------------------------------------
    float GetVolume()
    {
        return BGMSource.volume;
    }



    //-------------------------------------------

    //  一回だけSEを鳴らす

    //-------------------------------------------
    public void PlaySE(string name)
    {
        if (!_seDic.ContainsKey(name))
        {
            Debug.Log(name + "という名前のSEはありません");
            return;
        }


        SESource.PlayOneShot(_seDic[name] as AudioClip, volume);
        
    }


    public void PlaySE(string name,bool loop)
    {
        if (!_seDic.ContainsKey(name))
        {
            Debug.Log(name + "という名前のSEはありません");
            return;
        }

        if (!SESource.isPlaying && loop)
        {
            SESource.PlayOneShot(_seDic[name] as AudioClip, volume);
        }
    }



    public void PlayerBGM()
    {
        if (!_bgmDic.ContainsKey(bgm_name))
        {
            Debug.Log(bgm_name + "という名前のBGMはありません");
            return;
        }

        BGMSource.PlayOneShot(_bgmDic[bgm_name] as AudioClip,volume);
    }




}
