using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IndividualProjectFinalPartA_Danieel_Bogdan
{
    class Student
    {
        // Data Base Connection
        static string connectionString =
            @"Server = LAPTOP-KQG7KTD0\SQLEXPRESS; Database = IndividiualProject_PartB_DanieelBogdan; Trusted_Connection = True;";

        static SqlConnection sqlConnection = new SqlConnection(connectionString);


        private const int MinCharactersAllowed = 2;
        private const int MinimumFees = 100;
        private const int MaximumFees = 2500;
        private const int minBornDate = 365 * 18; // = 6570 days = 18 years
        private const int maxBornDate = 365 * 50; // = 18250 days = 50 years

        private string s_FirstName;
        public string S_FirstName
        {
            set
            {
                if (value.Length < MinCharactersAllowed)
                {
                    throw new CustomException($"The first name should be more than {MinCharactersAllowed} charachters");
                }
                if (!value.All(Char.IsLetterOrDigit))
                {
                    throw new CustomException($"The first name should contain only numbers or digits");
                }               

                s_FirstName = char.ToUpper(value[0]) + value.Substring(1);
            }
        }
        private string s_LastName;
        public string S_LastName
        {
            set
            {
                if (value.Length < MinCharactersAllowed)
                {
                    throw new CustomException($"The last name should be more than {MinCharactersAllowed} charachters");
                }
                if (!value.All(Char.IsLetterOrDigit))
                {
                    throw new CustomException("The last name should contain only numbers or digits");
                }               

                s_LastName = char.ToUpper(value[0]) + value.Substring(1);
            }
        }
        private DateTime s_DateOfBirth;
        private DateTime S_DateOfBirth
        {            
            set
            {
                if (value > DateTime.Now.AddDays(-minBornDate) || value < DateTime.Now.AddDays(-maxBornDate))
                {
                    throw new CustomException($"You must enter a year value bigger {DateTime.Now.AddDays(-minBornDate).ToString("dd/MM/yyyy")} and smaller than {DateTime.Now.AddDays(-maxBornDate).ToString("dd/MM/yyyy")}");
                }               

                s_DateOfBirth = value;
            }
        }       

        private double s_Fees;
        private double S_Fees
        {            
            set
            {
                if (value <= MinimumFees || value >= MaximumFees)
                {
                    throw new CustomException(value <= MinimumFees || value >= MaximumFees ? $"Fees cannot be smaller than {MinimumFees}" : $"Fees cannot be bigger than {MaximumFees}");
                }

                s_Fees = value;
            }
        }
        public List<Course> S_Courses { get; set; }
        public List<Assignment> S_Assignments { get; set; }
        
        public Student(string sFirstName, string sLastName, int yearOfbirth, int monthOfBirth, int dayOfBirth, double sFees)
        {
            // Values need for Random generations
            Random random = new Random();
            var sDateOfBirth = new DateTime();
            bool activated = true;

            // Random generation for Names Arrays
            string[] firstName = { "Daniel", "Nikos", "George", "Takis", "Maria", "Dimitra", "Aria", "Tereza", "Iwanna", "Volfkan", "Matahari", "Gregory", "John" };
            string[] lastNames = { "Johnson", "Trump", "McGregor", "Thompson", "Obama", "Clinton", "DevOps", "Wick" };

            if (sFirstName.Length == 0)
            {
                sFirstName = firstName[random.Next(0, firstName.Length)];
            }
            S_FirstName = sFirstName;

            if (sLastName.Length == 0)
            {
                sLastName = lastNames[random.Next(0, lastNames.Length)];
            }
            S_LastName = sLastName;


            do
            {
                if (yearOfbirth == 0)
                {
                    yearOfbirth = random.Next(1970, 2004);
                }

                if (monthOfBirth == 0)
                {
                    monthOfBirth = random.Next(1, 12);
                }

                if (dayOfBirth == 0)
                {
                    dayOfBirth = random.Next(1, 30);
                }

                try
                {
                    sDateOfBirth = new DateTime(yearOfbirth, monthOfBirth, dayOfBirth);
                    activated = false;
                }
                catch (CustomException ex)
                {

                    Console.WriteLine(ex.Message);
                    activated = true;
                }                              
            } while (activated);
            S_DateOfBirth = sDateOfBirth;


            if (sFees == 0)
            {
                sFees = random.Next(MinimumFees, MaximumFees);

            }            
            S_Fees = sFees;


            S_Courses = new List<Course>();
            S_Assignments = new List<Assignment>();
        }

        public static void AddStudentToDB(Student student)
        {
            // Data Base Data Addition
            try
            {
                sqlConnection.Open();

                SqlCommand cmdInsert = new SqlCommand($"INSERT INTO Student(S_FirstName ,S_LastName, S_DateOfBirth, S_Fees) VALUES('{student.s_FirstName}','{student.s_LastName}','{student.s_DateOfBirth}', '{student.s_Fees}')", sqlConnection);
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

            sb.AppendLine($"Student First Name: {this.s_FirstName}")
              .AppendLine($"Student Last Name: {this.s_LastName}")
              .AppendLine($"Student Date of Birth: {this.s_DateOfBirth.ToString("dd/MM/yyyy")}")
              .AppendLine($"Student Fees: {this.s_Fees}");

            return sb.ToString();
        }
    }
}
