using UnityEngine;
using System.Collections;

public class Musique_Menu : MonoBehaviour {

	public Texture text_sound_on;
	public Texture text_sound_off;
	public AudioClip sound_menu;
	private Renderer render;
	private AudioSource aud;

	void Start () {
		render = GetComponent<Renderer>();
		aud = GetComponent<AudioSource>();
	}

	void Update () {

		if (!aud.isPlaying && aud.loop && Camera_Menu.hasMusic) {
			aud.PlayOneShot(sound_menu);
		} else if (!Camera_Menu.hasMusic) {
			aud.Stop ();
		}

		if (Camera_Menu.hasMusic) {
			render.material.mainTexture = text_sound_on;
		} else {
			render.material.mainTexture = text_sound_off;
		}
	}
}
