using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Choice_Menu : MonoBehaviour {

	public static int choice;
	public Texture texture_choice_0;
	public Texture texture_choice_1;
	public Texture texture_choice_2;
	public Texture texture_choice_3;
	public Texture texture_choice_4;
	public Texture texture_choice_5;
	private Renderer render;


	void Start () {
		choice = 0;
		render = GetComponent<Renderer>();
	}


	void Update () {
		if (choice < 5 && Input.GetKeyUp (KeyCode.S)) {
			choice++;
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
