using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Level_Manager : MonoBehaviour {

    private int select_P1;
    private int select_P2;
	private bool started;
	public GameObject j1_perso_01;
	public GameObject j1_perso_02;
	public GameObject j1_perso_03;
	public GameObject j1_perso_04;
	public GameObject j1_perso_05;
	public GameObject j2_perso_01;
	public GameObject j2_perso_02;
	public GameObject j2_perso_03;
	public GameObject j2_perso_04;
	public GameObject j2_perso_05;

	void Start(){
		started = false;
		print ("Scene en cours : "+SceneManager.GetActiveScene ().name);
	}

    void Update() {
		if (SceneManager.GetActiveScene ().name == "Scene_Menu") {
			select_P1 = Selection_P1.select_perso;
			select_P2 = Selection_P2.select_perso;
		} else if (!started) {
			print ("Scene en cours : "+SceneManager.GetActiveScene ().name);
			started = true;
			GameObject spawn_J1 = GameObject.FindGameObjectWithTag ("Spawn_J1");
			GameObject spawn_J2 = GameObject.FindGameObjectWithTag ("Spawn_J2");
			switch(select_P1){
				case 1:{
					Instantiate (j1_perso_01, spawn_J1.transform.position, spawn_J1.transform.rotation);
					break;
				}
				case 2:{
					Instantiate (j1_perso_02, spawn_J1.transform.position, spawn_J1.transform.rotation);
					break;
				}
				case 3:{
					Instantiate (j1_perso_03, spawn_J1.transform.position, spawn_J1.transform.rotation);
					break;
				}
				case 4:{
					Instantiate (j1_perso_04, spawn_J1.transform.position, spawn_J1.transform.rotation);
					break;
				}
				case 5:{
					Instantiate (j1_perso_05, spawn_J1.transform.position, spawn_J1.transform.rotation);
					break;
				}
			}
			switch(select_P2){
			case 1:{
					Instantiate (j2_perso_01, spawn_J2.transform.position, spawn_J2.transform.rotation);
					break;
				}
			case 2:{
					Instantiate (j2_perso_02, spawn_J2.transform.position, spawn_J2.transform.rotation);
					break;
				}
			case 3:{
					Instantiate (j2_perso_03, spawn_J2.transform.position, spawn_J2.transform.rotation);
					break;
				}
			case 4:{
					Instantiate (j2_perso_04, spawn_J2.transform.position, spawn_J2.transform.rotation);
					break;
				}
			case 5:{
					Instantiate (j2_perso_05, spawn_J2.transform.position, spawn_J2.transform.rotation);
					break;
				}
			}
		}
    }
}