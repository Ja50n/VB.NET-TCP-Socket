Imports System.Net
Imports System.Net.Sockets
Imports System.Threading

Public Class TCPClient

#Region "私有成员"
    Private _LocationClientSocket As Socket '本地侦听服务   
    Private _LocalPort As String '本地端口   
    Private _LocalHostName As String
    Private _LocalIP As String
    Private autoEvent As AutoResetEvent
    Private _RemoteHostName As String '遠程端計算機名   
    Private _RemoteIP As String     '遠程端計算機IP   
    Private _RemotePort As String   '遠程端計算機Port   
    Private _RemoteIPOrHostName As String
    'Private _MaxClient As Integer '最大客户端连接数   
    'Private _Clients As New SortedList '客户端队列   
    'Private _ListenThread As Thread = Nothing '侦听线程   
    'Private _ServerStart As Boolean = False '服务器是否已经启动   
    Private _RecvMax As Integer '接收缓冲区大小    

    Private ClientThread As Thread
    'Private ClitenStream As NetworkStream   
    Private IsStop As Boolean = False

#End Region

#Region "事件"
    ''' <summary>   
    ''' 客户端联接事件   
    ''' </summary>    
    ''' <remarks></remarks>   
    Public Event ClientConnected()
    ''' <summary>   
    ''' 客户端断开事件   
    ''' </summary>   
    ''' <remarks></remarks>   
    Public Event ClientClosed()
    ''' <summary>   
    ''' 接收到客户端的数据   
    ''' </summary>   
    ''' <param name="value">数据</param>   
    ''' <remarks></remarks>   
    Public Event DataArrived(ByVal value As Byte(), ByVal Len As Integer)
    ''' <summary>   
    ''' 异常数据   
    ''' </summary>   
    ''' <param name="ex"></param>   
    ''' <remarks></remarks>   
    Public Event Exception(ByVal ex As Exception)
#End Region

#Region "属性"
    ''' <summary>   
    ''' 是否已經連接   
    ''' </summary>   
    ''' <value></value>   
    ''' <returns></returns>   
    ''' <remarks></remarks>   
    Public ReadOnly Property Connected() As Boolean
        Get
            Return _LocationClientSocket.Connected
        End Get
    End Property

    ''' <summary>   
    ''' 本地計算機名稱   
    ''' </summary>   
    ''' <value></value>   
    ''' <returns></returns>   
    ''' <remarks></remarks>   
    Public ReadOnly Property LocalHostName() As String
        Get
            Return _LocalHostName
        End Get
    End Property

    ''' <summary>   
    ''' 本地計算IP   
    ''' </summary>   
    ''' <value></value>   
    ''' <returns></returns>   
    ''' <remarks></remarks>   
    Public ReadOnly Property LocalIP() As String
        Get
            Return _LocalIP
        End Get
    End Property

    ''' <summary>   
    ''' 本地計算機端口   
    ''' </summary>   
    ''' <value></value>   
    ''' <returns></returns>   
    ''' <remarks></remarks>   
    Public ReadOnly Property LocalPort() As String
        Get
            Return _LocalPort
        End Get
    End Property

    ''' <summary>   
    ''' 遠程計算機IP   
    ''' </summary>   
    ''' <value></value>   
    ''' <returns></returns>   
    ''' <remarks></remarks>   
    Public ReadOnly Property RemoteIP() As String
        Get
            Return _RemoteIP
        End Get

    End Property

    ''' <summary>   
    ''' 遠程計算機端口   
    ''' </summary>   
    ''' <value></value>   
    ''' <returns></returns>   
    ''' <remarks></remarks>   
    Public ReadOnly Property RemotePort() As String
        Get
            Return _RemotePort
        End Get

    End Property

    ''' <summary>   
    ''' 遠程計算機名稱   
    ''' </summary>   
    ''' <value></value>   
    ''' <returns></returns>   
    ''' <remarks></remarks>   
    Public ReadOnly Property RemoteHostName() As String
        Get
            Return _RemoteHostName
        End Get

    End Property
#End Region

