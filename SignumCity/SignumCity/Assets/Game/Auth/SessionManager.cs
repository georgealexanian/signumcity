using Newtonsoft.Json;
using System.Collections;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;
using System.Threading.Tasks;
using UnityEngine.UI;

public class SessionManager : MonoBehaviour
{
	public InputField logger;

	private string access_token;
	private string refresh_token;


	//register codes
	private const int USER_CREATED_SUCCES_CODE = 201;
	private const int USER_ALREADY_EXIST_CODE = 400;

	//other codes
	private const int INTERNAL_SERVER_ERROR_CODE = 500;

	[SerializeField]
	private string email;
	[SerializeField]
	private string phone;
	[SerializeField]
	private string password;

	private const string LOGIN_URL = "http://165.22.131.242:5000/v1/login";
	private UserLoginResponsePayload loginResponse = null;

	private const string REGISTER_URL = "http://165.22.131.242:5000/v1/register";
	private const string REGISTER_SOCIAL_URL = "http://165.22.131.242:5000/v1/login/social";


	private void Start()
	{
		//Login(email, password);
		//RegisterEmail(email, password);
		//RegisterPhone(phone, password);
		//RegisterFacebook();
		//RegisterTwitter();
		RegisterGoogle();
	}

	public void Login(string login, string password)
	{
		var user = new UserLoginPayload(login: login, password: password);
		StartCoroutine(LoginProcess(user));
	}

	private IEnumerator LoginProcess(UserLoginPayload user)
	{
		string jsonNewUser = JsonConvert.SerializeObject(user);
		Debug.Log(jsonNewUser);
		UnityWebRequest www = new UnityWebRequest(LOGIN_URL, HTTP_METHOD.POST);

		UploadHandler uploader = new UploadHandlerRaw(Encoding.UTF8.GetBytes(jsonNewUser));
		uploader.contentType = CONTENT_TYPE.JSON;

		DownloadHandlerBuffer downloader = new DownloadHandlerBuffer();

		www.uploadHandler = uploader;
		www.downloadHandler = downloader;

		yield return www.SendWebRequest();
		if (www.isNetworkError || www.isHttpError)
		{
			//TODO show error to player + handle error
		}
		else
		{
			Debug.Log(www.downloadHandler.text);
			loginResponse = JsonConvert.DeserializeObject<UserLoginResponsePayload>(www.downloadHandler.text);
			Debug.Log(loginResponse.credentials.access_token);//todo set session tokens and handle

			access_token = loginResponse.credentials.access_token;
			refresh_token = loginResponse.credentials.refresh_token;
		}
	}


	public void RegisterEmail(string email, string password)
	{
		StartCoroutine(
			RegisterProcess(
				new UserRegisterEmailPayload(email: email, password: password)
			)
		);
	}
	public void RegisterPhone(string phone, string password)
	{
		StartCoroutine(
			RegisterProcess(
				new UserRegisterPhonePayload(phone: phone, password: password)
			)
		);
	}

	public void RegisterFacebook()//todo rename
	{
		FacebookHelper.GetAccessToken(
			(SocialLoginToken token) => RegisterSocialProcessAsync(OAUTH_PROVIDER.FACEBOOK, token)
		);
	}
	
	public void RegisterTwitter()//todo rename
	{
		TwitterHelper.GetAccessToken(
			(SocialLoginToken token) => RegisterSocialProcessAsync(OAUTH_PROVIDER.TWITTER, token)
		);
	}

	public void RegisterGoogle()//todo rename
	{
		GoogleHelper.GetAccessToken((SocialLoginToken token)=>logger.text=token.token);
	}

