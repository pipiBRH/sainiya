using UnityEngine;
using System.Collections;



public class ohregoal : MonoBehaviour
{
	public string b=null;
	public string c=null;
	public GameObject main=null;
	public ogrecrt og=null;
		// Use this for initialization
		void Start ()
		{
		}

	
		// Update is called once per frame
		void Update ()
		{

	}
	void OnTriggerEnter(Collider other) 
	{     
		if (other.gameObject.tag == "Player") 
		{	/*Main_GUI nu=other.GetComponent<Main_GUI>();
				nu.mlit++;
			if(nu.mlit<15)*/
			//x=this.GetComponent<monstercrt>();
			main = other.gameObject;
			og.elm=other.gameObject;
			og.goal=true;
			b=other.name;
				c = other.tag;
		}
	}
	void OnTriggerStay(Collider other) 
	{
				/*if (other.gameObject.tag == "Player") {
						Main_GUI nu = other.GetComponent<Main_GUI> ();
						if (og.hp == 0 && a == 0) {
								nu.mlit--;
								a++;
						}
				}*/
		}
	void OnTriggerExit(Collider a) 
	{     
		if (a.gameObject.tag == "Player") 
		{	Main_GUI nu=a.GetComponent<Main_GUI>();
			//nu.mlit--;
			//x=this.GetComponent<monstercrt>();
			og.goal=false;
			main = null;
			og.elm=null;
			b=null;
			c=null;		
		}
	}
}

