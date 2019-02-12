select e1.EmployeeID as ManagerId, e1.FirstName + ' ' + e1.LastName as Manager, 
e2.EmployeeID as EmployeeId, e2.FirstName + ' ' + e2.LastName as Employee
from Employees e1 left join Employees e2 on
e1.EmployeeID = e2.ReportsTo
where e1.EmployeeID in (select ReportsTo from Employees)

