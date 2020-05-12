using SQLite;
using System;

namespace UNDBns
{
    public class UNDB
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public String UserName { get; set; }

        public String Lang1st { get; set; }

        public String Lang2nd { get; set; }

        public String LastPage { get; set; }

        public String FontType { get; set; }

        public String ColorText { get; set; }

        public String ColorHex { get; set; }

        public Int64? FontSize { get; set; }
    }
}
