using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Teleport : MonoBehaviour {
    public string DestinationScene;

    // Use this for initialization
    void Start () {

    }
	
	// Update is called once per frame
	void Update () {
    }

    void OnTriggerEnter(Collider Other)
    {

        SceneManager.LoadScene(DestinationScene);
    }
}
