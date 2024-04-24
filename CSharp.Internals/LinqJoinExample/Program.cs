using CorporateStructure;

List<Employee> employees = 
[
    new("John", "Manager", 1),
    new("Jane", "Developer", 2)
];

List<Department> departments = 
[
    new(1, "HR"),
    new(2, "IT")
];

var employeesInfo = employees.Join(
    departments,
    employee => employee.DepartmentNumber,
    department => department.DepartmentNumber,
    (employee, department) => new
    {
        employee.EmployeeName,
        EmployeeJob = employee.Job,
        Department = department.DepartmentName
    });

foreach (var item in employeesInfo)
{
    Console.WriteLine($"Employee Name: {item.EmployeeName}, Job: {item.EmployeeJob}, Department: {item.Department}");
}

namespace CorporateStructure
{
    public record Employee(string EmployeeName, string Job, int DepartmentNumber);

    public record Department(int DepartmentNumber, string DepartmentName);
}