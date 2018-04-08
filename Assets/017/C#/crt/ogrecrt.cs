using UnityEngine;
using System.Collections;

public class ogrecrt : MonoBehaviour
{
	public float atkt,actin,actcd,hur,hurcd,dis,deat,hp=0f,plus,mix,p;
	public string asd;
	public string[] act,ogrename=new string[3];
	public Vector3 mod,goa;
	public int rand=0,damg=5,ogreid,bktime=100;
	public bool atking=false,hurt=false,paus=true,goal,drop=true,dead=false,bk=false;
	public GameObject elm=null;
	public GameObject[] it,tkit;
	public Main_GUI ply;
			// Use this for initialization
		void Start ()
		{
		mod = this.transform.position;
		deat = 1f;
		act = new string[5];
		int i = 0;
		for (i=0; i<5; i++) {
			act[i]=ogrename[ogreid];
				}
		damg *= (1+ogreid);
		setact (act);
		hurcd = 0.2f;
		hur = hurcd;
		actcd = 1.0f;
		actin = actcd;
		atkt = 0.45f;
		}
	void setact(string[] b){
		b[0] += "-Attack1";
		b[1] += "-Death";
		b[2] += "-Idle";
		b[3] += "-TakeDamage2";
		b[4] += "-Walk";
	}
		// Update is called once per frame
	void Update (){
		mix += Time.deltaTime;
		if (elm != null) {
			ply=elm.GetComponent<Main_GUI> ();
			paus=ply.date.paus;
				}
		if (!paus) {
			if(mix>0.01f){
				mix=0f;					
						if (elm != null) {
							ply=elm.GetComponent<Main_GUI> ();
								
						}
				if(bk){
					back();
				}
				else{
				if (goal) {
					way ();
				}
				}
						if (hp <= 0f) {
							anima (1);
							deat -= Time.deltaTime;
					if (!dead&&deat<=0f){
						ply.scor += 10 * (ogreid + 1);
						dead=true;
						drops();
						DestroyObject (this.gameObject);								
						}
				}
			}
				}					
	}
	void OnTriggerStay(Collider a){
		float dis = Vector3.Distance (a.transform.position, this.transform.position);
		asd = a.name;
		if (dis <= 1.0f&&elm!=null) {			
			bomcrt b = a.GetComponent<bomcrt>();
			states h = elm.GetComponent<states> ();
			if (a.gameObject.tag == "bom" && !hurt) {
				hurt=true;
				hp -= ply.damg*Mathf.Max(h.plus[2],1);
				hp =Mathf.Max(hp,0);
				anima (3);
				Instantiate (tkit[Random.Range(0,1)], this.transform.position, this.transform.rotation);
				DestroyObject(a.gameObject);
			}
			if (a.gameObject.tag == "Player" && atking) {
				ply = a.GetComponent<Main_GUI> ();
				if (!ply.hurt && atking) {
					Instantiate (tkit[2], a.transform.position, a.transform.rotation);
					ply.hp -= damg;
					ply.hp =Mathf.Max(ply.hp,0);
					ply.hurt=true;
					ply.wd=0f;
					h.plus[2]=Mathf.Max(h.plus[2]-1,0);
					atkt=-0.75f;
					atking = false;
				}
			}
		}
	}


	void drops(){
		if (drop) {
			if(Random.Range(1,5-ogreid)==1){
				Instantiate(it[Random.Range(0,ogreid)],this.transform.position,this.transform.localRotation);
			}
			if(ogreid==2){
				Vector3 zxc=this.transform.position;
				zxc.z+=1f;
				zxc.x+=1f;
				Instantiate(it[2],zxc,this.transform.localRotation);
			}
			drop=!drop;
				}
		}             

	void anima(int a){
		animation.CrossFade (act[a]);
	}

	void way(){
		dis = Vector3.Distance (this.transform.position, mod);
		if (dis > 15f) {
			bk=true;
			bktime=0;
		}
		actin-=Time.deltaTime;
		if (hurt) {
			anima(3);
			hur-=Time.deltaTime;
			if(hur<0f){
				this.transform.Translate(0f,0f,-0.01f,Space.Self);
				hurt=false;
				hur=hurcd;
			}
		}
		else{
			dis=Vector3.Distance(this.transform.position,elm.transform.position);
			if(dis<1.0f&& atkt>0){
				atkt-=Time.deltaTime;
				anima(0);
				atking=true;
			}
			else{
				if(dis<1.0f){
					Vector3 r=goa;
					r.x+=Random.Range(-5,5);
					r.z+=Random.Range(-5,5);
					this.transform.LookAt(r);
				}
				atkt+=Time.deltaTime;
				if(atkt>actcd*0.45f){
					atkt=0.45f;
				}
				anima(4);
				atking=false;
				if(actin<0.01f){
					goa=elm.transform.position;
					if(Random.Range(1,2)==2){
						
					}
					this.transform.LookAt(goa);
					actin=actcd;
				}
				else{
					this.transform.Translate(0f,0f,0.076f,Space.Self);
				}
			}
		}
	}

	void back(){
		p += Time.deltaTime;
		if (p > 0.01f) {
			p=0f;
			if (bktime == 0) {
				dis = Vector3.Distance (this.transform.position, mod);
			}
			if (bktime < 100) {
				bktime++;
				this.transform.LookAt (mod);
				this.transform.Translate (0f, 0f, dis / 100, Space.Self);
			}
			if(bktime==100){
				bk=false;
			}
		}
	}

}

