

#include <GUIConstantsEx.au3>

#Region ### START Koda GUI section ###
$frmDNSease = GUICreate("DNS Ease v1.00 - Freeware", 376, 403, 423, 202)
GUISetIcon("C:\Users\NGOC KHIEM\Desktop\DNS ease Icon.ico", -1)
$Copyright = GUICtrlCreateLabel("F&&P e-Homeware © 2011", 221, 378, 153, 17)
GUICtrlSetFont(-1, 8, 800, 0, "MS Sans Serif")
GUICtrlSetState(-1, $GUI_DISABLE)
$cmdRun = GUICtrlCreateButton("&Run CMD", 16, 16, 75, 25)
GUICtrlSetState(-1, $GUI_FOCUS)	;the focus is on this button
GUICtrlSetTip(-1, "Open command prompt")
$cmdIPC = GUICtrlCreateButton("&Ipconfig", 104, 16, 75, 25)
GUICtrlSetTip(-1, "Get the IP address information of this PC")
$cmdIPCall = GUICtrlCreateButton("Ipconfig/&All", 16, 48, 75, 25)
GUICtrlSetTip(-1, "Get ALL the IP address information of this PC")
$cmdIPCflush = GUICtrlCreateButton("&FlushDNS", 104, 48, 75, 25)
GUICtrlSetTip(-1, "Flush the DNS resolver cache")
$cmdDNS0 = GUICtrlCreateButton("R&eset DNS", 200, 48, 75, 25)
GUICtrlSetTip(-1, "Obtain DNS server address automatically")
$cmdDNS1 = GUICtrlCreateButton("&Change", 16, 112, 51, 25)
GUICtrlSetTip(-1, "Try this DNS")
$cmdP1 = GUICtrlCreateButton("P", 72, 112, 25, 25)
GUICtrlSetTip(-1, "Test the reachability of this DNS server")
$txtDNS1a = GUICtrlCreateInput("8.8.8.8", 104, 114, 89, 21)
$txtDNS1b = GUICtrlCreateInput("8.8.4.4", 200, 114, 89, 21)
$txtName1 = GUICtrlCreateInput("Google", 296, 114, 65, 21)
$cmdDNS2 = GUICtrlCreateButton("Change", 16, 144, 51, 25)
GUICtrlSetTip(-1, "Try this DNS")
$cmdP2 = GUICtrlCreateButton("P", 72, 144, 25, 25)
GUICtrlSetTip(-1, "Test the reachability of this DNS server")
$txtDNS2a = GUICtrlCreateInput("210.245.24.20", 104, 146, 89, 21)
$txtDNS2b = GUICtrlCreateInput("210.245.31.20", 200, 146, 89, 21)
$txtName2 = GUICtrlCreateInput("FPT", 296, 146, 65, 21)
$cmdDNS3 = GUICtrlCreateButton("Change", 16, 176, 51, 25)
GUICtrlSetTip(-1, "Try this DNS")
$cmdP3 = GUICtrlCreateButton("P", 72, 176, 25, 25)
GUICtrlSetTip(-1, "Test the reachability of this DNS server")
$txtDNS3a = GUICtrlCreateInput("198.153.194.1", 104, 178, 89, 21)
$txtDNS3b = GUICtrlCreateInput("198.153.192.1", 200, 178, 89, 21)
$txtName3 = GUICtrlCreateInput("Norton", 296, 178, 65, 21)
$cmdDNS4 = GUICtrlCreateButton("Change", 16, 208, 51, 25)
GUICtrlSetTip(-1, "Try this DNS")
$cmdP4 = GUICtrlCreateButton("P", 72, 208, 25, 25)
GUICtrlSetTip(-1, "Test the reachability of this DNS server")
$txtDNS4a = GUICtrlCreateInput("208.67.222.222", 104, 210, 89, 21)
$txtDNS4b = GUICtrlCreateInput("208.67.220.220", 200, 210, 89, 21)
$txtName4 = GUICtrlCreateInput("OpenDNS", 296, 210, 65, 21)
$cmdDNS5 = GUICtrlCreateButton("Change", 16, 240, 51, 25)
GUICtrlSetTip(-1, "Try this DNS")
$cmdP5 = GUICtrlCreateButton("P", 72, 240, 25, 25)
GUICtrlSetTip(-1, "Test the reachability of this DNS server")
$txtDNS5a = GUICtrlCreateInput("156.154.70.1", 104, 242, 89, 21)
$txtDNS5b = GUICtrlCreateInput("156.154.71.1", 200, 242, 89, 21)
$txtName5 = GUICtrlCreateInput("DNS Adv.", 296, 242, 65, 21)
$cmdPing1 = GUICtrlCreateButton("&Ping", 16, 280, 75, 25)
GUICtrlSetTip(-1, "Test the reachability of this website")
$txtPing1 = GUICtrlCreateInput("www.google.com", 104, 282, 129, 21)
$cmdPing2 = GUICtrlCreateButton("Ping", 16, 312, 75, 25)
GUICtrlSetTip(-1, "Test the reachability of this website")
$txtPing2 = GUICtrlCreateInput("www.yahoo.com", 104, 314, 129, 21)
$cmdPing3 = GUICtrlCreateButton("Ping", 16, 344, 75, 25)
GUICtrlSetTip(-1, "Test the reachability of this website")
$txtPing3 = GUICtrlCreateInput("www.zing.vn", 104, 346, 129, 21)
$chkSetting1 = GUICtrlCreateCheckbox("Run command in ", 246, 288, 105, 17)
$chkSetting2 = GUICtrlCreateCheckbox("Close all Command", 246, 328, 105, 17)
GUICtrlSetState(-1, $GUI_CHECKED)
$cmdSave = GUICtrlCreateButton("&Save settings", 288, 16, 75, 25)
GUICtrlSetTip(-1, "Keep everything here for the next use")
$cmdHelp = GUICtrlCreateButton("&Tips", 288, 48, 75, 25)
$Label2 = GUICtrlCreateLabel("Preferred DNS", 104, 88, 86, 17)
GUICtrlSetFont(-1, 8, 800, 0, "MS Sans Serif")
$Label3 = GUICtrlCreateLabel("Alternative DNS", 198, 88, 95, 17)
GUICtrlSetFont(-1, 8, 800, 0, "MS Sans Serif")
$Label5 = GUICtrlCreateLabel("Providers", 298, 88, 57, 17)
GUICtrlSetFont(-1, 8, 800, 0, "MS Sans Serif")
$Label1 = GUICtrlCreateLabel("separate windows", 262, 306, 89, 17)
$Label6 = GUICtrlCreateLabel("Prompts on exit", 262, 344, 76, 17)
$Group1 = GUICtrlCreateGroup("", 240, 272, 121, 97)
GUICtrlCreateGroup("", -99, -99, 1, 1)
$cmdUseless = GUICtrlCreateButton("Non-purpose", 200, 16, 75, 25)
GUICtrlSetTip(-1, "This button is just for... beauty purpose only :)")
$txtStatus = GUICtrlCreateLabel("Status goes here", 18, 378, 155, 17)
GUICtrlSetFont(-1, 8, 800, 0, "MS Sans Serif")
GUISetState(@SW_SHOW)
#EndRegion ### END Koda GUI section ###


