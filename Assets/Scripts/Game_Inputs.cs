using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game_Inputs : MonoBehaviour {

	public static bool Valider;
	public static bool Retour;

	public static bool J1_Gauche;
	public static bool J1_Droit;
	public static bool J1_Gauche_Pressed;
	public static bool J1_Droit_Pressed;
	public static bool J1_Haut;
	public static bool J1_Bas;
	public static bool J1_Attaque;
	public static bool J1_Pouvoir;
	public static bool J1_PouvoirSpe;

	public static bool J2_Gauche;
	public static bool J2_Droit;
	public static bool J2_Gauche_Pressed;
	public static bool J2_Droit_Pressed;
	public static bool J2_Haut;
	public static bool J2_Bas;
	public static bool J2_Attaque;
	public static bool J2_Pouvoir;
	public static bool J2_PouvoirSpe;

	private int J1_Refresh_Horizontal;
	private int J1_Refresh_Vertical;
	private int J2_Refresh_Horizontal;
	private int J2_Refresh_Vertical;

	void Start () {
		J1_Refresh_Horizontal = 0;
		J1_Refresh_Vertical = 0;
		J2_Refresh_Horizontal = 0;
		J2_Refresh_Vertical = 0;
	}

	void Update () {

		Valider = Input.GetButtonDown ("Submit");
		Retour = Input.GetButtonDown ("Cancel");

		////////////////////////////////////////////////////////////////////////////////////////////////////////////
		
		if (J1_Refresh_Horizontal != 1 && (Input.GetAxisRaw ("J1_Keyboard_Horizontal") > 0.5)) {
			J1_Refresh_Horizontal = 1;
			J1_Gauche = false;
			J1_Droit = true;
			J1_Gauche_Pressed = false;
			J1_Droit_Pressed = true;
		} else if (J1_Refresh_Horizontal != -1 && (Input.GetAxisRaw ("J1_Keyboard_Horizontal") < -0.5)) {
			J1_Refresh_Horizontal = -1;
			J1_Gauche = true;
			J1_Droit = false;
			J1_Gauche_Pressed = true;
			J1_Droit_Pressed = false;
		} else if (-0.5 < Input.GetAxisRaw ("J1_Keyboard_Horizontal") && Input.GetAxisRaw ("J1_Keyboard_Horizontal") < 0.5) {
			J1_Refresh_Horizontal = 0;
			J1_Gauche = false;
			J1_Droit = false;
			J1_Gauche_Pressed = false;
			J1_Droit_Pressed = false;
		} else {
			J1_Gauche = false;
			J1_Droit = false;
		}

		if (J1_Refresh_Vertical != 1 && (Input.GetAxisRaw ("J1_Keyboard_Vertical") > 0.5)) {
			J1_Refresh_Vertical = 1;
			J1_Bas = false;
			J1_Haut = true;
		} else if (J1_Refresh_Vertical != -1 && (Input.GetAxisRaw ("J1_Keyboard_Vertical") < -0.5)) {
			J1_Refresh_Vertical = -1;
			J1_Bas = true;
			J1_Haut = false;
		} else if (-0.5 < Input.GetAxisRaw ("J1_Keyboard_Vertical") && Input.GetAxisRaw ("J1_Keyboard_Vertical") < 0.5) {
			J1_Refresh_Vertical = 0;
			J1_Bas = false;
			J1_Haut = false;
		} else {
			J1_Bas = false;
			J1_Haut = false;
		}

		J1_Attaque = Input.GetButtonDown ("J1_Attaque");
		J1_Pouvoir = Input.GetButtonDown ("J1_Pouvoir");
		J1_PouvoirSpe = Input.GetButtonDown ("J1_PouvoirSpe");

		////////////////////////////////////////////////////////////////////////////////////////////////////////////

		if (J2_Refresh_Horizontal != 1 && (Input.GetAxisRaw ("J2_Keyboard_Horizontal") > 0.5)) {
			J2_Refresh_Horizontal = 1;
			J2_Gauche = false;
			J2_Droit = true;
			J2_Gauche_Pressed = false;
			J2_Droit_Pressed = true;
		} else if (J2_Refresh_Horizontal != -1 && (Input.GetAxisRaw ("J2_Keyboard_Horizontal") < -0.5)) {
			J2_Refresh_Horizontal = -1;
			J2_Gauche = true;
			J2_Droit = false;
			J2_Gauche_Pressed = true;
			J2_Droit_Pressed = false;
		} else if (-0.5 < Input.GetAxisRaw ("J2_Keyboard_Horizontal") && Input.GetAxisRaw ("J2_Keyboard_Horizontal") < 0.5) {
			J2_Refresh_Horizontal = 0;
			J2_Gauche = false;
			J2_Droit = false;
			J2_Gauche_Pressed = false;
			J2_Droit_Pressed = false;
		} else {
			J2_Gauche = false;
			J2_Droit = false;
		}

		if (J2_Refresh_Vertical != 1 && (Input.GetAxisRaw ("J2_Keyboard_Vertical") > 0.5)) {
			J2_Refresh_Vertical = 1;
			J2_Bas = false;
			J2_Haut = true;
		} else if (J2_Refresh_Vertical != -1 && (Input.GetAxisRaw ("J2_Keyboard_Vertical") < -0.5)) {
			J2_Refresh_Vertical = -1;
			J2_Bas = true;
			J2_Haut = false;
		} else if (-0.5 < Input.GetAxisRaw ("J2_Keyboard_Vertical") && Input.GetAxisRaw ("J2_Keyboard_Vertical") < 0.5) {
			J2_Refresh_Vertical = 0;
			J2_Bas = false;
			J2_Haut = false;
		} else {
			J2_Bas = false;
			J2_Haut = false;
		}

		J2_Attaque = Input.GetButtonDown ("J2_Attaque");
		J2_Pouvoir = Input.GetButtonDown ("J2_Pouvoir");
		J2_PouvoirSpe = Input.GetButtonDown ("J2_PouvoirSpe");

		////////////////////////////////////////////////////////////////////////////////////////////////////////////
	}
}
