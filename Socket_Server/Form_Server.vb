Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports System.Threading.Tasks
Imports System.Net
Imports System.Net.Sockets
Imports System.Threading


Public Class Form_Server

#Region "定义变量"

    'socket也是一个类,位于System.Net.Sockets下面

    '1:创建一个socket
    Dim tcpServer As Socket
    Dim ClientSocket As Socket '定义返回客户端的socket

    '2:定义IP地址和端口号
    Dim mIP As IPAddress '需要指定时使用
    Dim mPort As Integer
    Dim mPoint As EndPoint 'IPEndPoint类是对ip端口做了一层封装的类

    '监听线程
    Dim thThreadListen As Thread

    '是否循环接收
    Dim Boolean_Server As Boolean = False

    '进行任务委托（把其他线程运行出的一些值赋值到主线程控件上）
    Public Delegate Sub DelegateSub_Data(ByVal DATA As String)
    Public Delegate Sub DelegateSub_Status(ByVal TEXT As String, ByVal COLOR As Color)

#End Region

#Region "窗体事件"

    '窗体关闭
    Private Sub Form_Server_Closing(sender As Object, e As EventArgs) Handles MyBase.FormClosing
        Try
            ClientSocket.Close()
        Catch ex As Exception

        End Try
        Try
            tcpServer.Close()
        Catch ex As Exception

        End Try
        Try
            thThreadListen.Abort()
        Catch ex As Exception

        End Try
    End Sub

    '启动/停止 按钮事件
    Private Sub Button_Control_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button_Control.Click

        If Button_Control.Text = "启动" Then
            Boolean_Server = True

            '开始监听线程
            thThreadListen = New Thread(New ThreadStart(AddressOf Listen))
            thThreadListen.Start()

            Button_Control.Text = "停止"
            Button_Control.BackColor = Color.Red

        ElseIf Button_Control.Text = "停止" Then
            Try
                Boolean_Server = False
                '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
                '关闭Socket服务
                Thread.Sleep(10)
                If Label_ConnectStatus.Text = "已接入客户端" Then
                    ClientSocket.Close()
                End If
                'If Label_ConnectStatus.Text = "开始监听" Then
                tcpServer.Close()
                'End If
                ClientSocket = Nothing

                '关闭监听线程
                thThreadListen.Abort()
                '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
            Catch ex As Exception

            End Try

            Button_Control.Text = "启动"
            Button_Control.BackColor = Color.Green

            ConnectStatus("未启动服务", Color.Red)
        End If

    End Sub

    '发送 按钮事件
    Private Sub Button_Send_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button_Send.Click
        Try

            Dim SendStr As String = TextBox_Send.Text.ToString

            Dim SendByte As Byte() = Encoding.UTF8.GetBytes(SendStr) '利用这个方法将string型转化为byte型数组
            'Dim ClientSocket As Socket = tcpServer.Accept() '暂停当前线程知道有客户端连接进来才进行下面的代码,返回客户端的socket
            ClientSocket.Send(SendByte) '向客户端发送数据
            SentList(SendStr)
        Catch
            MsgBox("发送失败，请检查是否有客户端连接！", MsgBoxStyle.OkOnly)
        End Try

        TextBox_Send.Text = Nothing

    End Sub

    '发送文本框,聚焦按键
    Private Sub TextBox_Send_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles TextBox_Send.GotFocus
        If Label_ConnectStatus.Text = "已接入客户端" Then
            Me.AcceptButton = Button_Send
        ElseIf Label_ConnectStatus.Text = "监听中..." Then
            Me.AcceptButton = Nothing
        ElseIf Label_ConnectStatus.Text = "未启动服务" Then
            Me.AcceptButton = Button_Control
        End If
    End Sub

    '清空已发送列表
    Private Sub Button_ClearSentList_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button_ClearSentList.Click
        ListBox_Sent.Items.Clear()
    End Sub

    '清空已接受列表
    Private Sub Button_ClearReceivedList_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button_ClearReceivedList.Click
        ListBox_Received.Items.Clear()
    End Sub

#End Region

#Region "状态/数据显示"

    '显示连接状态
    Private Sub ConnectStatus(ByVal Status As String, ByVal StatusColor As Color)
        Label_ConnectStatus.Text = Status
        Label_ConnectStatus.ForeColor = StatusColor
    End Sub

    '显示已发送数据
    Private Sub SentList(ByVal SentStr As String)
        ListBox_Sent.Items.Add("向客户端发送消息:" + SentStr)
    End Sub

    '显示已接受数据
    Private Sub ReceivedList(ByVal ReceivedStr As String)
        ListBox_Received.Items.Add("收到客户端的消息:" + ReceivedStr)

        '将受到的消息以特定分隔符分组
        Dim StrArr() As String = Split(ReceivedStr, ",")
        For i As Integer = 0 To StrArr.Length - 1
            ListBox_Received.Items.Add("收到客户端的消息分组后:" + StrArr(i))
        Next
    End Sub

#End Region

#Region "监听函数"

    '监听函数
    Private Sub Listen()
        Try
            '向操作系统申请一个可用的ip地址和端口号用于通信
            'mIP = IPAddress.Parse(TextBox_IP.Text)
            mIP = IPAddress.Any
            mPort = CInt(TextBox_Port.Text)
            mPoint = New IPEndPoint(mIP, mPort) 'IPEndPoint类是对ip端口做了一层封装的类

            '当客户端掉线，跳转到此步骤，重新建立一个连接
label_ReConnect:
            If Label_ConnectStatus.Text = "已接入客户端" Then
                ClientSocket.Close()
                ClientSocket = Nothing
                tcpServer.Close()
            End If


            tcpServer = New Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp)
            tcpServer.Bind(mPoint)

            '3:开始监听(等待客户端的连接)
            Invoke(New DelegateSub_Status(AddressOf ConnectStatus), "监听中...", Color.Orange)
            tcpServer.Listen(100) '设置最大的连接数

            'Dim ClientSocket As Socket
            ClientSocket = tcpServer.Accept() '暂停当前线程知道有客户端连接进来才进行下面的代码,返回客户端的socket
            Invoke(New DelegateSub_Status(AddressOf ConnectStatus), "已接入客户端", Color.Green)

            '4：使用返回的socket向客户端发送消息
            Dim str As Byte() = Encoding.UTF8.GetBytes("Welcome to you") '利用这个方法将string型转化为byte型数组
            ClientSocket.Send(str) '向客户端发送欢迎信息
            Invoke(New DelegateSub_Data(AddressOf SentList), "Welcome to you")

            Try
                While Boolean_Server
                    '5:接收客户端发来的消息
                    Dim data As Byte() = New Byte(10000) {}
                    Dim length As Integer = ClientSocket.Receive(data) '这里的byte数组用来接收数据,返回值length表示接收的数据长度
                    Dim receiveMessage As String = Encoding.UTF8.GetString(data, 0, length) '把字节数组转化为字符串
                    If receiveMessage <> "" Then
                        Invoke(New DelegateSub_Data(AddressOf ReceivedList), receiveMessage)
                    Else
                        'Invoke(New DelegateSub_Status(AddressOf ConnectStatus), "客户端掉线，请检查并重启服务器！", Color.Black)
                        'Boolean_Server = False
                        'Exit While
                        GoTo label_ReConnect
                    End If
                End While
            Catch ex As Exception
                'MessageBox.Show(ex.ToString())
            End Try

            ClientSocket.Close()
            tcpServer.Close()
            ClientSocket = Nothing
            thThreadListen.Abort()

        Catch ex As Exception
            'MessageBox.Show(ex.ToString())
        End Try
    End Sub

#End Region

End Class
