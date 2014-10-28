using UnityEngine; 
using System.Collections; 


public class csManager : MonoBehaviour { 
	public static GameObject panel, rail, template, start, goal, train; 
	public static GameObject r1,r2,r3,r4,r5,r6,r7; 
	public static int stat=0; 
	public static float speed = 1f;
	//train 진행방향 u: up, d: down, l: left, r: right 
	public static string trainDirection=""; 
	public static GameObject[] queue = new GameObject[5]; 
	public static GameObject[] stack = new GameObject[50]; 
	public static int startTime, delayTime; 
	//train 진행방향 벡트값 
	public Vector3 inVector, outVector; 
	public static bool moveKey = false;
	public static bool gameOver = false;
	
	
	GameObject panel_bank; 
	GameObject rail_bank; 
	GameObject queue_bank; 
	
	
	//private bool train_on = false; 
	
	
	void Awake() 
	{ 
		for(int s=0;s<49;s++) 
		{ stack[s]=null; 
		} 
		
		panel=Resources.Load("Prefabs/Panel") as GameObject; 
		template=Resources.Load("Prefabs/Panel") as GameObject; 
		start=Resources.Load("Prefabs/Start") as GameObject; 
		goal=Resources.Load("Prefabs/Goal") as GameObject;
		
		train=Resources.Load("Prefabs/Train") as GameObject; 
		
		r1 = Resources.Load("Prefabs/R1") as GameObject; 
		r2 = Resources.Load("Prefabs/R2") as GameObject; 
		r3 = Resources.Load("Prefabs/R3") as GameObject; 
		r4 = Resources.Load("Prefabs/R4") as GameObject; 
		r5 = Resources.Load("Prefabs/R5") as GameObject; 
		r6 = Resources.Load("Prefabs/R6") as GameObject; 
		r7 = Resources.Load("Prefabs/R7") as GameObject; 
		
		
		panel_bank=GameObject.Find("Panel_Bank"); 
		rail_bank=GameObject.Find("Rail_Bank"); 
		queue_bank=GameObject.Find("Queue_Bank"); 
		
		
		//패널 기본 위치 지정 
		int posX = -3; 
		int posZ = 5; 
		
		
		//배경 패널 배치 
		for(int i=0;i<7;++i) 
		{ 
			for(int j=0;j<7;++j) 
			{ 
				stat++; 
				GameObject temp1=Instantiate(panel,new Vector3(posX,0,posZ),transform.rotation) as GameObject; 
				temp1.transform.parent=panel_bank.transform; 
				temp1.name = stat+""; //패널의 위치값으로 이름을 지정 
				
				//temp.name=panel.name; 
				posX++; 
			} 
			posZ++; 
			posX = -3; 
		} 
		
		
		/*레일 배경 배치 
  int railX = -5; 
  int railZ = 11; 
  for(int k=0;k<5;++k) 
  { 
   GameObject temp2=Instantiate(template, new Vector3(railX,0,railZ),transform.rotation) as GameObject; 
   temp2.transform.parent=panel_bank.transform; 
   temp2.name=template.name; 
   railZ--; 
  } 
  */
	} 
	// Use this for initialization 
	void Start ()  
	{ 
		int railX = -5; 
		int railZ = 11; 
		speed = 0.5f;
		for(int k=0;k<5;++k) 
		{ 
			//queue에 rail을 랜덤 생성해서 리턴 받음 
			rail = make_rail();
			
			//큐에 5개의 레일을 배치하고 대기 
			queue[k]=Instantiate(rail, new Vector3(railX,1,railZ),transform.rotation) as GameObject; 
			queue[k].transform.parent=queue_bank.transform; 
			queue[k].name=rail.name; 
			railZ--; 
		} 
		
		//임시로 시작지점과 진행 방향을 지정 
		GameObject temp3=Instantiate(start, new Vector3(1,1,8),transform.rotation) as GameObject; 
		temp3.name=start.name; 
		
		GameObject temp4=Instantiate(train, new Vector3(1,2,8),transform.rotation) as GameObject; 
		temp4.name =train.name; 
		train = temp4; 

		GameObject temp5=Instantiate(goal, new Vector3(-1,1,6),transform.rotation) as GameObject; 
		temp5.name =goal.name; 
		
		inVector = new Vector3(0, 0, -1);
		outVector = new Vector3(0, 0, -1);
		
		StartCoroutine("Countdown"); 
	}  
	
	
	// Update is called once per frame 
	void Update () { 
		if(moveKey == true) 
		{ 
			TrainRayShoot(); 
			//StartCoroutine("TrainRayShoot"); 
		}
	} 
	
