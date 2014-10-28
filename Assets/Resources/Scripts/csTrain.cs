using UnityEngine; 
using System.Collections; 


public class csTrain : MonoBehaviour { 
	GameObject manager,train;
	public static Vector3 inVector,outVector;
	public static float angle,bangle;
	public static float speed=1f;
	public static bool on = false;
	
	// Use this for initialization 
	void Start () { 
		manager=GameObject.Find("Manager"); 
		train=GameObject.Find("Train");
	} 
	// Update is called once per frame 
	void Update () { 
		
	} 
	//이거 없이 그냥 바로 Shooting_Raycast 호출
	void Switch_On()
	{
		Debug.Log("우와");
		manager.SendMessage("Switch_On");
	}
	void gogogo(){
		Debug.Log("gogogo:"+outVector);
		Shooting_Raycast(outVector);
	}
	
	void Shooting_Raycast(Vector3 outvector) 
	{ 
		outVector = outvector;
		Vector3 rot=new Vector3(0,-1,0); 
		Vector3 pos=this.gameObject.transform.position; 
		
		Debug.Log("Train shooting Ray !!!"); 
		
		RaycastHit input_hit; 
		if(Physics.Raycast(pos,rot,out input_hit,10)) 
		{ 
			if(input_hit.collider.tag == "PANEL") 
			{ 
				Debug.Log("shooted: PANEL"); 
				input_hit.collider.SendMessage("End"); 
			}else if(input_hit.collider.tag == "START")
			{
				Debug.Log("shooted: START"+outvector);
				input_hit.collider.SendMessage("Check_Start",outvector);
			}else if(input_hit.collider.tag == "RAIL") 
			{ 
				Debug.Log("shooted: "+input_hit.collider.name); 
				input_hit.collider.SendMessage("Check_Rail",outvector);  
			}else{ 
				//외부로 나가도 게임 오버 
				Debug.Log("shooted: NOTHING"); 
				manager.SendMessage("Game_Over"); 
			} 
		} 
		//Debug.Log("oops!!"); 
		
	} 
	
	void Chainge_Vector(string railName){
		
		//rail이름과 outVetor 값을 이용해 진행 가능한지 확인하고 
		if (inVector.x == 1) 
		{
			if(railName == "R1")
			{
				Debug.Log("OK "+railName);
				outVector = new Vector3(1,0,0);
				angle = 0f;
				Gogo_Train();
				Rotate_Train(angle);
				//manager.SendMessage("Move_Train",outVector);
				//manager.SendMessage("Rotate_Train",0f);
			}else if(railName == "R3")
			{ Debug.Log("OK "+railName);
				outVector = new Vector3(0,0,1);
				angle = -90f;
				Gogo_Train();
				Rotate_Train(angle);
				//manager.SendMessage("Move_Train",outVector);
				//manager.SendMessage("Rotate_Train",-90f);
			}else if(railName == "R6")
			{ Debug.Log("OK "+railName);
				outVector = new Vector3(0,0,-1);
				angle = 90f;
				Gogo_Train();
				Rotate_Train(angle);
				//manager.SendMessage("Move_Train",outVector);
				//manager.SendMessage("Rotate_Train",90f);
			}else if(railName == "R7")
			{
				Debug.Log("OK "+railName);
				outVector = new Vector3(1,0,0);
				angle = 0f;
				Gogo_Train();
				Rotate_Train(angle);
				//manager.SendMessage("Move_Train",outVector);
				//manager.SendMessage("Rotate_Train",0f);
			}else
			{
				manager.SendMessage("Game_Over",inVector);
				manager.SendMessage("Rotate_Train",0f);
			}
		}else if (inVector.x == -1) 
		{
			if(railName == "R1")
			{
				Debug.Log("OK "+railName);
				outVector = new Vector3(-1,0,0);
				angle = 0f;
				Gogo_Train();
				Rotate_Train(angle);
				//manager.SendMessage("Move_Train",outVector);
				//manager.SendMessage("Rotate_Train",0f);
			}else if(railName == "R4")
			{ Debug.Log("OK "+railName);
				outVector = new Vector3(0,0,1);
				angle = 90f;
				Gogo_Train();
				Rotate_Train(angle);
				//manager.SendMessage("Move_Train",outVector);
				//manager.SendMessage("Rotate_Train",90f);
			}else if(railName == "R5")
			{ Debug.Log("OK "+railName);
				outVector = new Vector3(0,0,-1);
				angle = -90f;
				Gogo_Train();
				Rotate_Train(angle);
				//manager.SendMessage("Move_Train",outVector);
				//manager.SendMessage("Rotate_Train",-90f);
			}else if(railName == "R7")
			{
				Debug.Log("OK "+railName);
				outVector = new Vector3(-1,0,0);
				angle = 0f;
				Gogo_Train();
				Rotate_Train(angle);
				//manager.SendMessage("Move_Train",outVector);
				//manager.SendMessage("Rotate_Train",0f);
			}else
			{
				manager.SendMessage("Game_Over",inVector);
				manager.SendMessage("Rotate_Train",0f);
			}
		}else if (inVector.z == 1) 
		{
			if(railName == "R2")
			{
				Debug.Log("OK "+railName);
				outVector = new Vector3(0,0,1);
				angle = 0f;
				Gogo_Train();
				Rotate_Train(angle);
				//manager.SendMessage("Move_Train",outVector);
				//manager.SendMessage("Rotate_Train",0f);
			}else if(railName == "R5")
			{ Debug.Log("OK "+railName);
				outVector = new Vector3(1,0,0);
				angle = 90f;
				Gogo_Train();
				Rotate_Train(angle);
				//manager.SendMessage("Move_Train",outVector);
				//manager.SendMessage("Rotate_Train",90f);
			}else if(railName == "R6")
			{ Debug.Log("OK "+railName);
				outVector = new Vector3(-1,0,0);
				angle = -90f;
				Gogo_Train();
				Rotate_Train(angle);
				//manager.SendMessage("Move_Train",outVector);
				//manager.SendMessage("Rotate_Train",-90f);
			}else if(railName == "R7")
			{
				Debug.Log("OK "+railName);
				outVector = new Vector3(0,0,1);
				angle = 0f;
				Gogo_Train();
				Rotate_Train(angle);
				//manager.SendMessage("Move_Train",outVector);
				//manager.SendMessage("Rotate_Train",0f);
			}else
			{
				manager.SendMessage("Game_Over",inVector);
				manager.SendMessage("Rotate_Train",0f);
			}
		}else if (inVector.z == -1) 
		{
			if(railName == "R2")
			{
				Debug.Log("OK "+railName);
				outVector = new Vector3(0,0,-1);
				angle = 0f;
				Gogo_Train();
				Rotate_Train(angle);
				//manager.SendMessage("Move_Train",outVector);
				//manager.SendMessage("Rotate_Train",0f);
			}else if(railName == "R3")
			{ Debug.Log("OK "+railName);
				outVector = new Vector3(-1,0,0);
				angle = 90f;
				Gogo_Train();
				Rotate_Train(angle);
				//manager.SendMessage("Move_Train",outVector);
				//manager.SendMessage("Rotate_Train",90f);
			}else if(railName == "R4")
			{ Debug.Log("OK "+railName);
				outVector = new Vector3(1,0,0);
				angle = -90f;
				Gogo_Train();
				Rotate_Train(angle);
				//manager.SendMessage("Move_Train",outVector);
				//manager.SendMessage("Rotate_Train",-90);
			}else if(railName == "R7")
			{
				Debug.Log("OK "+railName);
				outVector = new Vector3(0,0,-1);
				angle = 0f;
				//manager.SendMessage("Move_Train",outVector);
				//manager.SendMessage("Rotate_Train",0f);
			}else
			{
				manager.SendMessage("Game_Over",inVector); 
				manager.SendMessage("Rotate_Train",0f);
			}
		}else
		{
			manager.SendMessage("Game_Over");
			manager.SendMessage("Rotate_Train",0f);
		}
	}
	
