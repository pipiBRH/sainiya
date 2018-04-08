using UnityEngine;
using System.Collections;

public class mapcrt : MonoBehaviour {
	public float r,sc;
	public int nums,s;
	public GameObject wall;
	public Vector3 v3;
	public Main_GUI hp;
	// Use this for initialization
	void Start () {
		v3.y = 0;
	}
	// Update is called once per frame
	void Update () {
		if (s < nums) {
			sc += Time.deltaTime;
			if (sc > 0.3f/(s+1)) {
				v3.x = Mathf.Sin (s * 360 / nums) * r;
				v3.z = Mathf.Cos (s * 360 / nums) * r;
				Instantiate (wall, v3, this.transform.rotation);
				s++;
				sc = 0f;
						}
		}				
	}
	void OnTriggerStay(Collider a){
				if (hp.hp == 0) {
						if (a.tag == "ogre") {
								DestroyObject (a.gameObject);
						}
				}
	}

}
