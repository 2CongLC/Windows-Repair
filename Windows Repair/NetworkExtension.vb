Imports System.Net
Imports System.Net.NetworkInformation
Imports System.Net.Sockets

Class NetworkExtension


    Private _IPV4Address As String
    Private _SubnetMask As String
    Private _Gateway As String
    Private _Dns1 As String
    Private _Dns2 As String
    Private _Dns3 As String
    Private _MacAddress As String

    Private _ConnectionName As String
    Private _ConnectDescription As String
    Private _IsDHCPEnabled As Boolean
    Private _IsDnsEnable As Boolean
    Private _IsDynamicDnsEnabled As Boolean
    Private _Status As OperationalStatus

    Friend ReadOnly Property IPV4Address As String
        Get
            Return _IPV4Address
        End Get
    End Property
    Friend ReadOnly Property SubnetMask As String
        Get
            Return _SubnetMask
        End Get
    End Property
    Friend ReadOnly Property Gateway As String
        Get
            Return _Gateway
        End Get
    End Property
    Friend ReadOnly Property Dns1 As String
        Get
            Return _Dns1
        End Get
    End Property
    Friend ReadOnly Property PreferredDnsServer As String
        Get
            Return _Dns2
        End Get
    End Property
    Friend ReadOnly Property AlternateDnsServer As String
        Get
            Return _Dns3
        End Get
    End Property
    Friend ReadOnly Property MACAddress As String
        Get
            Return _MacAddress
        End Get
    End Property

    Friend ReadOnly Property ConnectionName As String
        Get
            Return _ConnectionName
        End Get
    End Property
    Friend ReadOnly Property ConnectDescription As String
        Get
            Return _ConnectDescription
        End Get
    End Property

    Friend ReadOnly Property IsDHCPEnabled As Boolean
        Get
            Return _IsDHCPEnabled
        End Get
    End Property

    Friend ReadOnly Property IsDynamicDnsEnabled As Boolean
        Get
            Return _IsDynamicDnsEnabled
        End Get
    End Property
    Friend ReadOnly Property IsDnsEnable As Boolean
        Get
            Return _IsDnsEnable
        End Get
    End Property

    Friend ReadOnly Property Status As OperationalStatus
        Get
            Return _Status
        End Get
    End Property
    Public Sub New()
        Dim Host As String = Dns.GetHostName()
        For Each ip In Dns.GetHostEntry(Host).AddressList
            If ip.AddressFamily = AddressFamily.InterNetwork Then
                'IPv4 Adress
                _IPV4Address = ip.ToString()

                For Each adapter As NetworkInterface In NetworkInterface.GetAllNetworkInterfaces()
                    For Each UnicastAddresses As UnicastIPAddressInformation In adapter.GetIPProperties().UnicastAddresses
                        If UnicastAddresses.Address.AddressFamily = AddressFamily.InterNetwork Then
                            If ip.Equals(UnicastAddresses.Address) Then
                                'Subnet Mask
                                _SubnetMask = UnicastAddresses.IPv4Mask.ToString()

                                Dim adapterProperties As IPInterfaceProperties = adapter.GetIPProperties()
                                For Each gateway As GatewayIPAddressInformation In adapterProperties.GatewayAddresses
                                    'Default Gateway
                                    _Gateway = gateway.Address.ToString()
                                Next

                                'DNS
                                _IsDnsEnable = adapterProperties.IsDnsEnabled
                                _IsDynamicDnsEnabled = adapterProperties.IsDynamicDnsEnabled

                                'DNS1
                                If adapterProperties.DnsAddresses.Count > 0 Then
                                    _Dns1 = adapterProperties.DnsAddresses(0).ToString()
                                End If

                                'DNS2
                                If adapterProperties.DnsAddresses.Count > 1 Then
                                    _Dns2 = adapterProperties.DnsAddresses(1).ToString()
                                End If

                                'DNS3
                                If adapterProperties.DnsAddresses.Count > 2 Then
                                    _Dns3 = adapterProperties.DnsAddresses(2).ToString()
                                End If

                                'MAC Address
                                ' _MacAddress = adapter.GetPhysicalAddress().GetAddressBytes().Select(Function(b) b.ToString("X")).Aggregate(Function(s1, s2) s2 + ":" + s1)
                                _MacAddress = adapter.GetPhysicalAddress().GetAddressBytes().Select(Function(b) b.ToString("X")).Aggregate(Function(s1, s2) s1 + ":" + s2)

                                'ConnectionName
                                _ConnectionName = adapter.Name

                                'ConnectDescription
                                _ConnectDescription = adapter.Description

                                'IsDHCPEnabled
                                _IsDHCPEnabled = adapterProperties.GetIPv4Properties().IsDhcpEnabled

                                'Status
                                _Status = adapter.OperationalStatus


                            End If
                        End If
                    Next
                Next
            End If
        Next
    End Sub
End Class
