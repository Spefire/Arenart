using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class Image_Menu_RK : MonoBehaviour {
	
	public Sprite[] textures;
	public int index;
	public float maxValue = 2.0f;
	private float speed;
	private bool isShowing;
	private Color color;
	private Image img;

	void OnEnable () {
		speed = 0.5f;
		isShowing = true;
		img = GetComponent<Image>();
	}

	void Update () {
		if (isShowing) {
			color = img.color;
			color.a += Time.deltaTime * speed;
			img.color = color;
			if (img.color.a >= maxValue) {
				isShowing = false;
			}
		} else {
			color = img.color;
			color.a -= Time.deltaTime * speed;
			img.color = color;
			if (img.color.a <= 0.0f) {
				isShowing = true;
				int oldIndex = index;
				while (oldIndex == index) {
					index = Random.Range(0, textures.Length);
				}
				img.sprite = textures [index];
			}
		}
	}
}