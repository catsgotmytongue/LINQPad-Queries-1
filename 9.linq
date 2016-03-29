<Query Kind="SQL">
  <Connection>
    <ID>c3458187-91e4-4861-857b-d4487d5e8722</ID>
    <Persist>true</Persist>
    <Driver Assembly="IQDriver" PublicKeyToken="5b59726538a49684">IQDriver.IQDriver</Driver>
    <Provider>Devart.Data.Oracle</Provider>
    <CustomCxString>AQAAANCMnd8BFdERjHoAwE/Cl+sBAAAAraKb7NNmEUmkqfWr0ymeKAAAAAACAAAAAAADZgAAwAAAABAAAADBd6AleAAw4FlQCmEx8Js6AAAAAASAAACgAAAAEAAAAEjAuW1dw6HZkYdJkagnMQs4AAAAWNZpoe4kloZF0Jbzbmy+4VK9dsiQvpca09SQTo2uLZ83ZqTOdL9qqIAZWAYdwHIkl/hkbKRBrEUUAAAANfXnB8tfnWNSeWJn351zxcvyxTY=</CustomCxString>
    <Server>DEVELOPMENT</Server>
    <UserName>jjisnet</UserName>
    <EncryptCustomCxString>true</EncryptCustomCxString>
    <DisplayName>Development (Oracle Native Client)</DisplayName>
    <Password>AQAAANCMnd8BFdERjHoAwE/Cl+sBAAAAraKb7NNmEUmkqfWr0ymeKAAAAAACAAAAAAADZgAAwAAAABAAAADhU2qgGyUX36yXkpztB3xMAAAAAASAAACgAAAAEAAAAA1DJOWgj0pR8ZjlmLvWaGsQAAAAyPF4WaSXs1TASaYysqr/rhQAAADqbyQcztqs6vYylD3meufdYBWwHw==</Password>
    <DriverData>
      <StripUnderscores>true</StripUnderscores>
      <QuietenAllCaps>true</QuietenAllCaps>
      <ConnectAs>Default</ConnectAs>
      <UseOciMode>true</UseOciMode>
    </DriverData>
  </Connection>
  <NuGetReference>State.OR.Oya.Core.Utility</NuGetReference>
  <Namespace>State.OR.Oya.Core.Utility.Caching</Namespace>
  <Namespace>State.OR.Oya.Core.Utility.Diagnostics</Namespace>
  <Namespace>State.OR.Oya.Core.Utility.IO</Namespace>
  <Namespace>State.OR.Oya.Core.Utility.Logging</Namespace>
  <Namespace>State.OR.Oya.Core.Utility.Reflection</Namespace>
  <Namespace>State.OR.Oya.Core.Utility.ServiceClient</Namespace>
  <Namespace>State.OR.Oya.Core.Utility.String</Namespace>
  <Namespace>State.OR.Oya.Core.Utility.Threading</Namespace>
  <Namespace>State.OR.Oya.Core.Utility.Xml</Namespace>
</Query>

select * from code 
where  1=1 
--description = 'Detention'
and code = 'DETN'
--and type = 'PLCM'