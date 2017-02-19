#cs ==========================================================
- Website: http://72ls.net
- Forum: http://autoitvn.co.cc
- Thiết kế: RynAki - Production
- AutoIT: v3.3.6.1
- Chức năng: Auto Hỗ Trợ Game Chiến Quốc
#ce ==========================================================

Func _MEMORYOPEN($IV_PID, $IV_DESIREDACCESS = 2035711, $IV_INHERITHANDLE = 1)
	If Not ProcessExists($IV_PID) Then
		SetError(1)
		Return 0
	EndIf
	Local $AH_HANDLE[2] = [DllOpen("kernel32.dll")]
	If @error Then
		SetError(2)
		Return 0
	EndIf
	Local $AV_OPENPROCESS = DllCall($AH_HANDLE[0], "int", "OpenProcess", "int", $IV_DESIREDACCESS, "int", $IV_INHERITHANDLE, "int", $IV_PID)
	If @error Then
		DllClose($AH_HANDLE[0])
		SetError(3)
		Return 0
	EndIf
	$AH_HANDLE[1] = $AV_OPENPROCESS[0]
	Return $AH_HANDLE
EndFunc


Func _MEMORYREAD($IV_ADDRESS, $AH_HANDLE, $SV_TYPE = "dword")
	If Not IsArray($AH_HANDLE) Then
		SetError(1)
		Return 0
	EndIf
	Local $V_BUFFER = DllStructCreate($SV_TYPE)
	If @error Then
		SetError(@error + 1)
		Return 0
	EndIf
	DllCall($AH_HANDLE[0], "int", "ReadProcessMemory", "int", $AH_HANDLE[1], "int", $IV_ADDRESS, "ptr", DllStructGetPtr($V_BUFFER), "int", DllStructGetSize($V_BUFFER), "int", "")
	If Not @error Then
		Local $V_VALUE = DllStructGetData($V_BUFFER, 1)
		Return $V_VALUE
	Else
		SetError(6)
		Return 0
	EndIf
EndFunc


Func _MEMORYWRITE($IV_ADDRESS, $AH_HANDLE, $V_DATA, $SV_TYPE = "dword")
	If Not IsArray($AH_HANDLE) Then
		SetError(1)
		Return 0
	EndIf
	Local $V_BUFFER = DllStructCreate($SV_TYPE)
	If @error Then
		SetError(@error + 1)
		Return 0
	Else
		DllStructSetData($V_BUFFER, 1, $V_DATA)
		If @error Then
			SetError(6)
			Return 0
		EndIf
	EndIf
	DllCall($AH_HANDLE[0], "int", "WriteProcessMemory", "int", $AH_HANDLE[1], "int", $IV_ADDRESS, "ptr", DllStructGetPtr($V_BUFFER), "int", DllStructGetSize($V_BUFFER), "int", "")
	If Not @error Then
		Return 1
	Else
		SetError(7)
		Return 0
	EndIf
EndFunc


Func _MEMORYCLOSE($AH_HANDLE)
	If Not IsArray($AH_HANDLE) Then
		SetError(1)
		Return 0
	EndIf
	DllCall($AH_HANDLE[0], "int", "CloseHandle", "int", $AH_HANDLE[1])
	If Not @error Then
		DllClose($AH_HANDLE[0])
		Return 1
	Else
		DllClose($AH_HANDLE[0])
		SetError(2)
		Return 0
	EndIf
EndFunc


