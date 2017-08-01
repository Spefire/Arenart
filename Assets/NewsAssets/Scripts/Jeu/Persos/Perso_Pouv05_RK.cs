using UnityEngine;
using System.Collections;

public class Perso_Pouv05_RK: MonoBehaviour {

	//SCRIPT PERSO DUNKY

	public bool first;
	public GameObject Fracas;
	public GameObject LanceFinn;
	public Sprite texture_attaque;
	public Sprite texture_pouvoir;
	public Sprite texture_spe;
	public AudioClip aud00_coup;
	public AudioClip aud00_punch;
	public AudioClip aud05_fracas;

	private AudioSource aud;
	private SpriteRenderer render;
	private Perso_Body_RK body;
	private Perso_Stats_RK stats;

	//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

	void Start () {
		aud = GetComponent<AudioSource>();
		render = GetComponent<SpriteRenderer>();
		body = GetComponent<Perso_Body_RK> ();
		stats = GetComponent<Perso_Stats_RK> ();
	}

	void Update () {
		Pouvoirs();
	}
		
	//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

	private void Pouvoirs (){
		if ((first && Game_Inputs.J1_Attaque) || (!first && Game_Inputs.J2_Attaque)) {
			if (body.recharge_Phy) {
				render.sprite = texture_attaque;
				body.Attaque ();
				Action_Attaque ();
			}
		} else if ((first && Game_Inputs.J1_Pouvoir) || (!first && Game_Inputs.J2_Pouvoir)) {
			if (body.recharge_Mag) {
				body.Pouvoir ();
				render.sprite = texture_pouvoir;
				Action_Pouvoir ();
			}
		} else if ((first && Game_Inputs.J1_PouvoirSpe) || (!first && Game_Inputs.J2_PouvoirSpe)) {
			if (body.recharge_Spe) {
				body.PouvoirSpe ();
				render.sprite = texture_spe;
				Action_Spe ();
			}
		}
	}

	//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

	//Frappe
	void Action_Attaque(){
		float distance = (transform.position - stats.enemyPos).magnitude;
		if (distance < 2) {
			Perso_Stats_RK statsEnemy = stats.enemy.GetComponent<Perso_Stats_RK> ();
			statsEnemy.SetDamage (statsEnemy.GetDamage (1), 5);
			stats.enemy.GetComponent<Rigidbody> ().AddForce (Vector3.up * 25, ForceMode.Impulse);
			aud.PlayOneShot (aud00_punch);
		} else {
			aud.PlayOneShot (aud00_coup);
		}
	}

	//Fracas
	void Action_Pouvoir(){
		body.SetFracased ();
		transform.Translate(new Vector3(0, 3f, 0));
		GetComponent<Rigidbody>().AddForce (-Vector3.up * 150, ForceMode.Impulse);
	}

	//Finnix
	void Action_Spe(){
		body.SetStunned(1f);
		float offX = 1f;
		if (body.turned) {
			offX = -1f;
		}
		Vector3 objPos = new Vector3 (transform.position.x + offX, transform.position.y, transform.position.z);
		GameObject instantiatedProjectile = (GameObject)Instantiate (LanceFinn, objPos, LanceFinn.transform.rotation);
		instantiatedProjectile.GetComponent<Objet_Lance> ().SetConfig (first, body.turned, 1f);
	}

	//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

	public void UseFracased() {
		body.SetStunned(1f);
		Vector3 objPos = new Vector3 (transform.position.x, transform.position.y - 0.5f, transform.position.z);
		GameObject instantiatedProjectile = (GameObject)Instantiate (Fracas, objPos, Fracas.transform.rotation);
		instantiatedProjectile.GetComponent<Objet_Suivi> ().SetConfig (first, body.turned, false);
		aud.PlayOneShot (aud05_fracas);
		float distance = (transform.position - stats.enemyPos).magnitude;
		if (distance < 2) {
			Perso_Stats_RK statsEnemy = stats.enemy.GetComponent<Perso_Stats_RK> ();
			statsEnemy.SetDamage (statsEnemy.GetDamage (1.25), 5);
			stats.enemy.GetComponent<Rigidbody> ().AddForce (Vector3.up * 25, ForceMode.Impulse);
			if (transform.position.x - stats.enemyPos.x > 0) {
				stats.enemy.GetComponent<Rigidbody> ().AddForce (Vector3.left * 25, ForceMode.Impulse);
			} else {
				stats.enemy.GetComponent<Rigidbody> ().AddForce (Vector3.right * 25, ForceMode.Impulse);
			}
		}
	}
}

