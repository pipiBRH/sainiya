using UnityEngine;
using System.Collections;

public class monstercrt : MonoBehaviour {
	public float hp=0f,action,actcd,hur,hurcd,mix,dis;
	public bool atking=false,hurt=false,paus=true,goal,drop=true,dead=false;
	public Vector3 mod,goa;
	public GameObject elm;
	// Use this for initialization
	void Start () {
		mod = this.transform.position;
		hurcd = 0.2f;
		hur = hurcd;
		actcd = 0.75f;
		action = actcd;
	}
	
	// Update is called once per frame
	void Update () {
		mix += Time.deltaTime;
		if (mix > 0.01f) {
			mix=0f;
			if (goal) {
				way ();
			}
		}
	}
	void way(){
		dis = Vector3.Distance (this.transform.position, mod);
		if (dis > 35f) {
			goal=false;
				}
		action-=Time.deltaTime;
		if (hurt) {
			hur-=Time.deltaTime;
			if(hur<0f){
				hp-=1f;
				this.transform.Translate(0f,0f,-0.01f,Space.Self);
				if(hp==0){
					dead=true;
					Destroy(this.gameObject);
				}
				hurt=false;
				hur=hurcd;
			}
		}
		else{
			if(action<0.01f){
				goa=elm.transform.position;
				this.transform.LookAt(goa);
				action=actcd;
			}
			else{
				this.transform.Translate(0f,0f,0.08f,Space.Self);
			}
		}

	}

}
