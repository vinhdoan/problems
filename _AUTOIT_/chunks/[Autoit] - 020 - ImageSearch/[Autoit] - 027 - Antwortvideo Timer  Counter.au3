#cs ----------------------------------------------------------------------------
	[Autoit] - 027 - Zuschauer Anfrage Timer / Counter
	
	AutoIt Version: 3.3.4.0
	Author:         Jnes Kröger
	
	Script Function:
	Beispielskript
	
#ce ----------------------------------------------------------------------------

; 1. Die Inputbox
; Wir nehmen hier eine einfache Inputbox und speicher den In halt in einer
; Variablen mit dem namen $zeit.

$zeit = InputBox("Timer-Counter", "Bitte gib die Zeit in Sekunkend ein:", _
		"10", '', -1, 140)

; 2. Prüfen ob es eine Zahl war, bzw. ob etwas eingetragen wurde
; Hier nehmen wir den Wert der in der Variablen $zahl steht und teilen
; diesen durch 1, kommt 0 raus, ist etwas Faul und wir machen nicht weiter.

If $zeit / 1 = 0 Then
	MsgBox(16, "Timer-Counter", "Es wurde kein Wert oder ein falscher Wert" & _
			" eingegeben. Das Programm wird beendet.")
	Exit
EndIf

; 3. Die GUI
; Hier habe ich nun mal einfach was mit Koda (ALT+M), nix besonderes, einfach mal mit
; spielen.
; Was ich hinzugefügt habe, ist beschrieben.

#include <GUIConstantsEx.au3>
#include <StaticConstants.au3>
#include <WindowsConstants.au3>

$Form1 = GUICreate("Timer - Counter", 288, 63)
GUISetBkColor(0xA6CAF0)
$lblZeit = GUICtrlCreateLabel("00", 0, 0, 58, 60)
GUICtrlSetFont(-1, 30, 400, 0, "Arial Black")
GUICtrlSetColor(-1, 0x0000FF)
$lblBezeichnung = GUICtrlCreateLabel("Sekunden", 64, 0, 222, 60)
GUICtrlSetFont(-1, 30, 400, 0, "Arial Black")
GUISetState(@SW_SHOW)

While 1
	$nMsg = GUIGetMsg()
	Switch $nMsg
		Case $GUI_EVENT_CLOSE
			Exit
	EndSwitch

	; Von unserer Zahl 1 abziehen und wenn wir bei kleiner 0 sind, das Skript beenden
	; bzw. eine Andere Aktion durchführen.
	$zeit = $zeit - 1
	If $zeit < 0 Then
		; Hier mache ich nun ein Exit, man könnte natürlich nun auch noch eine
		; andere Aktion durchführen.
		Exit
	EndIf

	; Unsere neue Zahl in das Label schreiben, was die verbleibenden Sekunden anzeigt.
	GUICtrlSetData($lblZeit, $zeit)

	; Pause einfügen, damit wir ungefähr eine Sekunde haben
	Sleep(1000)

WEnd