#include <ButtonConstants.au3>
#include <ComboConstants.au3>
#include <EditConstants.au3>
#include <GUIConstantsEx.au3>
#include <StaticConstants.au3>
#include <WindowsConstants.au3>
#include <process.au3>
#include <GuiRichEdit.au3>
;~ 648F00,648EE8,248EE4
;~ tarwai 278,c,648EE8
;~ $Label30,$Combo9
#include <StructureConstants.au3>
opt("GuiOnEventMode",1)
opt("TrayOnEventMode", 1)
Opt("TrayMenuMode",1)
Opt("SendKeyDownDelay",60)
opt("SendKeyDelay",2)
opt ("SendCapslockMode",0)
opt("SendAttachMode",0)
HotKeySet("{2}","checkwai")
HotKeySet("{1}","show")
Global $start="START", $stop= "STOP",$logic=True,$cuong,$docmang=False
Global $nick,$shutdown,$title,$newtitle
Global $Progress_hp[2][2],$Progress_mp[2][2],$LabelProgress[1][6] = [[0]]
Global $vip,$SKILL[3][8]
Global $HP[3],$HP_MAX[3],$phim_HP_nv[3],$HP_nv[3]
Global $MP[3],$MP_MAX[3],$phim_MP_nv[3],$MP_nv[3]
Global $number_pet[3],$HP_pet[3],$phim_HP_pet[3],$base_pet[3]
Global $tudanh[3],$ngu[3],$nhan[3]
Global $Combo[12],$Input[3],$count=0
Global $nho[4],$base_pet_max[3]
#Region ### START Koda GUI section ### Form=C:\koda_1.7.2.0\Forms\auto.kxf
$Form1 = GUICreate("AUTO TLBB (HCC)", 263, 404, 410, 116)
GUISetOnEvent($GUI_EVENT_CLOSE,"_exit")
GUISetOnEvent($GUI_EVENT_MINIMIZE,"show")
TrayCreateItem("","")
TraySetOnEvent(-8,"show")
$Tab0 = GUICtrlCreateTab(0, 0, 257, 393)
$tab1 = GUICtrlCreateTabItem("Thông tin nv")
$List1 = GUICtrlCreateListView("Tên nhân vật       |    cấp    ", 8, 25, 240, 70)
$Label1 = GUICtrlCreateLabel("Máu", 8, 114, 25, 17)
$Progress_hp[1][0] = GUICtrlCreateLabelProgress("0/0", 45, 118, 70, 17,1,16711680,0xFFFFF)
$Group1 = GUICtrlCreateGroup("Nhân vật", 8, 96, 113, 73)
GUICtrlCreateGroup("", -99, -99, 1, 1)
$Label2 = GUICtrlCreateLabel("Mana", 6, 142, 31, 17)
$Progress_mp[1][0] = GUICtrlCreateLabelProgress("0/0", 46, 141, 70, 17,1,6184703,0xFFFFF)
$Label3 = GUICtrlCreateLabel("Máu", 132, 118, 25, 17)
$Progress_hp[0][1] = GUICtrlCreateLabelProgress("0/0", 169, 114, 70, 17,1,16711680,0xFFFFF)
$Label4 = GUICtrlCreateLabel("Mana", 130, 138, 31, 17)
$Progress_mp[0][1] = GUICtrlCreateProgress(170, 137, 70, 17)
$Group2 = GUICtrlCreateGroup("Thú nuôi", 126, 97, 121, 73)
GUICtrlCreateGroup("", -99, -99, 1, 1)