Func SETPRIVILEGE($PRIVILEGE, $BENABLE)
	Const $TOKEN_ADJUST_PRIVILEGES = 32
	Const $TOKEN_QUERY = 8
	Const $SE_PRIVILEGE_ENABLED = 2
	Local $HTOKEN, $SP_AUXRET, $SP_RET, $HCURRPROCESS, $NTOKENS, $NTOKENINDEX, $PRIV
	$NTOKENS = 1
	$LUID = DllStructCreate("dword;int")
	If IsArray($PRIVILEGE) Then $NTOKENS = UBound($PRIVILEGE)
	$TOKEN_PRIVILEGES = DllStructCreate("dword;dword[" & (3 * $NTOKENS) & "]")
	$NEWTOKEN_PRIVILEGES = DllStructCreate("dword;dword[" & (3 * $NTOKENS) & "]")
	$HCURRPROCESS = DllCall("kernel32.dll", "hwnd", "GetCurrentProcess")
	$SP_AUXRET = DllCall("advapi32.dll", "int", "OpenProcessToken", "hwnd", $HCURRPROCESS[0], "int", BitOR($TOKEN_ADJUST_PRIVILEGES, $TOKEN_QUERY), "int_ptr", 0)
	If $SP_AUXRET[0] Then
		$HTOKEN = $SP_AUXRET[3]
		DllStructSetData($TOKEN_PRIVILEGES, 1, 1)
		$NTOKENINDEX = 1
		While $NTOKENINDEX <= $NTOKENS
			If IsArray($PRIVILEGE) Then
				$PRIV = $PRIVILEGE[$NTOKENINDEX - 1]
			Else
				$PRIV = $PRIVILEGE
			EndIf
			$RET = DllCall("advapi32.dll", "int", "LookupPrivilegeValue", "str", "", "str", $PRIV, "ptr", DllStructGetPtr($LUID))
			If $RET[0] Then
				If $BENABLE Then
					DllStructSetData($TOKEN_PRIVILEGES, 2, $SE_PRIVILEGE_ENABLED, (3 * $NTOKENINDEX))
				Else
					DllStructSetData($TOKEN_PRIVILEGES, 2, 0, (3 * $NTOKENINDEX))
				EndIf
				DllStructSetData($TOKEN_PRIVILEGES, 2, DllStructGetData($LUID, 1), (3 * ($NTOKENINDEX - 1)) + 1)
				DllStructSetData($TOKEN_PRIVILEGES, 2, DllStructGetData($LUID, 2), (3 * ($NTOKENINDEX - 1)) + 2)
				DllStructSetData($LUID, 1, 0)
				DllStructSetData($LUID, 2, 0)
			EndIf
			$NTOKENINDEX += 1
		WEnd
		$RET = DllCall("advapi32.dll", "int", "AdjustTokenPrivileges", "hwnd", $HTOKEN, "int", 0, "ptr", DllStructGetPtr($TOKEN_PRIVILEGES), "int", DllStructGetSize($NEWTOKEN_PRIVILEGES), "ptr", DllStructGetPtr($NEWTOKEN_PRIVILEGES), "int_ptr", 0)
		$F = DllCall("kernel32.dll", "int", "GetLastError")
	EndIf
	$NEWTOKEN_PRIVILEGES = 0
	$TOKEN_PRIVILEGES = 0
	$LUID = 0
	If $SP_AUXRET[0] = 0 Then Return 0
	$SP_AUXRET = DllCall("kernel32.dll", "int", "CloseHandle", "hwnd", $HTOKEN)
	If Not $RET[0] And Not $SP_AUXRET[0] Then Return 0
	Return $RET[0]
EndFunc



Opt("GUIOnEventMode", 1)
Opt("TrayIconHide", 1)
Opt("SendAttachMode", 1)
Opt("SendKeyDownDelay", 25)

#include <GUIConstantsEx.au3>
#include <WindowsConstants.au3>
#include <ButtonConstants.au3>

