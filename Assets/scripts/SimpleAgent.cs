using UnityEngine;
using System.Collections;

public class SimpleAgent : MonoBehaviour {

    public Transform Target;
    NavMeshAgent agent;
    Rigidbody rb;

    float timer = 0;

	// Use this for initialization
	void Start () {

        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        rb = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
        

        


        if (!agent.enabled)
        {

            timer += 1.0f * Time.deltaTime;

            if (timer > 4)
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

        Vector3 targetLookAt = Target.transform.position - transform.position;

        Quaternion AILook = Quaternion.LookRotation(targetLookAt);


        if (agent.enabled)
        {
            rb.MoveRotation(AILook);
            agent.SetDestination(Target.transform.position);
        }



    }
}