$Label5 = GUICtrlCreateLabel("- Chủ máy", 15, 176, 51, 17)
$Label6 = GUICtrlCreateLabel(@UserName,79, 176, 138, 17)
$Label7 = GUICtrlCreateLabel("- Hệ điều hành :", 15, 200, 78, 17)
$Label8 = GUICtrlCreateLabel(@OSVersion&@TAB&@OSServicePack,95, 200, 82, 25)
$Label9 = GUICtrlCreateLabel("- RAM :", 15, 232, 40, 17)
$Label10 = GUICtrlCreateLabel("Label26", 63, 232, 42, 17)
$Label11 = GUICtrlCreateLabel("- RAM đã sử dụng :", 15, 256, 96, 17)
$Progress_ram = GUICtrlCreateLabelProgress("0/0", 119, 254, 102, 17,1,0xffd7c6,0xdeebef)
$Checkbox1 = GUICtrlCreateCheckbox("ẩn game", 15, 280, 65, 17)
GUICtrlSetOnEvent(-1,"an")
$Combo1 = GUICtrlCreateCombo("", 157, 291, 73, 25)
GUICtrlSetData(-1, "shutdown|stand by|logoff","shutdown")
$Label12 = GUICtrlCreateLabel("chọn", 127, 293, 28, 17)
$Group3 = GUICtrlCreateGroup("shutdown", 123, 274, 121, 105)

$Label13 = GUICtrlCreateLabel("Time", 127, 327, 27, 17)
$Input1 = GUICtrlCreateInput("", 160, 320, 49, 21)
$q=GUICtrlCreateLabel("phút", 216, 328, 25, 17)
$Button1 = GUICtrlCreateButton("Bắt đầu", 160, 344, 51, 25, $WS_GROUP)
GUICtrlSetOnEvent(-1,"_shutdown")
GUICtrlCreateGroup("", -99, -99, 1, 1)
$Checkbox2 = GUICtrlCreateCheckbox("nv chết shutdown", 16, 304, 105, 17)
$Button2 = GUICtrlCreateButton("START", 16, 328, 75, 25, $WS_GROUP)
GUICtrlSetOnEvent(-1,"thaydoi")
$Button3 = GUICtrlCreateButton("EXIT", 16, 360, 75, 25, $WS_GROUP)
GUICtrlSetOnEvent(-1,"_exit")
;~ $Button3,$Checkbox2,$Label13,$Group3
;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;

