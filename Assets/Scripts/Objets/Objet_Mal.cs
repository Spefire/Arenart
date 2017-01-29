using UnityEngine;
using System.Collections;

public class Objet_Mal : MonoBehaviour {

	public string cible;
	private float vitesse;
	private float taille;
	private bool first;
	private bool versGauche;
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
			versGauche = Joueur_Deplacement_J1.versGauche;
		}else if (cible == "J2") {
			versGauche = Joueur_Deplacement_J2.versGauche;
		}
	}

	void Update () {

		/*//Changer la position
		Vector3 pos = joueur.transform.position;
		if(versGauche){
			pos.x = pos.x - 2f - taille/2;
		}else{
			pos.x = pos.x + 2f + taille/2;
		}
		transform.position = pos;*/

		//Agrandir ou retrecir
		if (taille < 4.5f) {
			//transform.localScale += new Vector3 (vitesse/2, 0, 0);
			taille += vitesse;
		} else if (taille >= 4.5f && taille < 9f) {
			transform.localScale += new Vector3 (vitesse/2, vitesse/2, 0);
			taille += vitesse/2;
			if (first) {
				if (versGauche) {
					render.material.mainTexture = texture_finn_big;
				} else {
					render.material.mainTexture = texture_finn_big_inv;
				}
				first = false;
			}
		} else if (taille >= 9f) {
			//Destroy (gameObject);
		}

	}
}
