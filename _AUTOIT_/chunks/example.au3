Run(@WindowsDir & "\Notepad.exe", "", @SW_SHOWNORMAL) ; Goi notepad tu thu muc goc
WinWait("[CLASS:Notepad]") ;Doi notepad xuat hien
send("Now ")
dim $i
While 1
	ControlSend("[CLASS:Notepad]","Now" , "Edit1", $i) ;Goi thong tin vao khung soan thao
	Sleep (900)
	ControlSend("[CLASS:Notepad]", "Now", "Edit1", "{BACKSPACE}") ;Goi thong tin vao khung soan thao
	Sleep (100)
	$i += 1
WEnd
