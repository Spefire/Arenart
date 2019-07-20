using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Selection_Arene_RK : MonoBehaviour {

	public static int select_arene;
	public Text textSelectArene;
	public AudioClip son_selection;
	private AudioSource aud;

	void OnEnable () {
		select_arene = 1;
		aud = GetComponent<AudioSource>();
	}
		
	void Update () {
		if (Game_Inputs.J1_Droit && select_arene < 3) {
			select_arene++;
			Affichage ();
		}
		else if (Game_Inputs.J1_Gauche && select_arene > 1) {
			select_arene--;
			Affichage ();
		}
	}
		
	void Affichage (){
		aud.PlayOneShot (son_selection);
		switch (select_arene) {
		case 1:
			textSelectArene.text = "Le Volcan de Célestia";
			break;
		case 2:
			textSelectArene.text = "Le Colisée de Faradel";
			break;
		case 3:
			textSelectArene.text = "La Forêt d'Alsos";
			break;
		}
	}
}

