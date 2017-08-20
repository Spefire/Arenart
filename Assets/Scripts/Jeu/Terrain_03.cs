using UnityEngine;
using System.Collections;

public class Terrain_03 : MonoBehaviour {

	private bool versHaut;
	private float speed;

	void Start(){
		versHaut = true;
		speed = 2.5f;
	}

	// Update is called once per frame
	void Update () {
		float y = transform.localPosition.y;
		if (y < 3.5f && versHaut) {
			transform.Translate (Vector2.up * speed * Time.deltaTime);
		}
		else if (y >= 3.5f) {
			versHaut = false;
		}
		if (y > -5f && !versHaut) {
			transform.Translate (-Vector2.up * speed * Time.deltaTime);
		}
		else if (y <= 5f) {
			versHaut = true;
		}
	}
}
