namespace Affinity.ExportRenderer
{
	public interface IBaseRenderer
	{
		string GetFileName();
		string GetMimeType();
		string Render(string keyAttribute);
	}
}