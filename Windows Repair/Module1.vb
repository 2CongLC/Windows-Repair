Imports System.Drawing.Imaging
Imports System.IO

Module Module1
    Friend Function FileSystem32(Filename As String) As String
        Return Environment.ExpandEnvironmentVariables("%Systemroot%") & "\System32\" & Filename
    End Function
    Friend Function FileSystem64(Filename As String) As String
        Return Environment.GetFolderPath(Environment.SpecialFolder.SystemX86) & "\" & Filename
    End Function
    Friend Function FileProgram32(Filename As String) As String
        Return Environment.ExpandEnvironmentVariables("%Programfiles%") & "\Internet Explorer\" & Filename
    End Function
    Friend Function ByteArrayToHexString(value As Object) As String
        Return BitConverter.ToString(value).Replace("-", "")
    End Function
    Public Function ConvertToHexString(ByVal value As Byte()) As String
        Return String.Join("", value.Select(Function(by) by.ToString("X2")))
    End Function


    Friend Function HexStringToByteArray(ByVal shex As String) As Byte()
        Dim B As Byte() = Enumerable.Range(0, shex.Length).Where(Function(x) x Mod 2 = 0).[Select](Function(x) Convert.ToByte(shex.Substring(x, 2), 16)).ToArray()
        'Return Enumerable.Range(0, shex.Length).Where(Function(x) x Mod 2 = 0).[Select](Function(x) Convert.ToByte(shex.Substring(x, 2), 16)).ToArray()
        Return B
    End Function
    Friend Function GetImageSize(inFile As String, Optional Width As String = "110", Optional Height As String = " 95") As Boolean
        Dim bm As New Bitmap(inFile)
        If bm.Width.ToString = 110 AndAlso bm.Height.ToString = 95 Then Return True Else Return False
    End Function
    Friend Function GetImageFormat(inFile As String) As String
        Try
            Dim img As Image = Image.FromFile(inFile)
            If ImageFormat.Bmp.Equals(img.RawFormat) Then
                Return "Bmp"
            ElseIf ImageFormat.Emf.Equals(img.RawFormat) Then
                Return "Emf"
            ElseIf ImageFormat.Exif.Equals(img.RawFormat) Then
                Return "Exif"
            ElseIf ImageFormat.Gif.Equals(img.RawFormat) Then
                Return "Gif"
            ElseIf ImageFormat.Icon.Equals(img.RawFormat) Then
                Return "Icon"
            ElseIf ImageFormat.Jpeg.Equals(img.RawFormat) Then
                Return "Jpeg"
            ElseIf ImageFormat.Png.Equals(img.RawFormat) Then
                Return "Png"
            ElseIf ImageFormat.Tiff.Equals(img.RawFormat) Then
                Return "Tiff"
            ElseIf ImageFormat.Wmf.Equals(img.RawFormat) Then
                Return "Wmf"
            Else
                Return "Unknow"
            End If
        Catch ex As Exception
            Return ex.ToString
        End Try
    End Function
    Friend Function GetAllLogicalDrives() As String()
        Return Directory.GetLogicalDrives()
    End Function

End Module
