Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports System.Threading.Tasks
Imports System.Net
Imports System.Net.Sockets
Imports System.Threading


Public Class Form_Client

#Region "定义变量"

    'socket也是一个类,位于System.Net.Sockets下面

    '1:创建一个socket
    Dim tcpClient As Socket

    '2:定义IP地址和端口号
    Dim mIP As IPAddress
    Dim mPort As Integer
    Dim mPoint As EndPoint

    '监听线程
    Dim thThreadListen As Thread

    '是否循环接收
    Dim Boolean_Client As Boolean = True

    '进行任务委托（把其他线程运行出的一些值赋值到主线程控件上）
    Public Delegate Sub DelegateSub_Data(ByVal DATA As String)
    Public Delegate Sub DelegateSub_Status(ByVal TEXT As String, ByVal COLOR As Color)

#End Region

#Region "窗体事件"

    '窗体关闭
    Private Sub Form_Server_Closing(sender As Object, e As EventArgs) Handles MyBase.FormClosing
        Try
            tcpClient.Close()
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
            Boolean_Client = True

            '打开监听线程
            thThreadListen = New Thread(New ThreadStart(AddressOf Listen))
            thThreadListen.Start()

            Button_Control.Text = "停止"
            Button_Control.BackColor = Color.Red

        ElseIf Button_Control.Text = "停止" Then
            Try
                Boolean_Client = False
                Thread.Sleep(10)
                '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
                '关闭Socket服务
                tcpClient.Close()
                tcpClient = Nothing

                '关闭监听线程
                thThreadListen.Abort()
                '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
                Button_Control.Text = "启动"
                Button_Control.BackColor = Color.Green

                ConnectStatus("未启动服务", Color.Red)
            Catch ex As Exception

            End Try

        End If
    End Sub

    '发送 按钮事件
    Private Sub Button_Send_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button_Send.Click
        Try
            '向服务器端发送消息            
            Dim SendStr As String = TextBox_Send.Text.ToString

            Dim SendByte As Byte() = Encoding.UTF8.GetBytes(SendStr) '利用这个方法将string型转化为byte型数组
            tcpClient.Send(SendByte) '向服务器端发送数据
            SentList(SendStr)
        Catch
            MsgBox("发送失败，请检查是否连接上服务器！", MsgBoxStyle.OkOnly)
        End Try

        TextBox_Send.Text = Nothing

    End Sub

    '清空已发送数据
    Private Sub Button_ClearSentList_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button_ClearSentList.Click
        ListBox_Sent.Items.Clear()
    End Sub

    '清空已接收数据
    Private Sub Button_ClearReceivedList_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button_ClearReceivedList.Click
        ListBox_Received.Items.Clear()
    End Sub

#End Region

#Region "状态/数据显示"

    '显示已发送数据
    Private Sub SentList(ByVal SentStr As String)
        ListBox_Sent.Items.Add("向客户端发送消息:" + SentStr)
    End Sub

    '显示已接受数据
    Private Sub ReceivedList(ByVal ReceivedStr As String)
        ListBox_Received.Items.Add("收到客户端的消息:" + ReceivedStr)
    End Sub

    '显示连接状态
    Private Sub ConnectStatus(ByVal Status As String, ByVal StatusColor As Color)
        Label_ConnectStatus.Text = Status
        Label_ConnectStatus.ForeColor = StatusColor
    End Sub

#End Region

#Region "监听函数"

    '监听函数
    Private Sub Listen()
        Try
            ''1:创建socket
            tcpClient = New Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp)

            ''2:向服务器端发送连接请求
            mIP = IPAddress.Parse(TextBox_IP.Text) ''IPAddress.Parse可以把string类型的ip地址转化为ipAddress型
            mPort = CInt(TextBox_Port.Text)
            mPoint = New IPEndPoint(mIP, mPort) ''通过ip地址和端口号定位要连接的服务器端
            'Invoke(New DelegateSub_Status(AddressOf ConnectStatus), "请先开启服务器...", Color.Orange)

            '建立连接
            tcpClient.Connect(mPoint)
            Invoke(New DelegateSub_Status(AddressOf ConnectStatus), "连接到服务器", Color.Green)

            Try
                While Boolean_Client
                    ''3:接收服务器端发来的消息
                    Dim data As Byte() = New Byte(10000) {}
                    Dim length As Integer = tcpClient.Receive(data) ''这里的byte数组用来接收数据,返回值length表示接收的数据长度
                    Dim receiveMessage As String = Encoding.UTF8.GetString(data, 0, length) ''把字节数组转化为字符串
                    If receiveMessage <> "" Then
                        Invoke(New DelegateSub_Data(AddressOf ReceivedList), receiveMessage)
                    Else
                        Invoke(New DelegateSub_Status(AddressOf ConnectStatus), "客户端掉线，请检查并重启服务器！", Color.Black)
                        Boolean_Client = False
                        Exit While
                    End If
                End While
                thThreadListen.Abort()
            Catch
                'MessageBox.Show(ex.ToString())
            End Try

            tcpClient.Close()
            tcpClient = Nothing

            thThreadListen.Abort()

        Catch
            'MessageBox.Show(ex.ToString())
        End Try
    End Sub
#End Region

End Class