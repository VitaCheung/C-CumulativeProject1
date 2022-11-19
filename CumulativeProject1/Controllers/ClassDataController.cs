using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.InteropServices.ComTypes;
using System.Security.Claims;
using System.Web.Http;
using CumulativeProject1.Models;
using MySql.Data.MySqlClient;


namespace CumulativeProject1.Controllers
{
    public class ClassDataController : ApiController
    {
        private SchoolDbContext School = new SchoolDbContext();
        /// <summary>
        /// Returns a list of classes in the system
        /// </summary>
        /// <example> GET api/ClassData/ListTeachers </example>
        /// <returns> A list of classes </returns>
        [HttpGet]
        public IEnumerable<Class> ListClasses()
        {
            MySqlConnection Conn = School.AccessDatabase();

            Conn.Open();

            MySqlCommand cmd = Conn.CreateCommand();

            cmd.CommandText = "Select * from classes";

            MySqlDataReader ResultSet = cmd.ExecuteReader();

            List<Class> Classes = new List<Class>{};

            while (ResultSet.Read())
            {
                int ClassId = (int)ResultSet["classid"];
                string ClassName = (string)ResultSet["classname"];               
                object ClassCode = (object)ResultSet["classcode"];
                DateTime StartDate = (DateTime)ResultSet["startdate"];
                DateTime FinishDate = (DateTime)ResultSet["finishdate"];
                


                Class NewClass = new Class();
                NewClass.ClassId = ClassId;
                NewClass.ClassCode = ClassCode;
                NewClass.StartDate = StartDate;
                NewClass.FinishDate = FinishDate;
                NewClass.ClassName = ClassName;
                


                Classes.Add(NewClass);
            }

            Conn.Close();

            return Classes;
        }

        [HttpGet]
        public Class FindClass(int id)
        {
            Class NewClass = new Class();

            MySqlConnection Conn = School.AccessDatabase();

            Conn.Open();

            MySqlCommand cmd = Conn.CreateCommand();

            cmd.CommandText = "Select * from classes where classid =" + id;

            MySqlDataReader ResultSet = cmd.ExecuteReader();

            while (ResultSet.Read())
            {
                int ClassId = (int)ResultSet["classid"];
                string ClassName = (string)ResultSet["classname"];
                object ClassCode = (object)ResultSet["classcode"];
                DateTime StartDate = (DateTime)ResultSet["startdate"];
                DateTime FinishDate = (DateTime)ResultSet["finishdate"];
                

                NewClass.ClassId = ClassId;
                NewClass.ClassCode = ClassCode;
                NewClass.StartDate = StartDate;
                NewClass.FinishDate = FinishDate;
                NewClass.ClassName = ClassName;
                

            }

            return NewClass;
        }

    }
}
