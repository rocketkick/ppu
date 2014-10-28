using UnityEngine;
using System.Collections;

public class csTrain : MonoBehaviour {
	GameObject manager;

	// Use this for initialization
	void Start () {
		manager=GameObject.Find("Manager");
	}
	// Update is called once per frame
	void Update () {

	}
	void Shooting_Raycast(Vector3 inVector)
	{
		Vector3 rot=new Vector3(0,-1,0);
		Vector3 pos=this.gameObject.transform.position;


		Debug.Log("Train shooting Ray!!!");

		//Debug.DrawRay(pos,rot*20,Color.green);

		RaycastHit input_hit;
		if(Physics.Raycast(pos,rot,out input_hit,10))
		{
			if(input_hit.collider.tag == "PANEL")
			{
				Debug.Log("shooted: PANEL");
				input_hit.collider.SendMessage("End");
			}else if(input_hit.collider.tag == "RAIL")
			{
				Debug.Log("shooted: "+input_hit.collider.name);
				input_hit.collider.SendMessage("Check_Rail",trainDirction);
				
			}
			else{
			//외부로 나가도 게임 오버
			Debug.Log("shooted: NOTHING");
			manager.SendMessage("Game_Over");
			}
		}
		//Debug.Log("oops!!");

	}
}
