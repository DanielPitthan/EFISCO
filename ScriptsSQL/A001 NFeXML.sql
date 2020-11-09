use master
go
if exists (select 1 from sys.databases where name='NFeXML')
begin 
	drop database NFeXML
end
go
create database NFeXML
go
use NFeXML
go
create table NFe(
	Id int not null Identity primary key ,
	infNFeId int ,
	infNFeSuplId int,
	SignatureId int 
)

go
create table Signature (
	Id int not null identity primary key,
	SignedInfoId int ,
	SignatureValue varchar(Max),
	KeyInfoid int 
)
go
create table KeyInfo(
	Id int not null identity primary key,
	X509DataId int
)
go
create table X509Data(
	Id int not null identity primary key,
	X509Certificate varchar(max)
)
go
create table SignedInfo(
	Id int not null identity primary key,
	CanonicalizationMethodId int ,
	SignatureMethodId int, 
	ReferenceId int 
)

go 
create table Reference(
	Id int not null identity primary key,
	URI varchar(max),
	DigestMethodId int, 
	DigestValue varchar(max) 
)
go

create table Transform(
	Id int not null identity primary key,
	Algorithm varchar(max),
	ReferenceId int 
)
go
create table DigestMethod(
	Id int not null identity primary key,
	Algorithm varchar(max) 
)
go
create table SignatureMethod(
	Id int not null identity primary key,
	Algorithm varchar(max) 
)
go
create table CanonicalizationMethod(
	Id int not null identity primary key,
	Algorithm varchar(max) 
)
go

alter table NFe add constraint FK_NFeSignature foreign key (SignatureId) References Signature(Id)
go
alter table Signature add constraint FK_SignatureKeyInfo foreign key (KeyInfoId) References KeyInfo(Id)
go
alter table KeyInfo add constraint FK_KeyInfoKeyInfo foreign key (X509DataId) References X509Data(Id)
go
alter table Signature add constraint FK_SignatureSignedInfo foreign key (SignedInfoId) References SignedInfo(Id)
go
alter table SignedInfo add constraint FK_SignedInfoCanonicalizationMethod foreign key (CanonicalizationMethodId) references CanonicalizationMethod(Id)
go
alter table SignedInfo add constraint FK_SignedInfoReference foreign key (ReferenceId) references Reference(Id)
go
alter table SignedInfo add constraint FK_SignedInfoSignatureMethod foreign key (SignatureMethodId) references SignatureMethod(Id)
go
alter table Transform add constraint FK_TransformReference foreign key (ReferenceId) references Reference(Id)
go
alter table Reference add constraint FK_ReferenceDigestMethod foreign key (DigestMethodId) references DigestMethod(Id)
go

Create table infNFeSupl(
	Id int identity not null primary key,
	qrCode varchar(max), 
	urlChave varchar(max)
)
go
alter table NFe add constraint FK_NFeinfNFeSupl foreign key (infNFeSuplId) references infNFeSupl (Id)
go 

create table infNFe(
	   infNFeId int identity not null primary key	,        
       ideId int,
       emitId int,       
       avulsaId		int,
       destId		int,
       retiradaId	int,
       entregaId	int,
       totalId		int,
       transpId		int,
       cobrId		int,
       infAdicId	int,
       exportaId	int,
       compraId		int,
       canaId		int,
       infRespTecId	int
)
go
alter table infNFe add Id varchar(max)
go
create table ide(
		Id int identity not null primary key,
        cUF nvarchar(max),
        cNF nvarchar(max),
        natOp nvarchar(Max),
        IndicadorPagamento nvarchar(max),
        indPagSpecified bit,
        ModeloDocumento nvarchar(max),
        serie int,
        nNF bigint,
        dEmi DateTime,
        dSaiEnt DateTime,
        dhEmi DateTimeOffset,
        dhSaiEnt DateTimeOffset,
        tpNF nvarchar(max),
        idDest nvarchar(max),
        cMunFG bigint,
        tpImp  nvarchar(max),
        tpEmis  nvarchar(max),
        cDV int,
        tpAmb  nvarchar(max),
        finNFe  nvarchar(max),
        indFinal  nvarchar(max),
        indPres  nvarchar(max),
        procEmi  nvarchar(max),
        verProc  nvarchar(max),
        dhCont DateTimeOffset,        
        xJust   nvarchar(max)
)
go
alter table infNFe add constraint FK_infNFeide foreign key (ideId) references ide(Id)
go
alter table ide add mod nvarchar(max)
go
alter table ide add indPag nvarchar(max)
go
create table NFref(
	Id int identity not null primary key,
	ideId int,
	refNFe nvarchar(max)
)

alter table NFref add constraint FK_NFrefide foreign key (ideId) References ide(Id)
go

create table emit(
	Id int identity not null primary key,
	 CNPJ varchar(max),
	 CPF varchar(max),
	 xNome varchar(max),
	 xFant VARCHAR(max),
	 enderEmitId int ,
	 IE varchar(max),
	 IEST varchar(max),
	 IM varchar(max),
	 CNAE varchar(max),
	 CRT varchar(max)
)
go
alter table infNFe add constraint FK_infNFeemit foreign key(emitId) references emit(Id)
go
create table enderEmit
(
		 Id int identity not null primary key,
         xLgr varchar(max),  
         nro  varchar(max),
         xCpl varchar(max),
         xBairro  varchar(max),
		 cMun bigint,
		 xMun  varchar(max),
		 UF nvarchar(max),
		 CEP varchar(max),
         cPais int,
		 xPais varchar(max),
		fone bigint
)
go
alter table emit add constraint FK_emitenderEmit foreign key(enderEmitId) references enderEmit(Id)
go


