using UnityEngine;
using System.Collections;

public class bomcrt : MonoBehaviour {
	public int kind;
	public float t;
	// Use this for initialization
	void Start () {

	}
	// Update is called once per frame
	void Update () {
		t += Time.deltaTime;
		if (t > 0.01f) {
			t=0;
			this.transform.Translate(0,0,1f);
				}

	}
}


















