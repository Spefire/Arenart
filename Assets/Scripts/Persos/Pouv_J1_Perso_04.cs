using UnityEngine;
using System.Collections;
public class Pouv_J1_Perso_04: MonoBehaviour {

	//SCRIPT PERSO KRIZALIED

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
	public static bool cosplay;
	private bool versGauche;
	public static int Degats;
	public Texture texture_attaque;
	public Texture texture_attaque_Noctali;
	public Texture texture_pouvoir;
	public Texture texture_pouvoir_Noctali;
	public Texture texture_spe;
	public Texture texture_base;
	public Texture texture_base_Noctali;
	private double tmp_ViveAttaque = 0.5;
	private double time_ViveAttaque;
	private bool viveAttaque;
	public GameObject Encre;
	public GameObject BallOmbre;
	private GameObject ennemi;
	private Renderer render;
	private AudioSource aud;
	public AudioClip aud04_smosh;
	public AudioClip aud04_slash;
	public AudioClip aud04_change;
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
		viveAttaque = false; 
		cosplay = false;
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
				if (!cosplay) {
					render.material.mainTexture = texture_attaque;
					Action_Attaque ();
				} else {
					render.material.mainTexture = texture_attaque_Noctali;
					Action_Attaque_Noctali ();
				}
			}
		} else if (Input.GetButton ("J1_Pouvoir")) {
			if (recharge_Mag) {
				print ("J1 utilise Pouvoir normal");
				recharge_Mag = false;
				time_recharge_Mag = (double) Time.time;
				time_recharge_Text = (double) Time.time;
				if (!cosplay) {
					render.material.mainTexture = texture_pouvoir;
					Action_Pouvoir ();
				} else {
					render.material.mainTexture = texture_pouvoir_Noctali;
					Action_Pouvoir_Noctali ();
				}
			}
		} else if (Input.GetButton ("J1_Pouvoir_Spe")) {
			if (recharge_Spe) {
				print ("J1 utilise Pouvoir spécial");
				recharge_Spe = false;
				time_recharge_Spe = (double) Time.time;
				time_recharge_Text = (double) Time.time;
				render.material.mainTexture = texture_spe;
				aud.PlayOneShot (aud04_change);
				cosplay = !cosplay;
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
				if (!cosplay) {
					render.material.mainTexture = texture_base;
				} else {
					render.material.mainTexture = texture_base_Noctali;
				}
			}
		}
		if (!recharge_Mag) {
			double time_cours_Mag = (double)Time.time - time_recharge_Mag;
			if(time_cours_Mag >= tmp_recharge_Mag){
				recharge_Mag = true;
			}
			if(time_cours_Text >= tmp_text_Mag){
				if (!cosplay) {
					render.material.mainTexture = texture_base;
				} else {
					render.material.mainTexture = texture_base_Noctali;
				}
			}
		}
		if (!recharge_Spe) {
			double time_cours_Spe = (double)Time.time - time_recharge_Spe;
			if(time_cours_Spe >= tmp_recharge_Spe){
				recharge_Spe = true;
			}
			if(time_cours_Text >= tmp_text_Spe){
				if (!cosplay) {
					render.material.mainTexture = texture_base;
				} else {
					render.material.mainTexture = texture_base_Noctali;
				}
			}
		}
		if (viveAttaque) {
			double time_cours_ViveAttaque = (double)Time.time - time_ViveAttaque;
			if(time_cours_ViveAttaque >= tmp_ViveAttaque){
				viveAttaque = false;
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
			aud.PlayOneShot (aud04_slash);
		} else {
			aud.PlayOneShot (aud04_smosh);
		}
	}

	//ViveAttaque
	void Action_Attaque_Noctali(){
		if (versGauche) {
			transform.Translate (Vector2.left * 1.5f);
			GetComponent<Rigidbody> ().AddForce (Vector3.up * 30, ForceMode.Impulse);
			GetComponent<Rigidbody> ().AddForce (Vector3.left * 30, ForceMode.Impulse);
			viveAttaque = true;
			time_ViveAttaque = (double) Time.time;
		} else {
			transform.Translate (Vector2.right * 1.5f);
			GetComponent<Rigidbody> ().AddForce (Vector3.up * 30, ForceMode.Impulse);
			GetComponent<Rigidbody> ().AddForce (-Vector3.left * 30, ForceMode.Impulse);
			viveAttaque = true;
			time_ViveAttaque = (double) Time.time;
		}
		float distance = (transform.position - ennemi.transform.position).magnitude;
		if (distance < 2) {
			double calculDegats = Degats * (1.25 - Joueur_Stats_J2.Resistance / 100);
			Joueur_Stats_J2.getDamage (calculDegats, 0);
			aud.PlayOneShot (aud00_punch);
		} else {
			aud.PlayOneShot (aud00_coup);
		}
	}

	//Encre
	void Action_Pouvoir(){
		if (versGauche) {
			Vector3 posFire = new Vector3 (transform.position.x - 1F, transform.position.y - 0.5F, transform.position.z);
			GameObject instantiatedProjectile = (GameObject)Instantiate (Encre, posFire, Encre.transform.rotation);
			instantiatedProjectile.GetComponent<Rigidbody> ().velocity = new Vector3 (-4 * Mathf.Sign (transform.forward.x), 0, 0);
			Physics.IgnoreCollision(instantiatedProjectile.GetComponent<Collider>(), transform.root.GetComponent<Collider>());
		} else {
			Vector3 posFire = new Vector3 (transform.position.x + 1F, transform.position.y - 0.5F, transform.position.z);
			GameObject instantiatedProjectile = (GameObject)Instantiate (Encre, posFire, Encre.transform.rotation);
			instantiatedProjectile.GetComponent<Rigidbody> ().velocity = new Vector3 (4 * Mathf.Sign (transform.forward.x), 0, 0);
			Physics.IgnoreCollision(instantiatedProjectile.GetComponent<Collider>(), transform.root.GetComponent<Collider>());
		}
	}

	//BallOmbre
	void Action_Pouvoir_Noctali(){
		if (versGauche) {
			Vector3 posFire = new Vector3 (transform.position.x - 1F, transform.position.y, transform.position.z);
			GameObject instantiatedProjectile = (GameObject)Instantiate (BallOmbre, posFire, BallOmbre.transform.rotation);
			instantiatedProjectile.GetComponent<Rigidbody> ().velocity = new Vector3 (-12 * Mathf.Sign (transform.forward.x), 0, 0);
			Physics.IgnoreCollision(instantiatedProjectile.GetComponent<Collider>(), transform.root.GetComponent<Collider>());
		} else {
			Vector3 posFire = new Vector3 (transform.position.x + 1F, transform.position.y, transform.position.z);
			GameObject instantiatedProjectile = (GameObject)Instantiate (BallOmbre, posFire, BallOmbre.transform.rotation);
			instantiatedProjectile.GetComponent<Rigidbody> ().velocity = new Vector3 (12 * Mathf.Sign (transform.forward.x), 0, 0);
			Physics.IgnoreCollision(instantiatedProjectile.GetComponent<Collider>(), transform.root.GetComponent<Collider>());
		}
	}

	void OnCollisionEnter (Collision objetInfo){
		if (objetInfo.gameObject.tag == "J2" && viveAttaque) {
			double calculDegats = Degats * (1.25 - Joueur_Stats_J2.Resistance / 100);
			Joueur_Stats_J2.getDamage (calculDegats, 0);
		}
	}

	void OnDestroy() {
		Degats = 0;
	}
}

