using UnityEngine;
using System.Collections;

public class SimpleAgent : MonoBehaviour {

    public Transform Target;
    NavMeshAgent agent;
    Rigidbody rb;
    Collider col;
    public Transform Floor;

    float timer = 0;

	// Use this for initialization
	void Start () {

        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        rb = GetComponent<Rigidbody>();
        Physics.IgnoreCollision(Floor.GetComponent<Collider>(), GetComponent<Collider>());
    }
	
	// Update is called once per frame
	void Update () {
        

        


        if (!agent.enabled )
        {

            timer += 1.0f * Time.deltaTime;
            
           

            if (timer > 4 && agent.transform.position.y < 1.5f)
            {
                agent.enabled = true;
                rb.isKinematic = true;
                rb.constraints = RigidbodyConstraints.FreezeRotation;
                agent.updatePosition = true;
                agent.SetDestination(Target.transform.position);
                timer = 0;
            }

        }
	
	}



    void FixedUpdate()
    {

        


        if (agent.enabled)
        {

            Vector3 targetLookAt = Target.transform.position - transform.position;

            Quaternion AILook = Quaternion.LookRotation(targetLookAt);


            rb.MoveRotation(AILook);
            agent.SetDestination(Target.transform.position);
        }

        


    }


    void OnCollisionEnter(Collision col)
    {

        if (agent.enabled == false && col.gameObject.tag == "Floor")
        {
            agent.enabled = true;

        }



    }
}
