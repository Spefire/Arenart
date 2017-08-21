using UnityEngine;
using System.Collections;

public class Perso_Stats_RK: MonoBehaviour {

	public bool first;

	[HideInInspector]public int vie;
	[HideInInspector]public int resistance;
	[HideInInspector]public int degats;
	[HideInInspector]public int points;
	[HideInInspector]public GameObject enemy;
	[HideInInspector]public Vector3 enemyPos;
	[HideInInspector]public Rigidbody enemyRigid;

	private GameObject spawn;

	//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

	void Start () {
		vie = 100;
		resistance = 100;
		degats = 10;
		points = 0;
		if (first) {
			spawn = GameObject.FindGameObjectWithTag ("Spawn_J1");
			enemy = GameObject.FindGameObjectWithTag ("J2");
		} else {
			spawn = GameObject.FindGameObjectWithTag ("Spawn_J2");
			enemy = GameObject.FindGameObjectWithTag ("J1");
		}
		if (enemy != null) {
			Physics.IgnoreCollision (enemy.GetComponent<Collider> (), transform.root.GetComponent<Collider> ());
			enemyPos = enemy.transform.position;
			enemyRigid = enemy.GetComponent<Rigidbody> ();
		}
	}

	void Update () {
		UpdateEnemyPosition ();
		TestVie ();
	}

	//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

	private void UpdateEnemyPosition() {
		enemyPos = enemy.transform.position;
	}

	private void Respawn() {
		transform.position = spawn.transform.position;
		GetComponent<Rigidbody> ().velocity = Vector3.zero;
		GetComponent<Rigidbody> ().angularVelocity = Vector3.zero; 
	}

	private void TestVie () {
		if (vie <= 0) {
			vie = 100;
			resistance = 100;
			Respawn ();
		}
	}

	public int GetDamage (double coeff) {
		int result = (int) (degats * (coeff - resistance / 100));
		return result;
	}

	public void SetDamage (int v, int r){
		if (vie - v < 1) {
			vie = 0;
			Perso_Stats_RK statsEnemy = enemy.GetComponent<Perso_Stats_RK> ();
			statsEnemy.GivePoints (+1);
		} else if (vie - v > 100) {
			vie = 100;
		} else {
			vie -= v;
			if (first && v > 0) {
				Indic_Vie_J1.Set_Vie_Moins ();
			} else if (first && v < 0) {
				Indic_Vie_J1.Set_Vie_Plus ();
			} else if (!first && v > 0) {
				Indic_Vie_J2.Set_Vie_Moins ();
			} else if (!first && v < 0) {
				Indic_Vie_J2.Set_Vie_Plus ();
			}
		}
		if (resistance - r < 1) {
			resistance = 0;
		} else if (resistance - r > 100) {
			resistance = 100;
		} else {
			resistance -= r;
			if (first && r > 0) {
				Indic_Resist_J1.Set_Resist_Moins ();
			} else if (first && r < 0) {
				Indic_Resist_J1.Set_Resist_Plus ();
			} else if (!first && r > 0) {
				Indic_Resist_J2.Set_Resist_Moins ();
			} else if (!first && r < 0) {
				Indic_Resist_J2.Set_Resist_Plus ();
			}
		}
	}

	public void GivePoints(int p) {
		points += p;
	}

	//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

	void OnCollisionEnter (Collision objetInfo){
		if (objetInfo.gameObject.tag == "Mort") {
			resistance = 0;
			if (first) {
				Indic_Resist_J1.Set_Resist_Moins ();
			} else {
				Indic_Resist_J2.Set_Resist_Moins ();
			}
			Respawn ();
		}
	}
}