; �ýű�ʹ�� HM VNISEdit �ű��༭���򵼲���

; ��װ�����ʼ���峣��
!define PRODUCT_NAME "������"
!define PRODUCT_VERSION "1.0"
!define PRODUCT_UNINST_KEY "Software\Microsoft\Windows\CurrentVersion\Uninstall\${PRODUCT_NAME}"
!define PRODUCT_UNINST_ROOT_KEY "HKLM"

SetCompressor lzma

; ------ MUI �ִ����涨�� (1.67 �汾���ϼ���) ------
!include "MUI.nsh"

; MUI Ԥ���峣��
!define MUI_ABORTWARNING
!define MUI_ICON "${NSISDIR}\Contrib\Graphics\Icons\modern-install.ico"
!define MUI_UNICON "${NSISDIR}\Contrib\Graphics\Icons\modern-uninstall.ico"

; ��ӭҳ��
!insertmacro MUI_PAGE_WELCOME
; ���Э��ҳ��
!insertmacro MUI_PAGE_LICENSE "I:\NewC#demo\New2\ArchiElementDemo\ArchiElementDemo\bin\x64\Debug\help.txt"
; ��װĿ¼ѡ��ҳ��
!insertmacro MUI_PAGE_DIRECTORY
; ��װ����ҳ��
!insertmacro MUI_PAGE_INSTFILES
; ��װ���ҳ��
!insertmacro MUI_PAGE_FINISH

; ��װж�ع���ҳ��
!insertmacro MUI_UNPAGE_INSTFILES

; ��װ�����������������
!insertmacro MUI_LANGUAGE "SimpChinese"

; ��װԤ�ͷ��ļ�
!insertmacro MUI_RESERVEFILE_INSTALLOPTIONS
; ------ MUI �ִ����涨����� ------

Name "${PRODUCT_NAME} ${PRODUCT_VERSION}"
OutFile "Setup.exe"
InstallDir "$PROGRAMFILES64\������"
ShowInstDetails show
ShowUnInstDetails show

Section "MainSection" SEC01
  SetOutPath "C:\Users\Administrator\AppData\Roaming\Autodesk\Revit\Addins\2018"
  SetOverwrite ifnewer
  File "window.png"
  File "window.ico"
  File "wall.png"
  File "wall.ico"
  File "room.png"
  File "room.ico"
  File "qiaojia.png"
  File "qiaojia.ico"
  File "pipe.png"
  File "pipe.ico"
  File "MySql.Data.dll"
  File "floor.png"
  File "floor.ico"
  File "duct.png"
  File "duct.ico"
  File "door.png"
  File "door.ico"
  File "column.png"
  File "column.ico"
  File "beam.png"
  File "beam.ico"
  File "ArchiElementDemo.pdb"
  File "ArchiElementDemo.dll"
  File "ArchiElementDemo.addin"
SectionEnd

Section -AdditionalIcons
  SetOutPath $INSTDIR
  CreateDirectory "$SMPROGRAMS\������"
  CreateShortCut "$SMPROGRAMS\������\Uninstall.lnk" "$INSTDIR\uninst.exe"
SectionEnd

Section -Post
  WriteUninstaller "$INSTDIR\uninst.exe"
  WriteRegStr ${PRODUCT_UNINST_ROOT_KEY} "${PRODUCT_UNINST_KEY}" "DisplayName" "$(^Name)"
  WriteRegStr ${PRODUCT_UNINST_ROOT_KEY} "${PRODUCT_UNINST_KEY}" "UninstallString" "$INSTDIR\uninst.exe"
  WriteRegStr ${PRODUCT_UNINST_ROOT_KEY} "${PRODUCT_UNINST_KEY}" "DisplayVersion" "${PRODUCT_VERSION}"
SectionEnd

/******************************
 *  �����ǰ�װ�����ж�ز���  *
 ******************************/