create table dest(
	Id int identity not null primary key,
	 CNPJ varchar(max),
	 CPF varchar(max),
	 xNome varchar(max),
	 xFant VARCHAR(max),
	 enderDestId int ,
	 indIEDest nvarchar(max),
	 IE varchar(max),
	 ISUF varchar(max),
	 IM varchar(max),
	 emial varchar(max)
)
go
alter table dest drop column emial
alter table dest add idEstrangeiro int 
go
alter table dest add [email] varchar(max)
go
alter table infNFe add constraint FK_infNFedest foreign key(destId) references dest(Id)
go
create table enderDest
(
		 Id int identity not null primary key,
         xLgr varchar(max),  
         nro  varchar(max),
         xCpl varchar(max),
         xBairro  varchar(max),
		 cMun bigint,
		 xMun  varchar(max),
		 UF nvarchar(max),
		 CEP varchar(max),
         cPais int,
		 xPais varchar(max),
		fone bigint
)
go
alter table dest add constraint FK_destenderDest foreign key(enderDestId) references enderDest(Id)
go

create table retirada(
	 Id int identity not null primary key,
	 CNPJ varchar(max),
	 CPF varchar(max),
	 xNome varchar(max),
	 xLgr varchar(max),  
     nro  varchar(max),
     xCpl varchar(max),
     xBairro  varchar(max),
	 cMun bigint,
	 xMun  varchar(max),
	 UF nvarchar(max),
	 CEP varchar(max),
     cPais int,
	 xPais varchar(max),
	 xFant VARCHAR(max),
	 fone bigint,
	 enderDestId int ,
	 indIEDest nvarchar(max),
	 IE varchar(max),
	 ISUF varchar(max),
	 IM varchar(max),
	 emial varchar(max)
)
go
create table entrega(
	 Id int identity not null primary key,
	 CNPJ varchar(max),
	 CPF varchar(max),
	 xNome varchar(max),
	 xLgr varchar(max),  
     nro  varchar(max),
     xCpl varchar(max),
     xBairro  varchar(max),
	 cMun bigint,
	 xMun  varchar(max),
	 UF nvarchar(max),
	 CEP varchar(max),
     cPais int,
	 xPais varchar(max),
	 xFant VARCHAR(max),
	 fone bigint,
	 enderDestId int ,
	 indIEDest nvarchar(max),
	 IE varchar(max),
	 ISUF varchar(max),
	 IM varchar(max),
	 emial varchar(max)
)
go
alter table infNFe add constraint FK_infNFeretirada foreign key(retiradaId) references retirada(Id)
alter table infNFe add constraint FK_infNFeentrega foreign key(entregaId) references entrega(Id)
go

create table autXML (
   Id int identity not null primary key,
   CNPJ varchar(max),
   CPF varchar(max),
   infNFeId int 
	)
go
alter table autXML add constraint FK_autXMLinfNFe foreign key(infNFeId) references infNFe(infNFeId)
go

create table det(
   Id int identity not null primary key,
   nItem int,
   prodId int,
   impostoId int,
   impostoDevolId int,
   infAdProd varchar(max),
   infNFeId int 
)
go
alter table det add constraint FK_detinfNFe foreign key(infNFeId) references infNFe(infNFeId)
go
create table prod
(
	Id int identity not null primary key,
	cProd varchar(max),
	cEAN varchar(max),
	xProd varchar(max),
	NCM varchar(max),
	CEST varchar(max),
	indEscala varchar(max),
	indEscalaSpecified bit,
	CNPJFab varchar(max),
	cBenef varchar(max),
	EXTIPI varchar(max),
	CFOP varchar(max),
	uCom varchar(max),
	qCom decimal(18,4),
	vUnCom  decimal(18,4),
	vProd decimal(18,4),
	cEANTrib  varchar(max),
	uTrib varchar(max),
	qTrib decimal(18,4),
	vUnTrib decimal(18,4),
	vFrete decimal(18,4),
	vSeg decimal(18,4),
	vDesc decimal(18,4),
	vOutro decimal(18,4),
	indTot int, 
	xPed varchar(max),
	nItemPed int,
	nFCI varchar(max),
	nRECOPI varchar(max),
)
go
alter table det add constraint FK_detprod foreign key (prodId) references prod(Id)
go
create table comb(
	Id int identity not null primary key,
	cProdANP varchar(max),
	pMixGN decimal(18,4),
	pMixGNSpecified bit,
	descANP varchar(max),
	pGLP decimal(18,4),
	pGLPSpecified bit,
	pGNn decimal(18,4),
	pGNi decimal(18,4),
	pGNiSpecified bit,
	vPart decimal(18,4),
	vPartSpecified bit ,
	CODIF varchar(max),
	qTemp decimal(18,4),
	qTempSpecified bit,
	UFCons varchar(max),
	CIDEId int
)
go
create table CIDE(
	Id int identity not null primary key,
		qBCProd decimal(18,4),      
        vAliqProd decimal(18,4),       
        vCIDE decimal(18,4)        
)
go
alter table comb add constraint FK_combCIDE foreign key (CIDEId) references CIDE(Id)
go
alter table prod add comId int
go
alter table prod add constraint FK_prodcomb foreign key (comId) references comb(Id)
go
create table imposto(
	Id int identity not null primary key,
	vTotTrib decimal(18,4),
	pICMS         decimal(18,4),
	vICMS		  decimal(18,4),
	pICMSST		  decimal(18,4),
	vICMSST		  decimal(18,4),
	pISSQN		  decimal(18,4),
	vISSQN		  decimal(18,4),
	pIPI		  decimal(18,4),
	vIPI		  decimal(18,4),
	pII			  decimal(18,4),
	vII			  decimal(18,4),
	pPIS		  decimal(18,4),
	vPIS		  decimal(18,4),
	pPISST		  decimal(18,4),
	vPISST		  decimal(18,4),
	pCOFINS		  decimal(18,4),
	vCOFINS		  decimal(18,4),
	pCOFINSST	  decimal(18,4),
	vCOFINSST	  decimal(18,4),
	pICMSUFDest	  decimal(18,4),
	vICMSUFDest	  decimal(18,4)
)
go
alter table det add constraint FK_detimposto foreign key  (impostoId) references imposto (Id)
go
create table impostoDevol(
	Id int identity not null primary key,
	pDevol decimal(18,4),
	pIPI		  decimal(18,4),
	vIPI		  decimal(18,4)
)
go
create table  total(
	Id int identity not null primary key,
	ICMSTotId int,
	ISSQNtotId int,
	retTribId  int
)
go
alter table infNFe add constraint FK_infNFe_total foreign key (totalId) references total(Id)
go

