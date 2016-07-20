using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ICMXamarin.UWP
{
    public class Fala
    {
        public event EventHandler<FalaEventArgs> ProntoParaFalar;

        public void Falar(string texto)
        {
            ProntoParaFalar?.Invoke(this, new FalaEventArgs { Mensagem = texto });
        }
    }

    public class FalaEventArgs : EventArgs
    {
        public string Mensagem { get; set; }
    }
}
