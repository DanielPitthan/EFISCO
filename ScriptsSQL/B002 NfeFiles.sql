use WebFrame
go
if Exists (select 1 from sys.tables where name='NFeFiles')
begin 
	drop table NFeFiles
end 
create  table NFeFiles(
	Id int identity not null primary key,
	Arquivo varchar(max),
	Path varchar(max),
	ChaveAcesso varchar(max),
	Processada bit, 
	DataInclusao DateTime,
	DataProcessamento DateTime
)
go
alter table NFeFiles add NumeroNota varchar(max)
go
alter table NFeFiles add Serie int
go
alter table NFeFiles add CnpjFornecedor varchar(max)
go
alter table NFeFiles add Fornecedor varchar(max)
go
alter table NFeFiles add ValorTotal Decimal(18,4)
go
alter table NFeFiles add DataEmnissaoNfe DateTime2
go
alter table NFeFiles add Validado bit default(0)
