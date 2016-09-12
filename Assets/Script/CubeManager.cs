/*
 Instantiate/Destroyで管理
 各情報は、GameObjectと別に、データベース化してリストで保存
 
 
 */

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class CubeManager : MonoBehaviour {
	public GameObject _Cube1;
	public GameObject _Cube2;
	public GameObject _Cube3;

    [System.Serializable]
    public class PosAndRotAndTime
    {
        public Vector3 pos;
        public Quaternion rot;
        public int SpawnTime;
    }
    
    [SerializeField] public PosAndRotAndTime[] First_Spawn_PosAndTime;
    public List<Cube> CubeList; //データベース
    public GameObject TimeKeeper;
    int nowTime, wasTime;
    GameObject CubeListObject;
    public float mass;

    // Use this for initialization
    void Start () {
        int id = 1;
        nowTime = TimeKeeper.GetComponent<TimeKeeper>().StartTime;
        wasTime = nowTime;
        CubeListObject = new GameObject("Cubes");

	    foreach(PosAndRotAndTime fspt in First_Spawn_PosAndTime)
        {
            Vector3 pos = fspt.pos;
            Quaternion rot = fspt.rot; 
            int time = fspt.SpawnTime;
            int timer = 0;
            GameObject CubeListObjectC = new GameObject(id.ToString());
            CubeListObjectC.transform.parent = CubeListObject.transform;

            for (int i = time; i <= 3; i ++)
            {
                Cube c = new Cube();
                c.id = id;
                c.timer = timer;
                c.spTime = i;
                c.pos = pos;
                c.rot = rot;
                c.isOn = i == nowTime;
                c.isDraged = false;

                CubeList.Add(c);

                timer++;
            }
            id++;
        }
        //print(CubeList.Count);
        foreach (Cube C in CubeList.FindAll(c => c.id == 0))
        {
            CubeList.Remove(C);
        }
        
        foreach (Cube C in CubeList.FindAll(c => c.isOn))
        {
            spawn(C);
        }
    }

    // Update is called once per frame
    void Update()
    {
        nowTime = TimeKeeper.GetComponent<TimeKeeper>().getTime();
        if (nowTime != wasTime) {
            foreach(Cube C in CubeList.FindAll(c => c.isDraged))
            {
                //CubeUpdateDel(C);
                C.spTime = nowTime;
                //CubeUpdateSpwn(C);
            }
            foreach (Cube C in CubeList.FindAll(c => c.isOn && c.spTime != nowTime && !c.isDraged))
            {
                C.isOn = false;
                PosAndRot pr = del(C);

                if(pr.pos != C.pos ||pr.rot != C.rot)
                {
                    foreach (Cube D in CubeList.FindAll(d => d.id == C.id && d.timer >= C.timer && d.spTime >= C.spTime)){
                        D.pos = pr.pos;
                        D.rot = pr.rot;
                    }
                }
            }

            foreach (Cube C in CubeList.FindAll(c => !c.isOn && c.spTime == nowTime))
            {
                C.isOn = true;
                spawn(C);
            }
        }
        wasTime = nowTime;
    }

    void spawn(Cube data)
    {
		GameObject _Cube = new GameObject();
		switch(data.id){
		case 1:
			 _Cube = _Cube1;
			break;
		case 2:
			 _Cube = _Cube2;
			break;
		case 3:
			 _Cube = _Cube2;
			break;
		}
			
		GameObject c = Instantiate(_Cube) as GameObject;

        c.name = data.timer.ToString();
        string id = data.id.ToString();
        c.transform.parent = CubeListObject.transform.FindChild(id).gameObject.transform;

        c.GetComponent<Transform>().position = data.pos;
        c.GetComponent<Transform>().rotation = data.rot;
        //c.AddComponent<BoxCollider>();
        c.AddComponent<Rigidbody>();
        c.GetComponent<Rigidbody>().mass = mass;
        //c.GetComponent<Rigidbody>().useGravity = false;

    }
    PosAndRot del(Cube data)
    {
        string id = data.id.ToString();
        string timer = data.timer.ToString();
        print(timer);
        GameObject c = CubeListObject.transform.FindChild(id).FindChild(timer).gameObject;
        PosAndRot pr = new PosAndRot();
        pr.pos = c.GetComponent<Transform>().position;
        pr.rot = c.GetComponent<Transform>().rotation;
        Destroy(c);

        return pr;
    } //CubeのGameObjectを消す
    public Cube checkdata(GameObject C)
    {
        int timer = int.Parse(C.name);
        int id = int.Parse(C.transform.parent.name);
        return (CubeList.Find(c => c.timer == timer && c.id == id));
    }
    public void CubeUpdateDel(GameObject GOC)//未来の影響下のCubeのデータを消す
    {
        Cube C = checkdata(GOC);
		List<Cube> DelCubeList = CubeList.FindAll(del => del.id == C.id && del.timer > C.timer);
        foreach (Cube del in DelCubeList)
        {
            CubeList.Remove(del);
            if (del.isOn)
            {
                string id = del.id.ToString();
                string timer = del.timer.ToString();
                del.isOn = false;
                GameObject c = CubeListObject.transform.FindChild(id).FindChild(timer).gameObject;
                Destroy(c);
            }
        }
    }
    public void CubeUpdateSpwn(GameObject GOC)
    {
        Cube C = checkdata(GOC);
        int timer = C.timer + 1;
        for (int i = C.spTime + 1; i <= 3; i ++)
        {
            Cube newC = new Cube();
            newC.id = C.id;
            newC.timer = timer;
            newC.spTime = i;
            newC.pos = C.pos;
            newC.rot = C.rot;
            newC.isOn = false;
			newC.isDraged = false;

            CubeList.Add(newC);

            timer++;
        }
    }
    public void timeUpd(GameObject C)
    {
        checkdata(C).spTime = nowTime;
    }
}

[System.Serializable]
public class Cube
{
    public int id, timer, spTime;
    public Vector3 pos;
    public Quaternion rot;
    public bool isOn, isDraged;
}

public class PosAndRot
{
    public Vector3 pos;
    public Quaternion rot;
}


