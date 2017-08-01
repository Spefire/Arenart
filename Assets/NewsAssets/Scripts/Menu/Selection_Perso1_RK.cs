using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Selection_Perso1_RK : MonoBehaviour {

	public static int select_perso;
	public Text textSelectPerso;
	public AudioClip son_selection;
	private AudioSource aud;

	void OnEnable () {
		select_perso = 1;
		aud = GetComponent<AudioSource>();
	}
		
	void Update () {
		if (Game_Inputs.J1_Droit && select_perso < 5) {
			select_perso++;
			Affichage ();
		}
		else if (Game_Inputs.J1_Gauche && select_perso > 1) {
			select_perso--;
			Affichage ();
		}
	}
		
	void Affichage (){
		aud.PlayOneShot (son_selection);
		switch (select_perso) {
		case 1:
			textSelectPerso.text = "Spefire, le combattant de feu";
			break;
		case 2:
			textSelectPerso.text = "Apkareru, le sorcier aqueux";
			break;
		case 3:
			textSelectPerso.text = "Ilana, la gardienne de la forêt";
			break;
		case 4:
			textSelectPerso.text = "Kriza Lied, la mangaka cosplayée";
			break;
		case 5:
			textSelectPerso.text = "Dunklayth, le sale gosse";
			break;
		}
	}
}

