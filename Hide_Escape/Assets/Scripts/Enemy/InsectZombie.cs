using UnityEngine;
using System.Collections;

public class InsectZombie : MonoBehaviour {

    [SerializeField]
    public Transform p_player;

    [SerializeField]
    public LightSystem light;

    [SerializeField]
    public NavMeshAgent agent;

    public float Speed;

    Vector3 p_pos;
    Vector3 direction;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
        if(light.GetIsLighting())
        {
            Approach();

        }
        else
        {

        }

	}

    public void Approach()
    {
       
        p_pos = p_player.position;
        agent.SetDestination(p_pos);
        direction = p_pos - transform.position;
        transform.position = transform.position + (direction * Speed * Time.deltaTime);
        transform.LookAt(p_pos);

    }

}
