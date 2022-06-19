using Assignment2.Model;
using Assignment2.ServiceInterface;

namespace Assignment2.Services
{
    public class EmployeeService : IEmployeeService
    {
        public static List<Employee> Employees { get; set; }

        static EmployeeService()
        {
            Employees = new List<Employee>
            {
                new Employee{ Id = 1, Name ="Sumit" ,Salary=1000,Gender="Male" },
                new Employee{ Id = 2, Name ="Kiran" ,Salary=2000,Gender="Female" },
                new Employee{ Id = 3, Name ="Ranjit" ,Salary=3000,Gender="Male" },
                new Employee{ Id = 4, Name ="Kareeshma" ,Salary=4000,Gender="Female" },
                new Employee{ Id = 5, Name ="Shoheb" ,Salary=5000,Gender="Male" }
            };
        }
        public List<Employee> GetAll()
        {
            return Employees;
        }

        public Employee Get(int id)
        {
            return Employees.FirstOrDefault(emp => emp.Id == id);
        }
        public Employee Save(Employee employee)
        {
            Employees.Add(employee);
            return employee;
        }

        public void Update(int empId, Employee employee)
        {
            var emp = Employees.FirstOrDefault(x => x.Id == empId);
            if (emp != null)
            {
                emp.Name = employee.Name;
                emp.Salary = employee.Salary;
                emp.Gender = employee.Gender;
            }

        }
        public void Delete(int empId)
        {
            var emp = Employees.FirstOrDefault(x => x.Id == empId);
            if (emp != null)
            {
                Employees.Remove(emp);
            }

        }


    }
}