create table retTrib(
	Id int identity not null primary key,
		vRetPis  decimal(18,4),
        vRetCofins  decimal(18,4),
        vRetCsll  decimal(18,4),
        vBcirrf  decimal(18,4),
        vIrrf  decimal(18,4),
        vBcRetPrev  decimal(18,4),
        vRetPrev  decimal(18,4)
)
go
alter table total add constraint FK_total_retTrib foreign key (retTribId) references retTrib(Id)
go
create table ICMSTot(
	Id int identity not null primary key,
	vBc decimal (18,4),
	vIcms decimal (18,4),
	vIcmsDeson decimal (18,4),
	vBcst decimal (18,4),
	vSt decimal (18,4),
	vProd decimal (18,4),
	vFrete decimal (18,4),
	vSeg decimal (18,4),
	vDesc decimal (18,4),
	vIi decimal (18,4),
	vIpi decimal (18,4),
	vPis decimal (18,4),
	vCofins decimal (18,4),
	vOutro decimal (18,4),
	vNf decimal (18,4),
	vTotTrib decimal (18,4),
	vFcpufDest decimal (18,4),
	vIcmsufDest decimal (18,4),
	vIcmsufRemet decimal (18,4),
	vFcp decimal (18,4),
	vFcpst decimal (18,4),
	vFcpstRet decimal (18,4),
	vIpiDevol decimal (18,4),
)
go
alter table total add constraint FK_total_ICMSTot foreign key (ICMSTotId) references ICMSTot(id)
go
create table ISSQNtot(
	Id int identity not null primary key,
	vServ decimal (18,4),
	vBc decimal (18,4),
	vIss decimal (18,4),
	vPis decimal (18,4),
	vCofins decimal (18,4),
	vDeducao decimal (18,4),
	vOutro decimal (18,4),
	vDescIncond decimal (18,4),
	vDescCond decimal (18,4),
	vIssRet decimal (18,4)
)
go
alter table total add constraint FK_total_ISSQNtotId foreign key (ISSQNtotId) references ISSQNtot(id)
go
create table transp(
	Id int identity not null primary key,
	modFrete varchar(max),
	modFreteSpecified bit,
	transportaId int,
	retTranspId int,
	veicTranspId int,
	vagao varchar(30),
	balsa varchar(30)
)
go
alter table infNFe add constraint FK_infNFetransp foreign key (transpId) references transp(Id)
go
create table transporta(
	Id int identity not null primary key,
	CNPJ varchar(max),
	CPF varchar(max),
	xNome varchar(max),
	IE varchar(max),
	xEnder varchar(max),
	xMun varchar(max),
	UF varchar(max)
)
go
alter table transp add constraint FKtransptransportadora foreign key (transportaId) references transporta(id)
go
create table retTransp(
		Id int identity not null primary key,
		vServ  decimal(18,4),
        vBcRet  decimal(18,4),
        pIcmsRet  decimal(18,4),
        vIcmsRet  decimal(18,4)
)
go
alter table transp add constraint FKtranspretTransp foreign key (retTranspId) references retTransp(id)
go
create table veicTransp(
	Id int identity not null primary key,
	placa varchar(max),
	UF varchar(max),
	RNTC varchar(max)
)
go
alter table transp add constraint FKtranspveicTransp foreign key (veicTranspId) references veicTransp(id)
go
create table reboque(
	Id int identity not null primary key,
	placa varchar(max),
	UF varchar(max),
	RNTC varchar(max),
	transpId int
)
go
alter table reboque add constraint FKreboquetransp foreign key (transpId) references transp(id)
go 
create table vol(
		Id int identity not null primary key,
		qVol int,
        esp varchar(max),
        marca varchar(max),
        nVol varchar(max),
        pesoL decimal(18,4),
        pesoB decimal(18,4),
		transpId int
)
go
alter table vol add constraint FKvoltransp foreign key (transpId) references transp(Id)
go
create table cobr(
	Id int identity not null primary key,
	fatId int,
	dupId int
)
go
alter  table cobr drop column dupId
go
alter table infNFe add constraint FKinfNFecobr foreign key (cobrId) references cobr(Id)
go

