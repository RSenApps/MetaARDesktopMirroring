using UnityEngine;
using System.Collections;
using System.Net;
using System.IO;
using System.Text;
using System.Security.Cryptography.X509Certificates;
using System.Net.Security;
using Meta;
public class TapDetection : MonoBehaviour {
	public GameObject phoneDisplay;
	public ThalmicMyo myo;
	public TapHandler handler;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	
	}

	

	public void OnTouchEnter() {
		handler.handleTap (Meta.Hands.right.pointer.position.x, Meta.Hands.right.pointer.position.y);
        Debug.Log("TAPDETECTED: " + Meta.Hands.right.pointer.position.x + ", " + Meta.Hands.right.pointer.position.y);
	}
    

}
