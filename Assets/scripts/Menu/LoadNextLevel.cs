using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class LoadNextLevel : MonoBehaviour {

    AsyncOperation ao;
    bool doneLoadingScene = false;
    public TextMesh Text;
    public Light light;
    public Light InternalLight;
    public GameObject Internals;

    bool IsLoading = false;
    float timer = 0;

    // Use this for initialization
    void Start () {

        Internals.SetActive(false);
        InternalLight.gameObject.SetActive(false);
	
	}
	
	// Update is called once per frame
	void Update () {

        

        if (Input.GetButton("Fire"))
        {
            if (!IsLoading)
            {
                Internals.SetActive(true);
                light.gameObject.SetActive(false);
                InternalLight.gameObject.SetActive(true);
                IsLoading = true;
            }   
        }


        if (timer > 2)
        {
            timer = 0;
            StartCoroutine(loadNextLevelAsync());
           

        }

        if (IsLoading)
        {

            timer += 1 * Time.deltaTime;

        }
       

        if (ao != null && !doneLoadingScene)
        {

            if (ao.isDone)
            {
               
                doneLoadingScene = true;

                timer += 1 * Time.deltaTime;

                
            }
            

         }

        if (ao != null) {
            

            if (ao.progress > 0.1f)
            {
                Text.text = ao.progress.ToString();
            }
           

        }
       

    }

   
    IEnumerator loadNextLevelAsync()
    {
        
        ao = SceneManager.LoadSceneAsync(1);
        
        yield return ao;
    }
}