$tab2 = GUICtrlCreateTabItem("bơm máu")
$Label13 = GUICtrlCreateLabel("Máu <", 8, 64, 30, 17)
$Input[0] = GUICtrlCreateInput("5111", 48, 56, 65, 21)
$Label14 = GUICtrlCreateLabel("Phím", 128, 64, 29, 17)
$Combo[1] = GUICtrlCreateCombo("", 168, 56, 57, 25)
GUICtrlSetData(-1, "None|F1|F2|F3|F4|F5|F6|F7|F8|F9|F10","None")
$Label15 = GUICtrlCreateLabel("Mana <", 8, 104, 40, 17)
$Input[1] = GUICtrlCreateInput("511", 48, 96, 65, 21)
$Label16 = GUICtrlCreateLabel("Phím", 128, 104, 29, 17)
$Combo[2] = GUICtrlCreateCombo("", 168, 96, 57, 25)
GUICtrlSetData(-1,"None|F1|F2|F3|F4|F5|F6|F7|F8|F9|F10","None")
$Label17 = GUICtrlCreateLabel("SKILL", 24, 128, 33, 17)
$Checkbox2 = GUICtrlCreateCheckbox("tự đánh", 72, 128, 60, 17)
;~ $Checkbox3 = GUICtrlCreateCheckbox("t? ch?n qu�i", 140, 128, 97, 17)
$Label18 = GUICtrlCreateLabel("skill 1", 13, 162, 30, 17)
$Combo[3] = GUICtrlCreateCombo("", 54, 155, 57, 25)
GUICtrlSetData(-1, "None|F1|F2|F3|F4|F5|F6|F7|F8|F9|F10","None")
$Combo[4] = GUICtrlCreateCombo("", 172, 196, 57, 25)
GUICtrlSetData(-1, "None|F1|F2|F3|F4|F5|F6|F7|F8|F9|F10","None")
$Combo[5] = GUICtrlCreateCombo("", 172, 155, 57, 25)
GUICtrlSetData(-1, "None|F1|F2|F3|F4|F5|F6|F7|F8|F9|F10","None")
$Combo[6] = GUICtrlCreateCombo("", 54, 194, 57, 25)
GUICtrlSetData(-1, "None|F1|F2|F3|F4|F5|F6|F7|F8|F9|F10","None")
$Label19 = GUICtrlCreateLabel("skill 2", 132, 163, 30, 17)
$Label20 = GUICtrlCreateLabel("skill 3", 12, 198, 30, 17)
$Label21 = GUICtrlCreateLabel("skill 4", 131, 197, 30, 17)
$Label22 = GUICtrlCreateLabel("skill 5", 12, 230, 30, 17)
$Combo[7] = GUICtrlCreateCombo("", 54, 222, 57, 25)
GUICtrlSetData(-1, "None|F1|F2|F3|F4|F5|F6|F7|F8|F9|F10","None")
$Label23 = GUICtrlCreateLabel("skill 6", 131, 230, 30, 17)
$Combo[8] = GUICtrlCreateCombo("", 172, 222, 57, 25)
GUICtrlSetData(-1, "None|F1|F2|F3|F4|F5|F6|F7|F8|F9|F10","None")
$Group4 = GUICtrlCreateGroup("Nhân vật", 8, 25, 233, 220)
GUICtrlCreateGroup("", -99, -99, 1, 1)
$Group5 = GUICtrlCreateGroup("Thú nuôi", 8, 248, 233, 89)
$Label24 = GUICtrlCreateLabel("Máu <", 16, 280, 30, 17)
$Input[2] = GUICtrlCreateInput("511", 50, 274, 65, 21)
$Label25 = GUICtrlCreateLabel("Phím", 129, 277, 29, 17)
$Combo[10] = GUICtrlCreateCombo("", 168, 273, 57, 25)
GUICtrlSetData(-1, "None|F1|F2|F3|F4|F5|F6|F7|F8|F9|F10","None")
$Label26 = GUICtrlCreateLabel("skill 1", 18, 313, 30, 17)
$Combo[9] = GUICtrlCreateCombo("", 53, 306, 57, 25)
GUICtrlSetData(-1, "None|F1|F2|F3|F4|F5|F6|F7|F8|F9|F10","None")
$Label27 = GUICtrlCreateLabel("PET", 133, 310, 25, 17)
$Combo[11] = GUICtrlCreateCombo("", 171, 304, 33, 25)
GUICtrlSetData(-1, "0|1|2|3|4","0")
GUICtrlCreateGroup("", -99, -99, 1, 1)
$Button4 = GUICtrlCreateButton("START", 64, 352, 59, 25, $WS_GROUP)
GUICtrlSetOnEvent(-1,"thaydoi")
$Button5 = GUICtrlCreateButton("EXIT", 136, 352, 59, 25, $WS_GROUP)
GUICtrlSetOnEvent(-1,"_exit")
$tab2 = GUICtrlCreateTabItem("HCC")
$Label13 = GUICtrlCreateLabel("Thiết kế: Hoàng Chi Cường", 16, 48, 133, 17)
$Label14 = GUICtrlCreateLabel("Phiên bản: V 1.3", 16, 80, 80, 17)
$Label15 = GUICtrlCreateLabel("Nick: anhchangtinhsi2007@yahoo.com", 16, 112, 189, 17)
$Label16 = GUICtrlCreateLabel("Địa chỉ: Huyện Định Quán_Tỉnh Đồng Nai", 16, 144, 200, 17)
$Group6 = GUICtrlCreateGroup("Hướng dẫn", 8, 176, 233, 209)
text()
GUICtrlCreateGroup("", -99, -99, 1, 1)
GUISetState(@SW_SHOW)
GUIRegisterMsg($WM_NOTIFY,"_WM_NOTIFY")
#EndRegion ### END Koda GUI section ###
$nhan[1]=$start
$nhan[2]=$start
capnhapgame()
title()
Func _WM_NOTIFY($hWnd,$MsgID,$wParam,$lParam)
	Global $cu
			$data_struct=DllStructGetData(DllStructCreate($tagNMHDR, $LParam),3)
			If $data_struct=-2 Then
				$nick = Number(StringLeft(GUICtrlRead(GUICtrlRead($List1)),1))
			If $nick <>0 Then
				if $ngu[$nick] =1 Then
					GUICtrlSetState($Checkbox1,1)
				Else
					GUICtrlSetState($Checkbox1,4)
				EndIf
				if $nick <> $cu then
					$cu=$nick
				If $tudanh[$nick]=1 Then
					GUICtrlSetState($Checkbox2,$GUI_CHECKED)
				Else
					GUICtrlSetState($Checkbox2,4)
				EndIf
						GUICtrlSetData($Input[0],$HP_nv[$nick])
						GUICtrlSetData($Input[1],$MP_nv[$nick])
						GUICtrlSetData($Input[2],$HP_pet[$nick])
						GUICtrlSetData($Combo[1],$phim_HP_nv[$nick])
						GUICtrlSetData($Combo[2],$phim_MP_nv[$nick])
						GUICtrlSetData($Combo[10],$phim_HP_pet[$nick])
						GUICtrlSetData($Combo[3],$SKILL[$nick][0])
						GUICtrlSetData($Combo[4],$SKILL[$nick][1])
						GUICtrlSetData($Combo[5],$SKILL[$nick][2])
						GUICtrlSetData($Combo[6],$SKILL[$nick][3])
						GUICtrlSetData($Combo[7],$SKILL[$nick][4])
						GUICtrlSetData($Combo[8],$SKILL[$nick][5])
						GUICtrlSetData($Combo[9],$SKILL[$nick][6])
				EndIf
				;~ trao doi
					GUICtrlSetData($Button2,$nhan[$nick])
					GUICtrlSetData($Button4,$nhan[$nick])
					If $nhan[$nick]<>$start Then
					disble()
					Else
					enable()
				EndIf
			EndIf
			EndIf
