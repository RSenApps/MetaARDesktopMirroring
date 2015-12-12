using UnityEngine;
using System.Collections;
using Meta;
using Thalmic.Myo;
public class RayCastHit : MonoBehaviour {
	public GameObject metaWorld;
	public ThalmicMyo myo;
	public TapHandler tapHandler;
	public GameObject display;
	private Thalmic.Myo.Pose _lastPose = Pose.Unknown;
	static float timethreshold = .5f;
	float lastTime = 0f;
	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {

			//myo.Unlock (UnlockType.Hold);

		
		if (myo.pose != _lastPose) {

			if (myo.pose == Pose.Fist && Time.time - lastTime > timethreshold) {
				lastTime = Time.time;

				UnityEngine.Vector3 point = findCollisionPosition();
                Debug.Log(point.x + ", " + point.y + ", " + point.z);
				if (point != UnityEngine.Vector3.zero)
				{
					tapHandler.handleTap(point.x, point.y);
				}

            }
            _lastPose = myo.pose;


        }
    }
	UnityEngine.Vector3 findCollisionPosition() {
		Plane plane = new Plane(new UnityEngine.Vector3 (0f, 0f, -1f), new UnityEngine.Vector3 (0.02939663f, -0.03277239f, .4f));
		Ray ray = new Ray(new UnityEngine.Vector3(0,0,0), metaWorld.transform.forward);
        Debug.Log("metaworld:" + metaWorld.transform.forward);
		float distance;
		if (plane.Raycast(ray, out distance)) {
			return ray.GetPoint(distance);
        }
				else {
			return UnityEngine.Vector3.zero;
				}
	}
}
