using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Camera_Menu: MonoBehaviour {

	private float posx;
	private float posy;
	private float Sposx;
	private float Sposy;
	private float tex;
	private float tey;
	private float coef;
	public static int pos;
	public AudioClip son_validation;
	private AudioSource aud;

	// Use this for initialization
	void Start () {
		aud = GetComponent<AudioSource>();
		posx = transform.position.x;
		posy = transform.position.y;
		Sposx = transform.position.x;
		Sposy = transform.position.y;
		pos = 0;
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
	}

	// Update is called once per frame
	void Update () {
		tex = (float)Screen.width/1366;
		tey = (float)Screen.height/768;
		//Aides
		if (pos == 10) {
			if (Input.GetKeyUp (KeyCode.Escape)) {
				print ("Retour_Menu");
				transform.position = new Vector3 (posx, posy + 15, 0);
				posx = transform.position.x;
				posy = transform.position.y;
				pos = 0;
				aud.PlayOneShot (son_validation);
			}
		}
		//Aides
		if (pos == 11 || pos == 12 || pos == 13 || pos == 14 || pos == 15 || pos == 16 || pos == 17 || pos == 18 || pos == 19 || pos == 20) {
			if (Input.GetKeyUp (KeyCode.Escape)) {
				print ("Retour_Aides");
				transform.position = new Vector3 (Sposx, Sposy - 15, 0);
				posx = transform.position.x;
				posy = transform.position.y;
				pos = 10;
				aud.PlayOneShot (son_validation);
			}
		}
		//Crédits
		if (pos == 30) {
			if (Input.GetKeyUp (KeyCode.Escape)) {
				print ("Retour_Menu");
				transform.position = new Vector3 (posx, posy + 30, 0);
				posx = transform.position.x;
				posy = transform.position.y;
				pos = 0;
				aud.PlayOneShot (son_validation);
			}
		}
		//Selection_P1
		else if (pos == 1) {
			if (Input.GetKeyUp (KeyCode.Escape)) {
				print ("Retour_Menu");
				transform.position = new Vector3 (Sposx, Sposy, 0);
				posx = transform.position.x;
				posy = transform.position.y;
				pos = 0;
				aud.PlayOneShot (son_validation);
			}
			if(Input.GetKeyUp (KeyCode.Return) || Input.GetButtonDown ("J1_Saut")){
				print ("Selection_P2");
				transform.position = new Vector3 (posx + 35, posy, 0);
				posx = transform.position.x;
				posy = transform.position.y;
				pos = 2;
				aud.PlayOneShot (son_validation);
			}
		}
		//Selection_P2
		else if (pos == 2) {
			if (Input.GetKeyUp (KeyCode.Escape)) {
				print ("Retour_Selection_P1");
				transform.position = new Vector3 (posx - 35, posy, 0);
				posx = transform.position.x;
				posy = transform.position.y;
				pos = 1;
				aud.PlayOneShot (son_validation);
			}
			if (Input.GetKeyUp (KeyCode.Return) || Input.GetButtonDown ("J2_Saut")) {
				print ("Selection_Arene");
				transform.position = new Vector3 (posx + 35, posy, 0);
				posx = transform.position.x;
				posy = transform.position.y;
				pos = 3;
				aud.PlayOneShot (son_validation);
			}
		}
		//Selection_Arene
		else if (pos == 3) {
			if (Input.GetKeyUp (KeyCode.Escape)) {
				print ("Retour_Selection_P2");
				transform.position = new Vector3 (posx - 35, posy, 0);
				posx = transform.position.x;
				posy = transform.position.y;
				pos = 2;
				aud.PlayOneShot (son_validation);
			}
			if (Input.GetKeyUp (KeyCode.Return) || Input.GetButtonDown ("J1_Saut")) {
				print ("Select_Resume");
				transform.position = new Vector3 (posx + 35, posy, 0);
				posx = transform.position.x;
				posy = transform.position.y;
				pos = 4;
				aud.PlayOneShot (son_validation);
			}
		}
		//Select_Resume
		else if (pos == 4) {
			if (Input.GetKeyUp (KeyCode.Escape)) {
				print ("Retour_Select_Arene");
				transform.position = new Vector3 (posx - 35, posy, 0);
				posx = transform.position.x;
				posy = transform.position.y;
				pos = 3;
				aud.PlayOneShot (son_validation);
			}
			if (Input.GetKeyUp (KeyCode.Return) || Input.GetButtonDown ("J1_Saut")) {
				print ("Lancement_Partie");
				aud.PlayOneShot (son_validation);
				//Lancer la Partie
				var gameObjects = GameObject.FindGameObjectsWithTag("LevelManager");
				foreach (var gameObj in gameObjects) {
					DontDestroyOnLoad(gameObj);
				}
				if (Selection_Arene.select_arene == 1) {
					SceneManager.LoadScene ("Scene_Jeu_01");
				}else if (Selection_Arene.select_arene == 2) {
					SceneManager.LoadScene ("Scene_Jeu_02");
				}else if (Selection_Arene.select_arene == 3) {
					SceneManager.LoadScene ("Scene_Jeu_03");
				}
			}
		}
	}

	//-------------------------------------------------------------------------------------------------------------------
	//-------------------------------------------------------------------------------------------------------------------
	//-------------------------------------------------------------------------------------------------------------------

	void OnGUI() {
		
		//Aides
		if (pos == 10) {
			if (GUI.Button (new Rect (tex * 1140, tey * 25, tex * 150, tey * 75), "Retour")) {
				print ("Retour_Menu");
				transform.position = new Vector3 (Sposx, posy + 15, 0);
				posx = transform.position.x;
				posy = transform.position.y;
				pos = 0;
				aud.PlayOneShot (son_validation);
			}
			if(GUI.Button (new Rect (tex * 600, tey * 650, tex * 150, tey * 75), "Personnages")){
				print ("Personnage 1");
				transform.position = new Vector3 (posx + 35, posy, 0);
				posx = transform.position.x;
				posy = transform.position.y;
				pos = 11;
				aud.PlayOneShot (son_validation);
			}
		}
		//Aides 1
		if (pos == 11) {
			if (GUI.Button (new Rect (tex * 1140, tey * 25, tex * 150, tey * 75), "Retour")) {
				print ("Retour_Aides");
				transform.position = new Vector3 (Sposx, posy, 0);
				posx = transform.position.x;
				posy = transform.position.y;
				pos = 10;
				aud.PlayOneShot (son_validation);
			}
			if(GUI.Button (new Rect (tex * 700, tey * 650, tex * 150, tey * 75), "Personnage suivant")){
				print ("Personnage 2");
				transform.position = new Vector3 (posx + 35, posy, 0);
				posx = transform.position.x;
				posy = transform.position.y;
				pos = 12;
				aud.PlayOneShot (son_validation);
			}
		}
		//Aides 2, 3, 4, 5, 6, 7, 8, 9
		if (pos == 12 || pos == 13 || pos == 14 || pos == 15 || pos == 16 || pos == 17 || pos == 18 || pos == 19) {
			if (GUI.Button (new Rect (tex * 1140, tey * 25, tex * 150, tey * 75), "Retour")) {
				print ("Retour_Aides");
				transform.position = new Vector3 (Sposx, posy, 0);
				posx = transform.position.x;
				posy = transform.position.y;
				pos = 10;
				aud.PlayOneShot (son_validation);
			}
			if(GUI.Button (new Rect (tex * 500, tey * 650, tex * 150, tey * 75), "Personnage précédent")){
				print ("Personnage "+(pos-11));
				transform.position = new Vector3 (posx - 35, posy, 0);
				posx = transform.position.x;
				posy = transform.position.y;
				pos--;
				aud.PlayOneShot (son_validation);
			}
			if(GUI.Button (new Rect (tex * 700, tey * 650, tex * 150, tey * 75), "Personnage suivant")){
				print ("Personnage "+(pos-9));
				transform.position = new Vector3 (posx + 35, posy, 0);
				posx = transform.position.x;
				posy = transform.position.y;
				pos++;
				aud.PlayOneShot (son_validation);
			}
		}
		//Aides 10
		if (pos == 20) {
			if (GUI.Button (new Rect (tex * 1140, tey * 25, tex * 150, tey * 75), "Retour")) {
				print ("Retour_Aides");
				transform.position = new Vector3 (Sposx, posy, 0);
				posx = transform.position.x;
				posy = transform.position.y;
				pos = 10;
				aud.PlayOneShot (son_validation);
			}
			if(GUI.Button (new Rect (tex * 500, tey * 650, tex * 150, tey * 75), "Personnage précédent")){
				print ("Personnage 9");
				transform.position = new Vector3 (posx - 35, posy, 0);
				posx = transform.position.x;
				posy = transform.position.y;
				pos = 19;
				aud.PlayOneShot (son_validation);
			}
		}

		//-------------------------------------------------------------------------------------------------------------------
		//-------------------------------------------------------------------------------------------------------------------

		//Crédits
		if (pos == 30) {
			if (GUI.Button (new Rect (tex * 1140, tey * 25, tex * 150, tey * 75), "Retour")) {
				print ("Retour_Menu");
				transform.position = new Vector3 (posx, posy + 30, 0);
				posx = transform.position.x;
				posy = transform.position.y;
				pos = 0;
				aud.PlayOneShot (son_validation);
			}
		}

		//-------------------------------------------------------------------------------------------------------------------
		//-------------------------------------------------------------------------------------------------------------------

		//Menu_principal
		if (pos == 0) {
			if (GUI.Button (new Rect (tex * 620, tey * 300, tex * 190, tey * 75), "Player 1 Vs Player 2")) {
				print ("Selection_P1");
				transform.position = new Vector3 (posx + 35, posy, 0);
				posx = transform.position.x;
				posy = transform.position.y;
				pos = 1;
				aud.PlayOneShot (son_validation);
			}
			if (GUI.Button (new Rect (tex * 620, tey * 400, tex * 190, tey * 75), "Aides du jeu")) {
				print ("Aides");
				transform.position = new Vector3 (posx, posy - 15, 0);
				posx = transform.position.x;
				posy = transform.position.y;
				pos = 10;
				aud.PlayOneShot (son_validation);
			}
			if (GUI.Button (new Rect (tex * 620, tey * 500, tex * 190, tey * 75), "Crédits")) {
				transform.position = new Vector3 (posx, posy - 30, 0);
				posx = transform.position.x;
				posy = transform.position.y;
				pos = 30;
				aud.PlayOneShot (son_validation);
			}
			if(GUI.Button (new Rect (tex * 620, tey * 600, tex * 190, tey * 75), "Quitter")){
				print ("Quitter");
				aud.PlayOneShot (son_validation);
				Application.Quit();
			}
		}

		//-------------------------------------------------------------------------------------------------------------------
		//-------------------------------------------------------------------------------------------------------------------

		//Selection_P1
		else if (pos == 1) {
			if (GUI.Button (new Rect (tex * 1140, tey * 25, tex * 150, tey * 75), "Retour")) {
				print ("Retour_Menu");
				transform.position = new Vector3 (Sposx, Sposy, 0);
				posx = transform.position.x;
				posy = transform.position.y;
				pos = 0;
				aud.PlayOneShot (son_validation);
			}
			if(GUI.Button (new Rect (tex * 600, tey * 650, tex * 150, tey * 75), "Valider")){
				print ("Selection_P2");
				transform.position = new Vector3 (posx + 35, posy, 0);
				posx = transform.position.x;
				posy = transform.position.y;
				pos = 2;
				aud.PlayOneShot (son_validation);
			}
		}
		//Selection_P2
		else if (pos == 2) {
			if (GUI.Button (new Rect (tex * 1140, tey * 25, tex * 150, tey * 75), "Retour")) {
				print ("Retour_Selection_P1");
				transform.position = new Vector3 (posx - 35, posy, 0);
				posx = transform.position.x;
				posy = transform.position.y;
				pos = 1;
				aud.PlayOneShot (son_validation);
			}
			if (GUI.Button (new Rect (tex * 600, tey * 650, tex * 150, tey * 75), "Valider")) {
				print ("Selection_Arene");
				transform.position = new Vector3 (posx + 35, posy, 0);
				posx = transform.position.x;
				posy = transform.position.y;
				pos = 3;
				aud.PlayOneShot (son_validation);
			}
		}
		//Selection_Arene
		else if (pos == 3) {
			if (GUI.Button (new Rect (tex * 1140, tey * 25, tex * 150, tey * 75), "Retour")) {
				print ("Retour_Selection_P2");
				transform.position = new Vector3 (posx - 35, posy, 0);
				posx = transform.position.x;
				posy = transform.position.y;
				pos = 2;
				aud.PlayOneShot (son_validation);
			}
			if (GUI.Button (new Rect (tex * 600, tey * 650, tex * 150, tey * 75), "Valider")) {
				print ("Select_Resume");
				transform.position = new Vector3 (posx + 35, posy, 0);
				posx = transform.position.x;
				posy = transform.position.y;
				pos = 4;
				aud.PlayOneShot (son_validation);
			}
		}
		//Select_Resume
		else if (pos == 4) {
			if (GUI.Button (new Rect (tex * 1140, tey * 25, tex * 150, tey * 75), "Retour")) {
				print ("Retour_Select_Arene");
				transform.position = new Vector3 (posx - 35, posy, 0);
				posx = transform.position.x;
				posy = transform.position.y;
				pos = 3;
				aud.PlayOneShot (son_validation);
			}
			if (GUI.Button (new Rect (tex * 600, tey * 650, tex * 150, tey * 75), "Valider")) {
				print ("Lancement_Partie");
				aud.PlayOneShot (son_validation);
                //Lancer la Partie
                var gameObjects = GameObject.FindGameObjectsWithTag("LevelManager");
                foreach (var gameObj in gameObjects) {
                    DontDestroyOnLoad(gameObj);
                }
				if (Selection_Arene.select_arene == 1) {
					SceneManager.LoadScene ("Scene_Jeu_01");
				}else if (Selection_Arene.select_arene == 2) {
					SceneManager.LoadScene ("Scene_Jeu_02");
				}else if (Selection_Arene.select_arene == 3) {
					SceneManager.LoadScene ("Scene_Jeu_03");
				}
            }
        }
	}
}
