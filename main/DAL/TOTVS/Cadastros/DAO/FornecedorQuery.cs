using Models.TOTVS.Cadastros.ClienteFornecedor;

namespace DAL.TOTVS.Cadastros.DAO
{
    public class FornecedorQuery
    {
        public static string Insert(FornecedorTotvs fornecedor)
        {
            string query = @$"exec InsertSA2
		'{fornecedor.A2_NOME}',	
		'{fornecedor.A2_NREDUZ}',	
		'{fornecedor.A2_EST}',		
		'{fornecedor.A2_END}',		
		'{fornecedor.A2_COD_MUN}',		
		'{fornecedor.A2_MUN}',		
		'{fornecedor.A2_CEP}',		
		'{fornecedor.A2_CGC}',		
		'{fornecedor.A2_INSCR}',	
		'{fornecedor.A2_TEL}',		
		'{fornecedor.A2_COMPLEM}',		
		'{fornecedor.A2_BAIRRO}',	
		'{fornecedor.A2_NATUREZ}',		
		'{fornecedor.A2_CONTA}',	
		'{fornecedor.R_E_C_N_O_}',		
		'{fornecedor.A2_COD}',		
		'{fornecedor.A2_LOJA}',	
		'{fornecedor.A2_CODPAIS}',		
		'{fornecedor.A2_PAIS}',	
		'{fornecedor.A2_TIPO}',
		'{fornecedor.A2_FILIAL}'";

            return query;
        }

        public static string MaxCode(string filial)
        {
            return $@"SELECT * FROM SA2010
					WHERE 
					A2_FILIAL='{filial}' 
					AND A2_COD = (SELECT MAX(A2_COD) FROM SA2010
					WHERE A2_COD NOT IN('ESTADO', 'INPS', 'INPS01', 'INPS01', 'INPS26', 'INPS56', 
					'INPS57', 'INPS59', 'INPS63', 'INPS70', 'MUNIC', 'UNIAO', 'UNIAO', 'UNIAO', 
					'UNIAO', 'ESTADO', 'INPS', 'MUNIC', 'UNIAO'))
														";

        }
    }
}
