using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IndividualProjectFinalPartA_Danieel_Bogdan
{
    class Assignment
    {
        // Data Base Connection
        static string connectionString =
            @"Server = LAPTOP-KQG7KTD0\SQLEXPRESS; Database = IndividiualProject_PartB_DanieelBogdan; Trusted_Connection = True;";

        static SqlConnection sqlConnection = new SqlConnection(connectionString);

        private const int MinCharactersAllowed = 2;
        private const int range = 730;
        private const int range2 = 1095;

        private string a_Title;
        private string A_Title
        {

            set
            {
                if (value.Length < MinCharactersAllowed)
                {
                    throw new CustomException($"Assignment Title must be more than {MinCharactersAllowed} characters");
                }               

                a_Title = value;
            }
        }       

        private string a_Description;
        private string A_Description
        {
            set
            {
                if (value.Length < MinCharactersAllowed)
                {
                    throw new CustomException($"Assignment Descreption must be more than {MinCharactersAllowed} characters");
                }

                a_Description = value;
            }
        }
        private DateTime a_SubDateTime;
        private DateTime A_SubDateTime
        {
            set
            {
                if (value < DateTime.Now.AddDays(-range) || value > DateTime.Now.AddDays(range2))
                {
                    throw new CustomException($"Please enter a value between {DateTime.Now.AddDays(-range).ToString("dd/MM/yyyy")} and {DateTime.Now.AddDays(range2).ToString("dd/MM/yyyy")}");
                }
                
                a_SubDateTime = value;
            }
        }              
        public List<Course> A_Courses { get; set; }
        public List<Student> A_Students { get; set; }
        private int A_StudentCount { get; set; }

        private Assignment()
        {

        }

        public Assignment(string aTitle, string aDescreption, int aSubYear, int aSubMonth, int aSubDay)
        {
            // Values used in Random generation
            Random random = new Random();
            var aSubDateTime = new DateTime();
            bool activated = true;

            // Assignment Titles, Descreptions.
            string[] titles = { "As-1", "As-2", "As-3", "As-4", "As-5" };
            string[] desccriptions = { "Desc1", "Desc2", "Desc3", "Desc4", "Desc5", "Desc6" };

            if (aTitle.Length == 0)
            {                
                aTitle = titles[random.Next(0, titles.Length)];
                
            }
            A_Title = aTitle;

            if (aDescreption.Length == 0)
            {                
                aDescreption = desccriptions[random.Next(0, desccriptions.Length)];                
            }
            A_Description = aDescreption;

            do
            {
                if (aSubYear == 0)
                {                   
                    aSubYear = random.Next(2018, 2022);
                   
                }

                if (aSubMonth == 0)
                {
                    aSubMonth = random.Next(1, 12);
                }

                if (aSubDay == 0)
                {
                    aSubDay = random.Next(1, 30);
                }

                try
                {
                    aSubDateTime = new DateTime(aSubYear, aSubMonth, aSubDay);
                    activated = false;
                }
                catch (CustomException ex)
                {

                    Console.WriteLine(ex.Message);
                    activated = true;
                }
            } while (activated);
            A_SubDateTime = aSubDateTime;          
                     
            A_Courses = new List<Course>();
            A_Students = new List<Student>();
            
        }
        //public static void CheckWhenAssignmentMustBeDelivered(List<Student> students, List<Assignment> assignments, DateTime subDueAssignmentFrwd, DateTime subDueAssignmentBack)
        //{
        //    for (int i = 0; i < students.Count; i++)
        //    {
        //        // Create the check
        //        foreach (Assignment assignment in assignments)
        //        {
        //            if (assignment.a_SubDateTime.DayOfWeek == DayOfWeek.Saturday)
        //            {
        //                assignment.a_SubDateTime = assignment.a_SubDateTime.AddDays(-1);
        //            }
        //            else if (assignment.a_SubDateTime.DayOfWeek == DayOfWeek.Sunday)
        //            {
        //                assignment.A_SubDateTime = assignment.a_SubDateTime.AddDays(-2);
        //            }
        //            else
        //            {
        //                if (assignment.a_SubDateTime <= subDueAssignmentFrwd && assignment.a_SubDateTime >= subDueAssignmentBack)
        //                {
        //                    Console.WriteLine($"The student {students[i]} must subdue the assignment {students[i].S_Assignments[i]} at {assignment.a_SubDateTime.ToString("dd/MM/yyyy")}");
        //                }
        //                else
        //                {
        //                    Console.WriteLine($"There are no assignments to be subbed between {subDueAssignmentBack.ToString("dd/MM/yyyy")} and {subDueAssignmentFrwd.ToString("dd/MM/yyyy")}");
        //                    break;
        //                }
        //            }
        //        }
        //    }
        //}

        // Add Assignment to DB
        public static void AddAssignmentToDB(Assignment assignment)
        {
            // Data Base Addition
            try
            {
                sqlConnection.Open();

                SqlCommand cmdInsert = new SqlCommand($"INSERT INTO Assignment(A_Title, A_Description, A_SubDate) VALUES('{assignment.a_Title}', '{assignment.a_Description}', '{assignment.a_SubDateTime}')", sqlConnection);
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

        public static void CheckForAssignmentSubDateDATABASE()
        {

            // Enter the Sub Date time you wanna check
            DateTime subDueAssignment = PrivateSchool.SubDateTime();
            DateTime subDueAssignmentFrwd = subDueAssignment;
            DateTime subDueAssignmentBack = subDueAssignment;

            // Make the Date the user want to chake, have the desired range
            try
            {               
                // Put the checks for the days here - minus and plus days to check the full week
                if (subDueAssignment.DayOfWeek == DayOfWeek.Monday)
                {
                    subDueAssignmentFrwd = subDueAssignment.AddDays(4);
                    subDueAssignmentBack = subDueAssignment.AddDays(0);
                }
                else if (subDueAssignment.DayOfWeek == DayOfWeek.Tuesday)
                {
                    subDueAssignmentFrwd = subDueAssignment.AddDays(3);
                    subDueAssignmentBack = subDueAssignment.AddDays(-1);
                }
                else if (subDueAssignment.DayOfWeek == DayOfWeek.Wednesday)
                {
                    subDueAssignmentFrwd = subDueAssignment.AddDays(2);
                    subDueAssignmentBack = subDueAssignment.AddDays(-2);
                }
                else if (subDueAssignment.DayOfWeek == DayOfWeek.Thursday)
                {
                    subDueAssignmentFrwd = subDueAssignment.AddDays(1);
                    subDueAssignmentBack = subDueAssignment.AddDays(-3);
                }
                else if (subDueAssignment.DayOfWeek == DayOfWeek.Friday)
                {
                    subDueAssignmentFrwd = subDueAssignment.AddDays(0);
                    subDueAssignmentBack = subDueAssignment.AddDays(-4);
                }
                else if (subDueAssignment.DayOfWeek == DayOfWeek.Saturday)
                {
                    subDueAssignmentFrwd = subDueAssignment.AddDays(-1);
                    subDueAssignmentBack = subDueAssignment.AddDays(-5);
                }
                else
                {
                    subDueAssignmentFrwd = subDueAssignment.AddDays(-2);
                    subDueAssignmentBack = subDueAssignment.AddDays(-6);
                }
            }
            catch (Exception ex)
            {

                Console.WriteLine($"{ex.Message}");
            }

            // Here we will take the Data we need from the Server
            try
            {
                // Start Sql Connection
                sqlConnection.Open();

                // select multiple Assignments from DataBase and put them in a list so we can retrieve the Sub Date afterwards
                List<Assignment> assignmentListDataBase = new List<Assignment>();
                SqlCommand cmdSelectAssignments = new SqlCommand("SELECT DISTINCT A_SubDate, A_Title, A_Description, Count(AssignmentPerStudent.StudentID) FROM AssignmentPerStudent INNER JOIN Assignment on AssignmentPerStudent.AssignmentID = Assignment.ID INNER JOIN Student on Student.ID = AssignmentPerStudent.StudentID GROUP BY A_SubDate, A_Title, A_Description ORDER BY Count(AssignmentPerStudent.StudentID) desc", sqlConnection);

                // Start SqlReader so we can retrieve and read our data from the server
                SqlDataReader dataReader = cmdSelectAssignments.ExecuteReader();               

                while (dataReader.Read())
                {

                    // Create a mock object with the data we want to retrieve from the server put the instance in a list so we can use it later
                    Assignment assignmentDB = new Assignment()
                    {
                        A_Title = dataReader.GetString(1),
                        A_Description = dataReader.GetString(2),
                        // Maybe i m goint to need something like this in the future
                        A_StudentCount = dataReader.GetInt32(3),
                        A_SubDateTime = dataReader.GetDateTime(0)
                        
                    };

                    //Put the instance in a list so we can use it later
                    assignmentListDataBase.Add(assignmentDB);
                }

                
                // Data Reader must Close
                dataReader.Close();

                // Sql Connection must Close
                sqlConnection.Close();

                // We will guarantee that the date for sub in the server is a day from Monday to Friday
                foreach (Assignment assignmentDB in assignmentListDataBase)
                {                  
                    if (assignmentDB.a_SubDateTime.DayOfWeek == DayOfWeek.Saturday)
                    {
                        assignmentDB.a_SubDateTime = assignmentDB.a_SubDateTime.AddDays(-1);
                    }
                    else if (assignmentDB.a_SubDateTime.DayOfWeek == DayOfWeek.Sunday)
                    {
                        assignmentDB.a_SubDateTime = assignmentDB.a_SubDateTime.AddDays(-2);
                    }
                    else
                    {
                        // We make the check, if the date from server is within range of week the user entered, then the message will triger
                        if (assignmentDB.a_SubDateTime <= subDueAssignmentFrwd && assignmentDB.a_SubDateTime >= subDueAssignmentBack)
                        {
                            Console.WriteLine($"The assignment {assignmentDB.a_Title} {assignmentDB.a_Description} must be submited up to {assignmentDB.a_SubDateTime.ToString("dd/MM/yyyy")}");
                            
                        }
                        else
                        {
                            Console.WriteLine($"There are no assignments to be subbed between {subDueAssignmentBack.ToString("dd/MM/yyyy")} and {subDueAssignmentFrwd.ToString("dd/MM/yyyy")}");

                        }
                    }
                }               

            }
            catch (Exception ex)
            {
                Console.WriteLine($"{ex.Message} something was wrong with you Date insertion to C# from DataBase"); ;
            }
            
        }

        public override string ToString()
        {
            var sb = new StringBuilder();

            sb.AppendLine($"Assignment Title: {this.a_Title}")
              .AppendLine($"Assignment Descreption: {this.a_Description}")
              .AppendLine($"The Submission Date for the Assignment: {this.a_SubDateTime.ToString("dd/MM/yyyy")}");
            //.AppendLine($"The Oral Mark for the Assignment is: {A_OralMark}")
            //.AppendLine($"Total Mark for the Assignment is: {A_TotalMark}");

            return sb.ToString();
        }
    }
}
