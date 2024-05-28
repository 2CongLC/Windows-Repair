Imports System
Imports System.Collections
Imports System.Diagnostics
Imports System.IO
Imports System.IO.Compression
Imports System.Management
Imports System.Net
Imports System.Net.Sockets
Imports System.Text
Imports System.Text.RegularExpressions
Imports System.Threading
Public Class WinExt
#Region "Lấy thông tin máy tính"
    Public Structure PCInfo
        Shared Username As String = Environment.UserName
        Shared PCname As String = Environment.MachineName
        Shared Domain As String = My.User.Name
        Shared IsAuth As Boolean = My.User.IsAuthenticated
        Shared OSname As String = My.Computer.Info.OSFullName
        Shared OSversion As String = My.Computer.Info.OSVersion
        Shared Is64Bit As String = Environment.Is64BitOperatingSystem
        Shared SystemDir As String = Environment.SystemDirectory
        Shared Function GetCPUID() As String
            Dim result As String = ""
            Dim mos As ManagementObjectSearcher = New ManagementObjectSearcher("Select * FROM Win32_Processor")
            For Each i As ManagementObject In mos.Get()
                result = i.GetPropertyValue("ProcessorId").ToString
            Next
            Return result
        End Function
        Shared Function GetHDDID() As String
            Dim result As String = ""
            Dim mos As ManagementObjectSearcher = New ManagementObjectSearcher("SELECT * FROM Win32_PhysicalMedia")
            For Each i As ManagementObject In mos.Get()
                result = i.GetPropertyValue("SerialNumber").ToString
            Next
            Return result
        End Function
        Shared ReadOnly Property IsWinXP As Boolean
            Get
                Return InStr(WinExt.PCInfo.OSname, "Microsoft Windows XP") <> 0
            End Get
        End Property
        Shared ReadOnly Property IsWinVista As Boolean
            Get
                Return InStr(WinExt.PCInfo.OSname, "Microsoft Windows Vista") <> 0
            End Get
        End Property
        Shared ReadOnly Property IsWin7 As Boolean
            Get
                Return InStr(WinExt.PCInfo.OSname, "Microsoft Windows 7") <> 0
            End Get
        End Property
        Shared ReadOnly Property IsWin8 As Boolean
            Get
                Return InStr(WinExt.PCInfo.OSname, "Microsoft Windows 8") <> 0
            End Get
        End Property
        Shared ReadOnly Property IsWin81 As Boolean
            Get
                Return InStr(WinExt.PCInfo.OSname, "Microsoft Windows 8.1") <> 0
            End Get
        End Property
        Shared ReadOnly Property IsWin10() As Boolean
            Get
                Return InStr(WinExt.PCInfo.OSname, "Microsoft Windows 10") <> 0
            End Get
        End Property
        Shared ReadOnly Property IsDesktop As Boolean
            Get
                Return SystemInformation.PowerStatus.BatteryChargeStatus.ToString = "NoSystemBattery"
            End Get
        End Property


    End Structure

#End Region
#Region "Lấy thông tin kết nối mạng"
    'https://wtfismyip.com/
    'https://ipv4.wtfismyip.com/js
    'https://api.ipify.org/
    'https://ipinfo.io/ip
    'https://ipinfo.io/city

    Public Structure NetInfo

        ''' <summary>
        ''' Kiểm tra kết nối mạng
        ''' </summary>
        ''' <returns></returns>
        Shared Function IsInternet() As Boolean
            Try
                Return My.Computer.Network.Ping("www.google.com.vn", 1000)
            Catch Ex As Exception
                Return False
            End Try
        End Function
        ''' <summary>
        ''' Lấy thông tin dữ liệu từ host dịch vụ
        ''' </summary>
        ''' <param name="value"></param>
        ''' <returns></returns>
        Private Shared Function GetDataIPAddress(value As String) As String
            Dim result As String = ""
            Dim request As WebRequest = WebRequest.Create("https://ipinfo.io/" & value)
            Using response As WebResponse = request.GetResponse()
                Using stream As StreamReader = New StreamReader(response.GetResponseStream())
                    result = stream.ReadToEnd()
                End Using
            End Using
            Return result
        End Function
        ''' <summary>
        ''' Lấy thông tin địa chỉ mạng cục bộ (LAN)
        ''' </summary>
        ''' <returns></returns>
        Private Shared Function GetLocalIPAddress()
            Dim result As String = ""
            Using sock As Socket = New Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.IP)
                sock.Connect("8.8.8.8", 65530)
                Dim ipe As IPEndPoint = CType(sock.LocalEndPoint, IPEndPoint)
                result = ipe.Address.ToString
            End Using
            Return result
        End Function
        ''' <summary>
        ''' Địa chỉ mạng LAN
        ''' </summary>
        ''' <returns></returns>
        Shared ReadOnly Property LocalIP As String
            Get
                Return GetLocalIPAddress()
            End Get
        End Property
        ''' <summary>
        ''' Địa chỉ mạng Internet
        ''' </summary>
        ''' <returns></returns>
        Shared ReadOnly Property WanIP As String
            Get
                If IsInternet() = True Then Return GetDataIPAddress("ip") Else Return "N/A"
            End Get
        End Property
        ''' <summary>
        ''' Tên nhà cung cấp dịch vụ mạng ASN / ISP
        ''' </summary>
        ''' <returns></returns>
        Shared ReadOnly Property Hostname As String
            Get
                If IsInternet() = True Then Return GetDataIPAddress("hostname") Else Return "N/A"
            End Get
        End Property
        ''' <summary>
        ''' Vị trí Thành phố hiện tại đang sử dụng mạng
        ''' </summary>
        ''' <returns></returns>
        Shared ReadOnly Property City As String
            Get
                If IsInternet() = True Then Return GetDataIPAddress("city") Else Return "N/A"
            End Get
        End Property
        ''' <summary>
        ''' Vị trí Khu vực đang sử dụng mạng ở trong thành phố
        ''' </summary>
        ''' <returns></returns>
        Shared ReadOnly Property Region As String
            Get
                If IsInternet() = True Then Return GetDataIPAddress("region") Else Return "N/A"
            End Get
        End Property
        ''' <summary>
        ''' Vị trí Quốc gia hiện tại đang sử dụng mạng
        ''' </summary>
        ''' <returns></returns>
        Shared ReadOnly Property Country As String
            Get
                If IsInternet() = True Then Return GetDataIPAddress("country") Else Return "N/A"
            End Get
        End Property

    End Structure
#End Region

End Class
