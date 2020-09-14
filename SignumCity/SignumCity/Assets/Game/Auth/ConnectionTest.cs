//using JWT;
//using JWT.Serializers;
//using Newtonsoft.Json;
//using System;
//using System.Collections;
//using System.IO;
//using System.Text;
//using UnityEngine;
//using UnityEngine.Networking;

//public class ConnectionTest : MonoBehaviour
//{
//	//201 - success reg
//	//400 - user exist
//	//400 - не читается
//	//
//	//
//	// Start is called before the first frame update
//	void Start()
//	{
//		//StartCoroutine(SendGetRequest());
//		var user = new RegisterUserPayload("passworD1", "380933855231", email: "testnau1k2@mail.ru");
//		var loginPayload = new LoginPayload(login: user.email, password: user.password);
//		//StartCoroutine(Register(user));
//		//StartCoroutine(Login(loginPayload));
//		//StartCoroutine(RefreshJWT());
//		//StartCoroutine(GetUserInfo());
//	}


//	IEnumerator Register(RegisterUserPayload newUser)
//	{
//		string jsonNewUser = JsonUtility.ToJson(newUser);
//		//string jsonNewUser = "\"password\":\"testPass2\",\"phone\":\"testPhone2\",\"email\":\"testEmail2\"";
//		Debug.Log(jsonNewUser);
//		//.Post("http://165.22.131.242:5000/v1/register", Encoding.UTF8.GetBytes(jsonNewUser
//		UnityWebRequest www = new UnityWebRequest("http://165.22.131.242:5000/v1/register", "POST");
//		//www.SetRequestHeader("Content-Type", "application/json");//Content-Type

//		UploadHandler uploader = new UploadHandlerRaw(Encoding.UTF8.GetBytes(jsonNewUser));
//		uploader.contentType = "application/json";

//		DownloadHandlerBuffer downloader = new DownloadHandlerBuffer();

//		www.uploadHandler = uploader;
//		www.downloadHandler = downloader;

//		yield return www.SendWebRequest();
//		if (www.isNetworkError || www.isHttpError)
//		{
//			Debug.Log(www.responseCode);
//			Debug.Log(www.error);
//			Debug.Log(www.downloadHandler.text);
//			Debug.Log(www.uploadHandler.contentType);
//			Debug.Log(Encoding.UTF8.GetString(www.uploadHandler.data));
//			foreach (var header in www.GetResponseHeaders())
//			{
//				Debug.Log(header.Key + ": " + header.Value);
//			}

//		}
//		else
//		{

//			// Or retrieve results as binary data
//			byte[] results = www.downloadHandler.data;
//			Debug.Log(www.downloadHandler.text);
//			Debug.Log(www.responseCode);
//		}
//	}

//	IEnumerator Login(LoginPayload user)
//	{
//		string jsonNewUser = JsonUtility.ToJson(user);
//		Debug.Log(jsonNewUser);
//		UnityWebRequest www = new UnityWebRequest("http://165.22.131.242:5000/v1/login", "POST");

//		UploadHandler uploader = new UploadHandlerRaw(Encoding.UTF8.GetBytes(jsonNewUser));
//		uploader.contentType = "application/json";

//		DownloadHandlerBuffer downloader = new DownloadHandlerBuffer();

//		www.uploadHandler = uploader;
//		www.downloadHandler = downloader;

//		yield return www.SendWebRequest();
//		if (www.isNetworkError || www.isHttpError)
//		{
//			Debug.Log(www.responseCode);
//			Debug.Log(www.error);
//			Debug.Log(www.downloadHandler.text);
//			Debug.Log(www.uploadHandler.contentType);
//			Debug.Log(Encoding.UTF8.GetString(www.uploadHandler.data));
//			foreach (var header in www.GetResponseHeaders())
//			{
//				Debug.Log(header.Key + ": " + header.Value);
//			}

//		}
//		else
//		{

