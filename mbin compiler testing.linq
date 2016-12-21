<Query Kind="Program">
  <NuGetReference>MbinCompileLib</NuGetReference>
  <Namespace>MbinCompileLib</Namespace>
  <Namespace>MBINCompiler</Namespace>
  <Namespace>MBINCompiler.Models</Namespace>
  <Namespace>MBINCompiler.Models.Structs</Namespace>
  <Namespace>MBINCompiler.Models.Structs.Unfinished</Namespace>
</Query>

void Main()
{
	Debug.Listeners.Clear();
	var mbinDir = @"D:\temp\NMS_BACKUP\GAMEDATA\PCBANKS\output";
	var decompiledDir =  @"D:\temp\NMS_BACKUP\Decompiled\GAMEDATA\PCBANKS";
	
	var dir = new DirectoryInfo(mbinDir);
	
	var mbinFiles = dir.GetFiles("*.mbin.*", SearchOption.AllDirectories).Select(fi=>fi.FullName).ToList();

	foreach (var name in mbinFiles)
	{
	    var orgFilename = Path.GetFileName(name);
		
		var realExtension = name.GetLongExtension(@"(.*)(?<LongExt>\.MBIN(\.PC)?)");
		//realExtension.Dump();
		
		string decompiledExtension = MbinCompileLib.Extensions.Exml.ToUpper();
		
		if(realExtension == MbinCompileLib.Extensions.MbinPc.ToUpper())
			decompiledExtension = MbinCompileLib.Extensions.ExmlPc.ToUpper();
			
		
		var relativeDir = name.Replace(mbinDir,String.Empty).Replace(orgFilename, String.Empty).Substring(1);

		//var newFilePath = Path.ChangeExtension(relativePath, 
		if (MBINFileHelper.HasMbinExtension(name)/*MBINFileHelper.IsMBinFile(name)*/)
		{
			var newFilename = orgFilename.Replace(realExtension, decompiledExtension);
			var newDir = $"{decompiledDir}\\{relativeDir}";
			var newName = $"{newDir}{newFilename}";


			//($"RELDIR: {relativeDir}").Dump();
			//($"NEWDIR: {newDir}").Dump();
			//($"NEWFIL: {newName}").Dump();
			//("\n").Dump();
			Directory.CreateDirectory($"{newDir}");

			if (!File.Exists(newName))
			{
				try
				{
					MBINFileHelper.DecompileFile(name, newName);
					($"{name} => {newName}").Dump();
				}
				catch {
					
				}
			}
		}
	}
}

// Define other methods and classes here