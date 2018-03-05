using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DarknessController : MonoBehaviour {

	private float duration = 2.0f;
	private Renderer rend;
	public bool fading;

	// Use this for initialization
	void Start () {
		rend = gameObject.GetComponent<Renderer> ();
		fading = false;

		Color originalColor = rend.material.color;
		rend.material.color = new Color (originalColor.r, originalColor.g, originalColor.b, 1.0f);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public IEnumerator fade()
	{
		float counter = 0;
		Color darknessColor = rend.material.color;

		while (counter < duration)
		{
			counter += Time.deltaTime;

			float alpha = Mathf.Lerp(1.0f, -0.1f, counter / duration);
			Debug.Log("Changing Transparency: " + alpha);

			//Change alpha only
			rend.material.color = new Color(darknessColor.r, darknessColor.g, darknessColor.b, alpha);

			if (alpha < 0)
				break;
			//Wait for a frame
			yield return null;
		}
		fading = false;
		Destroy (gameObject);
	}
}
