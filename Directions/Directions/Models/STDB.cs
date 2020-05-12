using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace STDBns
{
    public partial class STDB
    {

        [PrimaryKey]
        public Int64? Sequence { get; set; }
        public String Language { get; set; }

        public String LeveLId { get; set; }

        public String LeveLIdSeq { get; set; }

        public String LeveLKey { get; set; }

        public String DetailType { get; set; }

        public String DetailFormat { get; set; }

        public String DetailVerse { get; set; }

        public String DetailDesc { get; set; }

        public String DetailText { get; set; }

    }
}