EndFunc
Func text()
Local $string[5]
$string[0]="Số 1 bên trái của bàn phím là ẩn, hiện của auto còn số 2 là check wai"
$string[1] = "Lưu ý: muốn checkwai đừng nên bấm tự đánh"
$string[2]="Muốn tự đánh thì phải chọn kill cơ bản nhất của môn phái"
$string[3]="Mở hết ac mới mở auto"
$string[4]=$string[0]&@CRLF&$string[1]&@CRLF&$string[2]&@CRLF&$string[3]
$Label23 = GUICtrlCreateLabel($string[4], 24, 200, 202, 169)
EndFunc
;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;
Func title()
Local $i=0,$list,$j,$E[2],$c=0,$q=0,$temp
If WinExists("[Class:TianLongBaBu WndClass]") Then
	$newtitle=WinGetTitle("[Class:TianLongBaBu WndClass]")
$title = WinList("[Class:TianLongBaBu WndClass]")
$list = ProcessList()
	for $j = 1 to $list[0][0]
		If $list[$j][0] = _ProcessGetName(WinGetProcess($title[1][0])) Then
			$MEMID = _MEMORYOPEN ($list[$j][1])
			If StringRight($title[1][0],3)<>"HCC" Then
				$E[$c]=Random(0,100,1)&"HCC"
				WinSetTitle($title[$c+1][0],"",$E[$c])
			EndIf
			If WinGetProcess($E[$c])<>$list[$j][1] Then
				$q=$q+1
			EndIf
			If $q=2 Then
				$temp=$E[0]
				$E[0]=$E[1]
				$E[1]=$temp
				WinSetTitle($E[1],"",$E[0])
				WinSetTitle($E[0],"",$E[1])
				$nho[1]=$E[0]
				$nho[3]=$E[1]
			EndIf
			$nho[$i+1]=$E[$c]
			$nho[$i]=$MEMID
			$c=$c+1
			$i=$i+2
		EndIf
	Next
;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;
;~ 	inmang()
	Local $a,$r=true,$cap[3],$ten[3]
	$j=0
	For $i=0 to $title[0][0]-1 Step 1


;~ 	$ten[$i] = _MemoryRead(0x123,$nho[$j],"char[25]")
;~ 	$cap[$i] = _MemoryRead(0x37274,$nho[$j])
;~ 	$cap[$i] = _MemoryRead($cap[$i]+$address_lever[1],$nho[$j])
	GUICtrlCreateListViewItem($i+1&"   . "&" khong tim thay "&"|     "&$cap[$i],$List1)
	$j=$j+2
	Next
	$docmang=True
