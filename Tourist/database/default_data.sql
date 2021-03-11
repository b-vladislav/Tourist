-- удалить все данные
delete from [dbo].[Rout]
delete from [dbo].[Point]
delete from [dbo].[Tour]
delete from [dbo].[Group]
delete from [dbo].[BusService]
delete from [dbo].[Service]
delete from [dbo].[Bus]
delete from [dbo].[Category]

-- сбросить счетчики автоинкремента
DBCC CHECKIDENT ('[Rout]', RESEED, 0)
GO 
DBCC CHECKIDENT ('[Point]', RESEED, 0)
GO
DBCC CHECKIDENT ('[Tour]', RESEED, 0)
GO
DBCC CHECKIDENT ('[Group]', RESEED, 0)
GO
DBCC CHECKIDENT ('[Service]', RESEED, 0)
GO
DBCC CHECKIDENT ('[Bus]', RESEED, 0)
GO
DBCC CHECKIDENT ('[Category]', RESEED, 0)
GO

-- вставить новые данные
insert into [dbo].[Service]
	select 'Питание', 120 union all --1
	select 'Развлечения', 180		--2
	
insert into [dbo].[Category]
	select 'Частный', 0 union all	--1
	select 'Фирменный 150', 150		--2
	
insert into	[dbo].[Bus]
	select 1, '000' union all	--1
	select 2, '111'	union all	--2
	select 2, '222'	union all	--3
	select 2, '333'	union all	--4
	select 2, '444'				--5

insert into	[dbo].[BusService]
	select 2, 1 union all		--1
	select 3, 1 union all		--2
	select 3, 2 union all		--3
	select 4, 2					--4

insert into [dbo].[Point]
	select '20210201', 'Пункт А', 100 union all		--1
	select '20210201', 'Пункт Б', 120 union all		--2
	select '20210201', 'Пункт В', 150 union all		--3
	select '20210201', 'Отель А', 500 union all		--4
	select '20210201', 'Отель Б', 100				--5
	
insert into [dbo].[Group]
	select 'Группа 1А' union all	--1
	select 'Группа 2Б' union all	--2
	select 'Группа 3В' union all	--3
	select 'Группа 4Г' union all	--4
	select 'Группа 5Ж'				--5
