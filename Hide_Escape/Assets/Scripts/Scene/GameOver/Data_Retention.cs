using UnityEngine;
using System.Collections;

public class Data_Retention : MonoBehaviour {



    public string sceneName;

    public string SceneName
    {
        get { return sceneName; }
    }


    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }



	// Use this for initialization
	void Start () {
    }

    // Update is called once per frame
    void Update () {
        if (Application.loadedLevelName != "GameOver")
        {
            sceneName = Application.loadedLevelName;
        }
    }

}
