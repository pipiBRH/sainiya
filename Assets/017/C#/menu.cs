using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class menu : MonoBehaviour
{
	public mapcrt data;
	public Main_GUI game;
	public int tt, t;
	public bool cofig = false;
	public string na = "";
	public float tx, tx2;
	public CMySql sq;
	public bool autoLogin = false;
	public string str_acc = "" ;
	public string str_pw = "";
	public string str_pw2 = "";
	public string str_mail = ""; 
	public string str_mail2 = "";
	public bool login = true;
	public int state;
	public string score;
	public LeaderBoard[] leaderBoard = new LeaderBoard[10];
	public bool rank = true;

	
	#region Variables	
	//在此輸入開發者後台顯示的gguid
	private const string gguid = "5f6a3e60-3c60-6443-be94-c80a460faa5a";
	
	//在此輸入開發者後台顯示的sguid
	private const string sguid = "d006b7c7-6df8-b043-8ad7-06405e1e3a7e";
	
	//在此輸入開發者後台顯示的憑證
	//EX: byte[] certificate = {0x60,0x1f,0xef,0xc5,0x..,0x..,0x..,0x..};
	byte[] certificate = {0x4b,0xce,0x23,0xcf,0x1c,0x12,0xa,0x40,0x9d,0x64,0xad,0xac,0x5e,0xe2,0x75,0x3c,0x69,0x61,0xc5,0xc,0x22,0x3e,0xbe,0x45,0x9d,0x27,0xb4,0x12,0x66,0x8b,0xe9,0x5a,0x10,0xa6,0x62,0x9c,0x87,0x51,0x55,0x43,0x9f,0x34,0x7f,0x54,0x6b,0xd0,0x88,0xed,0xc3,0x2b,0xc5,0x7c,0x2a,0xa9,0xd8,0x4a,0xac,0xce,0x6b,0x4e,0xa0,0xf6,0xd5,0xe0,0x90,0x54,0x13,0x31,0x3b,0x9f,0x34,0x42,0x82,0x6e,0x66,0x91,0x75,0x6d,0xbe,0xa5,0xf5,0x48,0x6b,0xd3,0x3d,0x16,0x74,0x4c,0xb1,0x71,0x37,0xf4,0x1b,0xe5,0x99,0xc0,0x20,0x43,0x10,0x87,0x5b,0x48,0x40,0x43,0xa6,0xba,0xe2,0x30,0x56,0x89,0xa0,0x15,0x5e,0x3f,0x53,0xaf,0x4f,0x2,0xb7,0x49,0x9e,0x13,0xa5,0x92,0xc,0x4c,0x53,0xc4};
	
	//在此輸入開發者後台顯示的lguid
	private const string lguid = "c19fee83-1b89-ba4c-84c2-1c20c0520748";
	
	ArcaletGame ag = null; //宣告ArcaletGame, 此Class將負責與arcalet Server的通訊
	
	
	
	void OnApplicationQuit()
	{
		//當程式關閉時使用者帳號並不會登出,我們可以此加入自動登出的機制
		if(ag!=null) ag.Dispose();		
	}		
	
	void ArcaletLaunch( string username, string password)
	{
		ag = new ArcaletGame(username, password, gguid, sguid, certificate ); //輸入所需要的參數
		ag.onCompletion += CB_ArcaletLaunch; //指定 CallBack function
		ag.Launch(); //啟動 arcalet
	}	
	
	/*******************************************************************
	//	當ArcaletGame執行之後,會在CallBack接收到是否成功登入的訊息
	//	code為0表示登入成功 code非0表示登入失敗
	//	登入失敗可參考 Error Code
	//	http://developer.arcalet.com/tutorials/index.asp?maintitleclass=3&dirid=229
	*******************************************************************/
	void CB_ArcaletLaunch(int code, ArcaletGame game)
	{
		camcrt de = this.GetComponent<camcrt> ();
		state = code;
		if(code==0) {
			//登入成功時執行的區段
			Debug.Log("Login Successed"); 
			de.mod = 1;
			de.paus = !de.paus;

		}
		else {
			//登入失敗時執行的區段
			Debug.LogWarning("Login Failed - Code:" + code); 
		}
	}
	
	
	
	/*******************************************************************
	* 註冊功能必須要有 帳號/密碼/E-Mail
	* 註冊資訊限制可參考以下網址
	* http://developer.arcalet.com/tutorials/index.asp?maintitleclass=3&dirid=229
	* Error Code : 10035~10050
	*******************************************************************/
	void Regist(string username, string password, string mail)
	{
		//在此製作一個Token, 當程式呼叫CallBack時可以再將Token取出
		string[] registToken = new string[] {username, password, mail };	
		ArcaletSystem.ApplyNewUser(gguid, certificate, username, password, 
		                           mail, CB_Regist, registToken);
	}
	
	/*******************************************************************
	* 註冊結果的CallBack
	*******************************************************************/	
	void CB_Regist( int code, object token ) 
	{	
		state = code;
		
		//Code為0表示註冊成功
		if(code == 0) {			
			string[] reg = token as string[]; //取回Token
			string acc = reg[0];
			string pw = reg[1];
			string mail = reg[2];
			Debug.Log("Regist Successed - Account:" + acc + " / Password:" + 
			          pw + " E-Mail" + mail);
			
			
		}		
		//Code非0表示註冊失敗
		else {			
			Debug.LogWarning("Regist Failed - Error:" + code);			
		}
	}
	/*******************************************************************
	*	寫入LeaderBoard需要有lguid
	*******************************************************************/
	void WriteLeaderBoard() 
	{
		score = game.sc.ToString();
		
		ArcaletScore.CommitLeaderBoard( ag, lguid, 0, score, CB_WriteLeaderBoard,"Token-Score");
	}
	
	
	void CB_WriteLeaderBoard(int code, object token) {
		//Code為0表示寫入資料成功
		if(code == 0) {
			string attr = token.ToString();
			Debug.Log("WriteLeaderBoard : " + attr + " Successed");
		}
		//Code非0表示寫入資料失敗
		else {
			Debug.LogWarning("WriteLeaderBoard Failed - Error:" + code);
		}
	}
	
	/*******************************************************************
	*	讀取LeaderBoard需要有lguid
	*******************************************************************/
	
	void LoadLeaderBoard() {
		ArcaletScore.GetLeaderBoard(ag, lguid, 0, 0, CB_LoadLeaderBoard, null);
	}
	
	void CB_LoadLeaderBoard(int code, object data, object token) {
		//Code為0表示讀取排行榜成功
		if(code == 0) {			
			List<Hashtable> list = data as List<Hashtable>;			
			Debug.Log("LoadLeaderBoard Successed - LeaderBoard Count : " + list.Count);
			
			int length = list.Count;
			length = Mathf.Clamp(length, 0, leaderBoard.Length);
			
			for(int i=0; i < length; i++) {
				leaderBoard[i].name = list[i]["userid"].ToString();
				leaderBoard[i].score = list[i]["score"].ToString();
			}			
		}
		//Code非0表示讀取排行榜失敗
		else {
			Debug.LogWarning("LoadLeaderBoard Failed - Error:" + code);
		}
	}
	#endregion
	// Use this for initialization
	void Start ()
	{		
		ArcaletSystem.UnityEnvironment();
		t = data.nums;
	}
	
	// Update is called once per frame
	void Update ()
	{
		tt = data.s;
		OnGUI ();
		
	}
	
	void OnGUI ()
	{
		camcrt de = this.GetComponent<camcrt> ();
		if (!game.death) {
			if (de.mod == 0) {
				if (tt != t) {
					GUI.color = Color.blue;
					GUI.Label (new Rect (Screen.width * 45 / 100, Screen.height * 50 / 100, 100, 20), "Loading..." + 100 * tt / t + "%");
				} else {
					
					if(login){
						GUI.Box (new Rect (Screen.width * 25 / 100, Screen.height * 25 / 100, Screen.width * 50 / 100, Screen.height * 50 / 100), "Login Menu");
						GUI.Label(new Rect( Screen.width * 32 / 100, Screen.height * 40 / 100, Screen.width * 25 / 100, Screen.height * 5 / 100), "Account");
						GUI.Label(new Rect( Screen.width * 32 / 100, Screen.height * 50 / 100, Screen.width * 25 / 100, Screen.height * 5 / 100), "Password");
						
						switch(state){
							
						case 103:
							GUI.Label(new Rect( Screen.width * 40 / 100, Screen.height * 30 / 100, Screen.width * 30 / 100, Screen.height * 5 / 100), "Account or Password Error");
							break;	
							
						case 102:
							GUI.Label(new Rect( Screen.width * 40 / 100, Screen.height * 30 / 100, Screen.width * 30 / 100, Screen.height * 5 / 100), "The Account is occupied");
							break;
							
						case 99:
							GUI.Label(new Rect( Screen.width * 40 / 100, Screen.height * 30 / 100, Screen.width * 30 / 100, Screen.height * 5 / 100), "Exceptions or connection timeout");
							break;
							
						case 101:
							GUI.Label(new Rect( Screen.width * 40 / 100, Screen.height * 30 / 100, Screen.width * 30 / 100, Screen.height * 5 / 100), "Sign reached the maximum number of games");
							break;
							
							
						default:
							GUI.Label(new Rect( Screen.width * 40 / 100, Screen.height * 30 / 100, Screen.width * 30 / 100, Screen.height * 5 / 100), "Please input Account & Password");
							break;	
						}
						
						str_acc = GUI.TextField(new Rect(Screen.width * 40 / 100, Screen.height * 40 / 100, Screen.width * 25 / 100, Screen.height * 5 / 100), str_acc, 10);
						str_pw = GUI.PasswordField(new Rect(Screen.width * 40 / 100, Screen.height * 50 / 100, Screen.width * 25 / 100, Screen.height * 5 / 100), str_pw, "*"[0], 20);
						
						if( GUI.Button(new Rect(Screen.width * 40 / 100, Screen.height * 60 / 100, Screen.width * 10 / 100, Screen.height * 7 / 100), "Login")) {
							ArcaletLaunch(str_acc, str_pw); //開始登入
						}
						
						if( GUI.Button(new Rect(Screen.width * 55 / 100, Screen.height * 60 / 100, Screen.width * 10 / 100, Screen.height * 7 / 100), "Regist")) {
							login = false; 
						}
					}else{
						
						GUI.Box( new Rect(Screen.width * 25 / 100, Screen.height * 15 / 100, Screen.width * 50 / 100, Screen.height * 70 / 100), "Regist Menu");
						GUI.Label(new Rect( Screen.width * 30 / 100, Screen.height * 25 / 100, Screen.width * 25 / 100, Screen.height * 5 / 100), "Account");
						GUI.Label(new Rect( Screen.width * 30 / 100, Screen.height * 35 / 100, Screen.width * 25 / 100, Screen.height * 5 / 100), "Password");
						GUI.Label(new Rect( Screen.width * 30 / 100, Screen.height * 45 / 100, Screen.width * 25 / 100, Screen.height * 5 / 100), "Confirm Password");
						GUI.Label(new Rect( Screen.width * 30 / 100, Screen.height * 55 / 100, Screen.width * 25 / 100, Screen.height * 5 / 100), "E-Mail");
						GUI.Label(new Rect( Screen.width * 30 / 100, Screen.height * 65 / 100, Screen.width * 25 / 100, Screen.height * 5 / 100), "Confirm E-Mail");
						
						str_acc = GUI.TextField(new Rect(Screen.width * 45 / 100, Screen.height * 24 / 100, Screen.width * 25 / 100, Screen.height * 5 / 100), str_acc, 10);
						str_pw = GUI.PasswordField(new Rect(Screen.width * 45 / 100, Screen.height * 34 / 100, Screen.width * 25 / 100, Screen.height * 5 / 100), str_pw, "*"[0], 18);
						str_pw2 = GUI.PasswordField(new Rect(Screen.width * 45 / 100, Screen.height * 44 / 100, Screen.width * 25 / 100, Screen.height * 5 / 100), str_pw2, "*"[0], 18);
						str_mail = GUI.TextField(new Rect(Screen.width * 45 / 100, Screen.height * 54 / 100, Screen.width * 25 / 100, Screen.height * 5 / 100), str_mail, 50);
						str_mail2 = GUI.TextField(new Rect(Screen.width * 45 / 100, Screen.height * 64 / 100, Screen.width * 25 / 100, Screen.height * 5 / 100), str_mail2, 50);
						
						
						switch(state){
							
						case 10035:
							GUI.Label(new Rect( Screen.width * 40 / 100, Screen.height * 20 / 100, Screen.width * 30 / 100, Screen.height * 5 / 100), "Blank Account");
							break;	
							
						case 10036:
							GUI.Label(new Rect( Screen.width * 40 / 100, Screen.height * 20 / 100, Screen.width * 30 / 100, Screen.height * 5 / 100), "Account is too short, 3~10 characters");
							break;
							
						case 10037:
							GUI.Label(new Rect( Screen.width * 40 / 100, Screen.height * 20 / 100, Screen.width * 30 / 100, Screen.height * 5 / 100), "Account is too long, 3~10 characters");
							break;
							
						case 10038:
							GUI.Label(new Rect( Screen.width * 40 / 100, Screen.height * 20 / 100, Screen.width * 30 / 100, Screen.height * 5 / 100), "Account only use 0~9 or a~Z");
							break;
							
						case 10039:
							GUI.Label(new Rect( Screen.width * 40 / 100, Screen.height * 20 / 100, Screen.width * 30 / 100, Screen.height * 5 / 100), "Account only 0~9 or a~Z begin and end");
							break;
							
						case 10040:
							GUI.Label(new Rect( Screen.width * 40 / 100, Screen.height * 20 / 100, Screen.width * 30 / 100, Screen.height * 5 / 100), "Account is not allowed '-' And '_'");
							break;
							
						case 10041:
							GUI.Label(new Rect( Screen.width * 40 / 100, Screen.height * 20 / 100, Screen.width * 30 / 100, Screen.height * 5 / 100), "Account does not allow continuous use of '-' or '.'");
							break;
							
						case 10042:
							GUI.Label(new Rect( Screen.width * 40 / 100, Screen.height * 20 / 100, Screen.width * 30 / 100, Screen.height * 5 / 100), "Blank Password");
							break;
							
						case 10043:
							GUI.Label(new Rect( Screen.width * 40 / 100, Screen.height * 20 / 100, Screen.width * 30 / 100, Screen.height * 5 / 100), "Password is too short, 6~18 characters");
							break;
							
						case 10044:
							GUI.Label(new Rect( Screen.width * 40 / 100, Screen.height * 20 / 100, Screen.width * 30 / 100, Screen.height * 5 / 100), "Password is too long, 6~18 characters");
							break;
							
						case 10045:
							GUI.Label(new Rect( Screen.width * 40 / 100, Screen.height * 20 / 100, Screen.width * 30 / 100, Screen.height * 5 / 100), "Game password can not use double quotation marks or whitespace");
							break;
							
						case 10046:
							GUI.Label(new Rect( Screen.width * 40 / 100, Screen.height * 20 / 100, Screen.width * 30 / 100, Screen.height * 5 / 100), "Blank E-mail");
							break;
							
						case 10047:
							GUI.Label(new Rect( Screen.width * 40 / 100, Screen.height * 20 / 100, Screen.width * 30 / 100, Screen.height * 5 / 100), "E-mail malformed");
							break;
							
						case 10048:
							GUI.Label(new Rect( Screen.width * 40 / 100, Screen.height * 20 / 100, Screen.width * 30 / 100, Screen.height * 5 / 100), "E-mail has exceeded the upper limit of the number of registered");
							break;
							
						case 10049:
							GUI.Label(new Rect( Screen.width * 40 / 100, Screen.height * 20 / 100, Screen.width * 30 / 100, Screen.height * 5 / 100), "This account already exists");
							break;
							
						case 10050:
							GUI.Label(new Rect( Screen.width * 40 / 100, Screen.height * 20 / 100, Screen.width * 30 / 100, Screen.height * 5 / 100), "Account must not contain indecent characters");
							break;
							
						default:
							GUI.Label(new Rect( Screen.width * 40 / 100, Screen.height * 20 / 100, Screen.width * 30 / 100, Screen.height * 5 / 100), "Please input data");
							break;	
						}
						
						if( GUI.Button(new Rect(Screen.width * 60 / 100, Screen.height * 75 / 100, Screen.width * 10 / 100, Screen.height * 7 / 100), "Back")) {
							login = true; //開始登入
						}
						
						if( GUI.Button(new Rect(Screen.width * 45 / 100, Screen.height * 75 / 100, Screen.width * 10 / 100, Screen.height * 7 / 100), "Regist")) {
							
							//檢驗帳號
							if(str_pw != str_pw2) {
								Debug.LogWarning("Please Confirm Your Password Information");
								return;
							}
							
							//檢驗E-mail
							if(str_mail != str_mail2) {
								Debug.LogWarning("Please Confirm Your E-mail Information");
								return;
							}
							
							Regist(str_acc, str_pw, str_mail); //開始註冊
						}
					}
					
					
					
					
				}
			} else {
				if (de.paus) {
					GUI.Box (new Rect (Screen.width * 20 / 100, Screen.height * 40 / 100, Screen.width * 60 / 100, Screen.height * 20 / 100), "Paus Menu");
					if (GUI.Button (new Rect (Screen.width * 37.5f / 100, Screen.height * 47.5f / 100, Screen.width * 25 / 100, Screen.height * 5 / 100), "Return Game", "Click")) {
						de.paus = !de.paus;
					}
					if (GUI.Button (new Rect (Screen.width * 37.5f / 100, Screen.height * 52.5f / 100, Screen.width * 25 / 100, Screen.height * 5 / 100), "Restart", "Click")) {
						
						if(ag!=null) ag.Dispose();	
						Application.LoadLevel ("fight");
					}
				}
			}
		} else {			
			de.paus = true;
			if(rank){
				WriteLeaderBoard();
				LoadLeaderBoard();
				rank = false;
			}
			
			int len = leaderBoard.Length;
			GUI.Box( new Rect(Screen.width * 20 / 100, Screen.height * 10 / 100, Screen.width * 60 / 100, Screen.height * 80 / 100), "Rank");
			if (GUI.Button (new Rect (Screen.width * 37.5f / 100, Screen.height * 80 / 100, Screen.width * 25 / 100, Screen.height * 5 / 100), "Try Again")) {
				
				if(ag!=null) ag.Dispose();	
				Application.LoadLevel ("fight");
			}
			for(int i= 0; i<len; i++) {
				GUI.Label(new Rect(Screen.width * 35 / 100, Screen.height * (0.2f +0.06f * i), Screen.width * 25 / 100, Screen.height * 5 / 100), "第"+(i+1)+"名  ");
				GUI.Label(new Rect(Screen.width * 45 / 100, Screen.height * (0.2f +0.06f * i), Screen.width * 25 / 100, Screen.height * 5 / 100), leaderBoard[i].name);
				GUI.Label(new Rect(Screen.width * 60 / 100, Screen.height * (0.19f + 0.06f * i), Screen.width * 25 / 100, Screen.height * 5 / 100), leaderBoard[i].score);
				
			}
			
			
			
		}
	}
	
	
}

[System.Serializable]
public class LeaderBoard 
{
	public string name = "";
	public string score = "";	
}



