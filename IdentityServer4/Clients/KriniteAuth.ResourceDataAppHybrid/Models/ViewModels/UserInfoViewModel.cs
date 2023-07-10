namespace KriniteAuth.ResourceDataAppHybrid.Models.ViewModels;

public class UserInfoViewModel
{
	public List<KeyValuePair<string, string>> UserInfo { get; init; } = null;

	public UserInfoViewModel(List<KeyValuePair<string, string>> userInfo)
	{
		UserInfo = userInfo;
	}
}
