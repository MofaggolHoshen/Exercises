# All about Excel Problems & Solusion

## SQL Queries from Excel

Write in function bar your SQL script

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
