using UnityEngine;
using System.Collections;

public class Indic_Resist_J1 : MonoBehaviour {

	public Texture texture_base;
	public Texture texture_resist_plus;
	public Texture texture_resist_moins;
	private static double tmp_resist = 1;
	private static double time_resist;
	private static bool resist;
	private static bool plus;
	private static bool moins;
	private Renderer render;

	void Start () {
		render = GetComponent<Renderer>();
		resist = false;
		moins = false;
		plus = false;
	}

	void Update () {
		if (resist) {
			double time_cours = (double)Time.time - time_resist;
			if (time_cours >= tmp_resist) {
				resist = false;
				render.material.mainTexture = texture_base;
			} else if (moins) {
				moins = false;
				render.material.mainTexture = texture_resist_moins;
			} else if (plus) {
				plus = false;
				render.material.mainTexture = texture_resist_plus;
			}
		}
	}

	public static void set_Resist_Moins(){
		time_resist = (double)Time.time;
		resist = true;
		moins = true;
		plus = false;
	}
	public static void set_Resist_Plus(){
		time_resist = (double)Time.time;
		resist = true;
		moins = false;
		plus = true;
	}
}
