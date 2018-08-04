﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityStandardAssets.Characters.FirstPerson;

public class Pause : MonoBehaviour {

	//public GameObject gun;
	public static bool isPaused;
	public GameObject pausemenu;
	public FirstPersonController mouseLook;
	public Gun gun;
	public GameObject htp;


    void Start () {
		isPaused = false;
        //exit = false;
        pausemenu.SetActive(false);
    }

	// Update is called once per frame
	void Update () {
		if (isPaused) {
			pauseGame (true);
			mouseLook.enabled = false;
			gun.enabled = false;
			Cursor.visible = true;
			Cursor.lockState = CursorLockMode.Confined;
		} else {
			pauseGame (false);
			mouseLook.enabled = true;
			gun.enabled = true;
			Cursor.visible = false;
			Cursor.lockState = CursorLockMode.Locked;
		}

		if (Input.GetButtonDown ("Cancel")) {
			switchPause ();

		}
	}
	void pauseGame(bool state){
		if (state) {
			Time.timeScale = 0.0f;

			//gun.GetComponent<AudioSource> ().enabled = false;
		} else {
			Time.timeScale = 1.0f;
			pausemenu.SetActive (false);
			//gun.GetComponent<AudioSource> ().enabled = true;
		}
		pausemenu.SetActive (state);
	}
	public void switchPause(){
		if (isPaused) {
			isPaused = false;
		} else {
			isPaused = true;
		}
	}
	public void mainmenu(){
		SceneManager.LoadScene(0);
	}
	public void resume(){
		pausemenu.SetActive(false);
		isPaused = false;
		pauseGame (false);
		mouseLook.enabled = true;
		gun.enabled = true;
		Cursor.visible = false;
	}
	public void htpON(){
		pausemenu.SetActive(false);
		htp.SetActive(true);
	}
	public void htpOFF(){
		pausemenu.SetActive(true);
		htp.SetActive(false);
	}

}