create table fat(
	Id int identity not null primary key,
	nFat varchar(max),
	vOrig decimal(18,4),
    vDesc decimal(18,4),
    vLiq decimal(18,4)
)
go
alter table cobr add constraint FKcobrfat foreign key (fatId) references fat(Id)
go
create table dup(
	Id int identity not null primary key,
	vDup decimal(18,4),
	nDup varchar(max),
	dVenc datetime,
	cobrId int
)
go
alter table dup add constraint FKdupcobr foreign key (cobrId) references cobr(Id)
go
create table pag(
	Id int identity not null primary key,
	vPag  decimal(18,4),
    vTroco  decimal(18,4),
	vTrocoSpecified bit,
	tPag nvarchar(max),
	vPagSpecified bit,
	infNFeId int 
)
go
alter table pag add constraint FKpaginfNFe foreign key (infNFeId) references infNFe(infNFeId)
go
create table infAdic(
	Id int identity not null primary key,
	infAdFisco varchar(max),
	infCpl varchar(max)
)
go
alter table infNFe add constraint FKinfNFeinfAdic foreign key (infAdicId) references infAdic(Id)
go
create table obsCont(
	Id int identity not null primary key,	
	xCampo  varchar(max),
	xTexto  varchar(max),
	infAdicId int
)
go
create table obsFisco(
	Id int identity not null primary key,	
	xCampo  varchar(max),
	xTexto  varchar(max),
	infAdicId int
)
go
alter table obsCont add constraint FKobsContinfAdic foreign key (infAdicId) references infAdic(Id)
alter table obsFisco add constraint FKobsFiscoinfAdic foreign key (infAdicId) references infAdic(Id)
go
create table procRef(
	Id int identity not null primary key,	
	nProc varchar(max),
	indProc nvarchar(max),
	infAdicId int
)
go
alter table procRef add constraint FKprocRefinfAdic foreign key (infAdicId) references infAdic(Id)
go
create table compra(
	Id int identity not null primary key,
	xNEmp varchar(max),
	xPed varchar(max),
	xCont varchar(max)
)
go
alter table infNFe add constraint FKinfNFecompra foreign key (compraId) references compra(Id)
go
alter table imposto add IcmsId int 
go

create table  ICMS(
	Id int not null identity primary key,
	ICMSBasicoId int
)
go
alter table imposto add constraint FK_imposto_ICMS foreign key (IcmsId) references ICMS (Id)
go
alter table ICMS add ICMS00Id		int
alter table ICMS add ICMS10Id		int
alter table ICMS add ICMS20Id		int
alter table ICMS add ICMS30Id		int
alter table ICMS add ICMS40Id		int
alter table ICMS add ICMS51Id		int
alter table ICMS add ICMS60Id		int
alter table ICMS add ICMS70Id		int
alter table ICMS add ICMS90Id		int
alter table ICMS add ICMSPartId		int
alter table ICMS add ICMSSTId		int
alter table ICMS add ICMSSN101Id	int
alter table ICMS add ICMSSN102Id	int
alter table ICMS add ICMSSN201Id	int
alter table ICMS add ICMSSN202Id	int
alter table ICMS add ICMSSN500Id	int
alter table ICMS add ICMSSN900Id	int


