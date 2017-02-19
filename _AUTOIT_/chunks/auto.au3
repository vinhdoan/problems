HotKeySet("s", "prg")
While 1
	sleep(100)
WEnd
Func prg()
	$a=InputBox("nothing","nothing")
	while $a>0
		Send("{DEL}{DEL}{DEL}{DEL}{DEL}{DOWN}")
		$a=$a-1
	WEnd
	Exit
	EndFunc