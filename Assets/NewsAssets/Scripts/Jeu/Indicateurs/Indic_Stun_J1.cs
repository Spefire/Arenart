using UnityEngine;
using System.Collections;

public class Indic_Stun_J1 : MonoBehaviour {

	public Texture texture_base;
	public Texture texture_stun;
	private static bool stunned;
	private Renderer render;

	void Start () {
		render = GetComponent<Renderer>();
		stunned = false;
	}

	void Update () {
		/*if (Joueur_Deplacement_J1.stunned && !stunned) {
			stunned = true;
			render.material.mainTexture = texture_stun;
		}
		else if (!Joueur_Deplacement_J1.stunned && stunned) {
			stunned = false;
			render.material.mainTexture = texture_base;
		}*/
	}
}
