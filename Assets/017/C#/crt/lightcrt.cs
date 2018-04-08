using UnityEngine;
using System.Collections;

public class lightcrt : MonoBehaviour {
	public camcrt c;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (c.paus) {
						Light a = this.GetComponent<Light> ();
						a.color = Color.gray;
		} else {Light a = this.GetComponent<Light> ();
			a.color=Color.Lerp(Color.white,Color.gray,0.45f);
				}
	}
}
