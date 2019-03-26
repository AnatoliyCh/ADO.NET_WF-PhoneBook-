using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data;
using System.Data.Odbc;
using System.Diagnostics;

using PhoneBook.Domain.Core;
using PhoneBook.Domain.Interfaces;

namespace PhoneBook.Infrastructure.Data
{
    public class RecordLineRepository : IRecordLineRepository
    {
        //подключение к БД через драйвер ODBC
        private const string DSN = "PostgreSQL35W";//data source
        private const string UID = "postgres";//user name
        private const string PWD = "AVT515";//password
        private string connectionString;
        private OdbcConnection connection;//соединение с источником данных

        private static RecordLineRepository repository = new RecordLineRepository();

        private RecordLineRepository()
        {
            connectionString =
                "DSN=" + DSN + ";" +
                "UID=" + UID + ";" +
                "PWD=" + PWD;
            if (DataBaseConnection())
            {
                Debug.WriteLine("все верно!");
                CreateTable();
            }
            else
            {
                Debug.WriteLine("Ошибка соединения");
            }
        }
        public static RecordLineRepository GetRepository()
        {
            return repository;
        }
        //CRUD
        public void Creating(RecordLine _cellBook)
        {
            using (connection = new OdbcConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string query = "INSERT INTO PhoneBookTable(LastName, FirstName, MidleName, Phone, Email) " +
                        "VALUES(?, ?, ?, ?, ?)";
                    using (OdbcCommand creatingCellBook = new OdbcCommand(query, connection))
                    {
                        creatingCellBook.Parameters.Add("LastName", OdbcType.VarChar).Value = _cellBook.LastName;
                        creatingCellBook.Parameters.Add("FirstName", OdbcType.VarChar).Value = _cellBook.FirstName;
                        creatingCellBook.Parameters.Add("MidleName", OdbcType.VarChar).Value = _cellBook.MiddleName;
                        creatingCellBook.Parameters.Add("Phone", OdbcType.VarChar).Value = _cellBook.Phone;
                        creatingCellBook.Parameters.Add("Email", OdbcType.VarChar).Value = _cellBook.Email;
                        try { creatingCellBook.ExecuteNonQuery(); }
                        catch (OdbcException ex)
                        {
                            Debug.WriteLine(ex.Message + "\n\n" + "StackTrace[Creating() => ExecuteNonQuery()]: \n\n" + ex.StackTrace);
                            return;
                        }
                    }
                }
                catch (OdbcException ex)
                {
                    Debug.WriteLine(ex.Message + "\n\n" + "StackTrace[Creating() => connection]: \n\n" + ex.StackTrace);
                }
            }
        }
        public RecordLine ReadingById(int _id)
        {
            using (connection = new OdbcConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string query = "SELECT * FROM phonebooktable WHERE id = ?";
                    using (OdbcCommand readingCellBook = new OdbcCommand(query, connection))
                    {
                        readingCellBook.Parameters.Add("id", OdbcType.Int).Value = _id;
                        try
                        {
                            RecordLine tmpCellBook = new RecordLine();
                            OdbcDataReader reader = readingCellBook.ExecuteReader(CommandBehavior.CloseConnection);
                            reader.Read();
                            tmpCellBook.Id = reader.GetString(0);
                            tmpCellBook.LastName = reader.GetString(1);
                            tmpCellBook.FirstName = reader.GetString(2);
                            tmpCellBook.MiddleName = reader.GetString(3);
                            tmpCellBook.Phone = reader.GetString(4);
                            tmpCellBook.Email = reader.GetString(5);
                            return tmpCellBook;
                        }
                        catch (OdbcException ex)
                        {
                            Debug.WriteLine(ex.Message + "\n\n" + "StackTrace[ReadingById() => ExecuteReader]: \n\n" + ex.StackTrace);
                            return null;
                        }
                    }
                }
                catch (OdbcException ex)
                {
                    Debug.WriteLine(ex.Message + "\n\n" + "StackTrac" +
                        "e[ReadingById() => connection]: \n\n" + ex.StackTrace);
                    return null;
                }
            }
        }
        public IEnumerable<RecordLine> ReadingAll()
        {
            using (connection = new OdbcConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string query = "SELECT * FROM phonebooktable ORDER BY id";
                    using (OdbcCommand readingCellBook = new OdbcCommand(query, connection))
                    {
                        try
                        {
                            List<RecordLine> cellsBook = new List<RecordLine>();
                            RecordLine tmp;
                            OdbcDataReader reader = readingCellBook.ExecuteReader(CommandBehavior.CloseConnection);
                            while (reader.Read() == true)
                            {
                                tmp = new RecordLine();
                                tmp.Id = reader.GetString(0);
                                tmp.LastName = reader.GetString(1);
                                tmp.FirstName = reader.GetString(2);
                                tmp.MiddleName = reader.GetString(3);
                                tmp.Phone = reader.GetString(4);
                                tmp.Email = reader.GetString(5);
                                cellsBook.Add(tmp);
                            }
                            return cellsBook;
                        }
                        catch (OdbcException ex)
                        {
                            Debug.WriteLine(ex.Message + "\n\n" + "StackTrace[ReadingAll() => ExecuteReader()]: \n\n" + ex.StackTrace);
                            return null;
                        }
                    }
                }
                catch (OdbcException ex)
                {
                    Debug.WriteLine(ex.Message + "\n\n" + "StackTrace[ReadingAll() => connection]: \n\n" + ex.StackTrace);
                    return null;
                }
            }
        }
        public void Updating(RecordLine _cellBook)
        {
            using (connection = new OdbcConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string query = "UPDATE PhoneBookTable SET (LastName, FirstName, MidleName, Phone, Email) = " +
                        "(?, ?, ?, ?, ?) WHERE id = ?";
                    using (OdbcCommand updatingCellBook = new OdbcCommand(query, connection))
                    {
                        updatingCellBook.Parameters.Add("LastName", OdbcType.VarChar).Value = _cellBook.LastName;
                        updatingCellBook.Parameters.Add("FirstName", OdbcType.VarChar).Value = _cellBook.FirstName;
                        updatingCellBook.Parameters.Add("MidleName", OdbcType.VarChar).Value = _cellBook.MiddleName;
                        updatingCellBook.Parameters.Add("Phone", OdbcType.VarChar).Value = _cellBook.Phone;
                        updatingCellBook.Parameters.Add("Email", OdbcType.VarChar).Value = _cellBook.Email;
                        updatingCellBook.Parameters.Add("id", OdbcType.Int).Value = _cellBook.Id;
                        try { updatingCellBook.ExecuteNonQuery(); }
                        catch (OdbcException ex)
                        {
                            Debug.WriteLine(ex.Message + "\n\n" + "StackTrace[Updating() => ExecuteNonQuery()]: \n\n" + ex.StackTrace);
                            return;
                        }
                    }
                }
                catch (OdbcException ex)
                {
                    Debug.WriteLine(ex.Message + "\n\n" + "StackTrac" +
                        "e[Updating() => connection]: \n\n" + ex.StackTrace);
                }
            }
        }
        public void Deleting(int _id)
        {
            using (connection = new OdbcConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string query = "DELETE FROM phonebooktable WHERE id = ?";
                    using (OdbcCommand deletingCellBook = new OdbcCommand(query, connection))
                    {
                        deletingCellBook.Parameters.Add("id", OdbcType.Int).Value = _id;
                        try { deletingCellBook.ExecuteNonQuery(); }
                        catch (OdbcException ex)
                        {
                            Debug.WriteLine(ex.Message + "\n\n" + "StackTrace[Deleting() => ExecuteNonQuery()]: \n\n" + ex.StackTrace);
                            return;
                        }
                    }
                }
                catch (OdbcException ex)
                {
                    Debug.WriteLine(ex.Message + "\n\n" + "StackTrace[Deleting() => connection]: \n\n" + ex.StackTrace);
                }
            }
        }

        public IEnumerable<int> GetAllId()
        {
            using (connection = new OdbcConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string query = "SELECT ID FROM phonebooktable ORDER BY id";
                    using (OdbcCommand readingCellBook = new OdbcCommand(query, connection))
                    {
                        try
                        {
                            List<int> ids = new List<int>();
                            OdbcDataReader reader = readingCellBook.ExecuteReader(CommandBehavior.CloseConnection);
                            while (reader.Read() == true)
                                ids.Add(reader.GetInt32(0));
                            return ids;
                        }
                        catch (OdbcException ex)
                        {
                            Debug.WriteLine(ex.Message + "\n\n" + "StackTrace[GetAllId() => ExecuteReader()]: \n\n" + ex.StackTrace);
                            return null;
                        }
                    }
                }
                catch (OdbcException ex)
                {
                    Debug.WriteLine(ex.Message + "\n\n" + "StackTrace[GetAllId() => connection]: \n\n" + ex.StackTrace);
                    return null;
                }
            }
        }
        //проверка соед. с БД
        private bool DataBaseConnection()
        {
            using (connection = new OdbcConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    Debug.WriteLine("Соединение : " + connection.State.ToString());
                    return true;
                }
                catch (OdbcException ex)
                {
                    Debug.WriteLine(ex.Message + "\n\n" + "StackTrace: \n\n" + ex.StackTrace);
                    return false;
                }
            }
        }
        //создание таблицы
        private void CreateTable()
        {
            string query = "SELECT COUNT(*) FROM information_schema.TABLES WHERE  TABLE_NAME ='phonebooktable'";
            using (connection = new OdbcConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    using (OdbcCommand checkTable = new OdbcCommand(query, connection))
                    {
                        try
                        {
                            //проверка на наличие таблицы
                            //если нет, то создаём
                            if (checkTable.ExecuteScalar().ToString() == "0")
                            {
                                string queryCreateTable =
                                    "CREATE TABLE PhoneBookTable " +
                                    "(Id SERIAL PRIMARY KEY," +
                                    "LastName CHARACTER VARYING(15)," +
                                    "FirstName CHARACTER VARYING(15)," +
                                    "MidleName CHARACTER VARYING(15)," +
                                    "Phone CHARACTER VARYING(30) UNIQUE NOT NULL," +
                                    "Email CHARACTER VARYING(30) UNIQUE);";
                                using (OdbcCommand createTable = new OdbcCommand(queryCreateTable, connection))
                                {
                                    try
                                    {
                                        createTable.ExecuteNonQuery();
                                    }
                                    catch (OdbcException ex)
                                    {
                                        Debug.WriteLine(ex.Message + "\n\n" + "StackTrace[CreateTable() => createTable]: \n\n" + ex.StackTrace);
                                        return;
                                    }
                                }
                            }
                        }
                        catch (OdbcException ex)
                        {
                            Debug.WriteLine(ex.Message + "\n\n" + "StackTrace[CreateTable() => checkTable]: \n\n" + ex.StackTrace);
                        }
                    }
                }
                catch (OdbcException ex)
                {
                    Debug.WriteLine(ex.Message + "\n\n" + "StackTrace[CreateTable() => connection]: \n\n" + ex.StackTrace);
                }
            }
        }
    }
}