go
create table ICMS00(
	Id int not null identity primary key,
	vBc decimal(18,4),
	pIcms decimal(18,4),
	vIcms decimal(18,4),
	pFcp decimal(18,4),
	vFcp decimal(18,4),
	orig nvarchar(max),
	CST nvarchar(max),
	modBC  nvarchar(max),
	pFCPSpecified bit,
	vFCPSpecified bit
)
go
create table ICMS10(
	Id int not null identity primary key,
	vBc decimal(18,4),
    pIcms decimal(18,4),
    vIcms decimal(18,4),
    pMvast decimal(18,4),
    pRedBcst decimal(18,4),
    vBcst decimal(18,4),
    pIcmsst decimal(18,4),
    vIcmsst decimal(18,4),
    pFcp decimal(18,4),
    vFcp decimal(18,4),
    vBcfcp decimal(18,4),
    vBcfcpst decimal(18,4),
    pFcpst decimal(18,4),
    vFcpst decimal(18,4),
	orig nvarchar(max),
	CST nvarchar(max),
	modBC  nvarchar(max),
	vFCPSTSpecified bit,
	ShouldSerializepMVAST bit,
	ShouldSerializepRedBCST bit	
)
go
create table ICMS20(
	Id int not null identity primary key,
	vBc decimal(18,4),
    pIcms decimal(18,4),
    vIcms decimal(18,4),
    pMvast decimal(18,4),
    pRedBcst decimal(18,4),
    vBcst decimal(18,4),
    pIcmsst decimal(18,4),
    vIcmsst decimal(18,4),
    pFcp decimal(18,4),
    vFcp decimal(18,4),
    vBcfcp decimal(18,4),
    vBcfcpst decimal(18,4),
    pFcpst decimal(18,4),
    vFcpst decimal(18,4),
	orig nvarchar(max),
	CST nvarchar(max),
	modBC  nvarchar(max),
	vFCPSTSpecified bit,
	ShouldSerializepMVAST bit,
	ShouldSerializepRedBCST bit	
)
go
create table ICMS30(
	Id int not null identity primary key,
	vBc decimal(18,4),
    pIcms decimal(18,4),
    vIcms decimal(18,4),
    pMvast decimal(18,4),
    pRedBcst decimal(18,4),
    vBcst decimal(18,4),
    pIcmsst decimal(18,4),
    vIcmsst decimal(18,4),
    pFcp decimal(18,4),
    vFcp decimal(18,4),
    vBcfcp decimal(18,4),
    vBcfcpst decimal(18,4),
    pFcpst decimal(18,4),
    vFcpst decimal(18,4),
	orig nvarchar(max),
	CST nvarchar(max),
	modBC  nvarchar(max),
	vFCPSTSpecified bit,
	ShouldSerializepMVAST bit,
	ShouldSerializepRedBCST bit	
)
go
create table ICMS40(
	Id int not null identity primary key,
	vBc decimal(18,4),
    pIcms decimal(18,4),
    vIcms decimal(18,4),
    pMvast decimal(18,4),
    pRedBcst decimal(18,4),
    vBcst decimal(18,4),
    pIcmsst decimal(18,4),
    vIcmsst decimal(18,4),
    pFcp decimal(18,4),
    vFcp decimal(18,4),
    vBcfcp decimal(18,4),
    vBcfcpst decimal(18,4),
    pFcpst decimal(18,4),
    vFcpst decimal(18,4),
	orig nvarchar(max),
	CST nvarchar(max),
	modBC  nvarchar(max),
	vFCPSTSpecified bit,
	ShouldSerializepMVAST bit,
	ShouldSerializepRedBCST bit	
)
go
create table ICMS51(
	Id int not null identity primary key,
	vBc decimal(18,4),
    pIcms decimal(18,4),
    vIcms decimal(18,4),
    pMvast decimal(18,4),
    pRedBcst decimal(18,4),
    vBcst decimal(18,4),
    pIcmsst decimal(18,4),
    vIcmsst decimal(18,4),
    pFcp decimal(18,4),
    vFcp decimal(18,4),
    vBcfcp decimal(18,4),
    vBcfcpst decimal(18,4),
    pFcpst decimal(18,4),
    vFcpst decimal(18,4),
	orig nvarchar(max),
	CST nvarchar(max),
	modBC  nvarchar(max),
	vFCPSTSpecified bit,
	ShouldSerializepMVAST bit,
	ShouldSerializepRedBCST bit	
)
go
create table ICMS60(
	Id int not null identity primary key,
	vBc decimal(18,4),
    pIcms decimal(18,4),
    vIcms decimal(18,4),
    pMvast decimal(18,4),
    pRedBcst decimal(18,4),
    vBcst decimal(18,4),
    pIcmsst decimal(18,4),
    vIcmsst decimal(18,4),
    pFcp decimal(18,4),
    vFcp decimal(18,4),
    vBcfcp decimal(18,4),
    vBcfcpst decimal(18,4),
    pFcpst decimal(18,4),
    vFcpst decimal(18,4),
	orig nvarchar(max),
	CST nvarchar(max),
	modBC  nvarchar(max),
	vFCPSTSpecified bit,
	ShouldSerializepMVAST bit,
	ShouldSerializepRedBCST bit	
)
go
create table ICMS70(
	Id int not null identity primary key,
	vBc decimal(18,4),
    pIcms decimal(18,4),
    vIcms decimal(18,4),
    pMvast decimal(18,4),
    pRedBcst decimal(18,4),
    vBcst decimal(18,4),
    pIcmsst decimal(18,4),
    vIcmsst decimal(18,4),
    pFcp decimal(18,4),
    vFcp decimal(18,4),
    vBcfcp decimal(18,4),
    vBcfcpst decimal(18,4),
    pFcpst decimal(18,4),
    vFcpst decimal(18,4),
	orig nvarchar(max),
	CST nvarchar(max),
	modBC  nvarchar(max),
	vFCPSTSpecified bit,
	ShouldSerializepMVAST bit,
	ShouldSerializepRedBCST bit	
)
go
create table ICMS90(
	Id int not null identity primary key,
	vBc decimal(18,4),
    pIcms decimal(18,4),
    vIcms decimal(18,4),
    pMvast decimal(18,4),
    pRedBcst decimal(18,4),
    vBcst decimal(18,4),
    pIcmsst decimal(18,4),
    vIcmsst decimal(18,4),
    pFcp decimal(18,4),
    vFcp decimal(18,4),
    vBcfcp decimal(18,4),
    vBcfcpst decimal(18,4),
    pFcpst decimal(18,4),
    vFcpst decimal(18,4),
	orig nvarchar(max),
	CST nvarchar(max),
	modBC  nvarchar(max),
	vFCPSTSpecified bit,
	ShouldSerializepMVAST bit,
	ShouldSerializepRedBCST bit	
)
go

create table ICMSPart(
	Id int not null identity primary key,
	vBc decimal(18,4),
    pRedBc decimal(18,4),
    pIcms decimal(18,4),
    vIcms decimal(18,4),
    pMvast decimal(18,4),
    pRedBcst decimal(18,4),
    vBcst decimal(18,4),
    pIcmsst decimal(18,4),
    vIcmsst decimal(18,4),
    pBcOp decimal(18,4),
	orig nvarchar(max),
	CST nvarchar(max),
	modBC  nvarchar(max),
	vFCPSTSpecified bit,
	ShouldSerializepMVAST bit,
	ShouldSerializepRedBCST bit	
)
go

create table ICMSST(
	Id int not null identity primary key,
	 vBcstRet decimal(18,4),
     vIcmsstRet decimal(18,4),
     vBcstDest decimal(18,4),
     vIcmsstDest decimal(18,4),
     vIcmsSubstituto decimal(18,4),
     pST decimal(18,4),
     vBcfcpstRet decimal(18,4),
     pFcpstRet decimal(18,4),
     vFcpstRet decimal(18,4),
     pRedBCEfet decimal(18,4),
     vBCEfet decimal(18,4),
     pICMSEfet decimal(18,4),
     vICMSEfet decimal(18,4),
	orig nvarchar(max),
	CST nvarchar(max),
	modBC  nvarchar(max),
	vFCPSTSpecified bit,
	ShouldSerializepMVAST bit,
	ShouldSerializepRedBCST bit	
)
go

