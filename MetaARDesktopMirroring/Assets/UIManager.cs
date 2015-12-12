using UnityEngine;
using System.Collections;
using System.Net;
using System.Text;
using System.Security.Cryptography.X509Certificates;
using System.Net.Security;

public class UIManager : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	public void PreviousTrack()
	{
		SendTrack (-1);
	}
	public void NextTrack()
	{
		SendTrack (1);
	}
	public void SendTrack(int x)
	{
				//WWWForm form = new WWWForm();
				// Upload to a cgi script
				string postData = "\"" + x + "\"";
				/*
		form.AddField ("data", data);
		WWW w = new WWW("http://metacast.firebaseio.com/click", form);
		yield return w;

		*/
				ServicePointManager.ServerCertificateValidationCallback = Validator;
		
				// Create a request using a URL that can receive a post. 
				var request = (HttpWebRequest)WebRequest.Create ("https://metacast.firebaseio.com/command.json");
		
				var data = Encoding.ASCII.GetBytes (postData);
				request.UserAgent = "test.net";
				request.Accept = "application/json";
				request.Method = "PUT";
				request.ContentType = "raw";
				request.ContentLength = data.Length;
		
				using (var stream = request.GetRequestStream()) {
						stream.Write (data, 0, data.Length);
				}
		
				var response = (HttpWebResponse)request.GetResponse ();
		}
	public static bool Validator (object sender, X509Certificate certificate, X509Chain chain,
	                              SslPolicyErrors sslPolicyErrors)
	{
		return true;
	}
}
