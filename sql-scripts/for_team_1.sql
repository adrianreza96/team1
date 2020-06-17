create table [dbo].[roles](
id int primary key,
rolename varchar(20) not null unique);

insert into roles(id, rolename) values
(1, 'guest'),
(2, 'customer'),
(3, 'kitchen_provider'),
(4, 'admin');

select * from roles;

CREATE TABLE [dbo].[users] (
id int primary key identity (1,1), 
firstname varchar(50) not null, 
lastname varchar(50) not null, 
email varchar(100) unique,
password varchar(50),
sec_question1 varchar(50), 
sec_question2 varchar(50),
role int default 1,
constraint fk_user_role foreign key(role) references roles(id) on delete set default
);


create table [dbo].[kitchens] (
kitchenid int primary key identity (1,1),
name varchar(50) not null,
userid int,
photo image,
opentime tinyint,
closetime tinyint,
check (opentime >=0 and opentime <=24),
check (closetime >=0 and closetime <=24),
check (opentime < closetime),
constraint fk_kitchen_user foreign key(userid) references users(id) on delete no action
);


create table [dbo].[menuitems] (
menuitemid int primary key identity (1,1),
name varchar(50) not null,
isvegen bit,
price money,
menutype varchar(50),
kitchenid int not null,
photo image,
ingredients varchar,
constraint fk_menuitem_kitchen foreign key(kitchenid) references kitchens(kitchenid) on delete cascade
);







);