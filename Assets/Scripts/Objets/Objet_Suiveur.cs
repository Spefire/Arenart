using UnityEngine;
using System.Collections;

public class Objet_Suiveur : MonoBehaviour {

	public string cible;
	private GameObject joueur;

	// Update is called once per frame
	void Update () {
		joueur = GameObject.FindGameObjectWithTag (cible);
		transform.position = joueur.transform.position;
	}
}
