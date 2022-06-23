# TestApp_17-06-2022
Launch instructions:
1. Install PostgreSQL https://www.postgresql.org/download/
2. Change in appsetting.json line ConnectionString:DefaultConnection (login, password)
3. Create a database called testdb
5. Rub app and go http://localhost:5000
  5.1. If table not created use tables.sql script in DB
  
P.S. The VAT parameter is placed in the AppProperties class this used as Singleton
