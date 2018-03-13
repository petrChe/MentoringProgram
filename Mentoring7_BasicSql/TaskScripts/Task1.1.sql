--Задание 1.1. Простая фильтрация данных.
select ord.OrderID, ord.ShippedDate, ord.ShipVia
from dbo.Orders as ord
where ord.ShippedDate >= try_convert(datetime, '1998-05-06', 102)
and ord.ShipVia >= 2;

select ord.OrderID,
case 
	when ord.ShippedDate is not null then left(convert(datetime, ord.ShippedDate,120),10)
	else 'Not shipped'
end as 'Shipped date'
from dbo.Orders as ord;

select ord.OrderID as 'Order Number',
case 
	when ord.ShippedDate is not null then left(convert(datetime, ord.ShippedDate,120),10)
	else 'Not shipped'
end as 'Shipped date'
from dbo.Orders as ord
where ord.ShippedDate > try_convert(datetime, '1998-05-06', 102);

--Задание 1.2. Использование операторов IN, DISTINCT, ORDER BY, NOT
select cust.ContactName, cust.Country
from dbo.Customers as cust
where cust.Country in ('USA', 'Canada')
order by cust.ContactName, cust.Country;

select cust.ContactName, cust.Country
from dbo.Customers as cust
where cust.Country not in ('USA', 'Canada')
order by cust.ContactName, cust.Country;

select distinct cust.Country
from dbo.Customers as cust
order by cust.Country desc;

--Задание 1.3. Использование оператора BETWEEN, DISTINCT
select distinct ordDet.OrderId
from dbo.[Order Details] as ordDet
where ordDet.Quantity between 3 and 10;

--LEFT(colName, 1) equals SUBSTRING(colName, 1, 1)
select cust.CustomerID, cust.Country
from dbo.Customers as cust
where left(cust.Country, 1) between 'b' and 'g'
order by cust.Country;

select cust.CustomerID, cust.Country
from dbo.Customers as cust
where left(cust.Country, 1) in ('b', 'c', 'd', 'e', 'f', 'g')
order by cust.Country;

--Задание 1.4. Использование оператора LIKE
select pr.ProductName
from dbo.Products as pr
where pr.ProductName like 'Cho_olade';

--Задание 2.1. Использование агрегатных функций (SUM, COUNT)
select SUM((ordDet.UnitPrice - ordDet.Discount) * ordDet.Quantity) as 'Totals'
from dbo.[Order Details] as ordDet;

--SUM(case when ord.ShippedDate is null then 1 else 0 end)
select COUNT(case when ord.ShippedDate is null then 1 else null end) as 'Count'
from dbo.Orders as ord;

select COUNT(distinct ord.CustomerID) as 'Total'
from dbo.Orders as ord;

--Задание 2.2. Соединение таблиц, использование агрегатных функций и предложений GROUP BY и HAVING
select year(ord.RequiredDate) as 'Year', Count(ord.OrderID) as 'Total'
from dbo.Orders as ord
group by year(ord.RequiredDate);

--select COUNT(ord.CustomerID) as 'Total'
--from dbo.Orders as ord;

select ord.EmployeeID,
(select emp.LastName + emp.FirstName
 from dbo.Employees emp
 where emp.EmployeeID = ord.EmployeeID) as 'Seller',
 COUNT(ord.OrderID) as 'Amount'
from dbo.Orders as ord
group by ord.EmployeeID
order by COUNT(ord.OrderID) desc;

select ord.EmployeeID,
(select emp.LastName + emp.FirstName
 from dbo.Employees emp
 where emp.EmployeeID = ord.EmployeeID) as 'Seller',
 (select cust.ContactName
 from dbo.Customers as cust
 where cust.CustomerID = ord.CustomerID) as 'Customer',
 COUNT(ord.OrderID) as 'Amount'
from dbo.Orders as ord 
group by ord.EmployeeID, ord.CustomerID, YEAR(ord.OrderDate)
having YEAR(ord.OrderDate) = '1998';

select
(select emp.LastName + emp.FirstName
 from dbo.Employees emp
 where emp.EmployeeID = ord.EmployeeID) as 'Seller',
 (select cust.ContactName
 from dbo.Customers as cust
 where cust.CustomerID = ord.CustomerID) as 'Customer',
 ord.ShipCity
from dbo.Orders as ord 
group by ord.ShipCity, ord.EmployeeID, ord.CustomerID
--having???

select cust.City, cust.ContactName
from dbo.Customers as cust
group by cust.City, cust.ContactName;

select emp.LastName, emp.ReportsTo
from dbo.Employees as emp
group by emp.ReportsTo, emp.LastName;

--Задание 2.3. Использование JOIN
select emp.LastName, reg.RegionDescription
from dbo.Employees as emp
join dbo.EmployeeTerritories empter on emp.EmployeeID = empter.EmployeeID
join dbo.Territories ter on empter.TerritoryID = ter.TerritoryID
join dbo.Region reg on ter.RegionID = reg.RegionID
where reg.RegionDescription = 'Western';

select cust.ContactName, COUNT(cust.CustomerID) as 'Total'
from dbo.Customers as cust
left join dbo.Orders as ord on cust.CustomerID = ord.CustomerID
group by cust.CustomerID, cust.ContactName;

--Задание 2.4. Использование подзапросов
select supp.CompanyName
from dbo.Suppliers as supp
where supp.SupplierID in (select pr.SupplierID
						  from dbo.Products as pr
						  where pr.UnitsInStock = 0);

select emp.LastName 
from dbo.Employees as emp
where emp.EmployeeID = (select ord.EmployeeID
						from dbo.Orders as ord
						group by ord.EmployeeID
						having COUNT(ord.OrderID) > 150)

select cust.ContactName 
from dbo.Customers as cust
where exists (select cust.CustomerID
			  from dbo.Orders as ord
			  where ord.CustomerID = cust.CustomerID
			  group by ord.EmployeeID
		      having COUNT(ord.OrderID) = 0)
