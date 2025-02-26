Imports System.ComponentModel
Imports System.Configuration.Install
Imports System.Globalization
Imports System.Windows.Forms

<RunInstaller(True)>
Public Class Installer1
    Inherits Installer

    Public Overrides Sub Install(stateSaver As IDictionary)
        MyBase.Install(stateSaver)

        ' OSの言語を取得
        Dim currentCulture As CultureInfo = CultureInfo.CurrentUICulture
        Dim message As String
        Dim title As String

        ' 言語に基づいてメッセージを設定
        Select Case currentCulture.TwoLetterISOLanguageName
            Case "ja"
                message = "スタートアップに追加しますか？"
                title = "スタートアップの設定"
            Case "ko"
                message = "시작 프로그램에 추가하시겠습니까?"
                title = "시작 프로그램 설정"
            Case "zh"
                message = "是否添加到启动项？"
                title = "启动项设置"
            Case Else
                message = "Do you want to add to startup?"
                title = "Startup Settings"
        End Select

        ' メッセージボックスを表示
        Dim result As DialogResult = MessageBox.Show(message, title, MessageBoxButtons.YesNo)
        If result = DialogResult.Yes Then
            AddToStartup()
        End If
    End Sub

    Private Sub AddToStartup()
        Dim startupPath As String = Environment.GetFolderPath(Environment.SpecialFolder.Startup)
        Dim shortcutPath As String = IO.Path.Combine(startupPath, "VRChat_Screenshot_Discord.lnk")
        Dim targetPath As String = IO.Path.Combine(Context.Parameters("targetdir"), "VRChat_Screenshot_Discord.exe")

        Dim shell As Object = CreateObject("WScript.Shell")
        Dim shortcut As Object = shell.CreateShortcut(shortcutPath)
        shortcut.TargetPath = targetPath
        shortcut.Save()
    End Sub
End Class
