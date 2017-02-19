#include <ImageSearch.au3>
$x = 0
$y = 0

#Region Einfache Suche
#cs
$res = _imagesearch('voll.bmp',1,$x,$y,100)
If $res = 1 Then
	MouseMove($x,$y,100)
Else
	MsgBox(0,'Info','Nichts gefunden....')
EndIf
#ce
#EndRegion


#Region Suche in Bereich
#cs
$res = _imagesearcharea('voll.bmp',1,0,0,200,200,$x,$y,100)
If $res = 1 Then
	MouseMove($x,$y,100)
Else
	MsgBox(0,'Info','Nichts gefunden....')
EndIf
#ce
#EndRegion

#Region Suche mit Wartefunktion
#cs
$res = _WaitForImageSearch('leer.bmp',10,1,$x,$y,100)
If $res = 1 Then
	MouseMove($x,$y,100)
Else
	MsgBox(0,'Info','Nichts gefunden....')
EndIf
#ce
#EndRegion

#Region Suche nach mehreren Bildern mit Wartefunktion
Dim $myPics[10]
$myPics[0] = 2
$myPics[1] = 'voll.bmp'
$myPics[2] = 'leer.bmp'

$res = _WaitForImagesSearch($myPics,5,1,$x,$y,100)
Switch $res
	Case 0
		MsgBox(0,'','Nix gefunden')
	Case 1
		MouseMove($x,$y,100)
		MsgBox(0,'','Ist voll')
	Case 2
		MouseMove($x,$y,100)
		MsgBox(0,'','Ist leer')
EndSwitch
