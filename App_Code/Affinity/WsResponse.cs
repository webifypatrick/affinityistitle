namespace Affinity
{
	/// <summary>
	/// Struct for returning a response from a web service update
	/// </summary>
	public class WsResponse
	{
		public bool IsSuccess = false;
		public bool ActionWasTaken = false;
		public string Message = "Unknown";
		public bool NotificationSent = false;
		public string NotificationMessage = "";
	}

}

