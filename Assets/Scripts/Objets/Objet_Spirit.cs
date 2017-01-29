using UnityEngine;
using System.Collections;

public class Objet_Spirit : MonoBehaviour {

	private Renderer render;
	public Texture texture_spirit_02;
	public Texture texture_spirit_03;
	public Texture texture_spirit_04;
	public Texture texture_spirit_05;
	private double time_Spirit;
	private double tmp_recharge_Spirit = 0.2;
	private int state;
	private AudioSource aud;
	public AudioClip aud03_punch;
	public AudioClip aud03_punchV2;

	// Use this for initialization
	void Start () {
		aud = GetComponent<AudioSource>();
		state = 1;
		time_Spirit = (double)Time.time;
		render = GetComponent<Renderer>();
		aud.PlayOneShot (aud03_punch);
	}
	
	// Update is called once per frame
	void Update () {
		double time_cours_Spirit = (double)Time.time - time_Spirit;
		if(time_cours_Spirit >= tmp_recharge_Spirit){
			switch (state) {
			case 1:
				render.material.mainTexture = texture_spirit_02;
				aud.PlayOneShot (aud03_punchV2);
				break;
			case 2:
				render.material.mainTexture = texture_spirit_03;
				aud.PlayOneShot (aud03_punch);
				break;
			case 3:
				render.material.mainTexture = texture_spirit_04;
				aud.PlayOneShot (aud03_punchV2);
				break;
			case 4:
				render.material.mainTexture = texture_spirit_05;
				aud.PlayOneShot (aud03_punch);
				break;
			}
			state++;
			time_Spirit = (double)Time.time;
		}
	}
}
