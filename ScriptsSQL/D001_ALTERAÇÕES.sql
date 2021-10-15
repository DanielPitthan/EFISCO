CREATE TABLE [dbo].[Empresa](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[CodigoEmpresaTotvs] [nvarchar](255) NULL,
	[CodigoFilialTotvs] [nvarchar](255) NULL,
	[RazaoSocial] [nvarchar](255) NULL,
	[Cnpj] [nvarchar](255) NULL,
	[IE] [nvarchar](255) NULL,
	[UF] [nvarchar](255) NULL,
	[Centralizadora] [bit] NULL,
	[DescricaoResumida] [nvarchar](255) NULL,
	[NomeFantasia] [nvarchar](255) NULL,
	[Sigla] [nvarchar](255) NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

CREATE TABLE [dbo].[Certificado](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Cnpj] [varchar](14) NULL,
	[Certf] [varbinary](max) NULL,
	[Senha] [varchar](254) NULL,
	[Nome] [varchar](50) NULL,
	[DataExpiracao] [datetime] NULL,
	[DataInclusao] [datetime] NULL,
	[Ativo] [bit] NULL,
	[EmpresaId] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

CREATE TABLE [dbo].[FileStorange](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[FileName] [varchar](max) NULL,
	[Path] [varchar](max) NULL,
	[MD5] [varchar](125) NULL,
	[DataInclusao] [datetime] NULL,
	[UsuarioId] [int] NULL,
	[FileStornageUnique] [uniqueidentifier] NULL,
	[OriginalFileName] [varchar](max) NULL,
	[XmlString] [varchar](max) NULL,
	[FileType] [varchar](10) NULL,
	[Processado] [bit] NULL,
	[DataByte] [varbinary](max) NULL,
	[OrigemId] [int] NULL,
	[RemetenteEmail] [varchar](254) NULL,
	[TipoXml] [varchar](3) NULL,
	[DataRecebimetoEmail] [datetime] NULL,
	[CorpoDoEmail] [varchar](80) NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]


    alter table fileStorange add [DataByte] [varbinary](max) NULL
	alter table fileStorange add [OrigemId] [int] NULL
	alter table fileStorange add [RemetenteEmail] [varchar](254) NULL
	alter table fileStorange add [TipoXml] [varchar](3) NULL
	alter table fileStorange add [DataRecebimetoEmail] [datetime] NULL
	alter table fileStorange add [CorpoDoEmail] [varchar](80) NULL


	create table Parametro(
	
	Id	int identity  primary key ,
Codigo	varchar(max),
Descricao	varchar(max),
Valor	nvarchar(max)
	)

go
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[DF__tmp_ms_xx__DataI__5D60DB10]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[Certificado] ADD  DEFAULT (getdate()) FOR [DataInclusao]
END
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[DF__tmp_ms_xx__Ativo__5E54FF49]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[Certificado] ADD  DEFAULT ((0)) FOR [Ativo]
END
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[DF__FileStora__TipoX__7908F585]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[FileStorange] ADD  DEFAULT ('') FOR [TipoXml]
END
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[fk_Certificado_Empresa]') AND parent_object_id = OBJECT_ID(N'[dbo].[Certificado]'))
ALTER TABLE [dbo].[Certificado]  WITH CHECK ADD  CONSTRAINT [fk_Certificado_Empresa] FOREIGN KEY([EmpresaId])
REFERENCES [dbo].[Empresa] ([Id])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[fk_Certificado_Empresa]') AND parent_object_id = OBJECT_ID(N'[dbo].[Certificado]'))
ALTER TABLE [dbo].[Certificado] CHECK CONSTRAINT [fk_Certificado_Empresa]
GO
USE [NFeXML]
GO
SET IDENTITY_INSERT [dbo].[Empresa] ON 
GO
INSERT [dbo].[Empresa] ([Id], [CodigoEmpresaTotvs], [CodigoFilialTotvs], [RazaoSocial], [Cnpj], [IE], [UF], [Centralizadora], [DescricaoResumida], [NomeFantasia], [Sigla]) VALUES (1, N'10', N'01', N'COLOROBBIA BRASIL PRODUTOS PARA CERAMICA LTDA               ', N'47939855000146', N'382009542116  ', N'SP', 1, N'BR  ITATIBA                ', N'COLOROBBIA', N'BR')
GO
INSERT [dbo].[Empresa] ([Id], [CodigoEmpresaTotvs], [CodigoFilialTotvs], [RazaoSocial], [Cnpj], [IE], [UF], [Centralizadora], [DescricaoResumida], [NomeFantasia], [Sigla]) VALUES (2, N'10', N'02', N'COLOROBBIA BRASIL PRODUTOS PARA CERAMICA LTDA               ', N'47939855000227', N'251509974     ', N'SC', 1, N'BR  CRICIUMA               ', N'COLOROBBIA', N'BR')
GO
INSERT [dbo].[Empresa] ([Id], [CodigoEmpresaTotvs], [CodigoFilialTotvs], [RazaoSocial], [Cnpj], [IE], [UF], [Centralizadora], [DescricaoResumida], [NomeFantasia], [Sigla]) VALUES (3, N'10', N'03', N'COLOROBBIA BRASIL PRODUTOS PARA CERAMICA LTDA               ', N'47939855000499', N'615014781110  ', N'SP', 1, N'BR  SANTA GERTRUDES        ', N'COLOROBBIA', N'BR')
GO
INSERT [dbo].[Empresa] ([Id], [CodigoEmpresaTotvs], [CodigoFilialTotvs], [RazaoSocial], [Cnpj], [IE], [UF], [Centralizadora], [DescricaoResumida], [NomeFantasia], [Sigla]) VALUES (4, N'20', N'01', N'COLOROBBIA NORDESTE PRODUTOS PARA CERAMICA LTDA             ', N'04019729000160', N'161306179     ', N'PB', 1, N'NE CONDE                ', N'COLOROBBIA', N'NE')
GO
INSERT [dbo].[Empresa] ([Id], [CodigoEmpresaTotvs], [CodigoFilialTotvs], [RazaoSocial], [Cnpj], [IE], [UF], [Centralizadora], [DescricaoResumida], [NomeFantasia], [Sigla]) VALUES (5, N'20', N'02', N'COLOROBBIA NORDESTE PRODUTOS PARA CERAMICA LTDA             ', N'04019729000241', N'258728574     ', N'SC', 1, N'NE CRICIUMA             ', N'COLOROBBIA', N'NE')
GO
SET IDENTITY_INSERT [dbo].[Empresa] OFF
GO
SET IDENTITY_INSERT [dbo].[Parametro] ON 
GO
INSERT [dbo].[Parametro] ([Id], [Codigo], [Descricao], [Valor]) VALUES (1, N'PATH_XML', N'Caminho dos arquivos XML', N'D:\Temp\XML COMBUSTIVEL')
GO
INSERT [dbo].[Parametro] ([Id], [Codigo], [Descricao], [Valor]) VALUES (2, N'HostEmailNfe', N'Host do Email da NFe', N'outlook.office365.com')
GO
INSERT [dbo].[Parametro] ([Id], [Codigo], [Descricao], [Valor]) VALUES (3, N'UsuarioDoEmailNfe', N'Usuário do E-Mail do NFe', N'nfe@colorobbia.com.br')
GO
INSERT [dbo].[Parametro] ([Id], [Codigo], [Descricao], [Valor]) VALUES (4, N'SenhaEmailDoNFe', N'Senha do E-mail do NFe', N'Colorobbia123')
GO
INSERT [dbo].[Parametro] ([Id], [Codigo], [Descricao], [Valor]) VALUES (12, N'PastaEmailNfe_Lido', N'Pasta do e-mail que sistema irá mover os e-mails Lidos ', N'NFeMotor_Lido')
GO
INSERT [dbo].[Parametro] ([Id], [Codigo], [Descricao], [Valor]) VALUES (13, N'PastaEmail_Ler', N'Pasta do e-mail que contem os e-mails com os arquivos XML', N'NFeMotor')
GO
SET IDENTITY_INSERT [dbo].[Parametro] OFF
GO



/****** Object:  Table [dbo].[NFeFiles]    Script Date: 15/10/2021 15:01:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[NFeFiles](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Arquivo] [varchar](255) NULL,
	[Path] [varchar](255) NULL,
	[ChaveAcesso] [varchar](44) NULL,
	[Processada] [bit] NULL,
	[DataInclusao] [datetime] NULL,
	[DataProcessamento] [datetime] NULL,
	[NumeroNota] [varchar](9) NULL,
	[Serie] [int] NULL,
	[CnpjFornecedor] [varchar](14) NULL,
	[Fornecedor] [varchar](255) NULL,
	[ValorTotal] [decimal](18, 4) NULL,
	[DataEmnissaoNfe] [datetime2](7) NULL,
	[Validado] [bit] NULL,
	[EmpresaId] [int] NULL,
	[AutoValidado] [bit] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[NFeFilesMensagens]    Script Date: 15/10/2021 15:01:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[NFeFilesMensagens](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[NFeFilesId] [int] NULL,
	[DataInclusao] [datetime] NULL,
	[Ativo] [bit] NULL,
	[Texto] [varchar](255) NULL,
	[ChaveNFe] [varchar](44) NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[NFeFiles] ADD  DEFAULT ((0)) FOR [Validado]
GO
ALTER TABLE [dbo].[NFeFiles] ADD  DEFAULT ((0)) FOR [AutoValidado]
GO
ALTER TABLE [dbo].[NFeFilesMensagens] ADD  DEFAULT (getdate()) FOR [DataInclusao]
GO
ALTER TABLE [dbo].[NFeFiles]  WITH CHECK ADD  CONSTRAINT [FK_NFeFiles_Empresa] FOREIGN KEY([EmpresaId])
REFERENCES [dbo].[Empresa] ([Id])
GO
ALTER TABLE [dbo].[NFeFiles] CHECK CONSTRAINT [FK_NFeFiles_Empresa]
GO
ALTER TABLE [dbo].[NFeFilesMensagens]  WITH CHECK ADD  CONSTRAINT [FK_NFeFilesMensagens_NFeFiles] FOREIGN KEY([NFeFilesId])
REFERENCES [dbo].[NFeFiles] ([Id])
GO
ALTER TABLE [dbo].[NFeFilesMensagens] CHECK CONSTRAINT [FK_NFeFilesMensagens_NFeFiles]
GO
