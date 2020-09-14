//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class Authentificator : MonoBehaviour
//{
//    // Start is called before the first frame update
//    void Start()
//    {
//		var jlt = new JsonLoaderTest();
//		jlt.Init();
//	}
//}

//public class JsonLoaderTest : MonoBehaviour
//{
//	public static string BASE_HTTP_URL = "http://localhost:8080/";
//	public static string BASE_HTTPS_URL = "https://localhost:8080/";

//	private string _idToken = "";

//	public void Init()
//	{
//		StartCoroutine(GetBeers());
//		StartCoroutine(Login());
//		StartCoroutine(GetBeers());
//	}

//	private IEnumerator GetBeers()
//	{
//		Dictionary<string, string> headers = new Dictionary<string, string>();
//		headers.Add("Authorization", "Bearer " + _idToken);
//		WWW www = new WWW(BASE_HTTP_URL + "api/beers", null, headers);
//		while (!www.isDone) yield return null;
//		Debug.Log(www.text);
//	}

//	public class LoginPackage
//	{
//		public string username;
//		public string password;
//		public bool rememberMe;
//	}

//	public class IdTokenPackage
//	{
//		public string idToken;
//	}

//	private IEnumerator Login()
//	{
//		LoginPackage loginPackage = new LoginPackage();
//		loginPackage.username = "admin";
//		loginPackage.password = "admin";
//		loginPackage.rememberMe = true;

//		Dictionary<string, string> postHeaders = new Dictionary<string, string>();
//		postHeaders.Add("Content-Type", "application/json");
//		string json = JsonUtility.ToJson(loginPackage);
//		byte[] postData = System.Text.Encoding.UTF8.GetBytes(json);
//		WWW www = new WWW(BASE_HTTP_URL + "api/authenticate", postData, postHeaders);
//		while (!www.isDone) yield return null;
//		Debug.Log(www.text);
//		_idToken = JsonUtility.FromJson<IdTokenPackage>(www.text).idToken;
//	}
//}