using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Choice_Menu : MonoBehaviour {

	private int pos;
	public static int choice;
	public Texture texture_choice_0;
	public Texture texture_choice_1;
	public Texture texture_choice_2;
	public Texture texture_choice_3;
	public Texture texture_choice_4;
	public Texture texture_choice_5;
	public AudioClip son_selection;
	private Renderer render;
	private AudioSource aud;


	void Start () {
		pos = 0;
		choice = 0;
		render = GetComponent<Renderer>();
		aud = GetComponent<AudioSource>();
	}


	void Update () {
		if (Camera_Menu.pos == pos) {
			if (choice < 5 && Input.GetKeyUp (KeyCode.S)) {
				choice++;
				aud.PlayOneShot (son_selection);
				switch (choice) {
				case 1: //Aides
					render.material.mainTexture = texture_choice_1;
					break;
				case 2: //Credits
					render.material.mainTexture = texture_choice_2;
					break;
				case 3: //Musique
					render.material.mainTexture = texture_choice_3;
					break;
				case 4: //Langage
					render.material.mainTexture = texture_choice_4;
					break;
				case 5: //Quitter
					render.material.mainTexture = texture_choice_5;
					break;
				}
			}
			if (choice > 0 && Input.GetKeyUp (KeyCode.Z)) {
				choice--;
				aud.PlayOneShot (son_selection);
				switch (choice) {
				case 0: //Jouer
					render.material.mainTexture = texture_choice_0;
					break;
				case 1: //Aides
					render.material.mainTexture = texture_choice_1;
					break;
				case 2: //Credits
					render.material.mainTexture = texture_choice_2;
					break;
				case 3: //Musique
					render.material.mainTexture = texture_choice_3;
					break;
				case 4: //Langage
					render.material.mainTexture = texture_choice_4;
					break;
				}
			}
		}
	}
}
