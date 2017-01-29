using UnityEngine;
using System.Collections;
public class Joueur_Deplacement_J1: MonoBehaviour {

	//Variables
	public int speed_mov = 10;
	public int speed_jump = 80;
	private int nb_saut = 2;
	public static bool jump;
	public static bool stunned;
	public static bool versGauche;
	public GameObject ennemi;
	public GameObject Fracas;
	public GameObject Spirit;
	public Texture texture01_base;
	public Texture texture01_base_embrased;
	public Texture texture01_saut;
	public Texture texture02_base_eau;
	public Texture texture02_base_glace;
	public Texture texture02_saut_eau;
	public Texture texture02_saut_glace;
	public Texture texture03_base;
	public Texture texture03_saut;
	public Texture texture04_base_kriza;
	public Texture texture04_base_noctali;
	public Texture texture04_saut_kriza;
	public Texture texture04_saut_noctali;
	public Texture texture05_base;
	public Texture texture05_saut;
	private Renderer render;
	//private AudioSource aud;

	//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
	//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

	void Start () {
		//aud = GetComponent<AudioSource>();
		versGauche = false;
		stunned = false;
		jump = false;
		render = GetComponent<Renderer>();
		ennemi = GameObject.FindGameObjectWithTag ("J2");
	}

	void Update () {
		Mouvement();
	}

	//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
	//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

	void Mouvement (){
		if (!stunned) {
			if (Input.GetAxis("J1_Deplacement") == 1) {
				if (versGauche) {
					versGauche = false;
					transform.localScale += new Vector3 (3f, 0f, 0f);
				}
				transform.Translate (Vector2.right * speed_mov * Time.deltaTime);
			}
			if (Input.GetAxis("J1_Deplacement") == -1) {
				if (!versGauche) {
					versGauche = true;
					transform.localScale -= new Vector3 (3f, 0f, 0f);
				}
				transform.Translate (-Vector2.right * speed_mov * Time.deltaTime);
			}
			if (Input.GetButtonDown ("J1_Saut") && nb_saut == 1) {
				if (!Camera_Jeu.paused) {
					GetComponent<Rigidbody> ().velocity = Vector3.zero;
					GetComponent<Rigidbody> ().angularVelocity = Vector3.zero; 
					GetComponent<Rigidbody> ().AddForce (Vector3.up * speed_jump, ForceMode.Impulse);
					nb_saut--;
					Sauter();
				}
			} else if (Input.GetButtonDown ("J1_Saut") && nb_saut == 2) {
				if (!Camera_Jeu.paused) {
					GetComponent<Rigidbody> ().AddForce (Vector3.up * speed_jump * 1.2f, ForceMode.Impulse);
					nb_saut--;
					Sauter();
				}
			}
		}
	}

	//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
	//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

	public static void setStun(bool s){
		stunned = s;
	}

	public void Sauter(){
		jump = true;
		if (Pouv_J1_Perso_01.Degats > 0) {
			render.material.mainTexture = texture01_saut;
		}else if (Pouv_J1_Perso_02.Degats > 0) {
			if (!Pouv_J1_Perso_02.modeIced) {
				render.material.mainTexture = texture02_saut_eau;
			} else {
				render.material.mainTexture = texture02_saut_glace;
			}
		}else if (Pouv_J1_Perso_03.Degats > 0) {
			render.material.mainTexture = texture03_saut;
		}else if (Pouv_J1_Perso_04.Degats > 0) {
			if (Pouv_J1_Perso_04.cosplay) {
				render.material.mainTexture = texture04_saut_noctali;
			} else {
				render.material.mainTexture = texture04_saut_kriza;
			}
		}else if (Pouv_J1_Perso_05.Degats > 0) {
			render.material.mainTexture = texture05_saut;
		}
	}

