
using System.IO;
using Xamarin.Forms;
// modify for your namespace ///
[assembly: Dependency(typeof(XamBookDatabase.Droid.FileHelper))]
namespace XamBookDatabase.Droid
{
    public class FileHelper : IFileHelper
    {
        public string GetLocalFilePath(string filename)
        {
            string path =System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
            return Path.Combine(path, filename);
        }
    }
}