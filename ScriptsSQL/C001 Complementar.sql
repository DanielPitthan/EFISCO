Use WebFrame
go

Alter table NFeXML.dbo.FileStorange Add Processado Bit
go
create index Ix_FileStorange_001 on  NFeXML.dbo.FileStorange(Processado)
go
create index Ix_NFeFiles_001 on NFeFiles  (DataInclusao)
go
create index IX_FileStorange_002 on NFeXML.dbo.FileStorange( DataInclusao)
go
Alter table NFeXML.dbo.FileStorange Add FileType varchar(10)
go
create index IX_FileStorange_003 on NFeXML.dbo.FileStorange( Processado, FileType)
go
create index IX_NFeFiles_002 on NFeFiles(Validado,Processada)
go
alter table NFeFiles add EmpresaId int
go
Alter table NFeFiles add constraint FK_NFeFiles_Empresa foreign key (EmpresaId) references Empresa(Id)
go
go
alter table NFeFiles alter column Arquivo varchar(255)
go
alter table NFeFiles alter column  Path  varchar(255)
go
alter table NFeFiles alter column  ChaveAcesso  varchar(44)
go
alter table NFeFiles alter column  NumeroNota  varchar(9)
go
alter table NFeFiles alter column  CnpjFornecedor  varchar(14)
go
alter table NFeFiles alter column  Fornecedor  varchar(255)
go

create table NFeFilesMensagens (
	Id int not null identity primary key,
	NFeFilesId int,
	DataInclusao DateTime default getdate(),
	Ativo bit,
	Texto varchar(255)
)
go
create index Ix_NFeFilesMensagens_001 on NFeFilesMensagens (NFeFilesId,Ativo) 
go
alter table NFeFilesMensagens add constraint FK_NFeFilesMensagens_NFeFiles foreign key (NFeFilesId) references NFeFiles(Id)
go
alter table NFeFiles add AutoValidado Bit Default 0
go