create table ICMSSN101(
	Id int not null identity primary key,
	 pCredSn decimal(18,4),
     vCredIcmssn decimal(18,4),    
	orig nvarchar(max),
	CST nvarchar(max),
	modBC  nvarchar(max),
	vFCPSTSpecified bit,
	ShouldSerializepMVAST bit,
	ShouldSerializepRedBCST bit	
)
go
create table ICMSSN102(
	Id int not null identity primary key,
	 pCredSn decimal(18,4),
     vCredIcmssn decimal(18,4),    
	orig nvarchar(max),
	CST nvarchar(max),
	modBC  nvarchar(max),
	vFCPSTSpecified bit,
	ShouldSerializepMVAST bit,
	ShouldSerializepRedBCST bit	
)
go
create table ICMSSN201(
	Id int not null identity primary key,
	 pMvast decimal(18,4),
	 pRedBcst decimal(18,4),
	vBcst decimal(18,4),
	pIcmsst decimal(18,4),
	vIcmsst decimal(18,4),
	pCredSn decimal(18,4),
	vCredIcmssn decimal(18,4),
	 vBcfcpst decimal(18,4),
	 pFcpst decimal(18,4),
	 vFcpst decimal(18,4),
	orig nvarchar(max),
	CST nvarchar(max),
	modBC  nvarchar(max),
	vFCPSTSpecified bit,
	ShouldSerializepMVAST bit,
	ShouldSerializepRedBCST bit	
)
go
create table ICMSSN202(
	Id int not null identity primary key,
	 pMvast decimal(18,4),
	 pRedBcst decimal(18,4),
	vBcst decimal(18,4),
	pIcmsst decimal(18,4),
	vIcmsst decimal(18,4),
	pCredSn decimal(18,4),
	vCredIcmssn decimal(18,4),
	 vBcfcpst decimal(18,4),
	 pFcpst decimal(18,4),
	 vFcpst decimal(18,4),
	orig nvarchar(max),
	CST nvarchar(max),
	modBC  nvarchar(max),
	vFCPSTSpecified bit,
	ShouldSerializepMVAST bit,
	ShouldSerializepRedBCST bit	
)
go
create table ICMSSN500(
	Id int not null identity primary key,
	 pMvast decimal(18,4),
	 pRedBcst decimal(18,4),
	vBcst decimal(18,4),
	pIcmsst decimal(18,4),
	vIcmsst decimal(18,4),
	pCredSn decimal(18,4),
	vCredIcmssn decimal(18,4),
	 vBcfcpst decimal(18,4),
	 pFcpst decimal(18,4),
	 vFcpst decimal(18,4),
	orig nvarchar(max),
	CST nvarchar(max),
	modBC  nvarchar(max),
	vFCPSTSpecified bit,
	ShouldSerializepMVAST bit,
	ShouldSerializepRedBCST bit	
)
go

create table ICMSSN900(
	Id int not null identity primary key,
	 pMvast decimal(18,4),
	 pRedBcst decimal(18,4),
	vBcst decimal(18,4),
	pIcmsst decimal(18,4),
	vIcmsst decimal(18,4),
	pCredSn decimal(18,4),
	vCredIcmssn decimal(18,4),
	 vBcfcpst decimal(18,4),
	 pFcpst decimal(18,4),
	 vFcpst decimal(18,4),
	orig nvarchar(max),
	CST nvarchar(max),
	modBC  nvarchar(max),
	vFCPSTSpecified bit,
	ShouldSerializepMVAST bit,
	ShouldSerializepRedBCST bit	
)
go
alter table ICMS add constraint FK_ICMS_ICMS00     foreign key ( ICMS00Id	) references ICMS00(Id)	
alter table ICMS add constraint FK_ICMS_ICMS10	   foreign key ( ICMS10Id	) references ICMS10(Id)	
alter table ICMS add constraint FK_ICMS_ICMS20	   foreign key ( ICMS20Id	) references ICMS20(Id)	
alter table ICMS add constraint FK_ICMS_ICMS30	   foreign key ( ICMS30Id	) references ICMS30(Id)	
alter table ICMS add constraint FK_ICMS_ICMS40	   foreign key ( ICMS40Id	) references ICMS40(Id)	
alter table ICMS add constraint FK_ICMS_ICMS51	   foreign key ( ICMS51Id	) references ICMS51(Id)	
alter table ICMS add constraint FK_ICMS_ICMS60	   foreign key ( ICMS60Id	) references ICMS60(Id)	
alter table ICMS add constraint FK_ICMS_ICMS70	   foreign key ( ICMS70Id	) references ICMS70(Id)	
alter table ICMS add constraint FK_ICMS_ICMS90	   foreign key ( ICMS90Id	) references ICMS90(Id)	
alter table ICMS add constraint FK_ICMS_ICMSPart   foreign key ( ICMSPartId	) references ICMSPart(Id)	
alter table ICMS add constraint FK_ICMS_ICMSST	   foreign key ( ICMSSTId	) references ICMSST(Id)	
alter table ICMS add constraint FK_ICMS_ICMSSN101  foreign key ( ICMSSN101Id) references ICMSSN101(Id)
alter table ICMS add constraint FK_ICMS_ICMSSN102  foreign key ( ICMSSN102Id) references ICMSSN102(Id)
alter table ICMS add constraint FK_ICMS_ICMSSN201  foreign key ( ICMSSN201Id) references ICMSSN201(Id)
alter table ICMS add constraint FK_ICMS_ICMSSN202  foreign key ( ICMSSN202Id) references ICMSSN202(Id)
alter table ICMS add constraint FK_ICMS_ICMSSN500  foreign key ( ICMSSN500Id) references ICMSSN500(Id)
alter table ICMS add constraint FK_ICMS_ICMSSN900  foreign key ( ICMSSN900Id) references ICMSSN900(Id)
go
create table IPITrib(
Id int not null identity primary key,
	vBc decimal(18,4),
	pIpi decimal(18,4),
	qUnid decimal(18,4),
	vUnid decimal(18,4),
	vIpi decimal(18,4)
)
go
create table IPI(
	Id int not null identity primary key,
	clEnq varchar(max),
	CNPJProd varchar(max),
	cSelo varchar(max),
	qSelo int, 
	cEnq varchar(max),
	IPITribId int,
	IPINTId int
)
go
alter table imposto add IPIId int
go

