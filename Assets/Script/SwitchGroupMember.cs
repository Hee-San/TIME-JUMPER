using UnityEngine;
using System.Collections;

public class SwitchGroupMember : MonoBehaviour
{
    private GameObject SwitchGroupManager;
    public bool isOn;
	private int flag;
	void OnTriggerStay()
	{
		flag = 1;
		transform.parent.GetComponent<Renderer>().material.color = Color.red;
		isOn = true;
	}


	// Use this for initialization
	void Start () {
		flag = 0;
		transform.parent.GetComponent<Renderer>().material.color = Color.white;
		isOn = false;
	}

	// Update is called once per frame
	void Update () {
		if (flag < 0) {
			transform.parent.GetComponent<Renderer> ().material.color = Color.white;
			isOn = false;
		} else {
			flag--;
		}
	}
}
