using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ICMXamarin.Model
{
    public interface ISaveAndLoad
    {
        Task<string> Carregar(string path);
        Task Salvar(string path, string dados);
    }
}
