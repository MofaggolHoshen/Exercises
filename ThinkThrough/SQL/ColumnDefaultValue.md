# SQL script for column default value

## Add/Drop default value

* Add Default vaue with new column

```SQL
-- Add default vaue with new Column
ALTER TABLE [dbo].[Applicants]
ADD 
  [StatusId] INT CONSTRAINT [DF_ApplicantStatusId]  DEFAULT ((1)) NOT NULL
  CONSTRAINT [FK_ApplicantStatusId] FOREIGN KEY ([StatusId]) REFERENCE [dbo].[Statuses] ([ID]);
GO

-- Or without CONSTRAINT, 
ALTER TABLE [dbo].[Applicants]
ADD 
  [StatusId] INT DEFAULT ((1)) NOT NULL
  CONSTRAINT [FK_ApplicantStatusId] FOREIGN KEY ([StatusId]) REFERENCE [dbo].[Statuses] ([ID]);
GO
```

* Add default value to existing column

```SQL
ALTER TABLE [dbo].[Clients] ADD  CONSTRAINT [DF_ClientsStatusId]  DEFAULT ((1)) FOR [StatusId]
GO
```
