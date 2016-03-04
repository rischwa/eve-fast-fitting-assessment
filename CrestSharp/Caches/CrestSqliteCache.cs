using System;
using System.Data;
using System.Data.SQLite;
using System.IO;
using System.IO.Compression;
using System.Threading.Tasks;
using CrestSharp.Implementation;
using CrestSharp.Infrastructure;
using CrestSharp.Model;
using Newtonsoft.Json;
using Serilog;

namespace CrestSharp.Caches
{
    public class CrestSqliteCache : ICrestCache
    {
        private readonly ILogger _logger = Log.Logger.ForContext(typeof (CrestSqliteCache));

        private static readonly string DATABASE_PATH = Path.Combine(
                                                                    Environment.GetEnvironmentVariable("APPDATA"),
                                                                    "CrestSharp",
                                                                    "SqliteCache");

        private static SQLiteConnection _sqliteConnectionRead;
        private static SQLiteConnection _sqliteConnectionWrite;
        private static readonly SQLiteCommand SQL_READ_COMMAND = new SQLiteCommand("SELECT json FROM jsonByHref WHERE href = @href");
        private static readonly SQLiteParameter SQL_READ_PARAMETER = SQL_READ_COMMAND.Parameters.Add("@href", DbType.String);

        private static readonly SQLiteCommand SQL_WRITE_COMMAND =
            new SQLiteCommand("INSERT OR REPLACE INTO jsonByHref (href, json) VALUES(@href, @json)");

        private static readonly SQLiteParameter SQL_WRITE_HREF_PARAMETER = SQL_WRITE_COMMAND.Parameters.Add("@href", DbType.String);
        private static readonly SQLiteParameter SQL_WRITE_JSON_PARAMETER = SQL_WRITE_COMMAND.Parameters.Add("@json", DbType.Binary);

        private static readonly string DATABASE_FILE = Path.Combine(DATABASE_PATH, "crest_cache.sqlite");

        public CrestSqliteCache()
        {
            CreateDatabaseIfNotExists();
            InitConnections();
        }

        public async Task<T> Get<T>(string href) where T : class, ICrestObject
        {
            return await Task.Factory.StartNew(
                                               () =>
                                               {
                                                   _logger.Debug($"Get {href}");
                                                   ICrestObject instance;
                                                   if (!CrestCacheHelper.TryCreateObject(href, out instance))
                                                   {
                                                       _logger.Debug($"Type not cached by {nameof(CrestSqliteCache)}: {href}");
                                                       return null;
                                                   }

                                                   string document;

                                                   lock (_sqliteConnectionRead)
                                                   {
                                                       SQL_READ_PARAMETER.Value = new Uri(href).PathAndQuery;
                                                       using (var reader = SQL_READ_COMMAND.ExecuteReader())
                                                       {
                                                           if (!reader.Read())
                                                           {
                                                               _logger.Debug($"Cache miss: {href}");
                                                               return null;
                                                           }
                                                           var stream = reader.GetStream(0);

                                                           using (var gzipStream = new GZipStream(stream, CompressionMode.Decompress))
                                                           {
                                                               using (var streamReader = new StreamReader(gzipStream))
                                                               {
                                                                   document = streamReader.ReadToEnd();
                                                               }
                                                           }
                                                       }
                                                   }

                                                   _logger.Debug($"Cache hit {href}");

                                                   JsonConvert.PopulateObject(document, instance, CrestCacheHelper.SETTINGS);
                                                   return instance as T;
                                               }).ConfigureAwait(false);
        }

        public async Task Put(ICrestObject value)
        {
            _logger.Debug($"Put {value.Href}");
            await Task.Factory.StartNew(
                                        () =>
                                        {
                                            if (string.IsNullOrEmpty(value.Href))
                                            {
                                                return;
                                            }

                                            var init = value as IIsInitializable;
                                            if (init != null && !init.IsInitialized)
                                            {
                                                _logger.Debug($"Not cached, because object is not initialized {value.Href}");
                                                return;
                                            }
                                            var json = JsonConvert.SerializeObject(value, CrestCacheHelper.SETTINGS);
                                            lock (_sqliteConnectionWrite)
                                            {
                                                InitWriteCommand(value.Href, json);
                                                SQL_WRITE_COMMAND.ExecuteNonQuery();
                                            }
                                        }).ConfigureAwait(false);
        }

        public void ClearCache()
        {
            _logger.Information($"Clearing cache");
            using (var connection = new SQLiteConnection($"Data Source={DATABASE_FILE}"))
            {
                connection.Open();
                const string DELETE_DATA = "DELETE FROM jsonByHref";
                var command = new SQLiteCommand(DELETE_DATA, connection);
                command.ExecuteNonQuery();
            }
        }

        private static void InitWriteCommand(string href, string value)
        {
            SQL_WRITE_HREF_PARAMETER.Value = new Uri(href).PathAndQuery;
            SetJson(value);
        }

        private static void SetJson(string json)
        {
            using (var memoryStream = new MemoryStream())
            {
                using (var gzipStream = new GZipStream(memoryStream, CompressionMode.Compress))
                {
                    using (var writer = new StreamWriter(gzipStream))
                    {
                        writer.Write(json);
                    }
                }
                var array = memoryStream.ToArray();
                SQL_WRITE_JSON_PARAMETER.Value = array;
            }
        }

        private void CreateDatabaseIfNotExists()
        {
            try
            {
                if (File.Exists(DATABASE_FILE))
                {
                    return;
                }

                if (!Directory.Exists(DATABASE_PATH))
                {
                    Directory.CreateDirectory(DATABASE_PATH);
                }

                SQLiteConnection.CreateFile(DATABASE_FILE);

                var sqlConnection = new SQLiteConnection($"Data Source={DATABASE_FILE}");
                sqlConnection.Open();

                CreateTable(sqlConnection);
                CreateIndex(sqlConnection);
            }
            catch (Exception e)
            {
                Log.Logger.Error(e, "Error during creation of sqlite cache");
            }
        }

        private static void InitConnections()
        {
            _sqliteConnectionWrite = new SQLiteConnection($"Data Source={DATABASE_FILE}");
            _sqliteConnectionWrite.Open();

            _sqliteConnectionRead = new SQLiteConnection($"Data Source={DATABASE_FILE}");
            _sqliteConnectionRead.Open();

            SQL_WRITE_COMMAND.Connection = _sqliteConnectionWrite;
            SQL_READ_COMMAND.Connection = _sqliteConnectionRead;
        }

        private static void CreateTable(SQLiteConnection connection)
        {
            const string CREAT_TABLE_SQL = "CREATE TABLE jsonByHref(href STRING PRIMARY KEY, json BLOB)";
            var command = new SQLiteCommand(CREAT_TABLE_SQL, connection);
            command.ExecuteNonQuery();
        }

        private static void CreateIndex(SQLiteConnection connection)
        {
            const string CREAT_INDEX_SQL = "CREATE UNIQUE INDEX idx_pk_jsonByHref ON jsonByHref(href)";
            var indexCommand = new SQLiteCommand(CREAT_INDEX_SQL, connection);
            indexCommand.ExecuteNonQuery();
        }
    }
}
