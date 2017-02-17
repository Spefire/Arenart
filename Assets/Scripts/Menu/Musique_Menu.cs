using UnityEngine;
using System.Collections;

public class Musique_Menu : MonoBehaviour {

	public Texture text_sound_on;
	public Texture text_sound_off;
	private Renderer render;

	void Start () {
		render = GetComponent<Renderer>();
	}

	void Update () {
		if (Camera_Menu.hasMusic) {
			render.material.mainTexture = text_sound_on;
		} else {
			render.material.mainTexture = text_sound_off;
		}
	}

}
