using UnityEngine;
using System.Collections;

public class csManager : MonoBehaviour {
	public static GameObject panel, rail, template, start, train;
	public static GameObject r1,r2,r3,r4,r5,r6,r7;
	public static int stat=0;
	//train 진행방향 u: up, d: down, l: left, r: right
	public static string trainDirection="";
	public static GameObject[] queue = new GameObject[5];
	public static GameObject[] stack = new GameObject[49];
	public static int startTime, delayTime;
	//train 진행방향 벡트값
	public Vector3 inVector,outVector;
	public static bool moveKey = false;

	GameObject panel_bank;
	GameObject rail_bank;
	GameObject queue_bank;

	//private bool train_on = false;

	void Awake()
	{
		for(int s=0;s<49;s++)
		{	stack[s]=null;
		}
	
		panel=Resources.Load("Prefabs/Panel") as GameObject;
		template=Resources.Load("Prefabs/Panel") as GameObject;
		start=Resources.Load("Prefabs/Start_u") as GameObject;

		train=Resources.Load("Prefabs/Train") as GameObject;

		//스타트 방향에 따라 이미지 삽입

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
				temp1.name = stat+"";	//패널의 위치값으로 이름을 지정

				//temp.name=panel.name;
				posX++;
			}
			posZ++;
			posX = -3;
		}

		//레일 배경 배치
		int railX = -3;
		int railZ = 3;
		for(int k=0;k<5;++k)
		{
			GameObject temp2=Instantiate(template, new Vector3(railX,0,railZ),transform.rotation) as GameObject;
			temp2.transform.parent=panel_bank.transform;
			temp2.name=template.name;
			railX++;
		}
	}
	// Use this for initialization
	void Start () 
	{
		int railX = -3;
		int railZ = 3;
		
		for(int k=0;k<5;++k)
		{
			//queue에 rail을 랜덤 생성해서 리턴 받음
			rail = make_rail();
			
			//큐에 5개의 레일을 배치하고 대기
			queue[k]=Instantiate(rail, new Vector3(railX,1,railZ),transform.rotation) as GameObject;
			queue[k].transform.parent=queue_bank.transform;
			queue[k].name=rail.name;
			railX++;
		}
		
		//임시로 시작지점과 진행 방향을 지정
		GameObject temp3=Instantiate(start, new Vector3(1,1,8),transform.rotation) as GameObject;
		temp3.name=start.name;
		start = temp3;
		
		GameObject temp4=Instantiate(train, new Vector3(1,2,8),transform.rotation) as GameObject;
		temp4.name =train.name;
		train = temp4;
		
		outVector = new Vector3(0,0,-1);
		outVector = new Vector3(0,0,-1);
		StartCoroutine("Wait");
	}	



	// Update is called once per frame
	void Update () {
		
		if(moveKey == true)
		{
			StartCoroutine("MoveTrain" outVector);
			//StartCoroutine("TrainRayShoot");
		}
		
	}
	
	IEnumerator TrainRayShoot(){
		//train 움직임을 멈추고
		moveKey = false;
		
		
		//yield가 나오면 Update로 돌아감
		yield return new WaitForSeconds(2);
		//yield return 0;
	}
	IEnumerator MoveTrain(Vector3 outVector)
	{
		moveKey = false;
		
		//이동전 다음 레일로 이동 가능한지 체그
		
		
		//이동
		iTween.MoveBy(train, iTween.Hash("x",outVector.x,"y",0,"z",outVector.z, "easeType", "easeInOutExpo", "delay", .1));
		//회전


		yield return new WaitForSeconds(2);
		//if(train.transform.position.x%1==0&&train.transform.position.y%1==0&&train.transform.position.z%1==0)
		train.SendMessage("Shooting_Raycast",outVector);
	}
	IEnumerator Wait()
	{
		yield return new WaitForSeconds(3);
		moveKey = true;
	}












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
			queue[i-1].transform.Translate(1,0,0);
		}

		//queue에 랜덤 rail을 생성해서 리턴 받음
		rail = make_rail();

		//큐에 1개의 레일을 배치하고 대기
		queue[0]=Instantiate(rail, queue[0].transform.position,transform.rotation) as GameObject;
		queue[0].transform.parent=queue_bank.transform;
		queue[0].name=rail.name;
		//queue[0]을 queue[1]의 위치로 마지막으로 이동
		queue[0].transform.Translate(-1,0,0);

	}

	void Choice_Rail(GameObject choice_temp)
	{
		Debug.Log(choice_temp.name);


		//선택된 오브젝트 파괴
		Debug.Log(" 선택 오브젝트 파괴 ");
		Destroy(choice_temp);

		/*************************************************/
		/******************파괴 패널티 코딩*****************/
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
			queue[i-1].transform.Translate(1,0,0);
		}

		//queue에 랜덤 rail을 랜덤 생성해서 리턴 받음
		rail = make_rail();

		//큐에 1개의 레일을 배치하고 대기
		queue[0]=Instantiate(rail, queue[0].transform.position,transform.rotation) as GameObject;
		queue[0].transform.parent=queue_bank.transform;
		queue[0].name=rail.name;
		//queue[0]을 마지막으로 이동
		queue[0].transform.Translate(-1,0,0);		
	}

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

	void Check_Rail(GameObject choice_temp)
	{
		//레이 맞은 레일의 이름을 이용해 해당 레일로 이동 가능한지 검사 후 이동 방향 받아옴
		trainVector =  Rail_Determine(choice_temp.name);

		//StartCoroutine("Twosecond",1);
	}
	void Chain_Rail(GameObject chain_rail)
	{

		Debug.Log(trainDirection+chain_rail.name);
		if(trainDirection == "u")
		{
			if(chain_rail.name=="R2"||chain_rail.name=="R3"||chain_rail.name=="R4"||chain_rail.name=="R7")
			{
				Debug.Log("OK");
			}
		}else if(trainDirection == "d")
		{
			if(chain_rail.name=="R2"||chain_rail.name=="R5"||chain_rail.name=="R6"||chain_rail.name=="R7")
			{
				Debug.Log("OK");
			}
		}else if(trainDirection == "l")
		{
			if(chain_rail.name=="R1"||chain_rail.name=="R3"||chain_rail.name=="R6"||chain_rail.name=="R7")
			{
				Debug.Log("OK");
			}
		}else if(trainDirection == "r")
		{
			if(chain_rail.name=="R1"||chain_rail.name=="R4"||chain_rail.name=="R5"||chain_rail.name=="R7")
			{
				Debug.Log("OK");
			}
		}else
		Debug.Log("SHIT!!");
	}

	Vector3 Rail_Determine(string railName)
	{
		//레일 이름과 trainDirection을 이용해 레일 이동 가능한지 검사
		if(trainDirection=="u")
		{
			if(railName=="R2")
			{
				trainDirection="u";
				trainVector = new Vector3(0,0,-1);
				Debug.Log("r2");
			}
			if(railName=="R3")
			{
				trainDirection="r";
				trainVector = new Vector3(-1,0,0);
				Debug.Log("r3");
			}
			if(railName=="R4")
			{
				trainDirection="l";
				trainVector = new Vector3(1,0,0);
				Debug.Log("r4");
			}
			if(railName=="R7")
			{
				trainDirection="u";
				trainVector = new Vector3(0,0,-1);
				Debug.Log("CROSS!!rr");
			}
		}else if(trainDirection=="d")
		{
			if(railName=="R2")
			{
				trainDirection="d";
				trainVector = new Vector3(0,0,1);
			}
			if(railName=="R5")
			{
				trainDirection="l";
				trainVector = new Vector3(1,0,0);
			}
			if(railName=="R6")
			{
				trainDirection="r";
				trainVector = new Vector3(-1,0,0);
			}
			if(railName=="R7")
			{
				trainDirection="d";
				trainVector = new Vector3(0,0,1);
				Debug.Log("CROSS!!");
			}
		}else if(trainDirection=="l")
		{
			if(railName=="R1")
			{
				trainDirection="l";
				trainVector = new Vector3(1,0,0);
			}
			if(railName=="R3")
			{
				trainDirection="d";
				trainVector = new Vector3(0,0,1);
			}
			if(railName=="R6")
			{
				trainDirection="u";
				trainVector = new Vector3(0,0,-1);
			}
			if(railName=="R7")
			{
				trainDirection="l";
				trainVector = new Vector3(1,0,0);
				Debug.Log("CROSS!!");
			}
		}else if(trainDirection=="r")
		{
			if(railName=="R1")
			{
				trainDirection="r";
				trainVector = new Vector3(-1,0,0);
			}
			if(railName=="R4")
			{
				trainDirection="d";
				trainVector = new Vector3(0,0,1);
			}
			if(railName=="R5")
			{
				trainDirection="u";
				trainVector = new Vector3(0,0,-1);
			}
			if(railName=="R7")
			{
				trainDirection="r";
				trainVector = new Vector3(-1,0,0);
				Debug.Log("CROSS!!");
			}
		}//크로스 보너스 포인트 코딩
		else Debug.Log("다음 레일 없음");

		Debug.Log(trainVector);
		Debug.Log(trainDirection);
		moveKey = true;
		return trainVector;
	}




	void Game_Over(){
		trainVector = new Vector3(0,0,0);
		trainDirection = "End";
		Debug.Log("Game Over");
		moveKey = false;
	}

}