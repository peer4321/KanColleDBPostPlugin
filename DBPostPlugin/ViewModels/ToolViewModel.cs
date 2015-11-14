using DBPostPlugin.Models;
using Livet;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBPostPlugin.ViewModels
{
    public class ToolViewModel : ViewModel
    {
        private readonly DBPostPlugin _Plugin;

        public Proxy Proxy { get; }
        
        public ToolViewModel(DBPostPlugin plugin)
        {
            this._Plugin = plugin;
            this.Proxy = new Proxy(plugin);
        }
        
    }
}
