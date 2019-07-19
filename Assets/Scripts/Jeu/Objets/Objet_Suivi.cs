using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Objet_Suivi : MonoBehaviour {

	public double coeffDegats;
	public int resistance;
	public bool followAlly;
	public bool followEnemy;

	private GameObject ally;
	private GameObject enemy;
	private bool inversed;
	private bool canDamaged;
	private SpriteRenderer render;

	void OnEnable () {
		render = GetComponent<SpriteRenderer>();
	}

	void Update () {
		if (followAlly) {
			this.transform.position = ally.transform.position;
		} else if (followEnemy) {
			this.transform.position = enemy.transform.position;
		}
	}

	public void SetConfig(bool first, bool inversed, bool canDamaged) {
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
		this.canDamaged = canDamaged;
		Physics.IgnoreCollision(this.GetComponent<Collider>(), ally.transform.root.GetComponent<Collider>());
	}

	void OnTriggerEnter(Collider objetInfo) {
		if (objetInfo.gameObject == enemy && canDamaged) {
			Perso_Stats_RK stats = enemy.GetComponent<Perso_Stats_RK> ();
			stats.SetDamage (stats.GetDamage (coeffDegats), resistance);
			Destroy (this.gameObject);
		}
	}

}
