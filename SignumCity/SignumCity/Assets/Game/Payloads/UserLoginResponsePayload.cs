using System;

public class UserLoginResponsePayload
{
	public string message { get; set; }
	public string user_id { get; set; }
	public LoginResponseCredintials credentials { get; set; }
}

public class LoginResponseCredintials
{
	public string access_token { get; set; }
	public string refresh_token { get; set; }
}