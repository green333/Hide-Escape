using UnityEngine;
using System.Collections;

public class Ghost : MonoBehaviour {

    [SerializeField]
    LightSystem light;

    [SerializeField]
    private GameObject[] obj = null;

    [SerializeField]
    public Animator animator = null;

    public Vector3[] wp;

    public int wpState;
    
    public Transform target;

    public float Speed;

    public float limitDistatnce = 100f;
    public int size;

    Vector3 p_pos;
    Vector3 direction;


    enum State
    {
        Patrol,
        Go_Away,
        Approach
    };

    [SerializeField]
    private State state = State.Patrol; 

	// Use this for initialization
	void Start () {
       for(int i=0;i<obj.Length;i++)
       {
           wp[i] = obj[i].transform.position;
           transform.position = wp[i];
       }

       animator.SetBool("W_Flag", true);
	
	}
	
	// Update is called once per frame
	void FixedUpdate() {

        DistanceDetermination();
        switch (state)
        {
            case State.Patrol:
                animator.SetBool("W_Flag", true);
                animator.SetBool("C_Flag", false);
                WaitPoint();
                break;
            case State.Go_Away:
                animator.SetBool("W_Flag", false);
                animator.SetBool("C_Flag", true);

                Go_Away();
                break;
            case State.Approach:
                animator.SetBool("W_Flag", false);
                animator.SetBool("C_Flag", true);
                Approach();
                AudioManager.Instance.PlaySE("enemy_to_chase", true);
                break;
        }

	}


    public bool DistanceDetermination()
    {
        p_pos = target.position;
        direction = p_pos - transform.position;
        float distance = direction.sqrMagnitude;

        if (distance < limitDistatnce)
        {
            return true;
        }
        
            return false;
    }
    public void WaitPoint()
    {
        if (Vector3.Distance(transform.position, wp[wpState]) <= 0.5f)
        {
            wpState++;
            if (wpState >= size)
            {
                wpState = 0;

            }
        }
        else
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(wp[wpState] - transform.position), 1.5f);
            transform.position += transform.forward * Speed*Time.deltaTime;
        }
        if(!DistanceDetermination())
        {
            state = State.Patrol;
            return;
        }

        if(light.GetIsLighting())
        {
            state = State.Go_Away;
        }
        else
        {
            state = State.Approach;
        }
    }

    public void Approach()
    {
        if (!DistanceDetermination())
        {
            state = State.Patrol;
            return;
        }

        p_pos = target.position;
        direction = p_pos - transform.position;
        transform.position += transform.forward * Speed * Time.deltaTime;
        transform.LookAt(p_pos);
        
        if(light.GetIsLighting())
        {
            state = State.Go_Away;
        }
    }

    public void Go_Away()
    {
            if (!DistanceDetermination())
            {
                state = State.Patrol;
                return;
            }

            p_pos = -target.position;
            direction = p_pos - transform.position;
            transform.position += transform.forward * Speed * Time.deltaTime;
            transform.LookAt(p_pos);

            if (!light.GetIsLighting())
            {
                state = State.Approach;
            }
    }
}
