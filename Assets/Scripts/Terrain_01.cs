using UnityEngine;
using System.Collections;

public class Terrain_01 : MonoBehaviour {

	public float speed;
	public bool destruct;
	public float posFinal;
	private float temps;
	private float pos;

	// Use this for initialization
	void Start () {
		temps = Time.time;
		pos = transform.localPosition.y;
	}
	
	// Update is called once per frame
	void Update () {
		if (Time.time - temps > 45) {
			if (transform.localPosition.y > (pos - posFinal)) {
				transform.Translate (-Vector2.up * speed * Time.deltaTime);
			}
			else if (destruct) {
				DestroyObject (this.gameObject);
			}
		}
	}
}