alter table imposto add constraint FK_imposto_IPI foreign key (IPIId) references IPI(Id)
go
alter table IPI add constraint FK_IPI_IPITrib foreign key (IPITribId) references IPITrib(Id)
go
Create table IPINT(
	Id int not null identity primary key,
	CST nvarchar(max)
)
go
alter table IPI add constraint FK_IPI_IPINT foreign key (IPINTId) references IPINT(Id)
go
create table COFINS(
	Id int not null identity primary key,
	COFINSAliqId int,
	COFINSQtdeId int,
	COFINSNTId int,
	COFINSOutrId int,
	)
go
alter table imposto add COFINSId int
go
alter table imposto add constraint FK_imposto_COFINS foreign key (COFINSId) references COFINS(Id)
go
create table COFINSAliq(
	Id int not null identity primary key,
	vBc decimal(18,4),
	pCofins decimal(18,4),
	vCofins decimal(18,4),
	qBcProd decimal(18,4),
	vAliqProd decimal(18,4),
	CST nvarchar(max)
)
go
create table COFINSQtde(
	Id int not null identity primary key,
	vBc decimal(18,4),
	pCofins decimal(18,4),
	vCofins decimal(18,4),
	qBcProd decimal(18,4),
	vAliqProd decimal(18,4),
	CST nvarchar(max)
)
go
create table COFINSNT(
	Id int not null identity primary key,
	vBc decimal(18,4),
	pCofins decimal(18,4),
	vCofins decimal(18,4),
	qBcProd decimal(18,4),
	vAliqProd decimal(18,4),
	CST nvarchar(max)
)
go
create table COFINSOutr(
	Id int not null identity primary key,
	vBc decimal(18,4),
	pCofins decimal(18,4),
	vCofins decimal(18,4),
	qBcProd decimal(18,4),
	vAliqProd decimal(18,4),
	CST nvarchar(max)
)
go
	alter table COFINS ADD CONSTRAINT FK_COFINS_COFINSAliqId FOREIGN KEY (COFINSAliqId) REFERENCES COFINSAliq(Id) 
	alter table COFINS ADD CONSTRAINT FK_COFINS_COFINSQtdeId FOREIGN KEY (COFINSQtdeId) REFERENCES COFINSQtde(Id) 
	alter table COFINS ADD CONSTRAINT FK_COFINS_COFINSNTId	 FOREIGN KEY (COFINSNTId ) REFERENCES COFINSNT(Id)	
	alter table COFINS ADD CONSTRAINT FK_COFINS_COFINSOutrId FOREIGN KEY (COFINSOutrId) REFERENCES COFINSOutr(Id) 
go

create table PIS (
	Id int not null identity primary key,
	PISAliqId int,
	PISQtdeId int,
	PISNTId	  int,
	PISOutrId int
)
go
alter table imposto add PISId int
go
alter table imposto add constraint FK_imposto_PIS foreign key (PISId) references PIS(Id)
go
create table PISAliq(
	Id int not null identity primary key,
	vBc  decimal(18,4),
	pPis decimal(18,4),
	vPis decimal(18,4),
	qBcProd decimal(18,4),
	vAliqProd decimal(18,4),	
	CST nvarchar(max)
)
go
create table PISQtde(
	Id int not null identity primary key,
	vBc  decimal(18,4),
	pPis decimal(18,4),
	vPis decimal(18,4),
	qBcProd decimal(18,4),
	vAliqProd decimal(18,4),	
	CST nvarchar(max)
)
go
create table PISNT(
	Id int not null identity primary key,
	vBc  decimal(18,4),
	pPis decimal(18,4),
	vPis decimal(18,4),
	qBcProd decimal(18,4),
	vAliqProd decimal(18,4),	
	CST nvarchar(max)
)

create table PISOutr(
	Id int not null identity primary key,
	vBc  decimal(18,4),
	pPis decimal(18,4),
	vPis decimal(18,4),
	qBcProd decimal(18,4),
	vAliqProd decimal(18,4),	
	CST nvarchar(max)
)
go
 alter table PIS add constraint		FK_PIS_PISAliqId	foreign key (PISAliqId)  references PISAliq(Id)
 alter table PIS add constraint		FK_PIS_PISQtdeId 	foreign key (PISQtdeId)  references PISQtde(Id) 
 alter table PIS add constraint		FK_PIS_PISNTId	  	foreign key (PISNTId) 	  references PISNT(Id)	 
 alter table PIS add constraint		FK_PIS_PISOutrId 	foreign key (PISOutrId)  references PISOutr(Id) 
 go
 alter table imposto add [ICMSUFDestId] int 
 go
 alter table imposto add IIId int
 go
 alter table imposto add ISSQNId int
 go
 alter table infNFe add versao varchar(max)
 go
 alter table entrega drop column emial
 go
 alter table entrega add email varchar(max)
 go

 create table FileStorange(
	Id int not null identity primary key,
	FileName varchar(Max),
	Path varchar(Max),
	MD5 varchar(125),
	DataInclusao DateTime,
	UsuarioId int,
	FileStornageUnique uniqueidentifier 
 )
 go
 alter table FileStorange add OriginalFileName varchar(Max)
 go
 alter table FileStorange add XmlString varchar(Max)
 go 
 alter table emit alter column CRT int
 go
 alter table ide alter column cUF int
 go
 alter table ide alter column IndicadorPagamento int
 go