	void TrainRayShoot() 
	{ 
		moveKey = false; 
		//이동전 다음 레일로 이동 가능한지 체그 
		train.SendMessage("Shooting_Raycast",outVector);
	}
	
	void Switch_On()
	{
		moveKey = true;
	}
	
	IEnumerator Countdown(){ 
		yield return new WaitForSeconds(4f); 
		moveKey = true; 
	}
	
	
	
	void Game_Over(){
		//Crash_Train(outVec);
		Debug.Log("Game Over");
	}
	
	
	
	//레일이 없는 곳을 클릭 했을시 레일 배치
	void Choice_Panel(GameObject choice_temp) 
	{ 
		Vector3 temp; 
		GameObject demo; 
		//선택된 패널 이름 
		Debug.Log(choice_temp.name); 
		//동작 설명 
		Debug.Log(" 큐에 마지막 레일을 해당 위치에 배치 "); 
		/* queue[4]의 레일을 직접 이동하고 신규 생성 하는 경우 */ 
		//클릭 지점으로 해당 레일 이동 
		//x축으로 -1 이동해서 생성해야 해서 코드가 길어짐 
		demo = queue[4]; 
		temp = new Vector3(choice_temp.transform.position.x,choice_temp.transform.position.y+1,choice_temp.transform.position.z); 
		demo.transform.position=temp; 
		
		//패널을 클릭한 경우이므로 패널에 레일이 비어 있는지 확인 필요 없이 스택에 추가 
		stack[int.Parse(choice_temp.name)]=demo; 
		demo.transform.parent=rail_bank.transform;
		
		/*큐를 하나씩 이동하고 queue[0]에 신규 레일 생성*/ 
		for( int i=4;i>0;i--) 
		{  
			queue[i] = queue[i-1]; 
			queue[i-1].transform.Translate(0,0,-1); 
		} 
		
		//queue에 랜덤 rail을 생성해서 리턴 받음 
		rail = make_rail(); 
		
		//큐에 1개의 레일을 배치하고 대기 
		queue[0]=Instantiate(rail, queue[0].transform.position,transform.rotation) as GameObject; 
		queue[0].transform.parent=queue_bank.transform; 
		queue[0].name=rail.name; 
		//queue[0]을 queue[1]의 위치로 마지막으로 이동 
		queue[0].transform.Translate(0,0,1);
	} 
	
	void Choice_Rail(GameObject choice_temp){ 
		Debug.Log(choice_temp.name); 
		//선택된 오브젝트 파괴 
		Debug.Log(" 선택 오브젝트 파괴 "); 
		Destroy(choice_temp); 
		
		/*************************************************/ 
		/***** 파괴 패널티 코딩 ****************************/ 
		/*************************************************/ 
		
		Debug.Log(" 큐에 마지막 레일을 해당 위치에 배치 "); 
		queue[4].transform.position = choice_temp.transform.position; 
		queue[4].transform.parent=rail_bank.transform; 
		queue[4].transform.position=choice_temp.transform.position; 
		//stack[int.Parse(choice_temp.name)]=queue[4]; 
		//설치 확인 
		//Debug.Log(stack[int.Parse(choice_temp.name)].name+" replace"); 
		
		/*큐를 하나씩 이동하고 queue[0]에 신규 레일 생성*/ 
		for( int i=4;i>0;i--) 
		{  
			queue[i] = queue[i-1]; 
			queue[i-1].transform.Translate(0,0,-1); 
		} 
		
		//queue에 랜덤 rail을 랜덤 생성해서 리턴 받음 
		rail = make_rail(); 
		
		//큐에 1개의 레일을 배치하고 대기 
		queue[0]=Instantiate(rail, queue[0].transform.position,transform.rotation) as GameObject; 
		queue[0].transform.parent=queue_bank.transform; 
		queue[0].name=rail.name; 
		//queue[0]을 마지막으로 이동 
		queue[0].transform.Translate(0,0,1);
	} 
	
	//레일을 랜덤 생성
	GameObject make_rail() 
	{ 
		int t = Random.Range(0,7); 
		GameObject rail=null; 
		
		if(t==0) 
			rail=r1; 
		else if(t==1) 
			rail=r2; 
		else if(t==2) 
			rail=r3; 
		else if(t==3) 
			rail=r4; 
		else if(t==4) 
			rail=r5; 
		else if(t==5) 
			rail=r6; 
		else if(t==6) 
			rail=r7; 
		return rail; 
	} 
} 
