using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class MySceneManager : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetButtonDown("ReSpawn"))
		{
			ReloadLevel();
		}
		if (Input.GetButtonDown("ReStart"))
		{
			LoadGate();
		}
    }

	public void ReloadLevel()
	{
		Application.LoadLevel(Application.loadedLevelName);
	}

	public void LoadGate()
    {
		SceneManager.LoadScene("Gate");
    }
}
