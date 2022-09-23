using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class School
{   
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.None)]
    public int SchoolNum { get; set; }
    public string? SchoolName { get; set; }
}

public class Student
{
   [Key]
   [DatabaseGenerated(DatabaseGeneratedOption.None)]
    public int StudentNum{get;set;}
    public string? StudentGender { get; set; }
    public string? StudentName { get; set; }
    [ForeignKey("School")]
    [Required]
    [DatabaseGenerated(DatabaseGeneratedOption.None)]
    public int SchoolNum{get;set;}
    public School? School { get; set; }
}
 
namespace EFCoreTutorialsss
{
 public class SchoolContext : DbContext
  {
   public DbSet<Student>? Students { get; set; }
   public DbSet<School>? Schools { get; set; }
   protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
     optionsBuilder.UseSqlServer(@"Data Source=TGU1SER15;Initial Catalog=Mounikadata3;User Id=sa;Password=Dbase@1234;TrustServerCertificate=True",
     builder => builder.EnableRetryOnFailure());
    }
  }
  class Program
  {
    public string? StudName;
    public string? Gender;
    public int schlID;
    public static void insertMethodStudent(){
    Console.WriteLine("Enter the number of elements you want to insert");
    int n = Convert.ToInt32(Console.ReadLine());
    Student[] stud = new Student[n];
    School[] stud1 = new School[n];
    using(var context = new SchoolContext()){
    for(int i =0;i<n;i++){
        Console.WriteLine("Enter student id:");
        int studID = Convert.ToInt32(Console.ReadLine());
        Console.WriteLine("Enter student Name:");
        string? studNamee = Console.ReadLine();
        Console.WriteLine("Enter student Gender:");
        string? studGen = Console.ReadLine();
        Console.WriteLine("Enter student school id:");
        int schlID = Convert.ToInt32(Console.ReadLine());
        Console.WriteLine("Enter student school Name:");
        string? schlName =  Console.ReadLine();
        Console.WriteLine(context.Schools.Find(schlID));
        var sc=context.Schools.Find(schlID);
        if(Convert.ToString(sc)!="School"){
          stud1[i] = new School()
            { 
              SchoolNum = schlID,
              SchoolName = schlName
            };
          stud[i] = new Student(){
              StudentNum = studID,
              StudentName = studNamee,
              StudentGender = studGen,
              School = stud1[i]
            };
        }
        else{
            stud[i] = new Student(){
              StudentNum = studID,
              StudentName = studNamee,
            StudentGender = studGen,
            SchoolNum=schlID
            };
            School? sc1=context.Schools.Find(schlID);
            sc1.SchoolName = schlName;
            context.SaveChanges();
              
        }
        context.Students.AddRange(stud);
        context.SaveChanges();
      }
      Console.WriteLine("Inserted Successfully!!!");
   }
  }
  public static void updateMethodStudent(){
    int option;
    Console.WriteLine("Enter the option:1.Upadte Student Name\n2.Update School Name");
    option=Convert.ToInt32(Console.ReadLine());
    if(option==1){
        Console.WriteLine("Enter the Student Id to update name");
        int sid=Convert.ToInt32(Console.ReadLine());
        Console.WriteLine("Enter the new student name:");
        string? sname=Console.ReadLine();
        using(var context = new SchoolContext()){
        var student = context.Students.Find(sid);
        student.StudentName = sname;
        context.SaveChanges();
        Console.WriteLine("Updated Successfully Student name successfully");
      }
    }
    else{
        Console.WriteLine("Enter the Student Id to update SchoolName");
        int s1id=Convert.ToInt32(Console.ReadLine());
        Console.WriteLine("Enter the new school name:");
        string? s1name=Console.ReadLine();
        using(var context = new SchoolContext()){
        var student = context.Students.Find(s1id);
        var sclid=student.SchoolNum;
        var school=context.Schools.Find(sclid);
        school.SchoolName=s1name;
        context.SaveChanges();
        Console.WriteLine("Updated school name successfully");
      }
    }
  }
  public static void others(){
    Console.WriteLine("1.Number of Schools\n2.Number of Students Present in a school and names\n3.Number of females in a school\n4.Number of Males in a School\n5.Female Students Names From a School\n6.Male Students Names From a School");
    int op=Convert.ToInt32(Console.ReadLine());
    if(op==1){
      using(var context=new SchoolContext()){
        var c=context.Schools.Count();
        context.SaveChanges();
        Console.WriteLine($"Total Number Of Schools:{c}");
      }
    }
    else if(op==2){
      int ui=Convert.ToInt32(Console.ReadLine());
      using(var context=new SchoolContext()){
      var c1=context.Students.Where(y=>y.SchoolNum==ui).ToList();
      context.SaveChanges();
      Console.WriteLine($"Total Number Of students {c1.Count()}");
      Console.WriteLine($"Names of all Students:");
      foreach(var i in c1){
        Console.WriteLine($"{i.StudentName}\n");
        }
      }
    }
    else if(op==3){
      int ui=Convert.ToInt32(Console.ReadLine());
       using(var context=new SchoolContext()){
        var c1=context.Students.Where(y=>y.SchoolNum==ui && y.StudentGender=="F").Count();
        context.SaveChanges();
        Console.WriteLine($"Total Number Of female students {c1}");
      }
    }
    else if(op==4){
      int ui=Convert.ToInt32(Console.ReadLine());
       using(var context=new SchoolContext()){
        var c1=context.Students.Where(y=>y.SchoolNum==ui && y.StudentGender=="M").Count();
        context.SaveChanges();
        Console.WriteLine($"Total Number Of Male students {c1}");
      }
    }
    else if(op==5){
      int ui=Convert.ToInt32(Console.ReadLine());
      using(var context=new SchoolContext()){
      var c1=context.Students.Where(y=>y.SchoolNum==ui && y.StudentGender=="F").ToList();
      Console.WriteLine("Total Male students : ");
      foreach(var i in c1){
          Console.WriteLine($"{i.StudentName}");
        }
        context.SaveChanges();
        // Console.WriteLine($"Total Female students {c1}");
      }
    }
    else if(op==6){
      int ui=Convert.ToInt32(Console.ReadLine());
      using(var context=new SchoolContext()){
      var c1=context.Students.Where(y=>y.SchoolNum==ui && y.StudentGender=="M").ToList();
      Console.WriteLine("Total Male students : ");
      foreach(var i in c1){
        Console.WriteLine($"{i.StudentName}");
        }
      context.SaveChanges(); 
      }
    }
    else{
      Console.WriteLine("Enter Valid Option");
    }
  }
  public static void deleteMethod(){
      Console.WriteLine("Enter the id of student to delete");
      int id=Convert.ToInt32(Console.ReadLine());
      using(var context = new SchoolContext()){
      var student = context.Students.Find(id);
      context.Students.Remove(student);
      context.SaveChanges();
      Console.WriteLine("Deleted successfully");
      }
    }
  static void Main(string[] args)
    {
    
      int c=1;
      while(c==1){
        Console.WriteLine("1.Insert\n2.Update\n3.Delete\n4.Others\n5.Exit");
        Console.WriteLine("Enter your choice:");
        int choice=Convert.ToInt32(Console.ReadLine());
        switch(choice){
          case 1:insertMethodStudent();   
                 break;
          case 2:updateMethodStudent();
                break;
          case 3:deleteMethod();
                break;
          case 4:others();
                break;
          case 5:c=0;
                break;
          default:c=0;
                break;

        }
      }
    }
  }   
}


