using SQLite;
using System;
using System.IO;
using System.Collections.Generic;
using System.Text;

namespace Directions
{
    public interface ISQLite
    {
        SQLiteConnection GetConnection();
    }
 }
