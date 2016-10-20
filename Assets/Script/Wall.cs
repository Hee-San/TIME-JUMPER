using UnityEngine;
using System.Collections;

public class Wall : MonoBehaviour {
	private GameObject TimeKeeper;
	public GameObject[] _Switches;
	public int OnTime, OffTime;
	public bool usualIsOn = false;
	public bool isOn;
	private bool wasOn;
	private int Time;
	// Use this for initialization
	void Start () {
		TimeKeeper = GameObject.FindWithTag ("TimeKeeper");
        if (!isOn)
        {
            GetComponent<BoxCollider>().enabled = false;
            GetComponent<MeshRenderer>().enabled = false;
        }
        wasOn = isOn;
	}
	
	// Update is called once per frame
	void Update () {
		Time = TimeKeeper.GetComponent<TimeKeeper>().getTime();
		if (usualIsOn) {
			isOn = OnTime <= Time || Time < OffTime;
		} else {
			isOn = OnTime <= Time && Time < OffTime;
		}

		foreach (GameObject _switch in _Switches) {
			isOn = isOn && _switch.transform.Find("suitti siro").Find("default").Find("SwitchZone").GetComponent<Switch> ().isOn;
		}


        if (wasOn && !isOn)
		{
			foreach (Transform child in transform) {
				child.gameObject.SetActive (false);
			}
            GetComponent<BoxCollider>().enabled = false;
            GetComponent<MeshRenderer>().enabled = false;
        }else if(!wasOn && isOn)
        {
            GetComponent<BoxCollider>().enabled = true;
			GetComponent<MeshRenderer>().enabled = true;
			foreach (Transform child in transform) {
				child.gameObject.SetActive (true);
			}
        }
        wasOn = isOn;
    }
}
