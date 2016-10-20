using UnityEngine;
using System.Collections;

public class Laser : MonoBehaviour {
    public int onTime, offTime;
    public GameObject wall;
    GameObject TimeKeeper;
    public Vector3 posW;
    public Quaternion rotW;
    bool isOn = false;
    int  time;
    GameObject wall1;

    //ゲーム開始時
    void Start () {
        TimeKeeper = GameObject.Find("TimeKeeper");
        checkOn(TimeKeeper.GetComponent<TimeKeeper>().getTime());
        /*GameObject TimeSc = transform.FindChild("TimeSchedule").gameObject;
        TextMesh Tex = TimeSc.GetComponent<TextMesh>();
        Tex.text = onTime + ":00 - " + (offTime - 1) + ":59";*/
    }

    //ゲーム中1フレームごとに呼び出し
    void Update () {
        if (TimeKeeper.GetComponent<TimeKeeper>().check())
        {
            checkOn(TimeKeeper.GetComponent<TimeKeeper>().getTime());
        }
    }

    //ユーザー関数　時間から起動中か判定
    void checkOn(int time){
        bool wasOn = isOn;
        if(onTime < offTime){
            isOn = (onTime <= time && time < offTime);
        }else{
            isOn = (time < offTime || onTime <= time);
        }

        if (!wasOn && isOn){
            wall1 = Instantiate(wall, posW, rotW) as GameObject;
            //print("IS ON");
        }
        else if(wasOn && !isOn){
            Destroy(wall1);
            //print("IS OFF");
        }
    }
    
}
