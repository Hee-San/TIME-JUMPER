using UnityEngine;
using System.Collections;

public class Switch : MonoBehaviour {
	public bool isOn;
    private int flag;
    public bool UsualIsOn;
	public int cubecolor;
	void OnTriggerStay(Collider other)
    {
		if(int.Parse(other.transform.parent.parent.name) == cubecolor){
		//if (other.CompareTag("Cube")) {
			flag = 5;
			transform.parent.GetComponent<Renderer>().material.color = Color.red;
			isOn = !UsualIsOn;
		//}
		}
        
    }
    

    // Use this for initialization
    void Start () {
        flag = 0;
		transform.parent.GetComponent<Renderer>().material.color = Color.white;
        isOn = UsualIsOn;
	}
	
	// Update is called once per frame
	void Update () {
        if (flag <= 0)
        {
			transform.parent.GetComponent<Renderer>().material.color = Color.white;
            isOn = UsualIsOn;
        }else
        {
            flag--;
        }
    }
}
