using UnityEngine;
using System.Collections;
public class Selection_P1 : MonoBehaviour {

	//Variables
	private int pos;
	public static int select_perso;
	public Texture texture_select_perso_1;
	public Texture texture_select_perso_2;
	public Texture texture_select_perso_3;
	public Texture texture_select_perso_4;
	public Texture texture_select_perso_5;
	public AudioClip son_selection;
	private Renderer render;
	private AudioSource aud;


	void Start () {
		pos = 1;
		select_perso = 1;
		render = GetComponent<Renderer>();
		aud = GetComponent<AudioSource>();
	}


	void Update () {
		if (Camera_Menu.pos == pos) {
			if (Choice_J1.choice == 0 && Game_Inputs.J1_Droit){
				if (select_perso < 5) {
					select_perso = select_perso + 1;
					Affichage ();
				}
			}
			else if (Choice_J1.choice == 0 && Game_Inputs.J1_Gauche){
				if (select_perso > 1) {
					select_perso = select_perso - 1;
					Affichage ();
				}
			}
		}
	}


	void Affichage (){
		aud.PlayOneShot (son_selection);
		switch (select_perso) {
		case 1:
			render.material.mainTexture = texture_select_perso_1;
			break;
		case 2:
			render.material.mainTexture = texture_select_perso_2;
			break;
		case 3:
			render.material.mainTexture = texture_select_perso_3;
			break;
		case 4:
			render.material.mainTexture = texture_select_perso_4;
			break;
		case 5:
			render.material.mainTexture = texture_select_perso_5;
			break;
		}
	}
}

