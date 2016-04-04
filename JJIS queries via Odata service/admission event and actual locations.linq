<Query Kind="SQL">
  <Connection>
    <ID>bd902168-e5b6-4103-822f-ae2c8e9e24bb</ID>
    <Persist>true</Persist>
    <Driver Assembly="IQDriver" PublicKeyToken="5b59726538a49684">IQDriver.IQDriver</Driver>
    <Provider>Devart.Data.Oracle</Provider>
    <CustomCxString>AQAAANCMnd8BFdERjHoAwE/Cl+sBAAAAraKb7NNmEUmkqfWr0ymeKAAAAAACAAAAAAADZgAAwAAAABAAAABoP1KaeZyVRtl2JbBFNT/wAAAAAASAAACgAAAAEAAAAPLyb4rTJEVRky2AbHqEOPNwAAAA6PGxdaHLH7XYv+tXdKwERiT0k++34jm+nA2iErFcjSk4xkEKSJdceF1XFXKwy87Izq6n5BgDUS5iGfatuMC6BsH61z07u4eK9HWL/LqKumfi8gZlqIMzeKmHF3lzUeYbmMsHBh078Vfrb1CNucaDbhQAAACo6kyBTmMROgTycGH6K6QCRHmAKA==</CustomCxString>
    <Password>AQAAANCMnd8BFdERjHoAwE/Cl+sBAAAAraKb7NNmEUmkqfWr0ymeKAAAAAACAAAAAAADZgAAwAAAABAAAAD4AyzhcSZ6H3/tNdd4S1ReAAAAAASAAACgAAAAEAAAANW6z2FqC4fzHCqy9hKUJB0QAAAAgXrj+IH4PSJsiIj46dao4hQAAAAzx4fvATuoFZ8V1gg9TekunOG40A==</Password>
    <UserName>jjisnet</UserName>
    <EncryptCustomCxString>true</EncryptCustomCxString>
    <Server>development.oya.state.or.us</Server>
    <DisplayName>Development (No Oracle Client)</DisplayName>
    <DriverData>
      <StripUnderscores>true</StripUnderscores>
      <QuietenAllCaps>true</QuietenAllCaps>
      <ConnectAs>Default</ConnectAs>
      <UseOciMode>false</UseOciMode>
      <Port>1527</Port>
      <SID>oyadevel</SID>
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

select * from admissionevent;


select * from actuallocation where actual_location_id = 700059039082