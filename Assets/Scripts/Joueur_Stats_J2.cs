using UnityEngine;
using System.Collections;
public class Joueur_Stats_J2: MonoBehaviour {

	public static int nbVie;
	public static double Vie;
	public static float Resistance;
	public GameObject spawn;
	//private AudioSource aud;

	void Start () {
		//aud = GetComponent<AudioSource>();
		nbVie = 5;
		Vie = 100;
		Resistance = 100;
		spawn = GameObject.FindGameObjectWithTag ("Spawn_J2");
	}

	void Update () {
		if (Vie <= 0) {
			print ("Le Joueur 2 est mort");
			nbVie--;
			if (nbVie != 0) {
				transform.position = spawn.transform.position;
				Vie = 100;
				Resistance = 100;
			} else {
				Vie = 100;
				Resistance = 100;
				print ("Le Joueur 2 a perdu");
				Camera_Jeu.J1_Gagne = true;
			}
		}
	}

	public static void getDamage (double dmg, int rest){
		/////////////////////////////////////////////////////
		if(dmg > 0){
			Indic_Vie_J2.set_Vie_Moins();
			if (Vie - dmg < 1) {
				Vie = 0;
			} else {
				Vie -= dmg;
			}
		} else if(dmg < 0){
			Indic_Vie_J2.set_Vie_Plus();
			if (Vie - dmg > 100) {
				Vie = 100;
			} else {
				Vie -= dmg;
			}
		}
		/////////////////////////////////////////////////////
		if(rest > 0){
			Indic_Resist_J2.set_Resist_Moins();
			if (Resistance - rest < 1) {
				Resistance = 0;
			} else {
				Resistance -= rest;
			}
		} else if(rest < 0){
			Indic_Resist_J2.set_Resist_Plus();
			if (Resistance - rest > 100) {
				Resistance = 100;
			} else {
				Resistance -= rest;
			}
		}
	}
}

