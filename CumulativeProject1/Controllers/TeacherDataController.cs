using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using CumulativeProject1.Models;
using MySql.Data.MySqlClient;


namespace CumulativeProject1.Controllers
{
    public class TeacherDataController : ApiController
    {
        private SchoolDbContext School = new SchoolDbContext();
        /// <summary>
        /// Returns a list of teachers in the system
        /// </summary>
        /// <example> GET api/TeacherData/ListTeachers </example>
        /// <returns> A list of teachers </returns>
        [HttpGet]
        [Route("api/TeacherData/ListTeachers/{SearchKey?}")]
        public IEnumerable<Teacher> ListTeachers(string SearchKey=null)
        {
            MySqlConnection Conn = School.AccessDatabase();

            Conn.Open();

            MySqlCommand cmd = Conn.CreateCommand();

            cmd.CommandText = "Select * from teachers where lower(teacherfname) like lower(@key) or lower(teacherlname) like lower(@key) or lower(concat(teacherfname, ' ', teacherlname)) like lower(@key)";

            cmd.Parameters.AddWithValue("@key", "%" + SearchKey + "%");
            cmd.Prepare();

            MySqlDataReader ResultSet = cmd.ExecuteReader();

            List<Teacher> Teachers = new List<Teacher>{};

            while (ResultSet.Read())
            {
                int TeacherId = (int)ResultSet["teacherid"];
                string TeacherFname = (string)ResultSet["teacherfname"];
                string TeacherLname = (string)ResultSet["teacherlname"];
                string EmployeeNumber = (string)ResultSet["employeenumber"];
                DateTime HireDate = (DateTime)ResultSet["hiredate"];
                decimal Salary = (decimal)ResultSet["salary"];

                Teacher NewTeacher = new Teacher();
                NewTeacher.TeacherId = TeacherId;   
                NewTeacher.TeacherFname = TeacherFname;
                NewTeacher.TeacherLname = TeacherLname;
                NewTeacher.EmployeeNumber = EmployeeNumber;
                NewTeacher.HireDate = HireDate; 
                NewTeacher.Salary = Salary; 


                Teachers.Add(NewTeacher);
            }

            Conn.Close();

            return Teachers;
        }

        [HttpGet]
        public Teacher FindTeacher(int id)
        {
            Teacher NewTeacher = new Teacher();

            MySqlConnection Conn = School.AccessDatabase();

            Conn.Open();

            MySqlCommand cmd = Conn.CreateCommand();

            cmd.CommandText = "Select * from teachers where teacherid =" + @id;
            cmd.Parameters.AddWithValue("@id",id);
            cmd.Prepare();

            MySqlDataReader ResultSet = cmd.ExecuteReader();

            while (ResultSet.Read())
            {
                int TeacherId = (int)ResultSet["teacherid"];
                string TeacherFname = (string)ResultSet["teacherfname"];
                string TeacherLname = (string)ResultSet["teacherlname"];
                string EmployeeNumber = (string)ResultSet["employeenumber"];
                DateTime HireDate = (DateTime)ResultSet["hiredate"];
                decimal Salary = (decimal)ResultSet["salary"];

                NewTeacher.TeacherId = TeacherId;
                NewTeacher.TeacherFname = TeacherFname;
                NewTeacher.TeacherLname = TeacherLname;
                NewTeacher.EmployeeNumber = EmployeeNumber;
                NewTeacher.HireDate = HireDate;
                NewTeacher.Salary = Salary;

            }
            Conn.Close();

            return NewTeacher;
        }
        
        /// <summary>
        /// This method will add a new teacher in the database
        /// </summary>
        /// <param name="NewTeacher">Teacher Object</param>
        /// <returns></returns>
        /// <example>
        /// api/TeacherData/AddTeacher
        /// POST DATA: Teacher Object
        /// </example>
        [HttpPost]
        
        public void AddTeacher([FromBody]Teacher NewTeacher)
        {
            MySqlConnection Conn = School.AccessDatabase();


            Conn.Open();

            MySqlCommand cmd = Conn.CreateCommand();

            string query = "insert into teachers (teacherid, teacherfname, teacherlname, employeenumber, hiredate, salary) values (@TeacherId, @TeacherFname, @TeacherLname, @EmployeeNumber, @HireDate, @Salary)";

            cmd.CommandText = query;

            cmd.Parameters.AddWithValue("@TeacherId", NewTeacher.TeacherId);
            cmd.Parameters.AddWithValue("@TeacherFname", NewTeacher.TeacherFname);
            cmd.Parameters.AddWithValue("@TeacherLname", NewTeacher.TeacherLname);
            cmd.Parameters.AddWithValue("@EmployeeNumber", NewTeacher.EmployeeNumber);
            cmd.Parameters.AddWithValue("@HireDate", NewTeacher.HireDate);
            cmd.Parameters.AddWithValue("@Salary", NewTeacher.Salary);
           

            cmd.Prepare();
            cmd.ExecuteNonQuery();

            Conn.Close();
            
        }

        /// <summary>
        /// This method will delete a teacher in the database
        /// </summary>
        /// <param name="TeacherId">The teacher id primary key</param>
        /// <example>
        /// POST: api/teacherdata/deleteteacher/2
        /// </example>
        [HttpPost]
        public void DeleteTeacher(int id)
        {
            MySqlConnection Conn = School.AccessDatabase();

            Conn.Open();

            MySqlCommand cmd = Conn.CreateCommand();

            string query = "DELETE from teachers where teacherid=@id";
            cmd.CommandText = query;

            cmd.Parameters.AddWithValue("@id", id);
            cmd.Prepare();
            cmd.ExecuteNonQuery();

            Conn.Close();

        }

        [HttpPost]
        [Route("api/teacherdata/updateteacher/{TeacherId}")]
        public void UpdateTeacher(int id, [FromBody]Teacher TeacherInfo)
        {
            MySqlConnection Conn = School.AccessDatabase();

            Conn.Open();

            MySqlCommand cmd = Conn.CreateCommand();

            cmd.CommandText = "update teachers set teacherfname=@TeacherFname, teacherlname=@TeacherLname, employeenumber=@EmployeeNumber, hiredate=@HireDate, salary=@Salary where teacherid=@TeacherId";

            cmd.Parameters.AddWithValue("@TeacherFname", TeacherInfo.TeacherFname);
            cmd.Parameters.AddWithValue("@TeacherLname", TeacherInfo.TeacherLname);
            cmd.Parameters.AddWithValue("@EmployeeNumber", TeacherInfo.EmployeeNumber);
            cmd.Parameters.AddWithValue("@HireDate", TeacherInfo.HireDate);
            cmd.Parameters.AddWithValue("@Salary", TeacherInfo.Salary);
            cmd.Parameters.AddWithValue("@TeacherId", id);

            cmd.Prepare();
            cmd.ExecuteNonQuery();

            Conn.Close();


        }
    }
}
