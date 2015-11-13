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
        private readonly DBPostPlugin plugin;

        private readonly Proxy proxy;

        private ObservableCollection<RecordViewModel> _Records;
        public ObservableCollection<RecordViewModel> Records
        {
            get { return this._Records; }
            set
            {
                if (this._Records != value)
                {
                    this._Records = value;
                    this.RaisePropertyChanged();
                }
            }
        }

        public RecordSortWorker SortWorker { get; }

        public ToolViewModel(DBPostPlugin plugin)
        {
            this.plugin = plugin;
            this.proxy = new Proxy(plugin);
            this.SortWorker = new RecordSortWorker();
            this.Records = new ObservableCollection<RecordViewModel>
            {
                new RecordViewModel(1, new Record(DateTime.Now, Api.api_port_port, "母港の色々")),
                new RecordViewModel(2, new Record(DateTime.Now, Api.api_get_member_mapinfo, "Map info")),
            };
        }

        public void Update()
        {
            var list = Records.Select(kvp => kvp.Record);
            this.Records = new ObservableCollection<RecordViewModel>(
                this.SortWorker.Sort(list).Select((x, i) => new RecordViewModel(i + 1, x)).ToList());
        }

        public void SortRecord(SortableColumn column)
        {
            this.SortWorker.SetFirst(column);
            this.Update();
        }
    }
}
