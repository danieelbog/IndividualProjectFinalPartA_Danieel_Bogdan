using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IndividualProjectFinalPartA_Danieel_Bogdan
{
    class AssignmentPerCourse
    {
        // Data Base Connection
        static string connectionString =
            @"Server = LAPTOP-KQG7KTD0\SQLEXPRESS; Database = IndividiualProject_PartB_DanieelBogdan; Trusted_Connection = True;";

        static SqlConnection sqlConnection = new SqlConnection(connectionString);


        public Assignment APC_Assignments { get; set; }
        public Course APC_Course { get; set; }

        public AssignmentPerCourse(Assignment assignments, Course course)
        {
            APC_Assignments = assignments;
            APC_Course = course;
        }

        public override string ToString()
        {
            var sb = new StringBuilder();

            sb.AppendLine($"Assignment {APC_Assignments}")
              .AppendLine($"Is currently enrolled to:")
              .AppendLine($"{APC_Course}");

            return sb.ToString();
        }
    }
}
