using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IndividualProjectFinalPartA_Danieel_Bogdan
{
    class StudentPerCourse
    {
        // Data Base Connection
        static string connectionString =
            @"Server = LAPTOP-KQG7KTD0\SQLEXPRESS; Database = IndividiualProject_PartB_DanieelBogdan; Trusted_Connection = True;";

        static SqlConnection sqlConnection = new SqlConnection(connectionString);


        public Student SWC_Students { get; set; }
        public Course SWC_Course { get; set; }        

        public StudentPerCourse(Student student, Course course)
        {
            SWC_Course = course;
            SWC_Students = student;
        }

        public override string ToString()
        {
            var sb = new StringBuilder();

            sb.AppendLine($"{SWC_Students}")
              .AppendLine($"Is currently enrolled to:")
              .AppendLine($"{SWC_Course}");

            return sb.ToString();

        }
    }
}
