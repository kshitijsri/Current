using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TranslatorService.Example
{
    public class Button
    {
        [SQLite.AutoIncrement, SQLite.PrimaryKey]
        public int ID { get; set; }

        public int Order { get; set; }

        public int ColSpan { get; set; }

        public int RowSpan { get; set; }

        [SQLite.MaxLength(50)]
        public string Text { get; set; }
    }
}
