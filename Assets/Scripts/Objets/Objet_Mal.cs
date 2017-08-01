using UnityEngine;
using System.Collections;

public class Objet_Mal : MonoBehaviour {

	public string cible;
	private float vitesse;
	private float taille;
	private bool first;
	private bool turned;
	private GameObject joueur;
	private Renderer render;
	public Texture texture_finn_big;
	public Texture texture_finn_big_inv;

	void Start(){
		taille = 0f;
		vitesse = 0.20f;
		first = true;
		render = GetComponent<Renderer>();
		if (cible == "J1") {
			turned = true; //TODO Not always true
		}else if (cible == "J2") {
			turned = true; //TODO
		}
	}

	void Update () {
		if (taille < 4.5f) {
			taille += vitesse;
		} else if (taille >= 4.5f && taille < 9f) {
			transform.localScale += new Vector3 (vitesse/2, vitesse/2, 0);
			taille += vitesse/2;
			if (first) {
				if (turned) {
					render.material.mainTexture = texture_finn_big;
				} else {
					render.material.mainTexture = texture_finn_big_inv;
				}
				first = false;
			}
		}
	}
}
