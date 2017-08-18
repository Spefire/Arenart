using UnityEngine;
using System.Collections;

public class Perso_Pouv01_RK: MonoBehaviour {

	//SCRIPT PERSO SPEFIRE

	public bool first;
	public GameObject BouleFeu;
	public GameObject Flammes;
	public Sprite texture_attaque;
	public Sprite texture_pouvoir;
	public Sprite texture_spe;
	public AudioClip aud00_coup;
	public AudioClip aud00_punch;

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
		if (!Canvas_Jeu_RK.isPaused && !Canvas_Jeu_RK.isFinished) {
			Pouvoirs();
		}
		if (stats.vie < 50) {
			body.transformed = true;
			if (render.sprite == body.texture_base_primaire) {
				render.sprite = body.texture_base_secondaire;
			}
		} else {
			body.transformed = false;
			if (render.sprite == body.texture_base_secondaire) {
				render.sprite = body.texture_base_primaire;
			}
		}
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

	//Coup de poing
	private void Action_Attaque(){
		float distance = (transform.position - stats.enemyPos).magnitude;
		if (distance < 2) {
			Perso_Stats_RK statsEnemy = stats.enemy.GetComponent<Perso_Stats_RK> ();
			statsEnemy.SetDamage (statsEnemy.GetDamage (1.25), 5);
			if (stats.enemyPos.x > transform.position.x) {
				stats.enemy.GetComponent<Rigidbody> ().AddForce (Vector3.up * 35, ForceMode.Impulse);
				stats.enemy.GetComponent<Rigidbody> ().AddForce (Vector3.right * 30, ForceMode.Impulse);
			} else {
				stats.enemy.GetComponent<Rigidbody> ().AddForce (Vector3.up * 35, ForceMode.Impulse);
				stats.enemy.GetComponent<Rigidbody> ().AddForce (Vector3.left * 30, ForceMode.Impulse);
			}
			aud.PlayOneShot (aud00_punch);
		} else {
			aud.PlayOneShot (aud00_coup);
		}
	}

	//Boule de feu
	private void Action_Pouvoir(){
		float offX = 1f;
		if (body.turned) {
			offX = -1f;
		}
		Vector3 objPos = new Vector3 (transform.position.x + offX, transform.position.y, transform.position.z);
		GameObject instantiatedProjectile = (GameObject)Instantiate (BouleFeu, objPos, BouleFeu.transform.rotation);
		instantiatedProjectile.GetComponent<Objet_Lance> ().SetConfig (first, body.turned, 1f);
	}

	//Embrasement
	private void Action_Spe(){
		Vector3 objPos = new Vector3 (transform.position.x, transform.position.y, transform.position.z);
		GameObject instantiatedProjectile = (GameObject)Instantiate (Flammes, objPos, Flammes.transform.rotation);
		instantiatedProjectile.GetComponent<Objet_Suivi> ().SetConfig (first, body.turned, true);
	}
}

