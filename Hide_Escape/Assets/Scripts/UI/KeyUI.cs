using UnityEngine;
using System.Collections;

public class KeyUI : MonoBehaviour {

    public GameObject key;

    public int keyInfo;
	// Use this for initialization
	void Start () {
        keyInfo = 0;
	}
	
	// Update is called once per frame
	void Update () {
	    for(int i = 0; i < GetComponent<DoorManager>().Key.Count; ++i)
        {
            if (GetComponent<DoorManager>().Key[i].obj.activeInHierarchy) { continue; }

            keyInfo = i;
            if (GetComponent<DoorManager>().IsOpen) { keyInfo++; }
        }
	}
}
