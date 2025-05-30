using UnityEngine;
using UnityEngine.UI;

public class UI_DisplayImage : MonoBehaviour {
	[SerializeField] Sprite brokenFurnitureSprite;
	[SerializeField] Image displayImage;

	Animator animator;


	void Start() => animator = GetComponentInChildren<Animator>();

	public void DisplayFurniture(Sprite furnitureSprite) {
		displayImage.sprite = furnitureSprite;
		animator.Play("DisplayFunitureCorrect");
	}

	public void DisplayFurniture() {
		displayImage.sprite = brokenFurnitureSprite;
		animator.Play("DisplayFunitureWrong");
	}
}
