using System;

namespace SuperCroods.Data.Database
{
    public class DbProvider
    {
        private readonly string name;

        private DbProvider(string name)
        {
            this.name = name;
        }

        public readonly static DbProvider SqlServer = new DbProvider("SQLSERVER");
        public readonly static DbProvider SQLite = new DbProvider("SQLITE");
        public readonly static DbProvider MySQL = new DbProvider("MYSQL");
        public readonly static DbProvider Unknown = new DbProvider("UNKNOWN");

        public static DbProvider GetSetting(string arg)
        {
            if (arg.Equals("SqlServer", StringComparison.OrdinalIgnoreCase))
                return SqlServer;
            if (arg.Equals("SQLite", StringComparison.OrdinalIgnoreCase))
                return SQLite;
            if (arg.Equals("MySQL", StringComparison.OrdinalIgnoreCase))
                return MySQL;

            return Unknown;
        }

        public string Name => name;

        public override string ToString()
        {
            return name;
        }
    }
}
