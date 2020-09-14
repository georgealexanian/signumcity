using Google;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public static class GoogleHelper
{
	private const string webClientId = "1057592789375-mgd1nb6m6k2a71qlf3b29q7n79vvsi4n.apps.googleusercontent.com";

	private static GoogleSignInConfiguration configuration;

	private static void Init()
	{
		configuration = new GoogleSignInConfiguration
		{
			WebClientId = webClientId,
			RequestIdToken = true
		}; ;
	}

	public static void GetAccessToken(Action<SocialLoginToken> tokenHandler)
	{
		if (configuration == null) Init();
		GoogleSignIn.Configuration = configuration;
		GoogleSignIn.Configuration.UseGameSignIn = false;
		GoogleSignIn.Configuration.RequestIdToken = true;
		Debug.Log("GetAccessToken");
		GoogleSignIn.DefaultInstance.SignIn().ContinueWith(
		  (task) => OnAuthenticationFinished(task, tokenHandler)
		);
	}

	private static void OnAuthenticationFinished(Task<GoogleSignInUser> task, Action<SocialLoginToken> tokenHandler)
	{
		Debug.Log("!!!!111111111!!!");
		Debug.Log("GetAccessTokenFinish");
		if(task.Exception!=null) Debug.Log("Exception: " + task.Exception.Message);
		Debug.Log(task.Exception.Message);
		Debug.Log("!!!!111111111!!!");
		//Debug.Log(task.Exception.Message);
		string token;
		if (task.IsFaulted)
		{
			using (IEnumerator<Exception> enumerator =
					task.Exception.InnerExceptions.GetEnumerator())
			{
				if (enumerator.MoveNext())
				{
					GoogleSignIn.SignInException error =
							(GoogleSignIn.SignInException)enumerator.Current;
					token=("Got Error: " + error.Status + " " + error.Message);
				}
				else
				{
					token = ("Got Unexpected Exception?!?" + task.Exception);
				}
			}
		}
		else if (task.IsCanceled)
		{
			token = ("Canceled");
		}
		else
		{
			token = ("Welcome: " + task.Result.DisplayName + "!");
		}
		Debug.Log(token);
		tokenHandler(new SocialLoginToken(token, null));
	}
}
