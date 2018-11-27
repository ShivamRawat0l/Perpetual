using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class audio : MonoBehaviour {
	public AudioClip[] clips;
	private AudioSource source;
	// Use this for initialization
	void Start () {
		source=GetComponent<AudioSource>();
		source.clip=clips[(int)Mathf.Round(Random.Range(1,16))];
		source.Play();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	public void pause(){
		source.Pause();
	}
	public void play(){
		source.Play();
	}
}
