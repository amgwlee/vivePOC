using UnityEngine;
using System.Collections;


[RequireComponent(typeof(SteamVR_TrackedObject))]
public class PickupParent : MonoBehaviour {


	SteamVR_TrackedObject trackedObj;

	// Use this for initialization regardless if obj is enabled
	void Awake () {
		trackedObj = GetComponent<SteamVR_TrackedObject> ();
	}
	
	// FixedUpdate is called every physics step
	void FixedUpdate () {
		SteamVR_Controller.Device device = SteamVR_Controller.Input ((int)trackedObj.index);
		if (device.GetTouch (SteamVR_Controller.ButtonMask.Trigger)) {
			Debug.Log ("You are gently holding down the Trigger");
		}

		if (device.GetTouchDown (SteamVR_Controller.ButtonMask.Trigger)) {
			Debug.Log ("You are clicking the Trigger");
		}

		if (device.GetTouchUp (SteamVR_Controller.ButtonMask.Trigger)) {
			Debug.Log ("You let go of the Trigger");
		}

	}
}
