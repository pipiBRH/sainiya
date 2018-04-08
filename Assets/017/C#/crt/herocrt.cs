using UnityEngine;
using System.Collections;

public class herocrt : MonoBehaviour {
	// Use this for initialization
	public mouse goal;
	public camcrt date;
	//public Animator am;
	//public Animation am;
	public GameObject skil00,skil01,skil02;
	public Main_GUI gui;
	public string[] acting;
	public float pt,speed,CD,delay,adely,range,mix;
	public Vector3 st;
	void Start () {
		speed = CD;
		//am =this.GetComponent<Animation>();
		delay = 0;
		}

	// Update is called once per frame
	void Update () {
		mix += Time.deltaTime;
		if (!date.paus) {
			if(!gui.death){
				if(gui.hurt&&gui.wd<0.15f){
					anima(4);
					if(gui.hp==0){
						anima(5);
						gui.death=true;
					}
					if (Input.GetKey (KeyCode.Alpha1)) {
						if (gui.s1cd == 0f) {
							states x=this.GetComponent<states>();
							Vector3 z=this.transform.position;
							z.y=-0.75f;
							adely += 0.6f;
							anima (3);
							Instantiate (skil01, z, this.transform.rotation);
							gui.s1cd = 2f*(1-x.plus[2]*0.2f);
						}
					}
				}
				else{
				if(mix>0.01f){
					states x=this.GetComponent<states>();
					if(x.plu1&&x.plu){
						x.plu=false;
						x.plu1=false;
					}

					mix=0f;
			range=Vector3.Distance(this.transform.position,st);
						if (speed < CD) {
								speed = speed + Time.deltaTime;
						}
						if (!animation.isPlaying)
								animation.Play ();
						if (this.transform.position.y > 0.01f) {
								this.transform.Translate (0, -2.5f * Time.deltaTime, 0, Space.World);
						}
						this.transform.LookAt (goal.mous);		
						delay += Time.deltaTime;
						if (delay >= 1.5f) {			
								delay -= 1.5f;
								anima (0);
						}
						if (Input.GetKey (KeyCode.Alpha1)) {
								if (gui.s1cd == 0f) {
							Vector3 z=this.transform.position;
							z.y=-0.75f;
										adely += 0.6f;
										anima (3);
										Instantiate (skil01, z, this.transform.rotation);
							gui.s1cd = 2f*(1-x.plus[2]*0.2f);
								}
						}
						if (Input.GetKey (KeyCode.Alpha2)) {
								if (gui.s2cd == 0f) {						
							Vector3 z=goal.mous;
							z.y=1.55f;
										adely += 0.6f;
										anima (3);
										Instantiate (skil02, z, this.transform.rotation);
							gui.s2cd = 3f*(1-x.plus[2]*0.2f);
								}
						}

					if (Input.GetKey (KeyCode.Space)) {
						if (speed >= CD*(1-x.plus[2]*0.2f)) {
							adely+=0.7f;
							anima (2);
							speed =0;
							Instantiate (skil00, this.transform.position, this.transform.rotation);
						} else {
						}
					}

						if (adely >= 0)
								adely -= Time.deltaTime;
			if(range<=54.5){act (0);
			}
			else{act (1);
			}
			}
			}
			}
				}

	}

	void anima(int a){
		if (adely > 0 && a == 1) {
						;
				} else {
						animation.CrossFade (acting [a]);
				}
		}
	void act(int z){
		float c = 0.075f;
			if (z == 0) {
						if (Input.GetKey (KeyCode.W)) {
								anima (1);
								this.transform.Translate (0, 0, c, Space.World);
						}
						if (Input.GetKey (KeyCode.S)) {
								anima (1);
								this.transform.Translate (0, 0, -1 * c, Space.World);
						}
						if (Input.GetKey (KeyCode.D)) {
								anima (1);
								this.transform.Translate (c, 0, 0, Space.World);
						}
						if (Input.GetKey (KeyCode.A)) {
								anima (1);
								this.transform.Translate (-1 * c, 0, 0, Space.World);
						}
				} else {
			if(z==1){
				if (this.transform.position.x>st.x) {
				this.transform.Translate(-1*c,0,0,Space.World);
				}
				if (this.transform.position.x<st.x) {
					this.transform.Translate(c,0,0,Space.World);
				}
				if (this.transform.position.z>st.z) {
					this.transform.Translate(0,0,-1*c,Space.World);
				}
				if (this.transform.position.z<st.z) {
					this.transform.Translate(0,0,c,Space.World);
				}
			}

				}
				

	}

	}

