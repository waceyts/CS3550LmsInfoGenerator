using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LmsInfoGenerator
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] names = new string[] { "Anima", "Anita", "Aria", "Asa", "Asha", "Azami", "Basia", "Baz", "Caris", "Duska", "David", "Elizabeth", "Evander", "Haris", "Idris", "Jennifer", "Kala", "Kamal", "Ken", "Laila", "Paz", "Samir", "Laila", "Zahara", "Jeff", "Daniel", "Hideo" };
            string[] surname = new string[] { "Smith", "Brown", "Garcia", "Rodriguez", "Hernandez", "Anderson", "Lee", "Nguyen", "Adams", "Kim", "Patel", "Long" };
            string[] dept = new string[] { "HIST", "CS", "ART", "MATH", "ENGL" };
            int[] points = new int[] { 10, 50, 100, 200 };
            Random r = new Random();

            lmsEntities lms = new lmsEntities();

            //List<Student> slist = new List<Student>();
            //List<Course> clist = new List<Course>();
            //List<Assignment> alist = new List<Assignment>();

            //GENERATE INSTRUCTORS
            System.Console.WriteLine("How many instructors would you like to generate?(for best results use 0 or minimum of " + dept.Length + "): ");
            int genNum = Convert.ToInt32(System.Console.ReadLine());
            System.Console.WriteLine("Generating instructors...");
            //generate guaranteed instructors
            if (genNum >= dept.Length)
            {
                for (int k = 0; k < dept.Length; k++)
                {
                    Instructor i = new Instructor();
                    i.department = dept[k];
                    i.first_name = names[r.Next(0, names.Length)];
                    i.last_name = surname[r.Next(0, surname.Length)];
                    i.email = i.first_name + i.last_name + "@lms.instructor.edu";
                    if (lms.Instructors.Any(x => x.email == i.email))
                    {
                        int j = 1;
                        while (lms.Instructors.Any(x => x.email == i.email))
                        {
                            i.email = i.first_name + i.last_name + j + "@lms.instructor.edu";
                            j++;
                        }
                    }
                    lms.Instructors.Add(i);
                    lms.SaveChanges();
                }


                //generate random instructors
                for (int k = 0; k < (genNum - 5); k++)
                {
                    Instructor i = new Instructor();
                    i.department = dept[r.Next(0, dept.Length)];
                    i.first_name = names[r.Next(0, names.Length)];
                    i.last_name = surname[r.Next(0, surname.Length)];
                    i.email = i.first_name + i.last_name + "@lms.instructor.edu";
                    if (lms.Instructors.Any(x => x.email == i.email))
                    {
                        int j = 1;
                        while (lms.Instructors.Any(x => x.email == i.email))
                        {
                            i.email = i.first_name + i.last_name + j + "@lms.instructor.edu";
                            j++;
                        }
                    }
                    lms.Instructors.Add(i);
                    lms.SaveChanges();
                }
            }

            //GENERATE COURSES
            System.Console.WriteLine("How many courses would you like to generate?(for best results use 0 or minimum of " + dept.Length + "): ");
            genNum = Convert.ToInt32(System.Console.ReadLine());
            System.Console.WriteLine("Generating courses...");
            //generate guaranteed courses
            if (genNum >= 5)
            {
                for (int i = 0; i < dept.Length; i++)
                {
                    Course c = new Course();
                    c.course_num = r.Next(11, 51) * 100;
                    c.department = dept[i];
                    c.course_title = c.department + " " + c.course_num;
                    while (lms.Courses.Any(x => x.course_title == c.course_title))
                    {
                        c.course_num = r.Next(11, 51) * 100;
                        c.course_title = c.department + " " + c.course_num;
                    }
                    lms.Courses.Add(c);
                    lms.SaveChanges();
                }
                //generate random courses
                for (int i = 0; i < (genNum - dept.Length); i++)
                {
                    Course c = new Course();
                    c.course_num = r.Next(11, 51) * 100;
                    c.department = dept[r.Next(0, dept.Length)];
                    c.course_title = c.department + " " + c.course_num;
                    while (lms.Courses.Any(x => x.course_title == c.course_title))
                    {
                        c.course_num = r.Next(11, 51) * 100;
                        c.course_title = c.department + " " + c.course_num;
                    }
                    lms.Courses.Add(c);
                    lms.SaveChanges();
                }
            }
            string[] sectionDepts = lms.Courses.Select(x => x.department).Distinct().ToArray();

            //GENERATE SECTION
            System.Console.WriteLine("How many sections would you like to generate?(Random # of assignments will be included)");
            genNum = Convert.ToInt32(System.Console.ReadLine());
            System.Console.WriteLine("Generating sections...");
            if (genNum > 0)
            {
                List<Instructor> iList = lms.Instructors.ToList();
                foreach (Instructor x in iList)
                {
                    List<Course> cList = lms.Courses.Where(y => y.department == x.department).ToList();
                    //List<Section> secList = lms.Sections.ToList();
                    //Number of classes
                    int classes = r.Next(1, 4);
                    for (int i = 0; i < classes; i++)
                    {
                        var c = cList[r.Next(0, cList.Count)];
                        Section s = new Section();
                        s.courseId = c.courseId;
                        s.instructorId = x.instructorId;

                        lms.Sections.Add(s);
                        lms.SaveChanges();
                    }
                }
                List<Section> secList = lms.Sections.ToList();
                //GENERATE ASSIGNMENTS
                System.Console.WriteLine("Generating assignments...");
                foreach (Section s in secList)
                {
                    int totalAssignments = r.Next(5, 9);
                    for (int i = 0; i < totalAssignments; i++)
                    {
                        Assignment a = new Assignment();
                        a.description = "Description " + i;
                        a.title = "Assignment " + i;
                        a.dueDate = DateTime.Now;
                        a.openDate = DateTime.Now.AddDays(-7);
                        a.points = points[r.Next(0, points.Length)];
                        a.sectionId = s.sectionId;
                        lms.Assignments.Add(a);
                        lms.SaveChanges();
                    }
                }
            }
            //GENERATE STUDENTS
            System.Console.WriteLine("How many students would you like to generate?(They randomly enroll and recive grades on their assignments)");
            genNum = Convert.ToInt32(System.Console.ReadLine());
            System.Console.WriteLine("Generating students...");
            if (genNum > 0)
            {
                List<Student> slist = new List<Student>();
                for (int i = 0; i < genNum; i++)
                {
                    Student student = new Student();
                    student.first_name = names[r.Next(0, names.Length)];
                    student.last_name = surname[r.Next(0, surname.Length)];
                    student.email = student.first_name + student.last_name + "@lms.student.edu";
                    if (lms.Students.Any(x => x.email == student.email))
                    {
                        int j = 1;
                        while (lms.Students.Any(x => x.email == student.email))
                        {
                            student.email = student.first_name + student.last_name + j + "@lms.student.edu";
                            j++;
                        }
                    }
                    slist.Add(student);
                    lms.Students.Add(student);
                    lms.SaveChanges();
                }

                //GENERATE ENROLLMENTS
                System.Console.WriteLine("Generating enrollments...");
                
                List<Section> secList = lms.Sections.ToList();
                foreach (Student s in slist)
                {
                    //Number of classes
                    int classes = r.Next(1, 4);
                    int passfail = r.Next(1, 20);
                    int i = 0;
                    while (i != classes)
                    {
                        //List<Section> secListLocal = lms.Sections.ToList();
                        Enrollment e = new Enrollment();
                        e.studentId = s.studentId;
                        e.final_grade = "In Progress";
                        e.sectionId = secList[r.Next(0, secList.Count())].sectionId;
                        if(passfail == 1)
                        {
                            e.PassFail = false;
                        }
                        else
                        {
                            e.PassFail = true;
                        }
                        while (lms.Enrollments.Any(x => (x.sectionId == e.sectionId) && (x.studentId == s.studentId)))
                        {
                            e.sectionId = secList[r.Next(0, secList.Count())].sectionId;
                        }
                        lms.Enrollments.Add(e);
                        lms.SaveChanges();
                        i++;
                    }
                }


                //GENERATE GRADES
                System.Console.WriteLine("Generating grades...");
                foreach (Student s in slist)
                {
                    int studentInitiative = r.Next(1, 20);
                    double baseGrade;
                    if (studentInitiative == 20)
                    {
                        baseGrade = .98;
                    }
                    else if (studentInitiative > 14)
                    {
                        baseGrade = .93;
                    }
                    else if (studentInitiative > 8)
                    {
                        baseGrade = .88;
                    }
                    else if (studentInitiative > 5)
                    {
                        baseGrade = .83;
                    }
                    else if (studentInitiative > 3)
                    {
                        baseGrade = .78;
                    }
                    else if (studentInitiative > 1)
                    {
                        baseGrade = .73;
                    }
                    else
                    {
                        studentInitiative = r.Next(1, 20);
                        if (studentInitiative == 1)
                        {
                            baseGrade = .45;
                        }
                        else
                        {
                            baseGrade = .70;
                        }
                    }

                    List<Enrollment> elist = lms.Enrollments.Where(x => x.studentId == s.studentId).ToList();
                    foreach (Enrollment e in elist)
                    {
                        List<Assignment> alist = lms.Assignments.Where(x => x.sectionId == e.sectionId).ToList();
                        foreach (Assignment a in alist)
                        {
                            Grade g = new Grade();
                            g.assignmentId = a.assignmentId;
                            g.studentId = s.studentId;
                            double rand = (r.NextDouble() * .1) + baseGrade;
                            if (rand > 1)
                            {
                                rand = 1;
                            }
                            //System.Console.WriteLine("(" + baseGrade + " + " + rand + ") /" a.points);
                            g.value = (int)(rand * a.points);

                            lms.Grades.Add(g);
                            lms.SaveChanges();
                        }
                    }
                }
            }
            //System.Console.ReadLine();

        }
    }
}
