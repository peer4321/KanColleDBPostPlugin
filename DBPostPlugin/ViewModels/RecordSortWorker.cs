using DBPostPlugin.Models;
using Grabacr07.KanColleWrapper;
using Livet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBPostPlugin.ViewModels
{
    public class RecordSortWorker : ViewModel
    {
        public static readonly SortableColumn NoneColumn = new SortableColumn {
            Name = "なし", KeySelector = null };
        public static readonly SortableColumn TimestampColumn = new SortableColumn {
            Name = "日付", DefaultIsDescending = true, KeySelector = x => x.Timestamp.Ticks };
        public static readonly SortableColumn TypeColumn = new SortableColumn {
            Name = "種類", KeySelector = x => (long)x.Api };

        public static SortableColumn[] Columns { get; set; }
        static RecordSortWorker()
        {
            Columns = new[]
            {
                NoneColumn,
                TimestampColumn,
                TypeColumn,
            };
        }

        private SortableColumnSelector[] _Selectors;
        public SortableColumnSelector[] Selectors
        {
            get { return this._Selectors; }
            set
            {
                if (this._Selectors != value)
                {
                    this._Selectors = value;
                    this.RaisePropertyChanged();
                }
            }
        }

        public RecordSortWorker()
        {
            this.UpdateSelectors();
            this.SetFirst(TimestampColumn);
        }

        public void SetFirst(SortableColumn column)
        {
            if (!this.Selectors.HasItems()) return;

            if (this.Selectors[0].Current == column)
            {
                this.Selectors[0].SafeUpdate(this.Selectors[0].IsDescending);
            }
            else
            {
                this.Selectors[0].SafeUpdate(column);
                this.Selectors[0].SafeUpdate(!column.DefaultIsDescending);
            }

            this.UpdateSelectors();
        }

        public void Clear()
        {
            this.Selectors = null;
            this.UpdateSelectors();
        }

        public IEnumerable<Record> Sort(IEnumerable<Record> records)
        {
            var selectors = this.Selectors.Where(x => x.Current.KeySelector != null).ToArray();
            if (selectors.Length == 0) return records;

            var selector = selectors[0].Current.KeySelector;
            var orderedRows = selectors[0].IsAscending ? 
                records.OrderBy(selector) : records.OrderByDescending(selector);

            for (var i = 1; i < selectors.Length; i++)
            {
                selector = selectors[i].Current.KeySelector;
                orderedRows = selectors[i].IsAscending ? 
                    orderedRows.ThenBy(selector) : orderedRows.ThenByDescending(selector);
            }

            return orderedRows;
        }

        private int selectorNum = 1;

        private void UpdateSelectors(SortableColumnSelector target = null)
        {
            if (this.Selectors == null)
            {
                this.Selectors = Enumerable.Range(0, selectorNum)
                    .Select(_ => new SortableColumnSelector { Updated = x => this.UpdateSelectors(x) })
                    .ToArray();
            }

            var selectedItems = new HashSet<SortableColumn>();

            var enabled = target == null;

            foreach (var selector in this.Selectors)
            {
                if (enabled)
                {
                    var sortables = Columns.Where(x => !selectedItems.Contains(x)).ToList();
                    var current = selector.Current;

                    selector.SelectableColumns = sortables.ToArray();
                    selector.SafeUpdate(sortables.Contains(current) ? current : sortables.FirstOrDefault());
                }
                else
                {
                    enabled = selector == target;
                }

                if (selector.Current != NoneColumn)
                {
                    selectedItems.Add(selector.Current);
                }
            }
        }

        public class SortableColumnSelector : ViewModel
        {
            internal Action<SortableColumnSelector> Updated { get; set; }

            private SortableColumn _Current;
            public SortableColumn Current
            {
                get { return this._Current; }
                set
                {
                    if (this._Current != value)
                    {
                        this._Current = value;
                        this.SafeUpdate(!value.DefaultIsDescending);
                        this.Updated(this);
                        this.RaisePropertyChanged();
                    }
                }
            }

            private SortableColumn[] _SelectableColumns;
            public SortableColumn[] SelectableColumns
            {
                get { return this._SelectableColumns; }
                set
                {
                    if (this._SelectableColumns != value)
                    {
                        this._SelectableColumns = value;
                        this.RaisePropertyChanged();
                    }
                }
            }

            private bool _IsAscending = true;
            public bool IsAscending
            {
                get { return this._IsAscending; }
                set
                {
                    if (this._IsAscending != value)
                    {
                        this._IsAscending = value;
                        this.Updated(this);
                        this.RaisePropertyChanged();
                        this.RaisePropertyChanged(nameof(this.IsAscending));
                    }
                }
            }

            public bool IsDescending => !this.IsAscending;

            internal void SafeUpdate(SortableColumn column)
            {
                this._Current = column;
                this.RaisePropertyChanged(nameof(this.Current));
            }

            internal void SafeUpdate(bool isAscending)
            {
                this._IsAscending = isAscending;
                this.RaisePropertyChanged(nameof(this.IsAscending));
                this.RaisePropertyChanged(nameof(this.IsDescending));
            }
        }

        public class SortableColumn
        {
            public string Name { get; set; }
            public bool DefaultIsDescending { get; set; }
            public Func<Record, long> KeySelector { get; set; }
        }
    }
}
