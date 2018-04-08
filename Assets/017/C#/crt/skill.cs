using UnityEngine;
using System.Collections;

public class skill : MonoBehaviour {
	public int id;
	public float lif,efc;   
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		efc = efc+Time.deltaTime;
		lif += Time.deltaTime;
		if (efc > 0.01f) {
			if(id==2){
				if (this.transform.position.y > 0.1f) {
					this.transform.Translate(0f,-0.1f,0f,Space.World);
				}
			}else{
				if (this.transform.position.y < -0.06f) {
					this.transform.Translate(0f,0.06f,0f,Space.World);
				}
			}
			efc=0f;
			if (lif > 0.75f){
				DestroyObject (this.gameObject);
			}
		}
	}
}