alter table ide alter column ModeloDocumento int
alter table ide alter column tpNF     int 
alter table ide alter column idDest   int 
alter table ide alter column tpImp    int 
alter table ide alter column tpEmis   int 
alter table ide alter column tpAmb    int 
alter table ide alter column finNFe   int 
alter table ide alter column indFinal int 
alter table ide alter column indPres  int 
alter table ide alter column procEmi  int 
alter table ide alter column indPag   int
alter table ide alter column mod   int
go
alter table dest alter column indIEDest int 
go
alter table retirada alter column CEP bigint 
alter table retirada drop  column emial  
alter table retirada add email  varchar(max)
go
alter table entrega alter column CEP bigint
alter table entrega drop  column email  
alter table entrega add email  varchar(max)
go
alter table impostoDevol add IPIId int 
go
alter table impostoDevol add constraint FK_impostoDevol_IPI foreign key (IPIId )references IPI(Id)
go
create table ICMSUFDest(
	Id int not null identity primary key,
	vBcufDest decimal(18,4),
	pFcpufDest decimal(18,4),
	pIcmsufDest decimal(18,4),
	pIcmsInter decimal(18,4),
	pIcmsInterPart decimal(18,4),
	vFcpufDest decimal(18,4),
	vIcmsufDest decimal(18,4),
	vIcmsufRemet decimal(18,4),
	vBcfcpufDest decimal(18,4),
	vBCFCPUFDestSpecified bit,
	pFCPUFDestSpecified bit
)
alter table imposto add constraint FK_imposto_ICMSUFDestId foreign key (ICMSUFDestId) references ICMSUFDest(Id)

go
	alter table ICMS00 alter column orig	int
	alter table ICMS00 alter column CST		int
	alter table ICMS00 alter column modBC	int
	
	alter table ICMS10 alter column orig	int
	alter table ICMS10 alter column CST		int
	alter table ICMS10 alter column modBC	int

	alter table ICMS20 alter column orig	int
	alter table ICMS20 alter column CST		int
	alter table ICMS20 alter column modBC	int
	
	alter table ICMS30 alter column orig	int
	alter table ICMS30 alter column CST		int
	alter table ICMS30 alter column modBC	int

	alter table ICMS40 alter column orig	int
	alter table ICMS40 alter column CST		int
	alter table ICMS40 alter column modBC	int
	
	alter table ICMS51 alter column orig	int
	alter table ICMS51 alter column CST		int
	alter table ICMS51 alter column modBC	int

	alter table ICMS60 alter column orig	int
	alter table ICMS60 alter column CST		int
	alter table ICMS60 alter column modBC	int

	alter table ICMS70 alter column orig	int
	alter table ICMS70 alter column CST		int
	alter table ICMS70 alter column modBC	int

	alter table ICMS90 alter column orig	int
	alter table ICMS90 alter column CST		int
	alter table ICMS90 alter column modBC	int


	alter table ICMSPart alter column orig	int
	alter table ICMSPart alter column CST	int
	alter table ICMSPart alter column modBC	int

	alter table ICMSST alter column orig	int
	alter table ICMSST alter column CST	int
	alter table ICMSST alter column modBC	int

	alter table ICMSSN101 alter column orig	int
	alter table ICMSSN101 alter column CST		int
	alter table ICMSSN101 alter column modBC	int

	alter table ICMSSN102 alter column orig	int
	alter table ICMSSN102 alter column CST		int
	alter table ICMSSN102 alter column modBC	int

	alter table ICMSSN201 alter column orig	int
	alter table ICMSSN201 alter column CST		int
	alter table ICMSSN201 alter column modBC	int

	alter table ICMSSN202 alter column orig	int
	alter table ICMSSN202 alter column CST		int
	alter table ICMSSN202 alter column modBC	int

	
	alter table ICMSSN500 alter column orig	int
	alter table ICMSSN500 alter column CST		int
	alter table ICMSSN500 alter column modBC	int

	alter table ICMSSN900 alter column orig	int
	alter table ICMSSN900 alter column CST		int
	alter table ICMSSN900 alter column modBC	int
go
alter table IPINT alter column CST int
alter table COFINSAliq alter column CST int
alter table COFINSQtde alter column CST int

alter table COFINSNT alter column CST int
alter table COFINSOutr alter column CST int
alter table PISAliq alter column CST int

alter table PISQtde alter column CST int
alter table PISNT alter column CST int
alter table PISOutr alter column CST int

alter table PISOutr alter column CST int
go
alter table IPI alter column cEnq int
go
alter table prod  alter column indEscala int
alter table prod  alter column CFOP int
go
alter table transp alter column  modFrete int 
go
alter table pag alter column tPag int
go
alter table [ISSQNtot] add  cRegTrib int 
go
alter table [ISSQNtot] add dCompet varchar(50)
go
alter table [retTransp] add CFOP int
go
alter table [retTransp] add cMunFG bigint
go
alter table enderEmit alter column UF int
go
alter table enderEmit alter column UF int