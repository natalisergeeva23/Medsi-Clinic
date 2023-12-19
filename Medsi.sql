set ansi_padding on
go
set ansi_nulls on
go
set quoted_identifier on
go

create database [Medsi]
go
use [Medsi]
go

CREATE TABLE Tokens (
    token_id INT IDENTITY(1,1) PRIMARY KEY,
    token VARCHAR(200) NOT NULL,
    token_datetime DATETIME2 NOT NULL DEFAULT(SYSDATETIME())
);

/*Роль*/

create table [dbo].[Role]
(
	[ID_Role] [int] not null identity(1,1),
	[Name_Role] [varchar] (100) not null
	constraint [PK_Role] primary key clustered
	([ID_Role] ASC) on [PRIMARY],
	[Is_Deleted] BIT not null DEFAULT 0
)
go

insert into [dbo].[Role]([Name_Role])
values ('Администратор'),
('Врач'),
('Пациент'),
('Медсестра')
go

/*Статус записи*/

create table [dbo].[Status]
(
	[ID_Status] [int] not null identity(1,1),
	[Record_Status] [varchar] (20) not null
	constraint [PK_Status] primary key clustered
	([ID_Status] ASC) on [PRIMARY],
	constraint [UQ_Record_Status] unique ([Record_Status])
)
go

insert into [dbo].[Status]([Record_Status])
values ('Запись создана'),
('Запись не создана'),
('Запись отменена')
go

select  * from [dbo].[Status]
go

select [Record_Status] as "Статус записи" from [dbo].[Status]
go

/*Должность*/

create table [dbo].[Position]
(
	[ID_Position] [int] not null  identity(1,1),
	[Name_Position] [varchar] (50) not null
	constraint [PK_Position] primary key clustered
	([ID_Position] ASC) on [PRIMARY],
	constraint [CH_Name_Position] check ([Name_Position] like '%[А-Я]%' or [Name_Position] like '%[а-я]%')

)
go

insert into  [dbo].[Position]([Name_Position])
values ('Иммунолог'),
('Педиатор'),
('Стоматолог'),
('Эпидемиолог'),
('Гинеколог')
go

select [Name_Position] as "Должность" from [dbo].[Position]
order by [Name_Position] DESC
go

/*Пациенты*/

create table [dbo].[Patient]
(
		[ID_Patient] [int] not null  identity(1,1),
		[First_Name_Patient] [varchar] (30) not null,
		[Second_Name_Patient] [varchar] (30) not null,
		[Middle_Name_Patient] [varchar] (30) null default('-'),
		[Passport_Series] [varchar] (4) not null default ('[1-10]'),
		[Passport_Number] [varchar] (6) not null default ('[1-10]'),
		[SNILS] [varchar] (11) not null default ('[1-10]'),
		[Insurance_Policy] [varchar] (16) not null default ('[1-16]'),
		[Login_Patient] [varchar] (100) not null,
		[Password_Patient] [varchar] (100) not null,
		constraint [PK_Patient] primary key clustered
		([ID_Patient] ASC) on [PRIMARY]
)
go

insert into [dbo].[Patient]([First_Name_Patient], [Second_Name_Patient], [Middle_Name_Patient], [Passport_Series], [Passport_Number], [SNILS], [Insurance_Policy], [Login_Patient], [Password_Patient])
values ('Кудряшов', ' Панкрат', 'Алексеевич', '2723', '542988', '85399555566', '3985060076229274', 'Kud7!', 'Pass1'),
('Шилова', 'Александрина', 'Юрьевна', '8510', '174782', '27181162043', '5231895284261555', 'Shilov11@', 'Pass2'),
('Мясников', 'Никита', 'Михаилович', '0347', '268041',  '25894077017', '7008767750114203', 'Muse2!', 'Pass3'),
('Смирнов', 'Ираклий', 'Николаевич', '3187', '546415',  '12448558952', '8296375791542233', 'Smirnov03!', 'Pass4'),
('Кошелев', 'Константин', 'Александрович', '0771', '487191', '37868592818', '7366536500398070', 'Koshel90!', 'Pass5')
go




/*Сотрудники*/

create table [dbo].[Employee]
(
		[ID_Employee] [int] not null  identity(1,1),
		[First_Name_Emp] [varchar] (30) not null,
		[Second_Name_Emp] [varchar] (30) not null,
		[Middle_Name_Emp] [varchar] (30) null default('-'),
		[Login_Emp] [varchar] (100) not null,
		[Password_Emp] [varchar] (100) not null,
		[Position_ID] [int] not null,
		[Role_ID] [int] not null,
		[Salt] [varchar] (256) null,

		constraint [PK_Employee] primary key clustered
		([ID_Employee] ASC) on [PRIMARY],
		constraint [FK_Position_Employee] foreign key ([Position_ID])
		references [dbo].[Position] ([ID_Position]),
		constraint [FK_Role_Employee] foreign key ([Role_ID])
		references [dbo].[Role] ([ID_Role]),
		constraint [CH_First_Name_Emp] check ([First_Name_Emp] like '%[А-Я]%' or [First_Name_Emp] like '%[а-я]%'),
		constraint [CH_Second_Name_Emp] check ([Second_Name_Emp] like '%[А-Я]%' or [Second_Name_Emp] like '%[а-я]%'),
		constraint [CH_Middle_Name_Emp] check ([Middle_Name_Emp] like '%[А-Я]%' or [Middle_Name_Emp] like '%[а-я]%')
)
go

