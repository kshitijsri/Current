﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TranslatorService.Example
{
    /*
     * Database to store each button for a given page.
     */
    public class Button
    {
        [SQLite.AutoIncrement, SQLite.PrimaryKey]
        public int ID { get; set; }

        /* Order on the page */
        public int Order { get; set; }

        /* Horizontal width */
        public int ColSpan { get; set; }

        /* Vertical height */
        public int RowSpan { get; set; }

        /* Text on the button */
        [SQLite.MaxLength(50)]
        public string Text { get; set; }

        /* Hexadecimal value of the button's color */
        public string ColorHex { get; set; }
    }
}
