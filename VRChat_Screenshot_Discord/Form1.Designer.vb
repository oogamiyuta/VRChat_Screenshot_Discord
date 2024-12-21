<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
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
        ComboBoxLanguage = New ComboBox()
        Label3 = New Label()
        SuspendLayout()
        ' 
        ' Label1
        ' 
        Label1.Anchor = AnchorStyles.Top Or AnchorStyles.Right
        Label1.Location = New Point(206, 2)
        Label1.Name = "Label1"
        Label1.Size = New Size(152, 19)
        Label1.TabIndex = 8
        Label1.Text = "言語/Language/语言/언어"
        Label1.TextAlign = ContentAlignment.MiddleRight
        ' 
        ' Label2
        ' 
        Label2.AutoSize = True
        Label2.Location = New Point(12, 8)
        Label2.Name = "Label2"
        Label2.Size = New Size(122, 15)
        Label2.TabIndex = 7
        Label2.Text = "VRChat画像フォルダパス"
        ' 
        ' ListBox1
        ' 
        ListBox1.FormattingEnabled = True
        ListBox1.ItemHeight = 15
        ListBox1.Location = New Point(12, 100)
        ListBox1.Name = "ListBox1"
        ListBox1.Size = New Size(644, 169)
        ListBox1.TabIndex = 6
        ' 
        ' TextBox1
        ' 
        TextBox1.Location = New Point(12, 27)
        TextBox1.Name = "TextBox1"
        TextBox1.Size = New Size(534, 23)
        TextBox1.TabIndex = 5
        ' 
        ' TextBox2
        ' 
        TextBox2.Location = New Point(12, 73)
        TextBox2.Name = "TextBox2"
        TextBox2.Size = New Size(534, 23)
        TextBox2.TabIndex = 4
        ' 
        ' Button1
        ' 
        Button1.Anchor = AnchorStyles.Top Or AnchorStyles.Right
        Button1.AutoSize = True
        Button1.Location = New Point(552, 75)
        Button1.Name = "Button1"
        Button1.Size = New Size(104, 25)
        Button1.TabIndex = 3
        Button1.Text = "監視開始"
        Button1.UseVisualStyleBackColor = True
        ' 
        ' UpdateLinkButton
        ' 
        UpdateLinkButton.Anchor = AnchorStyles.Top Or AnchorStyles.Right
        UpdateLinkButton.AutoSize = True
        UpdateLinkButton.Location = New Point(554, -1)
        UpdateLinkButton.Name = "UpdateLinkButton"
        UpdateLinkButton.Size = New Size(102, 25)
        UpdateLinkButton.TabIndex = 2
        UpdateLinkButton.Text = "更新をダウンロード"
        UpdateLinkButton.UseVisualStyleBackColor = True
        ' 
        ' ComboBoxLanguage
        ' 
        ComboBoxLanguage.Anchor = AnchorStyles.Top Or AnchorStyles.Right
        ComboBoxLanguage.FormattingEnabled = True
        ComboBoxLanguage.Location = New Point(364, 0)
        ComboBoxLanguage.Name = "ComboBoxLanguage"
        ComboBoxLanguage.Size = New Size(150, 23)
        ComboBoxLanguage.TabIndex = 1
        ' 
        ' Label3
        ' 
        Label3.AutoSize = True
        Label3.Location = New Point(12, 54)
        Label3.Name = "Label3"
        Label3.Size = New Size(82, 15)
        Label3.TabIndex = 0
        Label3.Text = "Webhook URL"
        ' 
        ' Form1
        ' 
        AutoScaleDimensions = New SizeF(7.0F, 15.0F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(668, 276)
        Controls.Add(Label3)
        Controls.Add(ComboBoxLanguage)
        Controls.Add(UpdateLinkButton)
        Controls.Add(Button1)
        Controls.Add(TextBox2)
        Controls.Add(TextBox1)
        Controls.Add(ListBox1)
        Controls.Add(Label2)
        Controls.Add(Label1)
        Icon = CType(resources.GetObject("$this.Icon"), Icon)
        Name = "Form1"
        Text = "VRChat写真をDiscordにアップ "
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
    Friend WithEvents ComboBoxLanguage As ComboBox
    Friend WithEvents Label3 As Label
End Class