//			// Or retrieve results as binary data
//			Debug.Log(www.downloadHandler.text);
//			Debug.Log("========================");
//			var loginResp = JsonConvert.DeserializeObject<LoginResponsePayload>(www.downloadHandler.text);
//			Debug.Log("message :" + loginResp.message);
//			Debug.Log("user_id :" + loginResp.user_id);
//			Debug.Log("credintials.access_token :" + loginResp.credentials.access_token);
//			Debug.Log("credintials.refresh_token :" + loginResp.credentials.refresh_token);
//			Debug.Log("========================");
//			Debug.Log(www.responseCode);
//		}
//	}

//	IEnumerator RefreshJWT()
//	{
//		UnityWebRequest www = new UnityWebRequest("http://165.22.131.242:5000/v1/refresh", "POST");

//		//UploadHandler uploader = new UploadHandlerRaw(Encoding.UTF8.GetBytes(jsonNewUser));
//		//uploader.contentType = "application/json";

//		DownloadHandlerBuffer downloader = new DownloadHandlerBuffer();
//		//Authorization: Bearer
//		string refresh_token = "eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJpYXQiOjE1NTU0MjA1MzcsIm5iZiI6MTU1NTQyMDUzNywianRpIjoiY2M2MDRhMGMtN2I1MC00NjlmLWJhMWUtODA3MDdlOThjYTRlIiwiZXhwIjoxNTU4MDEyNTM3LCJpZGVudGl0eSI6Ijd6SkxRa0wzNGQ2ZTYxN2QzZGQ0NGJkYjc5Y2E1NjJlZDQ1ZjNiNTJRREd4VEgiLCJ0eXBlIjoicmVmcmVzaCJ9.U1-05N2PTGxqnzBifOMJuMFQFob0LcpndCeZ25FP5To";
//		string access_token = "eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJpYXQiOjE1NTU0MjIyNTgsIm5iZiI6MTU1NTQyMjI1OCwianRpIjoiZDQ4YWYxMTQtODVlNC00NDY3LWJhYzgtNmUyNzBmOTFjMmVmIiwiZXhwIjoxNTU1NDIzMTU4LCJpZGVudGl0eSI6Ijd6SkxRa0wzNGQ2ZTYxN2QzZGQ0NGJkYjc5Y2E1NjJlZDQ1ZjNiNTJRREd4VEgiLCJmcmVzaCI6ZmFsc2UsInR5cGUiOiJhY2Nlc3MifQ.DhB8THp87no7x0BtGXk8pIk-ZJIuDmQyqK_ZGsB4FL4";
//		//www.SetRequestHeader("refresh_token", refresh_token);
//		//www.SetRequestHeader("access_token", access_token);
//		www.SetRequestHeader("Authorization", "Bearer " + refresh_token);

//		//www.uploadHandler = uploader;
//		www.downloadHandler = downloader;

//		yield return www.SendWebRequest();
//		if (www.isNetworkError || www.isHttpError)
//		{
//			Debug.Log(www.responseCode);
//			Debug.Log(www.error);
//			Debug.Log(www.downloadHandler.text);
//			//Debug.Log(www.uploadHandler.contentType);
//			//Debug.Log(Encoding.UTF8.GetString(www.uploadHandler.data));
//			foreach (var header in www.GetResponseHeaders())
//			{
//				Debug.Log(header.Key + ": " + header.Value);
//			}

//		}
//		else
//		{

//			// Or retrieve results as binary data
//			Debug.Log(www.downloadHandler.text);
//			Debug.Log(www.responseCode);
//		}
//	}

//	IEnumerator GetUserInfo()
//	{
//		UnityWebRequest www = new UnityWebRequest("http://165.22.131.242:5000/v1/user", "GET");

//		//UploadHandler uploader = new UploadHandlerRaw(Encoding.UTF8.GetBytes(jsonNewUser));
//		//uploader.contentType = "application/json";

