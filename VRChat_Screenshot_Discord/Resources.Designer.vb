﻿'------------------------------------------------------------------------------
' <auto-generated>
'     このコードはツールによって生成されました。
'     ランタイム バージョン:4.0.30319.42000
'
'     このファイルへの変更は、以下の状況下で不正な動作の原因になったり、
'     コードが再生成されるときに損失したりします。
' </auto-generated>
'------------------------------------------------------------------------------

Option Strict On
Option Explicit On

Imports System

Namespace My.Resources
    
    'このクラスは StronglyTypedResourceBuilder クラスが ResGen
    'または Visual Studio のようなツールを使用して自動生成されました。
    'メンバーを追加または削除するには、.ResX ファイルを編集して、/str オプションと共に
    'ResGen を実行し直すか、または VS プロジェクトをビルドし直します。
    '''<summary>
    '''  ローカライズされた文字列などを検索するための、厳密に型指定されたリソース クラスです。
    '''</summary>
    <Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "17.0.0.0"),  _
     Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
     Global.System.Runtime.CompilerServices.CompilerGeneratedAttribute()>  _
    Friend Class Resources
        
        Private Shared resourceMan As Global.System.Resources.ResourceManager
        
        Private Shared resourceCulture As Global.System.Globalization.CultureInfo
        
        <Global.System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")>  _
        Friend Sub New()
            MyBase.New
        End Sub
        
        '''<summary>
        '''  このクラスで使用されているキャッシュされた ResourceManager インスタンスを返します。
        '''</summary>
        <Global.System.ComponentModel.EditorBrowsableAttribute(Global.System.ComponentModel.EditorBrowsableState.Advanced)>  _
        Friend Shared ReadOnly Property ResourceManager() As Global.System.Resources.ResourceManager
            Get
                If Object.ReferenceEquals(resourceMan, Nothing) Then
                    Dim temp As Global.System.Resources.ResourceManager = New Global.System.Resources.ResourceManager("VRChat_Screenshot_Discord.Resources", GetType(Resources).Assembly)
                    resourceMan = temp
                End If
                Return resourceMan
            End Get
        End Property
        
        '''<summary>
        '''  すべてについて、現在のスレッドの CurrentUICulture プロパティをオーバーライドします
        '''  現在のスレッドの CurrentUICulture プロパティをオーバーライドします。
        '''</summary>
        <Global.System.ComponentModel.EditorBrowsableAttribute(Global.System.ComponentModel.EditorBrowsableState.Advanced)>  _
        Friend Shared Property Culture() As Global.System.Globalization.CultureInfo
            Get
                Return resourceCulture
            End Get
            Set
                resourceCulture = value
            End Set
        End Property
        
        '''<summary>
        '''  VRChat写真をDiscordにアップ - Ver.{currentVersion} に類似しているローカライズされた文字列を検索します。
        '''</summary>
        Friend Shared ReadOnly Property AppTitle() As String
            Get
                Return ResourceManager.GetString("AppTitle", resourceCulture)
            End Get
        End Property
        
        '''<summary>
        '''  VRChat写真をDiscordにアップ - Ver.{currentVersion}（新しいバージョン {latestVersion} があります！） に類似しているローカライズされた文字列を検索します。
        '''</summary>
        Friend Shared ReadOnly Property AppTitleWithUpdate() As String
            Get
                Return ResourceManager.GetString("AppTitleWithUpdate", resourceCulture)
            End Get
        End Property
        
        '''<summary>
        '''  撮影日時 に類似しているローカライズされた文字列を検索します。
        '''</summary>
        Friend Shared ReadOnly Property CaptureTimeLabel() As String
            Get
                Return ResourceManager.GetString("CaptureTimeLabel", resourceCulture)
            End Get
        End Property
        
        '''<summary>
        '''  Discord送信エラー に類似しているローカライズされた文字列を検索します。
        '''</summary>
        Friend Shared ReadOnly Property DiscordSendErrorMessage() As String
            Get
                Return ResourceManager.GetString("DiscordSendErrorMessage", resourceCulture)
            End Get
        End Property
        
        '''<summary>
        '''  Discord送信失敗 に類似しているローカライズされた文字列を検索します。
        '''</summary>
        Friend Shared ReadOnly Property DiscordSendFailureMessage_() As String
            Get
                Return ResourceManager.GetString("DiscordSendFailureMessage ", resourceCulture)
            End Get
        End Property
        
        '''<summary>
        '''  Discordに送信成功 に類似しているローカライズされた文字列を検索します。
        '''</summary>
        Friend Shared ReadOnly Property DiscordSendSuccessMessage() As String
            Get
                Return ResourceManager.GetString("DiscordSendSuccessMessage", resourceCulture)
            End Get
        End Property
        
        '''<summary>
        '''  エラー に類似しているローカライズされた文字列を検索します。
        '''</summary>
        Friend Shared ReadOnly Property ErrorOccurredMessage_() As String
            Get
                Return ResourceManager.GetString("ErrorOccurredMessage ", resourceCulture)
            End Get
        End Property
        
        '''<summary>
        '''  指定されたフォルダーが存在しません。 に類似しているローカライズされた文字列を検索します。
        '''</summary>
        Friend Shared ReadOnly Property FolderNotExistMessage() As String
            Get
                Return ResourceManager.GetString("FolderNotExistMessage", resourceCulture)
            End Get
        End Property
        
        '''<summary>
        '''  フォルダーのパスとWebhook URLを入力してください。 に類似しているローカライズされた文字列を検索します。
        '''</summary>
        Friend Shared ReadOnly Property FolderPathInputMessage() As String
            Get
                Return ResourceManager.GetString("FolderPathInputMessage", resourceCulture)
            End Get
        End Property
        
        '''<summary>
        '''  GitHubバージョンチェック中のエラー に類似しているローカライズされた文字列を検索します。
        '''</summary>
        Friend Shared ReadOnly Property GitHubVersionCheckError() As String
            Get
                Return ResourceManager.GetString("GitHubVersionCheckError", resourceCulture)
            End Get
        End Property
        
        '''<summary>
        '''  画像情報 に類似しているローカライズされた文字列を検索します。
        '''</summary>
        Friend Shared ReadOnly Property ImageInformationTitle() As String
            Get
                Return ResourceManager.GetString("ImageInformationTitle", resourceCulture)
            End Get
        End Property
        
        '''<summary>
        '''  VRChat画像フォルダパス に類似しているローカライズされた文字列を検索します。
        '''</summary>
        Friend Shared ReadOnly Property Label2() As String
            Get
                Return ResourceManager.GetString("Label2", resourceCulture)
            End Get
        End Property
        
        '''<summary>
        '''  言語/Language/语言/언어 に類似しているローカライズされた文字列を検索します。
        '''</summary>
        Friend Shared ReadOnly Property Language() As String
            Get
                Return ResourceManager.GetString("Language", resourceCulture)
            End Get
        End Property
        
        '''<summary>
        '''  中文 に類似しているローカライズされた文字列を検索します。
        '''</summary>
        Friend Shared ReadOnly Property Language_Chinese() As String
            Get
                Return ResourceManager.GetString("Language_Chinese", resourceCulture)
            End Get
        End Property
        
        '''<summary>
        '''  English に類似しているローカライズされた文字列を検索します。
        '''</summary>
        Friend Shared ReadOnly Property Language_English() As String
            Get
                Return ResourceManager.GetString("Language_English", resourceCulture)
            End Get
        End Property
        
        '''<summary>
        '''  日本語 に類似しているローカライズされた文字列を検索します。
        '''</summary>
        Friend Shared ReadOnly Property Language_Japanese() As String
            Get
                Return ResourceManager.GetString("Language_Japanese", resourceCulture)
            End Get
        End Property
        
        '''<summary>
        '''  한국어 に類似しているローカライズされた文字列を検索します。
        '''</summary>
        Friend Shared ReadOnly Property Language_Korean() As String
            Get
                Return ResourceManager.GetString("Language_Korean", resourceCulture)
            End Get
        End Property
        
        '''<summary>
        '''  ログファイルが見つかりません に類似しているローカライズされた文字列を検索します。
        '''</summary>
        Friend Shared ReadOnly Property LogFilesNotFoundMessage() As String
            Get
                Return ResourceManager.GetString("LogFilesNotFoundMessage", resourceCulture)
            End Get
        End Property
        
        '''<summary>
        '''  ログフォルダーが見つかりません に類似しているローカライズされた文字列を検索します。
        '''</summary>
        Friend Shared ReadOnly Property LogFolderNotFoundMessage() As String
            Get
                Return ResourceManager.GetString("LogFolderNotFoundMessage", resourceCulture)
            End Get
        End Property
        
        '''<summary>
        '''  ログ解析中のエラー に類似しているローカライズされた文字列を検索します。
        '''</summary>
        Friend Shared ReadOnly Property LogParsingErrorMessage() As String
            Get
                Return ResourceManager.GetString("LogParsingErrorMessage", resourceCulture)
            End Get
        End Property
        
        '''<summary>
        '''  新しい画像が検出されました に類似しているローカライズされた文字列を検索します。
        '''</summary>
        Friend Shared ReadOnly Property NewImageDetectedMessage() As String
            Get
                Return ResourceManager.GetString("NewImageDetectedMessage", resourceCulture)
            End Get
        End Property
        
        '''<summary>
        '''  新しい画像がアップロードされました  に類似しているローカライズされた文字列を検索します。
        '''</summary>
        Friend Shared ReadOnly Property NewImageUploadedMessage_() As String
            Get
                Return ResourceManager.GetString("NewImageUploadedMessage ", resourceCulture)
            End Get
        End Property
        
        '''<summary>
        '''  監視開始 に類似しているローカライズされた文字列を検索します。
        '''</summary>
        Friend Shared ReadOnly Property start() As String
            Get
                Return ResourceManager.GetString("start", resourceCulture)
            End Get
        End Property
        
        '''<summary>
        '''  監視を開始しました に類似しているローカライズされた文字列を検索します。
        '''</summary>
        Friend Shared ReadOnly Property StartWatchingMessage() As String
            Get
                Return ResourceManager.GetString("StartWatchingMessage", resourceCulture)
            End Get
        End Property
        
        '''<summary>
        '''  Ver. {0} を実行していますが、新しいバージョン {1} があります！ に類似しているローカライズされた文字列を検索します。
        '''</summary>
        Friend Shared ReadOnly Property UpdateAvailableMessage() As String
            Get
                Return ResourceManager.GetString("UpdateAvailableMessage", resourceCulture)
            End Get
        End Property
        
        '''<summary>
        '''  アップデートの確認 に類似しているローカライズされた文字列を検索します。
        '''</summary>
        Friend Shared ReadOnly Property UpdateCheckTitle() As String
            Get
                Return ResourceManager.GetString("UpdateCheckTitle", resourceCulture)
            End Get
        End Property
        
        '''<summary>
        '''  更新をダウンロード に類似しているローカライズされた文字列を検索します。
        '''</summary>
        Friend Shared ReadOnly Property UpdateDownloadButtonText() As String
            Get
                Return ResourceManager.GetString("UpdateDownloadButtonText", resourceCulture)
            End Get
        End Property
        
        '''<summary>
        '''  BoothでVer.{0}をダウンロードしますか？ に類似しているローカライズされた文字列を検索します。
        '''</summary>
        Friend Shared ReadOnly Property UpdateDownloadMessage() As String
            Get
                Return ResourceManager.GetString("UpdateDownloadMessage", resourceCulture)
            End Get
        End Property
        
        '''<summary>
        '''  ワールド名を検出 に類似しているローカライズされた文字列を検索します。
        '''</summary>
        Friend Shared ReadOnly Property WorldNameDetectedMessage() As String
            Get
                Return ResourceManager.GetString("WorldNameDetectedMessage", resourceCulture)
            End Get
        End Property
        
        '''<summary>
        '''  ワールド名 に類似しているローカライズされた文字列を検索します。
        '''</summary>
        Friend Shared ReadOnly Property WorldNameLabel() As String
            Get
                Return ResourceManager.GetString("WorldNameLabel", resourceCulture)
            End Get
        End Property
    End Class
End Namespace
