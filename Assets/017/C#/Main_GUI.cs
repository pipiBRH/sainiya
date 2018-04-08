using UnityEngine;
using System.Collections;

public class Main_GUI : MonoBehaviour {

	//public int ;
	public float flhp,hp,mhp,wd,s1cd,s2cd,scf,z,s;
	public int sc,h,m,mlit,scor,damg;
	public bool hurt=false,death=false;
	public Texture nhpd, hpd, sk1, sk11, sk2, sk22,timer,coin;
	public Texture[] number,scors;
	public Font a;
	public ogrecrt og;
	public camcrt date=new camcrt();
	// Use this for initialization
	void Start () {
		flhp = hp;
		damg = 1;
		s = Screen.width / 10;
	}
	
	// Update is called once per frame
	void Update () {
		if (!date.paus) {
			if(!death){
				scf+=Time.deltaTime;
				if (hurt) {
					wd += Time.deltaTime;
				}
						if (wd >=0.55f) {
								hurt = false;
								wd = 0f;
						}
						
						if(scf>1f){
							scf=0;
							sc++;
						}
				if (sc >= 60) {
					scor += 50;
					m ++;
					sc = 0;
					if(m>=60){
						h++;
						h=Mathf.Min(h,99);
					}
				}


						cd1 ();
						cd2 ();
			}
		}
	}
	void cd1(){
		if (s1cd > 0f)
						s1cd -= Time.deltaTime;
				else
						s1cd = 0f;
		}
	void cd2(){
		if (s2cd > 0f)
			s2cd -= Time.deltaTime;
		else
			s2cd = 0f;
	}
	void OnGUI(){
		if (!date.paus) {
			if(!death){
			states x=this.GetComponent<states>();
				if(x.plus[0]>0){
					if(x.plu){
						x.plu=false;
						x.plus[0]--;
						hp=Mathf.Min(hp+20,100);
					}
				}
				if(x.plus[1]>0){
					if(x.plu){
						x.plu=false;
						x.plus[1]--;
						scor=Mathf.Min(scor+500,999999);
					}
				}
			
						if (!nhpd || !hpd) {
								Debug.LogError ("Assign a Texture in the inspector.");
								return;
						}
						GUI.DrawTexture (new Rect (0, Screen.height * 95 / 100, 100 * flhp / mhp, 10), hpd, ScaleMode.StretchToFill, false, 10.0f);
						GUI.DrawTexture (new Rect (0, Screen.height * 95 / 100, 100 * hp / mhp, 10), nhpd, ScaleMode.StretchToFill, true, 10.0f);
						GUI.DrawTexture (new Rect (Screen.width / 100, Screen.height * 78 / 100, 35, 35), sk11, ScaleMode.StretchToFill, true, 10.0f);
						GUI.DrawTexture (new Rect (0, Screen.height * 88 / 100, 100 * (2 - s1cd) / 2, 10), sk1, ScaleMode.StretchToFill, true, 10.0f);
						GUI.DrawTexture (new Rect (Screen.width / 100, Screen.height * 62 / 100, 35, 35), sk22, ScaleMode.StretchToFill, true, 10.0f);
						GUI.DrawTexture (new Rect (0, Screen.height * 72 / 100, 100 * (3 - s2cd) / 3, 10), sk2, ScaleMode.StretchToFill, true, 10.0f);
			GUI.DrawTexture (new Rect (Screen.width * 0 / 100, Screen.height * z / 100, 40, 40), coin, ScaleMode.StretchToFill, true, 10.0f);
						//GUI.DrawTexture (new Rect (Screen.width * 48 / 100, 5f, 30, 30), timer, ScaleMode.StretchToFill, true, 10.0f);
			GUI.DrawTexture (new Rect (Screen.width * 55 / 100-78, 0f, 40, 50), number[Mathf.Min(h/10,9)], ScaleMode.StretchToFill, true, 10.0f);
			GUI.DrawTexture (new Rect (Screen.width * 55 / 100-52, 0f, 40, 50), number[h%10], ScaleMode.StretchToFill, true, 10.0f);
			GUI.DrawTexture (new Rect (Screen.width * 55 / 100-26, -20f, 40, 50), number[10], ScaleMode.StretchToFill, true, 10.0f);
			GUI.DrawTexture (new Rect (Screen.width * 55 / 100-26, 0f, 40, 50), number[10], ScaleMode.StretchToFill, true, 10.0f);
			GUI.DrawTexture (new Rect (Screen.width * 55 / 100, 0f, 40, 50), number[m/10], ScaleMode.StretchToFill, true, 10.0f);
			GUI.DrawTexture (new Rect (Screen.width * 55 / 100+26, 0f, 40, 50), number[m%10], ScaleMode.StretchToFill, true, 10.0f);
			GUI.DrawTexture (new Rect (Screen.width * 55 / 100+52, -20f, 40, 50), number[10], ScaleMode.StretchToFill, true, 10.0f);
			GUI.DrawTexture (new Rect (Screen.width * 55 / 100+52, 0f, 40, 50), number[10], ScaleMode.StretchToFill, true, 10.0f);
			GUI.DrawTexture (new Rect (Screen.width * 55 / 100+78, 0f, 40, 50), number[sc/10], ScaleMode.StretchToFill, true, 10.0f);
			GUI.DrawTexture (new Rect (Screen.width * 55 / 100+104, 0f, 40, 50), number[sc%10], ScaleMode.StretchToFill, true, 10.0f);
						if (flhp > hp) {
								flhp = flhp - Time.deltaTime * Mathf.Max (flhp - hp, 2);
								GUI.DrawTexture (new Rect (0, Screen.height * 95 / 100, 100 * flhp / mhp, 10), hpd, ScaleMode.StretchToFill, true, 10.0f);
						} else {
								flhp = hp;
						}
			GUI.DrawTexture (new Rect (Screen.width * 7 / 100+130, 0f, 30, 43), scors[scor%10], ScaleMode.StretchToFill, true, 10.0f);
			GUI.DrawTexture (new Rect (Screen.width * 7 / 100+104, 0f, 30, 43), scors[(scor/10)%10], ScaleMode.StretchToFill, true, 10.0f);
			GUI.DrawTexture (new Rect (Screen.width * 7 / 100+78, 0f, 30, 43), scors[(scor/100)%10], ScaleMode.StretchToFill, true, 10.0f);
			GUI.DrawTexture (new Rect (Screen.width * 7 / 100+52, 0f, 30, 43), scors[(scor/1000)%10], ScaleMode.StretchToFill, true, 10.0f);
			GUI.DrawTexture (new Rect (Screen.width * 7 / 100+26, 0f, 30, 43), scors[(scor/10000)%10], ScaleMode.StretchToFill, true, 10.0f);
			GUI.DrawTexture (new Rect (Screen.width * 7 / 100, 0f, 30, 43), scors[Mathf.Min(scor/100000,9)], ScaleMode.StretchToFill, true, 10.0f);
						GUI.color = Color.black;
						GUI.skin.font = a;
						GUI.Label (new Rect (Screen.width / 100, Screen.height * 90 / 100, 100, 20), "Hp");
						//GUI.Label (new Rect (Screen.width * 15 / 100, 7.5f, 100, 20), "" + scor);
						//GUI.Label (new Rect (Screen.width * 55 / 100, 7.5f, 100, 20), m + ":" + sc);						
			}
		}

	}
}
