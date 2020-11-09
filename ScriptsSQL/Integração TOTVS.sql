
-- Seleção de Arquivos 
select 
	E.CodigoEmpresaTotvs+E.CodigoFilialTotvs as Filial,
	NFF.Arquivo as Arquivo,
	NFF.NumeroNota as NumeroDaNota,
	right('000'+ltrim(NFF.Serie),3) as Serie,
	NFF.ChaveAcesso as ChaveDeAcesso,
	NFF.CnpjFornecedor as CnpjFornecedor,
	NFF.Fornecedor  as Fornecedor,
	NFF.ValorTotal as ValorTotal,
	CAST( NFF.DataEmnissaoNfe AS DATE) as DataEmissaoNF,
	CAST(NFF.DataInclusao AS DATE) as DataInclusao,
	prod.CFOP,
	prod.cProd,
	prod.xProd

from 
	ColorobbiaPlataform.dbo.NFeFiles as NFF With(nolock) 
	join NFeXML.dbo.infNFe as I with(nolock) on I.Id=NFF.ChaveAcesso collate Latin1_General_CI_AS
	join NFeXML.dbo.dest as D with(nolock) on D.Id = I.destId 
	left Join ColorobbiaPlataform.dbo.Empresa as E with(nolock) On E.Cnpj=D.CNPJ collate Latin1_General_CI_AS
	join NFeXML.dbo.det as det with(nolock) on det.infNFeId = I.infNFeId
	join NFeXML.dbo.prod as prod with(nolock) on prod.Id = det.prodId
where AutoValidado=1 and Processada=0
order by DataEmissaoNF,NumeroDaNota



--Busca os detalhes da NF
select E.CodigoEmpresaTotvs+E.CodigoFilialTotvs as Filial,
	NFF.Arquivo as Arquivo,
	NFF.NumeroNota as NumeroDaNota,
	right('000'+ltrim(NFF.Serie),3) as Serie,
	NFF.ChaveAcesso as ChaveDeAcesso,
	NFF.CnpjFornecedor as CnpjFornecedor,
	NFF.Fornecedor  as Fornecedor,
	NFF.ValorTotal as ValorTotal,
	CAST( NFF.DataEmnissaoNfe AS DATE) as DataEmissaoNF,
	CAST(NFF.DataInclusao AS DATE) as DataInclusao,
	prd.cProd,prd.xProd,prd.NCM,prd.qCom,prd.vUnCom, prd.vProd,
	isnull(prd.xPed,'') as xPed
from	
	ColorobbiaPlataform.dbo.NFeFiles as NFF with(nolock)
	join NFeXML.dbo.infNFe  as inf with(nolock) on NFF.ChaveAcesso=inf.Id
	join NFeXML.dbo.dest as D with(nolock) on D.Id = inf.destId 
	left Join ColorobbiaPlataform.dbo.Empresa as E with(nolock) On E.Cnpj=D.CNPJ collate Latin1_General_CI_AS
	join NFeXML.dbo.ide as ide with(nolock) on ide.Id=inf.ideId
	join NFeXML.dbo.det as det with(nolock) on det.infNFeId = inf.infNFeId
	join NFeXML.dbo.prod as prd with(nolock) on prd.Id= det.prodId
where 
	NFF.ChaveAcesso='23200102756334000387550000000051881517165326'




	--Busca os itens da NF

	select 
	inf.Id as ChaveNfe,
	prod.cProd,
	emit.CNPJ,
	emit.xNome,
	prod.vProd,
	prod.vUnCom,
	prod.qCom,
	A5.A5_PRODUTO,
	A2.A2_COD,
	A2.A2_LOJA,
	prod.xPed
from NFeXML.dbo.NFe		as nfe  with(nolock)
	join NFeXML.dbo.infNFe as inf  with(nolock) on nfe.Id=inf.infNFeId
	join NFeXML.dbo.det	as det  with(nolock) on det.infNFeId = inf.infNFeId
	join NFeXML.dbo.prod   as prod with(nolock) on prod.Id=det.prodId
	join NFeXML.dbo.emit	as emit with(nolock) on emit.Id = inf.emitId
	left join PROTHEUS_PRODUCAO.dbo.SA2010 AS A2 WITH(NOLOCK) ON A2.A2_CGC=emit.CNPJ COLLATE Latin1_General_BIN
	left JOIN PROTHEUS_PRODUCAO.dbo.SA5010 AS A5 WITH(NOLOCK) ON A5.A5_FORNECE=A2.A2_COD AND A5.A5_LOJA=A2_LOJA
	left join PROTHEUS_PRODUCAO.dbo.SB1010 AS B1 WITH(NOLOCK) ON B1.B1_COD= A5.A5_PRODUTO AND A5.A5_CODPRF =prod.cProd COLLATE Latin1_General_BIN
where 
	1=1 AND 
	B1.B1_FILIAL = '1001'	AND  B1.D_E_L_E_T_='' AND 
	A2.A2_FILIAL ='10'	AND A2.D_E_L_E_T_=''  AND 
	A5.A5_FILIAL='1001'		AND A5.D_E_L_E_T_=''  AND 
	inf.Id='35200252719481000193550110000006031000006061'


-- busca detalhes de impostos

select prod.cProd, prod.CFOP, Icm.vProd,Icm.vNf,Icm.vBc,Icm.vIcms,Icm.vBcst, Icm.vCofins, Icm.vPis,Icm.vIpi,Icm.vFrete,Icm.vSeg,Icm.vDesc
from NFeXML.dbo.infNFe as inf with(nolock)
	join NFeXML.dbo.det as det with(nolock) on det.infNFeId = inf.infNFeId
	join NFeXML.dbo.prod as prod with(nolock) on prod.Id = det.prodId
	join NFeXML.dbo.total as T with(nolock) on T.id = inf.totalId
	join NFeXML.dbo.ICMSTot as Icm with(nolock) on Icm.id = T.ICMSTotId
where 
	inf.Id='23200102756334000387550000000051881517165326'