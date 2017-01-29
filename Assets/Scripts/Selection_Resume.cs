using UnityEngine;
using System.Collections;

public class Selection_Resume : MonoBehaviour {

	//Variables
	public int selection;
	private int select;
	public Texture texture_select_1;
	public Texture texture_select_2;
	public Texture texture_select_3;
	public Texture texture_select_4;
	public Texture texture_select_5;
	private Renderer render;
	//private AudioSource aud;

	// Use this for initialization
	void Start () {
		//aud = GetComponent<AudioSource>();
		render = GetComponent<Renderer>();
	}

	// Update is called once per frame
	void Update () {
		if (selection == 1) {
			select = Selection_P1.select_perso;
            Affichage ();
		}else if(selection == 2) {
			select = Selection_P2.select_perso;
            Affichage ();
		}else if(selection == 3) {
			select = Selection_Arene.select_arene;
            Affichage ();
		}
	}

	//Autres fonctions
	void Affichage (){
		switch (select) {
		case 1:
			render.material.mainTexture = texture_select_1;
			break;
		case 2:
			render.material.mainTexture = texture_select_2;
			break;
		case 3:
			render.material.mainTexture = texture_select_3;
			break;
		case 4:
			render.material.mainTexture = texture_select_4;
			break;
		case 5:
			render.material.mainTexture = texture_select_5;
			break;
		}
	}
}

