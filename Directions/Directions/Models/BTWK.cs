using SQLite;
using System;
namespace BTWKns
{
    public partial class BTWK
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public int ButtonId { get; set; }

        public String BookName { get; set; }

        public Int64? ChapterNo { get; set; }

        public Int64? VerseNoFrom { get; set; }

        public Int64? VerseNoTo { get; set; }

    }
}
