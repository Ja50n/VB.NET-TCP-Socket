<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Form_Server
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
        Me.TextBox_Port = New System.Windows.Forms.TextBox()
        Me.Button_Control = New System.Windows.Forms.Button()
        Me.TextBox_Send = New System.Windows.Forms.TextBox()
        Me.Button_Send = New System.Windows.Forms.Button()
        Me.ListBox_Sent = New System.Windows.Forms.ListBox()
        Me.ListBox_Received = New System.Windows.Forms.ListBox()
        Me.Label_ConnectStatus = New System.Windows.Forms.Label()
        Me.Button_ClearSentList = New System.Windows.Forms.Button()
        Me.Button_ClearReceivedList = New System.Windows.Forms.Button()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.SuspendLayout()
        '
        'TextBox_Port
        '
        Me.TextBox_Port.Font = New System.Drawing.Font("宋体", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.TextBox_Port.Location = New System.Drawing.Point(87, 13)
        Me.TextBox_Port.Name = "TextBox_Port"
        Me.TextBox_Port.Size = New System.Drawing.Size(328, 30)
        Me.TextBox_Port.TabIndex = 1
        Me.TextBox_Port.Text = "8000"
        '
        'Button_Control
        '
        Me.Button_Control.BackColor = System.Drawing.Color.Green
        Me.Button_Control.Font = New System.Drawing.Font("Source Code Pro", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button_Control.ForeColor = System.Drawing.Color.White
        Me.Button_Control.Location = New System.Drawing.Point(421, 8)
        Me.Button_Control.Name = "Button_Control"
        Me.Button_Control.Size = New System.Drawing.Size(75, 38)
        Me.Button_Control.TabIndex = 2
        Me.Button_Control.Text = "启动"
        Me.Button_Control.UseVisualStyleBackColor = False
        '
        'TextBox_Send
        '
        Me.TextBox_Send.Location = New System.Drawing.Point(12, 162)
        Me.TextBox_Send.Name = "TextBox_Send"
        Me.TextBox_Send.Size = New System.Drawing.Size(403, 25)
        Me.TextBox_Send.TabIndex = 1
        '
        'Button_Send
        '
        Me.Button_Send.Location = New System.Drawing.Point(421, 162)
        Me.Button_Send.Name = "Button_Send"
        Me.Button_Send.Size = New System.Drawing.Size(75, 25)
        Me.Button_Send.TabIndex = 2
        Me.Button_Send.Text = "发送"
        Me.Button_Send.UseVisualStyleBackColor = True
        '
        'ListBox_Sent
        '
        Me.ListBox_Sent.FormattingEnabled = True
        Me.ListBox_Sent.ItemHeight = 15
        Me.ListBox_Sent.Location = New System.Drawing.Point(12, 193)
        Me.ListBox_Sent.Name = "ListBox_Sent"
        Me.ListBox_Sent.Size = New System.Drawing.Size(403, 214)
        Me.ListBox_Sent.TabIndex = 3
        '
        'ListBox_Received
        '
        Me.ListBox_Received.FormattingEnabled = True
        Me.ListBox_Received.ItemHeight = 15
        Me.ListBox_Received.Location = New System.Drawing.Point(12, 413)
        Me.ListBox_Received.Name = "ListBox_Received"
        Me.ListBox_Received.Size = New System.Drawing.Size(403, 214)
        Me.ListBox_Received.TabIndex = 3
        '
        'Label_ConnectStatus
        '
        Me.Label_ConnectStatus.AutoSize = True
        Me.Label_ConnectStatus.ForeColor = System.Drawing.Color.Red
        Me.Label_ConnectStatus.Location = New System.Drawing.Point(12, 630)
        Me.Label_ConnectStatus.Name = "Label_ConnectStatus"
        Me.Label_ConnectStatus.Size = New System.Drawing.Size(82, 15)
        Me.Label_ConnectStatus.TabIndex = 0
        Me.Label_ConnectStatus.Text = "未启动服务"
        '
        'Button_ClearSentList
        '
        Me.Button_ClearSentList.Location = New System.Drawing.Point(421, 369)
        Me.Button_ClearSentList.Name = "Button_ClearSentList"
        Me.Button_ClearSentList.Size = New System.Drawing.Size(75, 38)
        Me.Button_ClearSentList.TabIndex = 2
        Me.Button_ClearSentList.Text = "清空"
        Me.Button_ClearSentList.UseVisualStyleBackColor = True
        '
        'Button_ClearReceivedList
        '
        Me.Button_ClearReceivedList.Location = New System.Drawing.Point(421, 589)
        Me.Button_ClearReceivedList.Name = "Button_ClearReceivedList"
        Me.Button_ClearReceivedList.Size = New System.Drawing.Size(75, 38)
        Me.Button_ClearReceivedList.TabIndex = 2
        Me.Button_ClearReceivedList.Text = "清空"
        Me.Button_ClearReceivedList.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("黑体", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(12, 16)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(69, 20)
        Me.Label1.TabIndex = 5
        Me.Label1.Text = "Port :"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(9, 144)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(142, 15)
        Me.Label2.TabIndex = 16
        Me.Label2.Text = "请输入待发送数据："
        '
        'Form_Server
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 15.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(504, 653)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.ListBox_Received)
        Me.Controls.Add(Me.ListBox_Sent)
        Me.Controls.Add(Me.Button_ClearReceivedList)
        Me.Controls.Add(Me.Button_ClearSentList)
        Me.Controls.Add(Me.Button_Send)
        Me.Controls.Add(Me.Button_Control)
        Me.Controls.Add(Me.TextBox_Port)
        Me.Controls.Add(Me.Label_ConnectStatus)
        Me.Controls.Add(Me.TextBox_Send)
        Me.Name = "Form_Server"
        Me.Text = "服务器"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents TextBox_Port As System.Windows.Forms.TextBox
    Friend WithEvents Button_Control As System.Windows.Forms.Button
    Friend WithEvents TextBox_Send As System.Windows.Forms.TextBox
    Friend WithEvents Button_Send As System.Windows.Forms.Button
    Friend WithEvents ListBox_Sent As System.Windows.Forms.ListBox
    Friend WithEvents ListBox_Received As System.Windows.Forms.ListBox
    Friend WithEvents Label_ConnectStatus As System.Windows.Forms.Label
    Friend WithEvents Button_ClearSentList As System.Windows.Forms.Button
    Friend WithEvents Button_ClearReceivedList As System.Windows.Forms.Button
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label

End Class
