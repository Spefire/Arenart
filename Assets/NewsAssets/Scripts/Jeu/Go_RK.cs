using UnityEngine;
using System.Collections;

public class Go_RK : MonoBehaviour {

	private float vitesse;
	private float taille;

	void Start(){
		taille = 0f;
		vitesse = 0.05f;
	}

	void Update () {
		if (taille < 2f) {
			transform.localScale += new Vector3 (1.5f*vitesse, vitesse, 0f);
			taille += vitesse;
		} else if (taille >= 2f) {
			Destroy (gameObject);
		}
	}
}