EndIf
EndFunc
;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;
Func _shutdown()
	GUICtrlSetData($Button1,"Thực hiện")
	$shutdown = True
EndFunc
;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;
Func enable()
	Local $i
	GUICtrlSetState($Input[0],$GUI_ENABLE)
	GUICtrlSetState($Input[1],$GUI_ENABLE)
	GUICtrlSetState($Input[2],$GUI_ENABLE)
	GUICtrlSetState($Checkbox2,$GUI_ENABLE)
	For $i=1 to 11 Step 1
	GUICtrlSetState($Combo[$i],$GUI_ENABLE)
	Next
EndFunc
;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;
Func disble()
	GUICtrlSetState($Input[0],$GUI_DISABLE)
	GUICtrlSetState($Input[1],$GUI_DISABLE)
	GUICtrlSetState($Input[2],$GUI_DISABLE)
	GUICtrlSetState($Checkbox2,$GUI_DISABLE)
	For $i=0 to 11 Step 1
	GUICtrlSetState($Combo[$i],$GUI_DISABLE)
	Next
EndFunc
;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;
Func show()
	If $cuong Then
		$cuong=0
		GUISetState(@SW_HIDE)
	Else
		$cuong=1
	GUISetState(@SW_SHOW)
	EndIf
EndFunc
;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;
Func an()
	Local $temp
	If $nick =  1 Then
		$temp=1
	Else
		$temp=3
	EndIf
	if $nho[$temp]=@error Then
		Return 0
	EndIf
	If GUICtrlRead($Checkbox1)=$GUI_CHECKED Then
		$ngu[$nick]=1
		WinSetState($nho[$temp],"",@SW_HIDE)
	Else
		$ngu[$nick]=4
		WinSetState($nho[$temp],"",@SW_SHOW)
	EndIf
EndFunc
;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;

Func ram_dadung()
Local $ram
$ram=MemGetStats()
If $logic Then
$logic=not $logic
GUICtrlSetData($Label10,$ram[1])
EndIf
GUICtrlsetdataLabelProgress($Progress_ram,$ram[1]-$ram[2],$ram[1])
EndFunc
;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;
;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;
Func capnhapgame()
	Local $i
	If $logic Then
		For $i=0 to 2 step 1
			$tudanh[$i]=GUICtrlRead($Checkbox2)
			$HP_nv[$i] = GUICtrlRead($Input[0])
			$MP_nv[$i] = GUICtrlRead($Input[1])
			$HP_pet[$i] = GUICtrlRead($Input[2])
			$phim_HP_nv[$i] = GUICtrlRead($Combo[1])
			$phim_MP_nv[$i] = GUICtrlRead($Combo[2])
			$phim_HP_pet[$i] = GUICtrlRead($Combo[10])
			$SKILL[$i][0] = GUICtrlRead($Combo[3])
			$SKILL[$i][1] = GUICtrlRead($Combo[4])
			$SKILL[$i][2] = GUICtrlRead($Combo[5])
			$SKILL[$i][3] = GUICtrlRead($Combo[6])
			$SKILL[$i][4] = GUICtrlRead($Combo[7])
			$SKILL[$i][5] = GUICtrlRead($Combo[8])
			$SKILL[$i][6] = GUICtrlRead($Combo[9])
			$number_pet[$i] = GUICtrlRead($Combo[11])
		Next
	Else
		If $nick=0 Then
		MsgBox(64,"HCC"," xin chon nhan vat")
		If $title[0][0]=2 Then
		WinSetTitle($nho[1],"",$newtitle)
		WinSetTitle($nho[3],"",$newtitle)
		_MemoryClose($nho[0])
		_MemoryClose($nho[2])
		Else
		WinSetTitle($nho[1],"",$newtitle)
		_MemoryClose($nho[0])
		EndIf
		Exit
		EndIf
	$tudanh[$nick]=GUICtrlRead($Checkbox2)
	$HP_nv[$nick] = GUICtrlRead($Input[0])
	$MP_nv[$nick] = GUICtrlRead($Input[1])
	$HP_pet[$nick] = GUICtrlRead($Input[2])
	$phim_HP_nv[$nick] = GUICtrlRead($Combo[1])
	$phim_MP_nv[$nick] = GUICtrlRead($Combo[2])
	$phim_HP_pet[$nick] = GUICtrlRead($Combo[10])
	$SKILL[$nick][0] = GUICtrlRead($Combo[3])
	$SKILL[$nick][1] = GUICtrlRead($Combo[4])
	$SKILL[$nick][2] = GUICtrlRead($Combo[5])
	$SKILL[$nick][3] = GUICtrlRead($Combo[6])
	$SKILL[$nick][4] = GUICtrlRead($Combo[7])
	$SKILL[$nick][5] = GUICtrlRead($Combo[8])
	$SKILL[$nick][6] = GUICtrlRead($Combo[9])
	$number_pet[$nick] = GUICtrlRead($Combo[11])
	EndIf
