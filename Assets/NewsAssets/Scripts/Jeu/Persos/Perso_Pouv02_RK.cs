using UnityEngine;
using System.Collections;

public class Perso_Pouv02_RK: MonoBehaviour {

	//SCRIPT PERSO APKARERU

	public bool first;
	public Sprite texture_attaque_eau;
	public Sprite texture_attaque_glace;
	public Sprite texture_pouvoir_eau;
	public Sprite texture_pouvoir_glace;
	public Sprite texture_spe_eau;
	public Sprite texture_spe_glace;
	public GameObject FouetEau;
	public GameObject PrisonEau;
	public GameObject PiqueGlace;
	public GameObject LancesSort;
	public GameObject LancesGlace;
	public AudioClip aud02_transform;
	public AudioClip aud02_detransform;

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
				body.Attaque ();
				if (!body.transformed) {
					aud.PlayOneShot (aud02_transform);
					render.sprite = texture_attaque_eau;
				} else {
					aud.PlayOneShot (aud02_detransform);
					render.sprite = texture_attaque_glace;
				}
				body.transformed = !body.transformed;
			}
		} else if ((first && Game_Inputs.J1_Pouvoir) || (!first && Game_Inputs.J2_Pouvoir)) {
			if (body.recharge_Mag) {
				body.Pouvoir ();
				if (!body.transformed) {
					render.sprite = texture_pouvoir_eau;
					Action_Pouvoir_Eau ();
				} else {
					render.sprite = texture_pouvoir_glace;
					Action_Pouvoir_Glace ();
				}
			}
		} else if ((first && Game_Inputs.J1_PouvoirSpe) || (!first && Game_Inputs.J2_PouvoirSpe)) {
			if (body.recharge_Spe) {
				body.PouvoirSpe ();
				if (!body.transformed) {
					render.sprite = texture_spe_eau;
					Action_Spe_Eau ();
				} else {
					render.sprite = texture_spe_glace;
					Action_Spe_Glace ();
				}
			}
		}
	}

	//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

	//FouetEau
	private void Action_Pouvoir_Eau(){
		Vector3 objPos = new Vector3 (transform.position.x, transform.position.y, transform.position.z);
		GameObject instantiatedProjectile = (GameObject)Instantiate (FouetEau, objPos, FouetEau.transform.rotation);
		instantiatedProjectile.GetComponent<Objet_Suivi> ().SetConfig (first, body.turned, true);
	}

	//PrisonEau
	private void Action_Spe_Eau(){
		Vector3 objPos = new Vector3 (stats.enemyPos.x, stats.enemyPos.y, stats.enemyPos.z);
		GameObject instantiatedProjectile = (GameObject)Instantiate (PrisonEau, objPos, PrisonEau.transform.rotation);
		instantiatedProjectile.GetComponent<Objet_Suivi> ().SetConfig (first, body.turned, false);
		Perso_Body_RK bodyEnemy = stats.enemy.GetComponent<Perso_Body_RK> ();
		bodyEnemy.SetStunned(2f);
		stats.enemy.GetComponent<Rigidbody>().AddForce (-Vector3.up * 50, ForceMode.Impulse);
	}
		
	//PiqueGlace
	private void Action_Pouvoir_Glace(){
		float offX = 1f;
		if (body.turned) {
			offX = -1f;
		}
		Vector3 objPos = new Vector3 (transform.position.x + offX, transform.position.y, transform.position.z);
		GameObject instantiatedProjectile = (GameObject)Instantiate (PiqueGlace, objPos, PiqueGlace.transform.rotation);
		instantiatedProjectile.GetComponent<Objet_Lance> ().SetConfig (first, body.turned, 1f);
	}

	//LancesGlace
	private void Action_Spe_Glace(){
		body.SetStunned(1f);
		float offX = 1f;
		if (body.turned) {
			offX = -1f;
		}
		Vector3 objPosS = new Vector3 (transform.position.x, transform.position.y, transform.position.z);
		GameObject instantiatedProjectileS = (GameObject)Instantiate (LancesSort, objPosS, LancesSort.transform.rotation);
		instantiatedProjectileS.GetComponent<Objet_Suivi> ().SetConfig (first, body.turned, false);

		Vector3 objPos1 = new Vector3 (transform.position.x + offX, transform.position.y, transform.position.z);
		GameObject instantiatedProjectile1 = (GameObject)Instantiate (LancesGlace, objPos1, LancesGlace.transform.rotation);
		instantiatedProjectile1.GetComponent<Objet_Lance> ().SetConfig (first, body.turned, 1f);

		Vector3 objPos2 = new Vector3 (transform.position.x + offX, transform.position.y + 0.4f, transform.position.z);
		GameObject instantiatedProjectile2 = (GameObject)Instantiate (LancesGlace, objPos2, LancesGlace.transform.rotation);
		instantiatedProjectile2.GetComponent<Objet_Lance> ().SetConfig (first, body.turned, 0.9f);

		Vector3 objPos3 = new Vector3 (transform.position.x + offX, transform.position.y - 0.4f, transform.position.z);
		GameObject instantiatedProjectile3 = (GameObject)Instantiate (LancesGlace, objPos3, LancesGlace.transform.rotation);
		instantiatedProjectile3.GetComponent<Objet_Lance> ().SetConfig (first, body.turned, 0.9f);

		Vector3 objPos4 = new Vector3 (transform.position.x + offX, transform.position.y + 0.8f, transform.position.z);
		GameObject instantiatedProjectile4 = (GameObject)Instantiate (LancesGlace, objPos4, LancesGlace.transform.rotation);
		instantiatedProjectile4.GetComponent<Objet_Lance> ().SetConfig (first, body.turned, 0.7f);

		Vector3 objPos5 = new Vector3 (transform.position.x + offX, transform.position.y - 0.8f, transform.position.z);
		GameObject instantiatedProjectile5 = (GameObject)Instantiate (LancesGlace, objPos5, LancesGlace.transform.rotation);
		instantiatedProjectile5.GetComponent<Objet_Lance> ().SetConfig (first, body.turned, 0.7f);
	}
}

