using UnityEngine;
using System.Collections;
public class Pouv_J1_Perso_05: MonoBehaviour {

	//SCRIPT PERSO DUNKY

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
	public static bool fracased;
	private double tmp_mal = 0.5;
	private double time_mal;
	private bool maled;
	public GameObject LancesMal;
	public Texture textureInv_LancesMal;
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
		fracased = false;
		render = GetComponent<Renderer>();
		ennemi = GameObject.FindGameObjectWithTag ("J2");
		Physics.IgnoreCollision (ennemi.GetComponent<Collider> (), transform.root.GetComponent<Collider> ());
	}

	void Update () {
		Pouvoirs();
		Rechargement();
		versGauche = Joueur_Deplacement_J1.versGauche;
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
		if (maled) {
			double time_cours_Sort = (double)Time.time - time_mal;
			if(time_cours_Sort >= tmp_mal){
				maled = false;
				Joueur_Deplacement_J1.setStun (false);
			}
		}
	}

	//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
	//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

	void Action_Attaque(){
		float distance = (transform.position - ennemi.transform.position).magnitude;
		if (distance < 2) {
			double calculDegats = Degats * (1.5 - Joueur_Stats_J2.Resistance / 100);
			Joueur_Stats_J2.getDamage (calculDegats, 5);
			ennemi.gameObject.GetComponent<Rigidbody>().AddForce (Vector3.up * 25, ForceMode.Impulse);
			aud.PlayOneShot (aud00_punch);
		} else {
			aud.PlayOneShot (aud00_coup);
		}
	}

	//TaMere
	void Action_Pouvoir(){
		fracased = true;
		if (versGauche) {
			transform.Translate(new Vector3(0,2f,0));
			GetComponent<Rigidbody>().AddForce (-Vector3.up * 150, ForceMode.Impulse);
		} else {
			transform.Translate(new Vector3(0,2f,0));
			GetComponent<Rigidbody>().AddForce (-Vector3.up * 150, ForceMode.Impulse);
		}
	}

	//Main Morte
	void Action_Spe(){
		maled = true;
		time_mal = (double)Time.time;
		Joueur_Deplacement_J1.setStun (true);
		if (versGauche) {
			Vector3 posFire = new Vector3 (transform.position.x, transform.position.y+1, transform.position.z);
			GameObject instantiatedProjectile = (GameObject)Instantiate (LancesMal, posFire, LancesMal.transform.rotation);
			instantiatedProjectile.GetComponent<Rigidbody> ().velocity = new Vector3 (-12 * Mathf.Sign (transform.forward.x), 0, 0);
			Physics.IgnoreCollision(instantiatedProjectile.GetComponent<Collider>(), transform.root.GetComponent<Collider>());
		} else {
			Vector3 posFire = new Vector3 (transform.position.x, transform.position.y+1, transform.position.z);
			GameObject instantiatedProjectile = (GameObject)Instantiate (LancesMal, posFire, LancesMal.transform.rotation);
			instantiatedProjectile.GetComponent<Rigidbody> ().velocity = new Vector3 (12 * Mathf.Sign (transform.forward.x), 0, 0);
			instantiatedProjectile.GetComponent<Renderer> ().material.mainTexture = textureInv_LancesMal;
			Physics.IgnoreCollision(instantiatedProjectile.GetComponent<Collider>(), transform.root.GetComponent<Collider>());
		}
	}

	void OnDestroy() {
		Degats = 0;
	}
}