EndFunc
;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;
Func thaydoi()
	If GUICtrlRead($Button2)=$start  or GUICtrlRead($Button2)=$start Then
		GUICtrlSetData($Button2,$stop)
		GUICtrlSetData($Button4,$stop)
		$nhan[$nick]=$stop
		capnhapgame()
		disble()
		start()
	Else
		GUICtrlSetData($Button2,$start)
		GUICtrlSetData($Button4,$start)
		$nhan[$nick]=$start
		enable()
		stop()
	EndIf
EndFunc
;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;
Func KILL($thao,$nick)
	If $SKILL[$nick][0] <>"None" Then ControlSend($nho[$thao+1],"","","{"&$SKILL[$nick][0]&"}")
		Sleep(500)
	If $SKILL[$nick][1] <>"None" Then ControlSend($nho[$thao+1],"","","{"&$SKILL[$nick][1]&"}")
		Sleep(500)
	If $SKILL[$nick][2] <>"None" Then ControlSend($nho[$thao+1],"","","{"&$SKILL[$nick][2]&"}")
		Sleep(500)
	If $SKILL[$nick][3] <>"None" Then ControlSend($nho[$thao+1],"","","{"&$SKILL[$nick][3]&"}")
		Sleep(500)
	If $SKILL[$nick][4] <>"None" Then ControlSend($nho[$thao+1],"","","{"&$SKILL[$nick][4]&"}")
		Sleep(500)
	If $SKILL[$nick][5] <>"None" Then ControlSend($nho[$thao+1],"","","{"&$SKILL[$nick][5]&"}")
		Sleep(500)
	If $SKILL[$nick][6] <>"None" Then ControlSend($nho[$thao+1],"","","{"&$SKILL[$nick][6]&"}")
EndFunc


