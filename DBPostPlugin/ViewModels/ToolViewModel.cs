using DBPostPlugin.Models;
using Livet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBPostPlugin.ViewModels
{
    public class ToolViewModel : ViewModel
    {
        private readonly DBPostPlugin plugin;

        private readonly Proxy proxy;

        public ToolViewModel(DBPostPlugin plugin)
        {
            this.plugin = plugin;
            this.proxy = new Proxy(plugin);
        }
    }
}
