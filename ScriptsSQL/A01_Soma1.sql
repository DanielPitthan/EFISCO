/*
** Incrementa o alfa numericos 
** Daniel Pitthan Silveira - ExceptionBrasil.com.br
** 31/08/2020

Exemplo : declare @OUT_RESULTADO VarChar(100) =''
			EXEC SOMA1 '0000000004' ,'N', @OUT_RESULTADO out

*/
use PROTHEUS
go

CREATE or alter PROCEDURE SOMA1
(
@IN_SOMAR      VarChar(100),
@IN_SOMALOW    Char(01),
@OUT_RESULTADO VarChar(100) output
)
as

Declare @iAux     integer
Declare @iTamOri  integer
Declare @iNx      integer
Declare @cNext    Char(01)
Declare @cSpace   Char
(01)
Declare @cRef     VarChar(1)
Declare @cResult  VarChar(100)
Declare @iTamStr  integer

begin
select @iTamStr = ( Len( @IN_SOMAR + '#' ) - 1 )
select @iTamOri = ( Len( @IN_SOMAR + '#' ) - 1 )
select @iAux = 1
select @iNx  = 1
select @cRef = ' '
select

 @cNext   = '0'
select @cSpace  = '0'
select @cResult = ' '

 if @IN_SOMAR = Replicate( '*', @iTamOri) begin
select @OUT_RESULTADO = @IN_SOMAR
return @OUT_RESULTADO

end
else begin
While @iTamStr >= @iNx begin
select @cRef = Substring(  @IN_SOMAR + '#' , @iTamStr , 1 )
if @cRef = ' ' begin
select @cResult = ' ' + @cResult
select @cNext = '1'
select @cSpace = '1'
end
else if @IN_SOMAR = ( Replicate('z',  @iTamOri )) begin
select @cResult = ( Replicate('*',  @iTamOri ))
break
end
else if @cRef < '9' begin
select @cResult = Substring( @IN_SOMAR, 1, ( @iTamStr - 1) ) + Char( Ascii( @cRef ) + 1 ) + @cResult
select @cNext = '0'
end
else if ( @cRef = '9' and @iTamStr > 1 ) begin
If
 ( Substring( @IN_SOMAR,  @iTamStr - 1 ,1 ) <= '9'  and  Substring( @IN_SOMAR, @iTamStr - 1 ,1 ) <> ' ') begin
select @cResult = '0' + @cResult
select @cNext = '1'
end
else if ( Substring( @IN_SOMAR, ( @iTamStr -1 ), 1 ) = ' ' ) begin
select @cResult = Substring( @IN_SOMAR,1,( @iTamStr - 2 ) ) + '10' + @cResult
select @cNext = '0'
end
else begin
select @cResult = Substring( @IN_SOMAR, 1, ( @iTamStr - 1 ) ) + 'A' + @cResult
select @cNext = '0'
end
end
else if @cRef = '9' and ( @iTamStr = 1 ) and ( @cSpace 

= '1' ) begin
select @cResult = '10' + Substring( @cResult, 1, ( Len( @cResult + '#' ) - 1) )
select @cNext = '0'
end
else if @cRef = '9' and @iTamStr = 1 and @cSpace = '0' begin
select @cResult = 'A' + @cResult
select @cNext ='0'
end
else if @cRef > '9' 

and @cRef < 'Z' begin
select @cResult = Substring( @IN_SOMAR, 1, ( @iTamStr - 1 ) ) + Char( ( Ascii( @cRef )+ 1 ) ) + @cResult
select @cNext = '0'
end
else if @cRef > 'Z' and @cRef < 'z' begin
select @cResult = Substring( @IN_SOMAR, 1, ( @iTamStr - 1 )) +

 Char((Ascii( @cRef ) + 1)) + @cResult
select @cNext = '0'
end
else if @cRef = 'Z' and @IN_SOMALOW = '1' begin
select @cResult = Substring( @IN_SOMAR, 1, ( @iTamStr - 1 )) + 'a' + @cResult
select @cNext = '0'
end
else if ( @cRef='z' or @cRef = 'Z') and @cSpace = '1' begin
select @cResult = Substring( @IN_SOMAR, 1, @iTamStr ) + '0' + Substring( @cResult, 1, ( Len( @cResult +'#' ) - 2 ))
select @cNext = '0'
end
else if @cRef = 'z' or @cRef = 'Z' begin
select @cResult = '0' + @cResult
select @cNext = '1'
end


if @cNext = '0' break
select @iTamStr = @iTamStr - 1
End
set @OUT_RESULTADO = @cResult
return @OUT_RESULTADO
end
end




