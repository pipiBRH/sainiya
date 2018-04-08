using UnityEngine;
using System.Collections;

public class item_data : MonoBehaviour {
	public int id;
	public string s;
	public GameObject[] effect;


	// Use this for initialization
	void Start () {	
	}
	
	// Update is called once per frame
	void Update () {
		if (this.transform.position.y > 0f) {
			this.transform.Translate(0f,-0.1f,0f,Space.World);
				}

	}
	void OnTriggerEnter(Collider a){
		if (a.tag == "Player") {			
			Quaternion z=new Quaternion();
			states data=a.GetComponent<states>();
			data.plus[id]++;
			data.plu=true;
			data.plus[2]=Mathf.Min(data.plus[2],4);
			if(id==2){
				data.plu1=true;
			}
			if(id!=1){
				if(data.plus[id]>1){
					Instantiate (effect[id], a.transform.position,z);
				}else{
					Instantiate (effect[Mathf.Min(Mathf.Max(id-1,0),2)], a.transform.position,z);
				}
			}			
			DestroyObject(this.gameObject);
		}
	}
}