Opt("WinTitleMatchMode",2) ;Match any substring in the title
Global $hDos ;Forces creation of this variable in the Global scope
Global $config[1], $setSetting[21]

OpenConfig ()

While 1
	$nMsg = GUIGetMsg()

	Switch $nMsg
		
		Case $GUI_EVENT_CLOSE
			CheckClose ()
			Exit
						
		Case $cmdRun
			StartPrompt ()
		
		Case $cmdIPC
			CheckRun () 
			RunCommand ("ipconfig")
			
		Case $cmdIPCall
			CheckRun () 
			RunCommand ("ipconfig/all")
			
		Case $cmdIPCflush
			CheckRun () 
			RunCommand ("ipconfig/flushDNS")
			
		Case $cmdDNS0
			CheckRun () 
			RunCommand ('netsh interface ip set dns "Local Area Connection" dhcp')
			
			
		Case $cmdPing1
			CheckRun () 
			RunCommand ("ping " & GUICtrlRead($txtPing1))
		Case $cmdPing2
			CheckRun () 
			RunCommand ("ping " & GUICtrlRead($txtPing2))
		Case $cmdPing3
			CheckRun () 
			RunCommand ("ping " & GUICtrlRead($txtPing3))
			

		Case $cmdDNS1
			ChangeDNS (GUICtrlRead($txtDNS1a),GUICtrlRead($txtDNS1b))
		Case $cmdDNS2
			ChangeDNS (GUICtrlRead($txtDNS2a),GUICtrlRead($txtDNS2b))
		Case $cmdDNS3
			ChangeDNS (GUICtrlRead($txtDNS3a),GUICtrlRead($txtDNS3b))
		Case $cmdDNS4
			ChangeDNS (GUICtrlRead($txtDNS4a),GUICtrlRead($txtDNS4b))
		Case $cmdDNS5
			 ChangeDNS (GUICtrlRead($txtDNS5a),GUICtrlRead($txtDNS5b))
			
			
		Case $cmdP1
			PingDNS (GUICtrlRead($txtDNS1a),GUICtrlRead($txtDNS1b))
		Case $cmdP2
			PingDNS (GUICtrlRead($txtDNS2a),GUICtrlRead($txtDNS2b))
		Case $cmdP3
			PingDNS (GUICtrlRead($txtDNS3a),GUICtrlRead($txtDNS3b))
		Case $cmdP4
			PingDNS (GUICtrlRead($txtDNS4a),GUICtrlRead($txtDNS4b))
		Case $cmdP5
			PingDNS (GUICtrlRead($txtDNS5a),GUICtrlRead($txtDNS5b))			
			
			
		Case $cmdSave
			SaveConfig ()
		Case $cmdHelp
			Help ()
	EndSwitch
