using UnityEngine;
using System.Collections;

public class SwitchGroupManager : MonoBehaviour {
    public GameObject _Wall;
    public bool UsualIsOn;

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (check())
        {
            _Wall.GetComponent<Wall>().isOn = !UsualIsOn;
        }else
        {
            _Wall.GetComponent<Wall>().isOn = UsualIsOn;
        }
        
    }

    bool check()
    {
        foreach(Transform child in transform)
        {
			if (!child.transform.FindChild("suitti siro/default/SwitchZone").gameObject.GetComponent<SwitchGroupMember>().isOn)
            {
                return false;
            }
        }
        return true;
    }
}
