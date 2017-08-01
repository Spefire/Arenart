using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class Canvas_Jeu_RK : MonoBehaviour {

	public static bool isPaused;
	public static bool isFinished;
	public bool hasMusic;
	public int maxScore;
	public Sprite son_off;
	public Sprite son_on;
	public Image son_music;
	public AudioClip son_jeu;
	public GameObject panelPause;
	public GameObject panelFin;

	public Text textVieJ1;
	public Text textVieJ2;
	public Text textResistJ1;
	public Text textResistJ2;
	public Text textScoreJ1;
	public Text textScoreJ2;
	public Text textWin;

	private Perso_Stats_RK J1;
	private Perso_Stats_RK J2;
	private AudioSource aud;

	void OnEnable () {
		hasMusic = true;
		isPaused = false;
		Time.timeScale = 1.0f;
		Cursor.visible = false;
		aud = GetComponent<AudioSource>();
		panelPause.SetActive (false);
		panelFin.SetActive (false);
	}

	void Start() {
		J1 = GameObject.FindGameObjectWithTag ("J1").GetComponent<Perso_Stats_RK>();
		J2 = GameObject.FindGameObjectWithTag ("J2").GetComponent<Perso_Stats_RK>();
	}

	//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

	void Update () {
		UpdateStatsJ1 (J1.vie, J1.resistance);
		UpdateStatsJ2 (J2.vie, J2.resistance);
		UpdateScore (J1.points, J2.points);
		if (Input.GetKeyUp (KeyCode.Escape) && !isPaused) {
			Set_Pause ();
		} else if (Input.GetKeyUp (KeyCode.Escape) && isPaused) {
			Click_Button_Reprendre ();
		}
		if (J1.points >= maxScore) {
			Set_Win ();
			Set_J1_Win ();
		}
		if (J2.points >= maxScore) {
			Set_Win ();
			Set_J2_Win ();
		}
		if (!aud.isPlaying && aud.loop && !isPaused && hasMusic) {
			aud.PlayOneShot (son_jeu);
		}
		if (EventSystem.current != null) {
			if (EventSystem.current.currentSelectedGameObject == null) {
				EventSystem.current.SetSelectedGameObject (EventSystem.current.firstSelectedGameObject);
			}
		}
	}

	public void UpdateStatsJ1(int vie, int resist) {
		textVieJ1.text = vie.ToString ();
		textResistJ1.text = resist.ToString ();
	}

	public void UpdateStatsJ2(int vie, int resist) {
		textVieJ2.text = vie.ToString ();
		textResistJ2.text = resist.ToString ();
	}

	public void UpdateScore(int p1, int p2) {
		textScoreJ1.text = p1.ToString ();
		textScoreJ2.text = p2.ToString ();
	}

	//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

	public void Set_Pause() {
		print ("Pause");
		isPaused = true;
		panelPause.SetActive (true);
		Time.timeScale = 0f;
		if (hasMusic) {
			aud.Pause ();
		}
	}

	public void Set_Win() {
		print ("Win");
		isFinished = true;
		panelFin.SetActive (true);
		Time.timeScale = 0f;
		if (hasMusic) {
			aud.Pause ();
		}
	}

	public void Click_Button_Reprendre() {
		print ("Retour à la partie");
		isPaused = false;
		panelPause.SetActive (false);
		panelFin.SetActive (false);
		Time.timeScale = 1.0f;
		if (hasMusic) {
			aud.UnPause ();
		}
	}

	public void Click_Button_Musique() {
		print ("Musique");
		hasMusic = !hasMusic;
		if (hasMusic) {
			son_music.sprite = son_on;
		} else {
			son_music.sprite = son_off;
		}
	}

	public void Click_Button_Retour() {
		print ("Retour au menu");
		isPaused = false;
		panelPause.SetActive (false);
		Time.timeScale = 1.0f;
		if (hasMusic) {
			aud.UnPause ();
		}
		var gameObjects = GameObject.FindGameObjectsWithTag("LevelManager");
		foreach (var gameObj in gameObjects) {
			Destroy(gameObj);
		}
		SceneManager.LoadScene("Scene_Principale");
	}

	public void Click_Button_Retour_Fin() {
		print ("Retour au menu");
		isPaused = false;
		panelFin.SetActive (false);
		Time.timeScale = 1.0f;
		if (hasMusic) {
			aud.UnPause ();
		}
		var gameObjects = GameObject.FindGameObjectsWithTag("LevelManager");
		foreach (var gameObj in gameObjects) {
			Destroy(gameObj);
		}
		SceneManager.LoadScene("Scene_Principale");
	}
		
	public void Click_Button_Quitter() {
		Application.Quit();
	}

	public void Set_J1_Win() {
		textWin.color = new Color (0.9f, 0f, 0f);
		textWin.text = "Le Joueur 1 a gagné la partie !!!";
	}

	public void Set_J2_Win() {
		textWin.color = new Color (0f, 0f, 0.9f);
		textWin.text = "Le Joueur 2 a gagné la partie !!!";
	}

}
