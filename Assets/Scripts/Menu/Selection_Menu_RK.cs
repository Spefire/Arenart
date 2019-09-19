using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Selection_Menu_RK : MonoBehaviour {

	public int select_menu;
	public AudioClip son_selection;
	private AudioSource aud;

	void OnEnable () {
		select_menu = 1;
		aud = GetComponent<AudioSource>();
	}

	void Update () {
		Debug.LogWarning(select_menu);
		if (Game_Inputs.J1_Bas && select_menu < 4) {
			select_menu++;
			Affichage ();
		}
		else if (Game_Inputs.J1_Haut && select_menu > 1) {
			select_menu--;
			Affichage ();
		}
	}

	void Affichage (){
		aud.PlayOneShot (son_selection);
	}
}

