using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IndividualProjectFinalPartA_Danieel_Bogdan
{
    class TrainerPerCourse
    {
        // Data Base Connection
        static string connectionString =
            @"Server = LAPTOP-KQG7KTD0\SQLEXPRESS; Database = IndividiualProject_PartB_DanieelBogdan; Trusted_Connection = True;";

        static SqlConnection sqlConnection = new SqlConnection(connectionString);


        public Trainer TWC_Trainers { get; set; }
        public Course TWC_Course { get; set; }

        public TrainerPerCourse(Trainer trainers, Course course)
        {
            TWC_Trainers = trainers;
            TWC_Course = course;
        }

        public override string ToString()
        {
            var sb = new StringBuilder();

            sb.AppendLine($"Trainer {TWC_Trainers}")
              .AppendLine($"Is currently enrolled to:")
              .AppendLine($"{TWC_Course}");

            return sb.ToString();
        }
    }
}
