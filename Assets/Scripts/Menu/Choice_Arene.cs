using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Choice_Arene : MonoBehaviour {

	private int pos;
	public static int choice;
	public Texture texture_choice_0;
	public Texture texture_choice_1;
	public Texture texture_choice_2;
	public AudioClip son_selection;
	private Renderer render;
	private AudioSource aud;


	void Start () {
		pos = 3;
		choice = 0;
		render = GetComponent<Renderer>();
		aud = GetComponent<AudioSource>();
	}


	void Update () {
		if (Camera_Menu.pos == pos) {
			if (choice == 0 && (Game_Inputs.J1_Bas || Game_Inputs.J1_Attaque)) {
				choice = 2;
				render.material.mainTexture = texture_choice_2;
			}
			else if ((choice == 1 || choice == 2) && Game_Inputs.J1_Haut) {
				choice = 0;
				render.material.mainTexture = texture_choice_0;
				aud.PlayOneShot (son_selection);
			}
			else if (choice == 1 && Game_Inputs.J1_Droit) {
				choice = 2;
				render.material.mainTexture = texture_choice_2;
				aud.PlayOneShot (son_selection);
			}
			else if (choice == 2 && Game_Inputs.J1_Gauche) {
				choice = 1;
				render.material.mainTexture = texture_choice_1;
				aud.PlayOneShot (son_selection);
			}
		}
	}
}
