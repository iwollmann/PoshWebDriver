function Get-MyModule([string]$Module){
return (Split-Path $PSCommandPath -Parent) + "\bin\debug\$Module.dll"
}


function Get-CurrentLocation(){
return (Split-Path $PSCommandPath -Parent)
}

Start-Process ((Get-CurrentLocation)+"\..\..\tools\chromedriver\chromedriver.exe")

Import-Module (Get-MyModule PoshWebDriver)
New-WebDriverSession -Url "http://localhost:9515/"
Set-WebDriverSessionUrl -Url "http://www.bing.com"
Get-WebDriverSessionScreenshot | Show-Image