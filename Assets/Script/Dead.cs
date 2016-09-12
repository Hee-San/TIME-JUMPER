using UnityEngine;
using System.Collections;

public class Dead : MonoBehaviour {
    public float height;
    private GameObject SceneManager;
	// Use this for initialization
	void Start () {
        SceneManager = GameObject.FindGameObjectWithTag("SceneManager");
	}
	
	// Update is called once per frame
	void Update () {
	    if(transform.root.position.y <= height)
        {
            SceneManager.GetComponent<MySceneManager>().ReloadLevel();
        }
	}
}
