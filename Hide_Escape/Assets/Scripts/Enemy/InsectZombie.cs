using UnityEngine;
using System.Collections;

public class InsectZombie : MonoBehaviour {

    [SerializeField]
    public Transform p_player;

    [SerializeField]
    public LightSystem light;

    [SerializeField]
    public NavMeshAgent agent;

    [SerializeField]
    public Animator animator = null;

    Vector3 p_pos;
  
	// Use this for initialization
	void Start () {
        animator.SetBool("A_Flag", false);

	}
	
	// Update is called once per frame
    void FixedUpdate()
    {
        if(light.GetIsLighting())
        {
            Approach();
        }
        else
        {
            animator.SetBool("A_Flag", false);
            agent.SetDestination(transform.position);
        }

	}

    public void Approach()
    {
        animator.SetBool("A_Flag", true);

        p_pos = p_player.position;
        p_pos.y = 0;
        agent.SetDestination(p_pos);
        transform.LookAt(p_pos);
    }

}