Global $WINDOW  = "[CLASS:WSWINDOW]" ;~ Lấy Class của Game = Window Info
Dim $BASE_CHAR_ADD = 0x771754 ;~ Base Add Char , bạn dùng Cheat Engine 5.6 để scan
Dim $BASE_ORD_CHAR_ADD = 0x77F9E8 ;~ Base Add Tọa độ Char , bạn dùng Cheat Engine 5.6 để scan
Dim $INT_CHAR_HP = 0 ;~ Kiểu HP ban đầu
Dim $INT_CHAR_SP = 0 ;~ Kiểu SP ban đầu
Dim $X_CHAR_OFFSET = 0x18 ;~ Offset của tọa độ X
Dim $Y_CHAR_OFFSET = 0x1c ;~ Offset của tọa độ Y
Dim $KH_MAX_OFFSET = 0x1530 ;~ Offset của máu max
Dim $PL_MAX_OFFSET = 0x1538 ;~ Offset của mana max
Dim $EXP_MAX_OFFSET= 0x15C4 ;~ Offset của exp max
Dim $LV_CHAR_OFFSET= 0x15B0 ;~ Offset của lv max
Dim $KH_CURRENT_OFFSET = 0x152c ;~ Offset của máu hiện tại
Dim $PL_CURRENT_OFFSET = 0x1534 ;~ Offset của mana hiện tại
Dim $EXP_CURRENT_OFFSET= 0x159c ;~ Offset của exp hiện tại
Dim $CHAR_BUFFER, $ORD_CHAR_BUFFER, $MEMORY_OPEN, $PID ; Khai báo các biến để đọc memory Game
Dim $KH_CURRENT, $PL_CURRENT,$EXP_CURRENT, $KH_MAX, $PL_MAX, $EXP_MAX, $X_CHAR,  $LV_CHAR , $EXP_PERCENT, $Y_CHAR, $KH_PERCENT, $PL_PERCENT, $KEY_CHAR_HP, $KEY_CHAR_SP
;~ Khai báo các biến đọc chỉ số HP , SP , EXP , LV và các thanh đọc % Percent
Dim $HP_CHAR_BUFF, $SP_CHAR_BUFF ;~ Khai káo biến để xử lý bơm HP vs MP
Dim $TIMERINFO, $TIME_MAXHP, $TIME_MAXSP, $TIME_MAXMP , $TIME_MAXEXP, $TIME_BUFF_HP_CHAR, $TIME_BUFF_SP_CHAR
;~ Khai báo các biến thời gian để tính thời gian delay sử dụng HP , MP
Dim $MAXHP_CHECK = False, $MAXSP_CHECK = False, $START_OK = False ,$MAXEXP_CHECK= False
;~ Khai báo các biến để kiểm tra
Dim $BUFF_HP_CHAR = False, $BUFF_SP_CHAR = False
;~ Khai báo các biến để kiểm tra
Dim $TIME_DELAY = 30000 ;~ Khai báo biến thời gian delay