#Region "方法"
    ''' <summary>   
    ''' 实例　TCPServer   
    ''' </summary>   
    ''' <param name="RemoteIPOrHostName">需要連接服務的IP地址或計算機名稱</param>   
    ''' <param name="Port">侦听客户端联接的端口号</param>   
    ''' <param name="RecvMax">接收缓冲区大小</param>   
    ''' <param name="RecvSleep">接收线程睡眠时间</param>   
    ''' <remarks></remarks>   
    Sub New(ByVal RemoteIPOrHostName As String, ByVal Port As String, ByVal RecvMax As Integer, ByVal RecvSleep As Integer)
        Try
            _LocalHostName = Dns.GetHostName()

            '_RemoteIP = Dns.GetHostAddresses(RemoteIPOrHostName)(0).ToString   
            _RemotePort = Port
            _RecvMax = RecvMax
            _RemoteIPOrHostName = RemoteIPOrHostName
            _RemotePort = Port
            'Dim strServerHost As New IPEndPoint(IPAddress.Any, Int32.Parse(_ListenPort))   

            '建立TCP侦听   
            _LocationClientSocket = New Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp)
            autoEvent = New AutoResetEvent(False)
            Dim cThread As New Thread(New ThreadStart(AddressOf ConnectHost))
            cThread.Start()
            autoEvent.WaitOne(1000, False)
            cThread.Abort()
        Catch ex As Exception
            RaiseEvent Exception(ex)
        End Try
    End Sub
    Public Sub ConnectHost()
        Try
            Dim remoteIP As Net.IPAddress = Nothing
            If IPAddress.TryParse(_RemoteIPOrHostName, remoteIP) Then
                '_LocationClientSocket.BeginConnect()   
                _LocationClientSocket.Connect(remoteIP, _RemotePort)
                '_RemoteIP = RemoteHostName   

            Else
                _LocationClientSocket.Connect(_RemoteIPOrHostName, _RemotePort)
                _RemoteHostName = RemoteHostName
            End If

            If _LocationClientSocket.Connected Then
                _LocationClientSocket.SendBufferSize = _RecvMax
                _LocationClientSocket.ReceiveBufferSize = _RecvMax

                Dim clientInfoT As IPEndPoint

                clientInfoT = CType(_LocationClientSocket.RemoteEndPoint, IPEndPoint)
                _RemoteIP = clientInfoT.Address.ToString
                'Dim remoteHost As Net.IPHostEntry   

                _RemoteHostName = Dns.GetHostEntry(_RemoteIP).HostName

                clientInfoT = CType(_LocationClientSocket.LocalEndPoint, IPEndPoint)

                _LocalIP = clientInfoT.Address.ToString
                _LocalPort = clientInfoT.Port.ToString

                IsStop = False

                RaiseEvent ClientConnected()

                ClientThread = New Thread(New ThreadStart(AddressOf ClientListen))
                ClientThread.Start()
                autoEvent.Set()
            End If
        Catch ex As Exception

        End Try


    End Sub

    ''' <summary>   
    ''' 關閉客戶端連接   
    ''' </summary>   
    ''' <remarks></remarks>   
    Public Sub Close()
        Try
            If _LocationClientSocket Is Nothing Then Exit Sub
            IsStop = True
            If Not ClientThread Is Nothing Then
                Thread.Sleep(5)
                ClientThread.Abort()
            End If
            _LocationClientSocket.Close()
            _LocationClientSocket = Nothing
            ClientThread = Nothing
            RaiseEvent ClientClosed()
        Catch ex As Exception
            RaiseEvent Exception(ex)
        End Try

    End Sub

    ''' <summary>   
    ''' 实例　TCPServer   
    ''' </summary>   
    ''' <param name="value">發送的資料,二進制數組</param>   
    ''' <remarks></remarks>   
    Public Function SendData(ByVal value As Byte()) As Boolean
        Try
            _LocationClientSocket.Send(value)
        Catch ex As Exception
            RaiseEvent Exception(ex)
        End Try
    End Function

    Private Sub ClientListen()
        Dim tmpByt(8192) As Byte
        Dim recData() As Byte
        Dim R As Integer
        While Not IsStop
            Try
                If _LocationClientSocket.Poll(50, SelectMode.SelectWrite) Then
                    R = _LocationClientSocket.Receive(tmpByt)
                    If R > 0 Then
                        ReDim recData(R - 1)
                        Array.Copy(tmpByt, recData, R)
                        RaiseEvent DataArrived(recData, recData.Length)
                    Else
                        If (Not _LocationClientSocket Is Nothing) Then
                            _LocationClientSocket.Close()
                            _LocationClientSocket = Nothing
                            IsStop = True
                            RaiseEvent ClientClosed()
                        End If
                    End If
                End If
            Catch sex As SocketException
                If sex.ErrorCode = 10054 Then
                    If (Not _LocationClientSocket Is Nothing) Then
                        _LocationClientSocket.Close()
                        _LocationClientSocket = Nothing
                        IsStop = True
                        RaiseEvent ClientClosed()
                    End If
                Else
                    RaiseEvent Exception(sex)
                End If
            Catch ex As Exception
                RaiseEvent Exception(ex)
            End Try
        End While
    End Sub
#End Region

End Class