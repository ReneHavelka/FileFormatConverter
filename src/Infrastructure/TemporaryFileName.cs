namespace Infrastructure
{
	public static class TemporaryFileName
	{
		public static string TemporFileName()
		{
			//Meno a umiestnenie dočasného súboru.
			string currentFolder = Environment.GetFolderPath(Environment.SpecialFolder.CommonDocuments);
			string tempFileName = Path.Combine(currentFolder, "Temp.tmp");

			return tempFileName;
		}
	}
}
