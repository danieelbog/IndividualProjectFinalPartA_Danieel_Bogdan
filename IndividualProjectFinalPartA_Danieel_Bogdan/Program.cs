using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IndividualProjectFinalPartA_Danieel_Bogdan
{
    class Program
    {
        // Data Base Connection
        static string connectionString =
            @"Server = LAPTOP-KQG7KTD0\SQLEXPRESS; Database = IndividiualProject_PartB_DanieelBogdan; Trusted_Connection = True;";

        static SqlConnection sqlConnection = new SqlConnection(connectionString);

        static void Main(string[] args)
        {
            // Private School
            PrivateSchool privateSchool = new PrivateSchool();

            // Create lists to corelate instances

            //List<StudentPerCourse> listStudentPerCourse = new List<StudentPerCourse>();

            //List<TrainerPerCourse> listTrainerPerCourse = new List<TrainerPerCourse>();

            //List<AssignmentPerCourse> listAssignmentPerCourse = new List<AssignmentPerCourse>();

            //List<AssignmentPerStudent> listAssignmentsPerStudent = new List<AssignmentPerStudent>();           

            // Other Values used
            int counter1 = 0;
            //int counterChoose1 = 0;
            //int counterChoose2 = 0;
            bool activated = counter1 == 0;
            string choice = "";

            do
            {
                // User choose what he wants to create
                Console.WriteLine("Please choose one of the below:");
                Console.WriteLine("1) Create Course");
                Console.WriteLine("2) Create Trainer");
                Console.WriteLine("3) Create Student");
                Console.WriteLine("4) Create Assignment");
                Console.WriteLine("5) Exit");

                choice = Console.ReadLine();
                switch (choice.ToUpper())
                {
                    case "1":
                        // Create Course   
                        Course course = PrivateSchool.GetCourse();
                        privateSchool.PRS_Courses.Add(course);
                        Course.AddCourseToDB(course);

                        // A method where user will decide if he wants to countinue creating new entities.
                        Console.WriteLine("Would you like to continue? Y/N");
                        choice = Console.ReadLine();
                        activated = Repeater.RepeatTheProgram(choice);

                        // Getting Out of the Case
                        break;

                    case "2":
                        //Create Trainers   
                        Trainer trainer = PrivateSchool.GetTrainer();
                        privateSchool.PRS_Trainers.Add(trainer);
                        Trainer.AddTrainerToDB(trainer);

                        // A method where user will decide if he wants to countinue creating new entities.
                        Console.WriteLine("Would you like to continue? Y/N");
                        choice = Console.ReadLine();
                        activated = Repeater.RepeatTheProgram(choice);

                        // Getting Out of the Case
                        break;

                    case "3":
                        // Create Student                        
                        Student student = PrivateSchool.GetStudent();
                        privateSchool.PRS_Students.Add(student);
                        Student.AddStudentToDB(student);

                        // A method where user will decide if he wants to countinue creating new entities.
                        Console.WriteLine("Would you like to continue? Y/N");
                        choice = Console.ReadLine();
                        activated = Repeater.RepeatTheProgram(choice);

                        // Getting Out of the Case
                        break;

                    case "4":
                        // Create Assignments                       
                        Assignment assignment = PrivateSchool.GetAssignment();
                        privateSchool.PRS_Assignments.Add(assignment);
                        Assignment.AddAssignmentToDB(assignment);

                        // A method where user will decide if he wants to countinue creating new entities.
                        Console.WriteLine("Would you like to continue? Y/N");
                        choice = Console.ReadLine();
                        activated = Repeater.RepeatTheProgram(choice);

                        // Getting Out of the Case
                        break;
                    case "5":
                        Console.WriteLine("Would you like to continue? Y/N");
                        choice = Console.ReadLine();
                        activated = Repeater.RepeatTheProgram(choice);
                        break;

                    default:
                        Console.WriteLine("There is no such Option.");
                        break;
                }
            }
            while (activated);

            // Enroll Instances to other Instances
            do
            {
                Console.WriteLine("1) Add Students to Courses");
                Console.WriteLine("2) Add Trainers to Courses");
                Console.WriteLine("3) Add Assignments to Courses");
                Console.WriteLine("4) Add Assignments to Students");
                Console.WriteLine("5) Exit");

                choice = Console.ReadLine();
                switch (choice.ToUpper())
                {
                    case "1":
                        // Add Students to Courses
                        //PrivateSchool.AddStudentsToCourses(privateSchool.PRS_Courses, privateSchool.PRS_Students, counter1, counterChoose1, counterChoose2, listStudentPerCourse);

                        // Add student to Courses with Querry Method
                        PrivateSchool.AddStudentToCourseDATABASE();
                                               
                        break;

                    case "2":
                        // Add Trainers to Courses
                        //PrivateSchool.AddTrainersToCourses(privateSchool.PRS_Courses, privateSchool.PRS_Trainers, counter1, counterChoose1, counterChoose2, listTrainerPerCourse);

                        // Add student to Courses with Querry Method
                        PrivateSchool.AddTrainerToCourseDATABASE();
                        break;

                    case "3":
                        // Add Assignments to Courses
                        //PrivateSchool.AddAssignmentsToCourse(privateSchool.PRS_Courses, privateSchool.PRS_Assignments, counter1, counterChoose1, counterChoose2, listAssignmentPerCourse);

                        // Add student to Courses with Querry Method
                        PrivateSchool.AddAssignmentToCourseDATABASE();
                        break;

                    case "4":
                        // Add Assignments to Students
                        //PrivateSchool.AddAssignmentsToStudent(privateSchool.PRS_Students, privateSchool.PRS_Assignments, counter1, counterChoose1, counterChoose2, listAssignmentsPerStudent, lsitMarkPerAssignmentPerStudents);

                        // Add student to Courses with Querry Method
                        PrivateSchool.AddAssignmentToStudentDATABASE();
                        break;

                    case "5":
                        break;

                    default:
                        Console.WriteLine("There is no Such option. Please try again");
                        break;
                }

                // A method where user will decide if he wants to countinue creating new entities.
                Console.WriteLine("Would you like to continue? Y/N");
                choice = Console.ReadLine();
                activated = Repeater.RepeatTheProgram(choice);

            }
            while (activated);

            // Print The instances enrolled to other Instances
            do
            {
                // See how how many created instances are in each list.

                Console.WriteLine("What would you like to check?");
                Console.WriteLine("1) List of all Students");
                Console.WriteLine("2) List of all Trainers");
                Console.WriteLine("3) List of all Assignments");
                Console.WriteLine("4) List of all Courses");
                Console.WriteLine("5) List of Students enrolled in Courses");
                Console.WriteLine("6) List of Trainers enrolled in Courses");
                Console.WriteLine("7) List of Assignments per Course");
                Console.WriteLine("8) List of the assignments per student");
                Console.WriteLine("9) List of the assignments per student per course");
                Console.WriteLine("10) List of Students that belong to more than one Courses");
                Console.WriteLine("11) Exit");

                choice = Console.ReadLine();
                switch (choice.ToUpper())
                {
                    case "1":
                        // Print List of all Students C#
                        //counter1 = 0;
                        //foreach (Student student in privateSchool.PRS_Students)
                        //{
                        //    counter1++;
                        //    Console.WriteLine($"Students number {counter1} details are: {student}");
                        //}

                        // Print List of All Students Querry Method

                        sqlConnection.Open();

                        SqlCommand cmdSelect = new SqlCommand("SELECT S_FirstName, S_LastName FROM Student",sqlConnection);                        
                        SqlDataReader DataReader = cmdSelect.ExecuteReader();
                        Console.WriteLine("The below Students are enrolled to our Private School");

                        while (DataReader.Read())
                        {                            
                            Console.WriteLine("Student: " + DataReader.GetString(0) + " " + DataReader.GetString(1));                                                                                    
                        }

                        // Reader Must Close
                        DataReader.Close();
                        // Connection Must Close
                        sqlConnection.Close();

                        break;

                    case "2":
                        //// Print List of all Trainers C# Method
                        //counter1 = 0;
                        //foreach (Trainer trainer in privateSchool.PRS_Trainers)
                        //{
                        //    counter1++;
                        //    Console.WriteLine($"Trainers number {counter1} details are: {trainer}");
                        //}

                        // Print List of all Trainer Querry Method

                        sqlConnection.Open();

                        cmdSelect = new SqlCommand("SELECT T_FirstName, T_LastName FROM Trainer", sqlConnection);
                        DataReader = cmdSelect.ExecuteReader();
                        Console.WriteLine("The below Trainer are teaching in our Private School");

                        while (DataReader.Read())
                        {
                            Console.WriteLine("Trainer: " + DataReader.GetString(0) + " " + DataReader.GetString(1));
                        }

                        // Reader Must Close
                        DataReader.Close();
                        // Connection Must Close
                        sqlConnection.Close();

                        break;

                    case "3":
                        //// Print List of all Assignments
                        //counter1 = 0;
                        //foreach (Assignment assignment in privateSchool.PRS_Assignments)
                        //{
                        //    counter1++;
                        //    Console.WriteLine($"Assignment number {counter1} details are: {assignment}");
                        //}

                        // Print List of all Assignments Querry Method

                        sqlConnection.Open();

                        cmdSelect = new SqlCommand("SELECT A_Title, A_Description, A_SubDate FROM Assignment", sqlConnection);
                        DataReader = cmdSelect.ExecuteReader();
                        Console.WriteLine("The below Assignments are Proposed:");

                        while (DataReader.Read())
                        {
                            Console.WriteLine($"Assignment Title: {DataReader.GetString(0)}. Your objective is: {DataReader.GetString(1)} and must be delivered {DataReader.GetDateTime(2).ToString("dd/MM/yyyy")}");
                        }

                        // Reader Must Close
                        DataReader.Close();
                        // Connection Must Close
                        sqlConnection.Close();

                        break;

                    case "4":
                        //// Print List of all Courses
                        //counter1 = 0;
                        //foreach (Course course in privateSchool.PRS_Courses)
                        //{
                        //    counter1++;
                        //    Console.WriteLine($"Course number {counter1} details are: {course}");
                        //}

                        //// Print List of all Courses Querry Method
                        
                        sqlConnection.Open();

                        cmdSelect = new SqlCommand("SELECT C_Title, C_Stream, C_Type, C_StartDate, C_EndDate FROM Course", sqlConnection);
                        DataReader = cmdSelect.ExecuteReader();
                        Console.WriteLine("The below Assignments are Proposed:");

                        while (DataReader.Read())
                        {
                            Console.WriteLine($"Course Title: {DataReader.GetString(0)}. The Course Stream is: {DataReader.GetString(1)}. It's {DataReader.GetString(2)}. The Starting date is {DataReader.GetDateTime(3).ToString("dd/MM/yyyy")} and Ending Date {DataReader.GetDateTime(4).ToString("dd/MM/yyyy")}");
                        }

                        // Reader Must Close
                        DataReader.Close();
                        // Connection Must Close
                        sqlConnection.Close();

                        break;

                    case "5":
                        // Print List of Students enrolled in Courses    
                        //counter1 = 0;
                        //foreach (StudentPerCourse studentsWithin in listStudentPerCourse)
                        //{
                        //    counter1++;
                        //    Console.WriteLine($"Student: {counter1}");
                        //    Console.WriteLine($"details are:{studentsWithin}");

                        //    // Could also write and use the class prperties, or even a tostring overide if i wanted to make those private. The same idea could be used below up to 8.
                        //    //Console.WriteLine($"Student {studentsWithin.SWC_Students} is enrolled to {studentsWithin.SWC_Course}");
                        //}

                        // Print List of Students enrolled in Courses Querry Method                        

                        sqlConnection.Open();

                        cmdSelect = new SqlCommand("SELECT S_FirstName, S_LastName, C_Title, C_Stream FROM StudentPerCourse INNER JOIN Student on Student.ID = StudentPerCourse.StudentID INNER JOIN Course on StudentPerCourse.CourseID = Course.ID ", sqlConnection);
                        DataReader = cmdSelect.ExecuteReader();
                        Console.WriteLine("Students that are enrolled to respective Courses are:");

                        while (DataReader.Read())
                        {
                            Console.WriteLine($"Student Full Name: {DataReader.GetString(0)} {DataReader.GetString(1)} is enrolled to {DataReader.GetString(2)} {DataReader.GetString(3)}");
                        }

                        // Reader Must Close
                        DataReader.Close();
                        // Connection Must Close
                        sqlConnection.Close();
                        break;

                    case "6":
                        //// Print List of Trainers enrolled in Courses
                        //counter1 = 0;
                        //foreach (TrainerPerCourse trainersWithin in listTrainerPerCourse)
                        //{
                        //    counter1++;
                        //    Console.WriteLine($"Trainer: {counter1}");
                        //    Console.WriteLine($"details are:{trainersWithin}");
                        //}

                        // Print List of Trainers enrolled in Courses Querry Method

                        sqlConnection.Open();

                        cmdSelect = new SqlCommand("SELECT T_FirstName, T_LastName, C_Title, C_Stream FROM TrainerPerCourse INNER JOIN Trainer on Trainer.ID = TrainerPerCourse.TrainerID INNER JOIN Course on TrainerPerCourse.CourseID = Course.ID ", sqlConnection);
                        DataReader = cmdSelect.ExecuteReader();
                        Console.WriteLine("Trainers teaching respective Courses are:");

                        while (DataReader.Read())
                        {
                            Console.WriteLine($"Student Full Name: {DataReader.GetString(0)} {DataReader.GetString(1)} is enrolled to {DataReader.GetString(2)} {DataReader.GetString(3)}");
                        }

                        // Reader Must Close
                        DataReader.Close();
                        // Connection Must Close
                        sqlConnection.Close();
                        
                        break;

                    case "7":
                        //// Print List of Assignments per Course
                        //counter1 = 0;
                        //foreach (AssignmentPerCourse assignmentsPer in listAssignmentPerCourse)
                        //{
                        //    counter1++;
                        //    Console.WriteLine($"Assignment: {counter1}");
                        //    Console.WriteLine($"details are:{assignmentsPer}");
                        //}

                        // Print List of Assignments per Course Querry Method
                        sqlConnection.Open();

                        cmdSelect = new SqlCommand("SELECT C_Title, A_Description, A_SubDate, C_Title, C_Stream FROM AssignmentPerCourse INNER JOIN Assignment on Assignment.ID = AssignmentPerCourse.AssignmentID INNER JOIN Course on AssignmentPerCourse.CourseID = Course.ID ", sqlConnection);
                        DataReader = cmdSelect.ExecuteReader();
                        Console.WriteLine("Assignments given to each Course are:");

                        while (DataReader.Read())
                        {
                            Console.WriteLine($"Assignments Title: {DataReader.GetString(0)}. Student must {DataReader.GetString(1)} and have to submit it until: {DataReader.GetDateTime(2).ToString("dd/MM/yyyy")} in order to Complete the Assignment for {DataReader.GetString(3)} Course at {DataReader.GetString(3)}");
                        }

                        // Reader Must Close
                        DataReader.Close();
                        // Connection Must Close
                        sqlConnection.Close();

                        break;                        

                    case "8":
                        //// Print List of the assignments per student
                        //counter1 = 0;
                        //foreach (AssignmentPerStudent assignmentsPer in listAssignmentsPerStudent)
                        //{
                        //    counter1++;
                        //    Console.WriteLine($"Assignment: {counter1}");
                        //    Console.WriteLine($"details are:{assignmentsPer}");
                        //}

                        // Print List of the assignments per student Querry Method

                        sqlConnection.Open();

                        cmdSelect = new SqlCommand("SELECT S_FirstName, S_LastName, A_Title, A_Description FROM AssignmentPerStudent INNER JOIN Assignment on Assignment.ID = AssignmentPerStudent.AssignmentID INNER JOIN Student on Student.ID = AssignmentPerStudent.StudentID", sqlConnection);
                        DataReader = cmdSelect.ExecuteReader();
                        Console.WriteLine("List of Assignments per Students:");

                        while (DataReader.Read())
                        {
                            Console.WriteLine($"Student: {DataReader.GetString(0)} {DataReader.GetString(1)} has the Assignment of: {DataReader.GetString(2)}");
                        }

                        // Reader Must Close
                        DataReader.Close();
                        // Connection Must Close
                        sqlConnection.Close();

                        break;

                    case "9":                      
                        // List of the assignments per student per course Querry method

                        sqlConnection.Open();

                        cmdSelect = new SqlCommand("SELECT S_FirstName, S_LastName, C_Title, C_Stream, A_Title FROM AssignmentPerCourse INNER JOIN Assignment on Assignment.ID = AssignmentPerCourse.AssignmentID INNER JOIN Course on AssignmentPerCourse.CourseID = Course.ID INNER JOIN StudentPerCourse on StudentPerCourse.CourseID = Course.ID INNER JOIN Student on Student.ID = StudentPerCourse.StudentID", sqlConnection);
                        DataReader = cmdSelect.ExecuteReader();
                        Console.WriteLine("Assignments given to each Course are:");

                        while (DataReader.Read())
                        {
                            Console.WriteLine($"Student: {DataReader.GetString(0)} {DataReader.GetString(1)} for the Course: {DataReader.GetString(2)} - {DataReader.GetString(3)} must deliver the Assginment {DataReader.GetString(4)}");
                        }

                        // Reader Must Close
                        DataReader.Close();
                        // Connection Must Close
                        sqlConnection.Close();

                        break;

                    case "10":

                        //// Print List of Students that belong to more than one Courses
                        //foreach (Student student in privateSchool.PRS_Students)
                        //{
                        //    if (student.S_Courses.Count > 1)
                        //    {
                        //        Console.WriteLine($"The following students are enrolled to more than one courses: {student}");                                
                        //    }
                        //    else
                        //    {
                        //        Console.WriteLine("There are no students that belong to more than one Course");
                        //        break;
                        //    }
                        //}

                        // Print List of Students that belong to more than one Courses Querry Methdo

                        sqlConnection.Open();

                        cmdSelect = new SqlCommand("SELECT S_FirstName, S_LastName, Count(StudentID) FROM StudentPerCourse INNER JOIN Student on Student.ID = StudentPerCourse.StudentID INNER JOIN Course on Course.ID = StudentPerCourse.CourseID GROUP BY S_FirstName, S_LastName HAVING COUNT(StudentPerCourse.StudentID) > 1", sqlConnection);
                        DataReader = cmdSelect.ExecuteReader();                        

                        while (DataReader.Read())
                        {
                            if (DataReader.GetInt32(2) > 1)
                            {
                                Console.WriteLine($"Student: {DataReader.GetString(0)} {DataReader.GetString(1)} is enrolled to {DataReader.GetInt32(2)} Courses");
                            }
                            else
                            {
                                Console.WriteLine("There are no Students in more than one Courses");
                            }
                        }

                        // Reader Must Close
                        DataReader.Close();
                        // Connection Must Close
                        sqlConnection.Close();

                        break;

                    case "11":
                        break;
                }

                // A method where user will decide if he wants to countinue watching the created lists.
                Console.WriteLine("Would you like to continue? Y/N");
                choice = Console.ReadLine();
                activated = Repeater.RepeatTheProgram(choice);

            }
            while (activated);

            // Next Part of the programm. Print the assignments to be delivered at certain dates.
            //do
            //{
            //    try
            //    {
            //        counter1 = 0;
            //        // Enter the Sub Date time you wanna check
            //        DateTime subDueAssignment = PrivateSchool.SubDateTime();
            //        DateTime subDueAssignmentFrwd = subDueAssignment;
            //        DateTime subDueAssignmentBack = subDueAssignment;

            //        // Put the checks for the days here - minus and plus days to check the full week
            //        if (subDueAssignment.DayOfWeek == DayOfWeek.Monday)
            //        {
            //            subDueAssignmentFrwd = subDueAssignment.AddDays(4);
            //            subDueAssignmentBack = subDueAssignment.AddDays(0);
            //        }
            //        else if (subDueAssignment.DayOfWeek == DayOfWeek.Tuesday)
            //        {
            //            subDueAssignmentFrwd = subDueAssignment.AddDays(3);
            //            subDueAssignmentBack = subDueAssignment.AddDays(-1);
            //        }
            //        else if (subDueAssignment.DayOfWeek == DayOfWeek.Wednesday)
            //        {
            //            subDueAssignmentFrwd = subDueAssignment.AddDays(2);
            //            subDueAssignmentBack = subDueAssignment.AddDays(-2);
            //        }
            //        else if (subDueAssignment.DayOfWeek == DayOfWeek.Thursday)
            //        {
            //            subDueAssignmentFrwd = subDueAssignment.AddDays(1);
            //            subDueAssignmentBack = subDueAssignment.AddDays(-3);
            //        }
            //        else if (subDueAssignment.DayOfWeek == DayOfWeek.Friday)
            //        {
            //            subDueAssignmentFrwd = subDueAssignment.AddDays(0);
            //            subDueAssignmentBack = subDueAssignment.AddDays(-4);
            //        }
            //        else if (subDueAssignment.DayOfWeek == DayOfWeek.Saturday)
            //        {
            //            subDueAssignmentFrwd = subDueAssignment.AddDays(-1);
            //            subDueAssignmentBack = subDueAssignment.AddDays(-5);
            //        }
            //        else
            //        {
            //            subDueAssignmentFrwd = subDueAssignment.AddDays(-2);
            //            subDueAssignmentBack = subDueAssignment.AddDays(-6);
            //        }

            //        Assignment.CheckWhenAssignmentMustBeDelivered(privateSchool.PRS_Students, privateSchool.PRS_Assignments, subDueAssignmentFrwd, subDueAssignmentBack);
            //    }
            //    catch (CustomException ex)
            //    {
            //        Console.WriteLine(ex.Message);
            //    }

            //    // A method where user will decide if he wants to countinue giving dates to check if there are Assignments to be delivered
            //    Console.WriteLine("Would you like to continue? Y/N");
            //    choice = Console.ReadLine();
            //    activated = Repeater.RepeatTheProgram(choice);

            //} while (activated);


            // Here the Code for the Sub Date Will go: 1) make any Date a week Period. 2) Take Date from Data Base through Object. 
            //3) Make it also a week Period 4) Make the check and create the text to print it. 
            //5) Ensure that all the Dates are stored somewhere sow e can check them, if there are date tha the Ass must be submited
            // Print a Querry that will say That X,Y student must submit the X,Y assignment at that Date.

            do
            {
                try
                {                   
                    Assignment.CheckForAssignmentSubDateDATABASE();
                }
                catch (Exception ex)
                {

                    Console.WriteLine($"{ex.Message}");
                }

                // A method where user will decide if he wants to countinue watching the created lists.
                Console.WriteLine("Would you like to continue? Y/N");
                choice = Console.ReadLine();
                activated = Repeater.RepeatTheProgram(choice);

            } while (activated);

            // Last part of the programm. Make the trainer give the Marks to the Students assignments (Peri tip).
            //Trainer.PutMarksToEachAssignment(privateSchool.PRS_Assignments, counter1, activated, choice, lsitMarkPerAssignmentPerStudents);

            // Put Marks to the Assignments per Student from the Trainer           
            do
            {
                Trainer.PutMarksToAssignmentDATABASE();

                // A method where user will decide if he wants to countinue giving dates to check if there are Assignments to be delivered
                Console.WriteLine("Would you like to continue? Y/N");
                choice = Console.ReadLine();
                activated = Repeater.RepeatTheProgram(choice);

            } while (activated);

        }
    }
}
