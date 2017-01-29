using UnityEngine;
using System.Collections;

public class Joueur_Touche : MonoBehaviour {
	
	public Texture texture_trans;
	public Texture texture_touche;
	public static bool touche = false;
	private Renderer render;
	private bool cours = false;
	private int time_debut;
	private int time_fin = 1;
	
	// Use this for initialization
	void Start () {
		render = GetComponent<Renderer>();
	}
	
	// Update is called once per frame
	void Update () {
		if (touche && !cours) {
			time_debut = (int) Time.time;
			cours = true;
			render.material.mainTexture = texture_touche;
		} else if(touche && cours){
			int time_cours = (int)Time.time - time_debut;
			if(time_cours == time_fin){
				cours = false;
				touche = false;
			}
		} else {
			render.material.mainTexture = texture_trans;
		}
	}
}
