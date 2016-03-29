<Query Kind="Program" />

void Main()
{
	var machineConfigPath = System.Runtime.InteropServices.RuntimeEnvironment.SystemConfigurationFile.Dump();
	var appConfigPath = AppDomain.CurrentDomain.SetupInformation.ConfigurationFile.Dump();
}

// Define other methods and classes here
