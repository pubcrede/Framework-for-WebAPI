# Parameters
param([String]$Parameter1=".\")

# Initialize
Set-ExecutionPolicy Unrestricted -Scope CurrentUser -Force
[String]$ThisScript = $MyInvocation.MyCommand.Path
[String]$ThisDir = Split-Path $ThisScript
Set-Location $ThisDir # Ensure our location is correct, so we can use relative paths

# Initialize
$Parameter1=$Parameter1.Replace("`"","")

# Relay parameter to output
Write-Output "Parameter1: $Parameter1"

# Add override to allow partial class extension of the EF generated files
$configFiles=get-childitem -Path "$Parameter1\*.cs"
foreach ($file in $configFiles)
{
	(Get-Content $file.PSPath) | 
	Foreach-Object {$_-replace "public System.Guid ID", "public override System.Guid ID" `
		-replace "public int ID", "public override int ID" `
		-replace "public System.Guid Key", "public override System.Guid Key" `
		-replace "public byte", "public override byte" `
		-replace "public int RecordStatus", "public override System.Guid RecordStatus" `
		-replace "public System.Guid ActivityWorkflowID { get; set; }", "public override System.Guid ActivityWorkflowID { get; set; }" `
		-replace "public int ActivityID { get; set; }", "public override int ActivityID { get; set; }" `
		-replace "public System.DateTime CreatedDate { get; set; }", "public override System.DateTime CreatedDate { get; set; }" `
		-replace "public System.DateTime ModifiedDate { get; set; }", "public override System.DateTime ModifiedDate { get; set; }"
	} | 
	Set-Content $file.PSPath
}