WEnd

Func StartPrompt ()
	Run("cmd", "", @SW_MAXIMIZE)	;Call cmd.exe to wait for commands
	WinWait("cmd.exe")
	$hDos = WingetHandle("cmd.exe")
	If @error Then
		MsgBox(0x10, "Error!", "Could not load the Command prompt or DNS ease has bugged.")
		Exit
	EndIf
EndFunc



Func CheckRun () 
	If GUICtrlRead($chkSetting1) = $GUI_CHECKED then 
		StartPrompt ()
	EndIf
EndFunc

Func CheckClose ()
	If GUICtrlRead($chkSetting2) = $GUI_CHECKED then 
		$wClose = WinList ("cmd.exe")
		GUICtrlSetData ($txtStatus, "Exiting... Please wait.")
		For $i = 1 to $wClose[0][0]
			WinClose ($wClose[$i][1])
		Next
	EndIf
EndFunc

Func RunCommand($command)
	WinActivate ("cmd.exe")  ;Activates (gives focus to) command prompt.

	If not WinActive("cmd.exe") Then
		StartPrompt()
		WinActivate ("cmd.exe")
		If not WinActive("cmd.exe") Then
			MsgBox(0x10, "Error!", "Command prompt not found! Please try again.")
			Exit
		EndIf
	EndIf
	
	SendKeepActive($hDos)	
	Send ($command)
	Send ("{ENTER}")
	
	Sleep (2000)
EndFunc

Func ChangeDNS ($DNSa, $DNSb)
	CheckRun ()
	RunCommand ('netsh interface ip set dns "Local Area Connection" static ' & $DNSa)
	CheckRun ()
	RunCommand ('netsh interface ip add dns "Local Area Connection" ' & $DNSb)
EndFunc

Func PingDNS ($DNSa, $DNSb)
	CheckRun ()
	RunCommand ("ping " & $DNSa)
	CheckRun ()
	RunCommand ("ping " & $DNSb)
EndFunc

