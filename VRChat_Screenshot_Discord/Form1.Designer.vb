﻿<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Form1
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Form1))
        Label1 = New Label()
        Label2 = New Label()
        ListBox1 = New ListBox()
        TextBox1 = New TextBox()
        TextBox2 = New TextBox()
        Button1 = New Button()
        UpdateLinkButton = New Button()
        SuspendLayout()
        ' 
        ' Label1
        ' 
        Label1.AutoSize = True
        Label1.Location = New Point(12, 14)
        Label1.Name = "Label1"
        Label1.Size = New Size(122, 15)
        Label1.TabIndex = 0
        Label1.Text = "VRChat画像フォルダパス"
        ' 
        ' Label2
        ' 
        Label2.AutoSize = True
        Label2.Location = New Point(12, 58)
        Label2.Name = "Label2"
        Label2.Size = New Size(82, 15)
        Label2.TabIndex = 1
        Label2.Text = "Webhook URL"
        ' 
        ' ListBox1
        ' 
        ListBox1.FormattingEnabled = True
        ListBox1.HorizontalScrollbar = True
        ListBox1.ItemHeight = 15
        ListBox1.Location = New Point(12, 102)
        ListBox1.Name = "ListBox1"
        ListBox1.Size = New Size(556, 154)
        ListBox1.TabIndex = 2
        ' 
        ' TextBox1
        ' 
        TextBox1.Location = New Point(12, 32)
        TextBox1.Name = "TextBox1"
        TextBox1.Size = New Size(543, 23)
        TextBox1.TabIndex = 3
        ' 
        ' TextBox2
        ' 
        TextBox2.Location = New Point(12, 76)
        TextBox2.Name = "TextBox2"
        TextBox2.Size = New Size(543, 23)
        TextBox2.TabIndex = 4
        ' 
        ' Button1
        ' 
        Button1.Location = New Point(574, 76)
        Button1.Name = "Button1"
        Button1.Size = New Size(75, 24)
        Button1.TabIndex = 5
        Button1.Text = "監視開始"
        Button1.UseVisualStyleBackColor = True
        ' 
        ' UpdateLinkButton
        ' 
        UpdateLinkButton.Location = New Point(527, 3)
        UpdateLinkButton.Name = "UpdateLinkButton"
        UpdateLinkButton.Size = New Size(122, 24)
        UpdateLinkButton.TabIndex = 6
        UpdateLinkButton.Text = "更新リンク"
        UpdateLinkButton.UseVisualStyleBackColor = True
        ' 
        ' Form1
        ' 
        AutoScaleDimensions = New SizeF(7F, 15F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(649, 263)
        Controls.Add(UpdateLinkButton)
        Controls.Add(Button1)
        Controls.Add(TextBox2)
        Controls.Add(TextBox1)
        Controls.Add(ListBox1)
        Controls.Add(Label2)
        Controls.Add(Label1)
        Icon = CType(resources.GetObject("$this.Icon"), Icon)
        Name = "Form1"
        Text = "VRChat写真をDiscordにアップ"
        ResumeLayout(False)
        PerformLayout()
    End Sub

    Friend WithEvents Label1 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents ListBox1 As ListBox
    Friend WithEvents TextBox1 As TextBox

    Private Sub TextBox1_TextChanged(sender As Object, e As EventArgs) Handles TextBox1.TextChanged

    End Sub

    Friend WithEvents TextBox2 As TextBox
    Friend WithEvents Button1 As Button
    Friend WithEvents UpdateLinkButton As Button
End Class
