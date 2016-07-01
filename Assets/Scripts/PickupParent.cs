using UnityEngine;
using System.Collections;


[RequireComponent(typeof(SteamVR_TrackedObject))]
public class PickupParent : MonoBehaviour {


	SteamVR_TrackedObject trackedObj;
	SteamVR_Controller.Device device;

	public Transform sphere;

	// Use this for initialization regardless if obj is enabled
	void Awake () {
		trackedObj = GetComponent<SteamVR_TrackedObject> ();
	}
	
	// FixedUpdate is called every physics step
	void FixedUpdate () {
		device = SteamVR_Controller.Input ((int)trackedObj.index);

		if (device.GetTouch (SteamVR_Controller.ButtonMask.Trigger)) {
			Debug.Log ("You are holding the Trigger");
		}

		if (device.GetTouchDown (SteamVR_Controller.ButtonMask.Trigger)) {
			Debug.Log ("You pressed down the Trigger");
		}

		if (device.GetTouchUp (SteamVR_Controller.ButtonMask.Trigger)) {
			Debug.Log ("You let go of the Trigger");
		}

		if (device.GetPress (SteamVR_Controller.ButtonMask.Grip)) {
			Debug.Log ("You are holding the Grip");
		}

		if (device.GetPressDown (SteamVR_Controller.ButtonMask.Grip)) {
			Debug.Log ("You pressed down the Grip");
		}

		if (device.GetPressUp (SteamVR_Controller.ButtonMask.Grip)) {
			Debug.Log ("You let go of the Grip");
		}
			
		if (device.GetPress (SteamVR_Controller.ButtonMask.Touchpad)) {
			Debug.Log ("You are holding the Touchpad");
		}

		if (device.GetPressDown (SteamVR_Controller.ButtonMask.Touchpad)) {
			Debug.Log ("You pressed down the Touchpad");
		}

		if (device.GetPressUp (SteamVR_Controller.ButtonMask.Touchpad)) {
			Debug.Log ("You let go of the Touchpad");
			sphere.transform.position = new Vector3 (0, .5f, 0);
			sphere.GetComponent<Rigidbody> ().velocity = Vector3.zero;
			sphere.GetComponent<Rigidbody> ().angularVelocity = Vector3.zero;
		}

	}

	void OnTriggerStay (Collider col) {
		Debug.Log ("Controller collided with " + col.name);
		if (device.GetTouch (SteamVR_Controller.ButtonMask.Trigger)) {
			Debug.Log ("You are holding " + col.name);
			col.attachedRigidbody.isKinematic = true;
			col.gameObject.transform.SetParent (this.gameObject.transform);
		}
		if (device.GetTouchUp (SteamVR_Controller.ButtonMask.Trigger)) {
			Debug.Log ("You release " + col.name);
			col.attachedRigidbody.isKinematic = false;
			col.gameObject.transform.SetParent (null);

			tossObject (col.attachedRigidbody);
		}
	}

	void tossObject(Rigidbody rigidbody) {
		Transform origin = trackedObj.origin ? trackedObj.origin : trackedObj.transform.parent;
		if (origin != null) {
			rigidbody.velocity = origin.TransformVector (device.velocity);
			rigidbody.angularVelocity = origin.TransformVector (device.angularVelocity);
		} else {
			rigidbody.velocity = device.velocity;
			rigidbody.angularVelocity = device.angularVelocity;
		}
	}
}
