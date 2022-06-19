using Assignment2.Model;
namespace Assignment2.ServiceInterface
{
    public interface IEmployeeService
    {
        public List<Employee> GetAll();
        public Employee Get(int id);
        public Employee Save(Employee employee);
        public void Update(int empId, Employee employee);
        public void Delete(int empId);
    }
}
