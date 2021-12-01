# SQL Trics & Trips

## Queries from Excel

Write in Excel function bar your SQL script, how we are suming two cel value

```EXCEL
="UPDATE [dbo].[AspNetUsers]
   SET 
      [UserName] = '"&LOWER([@[New email]])&"'
      ,[NormalizedUserName] = '"&UPPER([@[New email]])&"'
      ,[Email] = '"&LOWER([@[New email]] )&"'
      ,[NormalizedEmail] = '"&UPPER([@[New email]])&"'
 WHERE UserName = '"&[@UserName] &"' AND Email='"&[@Email] &"'
GO"
```
