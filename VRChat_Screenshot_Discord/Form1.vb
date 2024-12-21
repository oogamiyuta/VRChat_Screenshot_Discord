Imports System.IO
Imports System.Net.Http
Imports System.Text
Imports System.Text.RegularExpressions
Imports System.Configuration
Imports Newtonsoft.Json
Imports System.Reflection

Public Class Form1
    Private watcher As FileSystemWatcher
    Private httpClient As HttpClient = New HttpClient()
    Private currentLanguage As String = "ja" ' 言語の初期設定

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' 言語設定を読み込み
        currentLanguage = LoadLanguageSelection()
        If String.IsNullOrEmpty(currentLanguage) Then
            currentLanguage = "ja" ' デフォルト値
        End If

        ' 設定値の読み込み
        TextBox1.Text = ConfigurationManager.AppSettings("FolderPath")
        TextBox2.Text = ConfigurationManager.AppSettings("WebhookUrl")

        ' 言語リストをComboBoxに追加
        LoadLanguages()

        ' UIを初期化
        UpdateUIForLanguage()

        ' リンクボタンの初期化
        InitializeUpdateLinkButton()

        ' GitHubのバージョンチェック
        CheckGitHubVersion()
    End Sub

    Private Sub LoadLanguages()
        ComboBoxLanguage.Items.Clear()

        ' 言語をインデックス順に追加
        ComboBoxLanguage.Items.Add(GetLocalizedString("Language_Japanese")) ' インデックス 0
        ComboBoxLanguage.Items.Add(GetLocalizedString("Language_English")) ' インデックス 1
        ComboBoxLanguage.Items.Add(GetLocalizedString("Language_Korean"))  ' インデックス 2
        ComboBoxLanguage.Items.Add(GetLocalizedString("Language_Chinese")) ' インデックス 3

        ' 現在の言語に対応するインデックスを選択
        Select Case currentLanguage
            Case "ja"
                ComboBoxLanguage.SelectedIndex = 0
            Case "en"
                ComboBoxLanguage.SelectedIndex = 1
            Case "ko"
                ComboBoxLanguage.SelectedIndex = 2
            Case "zh"
                ComboBoxLanguage.SelectedIndex = 3
            Case Else
                ComboBoxLanguage.SelectedIndex = 0 ' デフォルトは日本語
        End Select
    End Sub

    Private Sub InitializeUpdateLinkButton()
        With UpdateLinkButton
            .Text = GetLocalizedString("UpdateDownloadButtonText")
            .Visible = False
        End With
        AddHandler UpdateLinkButton.Click, AddressOf OpenUpdateLink
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim folderPath As String = TextBox1.Text
        Dim webhookUrl As String = TextBox2.Text

        If String.IsNullOrWhiteSpace(folderPath) OrElse String.IsNullOrWhiteSpace(webhookUrl) Then
            MessageBox.Show(GetLocalizedString("FolderPathInputMessage"))
            Return
        End If

        If Not Directory.Exists(folderPath) Then
            MessageBox.Show(GetLocalizedString("FolderNotExistMessage"))
            Return
        End If

        SaveSettings("FolderPath", folderPath)
        SaveSettings("WebhookUrl", webhookUrl)

        watcher = New FileSystemWatcher()
        watcher.Path = folderPath
        watcher.Filter = "*.png"
        watcher.IncludeSubdirectories = True
        watcher.NotifyFilter = NotifyFilters.FileName Or NotifyFilters.CreationTime
        AddHandler watcher.Created, AddressOf OnNewImageCreated
        watcher.EnableRaisingEvents = True

        InvokeIfRequired(Sub() ListBox1.Items.Add(GetLocalizedString("StartWatchingMessage") & ": " & folderPath))
    End Sub

    Private Sub CheckGitHubVersion()
        Try
            Dim currentVersion As String = NormalizeVersion(GetCurrentVersion())
            Dim latestVersion As String = GetLatestGitHubVersion()

            latestVersion = latestVersion.Replace("Ver.", "").Trim()

            If Not String.IsNullOrEmpty(latestVersion) AndAlso currentVersion <> latestVersion Then
                Me.Text = GetLocalizedString("AppTitleWithUpdate").Replace("{currentVersion}", currentVersion).Replace("{latestVersion}", latestVersion)
                UpdateLinkButton.Visible = True

                Dim message As String = String.Format(
    GetLocalizedString("UpdateAvailableMessage").Replace("{currentVersion}", currentVersion).Replace("{latestVersion}", latestVersion) &
    Environment.NewLine & GetLocalizedString("UpdateDownloadMessage").Replace("{latestVersion}", latestVersion),
    currentVersion, latestVersion)


                Dim result As DialogResult = MessageBox.Show(message,
                GetLocalizedString("UpdateCheckTitle"),
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Exclamation,
                MessageBoxDefaultButton.Button1)

                If result = DialogResult.Yes Then
                    OpenUpdateLink(Nothing, Nothing)
                End If
            Else
                Me.Text = GetLocalizedString("AppTitle").Replace("{currentVersion}", currentVersion)
                UpdateLinkButton.Visible = False
            End If
        Catch ex As Exception
            InvokeIfRequired(Sub() ListBox1.Items.Add(GetLocalizedString("GitHubVersionCheckError") & ": " & ex.Message))
        End Try
    End Sub

    Private Function GetLocalizedString(key As String) As String
        Dim localizedString As String = Nothing

        Select Case currentLanguage
            Case "ja"
                localizedString = My.Resources.Resources.ResourceManager.GetString(key, New System.Globalization.CultureInfo("ja"))
            Case "en"
                localizedString = My.Resources.Resources.ResourceManager.GetString(key, New System.Globalization.CultureInfo("en"))
            Case "ko"
                localizedString = My.Resources.Resources.ResourceManager.GetString(key, New System.Globalization.CultureInfo("ko"))
            Case "zh"
                localizedString = My.Resources.Resources.ResourceManager.GetString(key, New System.Globalization.CultureInfo("zh"))
            Case Else
                localizedString = My.Resources.Resources.ResourceManager.GetString(key, New System.Globalization.CultureInfo("ja"))
        End Select

        If String.IsNullOrEmpty(localizedString) Then
            localizedString = "リソースが見つかりませんでした: " & key
        End If

        Return localizedString
    End Function

    Private Sub OpenUpdateLink(sender As Object, e As EventArgs)
        Process.Start("https://yuta-vtuber.booth.pm/items/6396052")
    End Sub

    Private Sub SaveSettings(key As String, value As String)
        Dim config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None)
        If config.AppSettings.Settings(key) Is Nothing Then
            config.AppSettings.Settings.Add(key, value)
        Else
            config.AppSettings.Settings(key).Value = value
        End If
        config.Save(ConfigurationSaveMode.Modified)
        ConfigurationManager.RefreshSection("appSettings")
    End Sub

    Private Function GetCurrentVersion() As String
        Return Assembly.GetExecutingAssembly().GetName().Version.ToString()
    End Function

    Private Function GetLatestGitHubVersion() As String
        Dim url As String = "https://api.github.com/repos/oogamiyuta/VRChat_Screenshot_Discord/releases/latest"
        httpClient.DefaultRequestHeaders.UserAgent.ParseAdd("MyApp")
        Dim response = httpClient.GetStringAsync(url).Result
        Dim jsonResponse = JsonConvert.DeserializeObject(Of Dictionary(Of String, Object))(response)

        Return jsonResponse("tag_name").ToString()
    End Function

    Private Function NormalizeVersion(version As String) As String
        Dim match = Regex.Match(version, "(\d+\.\d+\.\d+\.\d+)")
        Return If(match.Success, match.Groups(1).Value, "")
    End Function

    Private Sub OnNewImageCreated(sender As Object, e As FileSystemEventArgs)
        ' 画像が追加された際にログファイルを確認し、ワールド名を抽出
        Try
            Dim baseLogFolder As String = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData).Replace("Local", "LocalLow"), "VRChat", "VRChat")

            If Not Directory.Exists(baseLogFolder) Then
                InvokeIfRequired(Sub() ListBox1.Items.Add(GetLocalizedString("LogFolderNotFoundMessage") & ": " & baseLogFolder))
                Return
            End If

            Dim logFiles = Directory.GetFiles(baseLogFolder, "output_log_*.txt", SearchOption.AllDirectories)

            If logFiles.Length = 0 Then
                InvokeIfRequired(Sub() ListBox1.Items.Add(GetLocalizedString("LogFilesNotFoundMessage")))
                Return
            End If

            Dim latestLogFile = logFiles.OrderByDescending(Function(f) New FileInfo(f).LastWriteTime).FirstOrDefault()

            If latestLogFile IsNot Nothing Then
                Dim worldName As String = ExtractWorldNameFromLog(latestLogFile)
                If Not String.IsNullOrEmpty(worldName) Then
                    SendToDiscord(TextBox2.Text, worldName, e.FullPath)
                End If
            End If
        Catch ex As Exception
            InvokeIfRequired(Sub() ListBox1.Items.Add(GetLocalizedString("ErrorOccurredMessage") & ": " & ex.Message))
        End Try
    End Sub



    Private Sub InvokeIfRequired(action As Action)
        If InvokeRequired Then
            Invoke(action)
        Else
            action()
        End If
    End Sub

    Private Sub SaveLanguageSelection(language As String)
        SaveSettings("Language", language)
    End Sub

    Private Function LoadLanguageSelection() As String
        Return ConfigurationManager.AppSettings("Language")
    End Function

    Private Sub ComboBoxLanguage_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBoxLanguage.SelectedIndexChanged
        ' インデックスで言語を判定
        Select Case ComboBoxLanguage.SelectedIndex
            Case 0 : currentLanguage = "ja"
            Case 1 : currentLanguage = "en"
            Case 2 : currentLanguage = "ko"
            Case 3 : currentLanguage = "zh"
            Case Else : currentLanguage = "ja" ' デフォルト
        End Select

        SaveLanguageSelection(currentLanguage)
        UpdateUIForLanguage()
        ' フォームタイトル（Me.Text）も更新
        Dim currentVersion As String = GetCurrentVersion()
        Dim latestVersion As String = GetLatestGitHubVersion()
        latestVersion = latestVersion.Replace("Ver.", "").Trim()

        If currentVersion <> latestVersion Then
            ' 更新がある場合
            Me.Text = GetLocalizedString("AppTitleWithUpdate").Replace("{currentVersion}", currentVersion).Replace("{latestVersion}", latestVersion)
        Else
            ' 更新がない場合
            Me.Text = GetLocalizedString("AppTitle").Replace("{currentVersion}", currentVersion)
        End If
    End Sub

    Private Sub UpdateUIForLanguage()
        Label1.Text = GetLocalizedString("Language")
        Label2.Text = GetLocalizedString("Label2")
        Button1.Text = GetLocalizedString("start")
        UpdateLinkButton.Text = GetLocalizedString("UpdateDownloadButtonText")
    End Sub

    Private Function ExtractWorldNameFromLog(logFilePath As String) As String
        Try
            ' 読み取り専用でファイルを開く
            Using fs As New FileStream(logFilePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite)
                Using reader As New StreamReader(fs)
                    Dim lines = reader.ReadToEnd().Split(Environment.NewLine)
                    For Each line In lines.Reverse()
                        If line.Contains("Entering Room:") Then
                            Dim match = Regex.Match(line, "Entering Room: (.+)")
                            If match.Success Then
                                Dim worldName = match.Groups(1).Value
                                InvokeIfRequired(Sub() ListBox1.Items.Add(GetLocalizedString("WorldNameDetectedMessage") & ": " & worldName))
                                Return worldName
                            End If
                        End If
                    Next
                End Using
            End Using
        Catch ex As Exception
            InvokeIfRequired(Sub() ListBox1.Items.Add(GetLocalizedString("LogParsingErrorMessage") & ": " & ex.Message))
        End Try

        Return String.Empty
    End Function

    Private Async Sub SendToDiscord(webhookUrl As String, worldName As String, imagePath As String)
        Try
            Dim captureTime As String = File.GetCreationTime(imagePath).ToString("yyyy-MM-dd HH:mm:ss")

            ' JSON形式で送信内容を構築
            Dim jsonPayload As New With {
                Key .content = GetLocalizedString("NewImageUploadedMessage"),
                Key .embeds = New Object() {
                    New With {
                        Key .title = GetLocalizedString("ImageInformationTitle"),
                        Key .fields = New Object() {
                            New With {Key .name = GetLocalizedString("WorldNameLabel"), Key .value = worldName, Key .inline = True},
                            New With {Key .name = GetLocalizedString("CaptureTimeLabel"), Key .value = captureTime, Key .inline = True}
                        }
                    }
                }
            }

            Dim jsonString As String = JsonConvert.SerializeObject(jsonPayload)
            Dim content = New StringContent(jsonString, Encoding.UTF8, "application/json")

            ' 画像を添付する
            Dim boundary As String = "----WebKitFormBoundary" & DateTime.Now.Ticks.ToString("x")
            Dim multipartContent = New MultipartFormDataContent(boundary)
            multipartContent.Add(content, "payload_json")
            multipartContent.Add(New ByteArrayContent(File.ReadAllBytes(imagePath)), "file", Path.GetFileName(imagePath))

            Dim response = Await httpClient.PostAsync(webhookUrl, multipartContent)
            If response.IsSuccessStatusCode Then
                InvokeIfRequired(Sub() ListBox1.Items.Add(GetLocalizedString("DiscordSendSuccessMessage") & ": " & worldName))
            Else
                InvokeIfRequired(Sub() ListBox1.Items.Add(GetLocalizedString("DiscordSendFailureMessage") & ": " & response.StatusCode))
            End If
        Catch ex As Exception
            InvokeIfRequired(Sub() ListBox1.Items.Add(GetLocalizedString("DiscordSendErrorMessage") & ": " & ex.Message))
        End Try
    End Sub


End Class
