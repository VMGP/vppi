﻿Imports System.Threading
Imports System.Windows.Forms

Public Class WindowUIManager
    WithEvents windowhandle As System.Windows.Forms.Form
    Public nt1 As Thread
    Public t As Thread

    Public formready = False
    Public didactivate = False

    Public Delegate Sub drawdelegate(startx As Integer, starty As Integer, endx As Integer, endy As Integer, dcolor As Drawing.Color, dpensize As Integer)
    Public Delegate Sub textdelegate(startx As Integer, starty As Integer, dcolor As Drawing.Color, fontsize As Integer, fontfamily As String, textval As String)
    Public Delegate Sub cleardelegate(dcolor As Drawing.Color)

    Public mousex As Integer
    Public mousey As Integer

    Public ce_mx As Integer = 0
    Public ce_my As Integer = 0
    Public ce_mb As Integer = 0
    Public cmei As Boolean = False
    Public cei As Boolean = False

    Public kdi As String = ""
    Public kdii As Boolean = False

    Dim wsz As Drawing.Size
    Dim wt As String
    Dim wic As Drawing.Icon

    Sub New(windowsize As Drawing.Size, windowtitle As String, winicon As Drawing.Icon)
        wsz = windowsize
        wt = windowtitle
        wic = winicon
        Dim ts1 As New ThreadStart(AddressOf newform)
        nt1 = New Thread(ts1)
        nt1.Name = "GUIWindowInit"
        nt1.Start()
    End Sub

    Private Sub newform()
        If didactivate = False Then
            If t Is Nothing Then

            Else
                Exit Sub
            End If

            windowhandle = New System.Windows.Forms.Form()
            windowhandle.Size = wsz
            windowhandle.Text = wt
            windowhandle.Icon = wic
            windowhandle.MaximizeBox = False
            windowhandle.FormBorderStyle = Windows.Forms.FormBorderStyle.FixedDialog

            initform()

            While formready = False

            End While
        End If
    End Sub

    Private Sub initform()
        didactivate = True
        windowhandle.Activate()
        windowhandle.ShowDialog()
        windowhandle.Enabled = True
        windowhandle.Focus()
        windowhandle.Visible = True
    End Sub

    Private Sub _form_drawtext(startx As Integer, starty As Integer, dcolor As Drawing.Color, fontsize As Integer, fontfamily As String, textval As String)
        windowhandle.CreateGraphics.DrawString(textval, New Drawing.Font(fontfamily, fontsize), New Drawing.SolidBrush(dcolor), startx, starty)
    End Sub

    Private Sub _form_drawline(startx As Integer, starty As Integer, endx As Integer, endy As Integer, dcolor As Drawing.Color, dpensize As Integer)
        windowhandle.CreateGraphics.DrawLine(New Drawing.Pen(dcolor, dpensize), startx, starty, endx, endy)
    End Sub

    Private Sub _form_clear(dcolor As Drawing.Color)
        windowhandle.CreateGraphics.Clear(dcolor)
    End Sub

    Public Sub drawline(startx As Integer, starty As Integer, endx As Integer, endy As Integer, dcolor As Drawing.Color, dpensize As Integer)
        If windowhandle Is Nothing Then
            Exit Sub
        End If
        windowhandle.Invoke(New drawdelegate(AddressOf _form_drawline), startx, starty, endx, endy, dcolor, dpensize)
    End Sub

    Public Sub clear(dcolor As Drawing.Color)
        If windowhandle Is Nothing Then
            Exit Sub
        End If
        windowhandle.Invoke(New cleardelegate(AddressOf _form_clear), dcolor)
    End Sub

    Public Sub drawtext(startx As Integer, starty As Integer, dcolor As Drawing.Color, fontsize As Integer, fontfamily As String, textval As String)
        If windowhandle Is Nothing Then
            Exit Sub
        End If
        windowhandle.Invoke(New textdelegate(AddressOf _form_drawtext), startx, starty, dcolor, fontsize, fontfamily, textval)
    End Sub

    Private Sub windowhandle_Shown(sender As Object, e As EventArgs) Handles windowhandle.Shown
        formready = True
    End Sub

    Private Sub windowhandle_MouseMove(sender As Object, e As MouseEventArgs) Handles windowhandle.MouseMove
        mousex = e.X
        mousey = e.Y
        cmei = True
    End Sub

    Private Sub windowhandle_MouseClick(sender As Object, e As MouseEventArgs) Handles windowhandle.MouseClick
        ce_mx = e.X
        ce_my = e.Y
        ce_mb = Convert.ToDecimal(e.Button)
        cei = True
    End Sub

    Private Sub windowhandle_KeyDown(sender As Object, e As KeyEventArgs) Handles windowhandle.KeyDown
        kdi = e.KeyCode.ToString()
        kdii = True
    End Sub
End Class