	void Move_Train(string rail)
	{  
		inVector = outVector;
		bangle = angle;

		Debug.Log("MoveTrain");
		Debug.Log("Before chainge outVector:"+inVector);
		Debug.Log("Before chainge angle:"+bangle);
		
		//outVector를 inVector에 저장후 outVector 변경
		Chainge_Vector(rail);
		Debug.Log("Chainged inVector:"+inVector);
		Debug.Log("Chainged outVector:"+outVector);
		Debug.Log("Chainged angle:"+angle);

	
	}
	void Gogo_Train(){
		StartCoroutine("MoveTrain",inVector);
	}
	void Rotate_Train(float angle)
	{
		StartCoroutine("RotateTrain",angle);
		bangle = angle;
	}
	
	IEnumerator MoveTrain()
	{
		//이동 
		Vector3 temp = train.transform.position;
		iTween.MoveTo(train, iTween.Hash("x",temp.x+inVector.x,"y",temp.y,"z",temp.z+inVector.z,"time",2f*speed, "easeType", "linear"/*, "delay", .1*/,"onComplete","gogogo")); 
		yield return 0;
		//yield return new WaitForSeconds(0f);
	}
	IEnumerator RotateTrain(float gak)
	{
		//make global 
		//GameObject tr;
		//tr = GameObject.Find("Train");
		
		iTween.RotateBy(train, iTween.Hash("y",gak/360,"easeType","linear","time",2f*speed,"delay",.8f*speed));
		yield return 0;
		//yield return new WaitForSeconds(0f);
	}
	//게임 종료 조건을 만족시 마지막 칸으로 진행하고 폭발!!
	void Game_Over(){
		//이동 
		iTween.MoveBy(train, iTween.Hash("x",inVector.x,"y",0,"z",inVector.z, "easeType", "easeInOutExpo"/*, "delay", .1*/)); 
		Debug.Log("Crash!!!");
		Debug.Log("Game Over");
	}
	void Goal(Vector3 outvector){
		iTween.MoveBy (train, iTween.Hash ("x", outvector.x, "y", 0, "z", outvector.z, "time", 2f * speed, "easeType", "linear")); 
		//complete message!!
		manager.SendMessage ("Game_Over");
	}
} 