insert into [dbo].[Employee]([First_Name_Emp], [Second_Name_Emp], [Middle_Name_Emp], [Login_Emp], [Password_Emp], [Position_ID], [Role_ID])
values ('Потапов', 'Тимофей', 'Афанасьевич', 'Potap8!', 'PassPotap1', 1, 1),
('Ларионов', 'Игнатий', 'Юрьевич', 'Lar55@', 'PassIgn2', 2, 2),
('Никифоров', 'Николай', 'Акимович', 'Nikifr4!', 'PassNik3', 3, 3),
('Карпов', 'Афанасий', 'Саввеевич', 'Carpov44@', 'PassCarp4', 4, 4),
('Бурова', 'Барбара', 'Порфирьевна', 'Bubu23!', 'PassBar5', 5, 4)

go

select * from [dbo].[Employee]

/*Карта пациента*/

create table [dbo].[Patient_Card]
(
		[ID_Patient_Card] [int] not null  identity(1,1),
		[Card_Number_Patient] [varchar] (10) not null,
		[Date_Of_Creation] [date] not null default ( '0001-01-01'),
		[Name_Diseases] [varchar] (100) not null,
		[Patient_ID] [int] not null,
		[Employee_ID] [int] not null
		constraint [PK_Patient_Card] primary key clustered
		([ID_Patient_Card] ASC) on [PRIMARY],
		constraint [FK_Patient_Patient_Card] foreign key ([Patient_ID])
		references [dbo].[Patient] ([ID_Patient]),
		constraint [FK_Employee_Patient_Card] foreign key ([Employee_ID])
		references [dbo].[Employee] ([ID_Employee]),
		constraint [UQ_Card_Number] unique ([Card_Number_Patient]),
		constraint [CH_Card_Number] check ([Card_Number_Patient] like '%[0-10]%'),
		constraint [CH_Name_Diseases] check ([Name_Diseases] like '%[А-Я]%' or [Name_Diseases] like '%[а-я]%' or [Name_Diseases] like '%[a-z]%' or [Name_Diseases] like '%[A-Z]%' )

)
go

insert into [dbo].[Patient_Card]([Card_Number_Patient], [Date_Of_Creation], [Name_Diseases], [Patient_ID], [Employee_ID])
values ('6381656132', '2014-01-09', 'Астма', 1, 1),
('5077782430', '2000-04-23', 'Камни в почках', 2, 2),
('6304629358', '2016-03-10', 'Невралгия', 3, 3),
('4620175736', '2021-06-08', 'Остеопороз', 4, 4),
('5920315212', '2018-06-25', 'Язва желдука', 5, 5)
go

/*Справка*/

create table [dbo].[Synopsis]
(
		[ID_Synopsis] [int] not null identity(1,1),
		[Number_Of_Days] [varchar] (20) not null,
		[Patient_Card_ID] [int] not null
		constraint [PK_Synopsis] primary key clustered
		([ID_Synopsis] ASC) on [PRIMARY],
		constraint [FK_Patient_Card_Synopsis] foreign key ([Patient_Card_ID])
		references [dbo].[Patient_Card] ([ID_Patient_Card])

)
go

insert into [dbo].[Synopsis]([Number_Of_Days], [Patient_Card_ID])
values ('5 дней',1),
('10 дней',2),
('7 дней',3),
('3 дней',4),
('1 дней',5)
go

select *from  [dbo].[Synopsis]
go





/*Запись*/
create table [dbo].[Recording]
(
	[ID_Recording] [int] not null identity(1,1),
	[Record_Date] [date]  not null,
	[Record_Time] [time] not null,
	[Direction] [varchar] (255) not null,
	[Status_ID] [int] not null,
	[Patient_ID] [int] not null,
	[Employee_ID] [int] not null
	constraint [PK_Recording] primary key clustered
	([ID_Recording] ASC) on [PRIMARY],
	constraint [FK_Status_Recording] foreign key ([Status_ID])
	references [dbo].[Status] ([ID_Status]),
	constraint [FK_Patient_Recording] foreign key ([Patient_ID])
	references [dbo].[Patient] ([ID_Patient]),
	constraint [FK_Employee_Recording] foreign key ([Employee_ID])
	references [dbo].[Employee] ([ID_Employee])

)
go


insert into  [dbo].[Recording]([Record_Date],[Record_Time],[Direction], [Status_ID], [Patient_ID], [Employee_ID])
values ('2023-11-13', '12:00:00', 'Московская областная больница', 1, 1, 1),
('2023-11-25', '13:30:00', 'Венерологический центр', 2, 2, 2),
('2023-11-14', '16:00:00', 'Центр им. Логинова', 3, 3, 3),
('2023-12-01', '10:00:00', 'Поликлинника №4', 1, 4, 4),
('2023-12-13', '17:00:00', 'Больница №1', 2, 5, 5)
go