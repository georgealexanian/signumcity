public struct UserLoginPayload
{
	public string login { get; set; }
	public string password { get; set; }

	public UserLoginPayload(string login, string password)
	{
		this.login = login;
		this.password = password;
	}
}