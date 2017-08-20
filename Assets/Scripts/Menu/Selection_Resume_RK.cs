using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Selection_Resume_RK : MonoBehaviour {

	public Sprite texture_perso1;
	public Sprite texture_perso2;
	public Sprite texture_perso3;
	public Sprite texture_perso4;
	public Sprite texture_perso5;
	public Sprite texture_arene1;
	public Sprite texture_arene2;
	public Sprite texture_arene3;
	public Image selection_p1;
	public Image selection_p2;
	public Image selection_a;

	void OnEnable () {
		ShowSelection ();
	}

	void ShowSelection() {
		switch (Selection_Perso1_RK.select_perso) {
		case 1:
			selection_p1.sprite = texture_perso1;
			break;
		case 2:
			selection_p1.sprite = texture_perso2;
			break;
		case 3:
			selection_p1.sprite = texture_perso3;
			break;
		case 4:
			selection_p1.sprite = texture_perso4;
			break;
		case 5:
			selection_p1.sprite = texture_perso5;
			break;
		}
		switch (Selection_Perso2_RK.select_perso) {
		case 1:
			selection_p2.sprite = texture_perso1;
			break;
		case 2:
			selection_p2.sprite = texture_perso2;
			break;
		case 3:
			selection_p2.sprite = texture_perso3;
			break;
		case 4:
			selection_p2.sprite = texture_perso4;
			break;
		case 5:
			selection_p2.sprite = texture_perso5;
			break;
		}
		switch (Selection_Arene_RK.select_arene) {
		case 1:
			selection_a.sprite = texture_arene1;
			break;
		case 2:
			selection_a.sprite = texture_arene2;
			break;
		case 3:
			selection_a.sprite = texture_arene3;
			break;
		}
	}
}

