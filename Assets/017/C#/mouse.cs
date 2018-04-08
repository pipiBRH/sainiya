using UnityEngine;
using System.Collections;

public class mouse : MonoBehaviour {
	public Vector3 mous;
	RaycastHit[] hits;
	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		hits=Physics.RaycastAll(ray);

		for(int i=0;i<hits.Length;i++){
			if(hits[i].collider.tag == "Ground")
			mous =hits[i].point;
			mous.Set(mous.x,0f,mous.z);
			}
	}
}
