using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class Department
    {
        public byte Key_dep { get; set; }
        public string Name { get; set; }
        public byte RateCount { get; set; }
        static private byte max_k_dep = 0;

        public Department(string name, byte rateCount)
        {
            Name = name;
            RateCount = rateCount;
            Key_dep =++max_k_dep;
        }
        public Department(byte key, string name, byte rateCount)
        {
            Name = name;
            Key_dep = key;
            RateCount = rateCount;
            if (key > max_k_dep) max_k_dep = key;
        }
    }
    class Post
    {
        public byte Key_post { get; set; }
        public string Name { get; set; }
        public short PostCount { get; set; }
        public decimal PostMoney { get; set; }
        static private byte max_k_post = 0;
        public Post(string name, short postCount, decimal postMoney)
        {
            Name = name;
            PostCount = postCount;
            PostMoney = postMoney;
            Key_post =++max_k_post;
        }

        public Post(byte key, string name, short postCount, decimal postMoney)
        {
            Key_post = key;
            Name = name;
            PostCount = postCount;
            PostMoney = postMoney;
            if (key > max_k_post) max_k_post = key;
        }
    }
    class Person
    {
        public short Key_man { get; set; }
        public string PassNo { get; set; }
        public string Surname { get; set; }
        public string Name { get; set; }
        public DateTime Birthdate { get; set; }
        public char Sex { get; set; }
        public byte ChildCound { get; set; }
        public string Education { get; set; }
        static private short max_k_man = 0;
        public Person(string name, string surname, string passNo, DateTime birthdate, char sex, byte child, string educ)
        {
            Name = name;
            Surname = surname;
            PassNo = passNo;
            Birthdate = birthdate;
            Sex = sex;
            ChildCound = child;
            Education = educ;
            Key_man = ++max_k_man;
        }
        public Person(short key, string name, string surname, string passNo, DateTime birthdate, char sex, byte child, string educ)
        {
            Name = name;
            Key_man = key;
            Surname = surname;
            PassNo = passNo;
            Birthdate = birthdate;
            Sex = sex;
            ChildCound = child;
            Education = educ;
            if (key > max_k_man) max_k_man = key;
        }
    }
    class Worker
    {
        public short Man_key { get; set; }
        public byte Dep_key { get; set; }
        public byte Post_key { get; set; }
        public DateTime EmployDate { get; set; }
        public bool Staff { get; set; }
        public decimal Rate { get; set; }
        public Worker(short man_key, byte dep_key, byte post_key, DateTime employDate, bool staff, decimal rate)
        {
            Man_key = man_key;
            Dep_key = dep_key;
            Post_key = post_key;
            EmployDate = employDate;
            Staff = staff;
            Rate = rate;
        }
    }

    class Company
    {
        public static Company comp;
        public string name = "Сервис";
        public string adress = "ул. Дерибасовская, 18";
        public string phone = "+380001234567";
        public string email = "qwerty@gmail.com";
        private List<Person> Prs;
        private List<Department> Dprt;
        private List<Post> Pst;
        private List<Worker> Wrk;
        public List<Person> ListPrs { get => Prs; }
        public List<Department> ListDprt { get => Dprt; }
        public List<Post> ListPst { get => Pst; }
        public List<Worker> ListWrk { get => Wrk; }
        private Company()
        {
            Prs = new List<Person>();
            Dprt = new List<Department>();
            Pst = new List<Post>();
            Wrk = new List<Worker>();
        }

        public static Company GetCompany()
        {
            if (comp == null) comp = new Company();
            return comp;
        }

        public void AddPerson(byte key, string name, string surname, string passNo, DateTime birthdate, char sex, byte child, string educ)
        {
            Prs.Add(new Person(key, name, surname, passNo, birthdate, sex, child, educ));
        }
        public void AddPerson(string name, string surname, string passNo, DateTime birthdate, char sex, byte child, string educ)
        {
            Prs.Add(new Person(name, surname, passNo, birthdate, sex, child, educ));
        }
        public void AddDepartment(byte key, string name, byte rateCount)
        {
            Dprt.Add(new Department(key, name, rateCount));
        }
        public void AddDepartment(string name, byte rateCount)
        {
            Dprt.Add(new Department(name, rateCount));
        }
        public void AddPost(byte key, string name, short postCount, decimal postMoney)
        {
            Pst.Add(new Post(key, name, postCount, postMoney));
        }
        public void AddPost(string name, short postCount, decimal postMoney)
        {
            Pst.Add(new Post(name, postCount, postMoney));
        }
        public void AddWorker(short man_key, byte dep_key, byte post_key, DateTime employdate, bool staff, decimal rate)
        {
            Wrk.Add(new Worker(man_key, dep_key, post_key, employdate, staff, rate));
        }

        public static void FileRead_Post(string path)
        {
            Company comp = Company.GetCompany();
            StreamReader f1 = new StreamReader(path, System.Text.Encoding.UTF8);
            string str;
            while ((str = f1.ReadLine()) != null)
            {
                string[] ms = str.Split(';');
                comp.AddPost(Convert.ToByte(ms[0]), ms[1], Convert.ToInt16(ms[2]), Convert.ToDecimal(ms[3]));
            }
            f1.Close();
        }

        public static void FileRead_Department(string path)
        {
            Company comp = Company.GetCompany();
            StreamReader f1 = new StreamReader(path, System.Text.Encoding.UTF8);
            string str;
            while ((str = f1.ReadLine()) != null)
            {
                string[] ms = str.Split(';');
                comp.AddDepartment(Convert.ToByte(ms[0]), ms[1], Convert.ToByte(ms[2]));
            }
            f1.Close();
        }

        public static void FileRead_Person(string path)
        {
            Company comp = Company.GetCompany();
            StreamReader f1 = new StreamReader(path, System.Text.Encoding.UTF8);
            string str;
            while ((str = f1.ReadLine()) != null)
            {
                string[] ms = str.Split(';');
                comp.AddPerson(Convert.ToByte(ms[0]), ms[1], ms[2], ms[3], Convert.ToDateTime(ms[4]), ms[5][0],
                    Convert.ToByte(ms[6]), ms[7]);
            }
            f1.Close();
        }

        public static void FileRead_Worker(string path)
        {
            Company comp = Company.GetCompany();
            StreamReader f1 = new StreamReader(path, System.Text.Encoding.UTF8);
            string str;
            while ((str = f1.ReadLine()) != null)
            {
                string[] ms = str.Split(';');
                comp.AddWorker(Convert.ToInt16(ms[0]), Convert.ToByte(ms[1]), Convert.ToByte(ms[2]),
                    Convert.ToDateTime(ms[3]), Convert.ToBoolean(ms[4]), Convert.ToDecimal(ms[5]));
            }
            f1.Close();
        }
    } 

    class Programm
    {
        static void Main(string[] args)
        {
            Company comp = Company.GetCompany();
            // код для 1-й лабораторной 
            comp.AddDepartment("Бухгалтерия", 4);
            comp.AddDepartment("Компьютерный", 4);

            comp.AddPost("главный бухгалтер", 1, 15000);
            comp.AddPost("бухгалтер", 2, 12000);
            comp.AddPost("системный адмнистратор", 2, 14000);
            comp.AddPost("программист", 2, 16000);

            comp.AddPerson("Петр", "Иванов", "КК123456", Convert.ToDateTime("10.12.1995"), 'м', 2, "высшее");
            comp.AddPerson("Анна", "Коваль", "КМ123456", Convert.ToDateTime("10.12.1985"), 'ж', 2, "высшее");
            comp.AddPerson("Глеб", "Туз", "123456789", Convert.ToDateTime("10.12.2005"), 'м', 2, "н/высшее");
            comp.AddPerson("Ольга", "Иванова", "ЬК123456", Convert.ToDateTime("10.12.1992"), 'ж', 2, "высшее");

            comp.AddWorker(1, 2, 3, Convert.ToDateTime("10.12.2020"), true, 1);
            comp.AddWorker(1, 2, 4, Convert.ToDateTime("10.12.2022"), false, (decimal)1 / 2);
            comp.AddWorker(3, 2, 3, Convert.ToDateTime("10.12.2022"), false, (decimal)1 / 2);
            comp.AddWorker(2, 1, 1, Convert.ToDateTime("10.12.2015"), true, 1);
            comp.AddWorker(2, 1, 2, Convert.ToDateTime("10.12.2022"), false, (decimal)1 / 2);
            comp.AddWorker(4, 2, 4, Convert.ToDateTime("10.12.2018"), true, 1);

            Console.WriteLine("Отделы: ");
            foreach (var d in comp.ListDprt)
                Console.WriteLine($"Отдел: {d.Name}, колличество ставок: {d.RateCount} ");

            Console.WriteLine("\nДолжности: ");
            foreach (var p in comp.ListPst)
                Console.WriteLine($"{p.Name} - зарплата {p.PostMoney}, ставок {p.PostCount} ");

            Console.WriteLine("\nЛичный состав: ");
            foreach (var p in comp.ListPrs)
            {
                Console.WriteLine($"{p.Name} {p.Surname}, номер паспорта: {p.PassNo}, дата рождения: {p.Birthdate.ToString("d")}, пол: ");
                if (p.Sex == 'м') Console.Write("мужской, ");
                else Console.Write("женский, ");
                Console.WriteLine($"образование: {p.Education}, кол-во детей: {p.ChildCound}");
            }

            Console.WriteLine("Устройство на работу:\n");
            foreach (var w in comp.ListWrk)
            {
                foreach (var p in comp.ListPrs)
                    if (w.Man_key == p.Key_man) Console.Write($"{p.Name} {p.Surname}, ");

                foreach (var d in comp.ListDprt)
                    if (w.Dep_key == d.Key_dep) Console.Write($" отдел: {d.Name} ");

                foreach (var p in comp.ListPst)
                    if (w.Post_key == p.Key_post) Console.Write($" должность {p.Name} с окладом {p.PostMoney} ");

                Console.WriteLine($" с {w.EmployDate.ToString("d")}, ");
                if (w.Staff) Console.Write("штатный "); else Console.Write("совместитель ");
                Console.WriteLine($" на {w.Rate} ставки\n");
            }
            
            // код для 2-й лабораторной
            comp.ListPrs.Clear();
            Company.FileRead_Person("D:\\Person.txt");
            Console.WriteLine("\nЛичный состав: ");
            foreach (var p in comp.ListPrs)
            {
                Console.WriteLine($"{p.Name} {p.Surname}, номер паспорта: {p.PassNo}, дата рождения: {p.Birthdate.ToString("d")}, пол: ");
                if (p.Sex == 'м') Console.Write("мужской, ");
                else Console.Write("женский, ");
                Console.WriteLine($"образование: {p.Education}, кол-во детей: {p.ChildCound}");
            }


            comp.ListDprt.Clear();
            Company.FileRead_Department("D:\\Department.txt");
            Console.WriteLine("\nОтделы: ");
            foreach (var d in comp.ListDprt)
                Console.WriteLine($"Отдел: {d.Name}, колличество ставок: {d.RateCount} ");


            comp.ListPst.Clear();
            Company.FileRead_Post("D:\\Post.txt");
            Console.WriteLine("\nДолжности: ");
            foreach (var p in comp.ListPst)
                Console.WriteLine($"{p.Name} - зарплата {p.PostMoney}, ставок {p.PostCount} ");


            comp.ListWrk.Clear();
            Company.FileRead_Worker("D:\\Worker.txt");
            Console.WriteLine("Устройство на работу:\n");
            foreach (var w in comp.ListWrk)
            {
                foreach (var p in comp.ListPrs)
                    if (w.Man_key == p.Key_man) Console.Write($"{p.Name} {p.Surname}, ");

                foreach (var d in comp.ListDprt)
                    if (w.Dep_key == d.Key_dep) Console.Write($" отдел: {d.Name} ");

                foreach (var p in comp.ListPst)
                    if (w.Post_key == p.Key_post) Console.Write($" должность {p.Name} с окладом {p.PostMoney} ");

                Console.WriteLine($" с {w.EmployDate.ToString("d")}, ");
                if (w.Staff) Console.Write("штатный"); else Console.Write("совместитель");
                Console.WriteLine($" на {w.Rate} ставки\n");
            }
        }
    }
}