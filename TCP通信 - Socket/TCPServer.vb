Imports System.Net
Imports System.Net.Sockets
Imports System.Threading
Imports System.Text


'**********************************************************************************************************   
'''''' 类名：TCPServer     
'''''' 说明：监听主线程，用于监听客户端联接，并记录客户端联接，接收和发送数据   
'''''' 与客户端的联接采用TCP联接   
'**********************************************************************************************************   


''' <summary>   
''' 侦听客户端联接   
''' </summary>   
Public Class TCPServer

#Region "私有成员"
    Private _LocationListenSocket As Socket '本地侦听服务   
    Private _ListenPort As String '服务器侦听客户端联接的端口   
    Private _MaxClient As Integer '最大客户端连接数   
    Private _Clients As New SortedList '客户端队列   
    Private _ListenThread As Thread = Nothing '侦听线程   
    Private _ServerStart As Boolean = False '服务器是否已经启动   
    Private _RecvMax As Integer '接收缓冲区大小   
#End Region

#Region "事件"
    ''' <summary>   
    ''' 客户端联接事件   
    ''' </summary>   
    ''' <param name="IP">客户端联接IP</param>   
    ''' <param name="Port">客户端联接端口号</param>   
    ''' <remarks></remarks>   
    Public Event ClientConnected(ByVal IP As String, ByVal Port As String)
    ''' <summary>   
    ''' 客户端断开事件   
    ''' </summary>   
    ''' <param name="IP">客户端联接IP</param>   
    ''' <param name="Port">客户端联接端口号</param>   
    ''' <remarks></remarks>   
    Public Event ClientClose(ByVal IP As String, ByVal Port As String)
    ''' <summary>   
    ''' 接收到客户端的数据   
    ''' </summary>   
    ''' <param name="value">数据</param>   
    ''' <param name="IPAddress">数据来源IP</param>   
    ''' <param name="Port">数据来源端口</param>   
    ''' <remarks></remarks>   
    Public Event DataArrived(ByVal value As Byte(), ByVal Len As Integer, ByVal IPAddress As String, ByVal Port As String)
    ''' <summary>   
    ''' 异常数据   
    ''' </summary>   
    ''' <param name="ex"></param>   
    ''' <remarks></remarks>   
    Public Event Exception(ByVal ex As Exception)

#End Region

#Region "属性"
    ''' <summary>   
    ''' 侦听服务是否已经启动   
    ''' </summary>   
    ''' <value></value>   
    ''' <returns></returns>   
    ''' <remarks></remarks>   
    Public ReadOnly Property IsServerStart() As Boolean
        Get
            Return _ServerStart
        End Get
    End Property
#End Region

#Region "方法"
    ''' <summary>   
    ''' 实例　TCPServer   
    ''' </summary>   
    ''' <param name="Port">侦听客户端联接的端口号</param>   
    ''' <param name="MaxClient">最大可以联接的客户端数量</param>   
    ''' <param name="RecvMax">接收缓冲区大小</param>   
    ''' <param name="RecvSleep">接收线程睡眠时间</param>   
    ''' <remarks></remarks>   
    Sub New(ByVal Port As String, ByVal MaxClient As Integer, ByVal RecvMax As Integer, ByVal RecvSleep As Integer)
        Try
            Dim strHostName As String = Dns.GetHostName()
            _ListenPort = Port
            _MaxClient = MaxClient
            _RecvMax = RecvMax
            Dim strServerHost As New IPEndPoint(IPAddress.Any, Int32.Parse(_ListenPort))
            '建立TCP侦听   
            _LocationListenSocket = New Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp)
            _LocationListenSocket.Bind(strServerHost)
            _LocationListenSocket.Listen(_MaxClient)
            _LocationListenSocket.SetSocketOption(SocketOptionLevel.Tcp, SocketOptionName.AcceptConnection, 1)
        Catch ex As Exception
            RaiseEvent Exception(ex)
        End Try
    End Sub

    ''' <summary>   
    ''' 开始侦听服务   
    ''' </summary>   
    ''' <remarks></remarks>   
    Public Sub StartServer()
        _ServerStart = True
        Try
            _ListenThread = New Thread(New ThreadStart(AddressOf ListenClient))
            _ListenThread.Name = "监听客户端主线程"
            _ListenThread.Start()
        Catch ex As Exception
            If (Not _LocationListenSocket Is Nothing) Then
                If _LocationListenSocket.Connected Then
                    _LocationListenSocket.Close()
                End If
            End If
            RaiseEvent Exception(ex)
        End Try
    End Sub

    ''' <summary>   
    ''' 关闭侦听   
    ''' </summary>   
    ''' <remarks></remarks>   
    Public Sub Close()
        Try
            _ServerStart = False
            'CloseAllClient()   
            Thread.Sleep(5)
            _ListenThread.Abort()
            _LocationListenSocket.Close()
            _ListenThread = Nothing
        Catch ex As Exception
            RaiseEvent Exception(ex)
        End Try
    End Sub

    ''' <summary>   
    ''' 客户端侦听线程   
    ''' </summary>   
    ''' <remarks></remarks>   
    Private Sub ListenClient()
        Dim sKey As String
        While (_ServerStart)
            Try
                If Not _LocationListenSocket Is Nothing Then
                    Dim clientSocket As System.Net.Sockets.Socket
                    clientSocket = New Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp)
                    clientSocket = _LocationListenSocket.Accept()
                    If Not clientSocket Is Nothing Then
                        Dim clientInfoT As IPEndPoint = CType(clientSocket.RemoteEndPoint, IPEndPoint)
                        sKey = clientInfoT.Address.ToString & "\&" & clientInfoT.Port.ToString
                        _Clients.Add(sKey, clientSocket)
                        RaiseEvent ClientConnected(clientInfoT.Address.ToString, clientInfoT.Port.ToString) '举起有客户端联接的事件   
                        '启动客户端接收主线程，开始侦听并接收客户端上传的数据   
                        Dim lb As New ClientCommunication(_LocationListenSocket, clientSocket, Me)
                        AddHandler lb.Exception, AddressOf WriteErrorEvent_ClientCommunication
                        Dim thrClient As New Thread(New ThreadStart(AddressOf lb.serverThreadProc))
                        thrClient.Name = "客户端接收线程,客户端" & clientInfoT.Address.ToString & ":" & clientInfoT.Port.ToString
                        thrClient.Start()
                    End If
                End If
            Catch ex As Exception
                RaiseEvent Exception(ex)
            End Try
        End While
    End Sub

    Private Sub WriteErrorEvent_ClientCommunication(ByVal ex As Exception)
        RaiseEvent Exception(ex)
    End Sub

    Public Sub CloseClient(ByVal IP As String, ByVal Port As String)
        GetClientSocket(IP, Port).Close()
        GetClientClose(IP, Port)
    End Sub
    'Public Sub AlertNoticeClientAll(ByVal DepartmentName As String, ByVal LineName As String, ByVal ErrorCode As Integer)   
    '    '#DepartmentName,LineName,AlertCodeValue.   
    '    ' ''Dim mStr As String   
    '    ' ''mStr = "#" & DepartmentName & "," & LineName & "," & ErrorCode   
    '    ' ''Dim SendByte() As Byte = System.Text.UTF8Encoding.Default.GetBytes(mStr)   
    '    ' ''For Each sc As System.Net.Sockets.Socket In _ClientComputers.Values   
    '    ' ''    sc.Send(SendByte, SendByte.Length(), SocketFlags.None)   
    '    ' ''Next   
    'End Sub   
    Public Sub CloseAllClient()
        For Each sc As System.Net.Sockets.Socket In _Clients.Values
            '断开所有工作站的Socket连接。   
            Dim clientInfoT As IPEndPoint = CType(sc.RemoteEndPoint, IPEndPoint)
            CloseClient(clientInfoT.Address.ToString, clientInfoT.Port.ToString)
        Next
    End Sub

#Region "接收客户端的数据"

    ''' <summary>   
    ''' 接收到客户端的数据-字节数组   
    ''' </summary>   
    ''' <param name="value">数据内容</param>   
    ''' <param name="Len">字节长度</param>   
    ''' <param name="IPAddress">发送该数据的IP地址</param>   
    ''' <param name="Port">发送该数据的端口号</param>   
    ''' <remarks></remarks>   
    Private Sub GetData_Byte(ByVal value As Byte(), ByVal Len As Integer, ByVal IPAddress As String, ByVal Port As String)
        Try
            RaiseEvent DataArrived(value, Len, IPAddress, Port)
            'Catch exx As Sockets.SocketException   
            '    CloseClient(IPAddress, Port)   
        Catch ex As Exception
            RaiseEvent Exception(ex)
        End Try
    End Sub

    ''' <summary>   
    ''' 得到客户端断开或失去客户端联连事件   
    ''' </summary>   
    ''' <param name="IP">客户端联接IP</param>   
    ''' <param name="Port">客户端联接端口号</param>   
    ''' <remarks></remarks>   
    Private Sub GetClientClose(ByVal IP As String, ByVal Port As String)
        Try
            If _Clients.ContainsKey(IP & "\&" & Port) Then
                SyncLock _Clients.SyncRoot
                    '_Clients.Item(IP & "\&" & Port)   
                    _Clients.Remove(IP & "\&" & Port)
                End SyncLock
            End If
            RaiseEvent ClientClose(IP, Port)
        Catch ex As Exception
            RaiseEvent Exception(ex)
        End Try
    End Sub
#End Region

#Region "向客户端发送数据"

    ''' <summary>   
    ''' 向客户端发送信息   
    ''' </summary>   
    ''' <param name="value">发送的内容</param>   
    ''' <param name="IPAddress">IP地址</param>   
    ''' <param name="Port">端口号</param>   
    ''' <returns> Boolean</returns>   
    ''' <remarks></remarks>   
    Public Function SendData(ByVal value As Byte(), ByVal IPAddress As String, ByVal Port As String) As Boolean
        Try
            Dim clientSocket As System.Net.Sockets.Socket
            clientSocket = _Clients.Item(IPAddress & "\&" & Port)
            clientSocket.Send(value, value.Length, SocketFlags.None)
            Return True
        Catch ex As Exception
            RaiseEvent Exception(ex)
            Return False
        End Try
    End Function

    'Public Function SendFile(ByVal value As String, ByVal IPAddress As String, ByVal Port As String) As Boolean
    '    Try
    '        Dim clientSocket As System.Net.Sockets.Socket
    '        clientSocket = _Clients.Item(IPAddress & "\&" & Port)
    '        clientSocket.SendFile(value)
    '        Return True
    '    Catch ex As Exception
    '        RaiseEvent Exception(ex)
    '        Return False
    '    End Try
    'End Function

    'Public Function SendDataToAllClient(ByVal value As Byte()) As Boolean
    '    Try
    '        For Each clientSocket As System.Net.Sockets.Socket In _Clients.Values
    '            clientSocket.Send(value, value.Length, SocketFlags.None)
    '        Next
    '        Return True
    '    Catch ex As Exception
    '        RaiseEvent Exception(ex)
    '        Return False
    '    End Try
    'End Function

#End Region

    ''' <summary>   
    ''' 得到客户端的Socket联接   
    ''' </summary>   
    ''' <param name="IPAddress">客户端的IP</param>   
    ''' <param name="Port">客户端的端口号</param>   
    ''' <returns>Socket联接</returns>   
    ''' <remarks></remarks>   
    Private Function GetClientSocket(ByVal IPAddress As String, ByVal Port As String) As Socket
        Try
            Dim ClientSocket As Socket
            ClientSocket = _Clients.Item(IPAddress & "\&" & Port)
            Return ClientSocket
        Catch ex As Exception
            RaiseEvent Exception(ex)
            Return Nothing
        End Try
    End Function