	public void Atterrir(){
		if (jump) {
			if (Pouv_J1_Perso_01.Degats > 0) {
				if (!Pouv_J1_Perso_01.embrased) {
					render.material.mainTexture = texture01_base;
				} else {
					render.material.mainTexture = texture01_base_embrased;
				}
			} else if (Pouv_J1_Perso_02.Degats > 0) {
				if (!Pouv_J1_Perso_02.modeIced) {
					render.material.mainTexture = texture02_base_eau;
				} else {
					render.material.mainTexture = texture02_base_glace;
				}
			} else if (Pouv_J1_Perso_03.Degats > 0) {
				render.material.mainTexture = texture03_base;
			} else if (Pouv_J1_Perso_04.Degats > 0) {
				if (Pouv_J1_Perso_04.cosplay) {
					render.material.mainTexture = texture04_base_noctali;
				} else {
					render.material.mainTexture = texture04_base_kriza;
				}
			} else if (Pouv_J1_Perso_05.Degats > 0) {
				render.material.mainTexture = texture05_base;
			}
			jump = false;
		}
	}

	void OnCollisionEnter (Collision objetInfo){
		if (objetInfo.gameObject.tag == "Ground") {
			nb_saut = 2;
			Atterrir ();
			if (Pouv_J1_Perso_05.fracased) {
				Vector3 posFire = new Vector3 (transform.position.x, transform.position.y+0.1f, transform.position.z);
				GameObject instantiatedProjectile = (GameObject)Instantiate (Fracas, posFire, Fracas.transform.rotation);
				Physics.IgnoreCollision (instantiatedProjectile.GetComponent<Collider> (), transform.root.GetComponent<Collider> ());
				Pouv_J1_Perso_05.fracased = false;
			}
		}
		if (objetInfo.gameObject.tag == "Arena") {
			Joueur_Stats_J1.getDamage (100, 0);
		}
		if (objetInfo.gameObject.tag == "Encre") {
			Joueur_Stats_J1.getDamage (0, 15);
			DestroyObject (objetInfo.gameObject);
		}
	}

