select r.RegionDescription, t.TerritoryDescription, 
count(et.EmployeeId) as adet
from Region as r
inner join Territories as t on
r.RegionID = t.RegionID
inner join EmployeeTerritories as et on
t.TerritoryID = et.TerritoryID
group by r.RegionDescription, t.TerritoryDescription
order by r.RegionDescription, t.TerritoryDescription

