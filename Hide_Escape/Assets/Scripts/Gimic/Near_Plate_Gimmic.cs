using UnityEngine;
using System.Collections;

public class Near_Plate_Gimmic : MonoBehaviour {

    private bool shakePlateflg;

    public bool ShakePlateflg
    {
        get { return shakePlateflg; }
    }
	// Use this for initialization
	void Start () {
        shakePlateflg = false;
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Space))
        {

            shakePlateflg = true;
        }

        //shakePlateflg = GetComponentInChildren<Plate_Gimmick>().GetComponentInChildren<Plate_Gimmick>().Shakeflg;
        
	}

    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            shakePlateflg = true;
        }
    }

    void OnTriggerExit(Collider collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            shakePlateflg = false;
        }
    }
    
}