	async void RegisterSocialProcessAsync(string oAuthProvedier, SocialLoginToken accessToken)
	{
		UnityWebRequest www = new UnityWebRequest(REGISTER_SOCIAL_URL, HTTP_METHOD.POST);

		var payload = new SocialLoadPayload();
		payload.provider = OAUTH_PROVIDER.FACEBOOK;
		payload.token = accessToken.token;
		payload.secret = accessToken.secret;
		var json = JsonConvert.SerializeObject(payload);

		UploadHandler uploader = new UploadHandlerRaw(Encoding.UTF8.GetBytes(json));
		uploader.contentType = CONTENT_TYPE.JSON;

		DownloadHandlerBuffer downloader = new DownloadHandlerBuffer();

		www.uploadHandler = uploader;
		www.downloadHandler = downloader;

		www.SendWebRequest();

		while (!www.isDone)
		{
			await Task.Delay(100);
		}
		if (www.isNetworkError || www.isHttpError)
		{
			Debug.Log(www.responseCode);
			Debug.Log(www.error);
			Debug.Log(www.downloadHandler.text);
			Debug.Log(www.uploadHandler.contentType);
			Debug.Log(Encoding.UTF8.GetString(www.uploadHandler.data));
			foreach (var header in www.GetResponseHeaders())
			{
				Debug.Log(header.Key + ": " + header.Value);
			}
			logger.text=www.downloadHandler.text;
			//TODO show error to player + handle error
		}
		else
		{
			// Or retrieve results as binary data
			byte[] results = www.downloadHandler.data;
			logger.text = www.downloadHandler.text;
			Debug.Log(www.downloadHandler.text);
			Debug.Log(www.responseCode);
		}
	}

	//IEnumerator RegisterSocialProcess(string socialToken)//todo refactor
	//{
	//	UnityWebRequest www = new UnityWebRequest("http://165.22.131.242:5000/v1/login/social", HTTP_METHOD.POST);

	//	var payload =new SocialRegisterPayload();
	//	payload.provider = "facebook";
	//	payload.token = socialToken;
	//	var json = JsonConvert.SerializeObject(payload);

	//	UploadHandler uploader = new UploadHandlerRaw(Encoding.UTF8.GetBytes(json));
	//	uploader.contentType = CONTENT_TYPE.JSON;

	//	DownloadHandlerBuffer downloader = new DownloadHandlerBuffer();

	//	www.uploadHandler = uploader;
	//	www.downloadHandler = downloader;

	//	yield return www.SendWebRequest();
	//	if (www.isNetworkError || www.isHttpError)
	//	{
	//		Debug.Log(www.responseCode);
	//		Debug.Log(www.error);
	//		Debug.Log(www.downloadHandler.text);
	//		Debug.Log(www.uploadHandler.contentType);
	//		Debug.Log(Encoding.UTF8.GetString(www.uploadHandler.data));
	//		foreach (var header in www.GetResponseHeaders())
	//		{
	//			Debug.Log(header.Key + ": " + header.Value);
	//		}
	//		logger.SetText(www.downloadHandler.text);
	//		//TODO show error to player + handle error
	//	}
	//	else
	//	{
	//		// Or retrieve results as binary data
	//		byte[] results = www.downloadHandler.data;
	//		logger.SetText(www.downloadHandler.text);
	//		Debug.Log(www.downloadHandler.text);
	//		Debug.Log(www.responseCode);
	//	}
	//}

	IEnumerator RegisterProcess(object newUser)
	{
		string jsonNewUser = JsonConvert.SerializeObject(newUser);
		UnityWebRequest www = new UnityWebRequest(REGISTER_URL, HTTP_METHOD.POST);

		UploadHandler uploader = new UploadHandlerRaw(Encoding.UTF8.GetBytes(jsonNewUser));
		uploader.contentType = CONTENT_TYPE.JSON;

		DownloadHandlerBuffer downloader = new DownloadHandlerBuffer();

		www.uploadHandler = uploader;
		www.downloadHandler = downloader;

		yield return www.SendWebRequest();
		if (www.isNetworkError || www.isHttpError)
		{
			Debug.Log(www.responseCode);
			Debug.Log(www.error);
			Debug.Log(www.downloadHandler.text);
			Debug.Log(www.uploadHandler.contentType);
			Debug.Log(Encoding.UTF8.GetString(www.uploadHandler.data));
			foreach (var header in www.GetResponseHeaders())
			{
				Debug.Log(header.Key + ": " + header.Value);
			}
			//TODO show error to player + handle error
		}
		else
		{
			// Or retrieve results as binary data
			byte[] results = www.downloadHandler.data;
			Debug.Log(www.downloadHandler.text);
			Debug.Log(www.responseCode);
		}
	}
}

public struct SocialLoadPayload
{
	public string provider { get; set; }
	public string token { get; set; }
	public string secret { get; set; }

}
public struct SocialLoginToken
{
	public string token { get; set; }
	public string secret { get; set; }
	public SocialLoginToken(string token, string secret)
	{
		this.token = token;
		this.secret = secret;
	}
}