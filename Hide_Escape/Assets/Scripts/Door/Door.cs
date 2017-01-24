using UnityEngine;
using System.Collections;

public class Door : MonoBehaviour {


    private Animator _animator = null;


	// Use this for initialization
	void Start () {
        _animator = gameObject.GetComponent<Animator>();
        _animator.SetBool("tregger", false);

	}
	
	// Update is called once per frame
	void Update () {
	
	}
    public void Open_Door() {

        _animator.SetBool("tregger", true);
    
    }

}
