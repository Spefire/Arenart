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

	// Use this for initialization
	void Start () {
		aud = GetComponent<AudioSource>();
		pos = 1;
		select_perso = 1;
		render = GetComponent<Renderer>();
	}

	// Update is called once per frame
	void Update () {
		if (Input.GetAxis("J1_Deplacement") == 1){
			if ((select_perso < 5) && (Camera_Menu.pos == pos)) {
				select_perso = select_perso + 1;
				Affichage ();
			}
		}
		else if (Input.GetAxis("J1_Deplacement") == -1){
			if ((select_perso > 1) && (Camera_Menu.pos == pos)) {
				select_perso = select_perso - 1;
				Affichage ();
			}
		}
	}

	//Autres fonctions
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

