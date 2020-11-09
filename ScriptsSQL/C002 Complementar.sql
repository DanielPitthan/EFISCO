Use NFeXML
go
alter table infNFe alter column Id varchar(44)
go
alter table infNFe alter column versao varchar(50)
go
Create table EmitenteIntegrado(
	Id int not null identity primary key,
	DataInclusao datetime default(getdate()),
	IntegradaoTOTVS bit,
	EmitenteId int not null
)
go
alter table EmitenteIntegrado add constraint FK_EmitenteIntegrado_emit foreign key (EmitenteId) references emit (Id)
GO
alter table EmitenteIntegrado add CodigoTotvsEmpresaFilial varchar(20)
go

create table   ProdutoIntegrado(
	Id int not null identity primary key,
	DataInclusao datetime default(getdate()),
	IntegradaoTOTVS bit,
	ProdutoId int not null,
	CodigoTotvsEmpresaFilial varchar(20)
)
go
alter table ProdutoIntegrado add constraint FK_ProdutoIntegrado_prod foreign key (ProdutoId) references prod(Id)
go
alter table ProdutoIntegrado add CnpjFornecedor varchar(14)
go
 alter table prod alter column xProd varchar(254)
 go
 alter table prod alter column cProd varchar(254)
 go
 alter table prod alter column xPed varchar(254)
go
alter table ProdutoIntegrado add CodigoProdutoTOTVS varchar(15) 