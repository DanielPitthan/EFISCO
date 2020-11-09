/*
** 28/09/2020
** Daniel Pitthan Silveira - Exceptionbrasil.com.br		
** Faz o insert no SA5010 amarrando fornecedor x Produto 
*/
Use PROTHEUS 
go
Create  or alter procedure ProdutoVsFornecedor
	(
		@Filial varchar(4),
		@Cnpj Varchar(14),
		@CodigoProduto varchar(15),
		@CodigoProdutoFornecedor varchar(20)
	)
as
	
	Declare @NomeFornecedor Varchar(60)
	Declare @NomeProduto Varchar(80)
	Declare @CodigoFornecedor Varchar(6)
	Declare @LojaFornecedor Varchar(6)
	Declare @ProximoRecno int
	

	

	--Dados do fornecedor
	Select 
		@NomeFornecedor = A2_NOME ,
		@CodigoFornecedor = A2_COD,
		@LojaFornecedor = A2_LOJA
	FROM SA2010 WITH(NOLOCK)
	WHERE A2_CGC=@Cnpj AND A2_FILIAL=left(@Filial,2)




	--Dados do produto
	SELECT 
		@NomeProduto =B1_DESC
	FROM SB1010 WITH(NOLOCK)
	WHERE 
		B1_COD=@CodigoProduto
	


	--- VERIFICA SE JÁ EXISTE A AMARRACAO 
	IF EXISTS (SELECT * FROM SA5010 WHERE 
							A5_FILIAL=@Filial 
							AND A5_FORNECE	=@CodigoFornecedor 
							AND A5_LOJA		=	@LojaFornecedor
							AND A5_PRODUTO	=@CodigoProduto
							)
	BEGIN 
		RETURN 
	END

	--Proximo Recno
	set @ProximoRecno = (SELECT MAX(R_E_C_N_O_)+1 FROM SA5010)



	
	--Faz o insert
	INSERT INTO SA5010 
			(A5_FILIAL,A5_FORNECE       ,A5_LOJA        ,A5_PRODUTO   ,A5_NOMPROD  ,A5_NOMEFOR     ,A5_CODPRF,R_E_C_N_O_)
	VALUES  (@Filial ,@CodigoFornecedor,@LojaFornecedor,@CodigoProduto,@NomeProduto,@NomeFornecedor,@CodigoProdutoFornecedor,@ProximoRecno)

--Retorno para o Entity 
SELECT * FROM SA5010 WHERE R_E_C_N_O_=@ProximoRecno

