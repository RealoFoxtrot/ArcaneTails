using UnityEngine;
using System.Collections;

public class PushBackForce : MonoBehaviour {

    private Vector3 boomPosition;
    private float boomMultiplier;
    public float boomRadius;

    Vector3 explosionPos;
    Collider[] colliders;
    GameObject[] Enemies;
    NavMeshAgent agent;


    // Use this for initialization
    void Start() {

        explosionPos = transform.position;

        colliders = Physics.OverlapSphere(explosionPos, boomRadius);

    }

    // Update is called once per frame
    void Update() {




    }

    void FixedUpdate()
    {

        if (Input.GetButton("Fire"))
        {

            foreach (Collider hit in Physics.OverlapSphere(transform.position, boomRadius))
            {
                //Rigidbody rbOther = hit.GetComponent<Rigidbody>();
                //print(hit.attachedRigidbody.name);

                if (hit.attachedRigidbody != null && hit.gameObject.tag == "Attacker")
                {

                    print(hit.attachedRigidbody.name);
                    agent = hit.gameObject.GetComponent<NavMeshAgent>();

                    if (agent.enabled)
                    {
                        agent.enabled = false;
                        agent.updateRotation = false;
                        agent.updatePosition = false;
                        hit.attachedRigidbody.isKinematic = false;
                        hit.attachedRigidbody.constraints = RigidbodyConstraints.None;
                    }
                    
                    
                    hit.attachedRigidbody.AddExplosionForce(1000, explosionPos, boomRadius, 2.0f);



                }


            }

        }



    }
}
