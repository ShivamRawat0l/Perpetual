using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class loadnext : MonoBehaviour {

	// Use this for initialization
	void Start () {
		StartCoroutine(loadscene());
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	IEnumerator loadscene(){
		AsyncOperation operation = SceneManager.LoadSceneAsync("SampleScene");
		while(!operation.isDone){
			Debug.Log("Working");
			yield return null;
		}
	}
}
