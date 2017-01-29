using UnityEngine;
using System.Collections;
public class Pouv_J1_Perso_02: MonoBehaviour {

	//SCRIPT PERSO APKARERU

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
	public static bool modeIced;
	private bool versGauche;
	public static int Degats;
	private double tmp_prison = 0.5;
	private double time_prison;
	private bool prisoned;
	private double tmp_sort = 0.5;
	private double time_sort;
	private bool sorted;
	public Texture texture_base_eau;
	public Texture texture_base_glace;
	public Texture texture_attaque_eau;
	public Texture texture_attaque_glace;
	public Texture texture_pouvoir_eau;
	public Texture texture_pouvoir_glace;
	public Texture texture_spe_eau;
	public Texture texture_spe_glace;
	public GameObject FouetEau;
	public GameObject PrisonEau;
	public GameObject PiqueGlace;
	public GameObject LancesSort;
	public GameObject LancesGlace;
	public Texture textureInv_FouetEau;
	public Texture textureInv_LancesGlace;
	public Texture textureInv_LancesSort;
	public Texture textureInv_PiqueGlace;
	private GameObject ennemi;
	private Renderer render;
	public AudioClip aud02_transform;
	public AudioClip aud02_detransform;
	private AudioSource aud;

	//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
	//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

	void Start () {
		aud = GetComponent<AudioSource>();
		Degats = 10;
		recharge_Phy = true;
		recharge_Mag = true;
		recharge_Spe = true;
		modeIced = false;
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
				if (!modeIced) {
					aud.PlayOneShot (aud02_transform);
					render.material.mainTexture = texture_attaque_eau;
				} else {
					aud.PlayOneShot (aud02_detransform);
					render.material.mainTexture = texture_attaque_glace;
				}
				modeIced = !modeIced;
			}
		} else if (Input.GetButton ("J1_Pouvoir")) {
			if (recharge_Mag) {
				print ("J1 utilise Pouvoir normal");
				recharge_Mag = false;
				time_recharge_Mag = (double) Time.time;
				time_recharge_Text = (double) Time.time;
				if (!modeIced) {
					render.material.mainTexture = texture_pouvoir_eau;
					Action_Pouvoir_Eau ();
				} else {
					render.material.mainTexture = texture_pouvoir_glace;
					Action_Pouvoir_Glace ();
				}
			}
		} else if (Input.GetButton ("J1_Pouvoir_Spe")) {
			if (recharge_Spe) {
				print ("J1 utilise Pouvoir spécial");
				recharge_Spe = false;
				time_recharge_Spe = (double) Time.time;
				time_recharge_Text = (double) Time.time;
				if (!modeIced) {
					render.material.mainTexture = texture_spe_eau;
					Action_Spe_Eau ();
				} else {
					render.material.mainTexture = texture_spe_glace;
					Action_Spe_Glace ();
				}
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
				if (modeIced) {
					render.material.mainTexture = texture_base_glace;
				} else {
					render.material.mainTexture = texture_base_eau;
				}
			}
		}
		if (!recharge_Mag) {
			double time_cours_Mag = (double)Time.time - time_recharge_Mag;
			if(time_cours_Mag >= tmp_recharge_Mag){
				recharge_Mag = true;
			}
			if(time_cours_Text >= tmp_text_Mag){
				if (modeIced) {
					render.material.mainTexture = texture_base_glace;
				} else {
					render.material.mainTexture = texture_base_eau;
				}
			}
		}
		if (!recharge_Spe) {
			double time_cours_Spe = (double)Time.time - time_recharge_Spe;
			if(time_cours_Spe >= tmp_recharge_Spe){
				recharge_Spe = true;
			}
			if(time_cours_Text >= tmp_text_Spe){
				if (modeIced) {
					render.material.mainTexture = texture_base_glace;
				} else {
					render.material.mainTexture = texture_base_eau;
				}
			}
		}
		if (prisoned) {
			double time_cours_Prison = (double)Time.time - time_prison;
			if(time_cours_Prison >= tmp_prison){
				prisoned = false;
				Joueur_Deplacement_J2.setStun (false);
			}
		}
		if (sorted) {
			double time_cours_Sort = (double)Time.time - time_sort;
			if(time_cours_Sort >= tmp_sort){
				sorted = false;
				Joueur_Deplacement_J1.setStun (false);
			}
		}
	}

	//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
	//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

	//FouetEau
	void Action_Pouvoir_Eau(){
		if (versGauche) {
			Vector3 posFire = new Vector3 (transform.position.x, transform.position.y, transform.position.z);
			GameObject instantiatedProjectile = (GameObject)Instantiate (FouetEau, posFire, FouetEau.transform.rotation);
			Physics.IgnoreCollision(instantiatedProjectile.GetComponent<Collider>(), transform.root.GetComponent<Collider>());
		} else {
			Vector3 posFire = new Vector3 (transform.position.x, transform.position.y, transform.position.z);
			GameObject instantiatedProjectile = (GameObject)Instantiate (FouetEau, posFire, FouetEau.transform.rotation);
			instantiatedProjectile.GetComponent<Renderer> ().material.mainTexture = textureInv_FouetEau;
			Physics.IgnoreCollision(instantiatedProjectile.GetComponent<Collider>(), transform.root.GetComponent<Collider>());
		}
	}

	//PrisonEau
	void Action_Spe_Eau(){
		Vector3 posFire = new Vector3 (ennemi.transform.position.x, ennemi.transform.position.y, ennemi.transform.position.z - 1);
		GameObject instantiatedProjectile = (GameObject)Instantiate (PrisonEau, posFire, PrisonEau.transform.rotation);
		Physics.IgnoreCollision (instantiatedProjectile.GetComponent<Collider> (), transform.root.GetComponent<Collider> ());
		prisoned = true;
		time_prison = (double)Time.time;
		Joueur_Deplacement_J2.setStun (true);
		ennemi.gameObject.GetComponent<Rigidbody>().AddForce (-Vector3.up * 50, ForceMode.Impulse);
	}
		
	//PiqueGlace
	void Action_Pouvoir_Glace(){
		if (versGauche) {
			Vector3 posFire = new Vector3 (transform.position.x - 1F, transform.position.y, transform.position.z);
			GameObject instantiatedProjectile = (GameObject)Instantiate (PiqueGlace, posFire, PiqueGlace.transform.rotation);
			instantiatedProjectile.GetComponent<Rigidbody> ().velocity = new Vector3 (-16 * Mathf.Sign (transform.forward.x), 0, 0);
			Physics.IgnoreCollision(instantiatedProjectile.GetComponent<Collider>(), transform.root.GetComponent<Collider>());
		} else {
			Vector3 posFire = new Vector3 (transform.position.x + 1F, transform.position.y, transform.position.z);
			GameObject instantiatedProjectile = (GameObject)Instantiate (PiqueGlace, posFire, PiqueGlace.transform.rotation);
			instantiatedProjectile.GetComponent<Rigidbody> ().velocity = new Vector3 (16 * Mathf.Sign (transform.forward.x), 0, 0);
			instantiatedProjectile.GetComponent<Renderer> ().material.mainTexture = textureInv_PiqueGlace;
			Physics.IgnoreCollision(instantiatedProjectile.GetComponent<Collider>(), transform.root.GetComponent<Collider>());
		}
	}

	//LancesGlace
	void Action_Spe_Glace(){
		sorted = true;
		time_sort = (double)Time.time;
		Joueur_Deplacement_J1.setStun (true);
		if (versGauche) {
			Vector3 posFireS = new Vector3 (transform.position.x, transform.position.y, transform.position.z);
			GameObject instantiatedProjectileS = (GameObject)Instantiate (LancesSort, posFireS, LancesSort.transform.rotation);
			Physics.IgnoreCollision(instantiatedProjectileS.GetComponent<Collider>(), transform.root.GetComponent<Collider>());

			Vector3 posFire1 = new Vector3 (transform.position.x - 1F, transform.position.y, transform.position.z);
			GameObject instantiatedProjectile1 = (GameObject)Instantiate (LancesGlace, posFire1, LancesGlace.transform.rotation);
			instantiatedProjectile1.GetComponent<Rigidbody> ().velocity = new Vector3 (-20 * Mathf.Sign (transform.forward.x), 0, 0);
			Physics.IgnoreCollision(instantiatedProjectile1.GetComponent<Collider>(), transform.root.GetComponent<Collider>());

			Vector3 posFire2 = new Vector3 (transform.position.x - 1F, transform.position.y + 0.4f, transform.position.z);
			GameObject instantiatedProjectile2 = (GameObject)Instantiate (LancesGlace, posFire2, LancesGlace.transform.rotation);
			instantiatedProjectile2.GetComponent<Rigidbody> ().velocity = new Vector3 (-16 * Mathf.Sign (transform.forward.x), 0, 0);
			Physics.IgnoreCollision(instantiatedProjectile2.GetComponent<Collider>(), transform.root.GetComponent<Collider>());

			Vector3 posFire3 = new Vector3 (transform.position.x - 1F, transform.position.y - 0.4f, transform.position.z);
			GameObject instantiatedProjectile3 = (GameObject)Instantiate (LancesGlace, posFire3, LancesGlace.transform.rotation);
			instantiatedProjectile3.GetComponent<Rigidbody> ().velocity = new Vector3 (-16 * Mathf.Sign (transform.forward.x), 0, 0);
			Physics.IgnoreCollision(instantiatedProjectile3.GetComponent<Collider>(), transform.root.GetComponent<Collider>());

			Vector3 posFire4 = new Vector3 (transform.position.x - 1F, transform.position.y + 0.8f, transform.position.z);
			GameObject instantiatedProjectile4 = (GameObject)Instantiate (LancesGlace, posFire4, LancesGlace.transform.rotation);
			instantiatedProjectile4.GetComponent<Rigidbody> ().velocity = new Vector3 (-12 * Mathf.Sign (transform.forward.x), 0, 0);
			Physics.IgnoreCollision(instantiatedProjectile4.GetComponent<Collider>(), transform.root.GetComponent<Collider>());

			Vector3 posFire5 = new Vector3 (transform.position.x - 1F, transform.position.y - 0.8f, transform.position.z);
			GameObject instantiatedProjectile5 = (GameObject)Instantiate (LancesGlace, posFire5, LancesGlace.transform.rotation);
			instantiatedProjectile5.GetComponent<Rigidbody> ().velocity = new Vector3 (-12 * Mathf.Sign (transform.forward.x), 0, 0);
			Physics.IgnoreCollision(instantiatedProjectile5.GetComponent<Collider>(), transform.root.GetComponent<Collider>());
		} else {
			Vector3 posFireS = new Vector3 (transform.position.x, transform.position.y, transform.position.z);
			GameObject instantiatedProjectileS = (GameObject)Instantiate (LancesSort, posFireS, LancesSort.transform.rotation);
			instantiatedProjectileS.GetComponent<Renderer> ().material.mainTexture = textureInv_LancesSort;
			Physics.IgnoreCollision(instantiatedProjectileS.GetComponent<Collider>(), transform.root.GetComponent<Collider>());

			Vector3 posFire1 = new Vector3 (transform.position.x + 1F, transform.position.y, transform.position.z);
			GameObject instantiatedProjectile1 = (GameObject)Instantiate (LancesGlace, posFire1, LancesGlace.transform.rotation);
			instantiatedProjectile1.GetComponent<Rigidbody> ().velocity = new Vector3 (20 * Mathf.Sign (transform.forward.x), 0, 0);
			instantiatedProjectile1.GetComponent<Renderer> ().material.mainTexture = textureInv_LancesGlace;
			Physics.IgnoreCollision(instantiatedProjectile1.GetComponent<Collider>(), transform.root.GetComponent<Collider>());

			Vector3 posFire2 = new Vector3 (transform.position.x + 1F, transform.position.y + 0.4f, transform.position.z);
			GameObject instantiatedProjectile2 = (GameObject)Instantiate (LancesGlace, posFire2, LancesGlace.transform.rotation);
			instantiatedProjectile2.GetComponent<Rigidbody> ().velocity = new Vector3 (16 * Mathf.Sign (transform.forward.x), 0, 0);
			instantiatedProjectile2.GetComponent<Renderer> ().material.mainTexture = textureInv_LancesGlace;
			Physics.IgnoreCollision(instantiatedProjectile2.GetComponent<Collider>(), transform.root.GetComponent<Collider>());

			Vector3 posFire3 = new Vector3 (transform.position.x + 1F, transform.position.y - 0.4f, transform.position.z);
			GameObject instantiatedProjectile3 = (GameObject)Instantiate (LancesGlace, posFire3, LancesGlace.transform.rotation);
			instantiatedProjectile3.GetComponent<Rigidbody> ().velocity = new Vector3 (16 * Mathf.Sign (transform.forward.x), 0, 0);
			instantiatedProjectile3.GetComponent<Renderer> ().material.mainTexture = textureInv_LancesGlace;
			Physics.IgnoreCollision(instantiatedProjectile3.GetComponent<Collider>(), transform.root.GetComponent<Collider>());

			Vector3 posFire4 = new Vector3 (transform.position.x + 1F, transform.position.y + 0.8f, transform.position.z);
			GameObject instantiatedProjectile4 = (GameObject)Instantiate (LancesGlace, posFire4, LancesGlace.transform.rotation);
			instantiatedProjectile4.GetComponent<Rigidbody> ().velocity = new Vector3 (12 * Mathf.Sign (transform.forward.x), 0, 0);
			instantiatedProjectile4.GetComponent<Renderer> ().material.mainTexture = textureInv_LancesGlace;
			Physics.IgnoreCollision(instantiatedProjectile4.GetComponent<Collider>(), transform.root.GetComponent<Collider>());

			Vector3 posFire5 = new Vector3 (transform.position.x + 1F, transform.position.y - 0.8f, transform.position.z);
			GameObject instantiatedProjectile5 = (GameObject)Instantiate (LancesGlace, posFire5, LancesGlace.transform.rotation);
			instantiatedProjectile5.GetComponent<Rigidbody> ().velocity = new Vector3 (12 * Mathf.Sign (transform.forward.x), 0, 0);
			instantiatedProjectile5.GetComponent<Renderer> ().material.mainTexture = textureInv_LancesGlace;
			Physics.IgnoreCollision(instantiatedProjectile5.GetComponent<Collider>(), transform.root.GetComponent<Collider>());
		}
	}

	void OnDestroy() {
		Degats = 0;
	}
}

