using UnityEngine;
using System.Collections;

public class Wall : MonoBehaviour {
    public bool isOn;
    private bool wasOn;
	// Use this for initialization
	void Start () {
        if (!isOn)
        {
            GetComponent<BoxCollider>().enabled = false;
            GetComponent<MeshRenderer>().enabled = false;
        }
        wasOn = isOn;
	}
	
	// Update is called once per frame
	void Update () {
        if (wasOn && !isOn)
        {
            GetComponent<BoxCollider>().enabled = false;
            GetComponent<MeshRenderer>().enabled = false;
        }else if(!wasOn && isOn)
        {
            GetComponent<BoxCollider>().enabled = true;
            GetComponent<MeshRenderer>().enabled = true;
        }
        wasOn = isOn;
    }
}
