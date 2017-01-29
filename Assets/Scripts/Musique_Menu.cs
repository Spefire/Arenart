using UnityEngine;
using System.Collections;

public class Musique_Menu : MonoBehaviour {

	private bool sound;
	private AudioSource aud;
	public AudioClip sound_menu;
	public Texture text_sound_on;
	public Texture text_sound_off;

	void Start () {
		sound = true;
		aud = GetComponent<AudioSource>();
	}

	void Update () {
		if (!aud.isPlaying && aud.loop && sound) {
			aud.PlayOneShot(sound_menu);
		} else if (!sound) {
			aud.Stop ();
		}
	}

	void OnGUI() {
		if (Camera_Menu.pos == 0) {
			if (sound) {
				if (GUI.Button (new Rect (50, 50, 50, 50), text_sound_on)) { 
					print ("Musique : OFF");
					sound = false;
				}
			} else {
				if (GUI.Button (new Rect (50, 50, 50, 50), text_sound_off)) { 
					print ("Musique : ON");
					sound = true;
				}
			}
		}
	}
}
