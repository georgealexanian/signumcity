using System;
using TwitterKit.Unity;
using UnityEngine;

public static class TwitterHelper
{
	private static bool isInitialized=false;
	public static void GetAccessToken(Action<SocialLoginToken> tokenHandler)//todo add on hide action
	{
		if (!isInitialized)
		{
			Twitter.Init();
			isInitialized = true;
		}
		StartLogin(tokenHandler);

	}
	public static void StartLogin(Action<SocialLoginToken> tokenHandler)
	{
		TwitterSession session = Twitter.Session;
		if (session == null)
		{
			Twitter.LogIn(
				(newSession) => tokenHandler(new SocialLoginToken(token: newSession.authToken.token, secret: newSession.authToken.secret)),
				(error)=> LoginFailure(error, tokenHandler));
		}
		else
		{
			tokenHandler(
				new SocialLoginToken(token: session.authToken.token, secret: session.authToken.secret)
				);
			Debug.Log(session.authToken.token);
			Debug.Log(session.authToken.secret);
			Debug.Log(session.authToken.ToString());
		}
	}

	public static void LoginFailure(ApiError error, Action<SocialLoginToken> tokenHandler)
	{
		Debug.LogWarning("code=" + error.code + " msg=" + error.message);
		tokenHandler(new SocialLoginToken(null,null));
	}
}
