CREATE TABLE [dbo].[OrderState](
	[CorrelationId] [uniqueidentifier] NOT NULL,
	[CurrentState] [nvarchar](max) NULL,
	[OrderCreationDateTime] [datetime] NULL,
	[OrderCancelDateTime] [datetime] NULL,
	[OrderId] [uniqueidentifier] NULL,
	[Price] [decimal](18, 2) NULL,
	[Product] [nvarchar](max) NULL,
 CONSTRAINT [PK_OrderStateData] PRIMARY KEY CLUSTERED 
(
	[CorrelationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO