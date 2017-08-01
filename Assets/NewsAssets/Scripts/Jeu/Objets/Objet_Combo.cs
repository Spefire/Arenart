using UnityEngine;
using System.Collections;

public class Objet_Combo : MonoBehaviour {

	public Sprite texture_spirit_02;
	public Sprite texture_spirit_03;
	public Sprite texture_spirit_04;
	public Sprite texture_spirit_05;
	public AudioClip aud03_punch;
	public AudioClip aud03_punchV2;

	private double time_Spirit;
	private double tmp_recharge_Spirit = 0.2;
	private int state;
	private bool useCombo;
	private AudioSource aud;
	private SpriteRenderer render;

	void OnEnable () {
		state = 1;
		useCombo = false;
		aud = GetComponent<AudioSource>();
		render = GetComponent<SpriteRenderer>();
		aud.PlayOneShot (aud03_punch);
	}

	void Update () {
		if (useCombo) {
			double time_cours_Spirit = (double)Time.time - time_Spirit;
			if (time_cours_Spirit >= tmp_recharge_Spirit) {
				switch (state) {
				case 1:
					render.sprite = texture_spirit_02;
					aud.PlayOneShot (aud03_punchV2);
					break;
				case 2:
					render.sprite = texture_spirit_03;
					aud.PlayOneShot (aud03_punch);
					break;
				case 3:
					render.sprite = texture_spirit_04;
					aud.PlayOneShot (aud03_punchV2);
					break;
				case 4:
					render.sprite = texture_spirit_05;
					aud.PlayOneShot (aud03_punch);
					break;
				}
				state++;
				time_Spirit = (double)Time.time;
			}
		}
	}

	public void UseCombo() {
		useCombo = true;
		time_Spirit = (double)Time.time;
	}
}