Section Uninstall
  Delete "$INSTDIR\uninst.exe"
  Delete "C:\Users\Administrator\AppData\Roaming\Autodesk\Revit\Addins\2018\ArchiElementDemo.addin"
  Delete "C:\Users\Administrator\AppData\Roaming\Autodesk\Revit\Addins\2018\ArchiElementDemo.dll"
  Delete "C:\Users\Administrator\AppData\Roaming\Autodesk\Revit\Addins\2018\ArchiElementDemo.pdb"
  Delete "C:\Users\Administrator\AppData\Roaming\Autodesk\Revit\Addins\2018\beam.ico"
  Delete "C:\Users\Administrator\AppData\Roaming\Autodesk\Revit\Addins\2018\beam.png"
  Delete "C:\Users\Administrator\AppData\Roaming\Autodesk\Revit\Addins\2018\column.ico"
  Delete "C:\Users\Administrator\AppData\Roaming\Autodesk\Revit\Addins\2018\column.png"
  Delete "C:\Users\Administrator\AppData\Roaming\Autodesk\Revit\Addins\2018\door.ico"
  Delete "C:\Users\Administrator\AppData\Roaming\Autodesk\Revit\Addins\2018\door.png"
  Delete "C:\Users\Administrator\AppData\Roaming\Autodesk\Revit\Addins\2018\duct.ico"
  Delete "C:\Users\Administrator\AppData\Roaming\Autodesk\Revit\Addins\2018\duct.png"
  Delete "C:\Users\Administrator\AppData\Roaming\Autodesk\Revit\Addins\2018\floor.ico"
  Delete "C:\Users\Administrator\AppData\Roaming\Autodesk\Revit\Addins\2018\floor.png"
  Delete "C:\Users\Administrator\AppData\Roaming\Autodesk\Revit\Addins\2018\MySql.Data.dll"
  Delete "C:\Users\Administrator\AppData\Roaming\Autodesk\Revit\Addins\2018\pipe.ico"
  Delete "C:\Users\Administrator\AppData\Roaming\Autodesk\Revit\Addins\2018\pipe.png"
  Delete "C:\Users\Administrator\AppData\Roaming\Autodesk\Revit\Addins\2018\qiaojia.ico"
  Delete "C:\Users\Administrator\AppData\Roaming\Autodesk\Revit\Addins\2018\qiaojia.png"
  Delete "C:\Users\Administrator\AppData\Roaming\Autodesk\Revit\Addins\2018\room.ico"
  Delete "C:\Users\Administrator\AppData\Roaming\Autodesk\Revit\Addins\2018\room.png"
  Delete "C:\Users\Administrator\AppData\Roaming\Autodesk\Revit\Addins\2018\wall.ico"
  Delete "C:\Users\Administrator\AppData\Roaming\Autodesk\Revit\Addins\2018\wall.png"
  Delete "C:\Users\Administrator\AppData\Roaming\Autodesk\Revit\Addins\2018\window.ico"
  Delete "C:\Users\Administrator\AppData\Roaming\Autodesk\Revit\Addins\2018\window.png"

  Delete "$SMPROGRAMS\������\Uninstall.lnk"

  RMDir "C:\Users\Administrator\AppData\Roaming\Autodesk\Revit\Addins\2018"
  RMDir "$SMPROGRAMS\������"

  RMDir "$INSTDIR"

  DeleteRegKey ${PRODUCT_UNINST_ROOT_KEY} "${PRODUCT_UNINST_KEY}"
  SetAutoClose true
SectionEnd

#-- ���� NSIS �ű��༭�������� Function ���α�������� Section ����֮���д���Ա��ⰲװ�������δ��Ԥ֪�����⡣--#

Function un.onInit
  MessageBox MB_ICONQUESTION|MB_YESNO|MB_DEFBUTTON2 "��ȷʵҪ��ȫ�Ƴ� $(^Name) ���������е������" IDYES +2
  Abort
FunctionEnd

Function un.onUninstSuccess
  HideWindow
  MessageBox MB_ICONINFORMATION|MB_OK "$(^Name) �ѳɹ��ش����ļ�����Ƴ���"
FunctionEnd


Function AdvReplaceInFile
Exch $0 ;file to replace in
Exch
Exch $1 ;number to replace after
Exch
Exch 2
Exch $2 ;replace and onwards
Exch 2
Exch 3
Exch $3 ;replace with
Exch 3
Exch 4
Exch $4 ;to replace
Exch 4
Push $5 ;minus count
Push $6 ;universal
Push $7 ;end string
Push $8 ;left string
Push $9 ;right string
Push $R0 ;file1
Push $R1 ;file2
Push $R2 ;read
Push $R3 ;universal
Push $R4 ;count (onwards)
Push $R5 ;count (after)
Push $R6 ;temp file name

  GetTempFileName $R6
  FileOpen $R1 $0 r ;file to search in
  FileOpen $R0 $R6 w ;temp file
   StrLen $R3 $4
   StrCpy $R4 -1
   StrCpy $R5 -1

loop_read:
 ClearErrors
 FileRead $R1 $R2 ;read line
 IfErrors exit

   StrCpy $5 0
   StrCpy $7 $R2

loop_filter:
   IntOp $5 $5 - 1
   StrCpy $6 $7 $R3 $5 ;search
   StrCmp $6 "" file_write1
   StrCmp $6 $4 0 loop_filter

StrCpy $8 $7 $5 ;left part
IntOp $6 $5 + $R3
IntCmp $6 0 is0 not0
is0:
StrCpy $9 ""
Goto done
not0:
StrCpy $9 $7 "" $6 ;right part
done:
StrCpy $7 $8$3$9 ;re-join

IntOp $R4 $R4 + 1
StrCmp $2 all loop_filter
StrCmp $R4 $2 0 file_write2
IntOp $R4 $R4 - 1

IntOp $R5 $R5 + 1
StrCmp $1 all loop_filter
StrCmp $R5 $1 0 file_write1
IntOp $R5 $R5 - 1
Goto file_write2

file_write1:
 FileWrite $R0 $7 ;write modified line
Goto loop_read

file_write2:
 FileWrite $R0 $R2 ;write unmodified line
Goto loop_read

exit:
  FileClose $R0
  FileClose $R1

   SetDetailsPrint none
  Delete $0
  Rename $R6 $0
  Delete $R6
   SetDetailsPrint both

Pop $R6
Pop $R5
Pop $R4
Pop $R3
Pop $R2
Pop $R1
Pop $R0
Pop $9
Pop $8
Pop $7
Pop $6
Pop $5
Pop $0
Pop $1
Pop $2
Pop $3
Pop $4
FunctionEnd
