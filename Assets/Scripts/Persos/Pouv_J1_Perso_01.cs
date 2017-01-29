using UnityEngine;
using System.Collections;
public class Pouv_J1_Perso_01: MonoBehaviour {

	//SCRIPT PERSO SPEFIRE

	private double tmp_recharge_Phy = 0.5;
	private double tmp_recharge_Mag = 2;
	private double tmp_recharge_Spe = 5;
	private double time_recharge_Phy;
	private double time_recharge_Mag;
	private double time_recharge_Spe;
	private double time_recharge_Text;
	private double tmp_text_Phy = 0.5;
	private double tmp_text_Mag = 1;
	private double tmp_text_Spe = 1;
	private bool recharge_Phy;
	private bool recharge_Mag;
	private bool recharge_Spe;
	public static int Degats;
	public Texture texture_attaque;
	public Texture texture_pouvoir;
	public Texture texture_spe;
	public Texture texture_base;
	public Texture texture_base_embrased;
	public static bool embrased;
	private bool versGauche;
	public GameObject BouleFeu;
	public GameObject Flammes;
	public GameObject Meteore;
	private GameObject ennemi;
	private Renderer render;
	private AudioSource aud;
	public AudioClip aud00_coup;
	public AudioClip aud00_punch;

	//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
	//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

	void Start () {
		aud = GetComponent<AudioSource>();
		Degats = 10;
		recharge_Phy = true;
		recharge_Mag = true;
		recharge_Spe = true;
		embrased = false;
		render = GetComponent<Renderer>();
		ennemi = GameObject.FindGameObjectWithTag ("J2");
		Physics.IgnoreCollision (ennemi.GetComponent<Collider> (), transform.root.GetComponent<Collider> ());
	}

	void Update () {
		Pouvoirs();
		Rechargement();
		versGauche = Joueur_Deplacement_J1.versGauche;
		if (Joueur_Stats_J1.Vie < 50) {
			embrased = true;
			if (render.material.mainTexture == texture_base) {
				render.material.mainTexture = texture_base_embrased;
			}
		} else {
			embrased = false;
			if (render.material.mainTexture == texture_base_embrased) {
				render.material.mainTexture = texture_base;
			}
		}
	}

	//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
	//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

	void Pouvoirs (){
		if (Input.GetButton ("J1_Attaque")) {
			if (recharge_Phy) {
				print ("J1 utilise Attaque normale");
				recharge_Phy = false;
				time_recharge_Phy = (double) Time.time;
				time_recharge_Text = (double) Time.time;
				render.material.mainTexture = texture_attaque;
				Action_Attaque ();
			}
		} else if (Input.GetButton ("J1_Pouvoir")) {
			if (recharge_Mag) {
				print ("J1 utilise Pouvoir normal");
				recharge_Mag = false;
				time_recharge_Mag = (double) Time.time;
				time_recharge_Text = (double) Time.time;
				render.material.mainTexture = texture_pouvoir;
				Action_Pouvoir ();
			}
		} else if (Input.GetButton ("J1_Pouvoir_Spe")) {
			if (recharge_Spe) {
				print ("J1 utilise Pouvoir spécial");
				recharge_Spe = false;
				time_recharge_Spe = (double) Time.time;
				time_recharge_Text = (double) Time.time;
				render.material.mainTexture = texture_spe;
				Action_Spe ();
			}
		}
	}

	//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
	//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

	void Rechargement (){
		double time_cours_Text = (double)Time.time - time_recharge_Text;
		if (!recharge_Phy) {
			double time_cours_Phy = (double)Time.time - time_recharge_Phy;
			if(time_cours_Phy >= tmp_recharge_Phy){
				recharge_Phy = true;
			}
			if(time_cours_Text >= tmp_text_Phy){
				if (!embrased) {
					render.material.mainTexture = texture_base;
				} else {
					render.material.mainTexture = texture_base_embrased;
				}
			}
		}
		if (!recharge_Mag) {
			double time_cours_Mag = (double)Time.time - time_recharge_Mag;
			if(time_cours_Mag >= tmp_recharge_Mag){
				recharge_Mag = true;
			}
			if(time_cours_Text >= tmp_text_Mag){
				if (!embrased) {
					render.material.mainTexture = texture_base;
				} else {
					render.material.mainTexture = texture_base_embrased;
				}
			}
		}
		if (!recharge_Spe) {
			double time_cours_Spe = (double)Time.time - time_recharge_Spe;
			if(time_cours_Spe >= tmp_recharge_Spe){
				recharge_Spe = true;
			}
			if(time_cours_Text >= tmp_text_Spe){
				if (!embrased) {
					render.material.mainTexture = texture_base;
				} else {
					render.material.mainTexture = texture_base_embrased;
				}
			}
		}
	}

	//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
	//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

	//Coup de queue
	void Action_Attaque(){
		float distance = (transform.position - ennemi.transform.position).magnitude;
		if (distance < 2) {
			double calculDegats = Degats * (1.5 - Joueur_Stats_J2.Resistance / 100);
			Joueur_Stats_J2.getDamage (calculDegats, 5);
			ennemi.gameObject.GetComponent<Rigidbody> ().AddForce (Vector3.up * 25, ForceMode.Impulse);
			aud.PlayOneShot (aud00_punch);
		} else {
			aud.PlayOneShot (aud00_coup);
		}
	}

	//Boule de feu
	void Action_Pouvoir(){
		if (versGauche) {
			Vector3 posFire = new Vector3 (transform.position.x - 1F, transform.position.y, transform.position.z);
			GameObject instantiatedProjectile = (GameObject)Instantiate (BouleFeu, posFire, BouleFeu.transform.rotation);
			instantiatedProjectile.GetComponent<Rigidbody> ().velocity = new Vector3 (-13 * Mathf.Sign (transform.forward.x), 0, 0);
			Physics.IgnoreCollision(instantiatedProjectile.GetComponent<Collider>(), transform.root.GetComponent<Collider>());
		} else {
			Vector3 posFire = new Vector3 (transform.position.x + 1F, transform.position.y, transform.position.z);
			GameObject instantiatedProjectile = (GameObject)Instantiate (BouleFeu, posFire, BouleFeu.transform.rotation);
			instantiatedProjectile.GetComponent<Rigidbody> ().velocity = new Vector3 (13 * Mathf.Sign (transform.forward.x), 0, 0);
			Physics.IgnoreCollision(instantiatedProjectile.GetComponent<Collider>(), transform.root.GetComponent<Collider>());
		}
	}

	//Embrasement ou Echec Critique
	void Action_Spe(){
		if (!embrased) {
			Vector3 posFire = new Vector3 (transform.position.x, transform.position.y, transform.position.z);
			GameObject instantiatedProjectile = (GameObject)Instantiate (Flammes, posFire, Flammes.transform.rotation);
			Physics.IgnoreCollision(instantiatedProjectile.GetComponent<Collider>(), transform.root.GetComponent<Collider>());
		} else {
			Vector3 posFire = new Vector3 (ennemi.transform.position.x, ennemi.transform.position.y+15, ennemi.transform.position.z);
			GameObject instantiatedProjectile = (GameObject)Instantiate (Meteore, posFire, Meteore.transform.rotation);
			instantiatedProjectile.GetComponent<Rigidbody> ().velocity = new Vector3 (0, -15 * Mathf.Sign (transform.forward.y), 0);
			Physics.IgnoreCollision(instantiatedProjectile.GetComponent<Collider>(), transform.root.GetComponent<Collider>());
		}
	}

	void OnDestroy() {
		Degats = 0;
	}
}

