using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour {

	public Rigidbody rb;
	public float speed;
	public float fuelLevel;
	public float fuelUseRate;
	public Torchelight playerTorch;

	private float maxFuel;

	public Text fuelText;

	public Material litBrazier;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody> ();
		fuelLevel = 100.0f;
		maxFuel = 100.0f;
	}

	void Update()
	{
		updateFuelText ();
		if (Input.GetKeyDown (KeyCode.E)) {
			Collider nearestObject = findClosestCollider ();
			if (nearestObject.CompareTag ("Lightable")) {
				//light it up
				Debug.Log ("Light it up!");
				nearestObject.GetComponent<Renderer>().material = litBrazier;
				nearestObject.gameObject.transform.Find ("Brazier Light").GetComponent<Light> ().intensity = 5.0f;
			}
		}
	}
	
	void FixedUpdate () //before physics updates
	{
		float moveHorizontal = Input.GetAxis ("Horizontal");
		float moveVertical = Input.GetAxis ("Vertical");

		Vector3 movement = new Vector3 (moveHorizontal, 0.0f, moveVertical);

		rb.AddForce (movement * speed);
	}

	void OnTriggerEnter(Collider other) 
	{
		if (other.gameObject.CompareTag ("Fuel"))
		{
			other.gameObject.SetActive (false);
			fuelLevel += 50.0f;
			if (fuelLevel > maxFuel) {
				fuelLevel = maxFuel;
			}
		}
	}

	void updateFuelText()
	{
		fuelLevel -= fuelUseRate;
		if (fuelLevel < 0) {
			fuelLevel = 0;
			playerTorch.IntensityLight = 0;
		} else {
			playerTorch.IntensityLight = 5;
		}
		fuelText.text = "Fuel Level: " + fuelLevel;
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
		Debug.Log (nearestCollider.tag);
		return nearestCollider;
	}
}
