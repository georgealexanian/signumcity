using Facebook.Unity;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Networking;
using System.Text;
using Newtonsoft.Json;

//https://developers.facebook.com/apps/2198179500495532
public static class FacebookHelper
{
	const string FACEBOOK_APP_ID = "2198179500495532";

	public static void GetAccessToken(Action<SocialLoginToken> tokenHandler)//todo add on hide action
	{
		if (!FB.IsInitialized) { FB.Init(() => CallFBLogin(tokenHandler), OnHideUnity); }
		else { CallFBLogin(tokenHandler); }
	}

	private static void OnHideUnity(bool isUnityShown)
	{
		throw new NotImplementedException();//todo implement
	}

	private static void CallFBLogin(Action<SocialLoginToken> tokenHandler)
	{
		try
		{
			FB.LogInWithReadPermissions(new List<string>() { "public_profile", "email", "user_friends" }, (ILoginResult result) => { tokenHandler(new SocialLoginToken(result.AccessToken.TokenString, null)); });
		}
		catch (NullReferenceException e)
		{
			Debug.LogWarning(e.Message);
			tokenHandler(new SocialLoginToken(null,null));
		}
	}
}
