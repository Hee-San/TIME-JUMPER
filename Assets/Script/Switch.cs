using UnityEngine;
using System.Collections;

public class Switch : MonoBehaviour {
    public GameObject _Wall;
    private int flag;
    public bool UsualIsOn;
	public int cubecolor;
	void OnTriggerStay(Collider other)
    {
		//if (other.CompareTag("Cube")) {
			flag = 5;
			transform.parent.GetComponent<Renderer>().material.color = Color.red;
			_Wall.GetComponent<Wall>().isOn = !UsualIsOn;
		//}
        
    }
    

    // Use this for initialization
    void Start () {
        flag = 0;
		transform.parent.GetComponent<Renderer>().material.color = Color.white;
        _Wall.GetComponent<Wall>().isOn = UsualIsOn;
	}
	
	// Update is called once per frame
	void Update () {
        if (flag <= 0)
        {
			transform.parent.GetComponent<Renderer>().material.color = Color.white;
            _Wall.GetComponent<Wall>().isOn = UsualIsOn;
        }else
        {
            flag--;
        }
    }
}
