using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Objet_Finn : MonoBehaviour {

	public int veloX;
	public int veloY;
	public double coeffDegats;
	public int resistance;

	private bool big;
	private float speed = 7.5f;
	private float currentTime;
	private float maxTime = 1f;
	public Sprite texture_big;

	private GameObject ally;
	private GameObject enemy;
	private bool inversed;
	private SpriteRenderer render;

	void OnEnable () {
		render = GetComponent<SpriteRenderer>();
	}

	void Update() {
		if (!big && Time.time-currentTime >= maxTime) {
			big = true;
			render.sprite = texture_big;
			coeffDegats += 0.5;
		}
		if (big && transform.localScale.y < 5) {
			transform.localScale += new Vector3 (Time.deltaTime*speed/2, Time.deltaTime*speed, 0);
		}
	}

	public void SetConfig(bool first, bool inversed, float coeff) {
		big = false;
		currentTime = Time.time;
		if (first) {
			ally = GameObject.FindGameObjectWithTag ("J1");
			enemy = GameObject.FindGameObjectWithTag ("J2");
		} else {
			ally = GameObject.FindGameObjectWithTag ("J2");
			enemy = GameObject.FindGameObjectWithTag ("J1");
		}
		if (inversed) {
			render.flipX = true;
			Vector3 velocity = new Vector3 (coeff * -veloX * Mathf.Sign (transform.forward.x), coeff * veloY * Mathf.Sign (transform.forward.y), 0);
			this.GetComponent<Rigidbody> ().velocity = velocity;
		} else {
			Vector3 velocity = new Vector3 (coeff * veloX * Mathf.Sign (transform.forward.x), coeff * veloY * Mathf.Sign (transform.forward.y), 0);
			this.GetComponent<Rigidbody> ().velocity = velocity;
		}
		Physics.IgnoreCollision(this.GetComponent<Collider>(), ally.transform.root.GetComponent<Collider>());
	}

	void OnTriggerEnter(Collider objetInfo) {
		if (objetInfo.gameObject == enemy) {
			Perso_Stats_RK stats = enemy.GetComponent<Perso_Stats_RK> ();
			stats.SetDamage (stats.GetDamage (coeffDegats), resistance);
			DestroyObject (this.gameObject);
		}
	}

	void OnCollisionEnter(Collision objetInfo) {
		if (objetInfo.gameObject == enemy) {
			Perso_Stats_RK stats = enemy.GetComponent<Perso_Stats_RK> ();
			stats.SetDamage (stats.GetDamage (coeffDegats), resistance);
			DestroyObject (this.gameObject);
		}
	}
}
