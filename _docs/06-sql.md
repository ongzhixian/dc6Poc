# Sql

```sql
CREATE TABLE [dbo].[Blog](
	[Id] [int] IDENTITY(1,1) NOT NULL,
    [Name] [nvarchar](50) NOT NULL,
    [Rating] [int] NOT NULL,
    CONSTRAINT [PK_Blog] PRIMARY KEY CLUSTERED 
    (
        [Id] ASC
    )
);
```


```sql
CREATE TABLE [dbo].[BlogPost](
	[Id] [int] IDENTITY(1,1) NOT NULL,
    [Title] [nvarchar](50) NOT NULL,
    [MarkupContent] [nvarchar](max) NOT NULL,
    [BlogId] [int] NOT NULL,
    CONSTRAINT [PK_BlogPost] PRIMARY KEY CLUSTERED 
    (
        [Id] ASC
    )
);
```

```sql
ALTER TABLE [dbo].[BlogPost]  WITH CHECK ADD  CONSTRAINT [FK_BlogPost_Blog] FOREIGN KEY([BlogId])
REFERENCES [dbo].[Blog] ([Id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO

ALTER TABLE [dbo].[BlogPost] CHECK CONSTRAINT [FK_BlogPost_Blog]
GO
```