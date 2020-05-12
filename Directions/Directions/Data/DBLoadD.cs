using System.Collections.Generic;
using System.Threading.Tasks;
using SQLite;
using UNDBns;
using STDBns;
using BTDBns;
using BTWKns;
using ANWKns;
using System;

namespace DBLoadDns
{
    public class DBLoadD
    {
        readonly SQLiteAsyncConnection _database;

        public DBLoadD(string dbPath)
        {
            _database = new SQLiteAsyncConnection(dbPath);
        }

        /*      ******************   
                UNDB Datbase calls
         *      ****************** */
        // UNDB Methods for User Name (UNDB) to read User Name record
        // UNDB read by username
        public Task<List<UNDB>> UNDBGetAsyncUN(String username)
        {
            return _database.QueryAsync<UNDB>("SELECT * FROM [UNDB] WHERE UserName IS '" + username + "'");
        }
        // UNDB read by username array
        public Task<List<UNDB>> UNDBsGetAsync()
        {
            return _database.Table<UNDB>().ToListAsync();
        }
        // UNDB save one by by Id (key)
        public Task<int> UNDBSaveAsync(UNDB undb)
        {
            return _database.UpdateAsync(undb);
        }
        // UNDB end of UNDB IO calls
        /*      ******************   
               BTDB Datbase calls
        *      ****************** */

        public Task<List<BTDB>> BTDBsGetAsync(String wrkLanguage, String wrkBookName, int wrkChapterNo, int wrkVerseNoFrom, int wrkVerseNoTo)
        {
            return _database.QueryAsync<BTDB>
                ("SELECT * FROM [BTDB] WHERE Language IS '" + wrkLanguage + "' AND BookName IS '" + wrkBookName + "' AND ChapterNo=" 
                + wrkChapterNo + " AND VerseNo>=" + wrkVerseNoFrom + " AND VerseNo<=" + wrkVerseNoTo);
        }

        /*      ******************   
                STDBDatbase calls
         *      ****************** */
        // STDB Methods for User Name (STDB) to read structure record
        // STDB read by Language and LevelId array
        public Task<List<STDB>> STDBsGetAsync(String wrkLanguage, String wrkLeveLId)
        {
            return _database.QueryAsync<STDB>("SELECT * FROM [STDB] WHERE Language IS '" + wrkLanguage + "' AND LeveLId IS '" + wrkLeveLId + "'");
        }
        // STDB read by Language and LevelId only for Intro records array
        public Task<List<STDB>> STDBsGetIntroAsync(String wrkLanguage, String wrkLeveLId)
        {
            return _database.QueryAsync<STDB>("SELECT * FROM [STDB] WHERE  Language IS '" + wrkLanguage +
                "' AND LeveLId IS '" + wrkLeveLId + "' AND (LeveLIdSeq IS '0' OR LeveLIdSeq IS '1' OR LeveLIdSeq IS '2')");
        }
        // STDB read by Language and LevelId only for Intro records array
        public Task<List<STDB>> STDBsQueryAsyncDT(String wrkLang1st, String GlobalText1)
        {
            return _database.QueryAsync<STDB>("SELECT * FROM [STDB] WHERE Language IS '" + wrkLang1st + "' AND LeveLId IS '" + GlobalText1 + "'");
        }
        // STDB read by Language and LevelId only for Intro records array
        public Task<List<STDB>> STDBsQueryAsyncLS(String cmpLang1st, long cmpSequence)
        {
            return _database.QueryAsync<STDB>("SELECT * FROM [STDB] WHERE Language IS '" + cmpLang1st + "' AND Sequence=" + cmpSequence);
        }
        // STDB update the text with user answers by Id
        public Task<int> STDBUpdateAsync(STDB stdb)
        {
            return _database.UpdateAsync(stdb);
        }
        /*      ******************   
                 BTWK Datbase calls
         *      ****************** */
        // BTWK Methods for Bible Text work database (BTWK) to read structure record
        // BTWK Create table
        public void BTWKCreateTableAsync()
        {
            _database.CreateTableAsync<BTWK>();
        }
        // BTWK Drop Table
        public void BTWKDropTableAsync()
        {
            _database.DropTableAsync<BTWK>();
        }
        // BTWK Save (Insert) row
        public Task<List<BTWK>> BTWKsGetAsyncSI(int cmpStyleId)
        {
            return _database.QueryAsync<BTWK>("SELECT * FROM [BTWK] WHERE ButtonId =" + cmpStyleId);
        }
        public Task<int> BTWKSaveAsync(BTWK btwk)
        {
            return _database.InsertAsync(btwk);
        }
        // BTWK Delete all rows
        public void BTWKDeleteAsync()
        {
            _database.DeleteAsync<BTWK>("DELETE * FROM [BTWK]");
        }
        public void ANWKCreateTableAsync()
        {
            _database.CreateTableAsync<ANWK>();
        }
        public void ANWKDropTableAsync()
        {
            _database.DropTableAsync<ANWK>();
        }
        public Task<List<ANWK>> ANWKsGetAsyncSI(string cmpLang1st, int cmpentrySid)
        {
            return _database.QueryAsync<ANWK>("SELECT * FROM [ANWK] WHERE Language IS '" + cmpLang1st + "' AND StyleId IS " + cmpentrySid + ""); 
        }
        public Task<int> ANWKSaveAsync(ANWK anwk)
        {
            Console.WriteLine("DBLc-0060-50-ANWK-SA");
            return _database.InsertAsync(anwk);
        }
        public void ANWKDeleteAsync()
        {
            _database.DeleteAsync<ANWK>("DELETE * FROM [ANWK]");
        }
    }
}
