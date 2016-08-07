using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class UIManager : MonoBehaviour {

	GameObject[] finishObjects;
	public GameObject player;
    public GameObject MusicPlayer;
    public AudioSource audio;

	// Use this for initialization
	void Start () {
		Time.timeScale = 1;
		finishObjects = GameObject.FindGameObjectsWithTag("ShowOnFinish");
		hideFinished ();
	}

	void Update(){
		//shows finish gameobjects if player is dead and timescale = 0
		if (Time.timeScale == 0 && player.GetComponent<PlayerManager>().alive == false){
			showFinished();
		}
	}

	//Reloads the Level
	public void Reload(){
		//Application.LoadLevel(Application.loadedLevel);
		SceneManager.LoadScene(SceneManager.GetActiveScene().name);
	}

	//loads inputted level
	public void LoadLevel(string level){
		//Application.LoadLevel(level);
		SceneManager.LoadScene (level);
	}

	//shows objects with ShowOnFinish tag
	public void showFinished(){
		foreach(GameObject g in finishObjects){
			g.SetActive(true);
            MusicPlayer.SetActive(false);


            
		}
	}

	//hides objects with ShowOnFinish tag
	public void hideFinished(){
		foreach(GameObject g in finishObjects){
			g.SetActive(false);
		}
	}
}
