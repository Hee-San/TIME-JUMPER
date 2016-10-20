using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;


public class TimeKeeper : MonoBehaviour {
    public int StartTime;    //開始時刻をInspectorで登録
    public GameObject PlayerCamera;
	public Material sky1, sky2, sky3;
    [HideInInspector]
    public int time;
    public Text timeTxt;
    [HideInInspector]
    public bool Check;

    //ゲーム開始時
    void Start () {
        time = StartTime;
        Check = true;
		skyUpdate (time);
	}

    //ゲーム中1フレームごとに呼び出し
    void Update () {
        if (Input.GetButtonDown("+Time") && canJump() && time != 3)
        {
            Check = true;
            time ++;
			skyUpdate (time);
        }else if (Input.GetButtonDown("-Time") && canJump() && time != 1)
        {
            Check = true;
			time --;
			skyUpdate (time);
        }else{
            Check = false;
        }
        
        timeTxt.text = time.ToString() + ":00";
    }

    public int getTime()
    {
        return time;
    }
    public bool check()
    {
        return Check;
    }

    public bool canJump()
    {
        foreach(GameObject c in GameObject.FindGameObjectsWithTag("Cube"))
        {
            if(c.GetComponent<Rigidbody>().velocity.magnitude != 0)
            {
                Cube data = GameObject.FindGameObjectWithTag("CubeManager").GetComponent<CubeManager>().checkdata(c);

                if (!data.isDraged) {
                    return false;
                }
            }
        }
        return true;
    }

	void skyUpdate(int time){
		switch (time) {
		case 1:
			PlayerCamera.GetComponent<Skybox> ().material = sky1;
			break;
		case 2:
			PlayerCamera.GetComponent<Skybox> ().material = sky2;
			break;
		case 3:
			PlayerCamera.GetComponent<Skybox> ().material = sky3;
			break;
		}
	}
}
