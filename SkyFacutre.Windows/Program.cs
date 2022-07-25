using SkyFacture.Content;
using SkyFacture.IO;

namespace SkyFacture.Windows;

public class Program : ExecutorMain
{
	private static void Main(string[] args)
	{
		ResourceLoader = new AssemblyResourceLoader(typeof(ExecutorMain)); 
		ContentList.RegistryAll();

		Launch();
	}
}
