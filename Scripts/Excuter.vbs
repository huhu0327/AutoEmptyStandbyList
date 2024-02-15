If WScript.Arguments.Count = 0 Then
  WScript.Quit 1
End If

Set WshShell = CreateObject("WScript.Shell")
WshShell.Run chr(34) & WScript.Arguments(0) & Chr(34), 0
Set WshShell = Nothing