using UnityEngine;
using System.Collections;

public class Perso_Pouv03_RK: MonoBehaviour {

	//SCRIPT PERSO ILANA

	public bool first;
	public GameObject Bouclier;
	public GameObject Racines;
	public GameObject Esprit;
	public Sprite texture_attaque;
	public Sprite texture_pouvoir;
	public Sprite texture_spe;
	public AudioClip aud00_coup;
	public AudioClip aud03_racines;

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

	//Racines
	private void Action_Attaque(){
		float distance = (transform.position - stats.enemyPos).magnitude;
		if (distance < 2) {
			Perso_Stats_RK statsEnemy = stats.enemy.GetComponent<Perso_Stats_RK> ();
			statsEnemy.SetDamage (statsEnemy.GetDamage (1.25), 5);
			Vector3 objPos = new Vector3 (stats.enemyPos.x, stats.enemyPos.y - 0.5f, stats.enemyPos.z + 1);
			GameObject instantiatedProjectile = (GameObject)Instantiate (Racines, objPos, Racines.transform.rotation);
			Physics.IgnoreCollision (instantiatedProjectile.GetComponent<Collider> (), transform.root.GetComponent<Collider> ());
			aud.PlayOneShot (aud03_racines);
		} else {
			aud.PlayOneShot (aud00_coup);
		}
	}

	//Bouclier
	private void Action_Pouvoir(){
		Vector3 posFire = new Vector3 (transform.position.x, transform.position.y, transform.position.z);
		GameObject instantiatedProjectile = (GameObject)Instantiate (Bouclier, posFire, Bouclier.transform.rotation);
		instantiatedProjectile.GetComponent<Objet_Suivi> ().SetConfig (first, body.turned, false);
		stats.SetDamage (-2, 0);
	}

	//Esprit
	private void Action_Spe(){
		float offX = 1f;
		if (body.turned) {
			offX = -1f;
		}
		Vector3 objPos = new Vector3 (transform.position.x + offX, transform.position.y, transform.position.z);
		GameObject instantiatedProjectile = (GameObject)Instantiate (Esprit, objPos, Esprit.transform.rotation);
		instantiatedProjectile.GetComponent<Objet_Lance> ().SetConfig (first, body.turned, 1f);
	}
}

