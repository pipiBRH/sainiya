using UnityEngine;
using System.Collections;

public class camcrt : MonoBehaviour {
	public GameObject player=null;
	public float z,t,tt;
	public bool paus=false;
	public int s=0,mod=0,sui=0;
	public Vector3 vi,ui;
	// Use this for initialization
	void Start () {
		s = 0;
		z = 17f;
		paus = true;
		ui.x = 0;
		ui.y = 17f;
		ui.z = 0;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.F1)&&mod==1) {
			paus=!paus;
			vi=this.transform.position;
			/*if(paus){
				Time.timeScale = 0;
			}else{
				Time.timeScale = 1;
			}*/


		}
		if (!paus) {
			if(s<40){
				tt+=Time.deltaTime;
				if(tt>=0.01f){
					tt=0f;
				this.transform.Rotate(2f,0f,0f);
					s++;}
			}
						//this.transform.LookAt(player.transform.position);
			if(z<18f&& z>14f){
				z-=Time.deltaTime*3f;
				this.transform.Translate (0, -3f * Time.deltaTime, 0, Space.World);
			}
						if (Input.GetKey (KeyCode.KeypadPlus)) {
								if (z <= 14f) {
										z += Time.deltaTime * 3f;
										this.transform.Translate (0, 3f * Time.deltaTime, 0, Space.World);
								}
						}
						if (Input.GetKey (KeyCode.KeypadMinus)) {
								if (z >= 7f) {
										z -= Time.deltaTime * 3f;
										this.transform.Translate (0, -3f * Time.deltaTime, 0, Space.World);
								}
						}
						t = Vector3.Distance (player.transform.position, this.transform.position);
						if (t > z - 0.03f || t > z + 0.03f)
								act ();
				} else {
			act();
			if(s>0){
				tt+=Time.deltaTime;
				if(tt>=0.01f){
					tt=01f;
				this.transform.Rotate(-2f,0f,0f);
					s--;}
			}
				}

	}
	void act(){
				float c = 0.075f;
				if (mod == 0) {
						if (sui < 10) {
								if (this.transform.position.z < ui.z) {
										this.transform.Translate (0, 0, Mathf.Abs (ui.z - vi.z) / 10f, Space.World);
								}
								if (this.transform.position.z > ui.z) {
										this.transform.Translate (0, 0, -1 * Mathf.Abs (ui.z - vi.z) / 10f, Space.World);
								}
								if (this.transform.position.x < ui.x) {
										this.transform.Translate (Mathf.Abs (ui.x - vi.x) / 10f, 0, 0, Space.World);
								}
								if (this.transform.position.x > ui.x) {
										this.transform.Translate (-1 * Mathf.Abs (ui.x - vi.x) / 10f, 0, 0, Space.World);
								}
								sui++;
						}
				} else {
						if (sui > 0) {
				if (this.transform.position.z > ui.z) {
					this.transform.Translate (0, 0, Mathf.Abs (ui.z - vi.z) / 10f, Space.World);
				}
				if (this.transform.position.z < ui.z) {
					this.transform.Translate (0, 0, -1 * Mathf.Abs (ui.z - vi.z) / 10f, Space.World);
				}
				if (this.transform.position.x > ui.x) {
					this.transform.Translate (Mathf.Abs (ui.x - vi.x) / 10f, 0, 0, Space.World);
				}
				if (this.transform.position.x < ui.x) {
					this.transform.Translate (-1 * Mathf.Abs (ui.x - vi.x) / 10f, 0, 0, Space.World);
				}
				sui--;
						} else {
								if (this.transform.position.z < player.transform.position.z) {
										this.transform.Translate (0, 0, c, Space.World);
								}
								if (this.transform.position.z > player.transform.position.z) {
										this.transform.Translate (0, 0, -1 * c, Space.World);
								}
								if (this.transform.position.x < player.transform.position.x) {
										this.transform.Translate (c, 0, 0, Space.World);
								}
								if (this.transform.position.x > player.transform.position.x) {
										this.transform.Translate (-1 * c, 0, 0, Space.World);
								}
						}
				}
		}

}
