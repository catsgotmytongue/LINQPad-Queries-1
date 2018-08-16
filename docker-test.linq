<Query Kind="SQL">
  <Connection>
    <ID>aca5c6ca-300a-4af2-8b9d-8fb05da09340</ID>
    <Persist>true</Persist>
    <Driver Assembly="IQDriver" PublicKeyToken="5b59726538a49684">IQDriver.IQDriver</Driver>
    <Provider>Devart.Data.Oracle</Provider>
    <CustomCxString>AQAAANCMnd8BFdERjHoAwE/Cl+sBAAAA+DYpupyzEEexq8SAQjwOOwAAAAACAAAAAAAQZgAAAAEAACAAAAB8tcEHLp4zKG4gvVh9qhTZGTQYZhzh4PHJBcnAWA85lwAAAAAOgAAAAAIAACAAAACfnRZAB8Dr6lfBcp6u2F4nSdVAOWL3ErIxVsjU1Tk1qVAAAAChzFTMc3eSbWrHzfpEP1mZEiPVppX7PHZEjzbSXFhm3QE/prvDfBhmfRIHcPzghOcKZN1PEoea7Pb67qA9wHT+vE3gS993HN8HI/cijFLENkAAAABwWnf6mX2DL/Mmona8B0TBaNmo0wMNmv3xC7XG3IoPAjYRIBnoU3sZZhho/bR00btqtX61swdIDvHti22lR2iZ</CustomCxString>
    <Server>localhost</Server>
    <UserName>system</UserName>
    <EncryptCustomCxString>true</EncryptCustomCxString>
    <Password>AQAAANCMnd8BFdERjHoAwE/Cl+sBAAAA+DYpupyzEEexq8SAQjwOOwAAAAACAAAAAAAQZgAAAAEAACAAAAAXcKuKl/N6m9Sd8m3kxEN4xyLDZuMWL9SqL2JskpzoVQAAAAAOgAAAAAIAACAAAAC0RTZa2xNn22r9CnP4uBU40nsl8CiTf8CLf3aNRbByLhAAAAC+EGYo73elYUGTxcPSiA7aQAAAADwJZFnW5jsF/3E8TAZZFuzXgpG4OGBOCLn27X2LEjT6EfOdiIQ3BIN24h0F1RgX+Z8ZfMziahzL6Hh3stqKXNA=</Password>
    <DisplayName>OracleDocker</DisplayName>
    <DriverData>
      <StripUnderscores>true</StripUnderscores>
      <QuietenAllCaps>true</QuietenAllCaps>
      <ConnectAs>Default</ConnectAs>
      <UseOciMode>false</UseOciMode>
      <Port>32773</Port>
    </DriverData>
  </Connection>
</Query>

select * from all_tables  where table_name like '%V$%' order by table_name

create table OYA.test_table
    (
       id int not null,
       text varchar2(1000),
       primary key (id)
    );
	
	select * from Johny_table
	
	select * from all_tables where object_name = 'Johny_table' and object_type = 'table'	
	
	insert into johny_table  (id, text) values (2, 'TEST2')
	
	grant create session to oya
	
	SELECT DBMS_METADATA.get_ddl(object_Type, object_name, owner) FROM ALL_OBJECTS WHERE OWNER = 'OYA'; 