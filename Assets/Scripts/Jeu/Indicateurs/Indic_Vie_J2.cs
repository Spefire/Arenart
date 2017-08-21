using UnityEngine;
using System.Collections;

public class Indic_Vie_J2 : MonoBehaviour {

	public Sprite texture_base;
	public Sprite texture_vie_plus;
	public Sprite texture_vie_moins;
	private static double tmp_vie = 1;
	private static double time_vie;
	private static bool vie;
	private static bool plus;
	private static bool moins;
	private GameObject J2;
	private SpriteRenderer render;

	void Start () {
		J2 = GameObject.FindGameObjectWithTag ("J2");
		render = GetComponent<SpriteRenderer>();
		vie = false;
		moins = false;
		plus = false;
	}

	void Update () {
		transform.position = new Vector3 (J2.transform.position.x, J2.transform.position.y, J2.transform.position.z - 1);
		if (vie) {
			double time_cours = (double)Time.time - time_vie;
			if(time_cours >= tmp_vie){
				vie = false;
				render.sprite = texture_base;
			} else if (moins) {
				moins = false;
				render.sprite = texture_vie_moins;
			} else if (plus) {
				plus = false;
				render.sprite = texture_vie_plus;
			}
		}
	}

	public static void Set_Vie_Moins(){
		time_vie = (double)Time.time;
		vie = true;
		moins = true;
		plus = false;
	}
	public static void Set_Vie_Plus(){
		time_vie = (double)Time.time;
		vie = true;
		moins = false;
		plus = true;
	}
}
