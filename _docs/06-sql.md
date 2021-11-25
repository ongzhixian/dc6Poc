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

```sql
INSERT INTO Blog (Name, Rating) VALUES ('Blog A', 0);
INSERT INTO Blog (Name, Rating) VALUES ('Blog B', 0);
INSERT INTO Blog (Name, Rating) VALUES ('Blog C', 0);

SELECT * FROM BlogPost;

INSERT INTO BlogPost(Title, MarkupContent, BlogId) VALUES ('Post A-1', 'Post A-1 content', 1);
INSERT INTO BlogPost(Title, MarkupContent, BlogId) VALUES ('Post A-2', 'Post A-2 content', 1);
INSERT INTO BlogPost(Title, MarkupContent, BlogId) VALUES ('Post A-3', 'Post A-3 content', 1);

INSERT INTO BlogPost(Title, MarkupContent, BlogId) VALUES ('Post B-1', 'Post B-1 content', 2);
INSERT INTO BlogPost(Title, MarkupContent, BlogId) VALUES ('Post B-2', 'Post B-2 content', 2);
INSERT INTO BlogPost(Title, MarkupContent, BlogId) VALUES ('Post B-3', 'Post B-3 content', 2);
INSERT INTO BlogPost(Title, MarkupContent, BlogId) VALUES ('Post B-4', 'Post B-4 content', 2);
INSERT INTO BlogPost(Title, MarkupContent, BlogId) VALUES ('Post B-5', 'Post B-5 content', 2);
```
