using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Canvas_Selection_RK : MonoBehaviour {

	public int select_menu;
	public AudioClip son_selection;
	private AudioSource aud;

	void OnEnable () {
		select_menu = 1;
		aud = GetComponent<AudioSource>();
	}

	void Update () {
		if (Canvas_Jeu_RK.isFinished) {
			if ((Game_Inputs.J1_Bas || Game_Inputs.J2_Bas) && select_menu < 2) {
				select_menu++;
				Affichage ();
			} else if ((Game_Inputs.J1_Haut || Game_Inputs.J2_Haut) && select_menu > 1) {
				select_menu--;
				Affichage ();
			}
		}
		if (Canvas_Jeu_RK.isPaused) {
			if ((Game_Inputs.J1_Bas || Game_Inputs.J2_Bas) && select_menu < 4) {
				select_menu++;
				Affichage ();
			} else if ((Game_Inputs.J1_Haut || Game_Inputs.J2_Haut) && select_menu > 1) {
				select_menu--;
				Affichage ();
			}
		}
	}

	void Affichage (){
		aud.PlayOneShot (son_selection);
	}
}

