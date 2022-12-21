param(
	[switch] $duende,
	[switch] ${agent-identity},
	[switch] $reset,
	[switch] $help,
	[string] ${connection-config} = ''
)

${help-message} = '
Options:
  -d  -duende               Update Duende context
  -a  -agent-identity       Update AspNetIdentity context
  -r  -reset                Reset mode, set db back to primal
                            (only effect the AspNetIdentity context)
  -c  -connection-config    Set connection string and identity config, pattern: connection.~config
  -h  -help'

if ($help -or (!($duende) -and !(${agent-identity}))) {
	Write-Host 'Specify either or both -duende (-d) or -agent-identity (-a) flags' -f red
	${help-message}; return
}

${script-dir} = Split-Path -Parent $MyInvocation.MyCommand.Path
$mlog = Join-Path ${script-dir} 'mlog.ps1'

$persistedDBParams = @{ '-context' = 'PersistedGrantDbContext' };
$configDBParams = @{ '-context' = 'ConfigurationDbContext' };
$identityDBParams = @{ '-context' = 'AspNetIdentityDbContext' };

if (${connection-config}) {
	$connection = ${connection-config} -split '.~'

	$persistedDBParams.'-connection' = $connection[0]
	$configDBParams.'-connection' = $connection[0]
	$identityDBParams.'-connection' = $connection[0]
}

if (${agent-identity}) {
	# remove all migrations of AgentIdentity context
	if ($reset) {
		& $mlog -message 'Reset Agent Identity context'
		dotnet ef database update 0 @identityDBParams;
	}

	& $mlog -message 'Update Agent Identity context'
	dotnet ef database update @identityDBParams;
}

if ($duende) {
	# remove all migrations of Duende context
	& $mlog -message 'Reset Duende contexts'
	dotnet ef database update 0 @persistedDBParams
	dotnet ef database update 0 @configDBParams

	& $mlog -message 'Update Duende contexts'
	dotnet ef database update @persistedDBParams
	dotnet ef database update @configDBParams

	# init seed data (SeedData.cs)
	& $mlog -message 'Update Duende data'
	if (${connection-config}) {
		dotnet run .\bin\Debug\net6.0\AgentIdentityServer /seed-connection '${connection-config}'
	}
	else {
		dotnet run .\bin\Debug\net6.0\AgentIdentityServer /seed
	}
}
