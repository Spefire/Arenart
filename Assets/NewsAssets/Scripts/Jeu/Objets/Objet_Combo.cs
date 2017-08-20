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
	private AudioSource aud;
	private SpriteRenderer render;

	void OnEnable () {
		state = 1;
		time_Spirit = (double)Time.time;
		aud = GetComponent<AudioSource>();
		render = GetComponent<SpriteRenderer>();
		aud.PlayOneShot (aud03_punch);
	}

	void Update () {
		double time_cours_Spirit = (double)Time.time - time_Spirit;
		if (time_cours_Spirit >= tmp_recharge_Spirit) {
			state++;
			switch (state) {
			case 2:
				render.sprite = texture_spirit_02;
				aud.PlayOneShot (aud03_punchV2);
				break;
			case 3:
				render.sprite = texture_spirit_03;
				aud.PlayOneShot (aud03_punch);
				break;
			case 4:
				render.sprite = texture_spirit_04;
				aud.PlayOneShot (aud03_punchV2);
				break;
			case 5:
				render.sprite = texture_spirit_05;
				aud.PlayOneShot (aud03_punch);
				break;
			case 6:
				DestroyObject (this.gameObject);
				break;
			}
			time_Spirit = (double)Time.time;
		}
	}
}
