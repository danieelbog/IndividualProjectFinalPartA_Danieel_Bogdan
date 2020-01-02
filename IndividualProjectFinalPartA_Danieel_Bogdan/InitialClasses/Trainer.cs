using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IndividualProjectFinalPartA_Danieel_Bogdan
{
    class Trainer
    {
        // Data Base Connection
        static string connectionString =
            @"Server = LAPTOP-KQG7KTD0\SQLEXPRESS; Database = IndividiualProject_PartB_DanieelBogdan; Trusted_Connection = True;";

        static SqlConnection sqlConnection = new SqlConnection(connectionString);

        // Begining of Trainer Class
        private const int MaxOral = 50;
        private const int MinOral = 0;
        private const int MinTotal = 0;
        private const int MaxTotal = 50;

        private const int MinCharactersAllowed = 3;

        private string t_FirstName;
        private string T_FirstName
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

                t_FirstName = char.ToUpper(value[0]) + value.Substring(1);

            }
        }
        private string t_LastName;
        private string T_LastName
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

                t_LastName = char.ToUpper(value[0]) + value.Substring(1);
            }
        }
        private string t_Subject;
        private string T_Subject
        {
            set
            {              
                if (value.Length < MinCharactersAllowed)
                {
                    throw new CustomException($"The Subject Name must be more than {MinCharactersAllowed} charachters");
                }
                t_Subject = value;
            }
        }
        public List<Course> T_Courses { get; set; }               

        public Trainer(string tFirstName, string tLastName, string tSubject)
        {
            // Random generation for Names and Subjects Arrays
            Random random = new Random();
            string[] firstNames = { "Daniel", "Nikos", "George", "Takis", "Maria", "Dimitra", "Aria", "Tereza", "Iwanna", "Volfkan", "Matahari", "Gregory", "John", "Argyris", "Periklis" };
            string[] lastNames = { "Johnson", "Trump", "McGregor", "Thompson", "Obama", "Clinton", "DevOps", "Wick", "Pagidas", "Aidinopoulos" };
            string[] subjects = { "Math", "Physics", "Java", "C#.", "FrontEnd", "JavaScript", "Anguilar", "Cobolt", "Python", "C++", "HTML", "CSS" };

            if (tFirstName.Length == 0)
            {
                tFirstName = firstNames[random.Next(0, firstNames.Length)];
            }            
            T_FirstName = tFirstName;

            if (tLastName.Length == 0)
            {
                tLastName = lastNames[random.Next(0, lastNames.Length)];
            }
            T_LastName = tLastName;

            if (tSubject.Length == 0)
            {
                tSubject = subjects[random.Next(0, subjects.Length)];
            }
            T_Subject = tSubject;

            T_Courses = new List<Course>();
        }

        public static void PutMarksToAssignmentDATABASE()
        {
            try
            {
                sqlConnection.Open();

                SqlCommand cmdSelect = new SqlCommand("SELECT AssignmentPerStudent.ID, S_FirstName, S_LastName, A_Title, A_Description FROM AssignmentPerStudent INNER JOIN Assignment on Assignment.ID = AssignmentPerStudent.AssignmentID INNER JOIN Student on Student.ID = AssignmentPerStudent.StudentID", sqlConnection);
                SqlDataReader DataReader = cmdSelect.ExecuteReader();
                Console.WriteLine("List of Assignments per Students:");

                while (DataReader.Read())
                {
                    Console.WriteLine($"Assignment Per Student ID: {DataReader.GetInt32(0)} Student: {DataReader.GetString(1)} {DataReader.GetString(2)} has the Assignment of: {DataReader.GetString(3)} and must {DataReader.GetString(4)}");
                }

                // Reader Must Close
                DataReader.Close();
                // Connection Must Close
                sqlConnection.Close();

                //// Open Sql Conenction
                //sqlConnection.Open();

                //// Here we will see the Assignments per Student list so we can choose for what Assignment and to which student we want to put a Mark
                //SqlCommand cmdSelect = new SqlCommand("SELECT AssignmentPerStudent.ID, A_Title, S_FirstName, S_LastName FROM MarksPerAssignmentPerStudent INNER JOIN AssignmentPerStudent on AssignmentPerStudent.ID = MarksPerAssignmentPerStudent.AssignmentPerStudentID INNER JOIN Student on Student.ID = AssignmentPerStudent.StudentID INNER JOIN Assignment on AssignmentPerStudent.AssignmentID = Assignment.ID", sqlConnection);
                //SqlDataReader DataReader = cmdSelect.ExecuteReader();
                //Console.WriteLine("List of: Assignments per Students");

                //while (DataReader.Read())
                //{
                //    Console.WriteLine($"Assignment Per Student ID: {DataReader.GetInt32(0)} - {DataReader.GetString(1)} is on {DataReader.GetString(2)} {DataReader.GetString(3)}");
                //}

                //// Reader Must Close
                //DataReader.Close();
                //// Connection Must Close
                //sqlConnection.Close();

                //// Open Sql Conenction Again
                //sqlConnection.Open();

                // Somewhere bellow i must write some checks, like not to be able to insert the same student in the same course twice.
                // Here the user will pick the IDs of the Instances so we can Combine them

                sqlConnection.Open();

                Console.WriteLine("Please pick an Assignment per Student ID you want to put Marks on");
                int pickFirstInstanceID = int.Parse(Console.ReadLine());
                Console.WriteLine("Please pick an Oral Mark for the Assginment you Picked");
                int pickOralMark = int.Parse(Console.ReadLine());
                Console.WriteLine("Please pick a Total Mark for the Assginment you Picked");
                int pickTotalMark = int.Parse(Console.ReadLine());

                // The querry to insert the Student to a Course will Run Here
                SqlCommand cmDInsert = new SqlCommand($"UPDATE AssignmentPerStudent SET OralMark = '{pickOralMark}', TotalMark = {pickTotalMark + pickOralMark} WHERE AssignmentPerStudent.ID = '{pickFirstInstanceID}'", sqlConnection);
                
                int rowsInserted = cmDInsert.ExecuteNonQuery();

                // Check if the insertion of the student was completed succesfully.
                if (rowsInserted > 0)
                {
                    Console.WriteLine("The marks was succesfully inserted in the Assignment");
                }
                else
                {
                    Console.WriteLine("Something went wrong with the insertion");
                }

                // Reader Must Close
                DataReader.Close();
                // Connection Must Close
                sqlConnection.Close();

            }
            catch (Exception ex)
            {

                Console.WriteLine($"{ex.Message} Something went wrong with you Database List Printing");
            }
        }


        //public static void PutMarksToEachAssignment(List<Assignment> assignments, int counter, bool activated, string choice, List<MarkPerAssignmentPerStudent> listMarkPerAssignmentPerStudents)
        //{
        //    do
        //    {
        //        Random random = new Random();
        //        Console.WriteLine("At which student Assignment you would like to put your mark?");
        //        counter = 0;

        //        foreach (Assignment assignment in assignments)
        //        {
        //            foreach (MarkPerAssignmentPerStudent markPerAssignmentPerStudent in listMarkPerAssignmentPerStudents)
        //            {
        //                // Pick Assignment
        //                int subCounter = 0;
        //                Console.WriteLine("................................................");
        //                Console.WriteLine("Please choose which Assignment you would like to Put a Score on:");
        //                Console.WriteLine("");
        //                subCounter++;

        //                Console.WriteLine($"Assignment number: {subCounter}: {markPerAssignmentPerStudent.MRAP_Assignment}");

        //                int subCounter2 = int.Parse(Console.ReadLine()) - 1;
        //                Console.WriteLine("Assignment succesfully picked.");

        //                // Assignment Oral Mark
        //                do
        //                {
        //                    try
        //                    {
        //                        string checkOralMark = "";
        //                        Console.WriteLine("Please Enter the Oral Mark for the Assignment");
        //                        checkOralMark = Console.ReadLine();

        //                        if (checkOralMark.Length == 0)
        //                        {
        //                            Console.WriteLine("Oral Marks will be auto generated");
        //                            markPerAssignmentPerStudent.Oral_Mark = random.Next(MinOral, MaxOral);
        //                            Console.WriteLine($"Oral Mark is: {markPerAssignmentPerStudent.Oral_Mark}");
        //                        }
        //                        else
        //                        {
        //                            markPerAssignmentPerStudent.Oral_Mark = int.Parse(checkOralMark);
        //                            activated = false;
        //                        }

        //                        // A check to see if the use input is between the range we want to.
        //                        bool checkEdges = markPerAssignmentPerStudent.Oral_Mark <= MinOral || markPerAssignmentPerStudent.Oral_Mark >= MaxOral;
        //                        activated = false;

        //                        if (checkEdges)
        //                        {
        //                            throw new CustomException(checkEdges ? $"Oral Marks cannot be smaller than {MinOral}" : $"Oral Marks cannot be bigger than {MaxOral}");
        //                        }

        //                    }
        //                    catch (CustomException ex)
        //                    {
        //                        activated = true;
        //                        Console.WriteLine(ex.Message);

        //                    }
        //                } while (activated);

        //                // Assignment Total Mark
        //                do
        //                {
        //                    try
        //                    {
        //                        string checkTotalMark = "";
        //                        Console.WriteLine("Please Write The Total Mark for the Assignment");
        //                        checkTotalMark = Console.ReadLine();

        //                        if (checkTotalMark.Length == 0)
        //                        {
        //                            Console.WriteLine("Total Marks will be auto generated");
        //                            markPerAssignmentPerStudent.Total_Mark = random.Next(MinTotal, MaxTotal);
        //                            Console.WriteLine($"Total Mark is: {markPerAssignmentPerStudent.Total_Mark}");
        //                        }
        //                        else
        //                        {
        //                            markPerAssignmentPerStudent.Total_Mark = int.Parse(checkTotalMark);
        //                            activated = false;
        //                        }

        //                        if (markPerAssignmentPerStudent.Total_Mark < MinTotal || markPerAssignmentPerStudent.Total_Mark > MaxTotal)
        //                        {
        //                            throw new CustomException("Please enter a Mark that is between 0 and 50");
        //                        }

        //                    }
        //                    catch (CustomException ex)
        //                    {
        //                        activated = true;
        //                        Console.WriteLine(ex.Message);
        //                    }
        //                } while (activated);
        //                // Total Mark creation
        //                markPerAssignmentPerStudent.Total_Mark = markPerAssignmentPerStudent.Oral_Mark + markPerAssignmentPerStudent.Total_Mark;

        //                // Add the total mark to the list where marks students and assignments are corelated
        //                listMarkPerAssignmentPerStudents[subCounter2].Oral_Mark = markPerAssignmentPerStudent.Oral_Mark;
        //                listMarkPerAssignmentPerStudents[subCounter2].Total_Mark = markPerAssignmentPerStudent.Total_Mark;

        //                // Add the above created object to the Assginment object as a list
        //                assignment.A_MarksList.Add(markPerAssignmentPerStudent);
        //                Console.WriteLine("Mark succesfully saved");

        //                Console.WriteLine($"The student:{counter} {markPerAssignmentPerStudent.MRAP_Student}");
        //                Console.WriteLine($"for the current assignment: {markPerAssignmentPerStudent.MRAP_Assignment}");
        //                Console.WriteLine($"scored for oral mark: {markPerAssignmentPerStudent.Oral_Mark} and his total mark is {markPerAssignmentPerStudent.Total_Mark}");
        //            }

        //        }

        //        Console.WriteLine("Would you like to add another Mark to a Student Assginment? Y/N");
        //        choice = Console.ReadLine();
        //        activated = Repeater.RepeatTheProgram(choice);

        //    } while (activated);
        //}

        // Add Trainer to DB
        public static void AddTrainerToDB(Trainer trainer)
        {
            // Data Base Data Addition
            try
            {
                sqlConnection.Open();

                SqlCommand cmdInsert = new SqlCommand($"INSERT INTO Trainer(T_FirstName ,T_LastName, T_Subject) VALUES('{trainer.t_FirstName}','{trainer.t_LastName}','{trainer.t_Subject}')", sqlConnection);
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

            sb.AppendLine($"Trainer First Name: {this.t_FirstName}")
              .AppendLine($"Trainer Last Name: {this.t_LastName}")
              .AppendLine($"Trainer Specializstion is on: {this.t_Subject}");

            return sb.ToString();
        }
    }
}
