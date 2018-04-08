using UnityEngine;
using System.Collections;

public class lokat : MonoBehaviour {
	public Vector3 lok;
	// Use this for initialization
	void Start () {
		lok.x = 0f;
		lok.y = 0f;
		lok.z = 0f;
		this.transform.LookAt (lok);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
