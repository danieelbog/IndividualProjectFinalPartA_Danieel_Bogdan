using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IndividualProjectFinalPartA_Danieel_Bogdan
{
    class AssignmentPerStudent
    {
        // Data Base Connection
        static string connectionString =
            @"Server = LAPTOP-KQG7KTD0\SQLEXPRESS; Database = IndividiualProject_PartB_DanieelBogdan; Trusted_Connection = True;";

        static SqlConnection sqlConnection = new SqlConnection(connectionString);


        public Assignment APS_Assignments { get; set; }
        public Student APS_Student { get; set; }

        public double Oral_Mark
        {
            get
            {
                return oral_mark;
            }
            set
            {
                // Data Base Addition
                try
                {
                    sqlConnection.Open();

                    SqlCommand cmdInsert = new SqlCommand($"INSERT INTO MarksPerAssignmentPerStudentTable(OralMark) VALUES('{oral_mark}')", sqlConnection);
                    int rowsInserted = cmdInsert.ExecuteNonQuery();

                    if (rowsInserted > 0)
                    {
                        Console.WriteLine("Insertion Successfull");
                        Console.WriteLine($"{rowsInserted} rows inserted");
                    }

                }
                catch (Exception ex)
                {

                    Console.WriteLine(ex.Message + "Something Went Wrong from the DataBase Addition");
                }

                oral_mark = value;
            }
        }
        public double oral_mark;
        public double Total_Mark
        {
            get
            {
                return total_mark;
            }
            set
            {
                // Data Base Addition
                try
                {
                    sqlConnection.Open();

                    SqlCommand cmdInsert = new SqlCommand($"INSERT INTO MarksPerAssignmentPerStudentTable(TotalMark) VALUES('{total_mark}')", sqlConnection);
                    int rowsInserted = cmdInsert.ExecuteNonQuery();

                    if (rowsInserted > 0)
                    {
                        Console.WriteLine("Insertion Successfull");
                        Console.WriteLine($"{rowsInserted} rows inserted");
                    }

                }
                catch (Exception ex)
                {

                    Console.WriteLine(ex.Message + "Something Went Wrong from the DataBase Addition");
                }

                total_mark = value;
            }
        }
        public double total_mark;
        public AssignmentPerStudent()
        {

        }

        public AssignmentPerStudent(Assignment assignments, Student student)
        {
            APS_Assignments = assignments;
            APS_Student = student;
        }

        public override string ToString()
        {
            var sb = new StringBuilder();

            sb.AppendLine($"Assignment {APS_Assignments}")
              .AppendLine($"Is currently enrolled to:")
              .AppendLine($"{APS_Student}");

            return sb.ToString();
        }
    }
}
