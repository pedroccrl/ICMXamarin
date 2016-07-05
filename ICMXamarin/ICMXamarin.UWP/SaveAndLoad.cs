using ICMXamarin.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;

namespace ICMXamarin.UWP
{
    class SaveAndLoad : ISaveAndLoad
    {
        public async Task<string> Carregar(string path)
        {
            try
            {
                var _Folder = Windows.ApplicationModel.Package.Current.InstalledLocation;
                _Folder = await _Folder.GetFolderAsync("Assets");

                // acquire file
                var _File = await _Folder.GetFileAsync("futebol.jpg");
                
                // read content
                var _ReadThis = await Windows.Storage.FileIO.ReadTextAsync(_File);

                return _ReadThis;
            }
            catch (Exception e)
            {
                return string.Empty;
            }

        }

        public async Task Salvar(string path, string dados)
        {
            StorageFolder storageFolder = Windows.Storage.ApplicationData.Current.LocalFolder;
            Windows.Storage.StorageFile sampleFile = await storageFolder.CreateFileAsync(path, Windows.Storage.CreationCollisionOption.ReplaceExisting);

            await Windows.Storage.FileIO.WriteTextAsync(sampleFile, dados);
        }
    }
}
