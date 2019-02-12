SELECT emp.EmployeeID as ManagerID,
(emp.FirstName + ' ' + emp.LastName) as ManagerFullName ,
workers.EmployeeID, 
(workers.FirstName + ' ' + workers.LastName) as Employee
FROM Employees as emp
INNER JOIN Employees as workers on
workers.ReportsTo = emp.EmployeeID
Order by ManagerID, EmployeeID