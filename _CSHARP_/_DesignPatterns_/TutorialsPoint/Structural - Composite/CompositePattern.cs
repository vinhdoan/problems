using System;
using System.Collections.Generic;   // for List<T>

namespace CompositePattern
{
    // 1. Create Employee class having list of Employee objects
    public class Employee
    {
        private String name;
        private String dept;
        private int salary;
        private List<Employee> subordinates;

        // constructor
        public Employee(String name, String dept, int sal)
        {
            this.name = name;
            this.dept = dept;
            this.salary = sal;
            subordinates = new List<Employee>();
        }

        public void add(Employee e)
        {
            subordinates.Add(e);
        }

        public void remove(Employee e)
        {
            subordinates.Remove(e);
        }

        public List<Employee> getSubordinates()
        {
            return subordinates;
        }

        public override String ToString()
        {
            return ("Employee :[ Name : " + name + ", dept : " + dept + ", salary :" + salary + " ]");
        }   
    }

    // 2. Use the Employee class to create and print employee hierarchy
    public class CompositePatternDemo
    {
        public static void Main(String[] args)
        {
            Employee CEO = new Employee("John", "CEO", 30000);
            Employee headSales = new Employee("Robert", "Head Sales", 20000);
            Employee headMarketing = new Employee("Michel", "Head Marketing", 20000);

            Employee clerk1 = new Employee("Laura", "Marketing", 10000);
            Employee clerk2 = new Employee("Bob", "Marketing", 10000);

            Employee salesExecutive1 = new Employee("Richard", "Sales", 10000);
            Employee salesExecutive2 = new Employee("Rob", "Sales", 10000);

            CEO.add(headSales);
            CEO.add(headMarketing);

            headSales.add(salesExecutive1);
            headSales.add(salesExecutive2);

            headMarketing.add(clerk1);
            headMarketing.add(clerk2);

            //print all employees of the organization
            Console.WriteLine(CEO);

            foreach (Employee headEmployee in CEO.getSubordinates())
            {
                Console.WriteLine(headEmployee);

                foreach (Employee employee in headEmployee.getSubordinates())
                {
                    Console.WriteLine(employee);
                }
            }
            
            Console.ReadKey();
        }
    }
}

// 3. Verify the output

// Employee :[ Name : John, dept : CEO, salary :30000 ]
// Employee :[ Name : Robert, dept : Head Sales, salary :20000 ]
// Employee :[ Name : Richard, dept : Sales, salary :10000 ]
// Employee :[ Name : Rob, dept : Sales, salary :10000 ]
// Employee :[ Name : Michel, dept : Head Marketing, salary :20000 ]
// Employee :[ Name : Laura, dept : Marketing, salary :10000 ]
// Employee :[ Name : Bob, dept : Marketing, salary :10000 ]