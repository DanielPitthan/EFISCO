use WebFrame
go
if Exists (select 1 from sys.tables where name='Parametro')
begin 
	drop table Parametro
end 
create table Parametro (
	Id int not null identity primary key,
	Codigo varchar(max),
	Descricao varchar(max),
	Valor nvarchar(Max)
)

go
insert into Parametro (Codigo,Descricao,Valor) select 'PATH_XML','Caminho dos arquivos XML','D:\Temp\XML COMBUSTIVEL'
where not exists (select 1 from Parametro where Codigo='PATH_XML')