#Region ### START Koda GUI section ### Form=
$CheckGame = GUICreate("Check Char", 447, 259, 199, 176)
$LV_UP = GUICtrlCreateLabel("", 32, 140, 100, 17)
GUICtrlSetFont(-1, 10, 400, 0)
GUICtrlSetColor(-1, 0x0000FF)
$LABEL_HP_LEADER = GUICtrlCreateLabel("HP :", 32, 30, 30, 17)
GUICtrlSetFont(-1, 10, 400, 0)
GUICtrlSetColor(-1, 0x0000FF)
$PRO_HP_LEADER = GUICtrlCreateProgress(68, 30, 126, 17)
$PRO_EXP_LEADER= GUICtrlCreateProgress(68, 90, 126, 17)
$EXP_LEADER= GUICtrlCreateLabel("",200, 88, 200, 17)
GUICtrlSetFont(-1, 10, 400, 0)
GUICtrlSetColor(-1, 0x0000FF)
$EXP= GUICtrlCreateLabel("EXP :", 32, 90, 35, 17)
GUICtrlSetFont(-1, 10, 400, 0)
GUICtrlSetColor(-1, 0x0000FF)
$LABEL_SP_LEADER = GUICtrlCreateLabel("MP :", 32, 60, 30, 17)
GUICtrlSetFont(-1, 10, 400, 0)
GUICtrlSetColor(-1, 0x0000FF)
$Toado_Char = GUICtrlCreateLabel("",32,115,100,17)
GUICtrlSetFont(-1, 10, 400, 0)
GUICtrlSetColor(-1, 0x0000FF)
$PRO_SP_LEADER = GUICtrlCreateProgress(68, 60, 126, 17)
$USE_HP_LEADER = GUICtrlCreateCheckbox("Sử dụng HP <", 32, 161, 85, 17)
$USE_SP_LEADER = GUICtrlCreateCheckbox("Sử dụng MP <", 32, 185, 85, 17)
$CHOOSE_HP_LEADER = GUICtrlCreateCombo("", 172, 161, 150, 25)
GUICtrlSetColor(-1, 0x0000FF)
GUICtrlSetData(-1, "{F1}|{F2}|{F3}|{F4}|{F5}|{F6}|{F7}|{F8}|{F9}|{F10}", "{F1}")
$CHOOSE_SP_LEADER = GUICtrlCreateCombo("", 172, 185, 150, 25)
GUICtrlSetColor(-1, 0x0000FF)
GUICtrlSetData(-1, "{F1}|{F2}|{F3}|{F4}|{F5}|{F6}|{F7}|{F8}|{F9}|{F10}", "{F2}")
$HP_LEADER = GUICtrlCreateLabel("", 200, 30, 117, 17)
GUICtrlSetFont(-1, 10, 400, 0)
GUICtrlSetColor(-1, 0x0000FF)
$SP_LEADER = GUICtrlCreateLabel("", 200, 60, 117, 17)
GUICtrlSetFont(-1, 10, 400, 0)
GUICtrlSetColor(-1, 0x0000FF)
$HP_LEAD = GUICtrlCreateInput("70", 124, 161, 25, 21) ;~ Chỉ số % của HP
GUICtrlSetColor(-1, 0x0000FF)
$SP_LEAD = GUICtrlCreateInput("50", 124, 185, 25, 21) ;~ Chỉ số % của MP
GUICtrlSetColor(-1, 0x0000FF)
$LABEL1= GUICtrlCreateLabel("%", 156, 165, 12, 17)
GUICtrlSetColor(-1, 0x0000FF)
$LABEL2= GUICtrlCreateLabel("%", 156, 190, 12, 17)
GUICtrlSetColor(-1, 0x0000FF)
$LABEL5= GUICtrlCreateLabel("Delay", 330, 165, 31, 17)
GUICtrlSetColor(-1, 0x0000FF)
$DELAY_HP_CHAR= GUICtrlCreateInput("10", 370, 161, 25, 21) ;~ Thời gian delay sử dụng HP được tính = giây
GUICtrlSetColor(-1, 0x0000FF)
$LABEL6= GUICtrlCreateLabel("Delay", 330, 190, 31, 17)
GUICtrlSetColor(-1, 0x0000FF)
$DELAY_SP_CHAR = GUICtrlCreateInput("10", 370, 185, 25, 21) ;~ Thời gian delay sử dụng MP được tính = giây
GUICtrlSetColor(-1, 0x0000FF)
$START = GUICtrlCreateButton("START", 230, 120, 75, 25, 0)
GUICtrlSetOnEvent($START, "main")
$EXIT = GUICtrlCreateButton("EXIT", 320, 120, 75, 25, 0)
GUICtrlSetOnEvent($EXIT, "exitAuto")
GUISetOnEvent($GUI_EVENT_CLOSE, "exitAuto")
GUISetState(@SW_SHOW)
#EndRegion ### END Koda GUI section ###

;~ Vòng lặp kiểm tra và show các thông số hiễn thị trên GUI
While 1
	If $START_OK Then ;~ Nếu click button thì bắt đầu check và xử lý
		If TimerDiff($TIMERINFO) > 1000 Then ; Thời gian delay
				SHOWCHAR()
			If (CHECKWINDOWNS() == True) Then
				STOPAUTO()
				WinSetState("Game ...Disconnet", "", @SW_SHOW)
				MsgBox(64, "Disconnect...", "Mất kết nối!")
			EndIf
			CHARBUFF()
			$TIMERINFO = TimerInit()
		EndIf
	EndIf
	Sleep(72)
WEnd

