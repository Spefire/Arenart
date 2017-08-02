using UnityEngine;
using System.Collections;

public class Perso_Pouv04_RK: MonoBehaviour {

	//SCRIPT PERSO KRIZA LIED

	public bool first;
	public GameObject Encre;
	public GameObject BallOmbre;
	public Sprite texture_attaque;
	public Sprite texture_attaque_Noctali;
	public Sprite texture_pouvoir;
	public Sprite texture_pouvoir_Noctali;
	public Sprite texture_spe;
	public Sprite texture_spe_Noctali;
	public AudioClip aud04_smosh;
	public AudioClip aud04_slash;
	public AudioClip aud04_change;
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
	}
		
	//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

	private void Pouvoirs (){
		if ((first && Game_Inputs.J1_Attaque) || (!first && Game_Inputs.J2_Attaque)) {
			if (body.recharge_Phy) {
				body.Attaque ();
				if (!body.transformed) {
					render.sprite = texture_attaque;
					Action_Attaque ();
				} else {
					render.sprite = texture_attaque_Noctali;
					Action_Attaque_Noctali ();
				}
			}
		} else if ((first && Game_Inputs.J1_Pouvoir) || (!first && Game_Inputs.J2_Pouvoir)) {
			if (body.recharge_Mag) {
				body.Pouvoir ();
				if (!body.transformed) {
					render.sprite = texture_pouvoir;
					Action_Pouvoir ();
				} else {
					render.sprite = texture_pouvoir_Noctali;
					Action_Pouvoir_Noctali ();
				}
			}
		} else if ((first && Game_Inputs.J1_PouvoirSpe) || (!first && Game_Inputs.J2_PouvoirSpe)) {
			if (body.recharge_Spe) {
				body.PouvoirSpe ();
				if (!body.transformed) {
					aud.PlayOneShot (aud04_change);
					render.sprite = texture_spe;
				} else {
					aud.PlayOneShot (aud04_change);
					render.sprite = texture_spe_Noctali;
				}
				body.transformed = !body.transformed;
			}
		}
	}

	//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

	//Lance
	private void Action_Attaque(){
		float distance = (transform.position - stats.enemyPos).magnitude;
		if (distance < 2) {
			Perso_Stats_RK statsEnemy = stats.enemy.GetComponent<Perso_Stats_RK> ();
			statsEnemy.SetDamage (statsEnemy.GetDamage (1), 5);
			stats.enemy.GetComponent<Rigidbody> ().AddForce (Vector3.up * 25, ForceMode.Impulse);
			aud.PlayOneShot (aud04_slash);
		} else {
			aud.PlayOneShot (aud04_smosh);
		}
	}

	//ViveAttaque
	private void Action_Attaque_Noctali(){
		if (body.turned) {
			transform.Translate (Vector2.left * 1.25f);
			GetComponent<Rigidbody> ().AddForce (Vector3.up * 30, ForceMode.Impulse);
			GetComponent<Rigidbody> ().AddForce (Vector3.left * 30, ForceMode.Impulse);
		} else {
			transform.Translate (Vector2.right * 1.25f);
			GetComponent<Rigidbody> ().AddForce (Vector3.up * 30, ForceMode.Impulse);
			GetComponent<Rigidbody> ().AddForce (-Vector3.left * 30, ForceMode.Impulse);
		}
		float distance = (transform.position - stats.enemyPos).magnitude;
		if (distance < 2) {
			Perso_Stats_RK statsEnemy = stats.enemy.GetComponent<Perso_Stats_RK> ();
			statsEnemy.SetDamage (statsEnemy.GetDamage (1.25), 0);
			aud.PlayOneShot (aud00_punch);
		} else {
			aud.PlayOneShot (aud00_coup);
		}
	}

	//Encre
	private void Action_Pouvoir(){
		float offX = 1f;
		if (body.turned) {
			offX = -1f;
		}
		Vector3 objPos = new Vector3 (transform.position.x + offX, transform.position.y - 0.5F, transform.position.z);
		GameObject instantiatedProjectile = (GameObject)Instantiate (Encre, objPos, Encre.transform.rotation);
		instantiatedProjectile.GetComponent<Objet_Lance> ().SetConfig (first, body.turned, 1f);
	}

	//BallOmbre
	private void Action_Pouvoir_Noctali(){
		float offX = 1f;
		if (body.turned) {
			offX = -1f;
		}
		Vector3 objPos = new Vector3 (transform.position.x + offX, transform.position.y - 0.5F, transform.position.z);
		GameObject instantiatedProjectile = (GameObject)Instantiate (BallOmbre, objPos, BallOmbre.transform.rotation);
		instantiatedProjectile.GetComponent<Objet_Lance> ().SetConfig (first, body.turned, 1f);
	}
}