//		DownloadHandlerBuffer downloader = new DownloadHandlerBuffer();
//		//Authorization: Bearer
//		string refresh_token = "eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJpYXQiOjE1NTU0MjA1MzcsIm5iZiI6MTU1NTQyMDUzNywianRpIjoiY2M2MDRhMGMtN2I1MC00NjlmLWJhMWUtODA3MDdlOThjYTRlIiwiZXhwIjoxNTU4MDEyNTM3LCJpZGVudGl0eSI6Ijd6SkxRa0wzNGQ2ZTYxN2QzZGQ0NGJkYjc5Y2E1NjJlZDQ1ZjNiNTJRREd4VEgiLCJ0eXBlIjoicmVmcmVzaCJ9.U1-05N2PTGxqnzBifOMJuMFQFob0LcpndCeZ25FP5To";
//		string access_token = "eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJpYXQiOjE1NTU0MjIyNTgsIm5iZiI6MTU1NTQyMjI1OCwianRpIjoiZDQ4YWYxMTQtODVlNC00NDY3LWJhYzgtNmUyNzBmOTFjMmVmIiwiZXhwIjoxNTU1NDIzMTU4LCJpZGVudGl0eSI6Ijd6SkxRa0wzNGQ2ZTYxN2QzZGQ0NGJkYjc5Y2E1NjJlZDQ1ZjNiNTJRREd4VEgiLCJmcmVzaCI6ZmFsc2UsInR5cGUiOiJhY2Nlc3MifQ.DhB8THp87no7x0BtGXk8pIk-ZJIuDmQyqK_ZGsB4FL4";
//		//www.SetRequestHeader("refresh_token", refresh_token);
//		//www.SetRequestHeader("access_token", access_token);
//		www.SetRequestHeader("Authorization", "Bearer " + access_token);

//		//www.uploadHandler = uploader;
//		www.downloadHandler = downloader;

//		yield return www.SendWebRequest();
//		if (www.isNetworkError || www.isHttpError)
//		{
//			Debug.Log(www.responseCode);
//			Debug.Log(www.error);
//			Debug.Log(www.downloadHandler.text);
//			//Debug.Log(www.uploadHandler.contentType);
//			//Debug.Log(Encoding.UTF8.GetString(www.uploadHandler.data));
//			foreach (var header in www.GetResponseHeaders())
//			{
//				Debug.Log(header.Key + ": " + header.Value);
//			}

//		}
//		else
//		{

//			// Or retrieve results as binary data
//			Debug.Log(www.downloadHandler.text);
//			Debug.Log(www.responseCode);
//		}
//	}
//	//IEnumerator SendGetRequest()
//	//{
//	//	UnityWebRequest www = UnityWebRequest.Get("http://165.22.131.242:5000/v1/user/");
//	//	yield return www.SendWebRequest();
//	//	if (www.isNetworkError || www.isHttpError)
//	//	{
//	//		Debug.Log(www.error);
//	//	}
//	//	else
//	//	{
//	//		// Show results as text
//	//		Debug.Log(www.downloadHandler.text);

//	//		// Or retrieve results as binary data
//	//		byte[] results = www.downloadHandler.data;
//	//		Debug.Log(www.downloadHandler.text);
//	//	}
//	//}
//}
//class JWTHelpter
//{
//	public void Decode(string token, string secret)
//	{
//		try
//		{
//			IJsonSerializer serializer = new JsonNetSerializer();
//			IDateTimeProvider provider = new UtcDateTimeProvider();
//			IJwtValidator validator = new JwtValidator(serializer, provider);
//			IBase64UrlEncoder urlEncoder = new JwtBase64UrlEncoder();
//			IJwtDecoder decoder = new JwtDecoder(serializer, validator, urlEncoder);

//			var json = decoder.Decode(token, secret, verify: true);
//			Debug.Log(json);
//		}
//		catch (TokenExpiredException)
//		{
//			Debug.Log("Token has expired");
//		}
//		catch (SignatureVerificationException)
//		{
//			Debug.Log("Token has invalid signature");
//		}
//	}
//}