Func OPENMEMORYCHAR()
	$WINDOW = WinGetTitle("Chien Quoc") ;~ Tiêu đề text của Game
	$PID = WinGetProcess($WINDOW) ;~ Open cửa sổ Game
	$MEMORY_OPEN = _MEMORYOPEN($PID) ;~ Đọc Memory của cửa sổ Game
	$CHAR_BUFFER = _MEMORYREAD($BASE_CHAR_ADD, $MEMORY_OPEN)
	$ORD_CHAR_BUFFER = _MEMORYREAD($BASE_ORD_CHAR_ADD, $MEMORY_OPEN)
EndFunc

;~ Đọc HP hiện tại của nhân vật

Func READHPCHAR()
	$KH_CURRENT_ADD = $CHAR_BUFFER + $KH_CURRENT_OFFSET
	$KH_CURRENT = _MEMORYREAD($KH_CURRENT_ADD, $MEMORY_OPEN)
	Return $KH_CURRENT
EndFunc

;~ Đọc MP hiện tại của nhân vật

Func READSPCHAR()
	$PL_CURRENT_ADD = $CHAR_BUFFER + $PL_CURRENT_OFFSET
	$PL_CURRENT = _MEMORYREAD($PL_CURRENT_ADD, $MEMORY_OPEN)
	Return $PL_CURRENT
EndFunc

;~ Đọc HP max của nhân vậTan

Func READMAXHPCHAR()
	If ($MAXHP_CHECK == False) Then
		$KH_MAX_ADD = $CHAR_BUFFER + $KH_MAX_OFFSET
		$KH_MAX = _MEMORYREAD($KH_MAX_ADD, $MEMORY_OPEN)
		$MAXHP_CHECK = Not $MAXHP_CHECK
	Else
		If TimerDiff($TIME_MAXHP) > $TIME_DELAY Then
			$KH_MAX_ADD = $CHAR_BUFFER + $KH_MAX_OFFSET
			$KH_MAX = _MEMORYREAD($KH_MAX_ADD, $MEMORY_OPEN)
			$TIME_MAXHP = TimerInit()
		EndIf
	EndIf
	Return $KH_MAX
EndFunc

;~ Đọc MP max của nhân vật

Func READMAXSPCHAR()
	If ($MAXSP_CHECK == False) Then
		$PL_MAX_ADD = $CHAR_BUFFER + $PL_MAX_OFFSET
		$PL_MAX = _MEMORYREAD($PL_MAX_ADD, $MEMORY_OPEN)
		$MAXSP_CHECK = Not $MAXSP_CHECK
	Else
		If TimerDiff($TIME_MAXSP) > $TIME_DELAY Then
			$PL_MAX_ADD = $CHAR_BUFFER + $PL_MAX_OFFSET
			$PL_MAX = _MEMORYREAD($PL_MAX_ADD, $MEMORY_OPEN)
			$TIME_MAXSP = TimerInit()
		EndIf
	EndIf
	Return $PL_MAX
EndFunc

;~ Đọc tọa độ X của nhân vật

Func READXCHAR()
	$X_ADD = $ORD_CHAR_BUFFER + $X_CHAR_OFFSET
	$X_CHAR = _MEMORYREAD($X_ADD, $MEMORY_OPEN)
	Return $X_CHAR
EndFunc

;~ Đọc tọa độ Y của nhân vật

Func READYCHAR()
	$Y_ADD = $ORD_CHAR_BUFFER + $Y_CHAR_OFFSET
	$Y_CHAR = _MEMORYREAD($Y_ADD, $MEMORY_OPEN)
	Return $Y_CHAR
EndFunc

;~ Đọc Lever của nhân vật

Func READLVCHAR()
	$LV_ADD= $CHAR_BUFFER + $LV_CHAR_OFFSET
	$LV_CHAR= _MEMORYREAD($LV_ADD, $MEMORY_OPEN)
	Return $LV_CHAR
EndFunc

;~ Đọc Exp hiện tại của nhân vật

Func READEXPCHAR()
	$EXP_CURRENT_ADD= $CHAR_BUFFER + $EXP_CURRENT_OFFSET
	$EXP_CURRENT= _MEMORYREAD($EXP_CURRENT_ADD, $MEMORY_OPEN)
	Return $EXP_CURRENT
