public struct UserRegisterPhonePayload
{
	public string phone { get; set; }
	public string password { get; set; }

	public UserRegisterPhonePayload(string phone, string password)
	{
		this.phone = phone;
		this.password = password;
	}
}