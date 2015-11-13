using DBPostPlugin.Models;
using Livet;
using System;

namespace DBPostPlugin.ViewModels
{
    public class RecordViewModel : ViewModel
    {
        public int Index { get; }

        public Record Record { get; }

        public RecordViewModel (int index, Record record)
        {
            this.Index = index;
            this.Record = record;
        }
    }
}
