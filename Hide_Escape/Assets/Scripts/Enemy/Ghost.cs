using UnityEngine;
using System.Collections;

public class Ghost : MonoBehaviour {

    [SerializeField]
    LightSystem light;

    public Transform target;
    public float Speed;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

        if(light.GetIsLighting())
        {
            Go_Away();
        }
        else
        {
            Approach();
        }
	
	}

    public void Approach()
    {
        Vector3 p_pos = target.transform.position;
        Vector3 direction = p_pos - transform.position;
        transform.position = transform.position + (direction * Speed * Time.deltaTime);
        transform.LookAt(p_pos);

    }

    public void Go_Away()
    {
        Vector3 p_pos = target.transform.position;
        Vector3 direction = p_pos - transform.position;
        transform.position = transform.position - (direction * Speed * Time.deltaTime);
        transform.LookAt(-p_pos);

    }
}
