using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Objet_Fouet : MonoBehaviour {

	public double coeffDegats;
	public int resistance;

	private GameObject ally;
	private GameObject enemy;
	private bool inversed;
	private SpriteRenderer render;

	void OnEnable () {
		render = GetComponent<SpriteRenderer>();
	}

	void Update () {
		this.transform.position = ally.transform.position;
	}

	public void SetConfig(bool first, bool inversed) {
		if (first) {
			ally = GameObject.FindGameObjectWithTag ("J1");
			enemy = GameObject.FindGameObjectWithTag ("J2");
		} else {
			ally = GameObject.FindGameObjectWithTag ("J2");
			enemy = GameObject.FindGameObjectWithTag ("J1");
		}
		if (inversed) {
			render.flipX = true;
		}
		Physics.IgnoreCollision(this.GetComponent<Collider>(), ally.transform.root.GetComponent<Collider>());
	}

	void OnTriggerEnter(Collider objetInfo) {
		if (objetInfo.gameObject == enemy) {
			Perso_Stats_RK statsEnemy = enemy.GetComponent<Perso_Stats_RK> ();
			statsEnemy.SetDamage (statsEnemy.GetDamage (coeffDegats), resistance);
			if (enemy.transform.position.x > transform.position.x) {
				enemy.GetComponent<Rigidbody>().AddForce (Vector3.up * 120, ForceMode.Impulse);
				enemy.GetComponent<Rigidbody>().AddForce (Vector3.right * 30, ForceMode.Impulse);
			} else {
				enemy.GetComponent<Rigidbody>().AddForce (Vector3.up * 120, ForceMode.Impulse);
				enemy.GetComponent<Rigidbody>().AddForce (Vector3.left * 30, ForceMode.Impulse);
			}
			Destroy (this.gameObject);
		}
	}

}
