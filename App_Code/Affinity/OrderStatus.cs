namespace Affinity
{
	/// <summary>
	/// Business Logic For OrderStatus Class
	/// </summary>
	public partial class OrderStatus
	{
		public static string DefaultCode = "New";
		public static string PendingCode = "Pending";
		public static string ChangedCode = "Changed";
		public static string ReadyCode = "Ready";
		public static string InProgressCode = "InProgress";
		public static string ClosedCode = "Closed";
	}
}