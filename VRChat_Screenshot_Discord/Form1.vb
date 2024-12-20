﻿Imports System.IO
Imports System.Net.Http
Imports System.Text
Imports System.Text.RegularExpressions
Imports System.Configuration
Imports Newtonsoft.Json
Imports System.Reflection

Public Class Form1
    Private watcher As FileSystemWatcher
    Private httpClient As HttpClient = New HttpClient()

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' 設定値の読み込み
        TextBox1.Text = ConfigurationManager.AppSettings("FolderPath")
        TextBox2.Text = ConfigurationManager.AppSettings("WebhookUrl")

        ' リンクボタンの初期化
        InitializeUpdateLinkButton()

        ' GitHubのバージョンチェック
        CheckGitHubVersion()
    End Sub

    Private Sub InitializeUpdateLinkButton()
        With UpdateLinkButton
            .Text = "更新をダウンロード"
            .Visible = False ' 初期状態では非表示

        End With
        AddHandler UpdateLinkButton.Click, AddressOf OpenUpdateLink

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim folderPath As String = TextBox1.Text
        Dim webhookUrl As String = TextBox2.Text

        If String.IsNullOrWhiteSpace(folderPath) OrElse String.IsNullOrWhiteSpace(webhookUrl) Then
            MessageBox.Show("フォルダーのパスとWebhook URLを入力してください。")
            Return
        End If

        If Not Directory.Exists(folderPath) Then
            MessageBox.Show("指定されたフォルダーが存在しません。")
            Return
        End If

        ' 設定値の保存
        SaveSettings("FolderPath", folderPath)
        SaveSettings("WebhookUrl", webhookUrl)

        ' ファイル監視の設定
        watcher = New FileSystemWatcher()
        watcher.Path = folderPath
        watcher.Filter = "*.png"
        watcher.IncludeSubdirectories = True
        watcher.NotifyFilter = NotifyFilters.FileName Or NotifyFilters.CreationTime
        AddHandler watcher.Created, AddressOf OnNewImageCreated
        watcher.EnableRaisingEvents = True

        InvokeIfRequired(Sub() ListBox1.Items.Add("監視を開始しました: " & folderPath))
    End Sub

    Private Sub CheckGitHubVersion()
        Try
            Dim currentVersion As String = NormalizeVersion(GetCurrentVersion())
            Dim latestVersion As String = GetLatestGitHubVersion()

            If Not String.IsNullOrEmpty(latestVersion) AndAlso currentVersion <> latestVersion Then
                Me.Text = $"VRChat写真をDiscordにアップ - Ver. {currentVersion}（新しいバージョン {latestVersion} があります！）"
                UpdateLinkButton.Visible = True

                'メッセージボックスを表示する 
                Dim result As DialogResult = MessageBox.Show($" Ver. {currentVersion}（新しいバージョン {latestVersion} があります！）" & vbCrLf & "boothでダウンロードしますか？",
                                                             "アップデートの確認",
                                                             MessageBoxButtons.YesNo,
                                                             MessageBoxIcon.Exclamation,
                                                             MessageBoxDefaultButton.Button1)

                '何が選択されたか調べる 
                If result = DialogResult.Yes Then
                    '「はい」が選択された時 
                    OpenUpdateLink(Nothing, Nothing)
                ElseIf result = DialogResult.No Then
                    '「いいえ」が選択された時 


                End If

            Else
                Me.Text = $"VRChat写真をDiscordにアップ - Ver. {currentVersion}"
                UpdateLinkButton.Visible = False
            End If
        Catch ex As Exception
            InvokeIfRequired(Sub() ListBox1.Items.Add("GitHubバージョンチェック中のエラー: " & ex.Message))
        End Try
    End Sub

    Private Function GetCurrentVersion() As String
        Return Assembly.GetExecutingAssembly().GetName().Version.ToString()
    End Function

    Private Function GetLatestGitHubVersion() As String
        Dim apiUrl As String = "https://api.github.com/repos/oogamiyuta/VRChat_Screenshot_Discord/releases/latest" ' 必要に応じて変更
        Dim latestVersion As String = String.Empty

        Try
            httpClient.DefaultRequestHeaders.Add("User-Agent", "VB.NET Application")
            Dim response = httpClient.GetStringAsync(apiUrl).Result
            Dim jsonResponse = JsonConvert.DeserializeObject(Of Dictionary(Of String, Object))(response)

            If jsonResponse.ContainsKey("tag_name") Then
                ' プレフィックスを除去してバージョン番号を取得
                Dim rawVersion As String = jsonResponse("tag_name").ToString()
                latestVersion = NormalizeVersion(rawVersion)
            End If
        Catch ex As Exception
            InvokeIfRequired(Sub() ListBox1.Items.Add("GitHubからのバージョン取得エラー: " & ex.Message))
        End Try

        Return latestVersion
    End Function
    Private Function NormalizeVersion(version As String) As String
        ' 正規表現でバージョン番号部分を抽出（例: "Ver.1.0.0.0" → "1.0.0.0"）
        Dim match = Regex.Match(version, "\d+(\.\d+)+")
        If match.Success Then
            Return match.Value
        End If

        ' バージョン番号が見つからない場合はそのまま返す
        Return version
    End Function

    Private Sub OpenUpdateLink(sender As Object, e As EventArgs)
        Dim updateUrl As String = "https://yuta-vtuber.booth.pm/items/6396052"
        Process.Start(New ProcessStartInfo(updateUrl) With {.UseShellExecute = True})
    End Sub


    ' 以下は既存コード（編集なし）
    Private Sub OnNewImageCreated(sender As Object, e As FileSystemEventArgs)
        ' 画像が追加された際にログファイルを確認し、ワールド名を抽出
        Try
            Dim baseLogFolder As String = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData).Replace("Local", "LocalLow"), "VRChat", "VRChat")

            If Not Directory.Exists(baseLogFolder) Then
                InvokeIfRequired(Sub() ListBox1.Items.Add("ログフォルダーが見つかりません: " & baseLogFolder))
                Return
            End If

            Dim logFiles = Directory.GetFiles(baseLogFolder, "output_log_*.txt", SearchOption.AllDirectories)

            If logFiles.Length = 0 Then
                InvokeIfRequired(Sub() ListBox1.Items.Add("ログファイルが見つかりません。"))
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
            InvokeIfRequired(Sub() ListBox1.Items.Add("エラー: " & ex.Message))
        End Try
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
                                InvokeIfRequired(Sub() ListBox1.Items.Add("ワールド名を検出: " & worldName))
                                Return worldName
                            End If
                        End If
                    Next
                End Using
            End Using
        Catch ex As Exception
            InvokeIfRequired(Sub() ListBox1.Items.Add("ログ解析中のエラー: " & ex.Message))
        End Try

        Return String.Empty
    End Function

    Private Async Sub SendToDiscord(webhookUrl As String, worldName As String, imagePath As String)
        Try
            Dim captureTime As String = File.GetCreationTime(imagePath).ToString("yyyy-MM-dd HH:mm:ss")

            ' JSON形式で送信内容を構築
            Dim jsonPayload As New With {
                Key .content = "新しい画像がアップロードされました。",
                Key .embeds = New Object() {
                    New With {
                        Key .title = "画像情報",
                        Key .fields = New Object() {
                            New With {Key .name = "ワールド名", Key .value = worldName, Key .inline = True},
                            New With {Key .name = "撮影日時", Key .value = captureTime, Key .inline = True}
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
                InvokeIfRequired(Sub() ListBox1.Items.Add("Discordに送信成功: " & worldName))
            Else
                InvokeIfRequired(Sub() ListBox1.Items.Add("Discord送信失敗: " & response.StatusCode))
            End If
        Catch ex As Exception
            InvokeIfRequired(Sub() ListBox1.Items.Add("Discord送信エラー: " & ex.Message))
        End Try
    End Sub


    Private Sub InvokeIfRequired(action As Action)
        If Me.InvokeRequired Then
            Me.Invoke(action)
        Else
            action()
        End If
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
End Class
