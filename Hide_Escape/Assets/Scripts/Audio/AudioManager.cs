using UnityEngine;
using System.Collections;

using System.Collections.Generic;

public class AudioManager : MonoBehaviour {

    public AudioSource SESource;   //


    private Dictionary<string, AudioClip> _seDic;

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);

        _seDic = new Dictionary<string, AudioClip>();

        object[] seList = Resources.LoadAll("Audio/SE");

        foreach(AudioClip se in seList)
        {
            _seDic[se.name] = se;
            Debug.Log(se.name);
        }

    }

	void Update () {
	
	}


    /// <summary>
    /// 指定したSEを流す
    /// </summary>
    /// <param name="name"></param>
    public void PlaySE(string name)
    {
        if (!_seDic.ContainsKey(name))
        {
            Debug.Log(name + "という名前のSEはありません");
            return;
        }

        SESource.PlayOneShot(_seDic[name] as AudioClip);
    }

}
