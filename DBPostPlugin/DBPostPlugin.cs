using Grabacr07.KanColleViewer.Composition;
using System.ComponentModel.Composition;
using DBPostPlugin.Views;
using DBPostPlugin.ViewModels;
using System;

namespace DBPostPlugin
{
    [Export(typeof(IPlugin))]
    [Export(typeof(ITool))]
    [Export(typeof(IRequestNotify))]
    [ExportMetadata("Guid", "38022BEC-9578-4F4F-B8D7-A17B17CC57AD")]
    [ExportMetadata("Title", "KanColle DB Post")]
    [ExportMetadata("Description", "データをkancolle-db.netに送信します。")]
    [ExportMetadata("Version", "1.0.0")]
    [ExportMetadata("Author", "peer4321")]

    public class DBPostPlugin : IPlugin, ITool, IRequestNotify
    {
        private readonly ToolViewModel viewModel;

        public event EventHandler<NotifyEventArgs> NotifyRequested;
        public void InvokeNotifyRequested(NotifyEventArgs e) => this.NotifyRequested?.Invoke(this, e);

        public DBPostPlugin()
        {
            this.viewModel = new ToolViewModel(this);
        }

        public void Initialize() { }

        public string Name => "DBPost";

        public object View => new ToolView { DataContext = this.viewModel };
    }
}