using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Choice_Aides : MonoBehaviour {

	private int pos;
	public static int choice;
	public Texture texture_retour_gauche;
	public Texture texture_retour_droit;
	public Texture texture_persoprec;
	public Texture texture_persosuiv;
	public Texture texture_lespersos;
	public AudioClip son_selection;
	private Renderer render;
	private AudioSource aud;


	void Start () {
		pos = 5;
		choice = 2;
		render = GetComponent<Renderer>();
		aud = GetComponent<AudioSource>();
	}


	void Update () {
		if (Camera_Menu.pos == pos) {
			switch (Camera_Menu.help_pos) {
			case 0:
				if (choice == 1 && Input.GetKeyUp (KeyCode.D)) {
					choice = 2;
					aud.PlayOneShot (son_selection);
				} else if (choice == 2 && Input.GetKeyUp (KeyCode.Q)) {
					choice = 1;
					aud.PlayOneShot (son_selection);
				}
				if (choice == 1) {
					render.material.mainTexture = texture_retour_gauche;
				} else if (choice == 2) {
					render.material.mainTexture = texture_lespersos;
				}
				break;
			case 1:
				if (choice == 1 && Input.GetKeyUp (KeyCode.D)) {
					choice = 2;
					aud.PlayOneShot (son_selection);
				} else if (choice == 2 && Input.GetKeyUp (KeyCode.Q)) {
					choice = 1;
					aud.PlayOneShot (son_selection);
				}
				if (choice == 1) {
					render.material.mainTexture = texture_retour_gauche;
				} else if (choice == 2) {
					render.material.mainTexture = texture_persosuiv;
				}
				break;
			case 2:
				if (choice == 1 && Input.GetKeyUp (KeyCode.D)) {
					choice = 2;
					aud.PlayOneShot (son_selection);
				} else if (choice == 2 && Input.GetKeyUp (KeyCode.Q)) {
					choice = 1;
					aud.PlayOneShot (son_selection);
				}
				if (choice == 1) {
					render.material.mainTexture = texture_persoprec;
				} else if (choice == 2) {
					render.material.mainTexture = texture_persosuiv;
				}
				break;
			case 3:
				if (choice == 1 && Input.GetKeyUp (KeyCode.D)) {
					choice = 2;
					aud.PlayOneShot (son_selection);
				} else if (choice == 2 && Input.GetKeyUp (KeyCode.Q)) {
					choice = 1;
					aud.PlayOneShot (son_selection);
				}
				if (choice == 1) {
					render.material.mainTexture = texture_persoprec;
				} else if (choice == 2) {
					render.material.mainTexture = texture_persosuiv;
				}
				break;
			case 4:
				if (choice == 1 && Input.GetKeyUp (KeyCode.D)) {
					choice = 2;
					aud.PlayOneShot (son_selection);
				} else if (choice == 2 && Input.GetKeyUp (KeyCode.Q)) {
					choice = 1;
					aud.PlayOneShot (son_selection);
				}
				if (choice == 1) {
					render.material.mainTexture = texture_persoprec;
				} else if (choice == 2) {
					render.material.mainTexture = texture_persosuiv;
				}
				break;
			case 5:
				if (choice == 1 && Input.GetKeyUp (KeyCode.D)) {
					choice = 2;
					aud.PlayOneShot (son_selection);
				} else if (choice == 2 && Input.GetKeyUp (KeyCode.Q)) {
					choice = 1;
					aud.PlayOneShot (son_selection);
				}
				if (choice == 1) {
					render.material.mainTexture = texture_persoprec;
				} else if (choice == 2) {
					render.material.mainTexture = texture_retour_droit;
				}
				break;
			}
		}
	}
}
