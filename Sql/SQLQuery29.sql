select distinct subdivision_1_name
from Cities 
where country_name = 'TURKEY' and not subdivision_1_name is null
order by subdivision_1_name 