#End Region

    Private Class ClientCommunication
        Public Event Exception(ByVal ex As Exception)

        Private ServerSocket As New System.Net.Sockets.Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp)
        Private myClientSocket As New System.Net.Sockets.Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp)
        Private myParentObject As TCPServer
        Private oldbytes() As Byte
        Private _IPAddress, _Port As String
        Private NclientInfoT As IPEndPoint = Nothing
        Private iLen As Integer
        Private allDone As New ManualResetEvent(False)
        ''' <summary>   
        ''' 实例ClientCommunication类   
        ''' </summary>   
        ''' <param name="ServerSocket"></param>   
        ''' <param name="ClientSocket"></param>   
        ''' <param name="ParentObject"></param>   
        ''' <remarks></remarks>   
        Public Sub New(ByVal ServerSocket As Socket, ByVal ClientSocket As Socket, ByVal ParentObject As TCPServer)
            Me.ServerSocket = ServerSocket
            myClientSocket = ClientSocket
            myParentObject = ParentObject
            NclientInfoT = CType(myClientSocket.RemoteEndPoint, IPEndPoint)
            _IPAddress = NclientInfoT.Address.ToString
            _Port = NclientInfoT.Port.ToString
        End Sub

        ''' <summary>   
        ''' 客户端通讯主线程   
        ''' </summary>   
        ''' <remarks></remarks>   
        Public Sub serverThreadProc()
            Try
                Dim sb As New SocketAndBuffer
                sb.Socket = myClientSocket
                sb.Socket.BeginReceive(sb.Buffer, 0, sb.Buffer.Length, SocketFlags.None, AddressOf ReceiveCallBack, sb)
                'allDone.WaitOne()   
            Catch ex As Exception
                RaiseEvent Exception(ex)
            End Try
        End Sub

        ''' <summary>   
        ''' socket异步接收回调函数   
        ''' </summary>   
        ''' <param name="ar"></param>   
        ''' <remarks></remarks>   
        Private Sub ReceiveCallBack(ByVal ar As IAsyncResult)
            Dim sb As SocketAndBuffer
            allDone.Set()
            sb = CType(ar.AsyncState, SocketAndBuffer)
            Try
                If sb.Socket.Connected Then
                    iLen = sb.Socket.EndReceive(ar)
                    If iLen > 0 Then
                        ReDim oldbytes(iLen - 1)
                        Array.Copy(sb.Buffer, 0, oldbytes, 0, iLen)
                        myParentObject.GetData_Byte(oldbytes, oldbytes.Length, _IPAddress, _Port)
                        sb.Socket.BeginReceive(sb.Buffer, 0, sb.Buffer.Length, SocketFlags.None, AddressOf ReceiveCallBack, sb)
                    Else
                        If (Not myClientSocket Is Nothing) Then
                            If myClientSocket.Connected Then
                                myClientSocket.Close()
                            Else
                                myClientSocket.Close()
                            End If
                            myClientSocket = Nothing
                            If Not NclientInfoT Is Nothing Then
                                myParentObject._Clients.Remove(_IPAddress & "\&" & _Port)
                                myParentObject.GetClientClose(_IPAddress, _Port)
                            End If
                        End If
                    End If
                End If
            Catch ex As Exception
                If (Not myClientSocket Is Nothing) Then
                    If myClientSocket.Connected Then
                        myClientSocket.Close()
                    Else
                        myClientSocket.Close()
                    End If
                    myClientSocket = Nothing
                    If Not NclientInfoT Is Nothing Then
                        myParentObject._Clients.Remove(_IPAddress & "\&" & _Port)
                        myParentObject.GetClientClose(_IPAddress, _Port)
                    End If
                End If
                RaiseEvent Exception(ex)
            End Try
        End Sub

        ''' <summary>   
        ''' 异步操作socket缓冲类   
        ''' </summary>   
        ''' <remarks></remarks>   
        Private Class SocketAndBuffer
            Public Socket As System.Net.Sockets.Socket
            Public Buffer(8192) As Byte
        End Class
    End Class


End Class