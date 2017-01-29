using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Camera_Jeu : MonoBehaviour {

	private float tex;
	private float tey;
	private float coef;
	public static bool paused;
	public static bool J1_Gagne;
	public static bool J2_Gagne;
	public Texture text_nbvie;
	public Texture text_vie;
	public Texture text_resist;
	public AudioClip son_terrain;
	private AudioSource aud;
	private bool sound;
	public Texture text_sound_on;
	public Texture text_sound_off;

	// Use this for initialization
	void Start () {
		sound = true;
		paused = false;
		J1_Gagne = false;
		J2_Gagne = false;
		Time.timeScale = 1.0f;
		float TARGET_WIDTH = 1366.0f;
		float TARGET_HEIGHT = 768.0f;
		int PIXELS_TO_UNITS = 30; // 1:1 ratio of pixels to units
		float desiredRatio = TARGET_WIDTH / TARGET_HEIGHT;
		float currentRatio = (float)Screen.width/(float)Screen.height;
		if(currentRatio >= desiredRatio){
			GetComponent<Camera>().orthographicSize = TARGET_HEIGHT / 4 / PIXELS_TO_UNITS;
		}else{
			float differenceInSize = desiredRatio / currentRatio;
			GetComponent<Camera>().orthographicSize = TARGET_HEIGHT / 4 / PIXELS_TO_UNITS * differenceInSize;
		}
		Cursor.visible = false;
		aud = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
		tex = (float)Screen.width/1366;
		tey = (float)Screen.height/768;
		if (Input.GetKeyUp (KeyCode.Escape) && !paused) {
			print ("Pause");
			paused = true;
			Time.timeScale = 0f;
			Cursor.visible = true;
			if (sound) {
				aud.Pause ();
			}
		}else if (Input.GetKeyUp (KeyCode.Escape) && paused) {
			print ("Retour à la partie");
			paused = false;
			Time.timeScale = 1.0f;
			Cursor.visible = false;
			if (sound) {
				aud.UnPause ();
			}
		}
		if (!aud.isPlaying && aud.loop && !paused && sound) {
			aud.PlayOneShot (son_terrain);
		}
	}

	void OnGUI() {
		GUI.Box (new Rect (tex * 30, tey * 20, tex * 30, tey * 30), text_nbvie);
		GUI.Box (new Rect (tex * 60, tey * 20, tex * 30, tey * 30), ""+Joueur_Stats_J1.nbVie);
		GUI.Box (new Rect (tex * 130, tey * 20, tex * 30, tey * 30), text_vie);
		GUI.Box (new Rect (tex * 160, tey * 20, tex * 50, tey * 30), ""+(int)Joueur_Stats_J1.Vie);
		GUI.Box (new Rect (tex * 230, tey * 20, tex * 30, tey * 30), text_resist);
		GUI.Box (new Rect (tex * 260, tey * 20, tex * 50, tey * 30), ""+(int)Joueur_Stats_J1.Resistance+"%");

		GUI.Box (new Rect (tex * 1020, tey * 20, tex * 30, tey * 30), text_nbvie);
		GUI.Box (new Rect (tex * 1050, tey * 20, tex * 30, tey * 30), ""+Joueur_Stats_J2.nbVie);
		GUI.Box (new Rect (tex * 1120, tey * 20, tex * 30, tey * 30), text_vie);
		GUI.Box (new Rect (tex * 1150, tey * 20, tex * 50, tey * 30), ""+(int)Joueur_Stats_J2.Vie);
		GUI.Box (new Rect (tex * 1220, tey * 20, tex * 30, tey * 30), text_resist);
		GUI.Box (new Rect (tex * 1250, tey * 20, tex * 50, tey * 30), ""+(int)Joueur_Stats_J2.Resistance+"%");

		////////////////////////////////////////////////////////////////////////////////////////////////////////

		if (paused && !J1_Gagne && !J2_Gagne) {
			if (sound) {
				if (GUI.Button (new Rect (50, 50, 50, 50), text_sound_on)) { 
					print ("Musique : OFF");
					sound = false;
				}
			} else {
				if (GUI.Button (new Rect (50, 50, 50, 50), text_sound_off)) { 
					print ("Musique : ON");
					sound = true;
				}
			}
			if(GUI.Button(new Rect(Screen.width / 2 - (tex * 130), Screen.height / 2 - (tey * 100), Screen.width/ 6, Screen.height/ 12), "Reprendre le jeu")){
				print ("Reprendre le jeu");
				paused = false;
				Time.timeScale = 1.0f;
				Cursor.visible = false;
				if (sound) {
					aud.UnPause ();
				}
			}
			if(GUI.Button(new Rect(Screen.width / 2 - (tex * 130), Screen.height / 2, Screen.width/ 6, Screen.height/ 12), "Revenir au menu")){ 
				print ("Retour au menu");
				var gameObjects = GameObject.FindGameObjectsWithTag("LevelManager");
				foreach (var gameObj in gameObjects) {
					Destroy(gameObj);
				}
				SceneManager.LoadScene("Scene_Menu");
			}
			if(GUI.Button(new Rect(Screen.width / 2 - (tex * 130), Screen.height / 2 + (tey * 100), Screen.width/ 6, Screen.height/ 12), "Quitter le jeu")){
				print ("Quitter le jeu");
				Application.Quit();
			}
		}
		///////////////////////////////////////////////////////////////////////////////////////////////////////
		else if (J1_Gagne) {
			Time.timeScale = 0f;
			Cursor.visible = true;
			GUI.Box (new Rect (Screen.width / 2 - (tex * 200), Screen.height / 2 - (tey * 200), Screen.width/ 4, Screen.height/ 20), "\nLe Joueur 1 a gagné la partie !");
			if(GUI.Button(new Rect(Screen.width / 2 - (tex * 130), Screen.height / 2, Screen.width/ 6, Screen.height/ 12), "Revenir au menu")){ 
				print ("Retour au menu");
				var gameObjects = GameObject.FindGameObjectsWithTag("LevelManager");
				foreach (var gameObj in gameObjects) {
					Destroy(gameObj);
				}
				SceneManager.LoadScene("Scene_Menu");
			}
			if(GUI.Button(new Rect(Screen.width / 2 - (tex * 130), Screen.height / 2 + (tey * 150), Screen.width/ 6, Screen.height/ 12), "Quitter le jeu")){
				print ("Quitter le jeu");
				Application.Quit();
			}
		}
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		else if (J2_Gagne) {
			Time.timeScale = 0f;
			Cursor.visible = true;
			GUI.Box(new Rect(Screen.width / 2 - (tex * 200), Screen.height / 2 - (tey * 200), Screen.width/ 4, Screen.height/ 20), "Le Joueur 2 a gagné la partie !");
			if(GUI.Button(new Rect(Screen.width / 2 - (tex * 130), Screen.height / 2, Screen.width/ 6, Screen.height/ 12), "Revenir au menu")){ 
				print ("Retour au menu");
				var gameObjects = GameObject.FindGameObjectsWithTag("LevelManager");
				foreach (var gameObj in gameObjects) {
					Destroy(gameObj);
				}
				SceneManager.LoadScene("Scene_Menu");
			}
			if(GUI.Button(new Rect(Screen.width / 2 - (tex * 130), Screen.height / 2 + (tey * 150), Screen.width/ 6, Screen.height/ 12), "Quitter le jeu")){
				print ("Quitter le jeu");
				Application.Quit();
			}
		}
	}
}
