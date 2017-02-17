using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Choice_Resume : MonoBehaviour {

	private int pos;
	public static int choice;
	public Texture texture_choice_1;
	public Texture texture_choice_2;
	public AudioClip son_selection;
	private Renderer render;
	private AudioSource aud;


	void Start () {
		pos = 4;
		choice = 2;
		render = GetComponent<Renderer>();
		aud = GetComponent<AudioSource>();
	}


	void Update () {
		if (Camera_Menu.pos == pos) {
			if (choice == 1 && Game_Inputs.J1_Droit) {
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
