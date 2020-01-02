using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IndividualProjectFinalPartA_Danieel_Bogdan
{
    class Course
    {
        // Data Base Connection
        static string connectionString =
            @"Server = LAPTOP-KQG7KTD0\SQLEXPRESS; Database = IndividiualProject_PartB_DanieelBogdan; Trusted_Connection = True;";

        static SqlConnection sqlConnection = new SqlConnection(connectionString);

        private const int MinCharactersAllowed = 2;        

        private string c_Title;
        private string C_Title
        {
            set
            {
                if (value.Length < MinCharactersAllowed)
                {
                    throw new CustomException($"Course Title must be more than {MinCharactersAllowed} characters");
                }               

                c_Title = char.ToUpper(value[0]) + value.Substring(1);
            }
        }        

        private string c_Stream;
        private string C_Stream
        {
            set
            {
                if (value.Length < MinCharactersAllowed)
                {
                    throw new CustomException($"Course Stream must be more than {MinCharactersAllowed} characters");
                }                
                c_Stream = value;
            }
        }

        private string c_Type;      
        private string C_Type
        {
            set
            {                
                c_Type = value;
            }
        }
        
        private DateTime c_StartDate;
        private DateTime C_StartDate
        {
            get
            {
                return c_StartDate;
            }
            set
            {
                if (value < DateTime.Now.AddDays(-730) || value > DateTime.Now.AddDays(730))
                {
                    throw new CustomException($"Please for a Starting date enter a value between {DateTime.Now.AddDays(-730).ToString("dd/MM/yyyy")} and {DateTime.Now.AddDays(730).ToString("dd/MM/yyyy")}");
                }                
                c_StartDate = value;
            }
        }
        private DateTime c_EndDate;
        private DateTime C_EndDate
        {
            set
            {
                if (value < C_StartDate || value > DateTime.Now.AddDays(1095))
                {
                    throw new CustomException($"The Ending date value was not between {C_StartDate.ToString("dd/MM/yyyy")} and {DateTime.Now.AddDays(1095).ToString("dd/MM/yyyy")}. Generation will start again");
                }                
                c_EndDate = value;
            }
        }
        private List<Student> C_Students { get; set; }
        private List<Assignment> C_Assignments { get; set; }
        private List<Trainer> C_Trainers { get; set; }

        public Course(string cTitle, string cStream, string cType, int startYear, int startMonth, int startDay, int endYear, int endMonth, int endDay)
        {
            // Random creation
            Random random = new Random();
            bool activated = true;
            var cStartDate = new DateTime();
            var cEndDate = new DateTime();

            // Random generation for Courses Titles, Streams and Types Arrays.            
            string[] titles = { "BC9", "BC10", "BC8" };
            string[] streams = { "Java", "C#", "FrontEnd", "JavaScript", "Anguilar", "Cobolt", "Python", "C++", "HTML", "CSS" };
            string[] types = { "Full Time", "Part Time", "No Time", "Prepare To Die Edition" };

            if (cTitle.Length == 0)
            {
                cTitle = titles[random.Next(0, titles.Length)];
            }
            C_Title = cTitle;

            if (cStream.Length == 0)
            {
                cStream = streams[random.Next(0, streams.Length)];
            }
            C_Stream = cStream;


            if (cType.Length == 0)
            {
                cType = types[random.Next(0, types.Length)];
            }
            C_Type = cType;

            do
            {
                //  Check if the user inserted a value for a year
                if (startYear == 0)
                {
                    startYear = random.Next(2018, 2021);
                }

                //  Check if the user inserted a value for a month
                if (startMonth == 0)
                {
                    startMonth = random.Next(1, 12);
                }

                //  Check if the user inserted a value for a day
                if (startDay == 0)
                {
                    startDay = random.Next(1, 30);
                }

                try
                {
                    cStartDate = new DateTime(startYear, startMonth, startDay);
                    activated = false;
                }
                catch (CustomException ex)
                {
                    Console.WriteLine(ex.Message);
                    activated = true;
                }                

            } while (activated);       
            C_StartDate = cStartDate;            

            do
            {
                //  Check if the user inserted a value for the ending Year
                if (endYear == 0)
                {
                    endYear = random.Next(2018, 2022);
                }

                //  Check if the user inserted a value for the ending Month
                if (endMonth == 0)
                {
                    endMonth = random.Next(1, 12);
                }

                //  Check if the user inserted a value for the ending Day
                if (endDay == 0)
                {
                    endDay = random.Next(1, 30);
                }

                //  Check if the created ending date is after the starting date
                try
                {
                    cEndDate = new DateTime(endYear, endMonth, endDay);
                    activated = false;
                }
                catch (CustomException ex)
                {
                    Console.WriteLine(ex.Message);
                    activated = true;
                }
            } while (activated);            
            C_EndDate = cEndDate;

            C_Students = new List<Student>();
            C_Assignments = new List<Assignment>();
            C_Trainers = new List<Trainer>();
        }

        // Add Course to DB
        public static void AddCourseToDB(Course course)
        {
            try
            {
                sqlConnection.Open();

                SqlCommand cmdInsert = new SqlCommand($"INSERT INTO Course(C_Title, C_Stream, C_Type, C_StartDate, C_EndDate) VALUES('{course.c_Title}', '{course.c_Stream}', '{course.c_Type}', '{course.c_StartDate}', '{course.c_EndDate}')", sqlConnection);
                int rowsInserted = cmdInsert.ExecuteNonQuery();

                if (rowsInserted > 0)
                {
                    Console.WriteLine("Insertion Successfull");
                    Console.WriteLine($"{rowsInserted} rows inserted");
                }

                sqlConnection.Close();
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message + "Something Went Wrong from the DataBase Addition");
            }
        }

        public override string ToString()
        {
            var sb = new StringBuilder();

            sb.AppendLine($"Course Title: {this.c_Title}")
              .AppendLine($"Course Stream: {this.c_Stream}")
              .AppendLine($"Course Type: {this.c_Type}")
              .AppendLine($"Course Starting Date: {this.c_StartDate.ToString("dd/MM/yyyy")}")
              .AppendLine($"Course Ending Date: {this.c_EndDate.ToString("dd/MM/yyyy")}");

            return sb.ToString();
        }
    }
}