EndFunc

;~ Đọc Exp max cần up cấp của nhân vật

Func READMAXEXPCHAR()
	If ($MAXEXP_CHECK== False) Then
	$EXP_MAX_ADD= $CHAR_BUFFER + $EXP_MAX_OFFSET
	$EXP_MAX= _MEMORYREAD($EXP_MAX_ADD, $MEMORY_OPEN)
	$MAXEXP_CHECK= Not $MAXEXP_CHECK
Else
	If TimerDiff($TIME_MAXEXP)> $TIME_DELAY Then
	$EXP_MAX_ADD= $CHAR_BUFFER + $EXP_MAX_OFFSET
	$EXP_MAX= _MEMORYREAD($EXP_MAX_ADD, $MEMORY_OPEN)
	$TIME_MAXEXP= TimerInit()
	EndIf
	EndIf
	Return $EXP_MAX
EndFunc

;~ Show các thông số của nhân vật lên GUI

Func SHOWCHAR()
		READHPCHAR()
		READMAXHPCHAR()
		READSPCHAR()
		READMAXSPCHAR()
		READEXPCHAR()
		READMAXEXPCHAR()
		$KH_PERCENT = GETPERCENT($KH_CURRENT, $KH_MAX) ;~ Đọc thanh % máu của char
		$PL_PERCENT = GETPERCENT($PL_CURRENT, $PL_MAX) ;~ Đọc thanh % mana của char
		$EXP_PERCENT= GETPERCENT($EXP_CURRENT, $EXP_MAX) ;~ Đọc thanh % exp của char
		$X_CHAR = READXCHAR() ;~ Đọc tọa độ X của char
		$Y_CHAR = READYCHAR() ;~ Đọc tọa độ Y của char
		$LV_CHAR= READLVCHAR() ;~ Đọc lv của char

		;~ Sét thông số HP lên nhãn $SP_LEADER để hiễn thị trên GUI
		GUICtrlSetData($HP_LEADER, $KH_CURRENT & "  /  " & $KH_MAX)
		GUICtrlSetData($PRO_HP_LEADER, $KH_PERCENT)
		;~ Sét thông số MP lên nhãn $MP_LEADER để hiễn thị trên GUI
		GUICtrlSetData($SP_LEADER, $PL_CURRENT & "  /  " & $PL_MAX)
		GUICtrlSetData($PRO_SP_LEADER, $PL_PERCENT)
		;~ Sét thông số EXP lên nhãn $EXP_LEADER để hiễn thị trên GUI
		GUICtrlSetData($EXP_LEADER, $EXP_CURRENT& "  /  "& $EXP_MAX)
		GUICtrlSetData($PRO_EXP_LEADER, $EXP_PERCENT)
		;~ Sét thông số Lever  lên nhãn $LV_UP để hiễn thị trên GUI
		GUICtrlSetData($LV_UP, "Lever : "& $LV_CHAR)
		;~ Sét thông số tọa độ lên nhãn $Toado_Char để hiễn thị trên GUI
		GUICtrlSetData($Toado_Char, "X :"& $X_CHAR& " Y : "& $Y_CHAR)
EndFunc

	;~ Hàm tính % máu , mana , exp của char
Func GETPERCENT($NUMBER, $NUMBER_MAX)
	$PERCENT = Round(($NUMBER / $NUMBER_MAX) * 100)
	Return $PERCENT
EndFunc

	;~ Kiểm tra Checkbox sử dụng HP , MP
Func CHARBUFF()
	If (GUICtrlRead($USE_HP_LEADER) = $GUI_CHECKED) Then
		PROCESSBUFF("1", GUICtrlRead($CHOOSE_HP_LEADER), GUICtrlRead($DELAY_HP_CHAR) & "000")
	EndIf
	If (GUICtrlRead($USE_SP_LEADER) = $GUI_CHECKED) Then
		PROCESSBUFF("2", GUICtrlRead($CHOOSE_SP_LEADER), GUICtrlRead($DELAY_SP_CHAR) & "000")
	EndIf