Func OpenConfig ()
	
	$file = FileOpen("DNSease.cfg", 0)
	
	If $file = -1 Then
		GUICtrlSetData ($txtStatus, "Default settings loaded")
	Else
		While 1
			$line = FileReadLine($file)
			
			If @error = -1 Then ExitLoop
			
			$var = StringSplit ($line,"@",1)
			
			If $var[0] = 2 Then
				$setSetting[$var[1]] = $var[2]
				$setSetting[0] = $setSetting[0] + 1
			EndIf

		Wend
		
		FileClose($file)
		
		If not @error = -1 Then
			LoadConfig()
		Else
			GUICtrlSetData ($txtStatus, "Default settings loaded")
		EndIf
		
	EndIf

EndFunc

Func SaveConfig()
	$file = FileOpen("DNSease.cfg", 2)
	
	If $file = -1 Then
		GUICtrlSetData ($txtStatus, "Unable to save settings.")
	Else
		FileWriteLine($file, "../----------------------------/")
		FileWriteLine($file, "./*DNS Ease config*/. CAUTION!!! DO NOT CHANGE LINE ORDER.")
		FileWriteLine($file, "/---------------------------/..")
		FileWriteLine($file, "***Settings Config***")
		FileWriteLine($file, "1@" & GUICtrlRead($txtDNS1a))
		FileWriteLine($file, "2@" & GUICtrlRead($txtDNS1b))
		FileWriteLine($file, "3@" & GUICtrlRead($txtName1))
		FileWriteLine($file, "4@" & GUICtrlRead($txtDNS2a))
		FileWriteLine($file, "5@" & GUICtrlRead($txtDNS2b))
		FileWriteLine($file, "6@" & GUICtrlRead($txtName2))
		FileWriteLine($file, "7@" & GUICtrlRead($txtDNS3a))
		FileWriteLine($file, "8@" & GUICtrlRead($txtDNS3b))
		FileWriteLine($file, "9@" & GUICtrlRead($txtName3))
		FileWriteLine($file, "10@" & GUICtrlRead($txtDNS4a))
		FileWriteLine($file, "11@" & GUICtrlRead($txtDNS4b))
		FileWriteLine($file, "12@" & GUICtrlRead($txtName4))
		FileWriteLine($file, "13@" & GUICtrlRead($txtDNS5a))
		FileWriteLine($file, "14@" & GUICtrlRead($txtDNS5b))
		FileWriteLine($file, "15@" & GUICtrlRead($txtName5))
		FileWriteLine($file, "16@" & GUICtrlRead($txtPing1))
		FileWriteLine($file, "17@" & GUICtrlRead($txtPing2))
		FileWriteLine($file, "18@" & GUICtrlRead($txtPing3))
		
		If GUICtrlRead($chkSetting1) = $GUI_CHECKED Then
			FileWriteLine($file, "19@" & "yes")
		Else
			FileWriteLine($file, "19@" & "no")
		EndIf
	
	
		If GUICtrlRead($chkSetting2) = $GUI_CHECKED Then
			FileWriteLine($file, "20@" & "yes")
		Else
			FileWriteLine($file, "20@" & "no")
		EndIf
		
		GUICtrlSetData ($txtStatus, "Save successfully")
	EndIf
	
	FileClose($file)
EndFunc


