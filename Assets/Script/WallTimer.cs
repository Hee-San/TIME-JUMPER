using UnityEngine;
using System.Collections;

public class WallTimer : MonoBehaviour {
    private GameObject _Wall;
    public int OnTime, OffTime;
    public bool UsualIsOn;
    public GameObject TimeKeeper;
    private int Time;
    // Use this for initialization
    void Start () {
		_Wall = transform.parent.gameObject;
	    Time = TimeKeeper.GetComponent<TimeKeeper>().StartTime;
        _Wall.GetComponent<Wall>().isOn = check();
    }
	
	// Update is called once per frame
	void Update () {
        Time = TimeKeeper.GetComponent<TimeKeeper>().getTime();
        _Wall.GetComponent<Wall>().isOn = check();
    }

    bool check()
    {
        if (UsualIsOn)
        {
            if (OnTime <= Time && Time < OffTime)
            {
                return false;
            }else
            {
                return true;
            }
        }else
        {
            if (OnTime <= Time && Time < OffTime)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
