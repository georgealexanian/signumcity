public struct UserRegisterEmailPayload
{
	public string email { get; set; }
	public string password { get; set; }

	public UserRegisterEmailPayload(string email, string password)
	{
		this.email = email;
		this.password = password;
	}
}
