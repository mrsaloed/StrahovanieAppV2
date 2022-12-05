using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace StrahovanieAppV2
{
    class SQL
    {
        //экземпляр класса SQl. Возвращается методом GetInstance. 
        private static SQL instance;
        //Метод для возвращения единственного экземпляра класса SQL
        public static SQL GetInstance()
        {
            if (instance == null)
            {
                instance = new SQL();
            }
            return instance;
        }
        //Сокрытие конструктора для препядствия создания нескольких экземпляров
        private SQL() { }

        //Метод аутентификации пользорвателя. Возвращает ID пользователя системы или код ошибки.
        public int Autent(string login, string pass)
        {
            int result;
            using (var context = new dbModel())
            {
                SqlConnection connection = (SqlConnection)context.Database.Connection;
                SqlCommand cmd = connection.CreateCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "Auth";
                cmd.Parameters.Add("@username", SqlDbType.NVarChar).Value = login;
                cmd.Parameters.Add("@pass", SqlDbType.NVarChar).Value = pass;
                cmd.Parameters.Add("@return_code", SqlDbType.Int);
                cmd.Parameters["@username"].Direction = ParameterDirection.Input;
                cmd.Parameters["@pass"].Direction = ParameterDirection.Input;
                cmd.Parameters["@return_code"].Direction = ParameterDirection.ReturnValue;

                try
                {
                    connection.Open();
                    cmd.ExecuteNonQuery();
                }
                catch (SqlException)
                {
                    connection.Close();
                    result = -1;
                    return result;
                }
                result = Convert.ToInt32(cmd.Parameters["@return_code"].Value.ToString());
                connection.Close();
            }
            return result;
        }
        //Метод поиска пользователя по ID
        public Employee GetEmployee(int id)
        {
            try
            {
                using (var context = new dbModel())
                {
                    return context.Employee.Where(e => e.ID == id).First();
                }
            }
            catch (Exception)
            {
                return null;
            }
        }
        //Метод поиска должности по ID
        public Position GetPosition(Employee employee)
        {
            try
            {
                using (var context = new dbModel())
                {
                    return context.Position.Where(p => p.ID == employee.PositionID).First();
                }
            }
            catch (Exception)
            {
                return null;
            }
        }
        //Метод проверки подключения к БД
        public bool IsConnected()
        {
            using (var context = new dbModel())
            {
                return context.Database.Exists();
            }
        }
        //Метод возврата всех клиентов в виде Списка для таблиц
        public IEnumerable GetAllClients()
        {
            using (var context = new dbModel())
            {
                var clients = from client in context.Client
                              join passport in context.Passport on client.Passport equals passport.ID
                              join license in context.DriverLicense on client.DriverLicense equals license.ID
                              select new
                              {
                                  ID = client.ID,
                                  Фамилия = passport.Lastname,
                                  Имя = passport.Firstname,
                                  Отчество = passport.Surname,
                                  ДатаРождения = passport.DateOfBirth,
                                  СерияПаспорта = passport.Series,
                                  НомерПаспорта = passport.Number,
                                  ДатаВыдачи = passport.PassportDate,
                                  ВУ = license.Number,
                                  ДатаВыдачиВУ = license.DateOfIssue,
                                  ДатаОкончания = license.ExpirationDate
                              };
                return clients.ToList();
            }
        }
        //Метод возврата всех авто в виде Списка для таблиц
        public IEnumerable GetAllCar()
        {
            using (var context = new dbModel())
            {
                var cars = from car in context.Car
                           join inspection in context.Inspection on car.ID equals inspection.CarID
                           select new
                           {
                               Серия = car.Series,
                               Номер = car.Number,
                               ДатаВыдачи = car.DateOfIssue,
                               Отметки = car.SpecialMarks,
                               ДатаОсмотра = inspection.InspectionDate,
                               Состояние = inspection.CarCondition
                           };
                return cars.ToList();
            }
        }
        //Метод добавления клиента в БД
        public int AddClient(string lastname,
                                string firstname,
                                string surname,
                                DateTime dateOfBirth,
                                string passportSeries,
                                string passportNumber,
                                DateTime passportDate,
                                string licenseNumber,
                                DateTime licenseDateOfIssue,
                                DateTime licenseExpirationDate)
        {
            int returnValue;
            using (var context = new dbModel())
            {
                SqlConnection connection = (SqlConnection)context.Database.Connection;
                try
                {
                    SqlCommand cmd = connection.CreateCommand();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "AddNewClient";
                    cmd.Parameters.Add("@lastname", SqlDbType.NVarChar).Value = lastname;
                    cmd.Parameters.Add("@firstname", SqlDbType.NVarChar).Value = firstname;
                    cmd.Parameters.Add("@surname", SqlDbType.NVarChar).Value = surname;
                    cmd.Parameters.Add("@dateOfBirth", SqlDbType.Date).Value = dateOfBirth.Date;
                    cmd.Parameters.Add("@passportSeries", SqlDbType.NVarChar).Value = passportSeries;
                    cmd.Parameters.Add("@passportNumber", SqlDbType.NVarChar).Value = passportNumber;
                    cmd.Parameters.Add("@passportDate", SqlDbType.Date).Value = passportDate.Date;
                    cmd.Parameters.Add("@licenseNumber", SqlDbType.Int).Value = licenseNumber;
                    cmd.Parameters.Add("@licenseDateOfIssue", SqlDbType.Date).Value = licenseDateOfIssue.Date;
                    cmd.Parameters.Add("@licenseExpirationDate", SqlDbType.Date).Value = licenseExpirationDate.Date;
                    cmd.Parameters.Add("@return", SqlDbType.Int);

                    cmd.Parameters["@lastname"].Direction = ParameterDirection.Input;
                    cmd.Parameters["@firstname"].Direction = ParameterDirection.Input;
                    cmd.Parameters["@surname"].Direction = ParameterDirection.Input;
                    cmd.Parameters["@dateOfBirth"].Direction = ParameterDirection.Input;
                    cmd.Parameters["@passportSeries"].Direction = ParameterDirection.Input;
                    cmd.Parameters["@passportNumber"].Direction = ParameterDirection.Input;
                    cmd.Parameters["@passportDate"].Direction = ParameterDirection.Input;
                    cmd.Parameters["@licenseNumber"].Direction = ParameterDirection.Input;
                    cmd.Parameters["@licenseDateOfIssue"].Direction = ParameterDirection.Input;
                    cmd.Parameters["@licenseExpirationDate"].Direction = ParameterDirection.Input;
                    cmd.Parameters["@return"].Direction = ParameterDirection.Output;
                    connection.Open();
                    cmd.ExecuteNonQuery();
                    returnValue = (int)cmd.Parameters["@return"].Value;
                }
                catch (Exception)
                {
                    connection.Close();
                    throw new Exception();
                }
                connection.Close();
                return returnValue;
            }
        }
        //Метод обновления клиента в БД
        public int UpdateClient(string clientId,
                                    string lastname,
                                    string firstname,
                                    string surname,
                                    DateTime dateOfBirth,
                                    string passportSeries,
                                    string passportNumber,
                                    DateTime passportDate,
                                    string licenseNumber,
                                    DateTime licenseDateOfIssue,
                                    DateTime licenseExpirationDate)
        {
            int returnValue;
            using (var context = new dbModel())
            {
                SqlConnection connection = (SqlConnection)context.Database.Connection;
                try
                {
                    SqlCommand cmd = connection.CreateCommand();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "UpdateClient";
                    cmd.Parameters.Add("@ClientId", SqlDbType.Int).Value = Convert.ToInt32(clientId);
                    cmd.Parameters.Add("@lastname", SqlDbType.NVarChar).Value = lastname;
                    cmd.Parameters.Add("@firstname", SqlDbType.NVarChar).Value = firstname;
                    cmd.Parameters.Add("@surname", SqlDbType.NVarChar).Value = surname;
                    cmd.Parameters.Add("@dateOfBirth", SqlDbType.Date).Value = dateOfBirth.Date;
                    cmd.Parameters.Add("@passportSeries", SqlDbType.NVarChar).Value = passportSeries;
                    cmd.Parameters.Add("@passportNumber", SqlDbType.NVarChar).Value = passportNumber;
                    cmd.Parameters.Add("@passportDate", SqlDbType.Date).Value = passportDate.Date;
                    cmd.Parameters.Add("@licenseNumber", SqlDbType.Int).Value = licenseNumber;
                    cmd.Parameters.Add("@licenseDateOfIssue", SqlDbType.Date).Value = licenseDateOfIssue.Date;
                    cmd.Parameters.Add("@licenseExpirationDate", SqlDbType.Date).Value = licenseExpirationDate.Date;
                    cmd.Parameters.Add("@return", SqlDbType.Int);

                    cmd.Parameters["@ClientId"].Direction = ParameterDirection.Input;
                    cmd.Parameters["@lastname"].Direction = ParameterDirection.Input;
                    cmd.Parameters["@firstname"].Direction = ParameterDirection.Input;
                    cmd.Parameters["@surname"].Direction = ParameterDirection.Input;
                    cmd.Parameters["@dateOfBirth"].Direction = ParameterDirection.Input;
                    cmd.Parameters["@passportSeries"].Direction = ParameterDirection.Input;
                    cmd.Parameters["@passportNumber"].Direction = ParameterDirection.Input;
                    cmd.Parameters["@passportDate"].Direction = ParameterDirection.Input;
                    cmd.Parameters["@licenseNumber"].Direction = ParameterDirection.Input;
                    cmd.Parameters["@licenseDateOfIssue"].Direction = ParameterDirection.Input;
                    cmd.Parameters["@licenseExpirationDate"].Direction = ParameterDirection.Input;
                    cmd.Parameters["@return"].Direction = ParameterDirection.Output;
                    connection.Open();
                    cmd.ExecuteNonQuery();
                    returnValue = (int)cmd.Parameters["@return"].Value;
                }
                catch (Exception)
                {
                    connection.Close();
                    throw new Exception();
                }
                connection.Close();
                return returnValue;
            }
        }

        //Метод добавления авто в БД
        public int AddCar(int employee,
                            string series,
                            string number,
                            DateTime dateOfIssue,
                            string specialMarks,
                            string carCondition)
        {
            int returnValue;
            using (var context = new dbModel())
            {
                SqlConnection connection = (SqlConnection)context.Database.Connection;
                try
                {
                    SqlCommand cmd = connection.CreateCommand();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "AddCar";
                    cmd.Parameters.Add("@Employee", SqlDbType.Int).Value = employee;
                    cmd.Parameters.Add("@series", SqlDbType.NVarChar).Value = series;
                    cmd.Parameters.Add("@number", SqlDbType.NVarChar).Value = number;
                    cmd.Parameters.Add("@DateOfIssue", SqlDbType.Date).Value = dateOfIssue.Date;
                    cmd.Parameters.Add("@specialMarks", SqlDbType.NVarChar).Value = specialMarks;
                    cmd.Parameters.Add("@carCondition", SqlDbType.NVarChar).Value = carCondition;
                    cmd.Parameters.Add("@return", SqlDbType.Int);

                    cmd.Parameters["@Employee"].Direction = ParameterDirection.Input;
                    cmd.Parameters["@series"].Direction = ParameterDirection.Input;
                    cmd.Parameters["@number"].Direction = ParameterDirection.Input;
                    cmd.Parameters["@DateOfIssue"].Direction = ParameterDirection.Input;
                    cmd.Parameters["@specialMarks"].Direction = ParameterDirection.Input;
                    cmd.Parameters["@carCondition"].Direction = ParameterDirection.Input;
                    cmd.Parameters["@return"].Direction = ParameterDirection.Output;
                    connection.Open();
                    cmd.ExecuteNonQuery();
                    returnValue = (int)cmd.Parameters["@return"].Value;

                }
                catch (SqlException)
                {
                    connection.Close();
                    throw new Exception();
                }
                connection.Close();
                return returnValue;
            }
        }
        //Метод поиска клиента в БД
        public IEnumerable FindClient()
        {
            using (var context = new dbModel())
            {
                var clients = from client in context.Client
                              join passport in context.Passport on client.Passport equals passport.ID
                              select new
                              {
                                  client.ID,
                                  Фамилия = passport.Lastname,
                                  Имя = passport.Firstname,
                                  Отчество = passport.Surname,
                                  СерияПаспорта = passport.Series,
                                  НомерПаспорта = passport.Number,
                              };
                return clients.ToList();
            }
        }

        //Метод поиска авто в БД
        public IEnumerable FindCar()
        {
            using (var context = new dbModel())
            {
                var cars = from car in context.Car
                           select new
                           {
                               car.ID,
                               Серия = car.Series,
                               Номер = car.Number,
                               ДатаВыдачи = car.DateOfIssue
                           };
                return cars.ToList();
            }
        }
        //Метод вывода всех полисов для таблицы
        public IEnumerable GetAllPolicy()
        {
            using (var context = new dbModel())
            {
                var policies = from policy in context.PolicyTable
                               join insurer in context.Insurer on policy.InsurerID equals insurer.Id
                               select new
                               {
                                   policy.ID,
                                   Серия = policy.Series,
                                   Номер = policy.Number,
                                   ДатаВыдачи = policy.DateOfIssue,
                                   Страховщик = insurer.InsurerName
                               };
                return policies.ToList();
            }
        }

        //Метод вывода страховщиков для КомбоБокса
        public string[] GetAllInsurerNames()
        {
            using (var context = new dbModel())
            {
                var insurers = (from insurer in context.Insurer
                                select insurer.InsurerName.ToString()).ToArray();

                return insurers;
            }
        }

        //Метод поиска страховщика по имени
        public int GetInsurer(string name)
        {
            using (var context = new dbModel())
            {
                return context.Insurer.Where(x => x.InsurerName == name).Select(x => x.Id).First();
            }
        }

        //Метод добавления полиса в БД
        public int AddPolicy(
                                int employee,
                                int client,
                                int car,
                                int insurer,
                                string series,
                                string number)
        {
            int returnValue;
            using (var context = new dbModel())
            {
                SqlConnection connection = (SqlConnection)context.Database.Connection;
                try
                {
                    SqlCommand cmd = connection.CreateCommand();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "AddPolicy";
                    cmd.Parameters.Add("@employee", SqlDbType.Int).Value = employee;
                    cmd.Parameters.Add("@client", SqlDbType.Int).Value = client;
                    cmd.Parameters.Add("@insurer", SqlDbType.Int).Value = insurer;
                    cmd.Parameters.Add("@car", SqlDbType.Int).Value = car;
                    cmd.Parameters.Add("@series", SqlDbType.NVarChar).Value = series;
                    cmd.Parameters.Add("@number", SqlDbType.Int).Value = number;
                    cmd.Parameters.Add("@return", SqlDbType.Int);

                    cmd.Parameters["@employee"].Direction = ParameterDirection.Input;
                    cmd.Parameters["@client"].Direction = ParameterDirection.Input;
                    cmd.Parameters["@insurer"].Direction = ParameterDirection.Input;
                    cmd.Parameters["@car"].Direction = ParameterDirection.Input;
                    cmd.Parameters["@series"].Direction = ParameterDirection.Input;
                    cmd.Parameters["@number"].Direction = ParameterDirection.Input;
                    cmd.Parameters["@return"].Direction = ParameterDirection.Output;
                    connection.Open();
                    cmd.ExecuteNonQuery();
                    returnValue = (int)cmd.Parameters["@return"].Value;

                }
                catch (SqlException)
                {
                    connection.Close();
                    throw new Exception();
                }
                connection.Close();
                return returnValue;
            }
        }
    }
}