EndFunc

	;~ Sử dụng HP , MP
Func PROCESSBUFF($STR, $KEY, $DELAY)
	$HP_CHAR_BUFF = GUICtrlRead($HP_LEAD) ;~ Đọc thông số HP hiễn thị trên GUI để xử lý
	$SP_CHAR_BUFF = GUICtrlRead($SP_LEAD) ;~ Đọc thông số MP hiễn thị trên GUI để xử lý
	If ($STR == 1) Then ;~ Nếu PROCESSBUFF = 1 thì kiểm tra HP và buff
		If ($KH_PERCENT <= $HP_CHAR_BUFF And $BUFF_HP_CHAR == False) Then ;~ Kiểm tra thanh % HP
			ControlSend($WINDOW, "", "", $KEY) ;~ Send phím ( $KEY tức là các phím để sử dụng HP , MP )
			$BUFF_HP_CHAR = True
		ElseIf ($KH_PERCENT <= 100 And $BUFF_HP_CHAR == True) Then
			If TimerDiff($TIME_BUFF_HP_CHAR) > $DELAY Then
				ControlSend($WINDOW, "", "", $KEY)
				$TIME_BUFF_HP_CHAR = TimerInit()
			EndIf
			If ($KH_PERCENT == 100) Then
				$BUFF_HP_CHAR = False
			EndIf
		EndIf
	EndIf
	If ($STR == 2) Then ;~ Nếu PROCESSBUFF = 2 thì kiểm tra MP và buff
		If ($PL_PERCENT <= $SP_CHAR_BUFF And $BUFF_SP_CHAR == False) Then ;~ Kiểm tra thanh % HP
			ControlSend($WINDOW, "", "", $KEY) ;~ Send phím ( $KEY tức là các phím để sử dụng HP , MP )
			$BUFF_SP_CHAR = True
		ElseIf ($PL_PERCENT <= 100 And $BUFF_SP_CHAR == True) Then
			If TimerDiff($TIME_BUFF_SP_CHAR) > $DELAY Then
				ControlSend($WINDOW, "", "", $KEY)
				$TIME_BUFF_SP_CHAR = TimerInit()
			EndIf
			If ($PL_PERCENT == 100) Then
				$BUFF_SP_CHAR = False
			EndIf
		EndIf
	EndIf
EndFunc

;~ Xử lý button START
Func MAIN()
			$BUT_STAR = GUICtrlRead($START)
	If ($BUT_STAR == "START" == True) Then
			$START_OK = Not $START_OK
			OPENMEMORYCHAR()
			GUICtrlSetData($START, "STOP")
	ElseIf ($BUT_STAR == "STOP") Then
			$START_OK = Not $START_OK
			STOPAUTO()
	EndIf
EndFunc

;~ Tạm dừng Auto

Func STOPAUTO()
		_MEMORYCLOSE($PID)
		GUICtrlSetData($START, "START")
		GUICtrlSetData($HP_LEADER, "0 / 0")
		GUICtrlSetData($PRO_HP_LEADER, "0")
		GUICtrlSetData($EXP_LEADER, "0 / 0")
		GUICtrlSetData($PRO_EXP_LEADER, "0")
		GUICtrlSetData($SP_LEADER, "0 / 0")
		GUICtrlSetData($PRO_SP_LEADER, "0")
		$MAXHP_CHECK = False
		$MAXSP_CHECK = False
		$START_OK = False
		$BUFF_HP_CHAR = False
		$BUFF_SP_CHAR = False
EndFunc

;~ Exit Auto

Func EXITAUTO()
	_MEMORYCLOSE($PID)
	GUIDelete()
	Exit
EndFunc

;~ Kiểm tra cửa sổ của Game

Func CHECKWINDOWNS()
	$WINDOWS = WinGetTitle("Chien Quoc")
	If (WinExists($WINDOWS) == 1) Then
		Return False
	Else
		Return True
	EndIf
EndFunc

