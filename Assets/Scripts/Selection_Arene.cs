using UnityEngine;
using System.Collections;
public class Selection_Arene : MonoBehaviour {

	//Variables
	private int pos;
	public static int select_arene;
	public Texture texture_select_arene_1;
	public Texture texture_select_arene_2;
	public Texture texture_select_arene_3;
	public AudioClip son_selection;
	private Renderer render;
	private AudioSource aud;

	// Use this for initialization
	void Start () {
		aud = GetComponent<AudioSource>();
		pos = 3;
		select_arene = 1;
		render = GetComponent<Renderer>();
	}

	// Update is called once per frame
	void Update () {
		if (Input.GetAxis("J1_Deplacement") == 1){
			if ((select_arene < 3) && (Camera_Menu.pos == pos)) {
				select_arene = select_arene + 1;
				Affichage ();
			}
		}
		if (Input.GetAxis("J1_Deplacement") == -1){
			if ((select_arene > 1) && (Camera_Menu.pos == pos)) {
				select_arene = select_arene - 1;
				Affichage ();
			}
		}
	}

	//Autres fonctions
	void Affichage (){
		aud.PlayOneShot (son_selection);
		switch (select_arene) {
		case 1:
			render.material.mainTexture = texture_select_arene_1;
			break;
		case 2:
			render.material.mainTexture = texture_select_arene_2;
			break;
		case 3:
			render.material.mainTexture = texture_select_arene_3;
			break;
		}
	}
}

