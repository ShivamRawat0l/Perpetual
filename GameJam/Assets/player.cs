using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;
public class player : MonoBehaviour {
	public AudioClip[] clips;
	private AudioSource source;
	public TextMeshProUGUI score,des,high,highscore;
	private bool tele=false;
	public GameObject panel,pauseicon;
	private float teleasd;
	private int ground=1;
	private float speed=6,s=0f;
	private Rigidbody2D rig;
	private bool falling;
	public Button button;
	public Sprite mute,audio;
	public GameObject audioobj;
	public GameObject gameover;
	private bool increasescore,leftpressed,rightpressed;
	void Start () {
		leftpressed=false;
		rightpressed=false;
		
		rig= GetComponent<Rigidbody2D>();
		falling =false;
		Time.timeScale=0;
		high.text=">>> HighScore :- "+PlayerPrefs.GetInt("highscore");
		source= GetComponent<AudioSource>();
		increasescore=true;
		Debug.Log(PlayerPrefs.GetInt("audio"));
		if(PlayerPrefs.GetInt("audio")==0){
			button.image.sprite=audio;
			audioobj.GetComponent<audio>().play();
			source.mute=false;
		}
		else{
			button.image.sprite=mute;
			audioobj.GetComponent<audio>().pause();
			source.mute=true;
		}
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.Space)){
				jump();
		}
			if(Input.GetKey(KeyCode.A)||leftpressed){
				moveleft();
		}
			if(Input.GetKey(KeyCode.D)||rightpressed){
				moveright();
		}
			if(Input.GetKeyDown(KeyCode.Q)){
				teleport();

		}
		if(Input.GetKeyDown(KeyCode.E)){
			drop();
		}
		if(Input.GetKeyDown(KeyCode.Escape))
		{
			if(Time.timeScale==0)
			resume();
			else
			pause();
		}
		if(Time.timeScale==0){

		}
		else if(increasescore == true){
		s=s+0.1f+speed*Time.deltaTime;
		score.text="Score :- "+Mathf.Round(s);
		}
	}
	public void pause(){
		Time.timeScale=0;
		panel.SetActive(true);
		pauseicon.SetActive(false);
		audioobj.GetComponent<audio>().pause();
	}
	public void resume(){
		panel.SetActive(false);
		pauseicon.SetActive(true);
		Time.timeScale=1;
		if(PlayerPrefs.GetInt("audio")==0)
		audioobj.GetComponent<audio>().play();
	}
	public void moveleft(){
			transform.Translate(-speed/1.4f*Time.deltaTime,0,0);
			Time.timeScale=1;
			Destroy(des);
			leftpressed=true;
	}
	public void moveright(){
			transform.Translate(speed/1.4f*Time.deltaTime,0,0);
			Time.timeScale=1;
			Destroy(des);
			rightpressed=true;
	}
	public void releaseleft(){
		leftpressed=false;
	}
	public void releaseright(){
		rightpressed=false;
	}

	public void jump(){
		if(ground>0){
			
		
			ground--;
			source.clip=clips[2];
			source.Play();
			rig.velocity= new Vector2(rig.velocity.x,rig.velocity.y+8+speed/8);
			Time.timeScale=1;
			Destroy(des);
				}
	}
	public void drop(){
		
			rig.velocity= new Vector2(rig.velocity.x,rig.velocity.y-speed*2);
			Time.timeScale=1;
			Destroy(des);
			falling=true;
			s=s-5;
	}
	public void teleport(){
if(tele==true){
			transform.Translate(speed<18f?speed:18f,0,0);
			tele=false;
			source.clip=clips[1];
			source.Play();
			Time.timeScale=1;
			s=s-5;
			}
	}
	public void quit(){
		Application.Quit();
	}
	 void OnCollisionEnter2D(Collision2D other)
	{
		if(other.gameObject.tag=="ground"){
			ground=1;
			tele=true;
			speed=speed+0.5f;
			if(falling==true){
			source.clip=clips[0];
			source.Play();
			falling=false;
			}
		}
		if(other.gameObject.tag=="dead" ){
			StartCoroutine(endgame());
		
		}
	}
	void OnTriggerEnter2D(Collider2D other)
	{
		if(other.gameObject.tag=="destroyer")
			StartCoroutine(endgame());

	}
	public 	void Audio(){
		if(source.mute){
			button.image.sprite=audio;
			audioobj.GetComponent<audio>().play();
			PlayerPrefs.SetInt("audio",0);
			source.mute=false;
		}
		else{
			button.image.sprite=mute;
			Debug.Log("THIS IS CALLED");
			audioobj.GetComponent<audio>().pause();
			source.mute=true;
			PlayerPrefs.SetInt("audio",1);
		}
	}
	public IEnumerator endgame(){
		source.clip=clips[3];
			source.Play();
		gameover.SetActive(true);
		increasescore=false;
		audioobj.GetComponent<audio>().pause();
		if((int)Mathf.Round(s)>PlayerPrefs.GetInt("highscore"))
			highscore.text="NEW HIGHSCORE !";
		yield return new WaitForSeconds(1.5f);
			if((int)Mathf.Round(s)>PlayerPrefs.GetInt("highscore"))
			PlayerPrefs.SetInt("highscore",(int)Mathf.Round(s));
			gameover.SetActive(false);
			highscore.text="";
		SceneManager.LoadScene(SceneManager.GetActiveScene().name);
	}
	public void reset(){
		PlayerPrefs.SetInt("highscore",0);
		high.text=">>> HighScore :- "+PlayerPrefs.GetInt("highscore");
	}
}
