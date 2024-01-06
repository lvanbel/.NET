using LINQDataContext;

DataContext dc = new DataContext();

Console.WriteLine("");
Console.WriteLine("EXERCICE 1");
Console.WriteLine("");

Student? jdepp = (from student in dc.Students
                  where student.Login == "jdepp"
                  select student).SingleOrDefault();

if (jdepp != null)
{
    Console.WriteLine(jdepp.Last_Name + jdepp.First_Name);
}

// Exercice 2.2
Console.WriteLine("");
Console.WriteLine("EXERCICE 2.2");
Console.WriteLine("");
var studentV = from Student s in dc.Students
               select new { FullName = s.Last_Name + " " + s.First_Name, s.Student_ID, s.BirthDate }
                  ;

foreach (var stud in studentV)
{
    Console.WriteLine("{0} {1} {2}", stud.FullName, stud.Student_ID, stud.BirthDate);
}

// 5.1 

// groupement par section
var sectionsResult = from Student s in dc.Students
                     group s by s.Section_ID;



Console.WriteLine("Ex 5.1");
foreach (IGrouping<Int32, Student> section in sectionsResult)
{
    Console.WriteLine("Le max de la section {0} est {1}", section.Key, section.Max(s => s.Year_Result));

}