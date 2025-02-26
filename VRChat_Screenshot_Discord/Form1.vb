Imports System.Configuration
Imports System.Drawing.Imaging
Imports System.IO
Imports System.Net.Http
Imports System.Reflection
Imports System.Text
Imports System.Text.RegularExpressions
Imports Newtonsoft.Json

Public Class Form1
    Private watcher As FileSystemWatcher
    Private httpClient As HttpClient = New HttpClient()
    Private currentLanguage As String = "ja" ' 言語の初期設定
    Private appDataPath As String = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "VRChat_Screenshot_Discord")
    Private configFilePath As String = Path.Combine(appDataPath, "VRChat_Screenshot_Discord.dll.config")
    Private configMap As New ExeConfigurationFileMap() With {.ExeConfigFilename = configFilePath}
    Private config As Configuration = ConfigurationManager.OpenMappedExeConfiguration(configMap, ConfigurationUserLevel.None)

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' AppDataフォルダの作成
        If Not Directory.Exists(appDataPath) Then
            Directory.CreateDirectory(appDataPath)
        End If

        ' 言語設定を読み込み
        currentLanguage = LoadLanguageSelection()
        If String.IsNullOrEmpty(currentLanguage) Then
            currentLanguage = "ja" ' デフォルト値
        End If

        ' 設定値の読み込み
        TextBox1.Text = config.AppSettings.Settings("FolderPath")?.Value
        Dim compressionSetting As String = config.AppSettings.Settings("Compression")?.Value
        If String.IsNullOrEmpty(compressionSetting) Then
            Compression.Checked = True ' デフォルトでON
        Else
            Compression.Checked = Convert.ToBoolean(compressionSetting)
        End If

        ' 言語リストをComboBoxに追加
        LoadLanguages()

        ' UIを初期化
        UpdateUIForLanguage()

        ' リンクボタンの初期化
        InitializeUpdateLinkButton()

        ' GitHubのバージョンチェック
        CheckGitHubVersion()

        ' DataGridViewの内容をロード
        LoadDataGridView()
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

    Private Async Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim folderPath As String = TextBox1.Text
        Dim webhookUrls As New List(Of String)

        ' DataGridView1 から WebhookUrl を取得
        For Each row As DataGridViewRow In DataGridView1.Rows
            If Not row.IsNewRow AndAlso CBool(row.Cells("onoff").Value) Then
                webhookUrls.Add(row.Cells("WebHookURL").Value.ToString())
            End If
        Next

        If String.IsNullOrWhiteSpace(folderPath) OrElse webhookUrls.Count = 0 Then
            MessageBox.Show(GetLocalizedString("FolderPathInputMessage"))
            Return
        End If

        If Not Directory.Exists(folderPath) Then
            MessageBox.Show(GetLocalizedString("FolderNotExistMessage"))
            Return
        End If

        SaveSettings("FolderPath", folderPath)
        ' WebhookUrl の保存は DataGridView1 の内容に基づいて行う必要があります

        watcher = New FileSystemWatcher()
        watcher.Path = folderPath
        watcher.Filter = "*.png"
        watcher.IncludeSubdirectories = True
        watcher.NotifyFilter = NotifyFilters.FileName Or NotifyFilters.CreationTime
        AddHandler watcher.Created, AddressOf OnNewImageCreated
        watcher.EnableRaisingEvents = True

        InvokeIfRequired(Sub() ListBox1.Items.Add(GetLocalizedString("StartWatchingMessage") & ": " & folderPath))
    End Sub

    Private Async Sub CheckGitHubVersion()
        Try
            Dim currentVersion As String = NormalizeVersion(GetCurrentVersion())
            Dim latestVersion As String = Await GetLatestGitHubVersionAsync()

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
        If Not Directory.Exists(appDataPath) Then
            Directory.CreateDirectory(appDataPath)
        End If

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

    Private Async Function GetLatestGitHubVersionAsync() As Task(Of String)
        Dim url As String = "https://api.github.com/repos/oogamiyuta/VRChat_Screenshot_Discord/releases/latest"
        httpClient.DefaultRequestHeaders.UserAgent.ParseAdd("MyApp")
        Dim response = Await httpClient.GetStringAsync(url)
        Dim jsonResponse = JsonConvert.DeserializeObject(Of Dictionary(Of String, Object))(response)

        Return jsonResponse("tag_name").ToString()
    End Function

    Private Function NormalizeVersion(version As String) As String
        Dim match = Regex.Match(version, "(\d+\.\d+\.\d+\.\d+)")
        Return If(match.Success, match.Groups(1).Value, "")
    End Function

    Private Async Sub OnNewImageCreated(sender As Object, e As FileSystemEventArgs)
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
                    ' DataGridView1 から WebhookUrl を取得して送信
                    For Each row As DataGridViewRow In DataGridView1.Rows
                        If Not row.IsNewRow AndAlso CBool(row.Cells("onoff").Value) Then
                            Dim serverName As String = row.Cells("servername").Value.ToString()
                            Dim webhookUrl As String = row.Cells("WebHookURL").Value.ToString()
                            Await SendToDiscord(webhookUrl, worldName, e.FullPath, serverName)
                        End If
                    Next
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
        Return config.AppSettings.Settings("Language")?.Value
    End Function

    Private Async Sub ComboBoxLanguage_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBoxLanguage.SelectedIndexChanged
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
        Dim latestVersion As String = Await GetLatestGitHubVersionAsync()
        latestVersion = latestVersion.Replace("Ver.", "").Trim()
        DataGridView1.Columns("onoff").HeaderText = GetLocalizedString("onoff")
        DataGridView1.Columns("servername").HeaderText = GetLocalizedString("servername")
        Compression.Text = GetLocalizedString("10MBCompression")

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
        TabControl1.TabPages(0).Text = GetLocalizedString("Page1")
        TabControl1.TabPages(1).Text = GetLocalizedString("Page2")
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

    Private Async Function SendToDiscord(webhookUrl As String, worldName As String, imagePath As String, serverName As String) As Task
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

            ' リクエストの作成
            Dim boundary As String = "----WebKitFormBoundary" & DateTime.Now.Ticks.ToString("x")
            Dim multipartContent = New MultipartFormDataContent(boundary)
            multipartContent.Add(content, "payload_json")

            ' 画像の容量
            Dim originalFileSize As Long = New FileInfo(imagePath).Length
            ' Discordの上限容量
            Dim targetFileSize As Long = 10 * 1024 * 1024 ' 10MB

            ' Discordの上限を超えたら画像を圧縮する(チェックボックスがONのとき)
            If originalFileSize > targetFileSize And Compression.Checked = True Then
                ' 初期圧縮率は100%
                Dim quality As Long = 100

                ' 圧縮率の決め打ちで圧縮をかける(処理は少ないが，画質の劣化が激しい)
                ' Using originalImage As Image = Image.FromFile(imagePath)
                '     ' https://qiita.com/sonoshou/items/4e7d58ee3124973085bc
                '     quality = ((targetFileSize / 1024) / Math.Sqrt(3 * originalImage.Width * originalImage.Height)) * 100
                '     Dim jpegEncoder As ImageCodecInfo = ImageCodecInfo.GetImageDecoders().First(Function(c) c.FormatID = ImageFormat.Jpeg.Guid)
                '     Dim encoderParams As New EncoderParameters(1)
                '     encoderParams.Param(0) = New EncoderParameter(System.Drawing.Imaging.Encoder.Quality, quality)

                '     ' メモリストリームに書き出して添付する
                '     Using ms As New MemoryStream()
                '         originalImage.Save(ms, jpegEncoder, encoderParams)
                '         ms.Seek(0, SeekOrigin.Begin)

                '         ' 圧縮した画像を添付する
                '         multipartContent.Add(New ByteArrayContent(ms.ToArray()), "file", Path.GetFileName(imagePath))

                '         ' デバッグ用メッセージ
                '         ' 圧縮後のファイルサイズを取得
                '         ' Dim compressedFileSize As Long = ms.Length
                '         ' InvokeIfRequired(Sub() ListBox1.Items.Add("originalImage: " & Math.Floor(originalFileSize / 10e5 * 100) / 100 & "MB compressedImage: " & Math.Floor(compressedFileSize / 10e5 * 100) / 100 & "MB quality: " & quality & "%"))
                '     End Using
                ' End Using


                ' ' 圧縮後のファイルサイズによって２部探索で圧縮率を決定する
                Dim minQuality As Long = 10
                Dim maxQuality As Long = 100
                Dim jpegEncoder As ImageCodecInfo = ImageCodecInfo.GetImageDecoders().First(Function(c) c.FormatID = ImageFormat.Jpeg.Guid)

                Using originalImage As Image = Image.FromFile(imagePath)

                    ' 二分探索で最適な品質を探す
                    While minQuality <= maxQuality
                        Dim midQuality As Long = (minQuality + maxQuality) / 2

                        Dim encoderParams As New EncoderParameters(1)
                        encoderParams.Param(0) = New EncoderParameter(System.Drawing.Imaging.Encoder.Quality, midQuality)

                        Using ms As New MemoryStream()
                            originalImage.Save(ms, jpegEncoder, encoderParams)
                            Dim fileSize As Long = ms.Length

                            If fileSize <= targetFileSize Then
                                quality = midQuality
                                minQuality = midQuality + 1
                            Else
                                maxQuality = midQuality - 1
                            End If
                        End Using
                    End While

                    ' 最適な品質で保存
                    Dim finalParams As New EncoderParameters(1)
                    finalParams.Param(0) = New EncoderParameter(System.Drawing.Imaging.Encoder.Quality, quality)

                    ' メモリストリームに書き出して添付する
                    Using ms As New MemoryStream()
                        originalImage.Save(ms, jpegEncoder, finalParams)
                        ms.Seek(0, SeekOrigin.Begin)

                        ' 圧縮した画像を添付する
                        multipartContent.Add(New ByteArrayContent(ms.ToArray()), "file", Path.GetFileName(imagePath))

                        ' デバッグ用メッセージ
                        ' 圧縮後のファイルサイズを取得
                        ' Dim compressedFileSize As Long = ms.Length
                        ' InvokeIfRequired(Sub() ListBox1.Items.Add("originalImage: " & Math.Floor(originalFileSize / 10e5 * 100) / 100 & "MB compressedImage: " & Math.Floor(compressedFileSize / 10e5 * 100) / 100 & "MB quality: " & quality & "%"))
                    End Using

                End Using

            Else
                ' 画像を添付する
                multipartContent.Add(New ByteArrayContent(File.ReadAllBytes(imagePath)), "file", Path.GetFileName(imagePath))
            End If

            Dim response = Await httpClient.PostAsync(webhookUrl, multipartContent)
            If response.IsSuccessStatusCode Then
                InvokeIfRequired(Sub() ListBox1.Items.Add(GetLocalizedString("DiscordSendSuccessMessage") & ": " & serverName & " - " & worldName))
            Else
                InvokeIfRequired(Sub() ListBox1.Items.Add(GetLocalizedString("DiscordSendFailureMessage") & ": " & serverName & " - " & response.StatusCode))
            End If
        Catch ex As Exception
            InvokeIfRequired(Sub() ListBox1.Items.Add(GetLocalizedString("DiscordSendErrorMessage") & ": " & serverName & " - " & ex.Message))
        End Try
    End Function

    Private Sub LoadDataGridView()
        Dim filePath As String = Path.Combine(appDataPath, "DataGridViewData.json")
        Dim dataTable As New DataTable()
        ' DataTableの列を明示的に定義
        dataTable.Columns.Add("onoff", GetType(Boolean))
        dataTable.Columns.Add("servername", GetType(String))
        dataTable.Columns.Add("WebHookURL", GetType(String))

        If File.Exists(filePath) Then
            Dim jsonData As String = File.ReadAllText(filePath)
            Dim rows = JsonConvert.DeserializeObject(Of List(Of Dictionary(Of String, Object)))(jsonData)
            For Each row In rows
                Dim dataRow = dataTable.NewRow()
                dataRow("onoff") = Convert.ToBoolean(row("onoff"))
                dataRow("servername") = Convert.ToString(row("servername"))
                dataRow("WebHookURL") = Convert.ToString(row("WebHookURL"))
                dataTable.Rows.Add(dataRow)
            Next
        End If

        DataGridView1.DataSource = dataTable

        DataGridView1.Columns("onoff").HeaderText = GetLocalizedString("onoff")
        DataGridView1.Columns("servername").HeaderText = GetLocalizedString("servername")
        DataGridView1.Columns(0).AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
        DataGridView1.Columns(1).AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
        DataGridView1.Columns(2).AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
    End Sub


    Private Sub SaveDataGridView()
        Dim dataTable As New DataTable()
        ' DataTableの列を明示的に定義
        dataTable.Columns.Add("onoff", GetType(Boolean))
        dataTable.Columns.Add("servername", GetType(String))
        dataTable.Columns.Add("WebHookURL", GetType(String))
        For Each row As DataGridViewRow In DataGridView1.Rows
            If Not row.IsNewRow Then
                Dim dataRow As DataRow = dataTable.NewRow()
                dataRow("onoff") = row.Cells("onoff").Value
                dataRow("servername") = row.Cells("servername").Value
                dataRow("WebHookURL") = row.Cells("WebHookURL").Value
                dataTable.Rows.Add(dataRow)
            End If
        Next
        Dim jsonData As String = JsonConvert.SerializeObject(dataTable, Formatting.Indented)
        Dim filePath As String = Path.Combine(appDataPath, "DataGridViewData.json")
        If Not Directory.Exists(appDataPath) Then
            Directory.CreateDirectory(appDataPath)
        End If
        File.WriteAllText(filePath, jsonData)
    End Sub

    Protected Overrides Sub OnFormClosing(e As FormClosingEventArgs)
        MyBase.OnFormClosing(e)
        ' リソースの解放
        If watcher IsNot Nothing Then
            watcher.Dispose()
        End If
        If httpClient IsNot Nothing Then
            httpClient.Dispose()
        End If
        ' DataGridViewの内容をセーブ
        SaveDataGridView()
    End Sub

    Private Sub Compression_CheckedChanged(sender As Object, e As EventArgs) Handles Compression.CheckedChanged
        SaveSettings("Compression", Compression.Checked.ToString())
    End Sub
End Class

