param([string] $message)

Write-Host "`r`n[$(Get-Date -Format "HH:mm:ss")] " -f DarkGray -nonewline
Write-Host "$message" -f DarkBlue