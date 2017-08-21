using UnityEngine;
using System.Collections;

public class Indic_Resist_J1 : MonoBehaviour {

	public Sprite texture_base;
	public Sprite texture_resist_plus;
	public Sprite texture_resist_moins;
	private static double tmp_resist = 1;
	private static double time_resist;
	private static bool resist;
	private static bool plus;
	private static bool moins;
	private GameObject J1;
	private SpriteRenderer render;

	void Start () {
		J1 = GameObject.FindGameObjectWithTag ("J1");
		render = GetComponent<SpriteRenderer>();
		resist = false;
		moins = false;
		plus = false;
	}

	void Update () {
		transform.position = new Vector3 (J1.transform.position.x, J1.transform.position.y, J1.transform.position.z - 1);
		if (resist) {
			double time_cours = (double)Time.time - time_resist;
			if (time_cours >= tmp_resist) {
				resist = false;
				render.sprite = texture_base;
			} else if (moins) {
				moins = false;
				render.sprite = texture_resist_moins;
			} else if (plus) {
				plus = false;
				render.sprite = texture_resist_plus;
			}
		}
	}

	public static void Set_Resist_Moins(){
		time_resist = (double)Time.time;
		resist = true;
		moins = true;
		plus = false;
	}
	public static void Set_Resist_Plus(){
		time_resist = (double)Time.time;
		resist = true;
		moins = false;
		plus = true;
	}
}
