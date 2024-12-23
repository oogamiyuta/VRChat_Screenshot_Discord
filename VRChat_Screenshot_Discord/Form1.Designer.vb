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
        Button1 = New Button()
        UpdateLinkButton = New Button()
        ComboBoxLanguage = New ComboBox()
        TabControl1 = New TabControl()
        TabPage1 = New TabPage()
        TabPage2 = New TabPage()
        DataGridView1 = New DataGridView()
        TabControl1.SuspendLayout()
        TabPage1.SuspendLayout()
        TabPage2.SuspendLayout()
        CType(DataGridView1, ComponentModel.ISupportInitialize).BeginInit()
        SuspendLayout()
        ' 
        ' Label1
        ' 
        Label1.Anchor = AnchorStyles.Top Or AnchorStyles.Right
        Label1.Location = New Point(195, 9)
        Label1.Name = "Label1"
        Label1.Size = New Size(152, 19)
        Label1.TabIndex = 8
        Label1.Text = "言語/Language/语言/언어"
        Label1.TextAlign = ContentAlignment.MiddleRight
        ' 
        ' Label2
        ' 
        Label2.AutoSize = True
        Label2.Location = New Point(8, 42)
        Label2.Name = "Label2"
        Label2.Size = New Size(122, 15)
        Label2.TabIndex = 7
        Label2.Text = "VRChat画像フォルダパス"
        ' 
        ' ListBox1
        ' 
        ListBox1.FormattingEnabled = True
        ListBox1.ItemHeight = 15
        ListBox1.Location = New Point(3, 94)
        ListBox1.Name = "ListBox1"
        ListBox1.Size = New Size(657, 199)
        ListBox1.TabIndex = 6
        ' 
        ' TextBox1
        ' 
        TextBox1.Location = New Point(3, 60)
        TextBox1.Name = "TextBox1"
        TextBox1.Size = New Size(534, 23)
        TextBox1.TabIndex = 5
        ' 
        ' Button1
        ' 
        Button1.Anchor = AnchorStyles.Top Or AnchorStyles.Right
        Button1.AutoSize = True
        Button1.Location = New Point(550, 60)
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
        UpdateLinkButton.Location = New Point(552, 6)
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
        ComboBoxLanguage.Location = New Point(353, 7)
        ComboBoxLanguage.Name = "ComboBoxLanguage"
        ComboBoxLanguage.Size = New Size(150, 23)
        ComboBoxLanguage.TabIndex = 1
        ' 
        ' TabControl1
        ' 
        TabControl1.Controls.Add(TabPage1)
        TabControl1.Controls.Add(TabPage2)
        TabControl1.Dock = DockStyle.Fill
        TabControl1.Location = New Point(0, 0)
        TabControl1.Name = "TabControl1"
        TabControl1.SelectedIndex = 0
        TabControl1.Size = New Size(668, 321)
        TabControl1.TabIndex = 9
        ' 
        ' TabPage1
        ' 
        TabPage1.Controls.Add(ListBox1)
        TabPage1.Controls.Add(Button1)
        TabPage1.Controls.Add(Label2)
        TabPage1.Controls.Add(UpdateLinkButton)
        TabPage1.Controls.Add(ComboBoxLanguage)
        TabPage1.Controls.Add(TextBox1)
        TabPage1.Controls.Add(Label1)
        TabPage1.Location = New Point(4, 24)
        TabPage1.Name = "TabPage1"
        TabPage1.Padding = New Padding(3)
        TabPage1.Size = New Size(660, 293)
        TabPage1.TabIndex = 0
        TabPage1.Text = "TabPage1"
        TabPage1.UseVisualStyleBackColor = True
        ' 
        ' TabPage2
        ' 
        TabPage2.Controls.Add(DataGridView1)
        TabPage2.Location = New Point(4, 24)
        TabPage2.Name = "TabPage2"
        TabPage2.Padding = New Padding(3)
        TabPage2.Size = New Size(660, 293)
        TabPage2.TabIndex = 1
        TabPage2.Text = "TabPage2"
        TabPage2.UseVisualStyleBackColor = True
        ' 
        ' DataGridView1
        ' 
        DataGridView1.AllowUserToOrderColumns = True
        DataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
        DataGridView1.Dock = DockStyle.Fill
        DataGridView1.Location = New Point(3, 3)
        DataGridView1.Name = "DataGridView1"
        DataGridView1.Size = New Size(654, 287)
        DataGridView1.TabIndex = 0
        ' 
        ' Form1
        ' 
        AutoScaleDimensions = New SizeF(7F, 15F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(668, 321)
        Controls.Add(TabControl1)
        Icon = CType(resources.GetObject("$this.Icon"), Icon)
        Name = "Form1"
        Text = "VRChat写真をDiscordにアップ "
        TabControl1.ResumeLayout(False)
        TabPage1.ResumeLayout(False)
        TabPage1.PerformLayout()
        TabPage2.ResumeLayout(False)
        CType(DataGridView1, ComponentModel.ISupportInitialize).EndInit()
        ResumeLayout(False)
    End Sub

    Friend WithEvents Label1 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents ListBox1 As ListBox
    Friend WithEvents TextBox1 As TextBox

    Private Sub TextBox1_TextChanged(sender As Object, e As EventArgs) Handles TextBox1.TextChanged

    End Sub
    Friend WithEvents Button1 As Button
    Friend WithEvents UpdateLinkButton As Button
    Friend WithEvents ComboBoxLanguage As ComboBox
    Friend WithEvents TabPage1 As TabPage
    Friend WithEvents TabPage2 As TabPage
    Friend WithEvents DataGridView1 As DataGridView
    Public WithEvents TabControl1 As TabControl
End Class
