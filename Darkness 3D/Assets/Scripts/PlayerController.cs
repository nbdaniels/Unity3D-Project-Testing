using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {

	public Rigidbody rb;
	public float fuelLevel;
	public float fuelUseRate;
	public Torchelight playerTorch;
	public Slider fuelSlider;  

	private float maxFuel;
	private float lightingCost;

	public Text fuelText;

	public Material litBrazier;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody> ();
		fuelLevel = 100.0f;
		maxFuel = 100.0f;
		lightingCost = 10.0f;
	}

	void Update()
	{
		if (fuelLevel > lightingCost && Input.GetKeyDown (KeyCode.E)) {
			Collider nearestObject = findClosestCollider ();
			if (nearestObject != null && nearestObject.CompareTag ("Lightable")) {
				//light it up
				if (nearestObject.gameObject.transform.Find("Brazier Light").GetComponent<Light>().intensity <= 0.0f) {
					Debug.Log ("Light it up!");
					nearestObject.GetComponent<Renderer> ().material = litBrazier;
					nearestObject.gameObject.transform.Find ("Brazier Light").GetComponent<Light> ().intensity = 5.0f;
					fuelLevel -= lightingCost;
				}
			}
		}
		updateFuel ();
	}

	void OnTriggerEnter(Collider other) 
	{
		if (other.gameObject.CompareTag ("Fuel") && fuelLevel < maxFuel)
		{
			other.gameObject.SetActive (false);
			fuelLevel += 50.0f;
			if (fuelLevel > maxFuel) {
				fuelLevel = maxFuel;
			}
			Destroy (other);
		}
	}

	void updateFuel()
	{
		fuelLevel -= fuelUseRate;
		if (fuelLevel < 0) {
			fuelLevel = 0;
			playerTorch.IntensityLight = 0;
		} else {
			playerTorch.IntensityLight = 5;
		}
		fuelText.text = "" + fuelLevel;
		fuelSlider.value = fuelLevel;
	}

	Collider findClosestCollider ()
	{
		Vector3 center = transform.position;
		float radius = 1.0f;
		Collider[] colliders = Physics.OverlapSphere(center, radius);

		Collider nearestCollider = null;
		float minSqrDistance = Mathf.Infinity;

		for (int i = 0; i < colliders.Length; i++)
		{
			if (!colliders [i].CompareTag ("Lightable"))
				continue;
			float sqrDistanceToCenter = (center - colliders[i].transform.position).sqrMagnitude;

			if (sqrDistanceToCenter < minSqrDistance)
			{
				minSqrDistance = sqrDistanceToCenter;
				nearestCollider = colliders[i];
			}
		}
		if (nearestCollider == null)
			Debug.Log ("Null Collider");
		else
			Debug.Log (nearestCollider.tag);
		return nearestCollider;
	}
}
