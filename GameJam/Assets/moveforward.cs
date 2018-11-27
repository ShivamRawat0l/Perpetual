using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moveforward : MonoBehaviour {

	private float speed=1;
	public GameObject groundobj;
	private float localScale=0.94f;
	private float distance=20;
	// Use this for initialization
	void Start () {
		
	}

	// Update is called once per frame
	void Update () {
		transform.Translate(speed*Time.deltaTime,0,0);
		if(speed<12f)
		speed=speed+0.2f*Time.deltaTime;
	
		
	}
	 void OnTriggerExit2D(Collider2D other)
	{
		if(gameObject.tag=="destroyer"){
		if(other.gameObject.tag=="ground"){
			
			        Destroy(other.gameObject);
			}
		}
		else if(gameObject.tag=="spawner"){
			if(other.gameObject.tag=="ground"){
				GameObject obj=(GameObject)Instantiate(groundobj,new Vector3(transform.position.x+distance,Random.Range(-4f,3f),0),Quaternion.identity);
				obj.transform.localScale = new Vector3(localScale, 0.34f, 0);
				if(localScale>0.2){
					localScale=localScale-0.03f;
				}
				if(distance<40){
					distance+=0.2f;
				}
			}
		}
	}
	
}
