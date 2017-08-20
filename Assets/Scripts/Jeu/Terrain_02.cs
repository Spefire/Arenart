using UnityEngine;
using System.Collections;

public class Terrain_02 : MonoBehaviour {

	private bool versDroite;
	private float speed;

	void Start(){
		versDroite = true;
		speed = 4f;
	}

	// Update is called once per frame
	void Update () {
		float x = transform.localPosition.x;
		if (x < 6f && versDroite) {
			transform.Translate (Vector2.right * speed * Time.deltaTime);
		}
		else if (x >= 6f) {
			versDroite = false;
		}
		if (x > -6f && !versDroite) {
			transform.Translate (-Vector2.right * speed * Time.deltaTime);
		}
		else if (x <= -6f) {
			versDroite = true;
		}
	}
}
