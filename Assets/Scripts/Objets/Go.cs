using UnityEngine;
using System.Collections;

public class Go : MonoBehaviour {

	private float vitesse;
	private float taille;

	void Start(){
		taille = 0f;
		vitesse = 0.075f;
	}

	void Update () {

		//Changer la position
		/*Vector3 pos = transform.position;
		pos.y = pos.y - vitesse/2;
		transform.position = pos;*/

		//Agrandir ou retrecir
		if (taille < 5f) {
			transform.localScale += new Vector3 (3*vitesse, 2*vitesse, 0);
			taille += vitesse;
		} else if (taille >= 5f) {
			Destroy (gameObject);
		}

	}
}