Func capnhap($thao)
Local $base_char
$base_char= 0x648EE8
;~ MsgBox(0,"",$thao)
$base_char = _MemoryRead($base_char,$nho[$thao])
$base_char = _MemoryRead($base_char + 0xc,$nho[$thao])
$base_char = _MemoryRead($base_char + 0x154,$nho[$thao])
$base_char = _MemoryRead($base_char + 0x4,$nho[$thao])
Return $base_char
EndFunc
;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;
Func nhanvat($thao,$nick)
;~ HP_HIENTAI
Local $capnhap = capnhap($thao)
$HP[$nick] = _MemoryRead($capnhap + 0x8e8,$nho[$thao])
;~ HP_MAX
$HP_MAX[$nick] = _MemoryRead($capnhap + 0x950,$nho[$thao])
;MP_HIENTAI
$MP[$nick]=_MEMORYREAD($capnhap + 0x8ec,$nho[$thao])
;MP_MAX;
$MP_MAX[$nick]=_MEMORYREAD($capnhap + 0x954,$nho[$thao])
;HP_PET
$base_pet[$nick]=HP_PET($thao,$nick)
EndFunc
;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;
Func HP_PET($thao,$nick)
Local $base_pe
if $number_pet[$nick] = 1 Then
$base_pe = _MemoryRead(0x647C38,$nho[$thao])
$base_pe = _MemoryRead($base_pe + 0x47e9c,$nho[$thao])
$base_pe = _MemoryRead($base_pe + 0x3c,$nho[$thao])
;~ $base_pet_max[$nick] = _MemoryRead(0x3FE4FD9,$nho[$thao])
;~ GUICtrlSetData($Label16,$base_pet_max[$nick])
ElseIf $number_pet[$nick] = 2 Then
;~ $base_pe = _MemoryRead(0x2A39520,$nho[$thao])
$base_pe = _MemoryRead(0x2A39520 + 0x3c ,$nho[$thao])
;~ $base_pet_max[$nick] = _MemoryRead(0x3FE4FD9,$nho[$thao])
ElseIf $number_pet[$nick] = 3 Then
$base_pe = _MemoryRead(0x2a39610 +0x3c,$nho[$thao])
;~ $base_pet_max[$nick] = _MemoryRead(0x32E00A,$nho[$thao])
ElseIf $number_pet[$nick] = 4 Then
$base_pe = _MemoryRead(0x2A39700 + 0x3c,$nho[$thao])
;~ $base_pet_max[$nick] = _MemoryRead(0x338DA2,$nho[$thao])
EndIf
;~ 	GUICtrlSetData($Label16,$base_pe)
Return $base_pe
EndFunc
;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;
Func hoiphuc($thao,$nick)
	If $HP[$nick] < $HP_nv[$nick] Then
		If $phim_HP_nv[$nick]<>"None" then
		ControlSend($nho[$thao+1],"","","{"&$phim_HP_nv[$nick]&"}")
		EndIf
	EndIf
	If $number_pet[$nick] <>0 Then
		if $base_pet[$nick] < $HP_pet[$nick] Then
			If $phim_HP_pet[$nick]<>"None" then
				ControlSend($nho[$thao+1],"","","{"&$phim_HP_pet[$nick]&"}")
			EndIf
		EndIf
	EndIf
	If $MP[$nick] < $MP_nv[$nick] Then
		If $phim_MP_nv[$nick] <>"None" Then
		ControlSend($nho[$thao+1],"","","{"&$phim_MP_nv[$nick]&"}")
		EndIf
	EndIf
EndFunc
;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;
Func Thongtin_nv()
;~ 	GUICtrlSetData($Label16,$base_pet[$nick])
;~ 	$nic=Number(StringLeft(GUICtrlRead(GUICtrlRead($List1)),1))
	GUICtrlsetdataLabelProgress($Progress_hp[1][0],$HP[$nick],$HP_MAX[$nick])
	GUICtrlsetdataLabelProgress($Progress_mp[1][0],$MP[$nick],$MP_MAX[$nick])
	GUICtrlsetdataLabelProgress($Progress_hp[0][1],$base_pet[$nick],$base_pet_max[$nick])
EndFunc
Func checkwai()
	Local $titl
	$titl = WinList("[Class:TianLongBaBu WndClass]")
	ControlSend($titl[1][0],"","","^{TAB}")
EndFunc

;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;
;~ COI CÓ ĐƯỢC TARWAI HAY KHÔNG
func ack_quai($thao,$nick)
Local $ack[3]
$ack[$nick] = _MemoryRead(0x648EE8,$nho[$thao])
$ack[$nick] = _MemoryRead($ack[$nick] + 0xc,$nho[$thao])
$ack[$nick] = _MemoryRead($ack[$nick] + 0x278,$nho[$thao])
If $tudanh[$nick]=$GUI_CHECKED Then
	If not $ack[$nick] Then
		If WinGetTitle("[active]")<>"AUTO TLBB (HCC)" Then
		If $count =0 Then
		$count=$count+1
		ControlSend($nho[$thao+1],"","","^")
		Sleep(100)
		ControlSend($nho[$thao+1],"","","{TAB}")
		Sleep(77)
		KILL($thao,$nick)
		Return 0
		Else
		KILL($thao,$nick)
		EndIf
		EndIf
		If $count=3 Then
			$count=0
			Return 0
		EndIf
		$count=$count+1
	EndIf

EndIf
EndFunc
;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;
Func start()
	Local $dem=0
	$vip =1
