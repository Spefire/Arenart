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


	void Start () {
		pos = 3;
		select_arene = 1;
		aud = GetComponent<AudioSource>();
		render = GetComponent<Renderer>();
	}


	void Update () {
		if (Camera_Menu.pos == pos) {
			if (Choice_Arene.choice == 0 && Game_Inputs.J1_Droit){
				if (select_arene < 5) {
					select_arene = select_arene + 1;
					Affichage ();
				}
			}
			else if (Choice_Arene.choice == 0 && Game_Inputs.J1_Gauche){
				if (select_arene > 1) {
					select_arene = select_arene - 1;
					Affichage ();
				}
			}
		}
	}


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

