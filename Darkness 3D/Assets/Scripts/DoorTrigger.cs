using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorTrigger : MonoBehaviour {

	private Animation anim;
	private bool activated;
	public GameObject brazier1;
	public GameObject brazier2;
	public GameObject darkness;
	private DarknessController script;

	// Use this for initialization
	void Start () {
		anim = GetComponent<Animation> ();
		activated = false;
		script = darkness.GetComponent<DarknessController> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (!activated) {
			if (brazier1.GetComponent<Light> ().intensity > 0 && brazier2.GetComponent<Light> ().intensity > 0) {
				anim.Play ();
				activated = true;
				if (!script.fading) {
					StartCoroutine (script.fade ());
					script.fading = true;
				}
			}
		} else {
			if (!anim.isPlaying && !script.fading) {
				Destroy (gameObject);
			}
		}
	}
}