For $nick=1 to 2 Step 1
	if $nhan[$nick]=$stop Then

		$dem=$dem+1
		If $nick = 1 Then
			$thao=0
		Else
			$thao=2
		EndIf
		nhanvat($thao,$nick)
		hoiphuc($thao,$nick)
		ack_quai($thao,$nick)
	EndIf
Next
If $dem=0 Then
	$vip=2
EndIf
EndFunc
;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;
Func stop()
	$nhan[$nick]=$start
	$vip = 1
EndFunc
;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;
Func _exit()
If $title[0][0]=2 Then
	WinSetTitle($nho[1],"",$newtitle)
	WinSetTitle($nho[3],"",$newtitle)
	_MemoryClose($nho[0])
	_MemoryClose($nho[2])
Else
	WinSetTitle($nho[1],"",$newtitle)
	_MemoryClose($nho[0])
EndIf
Exit
EndFunc
;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;
While 1
Local $i
Sleep(77)
;~ $Label15

tatmay()
;~ MsgBox(0,"",$nick)
If $nick = 1 Then
		$thao=0
		nhanvat($thao,$nick)
		Thongtin_nv()
Elseif $nick=2 then

		$thao=2
		nhanvat($thao,$nick)
		Thongtin_nv()
EndIf
	Switch $vip
		Case 1
			start()
		Case 2
			Sleep(77)
	EndSwitch
WEnd
;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;

Func tatmay()
If $shutdown Then
Global $read
$read=GUICtrlRead($Input1)
$read=$read*60*1000
$yeucau=GUICtrlRead($Combo1)
$shutdown=not $shutdown
Else
	Return 0
EndIf
	If not $shutdown Then
		$read=$read-1000
		If $read =0 Then
			if $yeucau = "shutdown" then
				Shutdown(1)
			Elseif $yeucau ="Logoff" then
				Shutdown(0)
			ElseIf $yeucau = "Standby" Then
				Shutdown(32)
			EndIf

		EndIf
	EndIf
EndFunc

;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;
#region _Memory

Func _MemoryClose($ah_Handle)

    If Not IsArray($ah_Handle) Then
        SetError(1)
        Return 0
    EndIf

    DllCall($ah_Handle[0], 'int', 'CloseHandle', 'int', $ah_Handle[1])
    If Not @Error Then
        DllClose($ah_Handle[0])
        Return 1
    Else
        DllClose($ah_Handle[0])
        SetError(2)
        Return 0
    EndIf

EndFunc

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
Func GUICtrlCreateLabelProgress($text,$left,$top,$width,$height,$style="",$Color="",$color1= -1)
	$LabelProgress[0][0] += 1
	ReDim $LabelProgress[$LabelProgress[0][0]+1][6]
	If $color1 >= 0 Then
		GUICtrlCreateLabel("",$left,$top,$width,$height,$style)
		GUICtrlSetBkColor(-1,$color1)
	EndIf
	$LabelProgress[$LabelProgress[0][0]][0]= GUICtrlCreateLabel("",$left,$top,$width,$height,$style)
	$LabelProgress[$LabelProgress[0][0]][1] = GUICtrlCreateLabel($text,$left,$top,$width,$height,$style)
	$LabelProgress[$LabelProgress[0][0]][2] = $left
	$LabelProgress[$LabelProgress[0][0]][3] = $top
	$LabelProgress[$LabelProgress[0][0]][4] = $width
	$LabelProgress[$LabelProgress[0][0]][5] = $height
	GUICtrlSetBkColor($LabelProgress[$LabelProgress[0][0]][0], $Color)
	Return $LabelProgress[0][0]
EndFunc
Func GUICtrlsetdataLabelProgress($contro,$min="",$max="")
	Local $x=1,$y=1
	If $min >= $max Then
		$x = $LabelProgress[$contro][4]
	Else
		$x = Round($LabelProgress[$contro][4]*$min/$max)
		If $x = 0 Then $x = 1
	EndIf
	GUICtrlSetPos($LabelProgress[$contro][0],$LabelProgress[$contro][2],$LabelProgress[$contro][3],$x, $LabelProgress[$contro][5])
	GUICtrlSetData($LabelProgress[$contro][1],$min&"/"&$max)
EndFunc
#endregion _Memory
