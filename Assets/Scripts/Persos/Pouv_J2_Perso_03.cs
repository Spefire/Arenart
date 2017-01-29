using UnityEngine;
using System.Collections;
public class Pouv_J2_Perso_03: MonoBehaviour {

	//SCRIPT PERSO ILANA

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
	private bool versGauche;
	public Texture texture_attaque;
	public Texture texture_pouvoir;
	public Texture texture_spe;
	public Texture texture_base;
	public Texture textureInv_Esprit;
	public static double tmp_spirit = 0.5;
	public static double time_spirit;
	public static bool spirited;
	public GameObject Bouclier;
	public GameObject Racines;
	public GameObject Esprit;
	private GameObject ennemi;
	private Renderer render;
	private AudioSource aud;
	public AudioClip aud00_coup;
	public AudioClip aud03_racines;

	//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
	//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

	void Start () {
		aud = GetComponent<AudioSource>();
		Degats = 10;
		recharge_Phy = true;
		recharge_Mag = true;
		recharge_Spe = true;
		spirited = false; 
		render = GetComponent<Renderer>();
		ennemi = GameObject.FindGameObjectWithTag ("J1");
		Physics.IgnoreCollision (ennemi.GetComponent<Collider> (), transform.root.GetComponent<Collider> ());
	}

	void Update () {
		Pouvoirs();
		Rechargement();
		versGauche = Joueur_Deplacement_J2.versGauche;
	}

	//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
	//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

	void Pouvoirs (){
		if (Input.GetButton ("J2_Attaque")) {
			if (recharge_Phy) {
				print ("J2 utilise Attaque normale");
				recharge_Phy = false;
				time_recharge_Phy = (double) Time.time;
				time_recharge_Text = (double) Time.time;
				render.material.mainTexture = texture_attaque;
				Action_Attaque ();
			}
		} else if (Input.GetButton ("J2_Pouvoir")) {
			if (recharge_Mag) {
				print ("J2 utilise Pouvoir normal");
				recharge_Mag = false;
				time_recharge_Mag = (double) Time.time;
				time_recharge_Text = (double) Time.time;
				render.material.mainTexture = texture_pouvoir;
				Action_Pouvoir ();
			}
		} else if (Input.GetButton ("J2_Pouvoir_Spe")) {
			if (recharge_Spe) {
				print ("J2 utilise Pouvoir spécial");
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
				render.material.mainTexture = texture_base;
			}
		}
		if (!recharge_Mag) {
			double time_cours_Mag = (double)Time.time - time_recharge_Mag;
			if(time_cours_Mag >= tmp_recharge_Mag){
				recharge_Mag = true;
			}
			if(time_cours_Text >= tmp_text_Mag){
				render.material.mainTexture = texture_base;
			}
		}
		if (!recharge_Spe) {
			double time_cours_Spe = (double)Time.time - time_recharge_Spe;
			if(time_cours_Spe >= tmp_recharge_Spe){
				recharge_Spe = true;
			}
			if(time_cours_Text >= tmp_text_Spe){
				render.material.mainTexture = texture_base;
			}
		}
		if (spirited) {
			double time_cours_ViveAttaque = (double)Time.time - time_spirit;
			if(time_cours_ViveAttaque >= tmp_spirit){
				spirited = false;
				Joueur_Deplacement_J1.setStun (false);
			}
		}
	}

	//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
	//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

	//Racines
	void Action_Attaque(){
		float distance = (transform.position - ennemi.transform.position).magnitude;
		if (distance < 2) {
			Vector3 posFire = new Vector3 (ennemi.transform.position.x, ennemi.transform.position.y - 0.2f, ennemi.transform.position.z - 1);
			GameObject instantiatedProjectile = (GameObject)Instantiate (Racines, posFire, Racines.transform.rotation);
			Physics.IgnoreCollision (instantiatedProjectile.GetComponent<Collider> (), transform.root.GetComponent<Collider> ());
			double calculDegats = Degats * (1.5 - Joueur_Stats_J1.Resistance / 100);
			Joueur_Stats_J1.getDamage (calculDegats, 10);
			aud.PlayOneShot (aud03_racines);
		} else {
			aud.PlayOneShot (aud00_coup);
		}
	}

	//Bouclier
	void Action_Pouvoir(){
		Vector3 posFire = new Vector3 (transform.position.x, transform.position.y, transform.position.z-1);
		GameObject instantiatedProjectile = (GameObject)Instantiate (Bouclier, posFire, Bouclier.transform.rotation);
		Physics.IgnoreCollision(instantiatedProjectile.GetComponent<Collider>(), transform.root.GetComponent<Collider>());
		Joueur_Stats_J2.getDamage (-2, 0);
	}

	//Esprit
	void Action_Spe(){
		if (versGauche) {
			Vector3 posFire = new Vector3 (transform.position.x - 1F, transform.position.y, transform.position.z);
			GameObject instantiatedProjectile = (GameObject)Instantiate (Esprit, posFire, Esprit.transform.rotation);
			instantiatedProjectile.GetComponent<Rigidbody> ().velocity = new Vector3 (-12 * Mathf.Sign (transform.forward.x), 0, 0);
			Physics.IgnoreCollision(instantiatedProjectile.GetComponent<Collider>(), transform.root.GetComponent<Collider>());
		} else {
			Vector3 posFire = new Vector3 (transform.position.x + 1F, transform.position.y, transform.position.z);
			GameObject instantiatedProjectile = (GameObject)Instantiate (Esprit, posFire, Esprit.transform.rotation);
			instantiatedProjectile.GetComponent<Rigidbody> ().velocity = new Vector3 (12 * Mathf.Sign (transform.forward.x), 0, 0);
			instantiatedProjectile.GetComponent<Renderer> ().material.mainTexture = textureInv_Esprit;
			Physics.IgnoreCollision(instantiatedProjectile.GetComponent<Collider>(), transform.root.GetComponent<Collider>());
		}
	}

	void OnDestroy() {
		Degats = 0;
	}
}

