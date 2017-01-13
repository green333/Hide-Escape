using UnityEngine;
using System.Collections;

public class Map_Player : MonoBehaviour {

    [SerializeField]
    public GameObject Player;

	// Use this for initialization
	void Start () {
       
	}
	
	// Update is called once per frame
	void Update () {

        Vector3 newPosition = transform.position;
        newPosition.x = Player.transform.position.x;
        newPosition.y = Player.transform.position.y + 2.862f;
        newPosition.z = Player.transform.position.z;
        transform.position = Vector3.Lerp(transform.position, newPosition, 5.0f * Time.deltaTime);
        
        transform.eulerAngles = new Vector3(90f, 0f, -Player.transform.eulerAngles.y);

	}
}
