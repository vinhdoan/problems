Opt("WinTitleMatchMode",4)
$window1="[TITLE:????;CLASS:#32770]"
$window2="[TITLE:天龙八部环境安全检查;CLASS:#32770]"
Send("#r")
Send("C:\Users\Nhat-Tuan TRAN\Documents\My Toolbars\HSc\Launch.lnk",1)
Send("{ENTER}")
WinWait($window1)
ControlClick($window1,"","[CLASS:Button;INSTANCE:3]")
WinWait($window2)
ControlClick($window2,"","[CLASS:Button;INSTANCE:1]")