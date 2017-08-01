using UnityEngine;
using System.Collections;

public class Perso_Body_RK : MonoBehaviour {

	public bool first;
	public Sprite texture_base_primaire;
	public Sprite texture_base_secondaire;
	public Sprite texture_saut_primaire;
	public Sprite texture_saut_secondaire;

	[HideInInspector]public int speed_mov = 10;
	[HideInInspector]public int speed_jump = 80;
	[HideInInspector]public int nb_saut = 2;
	[HideInInspector]public bool jumped;
	[HideInInspector]public bool stunned;
	[HideInInspector]public bool fracased;
	[HideInInspector]public bool turned;
	[HideInInspector]public bool transformed;
	[HideInInspector]public bool recharge_Phy;
	[HideInInspector]public bool recharge_Mag;
	[HideInInspector]public bool recharge_Spe;

	private double tmp_recharge_Phy = 0.5;
	private double tmp_recharge_Mag = 2;
	private double tmp_recharge_Spe = 5;
	private double tmp_Stunned;
	private double time_recharge_Phy;
	private double time_recharge_Mag;
	private double time_recharge_Spe;
	private double time_Stunned;
	private double time_recharge_Text;
	private double tmp_text_Phy = 0.5;
	private double tmp_text_Mag = 1;
	private double tmp_text_Spe = 1;
	private SpriteRenderer render;

	//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

	void Start () {
		turned = false;
		stunned = false;
		jumped = false;
		recharge_Phy = true;
		recharge_Mag = true;
		recharge_Spe = true;
		render = GetComponent<SpriteRenderer>();
	}

	void Update () {
		Mouvement ();
		Rechargement ();
	}

	//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

	public void Attaque(){
		recharge_Phy = false;
		time_recharge_Phy = (double) Time.time;
		time_recharge_Text = (double) Time.time;
	}

	public void Pouvoir(){
		recharge_Mag = false;
		time_recharge_Mag = (double) Time.time;
		time_recharge_Text = (double) Time.time;
	}

	public void PouvoirSpe(){
		recharge_Spe = false;
		time_recharge_Spe = (double) Time.time;
		time_recharge_Text = (double) Time.time;
	}

	public void SetFracased(){
		fracased = true;
	}

	public void SetStunned(float tmp){
		stunned = true;
		tmp_Stunned = tmp;
		time_Stunned = (double) Time.time;
	}

	//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

	private void Mouvement (){
		if (!stunned) {
			if ((first && Game_Inputs.J1_Droit_Pressed) || (!first && Game_Inputs.J2_Droit_Pressed)) {
				if (turned) {
					turned = false;
					render.flipX = false;
				}
				transform.Translate (Vector2.right * speed_mov * Time.deltaTime);
			}
			if ((first && Game_Inputs.J1_Gauche_Pressed) || (!first && Game_Inputs.J2_Gauche_Pressed)) {
				if (!turned) {
					turned = true;
					render.flipX = true;
				}
				transform.Translate (-Vector2.right * speed_mov * Time.deltaTime);
			}
			if (((first && Game_Inputs.J1_Haut) || (!first && Game_Inputs.J2_Haut)) && nb_saut == 1) {
				if (!Canvas_Jeu_RK.isPaused) {
					GetComponent<Rigidbody> ().velocity = Vector3.zero;
					GetComponent<Rigidbody> ().angularVelocity = Vector3.zero; 
					GetComponent<Rigidbody> ().AddForce (Vector3.up * speed_jump, ForceMode.Impulse);
					nb_saut--;
					Sauter();
				}
			} else if (((first && Game_Inputs.J1_Haut) || (!first && Game_Inputs.J2_Haut)) && nb_saut == 2) {
				if (!Canvas_Jeu_RK.isPaused) {
					GetComponent<Rigidbody> ().AddForce (Vector3.up * speed_jump * 1.2f, ForceMode.Impulse);
					nb_saut--;
					Sauter();
				}
			}
		}
	}

	private void Rechargement (){
		double time_cours_Text = (double)Time.time - time_recharge_Text;
		if (!recharge_Phy) {
			double time_cours_Phy = (double)Time.time - time_recharge_Phy;
			if(time_cours_Phy >= tmp_recharge_Phy){
				recharge_Phy = true;
			}
			if(time_cours_Text >= tmp_text_Phy){
				if (!transformed) {
					render.sprite = texture_base_primaire;
				} else {
					render.sprite = texture_base_secondaire;
				}
			}
		}
		if (!recharge_Mag) {
			double time_cours_Mag = (double)Time.time - time_recharge_Mag;
			if(time_cours_Mag >= tmp_recharge_Mag){
				recharge_Mag = true;
			}
			if(time_cours_Text >= tmp_text_Mag){
				if (!transformed) {
					render.sprite = texture_base_primaire;
				} else {
					render.sprite = texture_base_secondaire;
				}
			}
		}
		if (!recharge_Spe) {
			double time_cours_Spe = (double)Time.time - time_recharge_Spe;
			if(time_cours_Spe >= tmp_recharge_Spe){
				recharge_Spe = true;
			}
			if(time_cours_Text >= tmp_text_Spe){
				if (!transformed) {
					render.sprite = texture_base_primaire;
				} else {
					render.sprite = texture_base_secondaire;
				}
			}
		}
		if (stunned) {
			double time_cours_stunned = (double)Time.time - time_Stunned;
			if(time_cours_stunned >= tmp_Stunned){
				stunned = false;
			}
		}
	}

	private void Sauter(){
		jumped = true;
		if(!transformed) {
			render.sprite = texture_saut_primaire;
		} else {
			render.sprite = texture_saut_secondaire;
		}
	}

	private void Atterrir(){
		if (jumped) {
			if(!transformed) {
				render.sprite = texture_base_primaire;
			} else {
				render.sprite = texture_base_secondaire;
			}
			jumped = false;
		}
	}

	//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

	void OnCollisionEnter (Collision objetInfo){
		if (objetInfo.gameObject.tag == "Sol") {
			nb_saut = 2;
			Atterrir ();
			if (fracased) {
				fracased = false;
				Perso_Pouv05_RK pouvoirs = GetComponent<Perso_Pouv05_RK>();
				pouvoirs.UseFracased ();
			}
		}
	}
}