Func LoadConfig()
	GUICtrlSetData ($txtDNS1a, $setSetting[1])
	GUICtrlSetData ($txtDNS1b, $setSetting[2])
	GUICtrlSetData ($txtName1, $setSetting[3])
	GUICtrlSetData ($txtDNS2a, $setSetting[4])
	GUICtrlSetData ($txtDNS2b, $setSetting[5])
	GUICtrlSetData ($txtName2, $setSetting[6])
	GUICtrlSetData ($txtDNS3a, $setSetting[7])
	GUICtrlSetData ($txtDNS3b, $setSetting[8])
	GUICtrlSetData ($txtName3, $setSetting[9])
	GUICtrlSetData ($txtDNS4a, $setSetting[10])
	GUICtrlSetData ($txtDNS4b, $setSetting[11])
	GUICtrlSetData ($txtName4, $setSetting[12])
	GUICtrlSetData ($txtDNS5a, $setSetting[13])
	GUICtrlSetData ($txtDNS5b, $setSetting[14])
	GUICtrlSetData ($txtName5, $setSetting[15])
	GUICtrlSetData ($txtPing1, $setSetting[16])
	GUICtrlSetData ($txtPing2, $setSetting[17])
	GUICtrlSetData ($txtPing3, $setSetting[18])
	
	If $setSetting[19] = "yes" Then
		GUICtrlSetState ($chkSetting1,$GUI_CHECKED)
	Else
		GUICtrlSetState ($chkSetting1,$GUI_UNCHECKED)
	EndIf
		
	
	If $setSetting[20] = "yes" Then
		GUICtrlSetState ($chkSetting2,$GUI_CHECKED)
	Else
		GUICtrlSetState ($chkSetting2,$GUI_UNCHECKED)
	EndIf
	
	GUICtrlSetData ($txtStatus, "Settings loaded")
EndFunc

Func Help ()
	#Region ### START Koda GUI section ###
	$frmDNSeaseHelp = GUICreate("DNS Ease Tips", 400, 244, 359, 199)
	GUISetIcon("C:\Users\NGOC KHIEM\Desktop\DNS ease Icon.ico", -1)
	$Tips = GUICtrlCreateLabel("Tips:", 16, 8, 32, 17)
	GUICtrlSetFont(-1, 8, 800, 4, "MS Sans Serif")
	$Tip1 = GUICtrlCreateLabel("- Place your mouse cursor over a Button to see hint.", 16, 32, 248, 17)
	$Tip3 = GUICtrlCreateLabel("stores your Setting Information inside.  ", 104, 56, 185, 17)
	$Tip4 = GUICtrlCreateLabel("It is loaded automatically when DNS Ease starts.", 24, 72, 232, 17)
	$Tip6 = GUICtrlCreateLabel("- If DNSease.cfg is broken or can't be found.                               will be loaded.", 16, 120, 375, 17)
	$Tip2 = GUICtrlCreateLabel("DNSease.cfg", 24, 56, 79, 17)
	GUICtrlSetFont(-1, 8, 800, 0, "MS Sans Serif")
	$Label7 = GUICtrlCreateLabel("-", 16, 56, 7, 17)
	$Tip7 = GUICtrlCreateLabel("Defaul settings", 231, 120, 89, 17)
	GUICtrlSetFont(-1, 8, 800, 0, "MS Sans Serif")
	$Tip5 = GUICtrlCreateLabel("- DNSease.cfg should be kept in the same place with DNS Ease application.", 16, 96, 365, 17)
	$Tip8 = GUICtrlCreateLabel("- When  Error: '", 16, 144, 75, 17)
	$Tip9 = GUICtrlCreateLabel("Unable to save settings", 90, 144, 138, 17)
	GUICtrlSetFont(-1, 8, 800, 0, "MS Sans Serif")
	$Tip10 = GUICtrlCreateLabel("' occured,", 226, 144, 51, 17)
	$Tip11 = GUICtrlCreateLabel("the Disk/Folder may be write-protected. Please check your User permission.", 22, 160, 361, 17)
	$Tip12 = GUICtrlCreateLabel("Thank you very much for using this software.", 168, 200, 215, 17)
	$Tip13 = GUICtrlCreateLabel("F&&P e-Homeware © 2011", 208, 216, 153, 17)
	GUICtrlSetFont(-1, 8, 800, 0, "MS Sans Serif")
	GUISetState(@SW_SHOW)
	#EndRegion ### END Koda GUI section ###
	
	Sleep (100)
	WinActivate ($frmDNSeaseHelp)
	
	While 1
		If not WinActive($frmDNSeaseHelp) Then
			ExitLoop
		EndIf
		
		$nMsgHelp = GUIGetMsg()
		Switch $nMsgHelp
			Case $GUI_EVENT_CLOSE
				ExitLoop
		EndSwitch
	WEnd
		
	GUIDelete ($frmDNSeaseHelp)
EndFunc
	