	void OnTriggerEnter(Collider objetInfo) {
		if (objetInfo.gameObject.tag == "J2") {
			if (Pouv_J2_Perso_05.fracased) {
				double calculDegats = Pouv_J2_Perso_05.Degats * (1.25 - Joueur_Stats_J1.Resistance / 100);
				Joueur_Stats_J1.getDamage (calculDegats, 10);
				if (transform.position.x < objetInfo.gameObject.transform.position.x) {
					GetComponent<Rigidbody> ().AddForce (-Vector3.right * 25, ForceMode.Impulse);
				} else if (transform.position.x > objetInfo.gameObject.transform.position.x) {
					GetComponent<Rigidbody> ().AddForce (Vector3.right * 25, ForceMode.Impulse);
				}
			}
		}
		if (objetInfo.gameObject.tag == "BallOmbre") {
			double calculDegats = Pouv_J2_Perso_04.Degats * (1.5 - Joueur_Stats_J1.Resistance / 100);
			Joueur_Stats_J1.getDamage (calculDegats, 0);
			if (transform.position.x < objetInfo.gameObject.transform.position.x) {
				GetComponent<Rigidbody>().AddForce (-Vector3.right * 25, ForceMode.Impulse);
			} else if (transform.position.x > objetInfo.gameObject.transform.position.x) {
				GetComponent<Rigidbody>().AddForce (Vector3.right * 25, ForceMode.Impulse);
			}
			DestroyObject (objetInfo.gameObject);
		}
		else if (objetInfo.gameObject.tag == "Esprit") {
			Vector3 posFire = new Vector3 (transform.position.x, transform.position.y - 0.5f, transform.position.z - 1);
			GameObject instantiatedProjectile = (GameObject)Instantiate (Spirit, posFire, Spirit.transform.rotation);
			Physics.IgnoreCollision (instantiatedProjectile.GetComponent<Collider> (), transform.root.GetComponent<Collider> ());
			Pouv_J2_Perso_03.spirited = true;
			Pouv_J2_Perso_03.time_spirit = (double)Time.time;
			stunned = true;
			double calculDegats = Pouv_J2_Perso_03.Degats * (1.5 - Joueur_Stats_J1.Resistance / 100);
			Joueur_Stats_J1.getDamage (calculDegats, 20);
			DestroyObject (objetInfo.gameObject);
		}
		else if (objetInfo.gameObject.tag == "BouleFeu") {
			double calculDegats = Pouv_J2_Perso_01.Degats * (1.5 - Joueur_Stats_J1.Resistance / 100);
			Joueur_Stats_J1.getDamage (calculDegats, 10);
			DestroyObject (objetInfo.gameObject);
		}
		else if (objetInfo.gameObject.tag == "Meteore") {
			double calculDegats = Pouv_J2_Perso_01.Degats * (2 - Joueur_Stats_J1.Resistance / 100);
			Joueur_Stats_J1.getDamage (calculDegats, 20);
			GetComponent<Rigidbody>().AddForce (Vector3.up * 40, ForceMode.Impulse);
			DestroyObject (objetInfo.gameObject);
		}
		else if (objetInfo.gameObject.tag == "Flammes") {
			double calculDegats = Pouv_J2_Perso_01.Degats * (2 - Joueur_Stats_J1.Resistance / 100);
			Joueur_Stats_J1.getDamage (calculDegats, 10);
		}
		else if (objetInfo.gameObject.tag == "PiqueGlace") {
			double calculDegats = Pouv_J2_Perso_02.Degats * (1.5 - Joueur_Stats_J1.Resistance / 100);
			Joueur_Stats_J1.getDamage (calculDegats, 10);
			DestroyObject (objetInfo.gameObject);
		}
		else if (objetInfo.gameObject.tag == "LancesGlace") {
			double calculDegats = Pouv_J2_Perso_02.Degats * (1.25 - Joueur_Stats_J1.Resistance / 100);
			print (Joueur_Stats_J1.Resistance / 100);
			Joueur_Stats_J1.getDamage (calculDegats, 5);
			DestroyObject (objetInfo.gameObject);
		}
		else if (objetInfo.gameObject.tag == "FouetEau") {
			if (Joueur_Deplacement_J2.versGauche && transform.position.x < ennemi.transform.position.x) {
				double calculDegats = Pouv_J2_Perso_02.Degats * (1.5 - Joueur_Stats_J1.Resistance / 100);
				Joueur_Stats_J1.getDamage (calculDegats, 10);
				GetComponent<Rigidbody>().AddForce (Vector3.up * 50, ForceMode.Impulse);
				GetComponent<Rigidbody>().AddForce (-Vector3.right * 50, ForceMode.Impulse);
			} else if (!Joueur_Deplacement_J2.versGauche && transform.position.x > ennemi.transform.position.x) {
				double calculDegats = Pouv_J2_Perso_02.Degats * (1.5 - Joueur_Stats_J1.Resistance / 100);
				Joueur_Stats_J1.getDamage (calculDegats, 10);
				GetComponent<Rigidbody>().AddForce (Vector3.up * 50, ForceMode.Impulse);
				GetComponent<Rigidbody>().AddForce (Vector3.right * 50, ForceMode.Impulse);
			}
		}
		else if (objetInfo.gameObject.tag == "Fracas") {
			if (Joueur_Deplacement_J2.versGauche && transform.position.x < ennemi.transform.position.x) {
				double calculDegats = Pouv_J2_Perso_05.Degats * (1.5 - Joueur_Stats_J1.Resistance / 100);
				Joueur_Stats_J1.getDamage (calculDegats, 15);
				GetComponent<Rigidbody> ().AddForce (Vector3.up * 50, ForceMode.Impulse);
				GetComponent<Rigidbody> ().AddForce (-Vector3.right * 50, ForceMode.Impulse);
			} else if (!Joueur_Deplacement_J2.versGauche && transform.position.x > ennemi.transform.position.x) {
				double calculDegats = Pouv_J2_Perso_05.Degats * (1.5 - Joueur_Stats_J1.Resistance / 100);
				Joueur_Stats_J1.getDamage (calculDegats, 15);
				GetComponent<Rigidbody> ().AddForce (Vector3.up * 50, ForceMode.Impulse);
				GetComponent<Rigidbody> ().AddForce (Vector3.right * 50, ForceMode.Impulse);
			} else {
				double calculDegats = Pouv_J2_Perso_05.Degats * (1.5 - Joueur_Stats_J1.Resistance / 100);
				Joueur_Stats_J1.getDamage (calculDegats, 10);
			}
		}
		else if (objetInfo.gameObject.tag == "LanceMal") {
			double calculDegats = Pouv_J2_Perso_05.Degats * (1.5 - Joueur_Stats_J1.Resistance / 100);
			Joueur_Stats_J1.getDamage (calculDegats, 0);
			Joueur_Stats_J2.getDamage (-5, 0);
			DestroyObject (objetInfo.gameObject);
		}
	}
}

