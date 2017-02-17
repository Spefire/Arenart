using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Affichage_Aides : MonoBehaviour {

	private int pos;
	public Texture texture_aide_0;
	public Texture texture_aide_1;
	public Texture texture_aide_2;
	public Texture texture_aide_3;
	public Texture texture_aide_4;
	public Texture texture_aide_5;
	private Renderer render;


	void Start () {
		pos = 5;
		render = GetComponent<Renderer>();
	}


	void Update () {
		if (Camera_Menu.pos == pos) {
			switch (Camera_Menu.help_pos) {
			case 0:
				render.material.mainTexture = texture_aide_0;
				break;
			case 1:
				render.material.mainTexture = texture_aide_1;
				break;
			case 2:
				render.material.mainTexture = texture_aide_2;
				break;
			case 3:
				render.material.mainTexture = texture_aide_3;
				break;
			case 4:
				render.material.mainTexture = texture_aide_4;
				break;
			case 5:
				render.material.mainTexture = texture_aide_5;
				break;
			}
		}
	}
}
