<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Form_Client
    Inherits System.Windows.Forms.Form

    'Form 重写 Dispose，以清理组件列表。
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Windows 窗体设计器所必需的
    Private components As System.ComponentModel.IContainer

    '注意: 以下过程是 Windows 窗体设计器所必需的
    '可以使用 Windows 窗体设计器修改它。
    '不要使用代码编辑器修改它。
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.Button_Control = New System.Windows.Forms.Button()
        Me.TextBox_IP = New System.Windows.Forms.TextBox()
        Me.TextBox_Port = New System.Windows.Forms.TextBox()
        Me.Label_IP = New System.Windows.Forms.Label()
        Me.Label_Port = New System.Windows.Forms.Label()
        Me.Label_ConnectStatus = New System.Windows.Forms.Label()
        Me.ListBox_Received = New System.Windows.Forms.ListBox()
        Me.ListBox_Sent = New System.Windows.Forms.ListBox()
        Me.Button_ClearReceivedList = New System.Windows.Forms.Button()
        Me.Button_ClearSentList = New System.Windows.Forms.Button()
        Me.Button_Send = New System.Windows.Forms.Button()
        Me.TextBox_Send = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.SuspendLayout()
        '
        'Button_Control
        '
        Me.Button_Control.BackColor = System.Drawing.Color.Green
        Me.Button_Control.Font = New System.Drawing.Font("Source Code Pro", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button_Control.ForeColor = System.Drawing.Color.White
        Me.Button_Control.Location = New System.Drawing.Point(421, 8)
        Me.Button_Control.Name = "Button_Control"
        Me.Button_Control.Size = New System.Drawing.Size(75, 38)
        Me.Button_Control.TabIndex = 7
        Me.Button_Control.Text = "启动"
        Me.Button_Control.UseVisualStyleBackColor = False
        '
        'TextBox_IP
        '
        Me.TextBox_IP.Font = New System.Drawing.Font("宋体", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.TextBox_IP.Location = New System.Drawing.Point(53, 13)
        Me.TextBox_IP.Name = "TextBox_IP"
        Me.TextBox_IP.Size = New System.Drawing.Size(197, 30)
        Me.TextBox_IP.TabIndex = 6
        Me.TextBox_IP.Text = "127.0.0.1"
        '
        'TextBox_Port
        '
        Me.TextBox_Port.Font = New System.Drawing.Font("宋体", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.TextBox_Port.Location = New System.Drawing.Point(321, 13)
        Me.TextBox_Port.Name = "TextBox_Port"
        Me.TextBox_Port.Size = New System.Drawing.Size(94, 30)
        Me.TextBox_Port.TabIndex = 5
        Me.TextBox_Port.Text = "8000"
        '
        'Label_IP
        '
        Me.Label_IP.AutoSize = True
        Me.Label_IP.Font = New System.Drawing.Font("黑体", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label_IP.Location = New System.Drawing.Point(8, 16)
        Me.Label_IP.Name = "Label_IP"
        Me.Label_IP.Size = New System.Drawing.Size(39, 20)
        Me.Label_IP.TabIndex = 3
        Me.Label_IP.Text = "IP:"
        '
        'Label_Port
        '
        Me.Label_Port.AutoSize = True
        Me.Label_Port.Font = New System.Drawing.Font("黑体", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label_Port.Location = New System.Drawing.Point(256, 16)
        Me.Label_Port.Name = "Label_Port"
        Me.Label_Port.Size = New System.Drawing.Size(69, 20)
        Me.Label_Port.TabIndex = 4
        Me.Label_Port.Text = "Port :"
        '
        'Label_ConnectStatus
        '
        Me.Label_ConnectStatus.AutoSize = True
        Me.Label_ConnectStatus.ForeColor = System.Drawing.Color.Red
        Me.Label_ConnectStatus.Location = New System.Drawing.Point(9, 629)
        Me.Label_ConnectStatus.Name = "Label_ConnectStatus"
        Me.Label_ConnectStatus.Size = New System.Drawing.Size(82, 15)
        Me.Label_ConnectStatus.TabIndex = 8
        Me.Label_ConnectStatus.Text = "未启动服务"
        '
        'ListBox_Received
        '
        Me.ListBox_Received.FormattingEnabled = True
        Me.ListBox_Received.ItemHeight = 15
        Me.ListBox_Received.Location = New System.Drawing.Point(12, 412)
        Me.ListBox_Received.Name = "ListBox_Received"
        Me.ListBox_Received.Size = New System.Drawing.Size(403, 214)
        Me.ListBox_Received.TabIndex = 13
        '
        'ListBox_Sent
        '
        Me.ListBox_Sent.FormattingEnabled = True
        Me.ListBox_Sent.ItemHeight = 15
        Me.ListBox_Sent.Location = New System.Drawing.Point(12, 192)
        Me.ListBox_Sent.Name = "ListBox_Sent"
        Me.ListBox_Sent.Size = New System.Drawing.Size(403, 214)
        Me.ListBox_Sent.TabIndex = 14
        '
        'Button_ClearReceivedList
        '
        Me.Button_ClearReceivedList.Location = New System.Drawing.Point(421, 588)
        Me.Button_ClearReceivedList.Name = "Button_ClearReceivedList"
        Me.Button_ClearReceivedList.Size = New System.Drawing.Size(75, 38)
        Me.Button_ClearReceivedList.TabIndex = 12
        Me.Button_ClearReceivedList.Text = "清空"
        Me.Button_ClearReceivedList.UseVisualStyleBackColor = True
        '
        'Button_ClearSentList
        '
        Me.Button_ClearSentList.Location = New System.Drawing.Point(421, 368)
        Me.Button_ClearSentList.Name = "Button_ClearSentList"
        Me.Button_ClearSentList.Size = New System.Drawing.Size(75, 38)
        Me.Button_ClearSentList.TabIndex = 10
        Me.Button_ClearSentList.Text = "清空"
        Me.Button_ClearSentList.UseVisualStyleBackColor = True
        '
        'Button_Send
        '
        Me.Button_Send.Location = New System.Drawing.Point(421, 161)
        Me.Button_Send.Name = "Button_Send"
        Me.Button_Send.Size = New System.Drawing.Size(75, 25)
        Me.Button_Send.TabIndex = 11
        Me.Button_Send.Text = "发送"
        Me.Button_Send.UseVisualStyleBackColor = True
        '
        'TextBox_Send
        '
        Me.TextBox_Send.Location = New System.Drawing.Point(12, 161)
        Me.TextBox_Send.Name = "TextBox_Send"
        Me.TextBox_Send.Size = New System.Drawing.Size(403, 25)
        Me.TextBox_Send.TabIndex = 9
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(12, 143)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(142, 15)
        Me.Label1.TabIndex = 15
        Me.Label1.Text = "请输入待发送数据："
        '
        'Form_Client
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 15.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(504, 653)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.ListBox_Received)
        Me.Controls.Add(Me.ListBox_Sent)
        Me.Controls.Add(Me.Button_ClearReceivedList)
        Me.Controls.Add(Me.Button_ClearSentList)
        Me.Controls.Add(Me.Button_Send)
        Me.Controls.Add(Me.TextBox_Send)
        Me.Controls.Add(Me.Label_ConnectStatus)
        Me.Controls.Add(Me.Button_Control)
        Me.Controls.Add(Me.TextBox_IP)
        Me.Controls.Add(Me.TextBox_Port)
        Me.Controls.Add(Me.Label_IP)
        Me.Controls.Add(Me.Label_Port)
        Me.Name = "Form_Client"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "客户端"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Button_Control As System.Windows.Forms.Button
    Friend WithEvents TextBox_IP As System.Windows.Forms.TextBox
    Friend WithEvents TextBox_Port As System.Windows.Forms.TextBox
    Friend WithEvents Label_IP As System.Windows.Forms.Label
    Friend WithEvents Label_Port As System.Windows.Forms.Label
    Friend WithEvents Label_ConnectStatus As System.Windows.Forms.Label
    Friend WithEvents ListBox_Received As System.Windows.Forms.ListBox
    Friend WithEvents ListBox_Sent As System.Windows.Forms.ListBox
    Friend WithEvents Button_ClearReceivedList As System.Windows.Forms.Button
    Friend WithEvents Button_ClearSentList As System.Windows.Forms.Button
    Friend WithEvents Button_Send As System.Windows.Forms.Button
    Friend WithEvents TextBox_Send As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
End Class
