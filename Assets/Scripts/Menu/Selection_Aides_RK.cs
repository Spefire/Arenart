using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Selection_Aides_RK : MonoBehaviour {

	public int select_aide;
	public AudioClip son_selection;
	private AudioSource aud;

	void OnEnable () {
		select_aide = 2;
		aud = GetComponent<AudioSource>();
	}

	void Update () {
		if (Game_Inputs.J1_Droit && select_aide < 2) {
			select_aide++;
			Affichage ();
		}
		else if (Game_Inputs.J1_Gauche && select_aide > 1) {
			select_aide--;
			Affichage ();
		}
	}

	void Affichage (){
		aud.PlayOneShot (son_selection);
	}
}

