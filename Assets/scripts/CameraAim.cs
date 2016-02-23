using UnityEngine;
using System.Collections;

public class CameraAim : MonoBehaviour {

    public Camera playerCamera;
    public Vector3 hitTransform;
    public GameObject hitCube;
    public GameObject Player;
	// Use this for initialization
	void Start () {

        playerCamera = GetComponent<Camera>();
        hitTransform = new Vector3();
    }
	
	// Update is called once per frame
	void Update () {

        //Vector3 Worldhit = playerCamera.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, playerCamera.farClipPlane));
        Ray ray = playerCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hit;

        Debug.DrawRay(ray.origin, ray.direction * 10, Color.red);

        if (Physics.Raycast(ray, out hit))
        {

            if (hit.transform.gameObject.tag == "FloorRay")
            {

                hitTransform = new Vector3(hit.point.x, hit.point.y, hit.point.z);

            }


        }

       // make the object appear at the hit point.
        hitCube.transform.position = hitTransform;
      

    }
}
