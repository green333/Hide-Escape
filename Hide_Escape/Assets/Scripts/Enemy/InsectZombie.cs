using UnityEngine;
using System.Collections;

public class InsectZombie : MonoBehaviour {


    [SerializeField]
    BaceEnemy bace;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        bace.Patrol();
	
	}
}
