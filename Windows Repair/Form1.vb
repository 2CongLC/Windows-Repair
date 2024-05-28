Imports System
Imports System.Text
Imports System.IO
Imports Microsoft.Win32

Public Class Form1
    Private Reg As RegistryKey
    Private ImagePath As String
    Private IconPath As String
    Private DriveName As String

#Region "Sửa lỗi Windows"

    Private MMCSubkey As String() = {"{8EAD3A12-B2C1-11d0-83AA-00A0C92C9D5D}", "{58221C67-EA27-11CF-ADCF-00AA00A80033}", "{C9BC92DF-5B9A-11D1-8F00-00C04FC2C17B}", "{EBC53A38-A23F-11D0-B09B-00C04FD8DCA6}", "{D967F824-9968-11D0-B936-00C04FD8D5B0}", "{E355E538-1C2E-11D0-8C37-00C04FD8FE93}", "{0F6B957D-509E-11D1-A7CC-0000F87571E3}", "{1AA7F83C-C7F5-11D0-A376-00C04FC9DA04}", "{53D6AB1D-2488-11D1-A28C-00C04FB94F17}",
 "{3F276EB4-70EE-11D1-8A0F-00C04FB93753}", "{C2FE450B-D6C2-11D0-A37B-00C04FC9DA04}", "{9EC88934-C774-11d1-87F4-00C04FC2C17B}", "{90087284-d6d6-11d0-8353-00a0c90640bf}", "{C2FE4502-D6C2-11D0-A37B-00C04FC9DA04}", "{43668E21-2636-11D1-A1CE-0080C88593A5}", "{677A2D94-28D9-11D1-A95B-008048918FB1}", "{975797FC-4E2A-11D0-B702-00C04FD8DBF7}",
 "{753EDB4D-2E1B-11D1-9064-00A0C90AB504}", "{88E729D6-BDC1-11D1-BD2A-00C04FB9603F}", "{8FC0B734-A0E1-11D1-A7D3-0000F87571E3}", "{D70A2BEA-A63E-11D1-A7D4-0000F87571E3}", "{2E19B602-48EB-11d2-83CA-00104BCA42CF}", "{C2FE4508-D6C2-11D0-A37B-00C04FC9DA04}", "{95AD72F0-44CE-11D0-AE29-00AA004B9986}", "{8F8F8DC0-5713-11D1-9551-0060B0576642}",
 "{FC715823-C5FB-11D1-9EEF-00A0C90347FF}", "{A841B6C2-7577-11D0-BB1F-00A0C922E79C}", "{C2FE4500-D6C2-11D0-A37B-00C04FC9DA04}", "{DEA8AFA0-CC85-11d0-9CE2-0080C7221EBD}", "{90810502-38F1-11D1-9345-00C04FC9DA04}", "{90810500-38F1-11D1-9345-00C04FC9DA04}", "{90810504-38F1-11D1-9345-00C04FC9DA04}", "{5D6179C8-17EC-11D1-9AA9-00C04FD8FE93}",
 "{6E8E0081-19CD-11D1-AD91-00AA00B8E05A}", "{C2FE4506-D6C2-11D0-A37B-00C04FC9DA04}", "{7478EF61-8C46-11d1-8D99-00A0C913CAD4}", "{34AB8E82-C27E-11D1-A6C0-00C04FB94F17}", "{FD57D297-4FD9-11D1-854E-00C04FC31FD3}", "{B52C1E50-1DD2-11D1-BC43-00C04FC31FD3}", "{5880CD5C-8EC0-11d1-9570-0060B0576642}", "{3060E8CE-7020-11D2-842D-00C04FA372D4}",
 "{243E20B0-48ED-11D2-97DA-00A024D77700}", "{3CB6973D-3E6F-11D0-95DB-00A024D77700}", "{C2FE4504-D6C2-11D0-A37B-00C04FC9DA04}", "{C2FE4504-D6C2-11D0-A37B-00C04FC9DA04}", "{DAB1A262-4FD7-11D1-842C-00C04FB6C218}", "{1AA7F839-C7F5-11D0-A376-00C04FC9DA04}", "{40B66650-4972-11D1-A7CA-0000F87571E3}", "{40B6664F-4972-11D1-A7CA-0000F87571E3}",
 "{011BE22D-E453-11D1-945A-00C04FB984F9}", "{803E14A0-B4FB-11D0-A0D0-00A0C90F574B}", "{5ADF5BF6-E452-11D1-945A-00C04FB984F9}", "{B1AFF7D0-0C49-11D1-BB12-00C04FC9A3A3}", "{BD95BA60-2E26-AAD1-AD99-00AA00B8E05A}", "{58221C66-EA27-11CF-ADCF-00AA00A80033}", "{58221C65-EA27-11CF-ADCF-00AA00A80033}", "{03f1f940-a0f2-11d0-bb77-00aa00a1eab7}",
 "{7AF60DD3-4979-11D1-8A6C-00C04FC33566}", "{942A8E4F-A261-11D1-A760-00C04FB9603F}", "{45ac8c63-23e2-11d1-a696-00c04fd58bc3}", "{0F3621F1-23C6-11D1-AD97-00AA00B88E5A}", "{E26D02A0-4C1F-11D1-9AA1-00C04FC3357A}", "{B91B6008-32D2-11D2-9888-00A0C925F917}", "{5C659257-E236-11D2-8899-00104B2AFB46}", "{74246bfc-4c96-11d0-abef-0020af6b0b7a}"}


    Private PoliciesMicrosoftControlPanelInternational As String() = {"HideAdminOptions", "HideCurrentLocation", "HideLanguageSelection", "HideLocaleSelectAndCustomize", "TurnOffAutocorrectMisspelledWords", "TurnOffHighlightMisspelledWords", "TurnOffInsertSpace", "TurnOffOfferTextPredictions"}
    Private PoliciesMicrosoftControlPanelDesktop As String() = {"PreferredUILanguages", "MultiUILanguageID"}
    Private PoliciesMicrosoftWindowsControlPanelDesktop As String() = {"ScreenSaverIsSecure", "ScreenSaveTimeOut"}
    Private PoliciesMicrosoftSystem As String() = {"DisableCMD"}
    Private PoliciesMicrosoftExplorer As String() = {"TaskbarNoMultimon", "TaskbarNoPinnedList", "NoPinningToDestinations", "NoPinningToTaskbar", "ShowWindowsStoreAppsOnTaskbar", "NoBalloonFeatureAdvertisements"}
    Private WindowsPoliciesSystem As String() = {"DisableRegistryTools", "DisableTaskMgr", "DisableChangePassword", "DisableLockWorkstation", "NoDispCPL", "NoDispSettingsPage", "NoColorChoice", "NoVisualStyleChoice", "NoSizeChoice", "NoDispAppearancePage", "NoDispBackgroundPage", "NoDispScrSavPage", "SetVisualStyle"}
    Private WindowsPoliciesExplorer As String() = {"NoDrives", "NoViewOnDrive", "RestrictRun", "NoLogoff", "NoFileMenu", "NoHardwareTab", "NoManageMyComputerVerb", "NoFolderOptions", "NoViewContextMenu", "NoChangeAnimation", "DisallowCpl", "NoControlPanel", "RestrictCpl", "NoThemesTab", "NoDeletePrinter",
    "LockTaskbar", "HideClock", "NoTaskGrouping", "NoSetTaskbar", "NoToolbarsOnTaskbar", "NoTrayContextMenu", "HideSCAHealth", "HideSCANetwork", "HideSCAPower", "HideSCAVolume", "TaskbarLockAll", "TaskbarNoAddRemoveToolbar", "TaskbarNoNotification", "TaskbarNoRedock", "TaskbarNoThumbnail",
    "DisablePersonalDirChange", "NoDesktop", "NoInternetIcon", "NoNetHood", "NoPropertiesMyComputer", "NoPropertiesMyDocuments", "NoRecentDocsNetHood", "NoPropertiesRecycleBin", "NoSaveSettings", "NoWindowMinimizingShortcuts", "NoCloseDragDropBands", "NoMovingBands", "NoActiveDesktopChanges", "NoActiveDesktop",
    "ClearRecentDocsOnExit", "ClearTilesOnExit", "Intellimenus", "NoChangeStartMenu", "NoClose", "NoCommonGroups", "NoInstrumentation", "NoNetworkConnections", "NoStartMenuPinnedList", "NoRecentDocsHistory", "NoRecentDocsMenu", "NoRun", "NoSMConfigurePrograms", "NoStartMenuNetworkPlaces", "NoSetFolders", "NoSimpleStartMenu",
    "NoTrayItemsDisplay", "NoUninstallFromStart", "NoUserNameInStartMenu", "PowerButtonAction", "NoStartMenuEjectPC", "ForceRunOnStartMenu", "StartMenuLogOff", "NoAddPrinter"}

    Private Sub AddListview1(key As String, value As String, group As String, description As String)
        Dim items As ListViewItem = New ListViewItem(key)
        items.SubItems.Add(value)
        items.SubItems.Add(group)
        items.SubItems.Add(description)
        ListView1.Invoke(CType(Sub()
                                   ListView1.BeginUpdate()
                                   ListView1.Items.Add(items)
                                   ListView1.EndUpdate()
                               End Sub, Action))
    End Sub
    Private Sub RemoveMMC()
        For Each i As String In MMCSubkey
            Reg = Registry.CurrentUser.OpenSubKey("SOFTWARE\Policies\Microsoft\MMC\" & i, True)
            If Reg IsNot Nothing Then
                Dim j As String = Reg.GetValue("Restrict_Run")
                ' If j <> 0 Then Reg.DeleteValue("Restrict_Run")
                If j <> 0 Then My.Computer.Registry.CurrentUser.DeleteSubKey("SOFTWARE\Policies\Microsoft\MMC\" & i)
                AddListview1(i, j, "MMC Snap-in", "Gây nguy hại cho hệ thống, đã loại bỏ !")
            End If
        Next
        Reg = Registry.CurrentUser.OpenSubKey("SOFTWARE\Policies\Microsoft\MMC", True)
        If Reg IsNot Nothing Then
            If Not Reg.GetValue("RestrictAuthorMode") = False Then
                AddListview1("RestrictAuthorMode", Reg.GetValue("RestrictAuthorMode"), "Policies", "Gây nguy hại cho hệ thống, đã loại bỏ !")
                Reg.DeleteValue("RestrictAuthorMode")
            End If
            If Not Reg.GetValue("RestrictToPermittedSnapins") = False Then
                AddListview1("RestrictToPermittedSnapins", Reg.GetValue("RestrictToPermittedSnapins"), "Policies", "Gây nguy hại cho hệ thống, đã loại bỏ !")
                Reg.DeleteValue("RestrictToPermittedSnapins")
            End If
        End If
    End Sub

    Private Sub RemovePoliciesMicrosoftControlPanelInternational()
        Reg = Registry.CurrentUser.OpenSubKey("SOFTWARE\Policies\Microsoft\Control Panel\International", True)
        If Reg IsNot Nothing Then
            For Each i In PoliciesMicrosoftControlPanelInternational
                Dim j As String = Reg.GetValue(i)
                If j <> 0 Then Reg.DeleteValue(i) : AddListview1(i, j, "International", "Gây nguy hại cho hệ thống, đã loại bỏ !")
            Next
        End If
    End Sub
    Private Sub RemovePoliciesMicrosoftControlPanelDesktop()
        Reg = Registry.CurrentUser.OpenSubKey("SOFTWARE\Policies\Microsoft\Control Panel\Desktop", True)
        If Reg IsNot Nothing Then
            For Each i In PoliciesMicrosoftControlPanelDesktop
                Dim j As String = Reg.GetValue(i)
                If j <> 0 Then Reg.DeleteValue(i) : AddListview1(i, j, "Desktop", "Gây nguy hại cho hệ thống, đã loại bỏ !")
            Next
        End If
    End Sub
    Private Sub RemovePoliciesMicrosoftWindowsControlPanelDesktop()
        Reg = Registry.CurrentUser.OpenSubKey("SOFTWARE\Policies\Microsoft\Windows\Control Panel\Desktop", True)
        If Reg IsNot Nothing Then
            For Each i In PoliciesMicrosoftWindowsControlPanelDesktop
                Dim j As String = Reg.GetValue(i)
                If j <> 0 Then Reg.DeleteValue(i) : AddListview1(i, j, "Desktop", "Gây nguy hại cho hệ thống, đã loại bỏ !")
            Next
        End If
    End Sub
    Private Sub RemovePoliciesMicrosoftSystem()
        Reg = Registry.CurrentUser.OpenSubKey("SOFTWARE\Policies\Microsoft\Windows\System", True)
        If Reg IsNot Nothing Then
            For Each i In PoliciesMicrosoftSystem
                Dim j As String = Reg.GetValue(i)
                If j <> 0 Then Reg.DeleteValue(i) : AddListview1(i, j, "PoliciesSystem", "Gây nguy hại cho hệ thống, đã loại bỏ !")
            Next
        End If
    End Sub
    Private Sub RemovePoliciesMicrosoftExplorer()
        Reg = Registry.CurrentUser.OpenSubKey("SOFTWARE\Policies\Microsoft\Windows\Explorer", True)
        If Reg IsNot Nothing Then
            For Each i In PoliciesMicrosoftExplorer
                Dim j As String = Reg.GetValue(i)
                If j <> 0 Then Reg.DeleteValue(i) : AddListview1(i, j, "PoliciesExplorer", "Gây nguy hại cho hệ thống, đã loại bỏ !")
            Next
            If WinExt.PCInfo.IsWinXP = True Then
                If Not Reg.GetValue("DisableNotificationCenter") = 1 Then
                    AddListview1("DisableNotificationCenter", Reg.GetValue("DisableNotificationCenter"), "PoliciesExplorer", "Đã thay đổi thành 1")
                    Reg.SetValue("DisableNotificationCenter", 1, RegistryValueKind.DWord)
                End If
            End If
        End If

    End Sub
    Private Sub RemoveWindowsPoliciesSystem()
        Reg = Registry.CurrentUser.OpenSubKey("SOFTWARE\Microsoft\Windows\CurrentVersion\Policies\System", True)
        If Reg IsNot Nothing Then
            For Each i In WindowsPoliciesSystem
                Dim j As String = Reg.GetValue(i)
                If j <> 0 Then Reg.DeleteValue(i) : AddListview1(i, j, "WindowsPoliciesSystem", "Gây nguy hại cho hệ thống, đã loại bỏ !")
            Next
        End If
    End Sub
    Private Sub RemoveWindowsPoliciesExplorer()
        Reg = Registry.CurrentUser.OpenSubKey("SOFTWARE\Microsoft\Windows\CurrentVersion\Policies\Explorer", True)
        If Reg IsNot Nothing Then
            For Each i In WindowsPoliciesExplorer
                Dim j As String = Reg.GetValue(i)
                If j <> 0 Then Reg.DeleteValue(i) : AddListview1(i, j, "WindowsPoliciesExplorer", "Gây nguy hại cho hệ thống, đã loại bỏ !")
            Next

            If Not Reg.GetValue("NoLowDiskSpaceChecks") = 1 Then
                AddListview1("NoLowDiskSpaceChecks", Reg.GetValue("NoLowDiskSpaceChecks"), "WindowsPoliciesExplorer", "Đã thay đổi thành 1")
                Reg.SetValue("NoLowDiskSpaceChecks", 1, RegistryValueKind.DWord)
            End If
        End If
    End Sub
    Private Sub DisableUAC(Optional Enable As Boolean = True)
        If Enable = True Then

            If WinExt.PCInfo.IsWinXP = False Then
                Reg = Registry.LocalMachine.OpenSubKey("SOFTWARE\Microsoft\Windows\CurrentVersion\Policies\System", True)
                If Reg IsNot Nothing Then
                    If Not Reg.GetValue("ConsentPromptBehaviorAdmin") = 0 Then
                        AddListview1("ConsentPromptBehaviorAdmin", Reg.GetValue("ConsentPromptBehaviorAdmin"), "UAC", "Đã thay đổi thành 0")
                        Reg.SetValue("ConsentPromptBehaviorAdmin", 0, RegistryValueKind.DWord)
                    End If
                    If Not Reg.GetValue("ConsentPromptBehaviorUser") = 3 Then
                        AddListview1("ConsentPromptBehaviorUser", Reg.GetValue("ConsentPromptBehaviorUser"), "UAC", "Đã thay đổi thành 3")
                        Reg.SetValue("ConsentPromptBehaviorUser", 3, RegistryValueKind.DWord)
                    End If
                    If Not Reg.GetValue("PromptOnSecureDesktop") = 0 Then
                        AddListview1("PromptOnSecureDesktop", Reg.GetValue("PromptOnSecureDesktop"), "UAC", "Đã thay đổi thành 0")
                        Reg.SetValue("PromptOnSecureDesktop", 0, RegistryValueKind.DWord)
                    End If
                    If Not Reg.GetValue("EnableLUA") = 1 Then
                        AddListview1("EnableLUA", Reg.GetValue("EnableLUA"), "UAC", "Đã thay đổi thành 1")
                        Reg.SetValue("EnableLUA", 1, RegistryValueKind.DWord)
                    End If
                End If
            End If

        End If
    End Sub
    Private Sub DisableWindowsFirewall(Optional Enable As Boolean = True)
        If Enable = True Then

            Reg = Registry.LocalMachine.OpenSubKey("SYSTEM\CurrentControlSet\Services\SharedAccess\Parameters\FirewallPolicy\DomainProfile", True)
            If Reg IsNot Nothing Then
                If Not Reg.GetValue("EnableFirewall") = "0" Then
                    AddListview1("EnableFirewall", Reg.GetValue("EnableFirewall"), "ControlSet", "Đã thay đổi thành 0")
                    Reg.SetValue("EnableFirewall", "0", RegistryValueKind.DWord)
                End If
                If Not Reg.GetValue("DisableNotifications") = "1" Then
                    AddListview1("DisableNotifications", Reg.GetValue("DisableNotifications"), "ControlSet", "Đã thay đổi thành 1")
                    Reg.SetValue("DisableNotifications", "1", RegistryValueKind.DWord)
                End If
            End If

            Reg = Registry.LocalMachine.OpenSubKey("SYSTEM\CurrentControlSet\Services\SharedAccess\Parameters\FirewallPolicy\PublicProfile", True)
            If Reg IsNot Nothing Then
                If Not Reg.GetValue("EnableFirewall") = "0" Then
                    AddListview1("EnableFirewall", Reg.GetValue("EnableFirewall"), "ControlSet", "Đã thay đổi thành 0")
                    Reg.SetValue("EnableFirewall", "0", RegistryValueKind.DWord)
                End If
                If Not Reg.GetValue("DisableNotifications") = "1" Then
                    AddListview1("DisableNotifications", Reg.GetValue("DisableNotifications"), "ControlSet", "Đã thay đổi thành 1")
                    Reg.SetValue("DisableNotifications", "1", RegistryValueKind.DWord)
                End If
            End If

            Reg = Registry.LocalMachine.OpenSubKey("SYSTEM\CurrentControlSet\Services\SharedAccess\Parameters\FirewallPolicy\StandardProfile", True)
            If Reg IsNot Nothing Then
                If Not Reg.GetValue("EnableFirewall") = "0" Then
                    AddListview1("EnableFirewall", Reg.GetValue("EnableFirewall"), "ControlSet", "Đã thay đổi thành 0")
                    Reg.SetValue("EnableFirewall", "0", RegistryValueKind.DWord)
                End If
                If Not Reg.GetValue("DisableNotifications") = "1" Then
                    AddListview1("DisableNotifications", Reg.GetValue("DisableNotifications"), "ControlSet", "Đã thay đổi thành 1")
                    Reg.SetValue("DisableNotifications", "1", RegistryValueKind.DWord)
                End If
            End If

        End If
    End Sub
    Private Sub EnableDriverSearch()
        Reg = Registry.CurrentUser.OpenSubKey("SOFTWARE\Policies\Microsoft\DriverSearching", True)
        If Reg IsNot Nothing Then
            If Not Reg.GetValue("DontPromptForWindowsUpdate") = 0 Then
                AddListview1("DontPromptForWindowsUpdate", Reg.GetValue("DontPromptForWindowsUpdate"), "Policies", "Gây nguy hại cho hệ thống, đã loại bỏ !")
                Reg.DeleteValue("DontPromptForWindowsUpdate")
            End If
            If Not Reg.GetValue("DontSearchFloppies") = 0 Then
                AddListview1("DontSearchFloppies", Reg.GetValue("DontSearchFloppies"), "Policies", "Gây nguy hại cho hệ thống, đã loại bỏ !")
                Reg.DeleteValue("DontSearchFloppies")
            End If
            If Not Reg.GetValue("DontSearchCD") = 0 Then
                AddListview1("DontSearchCD", Reg.GetValue("DontSearchCD"), "Policies", "Gây nguy hại cho hệ thống, đã loại bỏ !")
                Reg.DeleteValue("DontSearchCD")
            End If
            If Not Reg.GetValue("DontSearchWindowsUpdate") = 0 Then
                AddListview1("DontSearchWindowsUpdate", Reg.GetValue("DontSearchWindowsUpdate"), "Policies", "Gây nguy hại cho hệ thống, đã loại bỏ !")
                Reg.DeleteValue("DontSearchWindowsUpdate")
            End If
        End If
    End Sub
    Private Sub EnableStartMenu()
        Reg = Registry.CurrentUser.OpenSubKey("SOFTWARE\Microsoft\Windows\CurrentVersion\Policies\Explorer", True)
        If Reg IsNot Nothing Then
            If Not Reg.GetValue("ClearRecentProgForNewUserInStartMenu") = 1 Then
                AddListview1("ClearRecentProgForNewUserInStartMenu", Reg.GetValue("ClearRecentProgForNewUserInStartMenu"), "StartMenu", "Đã thay đổi thành 1")
                Reg.SetValue("ClearRecentProgForNewUserInStartMenu", 1, RegistryValueKind.DWord)
            End If
            If Not Reg.GetValue("ForceStartMenuLogOff") = 1 Then
                AddListview1("ForceStartMenuLogOff", Reg.GetValue("ForceStartMenuLogOff"), "StartMenu", "Đã thay đổi thành 1")
                Reg.SetValue("ForceStartMenuLogOff", 1, RegistryValueKind.DWord)
            End If
            If Not Reg.GetValue("NoFavoritesMenu") = 1 Then
                AddListview1("NoFavoritesMenu", Reg.GetValue("NoFavoritesMenu"), "StartMenu", "Đã thay đổi thành 1")
                Reg.SetValue("NoFavoritesMenu", 1, RegistryValueKind.DWord)
            End If
            If Not Reg.GetValue("NoFind") = 1 Then
                AddListview1("NoFind", Reg.GetValue("NoFind"), "StartMenu", "Đã thay đổi thành 1")
                Reg.SetValue("NoFind", 1, RegistryValueKind.DWord)
            End If
            If Not Reg.GetValue("NoSMHelp") = 1 Then
                AddListview1("NoSMHelp", Reg.GetValue("NoSMHelp"), "StartMenu", "Đã thay đổi thành 1")
                Reg.SetValue("NoSMHelp", 1, RegistryValueKind.DWord)
            End If
            If Not Reg.GetValue("NoStartMenuMyMusic") = 1 Then
                AddListview1("NoStartMenuMyMusic", Reg.GetValue("NoStartMenuMyMusic"), "StartMenu", "Đã thay đổi thành 1")
                Reg.SetValue("NoStartMenuMyMusic", 1, RegistryValueKind.DWord)
            End If
            If Not Reg.GetValue("NoSMMyPictures") = 1 Then
                AddListview1("NoSMMyPictures", Reg.GetValue("NoSMMyPictures"), "StartMenu", "Đã thay đổi thành 1")
                Reg.SetValue("NoSMMyPictures", 1, RegistryValueKind.DWord)
            End If
            If Not Reg.GetValue("NoWindowsUpdate") = 1 Then
                AddListview1("NoWindowsUpdate", Reg.GetValue("NoWindowsUpdate"), "StartMenu", "Đã thay đổi thành 1")
                Reg.SetValue("NoWindowsUpdate", 1, RegistryValueKind.DWord)
            End If
        End If

        Reg = Registry.CurrentUser.OpenSubKey("SOFTWARE\Microsoft\Windows\CurrentVersion\Explorer\Advanced", True)
        If Reg IsNot Nothing Then
            If Not Reg.GetValue("ShowSuperHidden") = 0 Then
                AddListview1("ShowSuperHidden", Reg.GetValue("ShowSuperHidden"), "StartMenu", "Đã thay đổi thành 0")
                Reg.SetValue("ShowSuperHidden", 0, RegistryValueKind.DWord)
            End If
            If Not Reg.GetValue("PersistBrowsers") = 0 Then
                AddListview1("ShowSuperHidden", Reg.GetValue("PersistBrowsers"), "StartMenu", "Đã thay đổi thành 0")
                Reg.SetValue("PersistBrowsers", 0, RegistryValueKind.DWord)
            End If
            If Not Reg.GetValue("EnableStartMenu") = 1 Then
                AddListview1("EnableStartMenu", Reg.GetValue("EnableStartMenu"), "StartMenu", "Đã thay đổi thành 1")
                Reg.SetValue("EnableStartMenu", 1, RegistryValueKind.DWord)
            End If
        End If

    End Sub
    Private Sub EnableControlPanel()
        Reg = Registry.CurrentUser.OpenSubKey("SOFTWARE\Microsoft\Windows\CurrentVersion\Policies\Programs", True)
        If Reg IsNot Nothing Then
            If Not Reg.GetValue("NoDefaultPrograms") = 0 Then
                AddListview1("NoDefaultPrograms", Reg.GetValue("NoDefaultPrograms"), "ControlPanel", "Gây nguy hại cho hệ thống, đã loại bỏ !")
                Reg.DeleteValue("NoDefaultPrograms")
            End If
            If Not Reg.GetValue("NoGetPrograms") = 0 Then
                AddListview1("NoGetPrograms", Reg.GetValue("NoGetPrograms"), "ControlPanel", "Gây nguy hại cho hệ thống, đã loại bỏ !")
                Reg.DeleteValue("NoGetPrograms")
            End If
            If Not Reg.GetValue("NoInstalledUpdates") = 0 Then
                AddListview1("NoInstalledUpdates", Reg.GetValue("NoInstalledUpdates"), "ControlPanel", "Gây nguy hại cho hệ thống, đã loại bỏ !")
                Reg.DeleteValue("NoInstalledUpdates")
            End If
            If Not Reg.GetValue("NoProgramsAndFeatures") = 0 Then
                AddListview1("NoProgramsAndFeatures", Reg.GetValue("NoProgramsAndFeatures"), "ControlPanel", "Gây nguy hại cho hệ thống, đã loại bỏ !")
                Reg.DeleteValue("NoProgramsAndFeatures")
            End If
            If Not Reg.GetValue("NoProgramsCPL") = 0 Then
                AddListview1("NoProgramsCPL", Reg.GetValue("NoProgramsCPL"), "ControlPanel", "Gây nguy hại cho hệ thống, đã loại bỏ !")
                Reg.DeleteValue("NoProgramsCPL")
            End If
            If Not Reg.GetValue("NoWindowsFeatures") = 0 Then
                AddListview1("NoWindowsFeatures", Reg.GetValue("NoWindowsFeatures"), "ControlPanel", "Gây nguy hại cho hệ thống, đã loại bỏ !")
                Reg.DeleteValue("NoWindowsFeatures")
            End If
            If Not Reg.GetValue("NoWindowsMarketplace") = 0 Then
                AddListview1("NoWindowsMarketplace", Reg.GetValue("NoWindowsMarketplace"), "ControlPanel", "Gây nguy hại cho hệ thống, đã loại bỏ !")
                Reg.DeleteValue("NoWindowsMarketplace")
            End If
        End If
    End Sub
    Private Sub EnablePrinter()
        Reg = Registry.CurrentUser.OpenSubKey("SOFTWARE\Policies\Microsoft\Windows NT\Printers\Wizard", True)
        If Reg IsNot Nothing Then
            If Not Reg.GetValue("Downlevel Browse") = False Then
                AddListview1("Downlevel Browse", Reg.GetValue("Downlevel Browse"), "Printer", "Gây nguy hại cho hệ thống, đã loại bỏ !")
                Reg.DeleteValue("Downlevel Browse")
            End If
            If Not Reg.GetValue("Printers Page URL") = False Then
                AddListview1("Printers Page URL", Reg.GetValue("Printers Page URL"), "Printer", "Gây nguy hại cho hệ thống, đã loại bỏ !")
                Reg.DeleteValue("Printers Page URL")
            End If
            If Not Reg.GetValue("Default Search Scope") = False Then
                AddListview1("Default Search Scope", Reg.GetValue("Default Search Scope"), "Printer", "Gây nguy hại cho hệ thống, đã loại bỏ !")
                Reg.DeleteValue("Default Search Scope")
            End If
        End If
        Reg = Registry.CurrentUser.OpenSubKey("SOFTWARE\Policies\Microsoft\Windows NT\Printers\PackagePointAndPrint", True)
        If Reg IsNot Nothing Then
            If Not Reg.GetValue("PackagePointAndPrintOnly") = False Then
                AddListview1("PackagePointAndPrintOnly", Reg.GetValue("PackagePointAndPrintOnly"), "Printer", "Gây nguy hại cho hệ thống, đã loại bỏ !")
                Reg.DeleteValue("PackagePointAndPrintOnly")
            End If
        End If
        Reg = Registry.CurrentUser.OpenSubKey("SOFTWARE\Policies\Microsoft\Windows NT\Printers\PointAndPrint", True)
        If Reg IsNot Nothing Then
            If Not Reg.GetValue("Restricted") = False Then
                AddListview1("Restricted", Reg.GetValue("Restricted"), "Printer", "Gây nguy hại cho hệ thống, đã loại bỏ !")
                Reg.DeleteValue("Restricted")
            End If
        End If
    End Sub
    Private Sub ResetWindowsPoliciesSystem()
        Reg = Registry.CurrentUser.OpenSubKey("SOFTWARE\Microsoft\Windows\CurrentVersion\Policies\System", True)
        If Reg IsNot Nothing Then
            If Not Reg.GetValue("DisableCAD") = 1 Then
                AddListview1("DisableCAD", Reg.GetValue("DisableCAD"), "WindowsPoliciesSystem", "Đã thay đổi thành 1")
                Reg.SetValue("DisableCAD", 1, RegistryValueKind.DWord)
            End If
            If Not Reg.GetValue("dontdisplaylastusername") = 0 Then
                AddListview1("dontdisplaylastusername", Reg.GetValue("dontdisplaylastusername"), "WindowsPoliciesSystem", "Đã thay đổi thành 0")
                Reg.SetValue("dontdisplaylastusername", 0, RegistryValueKind.DWord)
            End If
            If Not Reg.GetValue("EnableCursorSuppression") = 1 Then
                AddListview1("EnableCursorSuppression", Reg.GetValue("EnableCursorSuppression"), "WindowsPoliciesSystem", "Đã thay đổi thành 1")
                Reg.SetValue("EnableCursorSuppression", 1, RegistryValueKind.DWord)
            End If
            If Not Reg.GetValue("EnableInstallerDetection") = 1 Then
                AddListview1("EnableInstallerDetection", Reg.GetValue("EnableInstallerDetection"), "WindowsPoliciesSystem", "Đã thay đổi thành 1")
                Reg.SetValue("EnableInstallerDetection", 1, RegistryValueKind.DWord)
            End If
            If Not Reg.GetValue("EnableSecureUIAPaths") = 1 Then
                AddListview1("EnableSecureUIAPaths", Reg.GetValue("EnableSecureUIAPaths"), "WindowsPoliciesSystem", "Đã thay đổi thành 1")
                Reg.SetValue("EnableSecureUIAPaths", 1, RegistryValueKind.DWord)
            End If
            If Not Reg.GetValue("EnableUIADesktopToggle") = 0 Then
                AddListview1("EnableUIADesktopToggle", Reg.GetValue("EnableUIADesktopToggle"), "WindowsPoliciesSystem", "Đã thay đổi thành 0")
                Reg.SetValue("EnableUIADesktopToggle", 0, RegistryValueKind.DWord)
            End If
            If Not Reg.GetValue("EnableVirtualization") = 1 Then
                AddListview1("EnableVirtualization", Reg.GetValue("EnableVirtualization"), "WindowsPoliciesSystem", "Đã thay đổi thành 1")
                Reg.SetValue("EnableVirtualization", 1, RegistryValueKind.DWord)
            End If
            If Not Reg.GetValue("FilterAdministratorToken") = 0 Then
                AddListview1("FilterAdministratorToken", Reg.GetValue("FilterAdministratorToken"), "WindowsPoliciesSystem", "Đã thay đổi thành 0")
                Reg.SetValue("FilterAdministratorToken", 0, RegistryValueKind.DWord)
            End If
            If Not Reg.GetValue("shutdownwithoutlogon") = 1 Then
                AddListview1("shutdownwithoutlogon", Reg.GetValue("shutdownwithoutlogon"), "WindowsPoliciesSystem", "Đã thay đổi thành 1")
                Reg.SetValue("shutdownwithoutlogon", 1, RegistryValueKind.DWord)
            End If
            If Not Reg.GetValue("undockwithoutlogon") = 1 Then
                AddListview1("undockwithoutlogon", Reg.GetValue("undockwithoutlogon"), "WindowsPoliciesSystem", "Đã thay đổi thành 1")
                Reg.SetValue("undockwithoutlogon", 1, RegistryValueKind.DWord)
            End If
            If Not Reg.GetValue("ValidateAdminCodeSignatures") = 0 Then
                AddListview1("ValidateAdminCodeSignatures", Reg.GetValue("ValidateAdminCodeSignatures"), "WindowsPoliciesSystem", "Đã thay đổi thành 0")
                Reg.SetValue("ValidateAdminCodeSignatures", 0, RegistryValueKind.DWord)
            End If
            If Not Reg.GetValue("DSCAutomationHostEnabled") = "2" Then
                AddListview1("DSCAutomationHostEnabled", Reg.GetValue("DSCAutomationHostEnabled"), "WindowsPoliciesSystem", "Đã thay đổi thành 2")
                Reg.SetValue("DSCAutomationHostEnabled", "2", RegistryValueKind.DWord)
            End If
            If Not Reg.GetValue("legalnoticecaption") = "" Then
                AddListview1("legalnoticecaption", Reg.GetValue("legalnoticecaption"), "WindowsPoliciesSystem", "Đã thay đổi thành vô giá trị")
                Reg.SetValue("legalnoticecaption", "", RegistryValueKind.String)
            End If
            If Not Reg.GetValue("legalnoticetext") = "" Then
                AddListview1("legalnoticetext", Reg.GetValue("legalnoticetext"), "WindowsPoliciesSystem", "Đã thay đổi thành vô giá trị")
                Reg.SetValue("legalnoticetext", "", RegistryValueKind.String)
            End If
        End If

        Reg = Registry.CurrentUser.OpenSubKey("SOFTWARE\Microsoft\Windows\CurrentVersion\Policies\Comdlg32", True)
        If Reg IsNot Nothing Then
            If Not Reg.GetValue("NoBackButton") = False Then
                AddListview1("NoBackButton", Reg.GetValue("NoBackButton"), "WindowsPoliciesSystem", "Gây nguy hại cho hệ thống, đã loại bỏ !")
                Reg.DeleteValue("NoBackButton")
            End If
        End If

    End Sub
    Private Sub EnableTaskbar()
        Reg = Registry.CurrentUser.OpenSubKey("SOFTWARE\Policies\Microsoft\Explorer", True)
        If Reg IsNot Nothing Then
            If Not Reg.GetValue("NoPinningStoreToTaskbar") = "1" Then
                AddListview1("NoPinningStoreToTaskbar", Reg.GetValue("NoPinningStoreToTaskbar"), "Taskbar", "Đã thay đổi thành 1")
                Reg.SetValue("NoPinningStoreToTaskbar", "1", RegistryValueKind.DWord)
            End If
            If Not Reg.GetValue("TaskbarNoResize") = "1" Then
                AddListview1("TaskbarNoResize", Reg.GetValue("TaskbarNoResize"), "Taskbar", "Đã thay đổi thành 1")
                Reg.SetValue("TaskbarNoResize", "1", RegistryValueKind.DWord)
            End If
        End If


        Reg = Registry.CurrentUser.OpenSubKey("SOFTWARE\Microsoft\Windows\CurrentVersion\Explorer\Advanced", True)
        If Reg IsNot Nothing Then
            If WinExt.PCInfo.IsWinXP = True Then
                If Not Reg.GetValue("TaskbarGlomming") = "0" Then
                    AddListview1("TaskbarGlomming", Reg.GetValue("TaskbarGlomming"), "Taskbar", "Đã thay đổi thành 0")
                    Reg.SetValue("TaskbarGlomming", "0", RegistryValueKind.DWord)
                End If
            Else
                If Not Reg.GetValue("TaskbarGlomLevel") = "2" Then
                    AddListview1("TaskbarGlomLevel", Reg.GetValue("TaskbarGlomLevel"), "Taskbar", "Đã thay đổi thành 2")
                    Reg.SetValue("TaskbarGlomLevel", "2", RegistryValueKind.DWord)
                End If
            End If

            If WinExt.PCInfo.IsWin10 = True Then
                If Not Reg.GetValue("ShowTaskViewButton") = "0" Then
                    AddListview1("ShowTaskViewButton", Reg.GetValue("ShowTaskViewButton"), "Taskbar", "Đã thay đổi thành 0")
                    Reg.SetValue("ShowTaskViewButton", "0", RegistryValueKind.DWord)
                End If
            End If


        End If

        If WinExt.PCInfo.IsWin10 = True Then
            Reg = Registry.CurrentUser.OpenSubKey("SOFTWARE\Microsoft\Windows\CurrentVersion\Search", True)
            If Reg IsNot Nothing Then
                If Not Reg.GetValue("SearchboxTaskbarMode") = "0" Then
                    AddListview1("SearchboxTaskbarMode", Reg.GetValue("SearchboxTaskbarMode"), "Taskbar", "Đã thay đổi thành 0")
                    Reg.SetValue("SearchboxTaskbarMode", "0", RegistryValueKind.DWord)
                End If
            End If

            Reg = Registry.CurrentUser.OpenSubKey("SOFTWARE\Policies\Microsoft\Windows Search", True)
            If Reg IsNot Nothing Then
                If Not Reg.GetValue("AllowCortana") = "0" Then
                    AddListview1("AllowCortana", Reg.GetValue("AllowCortana"), "Taskbar", "Đã thay đổi thành 0")
                    Reg.SetValue("AllowCortana", "0", RegistryValueKind.DWord)
                End If
            End If
        End If

        Reg = Registry.CurrentUser.OpenSubKey("SOFTWARE\Microsoft\Windows\CurrentVersion\Explorer", True)
        If Reg IsNot Nothing Then
            If Not Reg.GetValue("EnableAutoTray") = "1" Then
                AddListview1("EnableAutoTray", Reg.GetValue("EnableAutoTray"), "Taskbar", "Đã thay đổi thành 1")
                Reg.SetValue("EnableAutoTray", "1", RegistryValueKind.DWord)
            End If
        End If



    End Sub
    Private Sub EnableDesktop()
        Reg = Registry.CurrentUser.OpenSubKey("SOFTWARE\Microsoft\Windows\CurrentVersion\Explorer\Advanced", True)
        If Reg IsNot Nothing Then
            If Not Reg.GetValue("DisablePreviewDesktop") = "1" Then
                AddListview1("DisablePreviewDesktop", Reg.GetValue("DisablePreviewDesktop"), "Desktop", "Đã thay đổi thành 1")
                Reg.SetValue("DisablePreviewDesktop", "1", RegistryValueKind.DWord)
            End If
        End If

        Reg = Registry.ClassesRoot.OpenSubKey("lnkfile", True)
        If Reg IsNot Nothing Then
            If Not Reg.GetValue("IsShortcut") = "" Then
                AddListview1("IsShortcut", Reg.GetValue("IsShortcut"), "Desktop", "Đã thay đổi thành 1")
                Reg.SetValue("IsShortcut", "", RegistryValueKind.String)
            End If
        End If

        Reg = Registry.LocalMachine.OpenSubKey("SOFTWARE\Microsoft\Windows\CurrentVersion\Policies\BuildAndTel", True)
        If Reg IsNot Nothing Then
            If Not Reg.GetValue("EnableBuildPreview") = "0" Then
                AddListview1("EnableBuildPreview", Reg.GetValue("EnableBuildPreview"), "Desktop", "Đã thay đổi thành 0")
                Reg.SetValue("EnableBuildPreview", "0", RegistryValueKind.DWord)
            End If
        End If

        If WinExt.PCInfo.IsWinXP = True Then
            Reg = Registry.CurrentUser.OpenSubKey("SOFTWARE\Microsoft\Windows\CurrentVersion\Explorer\Desktop\CleanupWiz", True)
            If Reg IsNot Nothing Then
                If Not Reg.GetValue("NoRun") = "1" Then
                    AddListview1("NoRun", Reg.GetValue("NoRun"), "Desktop", "Đã thay đổi thành 1")
                    Reg.SetValue("NoRun", "1", RegistryValueKind.DWord)
                End If
            End If
        End If

        If WinExt.PCInfo.IsWin7 = True Then
            Reg = Registry.CurrentUser.OpenSubKey("SOFTWARE\Microsoft\Windows\CurrentVersion\Policies\Explorer", True)
            If Reg IsNot Nothing Then
                If Not Reg.GetValue("NoDesktopCleanupWizard") = "1" Then
                    AddListview1("NoDesktopCleanupWizard", Reg.GetValue("NoDesktopCleanupWizard"), "Desktop", "Đã thay đổi thành 1")
                    Reg.SetValue("NoDesktopCleanupWizard", "1", RegistryValueKind.DWord)
                End If
            End If
        End If

        Reg = Registry.CurrentUser.OpenSubKey("SOFTWARE\Policies\Microsoft\Windows\Control Panel\Desktop", True)
        If Reg IsNot Nothing Then
            If Not Reg.GetValue("ScreenSaveActive") = "1" Then
                AddListview1("ScreenSaveActive", Reg.GetValue("ScreenSaveActive"), "Desktop", "Đã thay đổi thành 1")
                Reg.SetValue("ScreenSaveActive", "1", RegistryValueKind.DWord)
            End If
            If Not Reg.GetValue("ScreenSaverIsSecure") Is Nothing Then
                AddListview1("ScreenSaverIsSecure", Reg.GetValue("ScreenSaverIsSecure"), "Desktop", "Gây nguy hại cho hệ thống, đã loại bỏ !")
                Reg.DeleteValue("ScreenSaverIsSecure")
            End If
            If Not Reg.GetValue("ScreenSaveTimeOut") Is Nothing Then
                AddListview1("ScreenSaveTimeOut", Reg.GetValue("ScreenSaveTimeOut"), "Desktop", "Gây nguy hại cho hệ thống, đã loại bỏ !")
                Reg.DeleteValue("ScreenSaveTimeOut")
            End If
            If WinExt.PCInfo.IsWinXP = True Then
                If Not Reg.GetValue("SCRNSAVE.EXE") Is Nothing Then
                    AddListview1("SCRNSAVE.EXE", Reg.GetValue("SCRNSAVE.EXE"), "Desktop", "Gây nguy hại cho hệ thống, đã loại bỏ !")
                    Reg.DeleteValue("SCRNSAVE.EXE")
                End If
            End If
        End If

        Reg = Registry.CurrentUser.OpenSubKey("SOFTWARE\Policies\Microsoft\Windows\Personalization", True)
        If Reg IsNot Nothing Then
            If Not Reg.GetValue("ThemeFile") Is Nothing Then
                AddListview1("ThemeFile", Reg.GetValue("ThemeFile"), "Desktop", "Gây nguy hại cho hệ thống, đã loại bỏ !")
                Reg.DeleteValue("ThemeFile")
            End If
            If Not Reg.GetValue("NoChangingMousePointers") Is Nothing Then
                AddListview1("NoChangingMousePointers", Reg.GetValue("NoChangingMousePointers"), "Desktop", "Gây nguy hại cho hệ thống, đã loại bỏ !")
                Reg.DeleteValue("NoChangingMousePointers")
            End If
            If Not Reg.GetValue("NoChangingSoundScheme") Is Nothing Then
                AddListview1("NoChangingSoundScheme", Reg.GetValue("NoChangingSoundScheme"), "Desktop", "Gây nguy hại cho hệ thống, đã loại bỏ !")
                Reg.DeleteValue("NoChangingSoundScheme")
            End If
        End If

        Reg = Registry.CurrentUser.OpenSubKey("SOFTWARE\Policies\Microsoft\Windows\InputPersonalization", True)
        If Reg IsNot Nothing Then
            If Not Reg.GetValue("RestrictImplicitTextCollection") = False Then
                AddListview1("RestrictImplicitTextCollection", Reg.GetValue("RestrictImplicitTextCollection"), "Desktop", "Gây nguy hại cho hệ thống, đã loại bỏ !")
                Reg.DeleteValue("RestrictImplicitTextCollection")
            End If
            If Not Reg.GetValue("RestrictImplicitInkCollection") Is Nothing Then
                AddListview1("RestrictImplicitInkCollection", Reg.GetValue("RestrictImplicitInkCollection"), "Desktop", "Gây nguy hại cho hệ thống, đã loại bỏ !")
                Reg.DeleteValue("RestrictImplicitInkCollection")
            End If
        End If

        Reg = Registry.CurrentUser.OpenSubKey("SOFTWARE\Microsoft\Windows\CurrentVersion\Policies\System", True)
        If Reg IsNot Nothing Then
            If Not Reg.GetValue("NoDispScrSavPage") Is Nothing Then
                AddListview1("NoDispScrSavPage", Reg.GetValue("NoDispScrSavPage"), "Desktop", "Gây nguy hại cho hệ thống, đã loại bỏ !")
                Reg.DeleteValue("NoDispScrSavPage")
            End If
            If Not Reg.GetValue("SetVisualStyle") Is Nothing Then
                AddListview1("SetVisualStyle", Reg.GetValue("SetVisualStyle"), "Desktop", "Gây nguy hại cho hệ thống, đã loại bỏ !")
                Reg.DeleteValue("SetVisualStyle")
            End If
        End If

        Reg = Registry.CurrentUser.OpenSubKey("SOFTWARE\Microsoft\Windows\CurrentVersion\Policies\ActiveDesktop", True)
        If Reg IsNot Nothing Then
            If Not Reg.GetValue("NoChangingWallPaper") Is Nothing Then
                AddListview1("NoChangingWallPaper", Reg.GetValue("NoChangingWallPaper"), "Desktop", "Gây nguy hại cho hệ thống, đã loại bỏ !")
                Reg.DeleteValue("NoChangingWallPaper")
            End If
        End If

        If WinExt.PCInfo.IsWinXP = True Then
            Reg = Registry.LocalMachine.OpenSubKey("SOFTWARE\Microsoft\Windows\CurrentVersion\Applets\Tour", True)
            If Reg IsNot Nothing Then
                If Not Reg.GetValue("RunCount") = "0" Then
                    AddListview1("RunCount", Reg.GetValue("RunCount"), "Desktop", "Đã thay đổi thành 0")
                    Reg.SetValue("RunCount", "0", RegistryValueKind.DWord)
                End If
            End If
        End If

        Reg = Registry.LocalMachine.OpenSubKey("SOFTWARE\Policies\Microsoft\Windows\System", True)
        If Reg IsNot Nothing Then
            If WinExt.PCInfo.IsWinXP = True Then
                If Not Reg.GetValue("EnableSmartScreen") = "0" Then
                    AddListview1("EnableSmartScreen", Reg.GetValue("EnableSmartScreen"), "Desktop", "Đã thay đổi thành 0")
                    Reg.SetValue("EnableSmartScreen", "0", RegistryValueKind.DWord)
                End If
            End If

            If WinExt.PCInfo.IsWin10 = True Then
                If Not Reg.GetValue("DisableLogonBackgroundImage") = "0" Then
                    AddListview1("DisableLogonBackgroundImage", Reg.GetValue("DisableLogonBackgroundImage"), "Desktop", "Đã thay đổi thành 0")
                    Reg.SetValue("DisableLogonBackgroundImage", "0", RegistryValueKind.DWord)
                End If
            End If

        End If

        Reg = Registry.CurrentUser.OpenSubKey("Control Panel\Desktop", True)
        If Reg IsNot Nothing Then
            If Not Reg.GetValue("PaintDesktopVersion") = "0" Then
                AddListview1("PaintDesktopVersion", Reg.GetValue("PaintDesktopVersion"), "Desktop", "Đã thay đổi thành 0")
                Reg.SetValue("PaintDesktopVersion", "0", RegistryValueKind.DWord)
            End If
        End If

        Reg = Registry.LocalMachine.OpenSubKey("SOFTWARE\Microsoft\Windows\CurrentVersion\Explorer\Shell Icons", True)
        If Reg IsNot Nothing Then
            Dim ImageFile As String = FileSystem32("2CongLC.Blank.ico")
            If File.Exists(ImageFile) = False Then
                Dim fs As Stream = File.Create(ImageFile)
                My.Resources._2CongLC_Blank.Save(fs)
                fs.Close()
            End If
            If Not Reg.GetValue("29") = ImageFile Then
                AddListview1("29", Reg.GetValue("29"), "Desktop", "Đã thay đổi thành công !")
                Reg.SetValue("29", ImageFile, RegistryValueKind.String)
            End If
        End If

        Reg = Registry.CurrentUser.OpenSubKey("SOFTWARE\Microsoft\Windows\CurrentVersion\Explorer", True)
        If Reg IsNot Nothing Then
            If Not ByteArrayToHexString(Reg.GetValue("link")) = "00000000" Then
                AddListview1("link", ByteArrayToHexString(Reg.GetValue("link")), "Desktop", "Đã thay đổi thành công !")
                Reg.SetValue("link", CLng(0), RegistryValueKind.Binary)
            End If
        End If
    End Sub
    Private Sub ResetSystemControlSet()

        Reg = Registry.LocalMachine.OpenSubKey("SYSTEM\CurrentControlSet\Control\StorageDevicePolicies", True)
        If Reg IsNot Nothing Then
            If Not Reg.GetValue("WriteProtect") Is Nothing Then
                AddListview1("WriteProtect", Reg.GetValue("WriteProtect"), "ControlSet", "Gây nguy hại cho hệ thống, đã loại bỏ !")
                Reg.DeleteValue("WriteProtect")
            End If
        End If

        Reg = Registry.LocalMachine.OpenSubKey("SYSTEM\CurrentControlSet\Control\Power", True)
        If Reg IsNot Nothing Then
            If Not Reg.GetValue("HibernateEnabled") = "0" Then
                AddListview1("HibernateEnabled", Reg.GetValue("HibernateEnabled"), "ControlSet", "Đã thay đổi thành 0")
                Reg.SetValue("HibernateEnabled", "0", RegistryValueKind.DWord)
            End If
        End If

        If WinExt.PCInfo.IsWin10 = True Then
            Reg = Registry.LocalMachine.OpenSubKey("SYSTEM\CurrentControlSet\Control\Session Manager\Power", True)
            If Reg IsNot Nothing Then
                If Not Reg.GetValue("HiberbootEnabled") = "0" Then
                    AddListview1("HiberbootEnabled", Reg.GetValue("HiberbootEnabled"), "ControlSet", "Đã thay đổi thành 0")
                    Reg.SetValue("HiberbootEnabled", "0", RegistryValueKind.DWord)
                End If
            End If
        End If
    End Sub

    Private Sub EnableNetwork()

        Reg = Registry.CurrentUser.OpenSubKey("SYSTEM\CurrentControlSet\Control\Lsa", True)
        If Reg IsNot Nothing Then
            If Not Reg.GetValue("ForceGuest") = 1 Then
                AddListview1("ForceGuest", Reg.GetValue("ForceGuest"), "Network", "Đã thay đổi thành 1")
                Reg.SetValue("ForceGuest", "1", RegistryValueKind.DWord)
            End If
        End If

        If WinExt.PCInfo.IsWinXP = True Then
            Reg = Registry.LocalMachine.OpenSubKey("SYSTEM\ControlSet001\Hardware Profiles\0001\Software\Microsoft\windows\CurrentVersion\Internet Settings", True)
            If Reg IsNot Nothing Then
                If Not Reg.GetValue("ProxyEnable") = "0" Then
                    AddListview1("ProxyEnable", Reg.GetValue("ProxyEnable"), "Network", "Đã thay đổi thành 0")
                    Reg.SetValue("ProxyEnable", "0", RegistryValueKind.DWord)
                End If
            End If
        End If

        Reg = Registry.CurrentUser.OpenSubKey("SOFTWARE\Microsoft\Windows\CurrentVersion\Internet Settings", True)
        If Reg IsNot Nothing Then
            If Not Reg.GetValue("ProxyEnable") = "0" Then
                AddListview1("ProxyEnable", Reg.GetValue("ProxyEnable"), "Network", "Đã thay đổi thành 0")
                Reg.SetValue("ProxyEnable", "0", RegistryValueKind.DWord)
            End If
        End If

        Reg = Registry.CurrentUser.OpenSubKey("SOFTWARE\Microsoft\Windows\CurrentVersion\Internet Settings\Connections", True)
        If Reg IsNot Nothing Then
            If WinExt.PCInfo.IsWinXP = True Then

            End If
            If WinExt.PCInfo.IsWin7 = True Then

            End If
            If WinExt.PCInfo.IsWin8 = True Then

            End If
            If WinExt.PCInfo.IsWin81 = True Then

            End If
            If WinExt.PCInfo.IsWin10 = True Then
                If Not ByteArrayToHexString(Reg.GetValue("DefaultConnectionSettings")) = "4600000017000000090000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000" Then
                    AddListview1("DefaultConnectionSettings", ByteArrayToHexString(Reg.GetValue("DefaultConnectionSettings")), "Network", "Đã thay đổi về mặc định")
                    Reg.SetValue("DefaultConnectionSettings", HexStringToByteArray("4600000017000000090000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000"), RegistryValueKind.Binary)
                End If
                If Not ByteArrayToHexString(Reg.GetValue("SavedLegacySettings")) = "4600000014000000010000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000" Then
                    AddListview1("SavedLegacySettings", ByteArrayToHexString(Reg.GetValue("SavedLegacySettings")), "Network", "Đã thay đổi về mặc định")
                    Reg.SetValue("SavedLegacySettings", HexStringToByteArray("4600000014000000010000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000"), RegistryValueKind.Binary)
                End If
            End If
        End If

        Reg = Registry.CurrentUser.OpenSubKey("SOFTWARE\Policies\Microsoft\Windows NT\SharedFolders", True)
        If Reg IsNot Nothing Then
            If Not Reg.GetValue("PublishDfsRoots") = False Then
                AddListview1("PublishDfsRoots", Reg.GetValue("PublishDfsRoots"), "Network", "Gây nguy hại cho hệ thống, đã loại bỏ !")
                Reg.DeleteValue("PublishDfsRoots")
            End If
            If Not Reg.GetValue("PublishSharedFolders") = False Then
                AddListview1("PublishSharedFolders", Reg.GetValue("PublishSharedFolders"), "Network", "Gây nguy hại cho hệ thống, đã loại bỏ !")
                Reg.DeleteValue("PublishSharedFolders")
            End If
        End If

        Reg = Registry.CurrentUser.OpenSubKey("SOFTWARE\Microsoft\Windows\CurrentVersion\Policies\Network", True)
        If Reg IsNot Nothing Then
            If Not Reg.GetValue("NoEntireNetwork") = False Then
                AddListview1("NoEntireNetwork", Reg.GetValue("NoEntireNetwork"), "Network", "Gây nguy hại cho hệ thống, đã loại bỏ !")
                Reg.DeleteValue("NoEntireNetwork")
            End If
            If Not Reg.GetValue("NoFileSharing") = False Then
                AddListview1("NoFileSharing", Reg.GetValue("NoFileSharing"), "Network", "Gây nguy hại cho hệ thống, đã loại bỏ !")
                Reg.DeleteValue("NoFileSharing")
            End If
            If Not Reg.GetValue("NoPrintSharing") = False Then
                AddListview1("NoPrintSharing", Reg.GetValue("NoPrintSharing"), "Network", "Gây nguy hại cho hệ thống, đã loại bỏ !")
                Reg.DeleteValue("NoPrintSharing")
            End If
        End If

    End Sub
    Private Sub EnableFolders()

        Reg = Registry.ClassesRoot.OpenSubKey("Folder", True)
        If Reg IsNot Nothing Then
            If WinExt.PCInfo.IsWinXP = True Then
                If Not Reg.GetValue("shell") = "explorer" Then
                    AddListview1("shell", Reg.GetValue("shell"), "Folder", "Đã thay đổi thành explorer")
                    Reg.SetValue("explorer", "0", RegistryValueKind.String)
                End If
            Else
                If Not Reg.GetValue("shell") = "" Then
                    AddListview1("shell", Reg.GetValue("shell"), "Folder", "Đã thay đổi thành mặc định")
                    Reg.SetValue("shell", "", RegistryValueKind.String)
                End If
            End If
        End If

        Reg = Registry.CurrentUser.OpenSubKey("SOFTWARE\Microsoft\Windows\CurrentVersion\Explorer\Advanced", True)
        If Reg IsNot Nothing Then
            If Not Reg.GetValue("HideFileExt") = "0" Then
                AddListview1("HideFileExt", Reg.GetValue("HideFileExt"), "Folder", "Đã thay đổi thành 0")
                Reg.SetValue("HideFileExt", "0", RegistryValueKind.DWord)
            End If

            If WinExt.PCInfo.IsWin10 = True Then
                If Not Reg.GetValue("LaunchTo") = "1" Then
                    AddListview1("LaunchTo", Reg.GetValue("LaunchTo"), "Folder", "Đã thay đổi thành 1")
                    Reg.SetValue("LaunchTo", "1", RegistryValueKind.DWord)
                End If
            End If
        End If

        Reg = Registry.LocalMachine.OpenSubKey("SOFTWARE\Microsoft\Windows\CurrentVersion\Explorer", True)
        If Reg IsNot Nothing Then
            If Not Reg.GetValue("ShowDriveLettersFirst") = "4" Then
                AddListview1("ShowDriveLettersFirst", Reg.GetValue("ShowDriveLettersFirst"), "Folder", "Đã thay đổi thành 4")
                Reg.SetValue("ShowDriveLettersFirst", "4", RegistryValueKind.DWord)
            End If
        End If
    End Sub

    Private Sub DisableMetro(Optional Enable As Boolean = True)
        If Enable = True Then

            If WinExt.PCInfo.IsWin8 = True Or WinExt.PCInfo.IsWin81 = True Then
                Reg = Registry.CurrentUser.OpenSubKey("SOFTWARE\Microsoft\Windows\CurrentVersion\Explorer", True)
                If Reg IsNot Nothing Then
                    If Not Reg.GetValue("RPEnabled") = "0" Then
                        AddListview1("RPEnabled", Reg.GetValue("RPEnabled"), "Startup", "Đã thay đổi thành 0")
                        Reg.SetValue("RPEnabled", "0", RegistryValueKind.DWord)
                    End If
                End If

                Reg = Registry.LocalMachine.OpenSubKey("SOFTWARE\Microsoft\Windows NT\CurrentVersion\Winlogon", True)
                If Reg IsNot Nothing Then
                    If Not Reg.GetValue("Shell") = "explorer.exe /select,explorer.exe" Then
                        AddListview1("Shell", Reg.GetValue("Shell"), "Startup", "Đã thay đổi thành explorer.exe /select,explorer.exe")
                        Reg.SetValue("Shell", "explorer.exe /select,explorer.exe", RegistryValueKind.String)
                    End If
                End If

            End If

        End If

    End Sub
    Private Sub DisableAutoPlay(Optional Enable As Boolean = True)
        If Enable = True Then

            If WinExt.PCInfo.IsWinXP = True Then
                Reg = Registry.CurrentUser.OpenSubKey("SOFTWARE\Microsoft\Windows\CurrentVersion\Explorer\AutoplayHandlers", True)
                If Reg IsNot Nothing Then
                    If Not Reg.GetValue("DisableAutoplay") = "1" Then
                        AddListview1("DisableAutoplay", Reg.GetValue("DisableAutoplay"), "AutoPlay", "Đã thay đổi thành 1")
                        Reg.SetValue("DisableAutoplay", "1", RegistryValueKind.DWord)
                    End If
                End If
            End If

            Reg = Registry.CurrentUser.OpenSubKey("SOFTWARE\Microsoft\Windows\CurrentVersion\Policies\Explorer", True)
            If Reg IsNot Nothing Then
                If Not Reg.GetValue("NoDriveTypeAutoRun") = "255" Then
                    AddListview1("NoDriveTypeAutoRun", Reg.GetValue("NoDriveTypeAutoRun"), "AutoPlay", "Đã thay đổi thành 255")
                    Reg.SetValue("NoDriveTypeAutoRun", "255", RegistryValueKind.DWord)
                End If
                If Not Reg.GetValue("HonorAutorunSetting") = "1" Then
                    AddListview1("HonorAutorunSetting", Reg.GetValue("HonorAutorunSetting"), "AutoPlay", "Đã thay đổi thành 1")
                    Reg.SetValue("HonorAutorunSetting", "1", RegistryValueKind.DWord)
                End If
            End If

            Reg = Registry.LocalMachine.OpenSubKey("SOFTWARE\Microsoft\Windows NT\CurrentVersion\IniFileMapping", True)
            If Reg IsNot Nothing Then
                If Not Reg.GetValue("Autorun.inf") = "@SYS:DoesNotExist" Then
                    AddListview1("Autorun.inf", Reg.GetValue("Autorun.inf"), "AutoPlay", "Đã thay đổi thành @SYS:DoesNotExist")
                    Reg.SetValue("Autorun.inf", "@SYS:DoesNotExist", RegistryValueKind.String)
                End If
            End If

        End If
    End Sub

    Private Sub DisableCrashControl(Optional Enable As Boolean = True)
        If Enable = True Then

            Reg = Registry.LocalMachine.OpenSubKey("SYSTEM\ControlSet001\Control\CrashControl", True)
            If Reg IsNot Nothing Then
                If Not Reg.GetValue("LogEvent") = "0" Then
                    AddListview1("LogEvent", Reg.GetValue("LogEvent"), "CrashControl", "Đã thay đổi thành 0")
                    Reg.SetValue("LogEvent", "0", RegistryValueKind.DWord)
                End If
                If Not Reg.GetValue("SendAlert") = "0" Then
                    AddListview1("SendAlert", Reg.GetValue("SendAlert"), "CrashControl", "Đã thay đổi thành 0")
                    Reg.SetValue("SendAlert", "0", RegistryValueKind.DWord)
                End If
                If Not Reg.GetValue("AutoReboot") = "0" Then
                    AddListview1("AutoReboot", Reg.GetValue("AutoReboot"), "CrashControl", "Đã thay đổi thành 0")
                    Reg.SetValue("AutoReboot", "0", RegistryValueKind.DWord)
                End If
                If Not Reg.GetValue("AlwaysKeepMemoryDump") = "0" Then
                    AddListview1("AlwaysKeepMemoryDump", Reg.GetValue("AlwaysKeepMemoryDump"), "CrashControl", "Đã thay đổi thành 0")
                    Reg.SetValue("AlwaysKeepMemoryDump", "0", RegistryValueKind.DWord)
                End If
            End If

            Reg = Registry.LocalMachine.OpenSubKey("SOFTWARE\Microsoft\PCHealth\ErrorReporting", True)
            If Reg IsNot Nothing Then
                If Not Reg.GetValue("DoReport") = "0" Then
                    AddListview1("DoReport", Reg.GetValue("DoReport"), "CrashControl", "Đã thay đổi thành 0")
                    Reg.SetValue("DoReport", "0", RegistryValueKind.DWord)
                End If
            End If

        End If
    End Sub
    Private Sub DisableSecurity(Optional Enable As Boolean = True)
        If Enable = True Then
            Reg = Registry.LocalMachine.OpenSubKey("SOFTWARE\Microsoft\Security Center", True)
            If Reg IsNot Nothing Then
                If WinExt.PCInfo.IsWinXP = True Then
                    If Not Reg.GetValue("FirewallDisableNotify") = "1" Then
                        AddListview1("FirewallDisableNotify", Reg.GetValue("FirewallDisableNotify"), "Security", "Đã thay đổi thành 1")
                        Reg.SetValue("FirewallDisableNotify", "1", RegistryValueKind.DWord)
                    End If
                    If Not Reg.GetValue("UpdatesDisableNotify") = "1" Then
                        AddListview1("UpdatesDisableNotify", Reg.GetValue("UpdatesDisableNotify"), "Security", "Đã thay đổi thành 1")
                        Reg.SetValue("UpdatesDisableNotify", "1", RegistryValueKind.DWord)
                    End If
                    If Not Reg.GetValue("AntiVirusDisableNotify") = "1" Then
                        AddListview1("AntiVirusDisableNotify", Reg.GetValue("AntiVirusDisableNotify"), "Security", "Đã thay đổi thành 1")
                        Reg.SetValue("AntiVirusDisableNotify", "1", RegistryValueKind.DWord)
                    End If
                End If

            End If

            If WinExt.PCInfo.IsWinXP = False Then
                Reg = Registry.LocalMachine.OpenSubKey("SOFTWARE\Policies\Windows Defender", True)
                If Reg IsNot Nothing Then
                    If Not Reg.GetValue("DisableAntiSpyware") = "0" Then
                        AddListview1("DisableAntiSpyware", Reg.GetValue("DisableAntiSpyware"), "Security", "Đã thay đổi thành 0")
                        Reg.SetValue("DisableAntiSpyware", "0", RegistryValueKind.DWord)
                    End If
                End If

                Reg = Registry.LocalMachine.OpenSubKey("SOFTWARE\Policies\Windows Defender\Real-Time Protection", True)
                If Reg IsNot Nothing Then
                    If Not Reg.GetValue("EnableUnknownPrompts") = "0" Then
                        AddListview1("EnableUnknownPrompts", Reg.GetValue("DisableAntiSpyware"), "Security", "Đã thay đổi thành 0")
                        Reg.SetValue("EnableUnknownPrompts", "0", RegistryValueKind.DWord)
                    End If
                End If

                Reg = Registry.LocalMachine.OpenSubKey("SOFTWARE\Policies\Windows Defender\Scan", True)
                If Reg IsNot Nothing Then
                    If Not Reg.GetValue("CheckForSignaturesBeforeRunningScan") = "0" Then
                        AddListview1("CheckForSignaturesBeforeRunningScan", Reg.GetValue("CheckForSignaturesBeforeRunningScan"), "Security", "Đã thay đổi thành 0")
                        Reg.SetValue("CheckForSignaturesBeforeRunningScan", "0", RegistryValueKind.DWord)
                    End If
                End If

                Reg = Registry.LocalMachine.OpenSubKey("SOFTWARE\Policies\Windows Defender\Signature Updates", True)
                If Reg IsNot Nothing Then
                    If Not Reg.GetValue("ForceFullUpdate") = "0" Then
                        AddListview1("ForceFullUpdate", Reg.GetValue("ForceFullUpdate"), "Security", "Đã thay đổi thành 0")
                        Reg.SetValue("ForceFullUpdate", "0", RegistryValueKind.DWord)
                    End If
                    If Not Reg.GetValue("CheckAlternateDownloadLocation") = "0" Then
                        AddListview1("CheckAlternateDownloadLocation", Reg.GetValue("CheckAlternateDownloadLocation"), "Security", "Đã thay đổi thành 0")
                        Reg.SetValue("CheckAlternateDownloadLocation", "0", RegistryValueKind.DWord)
                    End If
                End If

                Reg = Registry.LocalMachine.OpenSubKey("SOFTWARE\Policies\Windows Defender\SpyNet", True)
                If Reg IsNot Nothing Then
                    If Not Reg.GetValue("SpyNetReporting") = "0" Then
                        AddListview1("SpyNetReporting", Reg.GetValue("SpyNetReporting"), "Security", "Đã thay đổi thành 0")
                        Reg.SetValue("SpyNetReporting", "0", RegistryValueKind.DWord)
                    End If
                End If

                Reg = Registry.LocalMachine.OpenSubKey("SOFTWARE\Policies\Windows Defender\Reporting", True)
                If Reg IsNot Nothing Then
                    If Not Reg.GetValue("DisableLoggingForKnownGood") = "0" Then
                        AddListview1("DisableLoggingForKnownGood", Reg.GetValue("DisableLoggingForKnownGood"), "Security", "Đã thay đổi thành 0")
                        Reg.SetValue("DisableLoggingForKnownGood", "0", RegistryValueKind.DWord)
                    End If
                    If Not Reg.GetValue("DisableLoggingForUnknown") = "0" Then
                        AddListview1("DisableLoggingForUnknown", Reg.GetValue("DisableLoggingForUnknown"), "Security", "Đã thay đổi thành 0")
                        Reg.SetValue("DisableLoggingForUnknown", "0", RegistryValueKind.DWord)
                    End If
                End If

            End If

        End If
    End Sub
    Private Sub DisableWindowsUpdate(Optional Enable As Boolean = True)
        If Enable = True Then

            If WinExt.PCInfo.IsWinXP = False Then
                Reg = Registry.CurrentUser.OpenSubKey("SOFTWARE\Policies\Windows\WindowsUpdate\AU", True)
                If Reg IsNot Nothing Then
                    If Not Reg.GetValue("NoAutoUpdate") = "1" Then
                        AddListview1("NoAutoUpdate", Reg.GetValue("NoAutoUpdate"), "WindowsUpdate", "Đã thay đổi thành 1")
                        Reg.SetValue("NoAutoUpdate", "1", RegistryValueKind.DWord)
                    End If
                    If Not Reg.GetValue("DetectionFrequencyEnabled") = "0" Then
                        AddListview1("DetectionFrequencyEnabled", Reg.GetValue("DetectionFrequencyEnabled"), "WindowsUpdate", "Đã thay đổi thành 0")
                        Reg.SetValue("DetectionFrequencyEnabled", "0", RegistryValueKind.DWord)
                    End If
                    If Not Reg.GetValue("DetectionFrequency") Is Nothing Then
                        AddListview1("DetectionFrequency", Reg.GetValue("DetectionFrequency"), "WindowsUpdate", "Gây nguy hại cho hệ thống, đã loại bỏ !")
                        Reg.DeleteValue("DetectionFrequency")
                    End If
                    If Not Reg.GetValue("AUOptions") = "2" Then
                        AddListview1("AUOptions", Reg.GetValue("AUOptions"), "WindowsUpdate", "Đã thay đổi thành 2")
                        Reg.SetValue("AUOptions", "2", RegistryValueKind.DWord)
                    End If
                End If

                Reg = Registry.LocalMachine.OpenSubKey("SOFTWARE\Policies\Windows\WindowsUpdate\AU", True)
                If Reg IsNot Nothing Then
                    If Not Reg.GetValue("NoAutoUpdate") = "1" Then
                        AddListview1("NoAutoUpdate", Reg.GetValue("NoAutoUpdate"), "WindowsUpdate", "Đã thay đổi thành 1")
                        Reg.SetValue("NoAutoUpdate", "1", RegistryValueKind.DWord)
                    End If
                    If Not Reg.GetValue("DetectionFrequencyEnabled") = "0" Then
                        AddListview1("DetectionFrequencyEnabled", Reg.GetValue("DetectionFrequencyEnabled"), "WindowsUpdate", "Đã thay đổi thành 0")
                        Reg.SetValue("DetectionFrequencyEnabled", "0", RegistryValueKind.DWord)
                    End If
                    If Not Reg.GetValue("DetectionFrequency") Is Nothing Then
                        AddListview1("DetectionFrequency", Reg.GetValue("DetectionFrequency"), "WindowsUpdate", "Gây nguy hại cho hệ thống, đã loại bỏ !")
                        Reg.DeleteValue("DetectionFrequency")
                    End If
                    If Not Reg.GetValue("AUOptions") = "2" Then
                        AddListview1("AUOptions", Reg.GetValue("AUOptions"), "WindowsUpdate", "Đã thay đổi thành 2")
                        Reg.SetValue("AUOptions", "2", RegistryValueKind.DWord)
                    End If
                End If

                Reg = Registry.CurrentUser.OpenSubKey("SOFTWARE\Policies\Windows\WindowsUpdate", True)
                If Reg IsNot Nothing Then
                    If Not Reg.GetValue("DeferUpgrade") = "0" Then
                        AddListview1("DeferUpgrade", Reg.GetValue("DeferUpgrade"), "WindowsUpdate", "Đã thay đổi thành 0")
                        Reg.SetValue("DeferUpgrade", "0", RegistryValueKind.DWord)
                    End If
                    If Not Reg.GetValue("DisableOSUpgrade") = "1" Then
                        AddListview1("DisableOSUpgrade", Reg.GetValue("DisableOSUpgrade"), "WindowsUpdate", "Đã thay đổi thành 1")
                        Reg.SetValue("DisableOSUpgrade", "1", RegistryValueKind.DWord)
                    End If
                End If

                Reg = Registry.LocalMachine.OpenSubKey("SOFTWARE\Policies\Windows\WindowsUpdate", True)
                If Reg IsNot Nothing Then
                    If Not Reg.GetValue("DeferUpgrade") = "0" Then
                        AddListview1("DeferUpgrade", Reg.GetValue("DeferUpgrade"), "WindowsUpdate", "Đã thay đổi thành 0")
                        Reg.SetValue("DeferUpgrade", "0", RegistryValueKind.DWord)
                    End If
                    If Not Reg.GetValue("DisableOSUpgrade") = "1" Then
                        AddListview1("DisableOSUpgrade", Reg.GetValue("DisableOSUpgrade"), "WindowsUpdate", "Đã thay đổi thành 1")
                        Reg.SetValue("DisableOSUpgrade", "1", RegistryValueKind.DWord)
                    End If
                End If

                Reg = Registry.CurrentUser.OpenSubKey("SOFTWARE\Policies\Windows\GWX", True)
                If Reg IsNot Nothing Then
                    If Not Reg.GetValue("DisableGWX") = "1" Then
                        AddListview1("DisableGWX", Reg.GetValue("DisableGWX"), "WindowsUpdate", "Đã thay đổi thành 1")
                        Reg.SetValue("DisableGWX", "1", RegistryValueKind.DWord)
                    End If
                End If

                Reg = Registry.LocalMachine.OpenSubKey("SOFTWARE\Policies\Windows\GWX", True)
                If Reg IsNot Nothing Then
                    If Not Reg.GetValue("DisableGWX") = "1" Then
                        AddListview1("DisableGWX", Reg.GetValue("DisableGWX"), "WindowsUpdate", "Đã thay đổi thành 1")
                        Reg.SetValue("DisableGWX", "1", RegistryValueKind.DWord)
                    End If
                End If

                Reg = Registry.CurrentUser.OpenSubKey("SOFTWARE\Microsoft\Windows\CurrentVersion\WindowsUpdate\OSUpgrade", True)
                If Reg IsNot Nothing Then
                    If Not Reg.GetValue("AllowOSUpgrade") = "0" Then
                        AddListview1("AllowOSUpgrade", Reg.GetValue("AllowOSUpgrade"), "WindowsUpdate", "Đã thay đổi thành 0")
                        Reg.SetValue("AllowOSUpgrade", "0", RegistryValueKind.DWord)
                    End If
                    If Not Reg.GetValue("ReservationsAllowed") = "0" Then
                        AddListview1("ReservationsAllowed", Reg.GetValue("ReservationsAllowed"), "WindowsUpdate", "Đã thay đổi thành 0")
                        Reg.SetValue("ReservationsAllowed", "0", RegistryValueKind.DWord)
                    End If
                End If

                Reg = Registry.LocalMachine.OpenSubKey("SOFTWARE\Microsoft\Windows\CurrentVersion\WindowsUpdate\OSUpgrade", True)
                If Reg IsNot Nothing Then
                    If Not Reg.GetValue("AllowOSUpgrade") = "0" Then
                        AddListview1("AllowOSUpgrade", Reg.GetValue("AllowOSUpgrade"), "WindowsUpdate", "Đã thay đổi thành 0")
                        Reg.SetValue("AllowOSUpgrade", "0", RegistryValueKind.DWord)
                    End If
                    If Not Reg.GetValue("ReservationsAllowed") = "0" Then
                        AddListview1("ReservationsAllowed", Reg.GetValue("ReservationsAllowed"), "WindowsUpdate", "Đã thay đổi thành 0")
                        Reg.SetValue("ReservationsAllowed", "0", RegistryValueKind.DWord)
                    End If
                End If

            Else

                Reg = Registry.CurrentUser.OpenSubKey("SOFTWARE\Microsoft\Windows\CurrentVersion\Policies\Explorer", True)
                If Reg IsNot Nothing Then
                    If Not Reg.GetValue("NoAutoUpdate") = "1" Then
                        AddListview1("NoAutoUpdate", Reg.GetValue("NoAutoUpdate"), "WindowsUpdate", "Đã thay đổi thành 1")
                        Reg.SetValue("NoAutoUpdate", "1", RegistryValueKind.DWord)
                    End If
                End If

                Reg = Registry.LocalMachine.OpenSubKey("SOFTWARE\Microsoft\Windows\CurrentVersion\Policies\Explorer", True)
                If Reg IsNot Nothing Then
                    If Not Reg.GetValue("NoAutoUpdate") = "1" Then
                        AddListview1("NoAutoUpdate", Reg.GetValue("NoAutoUpdate"), "WindowsUpdate", "Đã thay đổi thành 1")
                        Reg.SetValue("NoAutoUpdate", "1", RegistryValueKind.DWord)
                    End If
                End If

            End If

        End If
    End Sub

    Private Sub SetDateTimeVN()
        Reg = Registry.CurrentUser.OpenSubKey("Control Panel\International", True)
        If Reg IsNot Nothing Then
            If Not Reg.GetValue("sShortDate") = "dd/MM/yyyy" Then
                AddListview1("sShortDate", Reg.GetValue("sShortDate"), "DateTime", "Đã thay đổi thành dd/MM/yyyy")
                Reg.SetValue("sShortDate", "dd/MM/yyyy", RegistryValueKind.String)
            End If
            If Not Reg.GetValue("sShortTime") = "hh:mm tt" Then
                AddListview1("sShortTime", Reg.GetValue("sShortTime"), "DateTime", "Đã thay đổi thành hh:mm tt")
                Reg.SetValue("sShortTime", "hh:mm tt", RegistryValueKind.String)
            End If
            If Not Reg.GetValue("sTimeFormat") = "hh:mm:ss tt" Then
                AddListview1("sTimeFormat", Reg.GetValue("sTimeFormat"), "DateTime", "Đã thay đổi thành hh:mm:ss tt")
                Reg.SetValue("sTimeFormat", "hh:mm:ss tt", RegistryValueKind.String)
            End If
            If Not Reg.GetValue("sLongDate") = "dd MMMM yyyy" Then
                AddListview1("sLongDate", Reg.GetValue("sLongDate"), "DateTime", "Đã thay đổi thành dd MMMM yyyy")
                Reg.SetValue("sLongDate", "dd MMMM yyyy", RegistryValueKind.String)
            End If
            If Not Reg.GetValue("sYearMonth") = "MMMM yyyy" Then
                AddListview1("sYearMonth", Reg.GetValue("sYearMonth"), "DateTime", "Đã thay đổi thành MMMM yyyy")
                Reg.SetValue("sYearMonth", "MMMM yyyy", RegistryValueKind.String)
            End If
            If Not Reg.GetValue("sDate") = "/" Then
                AddListview1("sDate", Reg.GetValue("sDate"), "DateTime", "Đã thay đổi thành /")
                Reg.SetValue("sDate", "/", RegistryValueKind.String)
            End If
            If Not Reg.GetValue("sTime") = ":" Then
                AddListview1("sTime", Reg.GetValue("sTime"), "DateTime", "Đã thay đổi thành :")
                Reg.SetValue("sTime", ":", RegistryValueKind.String)
            End If
            If Not Reg.GetValue("sThousand") = "." Then
                AddListview1("sThousand", Reg.GetValue("sThousand"), "DateTime", "Đã thay đổi thành .")
                Reg.SetValue("sThousand", ".", RegistryValueKind.String)
            End If
            If Not Reg.GetValue("sNativeDigits") = "0123456789" Then
                AddListview1("sNativeDigits", Reg.GetValue("sNativeDigits"), "DateTime", "Đã thay đổi thành 0123456789")
                Reg.SetValue("sNativeDigits", "0123456789", RegistryValueKind.String)
            End If
            If Not Reg.GetValue("sNegativeSign") = "-" Then
                AddListview1("sNegativeSign", Reg.GetValue("sNegativeSign"), "DateTime", "Đã thay đổi thành 0123456789")
                Reg.SetValue("sNegativeSign", "-", RegistryValueKind.String)
            End If
            If Not Reg.GetValue("s1159") = "Sáng" Then
                AddListview1("s1159", Reg.GetValue("s1159"), "DateTime", "Đã thay đổi thành Sáng")
                Reg.SetValue("s1159", "Sáng", RegistryValueKind.String)
            End If
            If Not Reg.GetValue("s2359") = "Chiều" Then
                AddListview1("s2359", Reg.GetValue("s2359"), "DateTime", "Đã thay đổi thành Chiều")
                Reg.SetValue("s2359", "Chiều", RegistryValueKind.String)
            End If
            If Not Reg.GetValue("iCountry") = "84" Then
                AddListview1("iCountry", Reg.GetValue("iCountry"), "DateTime", "Đã thay đổi thành 84")
                Reg.SetValue("iCountry", "84", RegistryValueKind.String)
            End If
            If Not Reg.GetValue("sCountry") = "Việt Nam" Then
                AddListview1("sCountry", Reg.GetValue("sCountry"), "DateTime", "Đã thay đổi thành Việt Nam")
                Reg.SetValue("sCountry", "Việt Nam", RegistryValueKind.String)
            End If
            If Not Reg.GetValue("sLanguage") = "VIT" Then
                AddListview1("sLanguage", Reg.GetValue("sLanguage"), "DateTime", "Đã thay đổi thành VIT")
                Reg.SetValue("sLanguage", "VIT", RegistryValueKind.String)
            End If
            If Not Reg.GetValue("LocaleName") = "vi-VN" Then
                AddListview1("LocaleName", Reg.GetValue("LocaleName"), "DateTime", "Đã thay đổi thành vi-VN")
                Reg.SetValue("LocaleName", "vi-VN", RegistryValueKind.String)
            End If
            If Not Reg.GetValue("Locale") = "0000042a" Then
                AddListview1("Locale", Reg.GetValue("Locale"), "DateTime", "Đã thay đổi thành 0000042a")
                Reg.SetValue("Locale", "0000042a", RegistryValueKind.String)
            End If

        End If

        If WinExt.PCInfo.IsWinXP = False Then
            Reg = Registry.CurrentUser.OpenSubKey("Control Panel\International\User Profile", True)
            If Reg IsNot Nothing Then
                If Not Reg.GetValue("ShowAutoCorrection") = "1" Then
                    AddListview1("ShowAutoCorrection", Reg.GetValue("ShowAutoCorrection"), "DateTime", "Đã thay đổi thành 1")
                    Reg.SetValue("ShowAutoCorrection", "1", RegistryValueKind.DWord)
                End If
                If Not Reg.GetValue("ShowCasing") = "1" Then
                    AddListview1("ShowCasing", Reg.GetValue("ShowCasing"), "DateTime", "Đã thay đổi thành 1")
                    Reg.SetValue("ShowCasing", "1", RegistryValueKind.DWord)
                End If
                If Not Reg.GetValue("ShowShiftLock") = "1" Then
                    AddListview1("ShowShiftLock", Reg.GetValue("ShowShiftLock"), "DateTime", "Đã thay đổi thành 1")
                    Reg.SetValue("ShowShiftLockg", "1", RegistryValueKind.DWord)
                End If
                If Not Reg.GetValue("ShowTextPrediction") = "1" Then
                    AddListview1("ShowTextPrediction", Reg.GetValue("ShowTextPrediction"), "DateTime", "Đã thay đổi thành 1")
                    Reg.SetValue("ShowTextPrediction", "1", RegistryValueKind.DWord)
                End If
                If Not Reg.GetValue("UserLocaleFromLanguageProfileOptOut") = "1" Then
                    AddListview1("UserLocaleFromLanguageProfileOptOut", Reg.GetValue("UserLocaleFromLanguageProfileOptOut"), "DateTime", "Đã thay đổi thành 1")
                    Reg.SetValue("UserLocaleFromLanguageProfileOptOut", "1", RegistryValueKind.DWord)
                End If

            End If
        End If

    End Sub
    Private Sub SetIE(Optional HomePage As String = "https://www.google.com.vn/")
        If WinExt.PCInfo.IsWinXP = False Then
            Reg = Registry.CurrentUser.OpenSubKey("SOFTWARE\Microsoft\Internet Explorer\Main", True)
            If Reg IsNot Nothing Then
                If Not Reg.GetValue("Start Page") = HomePage Then
                    AddListview1("Start Page", Reg.GetValue("Start Page"), "IE", "Đã thay đổi thành " & HomePage)
                    Reg.SetValue("Start Page", HomePage, RegistryValueKind.String)
                End If
            End If

            Reg = Registry.CurrentUser.OpenSubKey("SOFTWARE\Microsoft\Internet Explorer\Geolocation", True)
            If Reg IsNot Nothing Then
                If Not Reg.GetValue("BlockAllWebsites") = "0" Then
                    AddListview1("BlockAllWebsites", Reg.GetValue("BlockAllWebsites"), "IE", "Đã thay đổi thành 0")
                    Reg.SetValue("BlockAllWebsites", "0", RegistryValueKind.DWord)
                End If
            End If

            Reg = Registry.CurrentUser.OpenSubKey("SOFTWARE\Microsoft\Internet Explorer\New Windows", True)
            If Reg IsNot Nothing Then
                If Not Reg.GetValue("PopupMgr") = "0" Then
                    AddListview1("PopupMgr", Reg.GetValue("PopupMgr"), "IE", "Đã thay đổi thành 0")
                    Reg.SetValue("PopupMgr", "0", RegistryValueKind.DWord)
                End If
            End If

            Reg = Registry.CurrentUser.OpenSubKey("SOFTWARE\Microsoft\Windows\CurrentVersion\Explorer\Desktop\NameSpace\{871C5380-42A0-1069-A2EA-08002B30301D}", True)
            If Reg Is Nothing Then My.Computer.Registry.CurrentUser.CreateSubKey("SOFTWARE\Microsoft\Windows\CurrentVersion\Explorer\Desktop\NameSpace\{871C5380-42A0-1069-A2EA-08002B30301D}", True)

            Try
                My.Computer.Registry.ClassesRoot.CreateSubKey("CLSID\{871C5380-42A0-1069-A2EA-08002B30301D}").SetValue("", "Internet Explorer", RegistryValueKind.String)
                My.Computer.Registry.ClassesRoot.OpenSubKey("CLSID\{871C5380-42A0-1069-A2EA-08002B30301D}", True).SetValue("InfoTip", FileSystem32("ieframe.dll,-881"), RegistryValueKind.String)
                My.Computer.Registry.ClassesRoot.CreateSubKey("CLSID\{871C5380-42A0-1069-A2EA-08002B30301D}\DefaultIcon").SetValue("", FileSystem32("ieframe.dll,-190"), RegistryValueKind.String)
                My.Computer.Registry.ClassesRoot.CreateSubKey("CLSID\{871C5380-42A0-1069-A2EA-08002B30301D}\InProcServer32").SetValue("", FileSystem32("ieframe.dll"), RegistryValueKind.String)
                My.Computer.Registry.ClassesRoot.OpenSubKey("CLSID\{871C5380-42A0-1069-A2EA-08002B30301D}\InProcServer32", True).SetValue("ThreadingModel", "Apartment", RegistryValueKind.String)
                My.Computer.Registry.ClassesRoot.CreateSubKey("CLSID\{871C5380-42A0-1069-A2EA-08002B30301D}\shell").SetValue("", "OpenHomePage", RegistryValueKind.String)
                My.Computer.Registry.ClassesRoot.CreateSubKey("CLSID\{871C5380-42A0-1069-A2EA-08002B30301D}\shell\NoAddOns").SetValue("", "Start Without Add-ons", RegistryValueKind.String)
                My.Computer.Registry.ClassesRoot.CreateSubKey("CLSID\{871C5380-42A0-1069-A2EA-08002B30301D}\shell\NoAddOns\Command").SetValue("", FileProgram32("iexplore.exe -extoff"), RegistryValueKind.String)
                My.Computer.Registry.ClassesRoot.CreateSubKey("CLSID\{871C5380-42A0-1069-A2EA-08002B30301D}\shell\OpenHomePage").SetValue("", "Open &Home Page", RegistryValueKind.String)
                My.Computer.Registry.ClassesRoot.CreateSubKey("CLSID\{871C5380-42A0-1069-A2EA-08002B30301D}\shell\OpenHomePage\Command").SetValue("", FileProgram32("iexplore.exe"), RegistryValueKind.String)
                My.Computer.Registry.ClassesRoot.CreateSubKey("CLSID\{871C5380-42A0-1069-A2EA-08002B30301D}\shell\Private").SetValue("", "Start InPrivate Browsing", RegistryValueKind.String)
                My.Computer.Registry.ClassesRoot.CreateSubKey("CLSID\{871C5380-42A0-1069-A2EA-08002B30301D}\shell\Private\Command").SetValue("", FileProgram32("iexplore.exe -private"), RegistryValueKind.String)
                My.Computer.Registry.ClassesRoot.CreateSubKey("CLSID\{871C5380-42A0-1069-A2EA-08002B30301D}\Properties").SetValue("", "P&roperties", RegistryValueKind.String)
                My.Computer.Registry.ClassesRoot.OpenSubKey("CLSID\{871C5380-42A0-1069-A2EA-08002B30301D}\Properties", True).SetValue("Position", "bottom", RegistryValueKind.String)
                My.Computer.Registry.ClassesRoot.CreateSubKey("CLSID\{871C5380-42A0-1069-A2EA-08002B30301D}\Properties\command").SetValue("", "control.exe inetcpl.cpl", RegistryValueKind.String)
                My.Computer.Registry.ClassesRoot.CreateSubKey("CLSID\{871C5380-42A0-1069-A2EA-08002B30301D}\Shellex\ContextMenuHandlers\ieframe").SetValue("", "{871C5380-42A0-1069-A2EA-08002B30309D}", RegistryValueKind.String)
                My.Computer.Registry.ClassesRoot.CreateSubKey("CLSID\{871C5380-42A0-1069-A2EA-08002B30301D}\Shellex\MayChangeDefaultMenu").SetValue("", "", RegistryValueKind.String)
                My.Computer.Registry.ClassesRoot.CreateSubKey("CLSID\{871C5380-42A0-1069-A2EA-08002B30301D}\ShellFolder").SetValue("", FileSystem32("ieframe.dll,-190"), RegistryValueKind.String)
                My.Computer.Registry.ClassesRoot.OpenSubKey("CLSID\{871C5380-42A0-1069-A2EA-08002B30301D}\ShellFolder", True).SetValue("HideAsDeletePerUser", "", RegistryValueKind.String)
                My.Computer.Registry.ClassesRoot.OpenSubKey("CLSID\{871C5380-42A0-1069-A2EA-08002B30301D}\ShellFolder", True).SetValue("HideFolderVerbs", "", RegistryValueKind.String)
                My.Computer.Registry.ClassesRoot.OpenSubKey("CLSID\{871C5380-42A0-1069-A2EA-08002B30301D}\ShellFolder", True).SetValue("WantsParseDisplayName", "", RegistryValueKind.String)
                My.Computer.Registry.ClassesRoot.OpenSubKey("CLSID\{871C5380-42A0-1069-A2EA-08002B30301D}\ShellFolder", True).SetValue("HideOnDesktopPerUser", "", RegistryValueKind.String)
                My.Computer.Registry.ClassesRoot.OpenSubKey("CLSID\{871C5380-42A0-1069-A2EA-08002B30301D}\ShellFolder", True).SetValue("Attributes", "24", RegistryValueKind.DWord)
            Catch ex As Exception
                MessageBox.Show(ex.ToString)
            End Try


        End If

    End Sub
    Private Sub SetDektopIcon(Optional MyComputer As Boolean = True, Optional RecycleBin As Boolean = True, Optional ControlPanel As Boolean = False,
                              Optional MyDocument As Boolean = True, Optional MyNetwork As Boolean = True, Optional HomeGroup As Boolean = False,
                              Optional IE As Boolean = True)

        Reg = Registry.CurrentUser.OpenSubKey("SOFTWARE\Microsoft\Windows\CurrentVersion\Explorer\HideDesktopIcons\NewStartPanel", True)
        If Reg IsNot Nothing Then
            If MyComputer = True Then
                If Not Reg.GetValue("{20D04FE0-3AEA-1069-A2D8-08002B30309D}") = "0" Then
                    AddListview1("{20D04FE0-3AEA-1069-A2D8-08002B30309D}", Reg.GetValue("{20D04FE0-3AEA-1069-A2D8-08002B30309D}"), "DesktopIcon", "Đã thay đổi thành 0")
                    Reg.SetValue("{20D04FE0-3AEA-1069-A2D8-08002B30309D}", "0", RegistryValueKind.DWord)
                End If
            End If
            If RecycleBin = True Then
                If Not Reg.GetValue("{645FF040-5081-101B-9F08-00AA002F954E}") = "0" Then
                    AddListview1("{645FF040-5081-101B-9F08-00AA002F954E}", Reg.GetValue("{645FF040-5081-101B-9F08-00AA002F954E}"), "DesktopIcon", "Đã thay đổi thành 0")
                    Reg.SetValue("{645FF040-5081-101B-9F08-00AA002F954E}", "0", RegistryValueKind.DWord)
                End If
            End If
            If ControlPanel = True Then
                If Not Reg.GetValue("{5399E694-6CE5-4D6C-8FCE-1D8870FDCBA0}") = "0" Then
                    AddListview1("{5399E694-6CE5-4D6C-8FCE-1D8870FDCBA0}", Reg.GetValue("{5399E694-6CE5-4D6C-8FCE-1D8870FDCBA0}"), "DesktopIcon", "Đã thay đổi thành 0")
                    Reg.SetValue("{5399E694-6CE5-4D6C-8FCE-1D8870FDCBA0}", "0", RegistryValueKind.DWord)
                End If
            End If
            If MyDocument = True Then
                If WinExt.PCInfo.IsWinXP = True Then
                    If Not Reg.GetValue("{450D8FBA-AD25-11D0-98A8-0800361B1103}") = "0" Then
                        AddListview1("{450D8FBA-AD25-11D0-98A8-0800361B1103}", Reg.GetValue("{450D8FBA-AD25-11D0-98A8-0800361B1103}"), "DesktopIcon", "Đã thay đổi thành 0")
                        Reg.SetValue("{450D8FBA-AD25-11D0-98A8-0800361B1103}", "0", RegistryValueKind.DWord)
                    End If
                Else
                    If Not Reg.GetValue("{59031a47-3f72-44a7-89c5-5595fe6b30ee}") = "0" Then
                        AddListview1("{59031a47-3f72-44a7-89c5-5595fe6b30ee}", Reg.GetValue("{59031a47-3f72-44a7-89c5-5595fe6b30ee}"), "DesktopIcon", "Đã thay đổi thành 0")
                        Reg.SetValue("{59031a47-3f72-44a7-89c5-5595fe6b30ee}", "0", RegistryValueKind.DWord)
                    End If
                End If
            End If
            If MyNetwork = True Then
                If WinExt.PCInfo.IsWinXP = True Then
                    If Not Reg.GetValue("{208D2C60-3AEA-1069-A2D7-08002B30309D}") = "0" Then
                        AddListview1("{208D2C60-3AEA-1069-A2D7-08002B30309D}", Reg.GetValue("{208D2C60-3AEA-1069-A2D7-08002B30309D}"), "DesktopIcon", "Đã thay đổi thành 0")
                        Reg.SetValue("{208D2C60-3AEA-1069-A2D7-08002B30309D}", "0", RegistryValueKind.DWord)
                    End If
                Else
                    If Not Reg.GetValue("{F02C1A0D-BE21-4350-88B0-7367FC96EF3C}") = "0" Then
                        AddListview1("{F02C1A0D-BE21-4350-88B0-7367FC96EF3C}", Reg.GetValue("{F02C1A0D-BE21-4350-88B0-7367FC96EF3C}"), "DesktopIcon", "Đã thay đổi thành 0")
                        Reg.SetValue("{F02C1A0D-BE21-4350-88B0-7367FC96EF3C}", "0", RegistryValueKind.DWord)
                    End If
                End If
            End If
            If HomeGroup = True Then
                If WinExt.PCInfo.IsWinXP = False Then
                    If Not Reg.GetValue("{B4FB3F98-C1EA-428d-A78A-D1F5659CBA93}") = "0" Then
                        AddListview1("{B4FB3F98-C1EA-428d-A78A-D1F5659CBA93}", Reg.GetValue("{B4FB3F98-C1EA-428d-A78A-D1F5659CBA93}"), "DesktopIcon", "Đã thay đổi thành 0")
                        Reg.SetValue("{B4FB3F98-C1EA-428d-A78A-D1F5659CBA93}", "0", RegistryValueKind.DWord)
                    End If
                End If
            End If
            If IE = True Then
                If Not Reg.GetValue("{871C5380-42A0-1069-A2EA-08002B30301D}") = "0" Then
                    AddListview1("{871C5380-42A0-1069-A2EA-08002B30301D}", Reg.GetValue("{871C5380-42A0-1069-A2EA-08002B30301D}"), "IE", "Đã thay đổi thành 0")
                    Reg.SetValue("{871C5380-42A0-1069-A2EA-08002B30301D}", "0", RegistryValueKind.DWord)
                End If
            End If
        End If

        Reg = Registry.CurrentUser.OpenSubKey("SOFTWARE\Microsoft\Windows\CurrentVersion\Explorer\HideDesktopIcons\ClassicStartMenu", True)
        If Reg IsNot Nothing Then
            If WinExt.PCInfo.IsWinXP = True Then
                If Not Reg.GetValue("{20D04FE0-3AEA-1069-A2D8-08002B30309D}") = "0" Then
                    AddListview1("{20D04FE0-3AEA-1069-A2D8-08002B30309D}", Reg.GetValue("{20D04FE0-3AEA-1069-A2D8-08002B30309D}"), "DesktopIcon", "Đã thay đổi thành 0")
                    Reg.SetValue("{20D04FE0-3AEA-1069-A2D8-08002B30309D}", "0", RegistryValueKind.DWord)
                End If
            End If
        End If

        Reg = Registry.CurrentUser.OpenSubKey("SOFTWARE\Classes\CLSID\{B4FB3F98-C1EA-428d-A78A-D1F5659CBA93}", True)
        If Reg IsNot Nothing Then
            If WinExt.PCInfo.IsWinXP = False Then
                If Not Reg.GetValue("System.IsPinnedToNameSpaceTree") = "1" Then
                    AddListview1("System.IsPinnedToNameSpaceTree", Reg.GetValue("System.IsPinnedToNameSpaceTree"), "DesktopIcon", "Đã thay đổi thành 1")
                    Reg.SetValue("System.IsPinnedToNameSpaceTree", "1", RegistryValueKind.DWord)
                End If
            End If
        End If

    End Sub
    Private Sub SetVisualEffect()
        Reg = Registry.CurrentUser.OpenSubKey("SOFTWARE\Microsoft\Windows\CurrentVersion\Explorer\Advanced", True)
        If Reg IsNot Nothing Then
            If Not Reg.GetValue("TaskbarAnimations") = "0" Then
                AddListview1("TaskbarAnimations", Reg.GetValue("TaskbarAnimations"), "VisualEffect", "Đã thay đổi thành 0")
                Reg.SetValue("TaskbarAnimations", "0", RegistryValueKind.DWord)
            End If
            If Not Reg.GetValue("ListviewAlphaSelect") = "0" Then
                AddListview1("ListviewAlphaSelect", Reg.GetValue("ListviewAlphaSelect"), "VisualEffect", "Đã thay đổi thành 0")
                Reg.SetValue("ListviewAlphaSelect", "0", RegistryValueKind.DWord)
            End If
            If WinExt.PCInfo.IsWinXP = True Then
                If Not Reg.GetValue("WebView") = "1" Then
                    AddListview1("WebView", Reg.GetValue("WebView"), "VisualEffect", "Đã thay đổi thành 1")
                    Reg.SetValue("WebView", "1", RegistryValueKind.DWord)
                End If
            Else
                If Not Reg.GetValue("ListviewShadow") = "1" Then
                    AddListview1("ListviewShadow", Reg.GetValue("ListviewAlphaSelect"), "VisualEffect", "Đã thay đổi thành 1")
                    Reg.SetValue("ListviewShadow", "1", RegistryValueKind.DWord)
                End If
                If Not Reg.GetValue("IconsOnly") = "0" Then
                    AddListview1("IconsOnly", Reg.GetValue("IconsOnly"), "VisualEffect", "Đã thay đổi thành 0")
                    Reg.SetValue("IconsOnly", "0", RegistryValueKind.DWord)
                End If
            End If

        End If

        Reg = Registry.CurrentUser.OpenSubKey("SOFTWARE\Microsoft\Windows\CurrentVersion\Explorer\VisualEffects", True)
        If Reg IsNot Nothing Then
            If Not Reg.GetValue("VisualFXSetting") = "3" Then
                AddListview1("VisualFXSetting", Reg.GetValue("VisualFXSetting"), "VisualEffect", "Đã thay đổi thành 3")
                Reg.SetValue("VisualFXSetting", "3", RegistryValueKind.DWord)
            End If
        End If

        Reg = Registry.LocalMachine.OpenSubKey("SOFTWARE\Microsoft\Windows\CurrentVersion\Explorer", True)
        If Reg IsNot Nothing Then
            If WinExt.PCInfo.IsWinXP = True Then
                If Not Reg.GetValue("AlwaysUnloadDLL") = "1" Then
                    AddListview1("AlwaysUnloadDLL", Reg.GetValue("AlwaysUnloadDLL"), "VisualEffect", "Đã thay đổi thành 1")
                    Reg.SetValue("AlwaysUnloadDLL", "1", RegistryValueKind.DWord)
                End If
            End If
        End If

        Reg = Registry.CurrentUser.OpenSubKey("Control Panel\Desktop", True)
        If Reg IsNot Nothing Then
            If Not Reg.GetValue("FontSmoothing") = "2" Then
                AddListview1("FontSmoothing", Reg.GetValue("FontSmoothing"), "VisualEffect", "Đã thay đổi thành 2")
                Reg.SetValue("FontSmoothing", "2", RegistryValueKind.String)
            End If
            If Not Reg.GetValue("DragFullWindows") = "0" Then
                AddListview1("DragFullWindows", Reg.GetValue("DragFullWindows"), "VisualEffect", "Đã thay đổi thành 0")
                Reg.SetValue("DragFullWindows", "0", RegistryValueKind.String)
            End If
            If WinExt.PCInfo.IsWinXP = True Then
                If Not Reg.GetValue("FontSmoothingType") = "1" Then
                    AddListview1("FontSmoothingType", Reg.GetValue("FontSmoothingType"), "VisualEffect", "Đã thay đổi thành 1")
                    Reg.SetValue("FontSmoothingType", "1", RegistryValueKind.DWord)
                End If
                If Not ByteArrayToHexString(Reg.GetValue("UserPreferencesMask")) = "98120380" Then
                    AddListview1("UserPreferencesMask", ByteArrayToHexString(Reg.GetValue("UserPreferencesMask")), "VisualEffect", "Đã thay đổi thành công !")
                    Reg.SetValue("UserPreferencesMask", HexStringToByteArray("98120380"), RegistryValueKind.Binary)
                End If
            Else
                If Not Reg.GetValue("FontSmoothingType") = "2" Then
                    AddListview1("FontSmoothingType", Reg.GetValue("FontSmoothingType"), "VisualEffect", "Đã thay đổi thành 2")
                    Reg.SetValue("FontSmoothingType", "2", RegistryValueKind.DWord)
                End If
            End If
            If WinExt.PCInfo.IsWinVista = True Then

            End If
            If WinExt.PCInfo.IsWin7 = True Then
                If Not ByteArrayToHexString(Reg.GetValue("UserPreferencesMask")) = "9C12038010000000" Then
                    AddListview1("UserPreferencesMask", ByteArrayToHexString(Reg.GetValue("UserPreferencesMask")), "VisualEffect", "Đã thay đổi thành công !")
                    Reg.SetValue("UserPreferencesMask", HexStringToByteArray("9C12038010000000"), RegistryValueKind.Binary)
                End If
            End If
            If WinExt.PCInfo.IsWin8 = True Then

            End If
            If WinExt.PCInfo.IsWin81 = True Then

            End If
            If WinExt.PCInfo.IsWin10 = True Then
                If Not ByteArrayToHexString(Reg.GetValue("UserPreferencesMask")) = "9C12038010010000" Then
                    AddListview1("UserPreferencesMask", ByteArrayToHexString(Reg.GetValue("UserPreferencesMask")), "VisualEffect", "Đã thay đổi thành công !")
                    Reg.SetValue("UserPreferencesMask", HexStringToByteArray("9C12038010010000"), RegistryValueKind.Binary)
                End If
            End If
        End If

        Reg = Registry.CurrentUser.OpenSubKey("Control Panel\Desktop\WindowMetrics", True)
        If Reg IsNot Nothing Then
            If Not Reg.GetValue("MinAnimate") = "0" Then
                AddListview1("MinAnimate", Reg.GetValue("MinAnimate"), "VisualEffect", "Đã thay đổi thành 0")
                Reg.SetValue("MinAnimate", "0", RegistryValueKind.String)
            End If
        End If

        If WinExt.PCInfo.IsWinXP = True Then
            Reg = Registry.CurrentUser.OpenSubKey("SOFTWARE\Microsoft\Internet Explorer\Desktop\Components", True)
            If Reg IsNot Nothing Then
                If Not Reg.GetValue("GeneralFlags") = "4" Then
                    AddListview1("GeneralFlags", Reg.GetValue("GeneralFlags"), "VisualEffect", "Đã thay đổi thành 2")
                    Reg.SetValue("GeneralFlags", "4", RegistryValueKind.DWord)
                End If
            End If
        End If

        If WinExt.PCInfo.IsWinXP = False Then
            Reg = Registry.CurrentUser.OpenSubKey("SOFTWARE\Microsoft\Windows\DWM", True)
            If Reg IsNot Nothing Then
                If Not Reg.GetValue("AlwaysHibernateThumbnails") = "0" Then
                    AddListview1("AlwaysHibernateThumbnails", Reg.GetValue("AlwaysHibernateThumbnails"), "VisualEffect", "Đã thay đổi thành 0")
                    Reg.SetValue("AlwaysHibernateThumbnails", "0", RegistryValueKind.DWord)
                End If
                If Not Reg.GetValue("EnableAeroPeek") = "0" Then
                    AddListview1("EnableAeroPeek", Reg.GetValue("EnableAeroPeek"), "VisualEffect", "Đã thay đổi thành 0")
                    Reg.SetValue("EnableAeroPeek", "0", RegistryValueKind.DWord)
                End If
            End If
        End If

        If WinExt.PCInfo.IsWin10 = True Then
            Reg = Registry.CurrentUser.OpenSubKey("SOFTWARE\Microsoft\Windows\CurrentVersion\Themes\Personalize", True)
            If Reg IsNot Nothing Then
                If Not Reg.GetValue("ColorPrevalence") = "1" Then
                    AddListview1("ColorPrevalence", Reg.GetValue("ColorPrevalence"), "VisualEffect", "Đã thay đổi thành 1")
                    Reg.SetValue("ColorPrevalence", "1", RegistryValueKind.DWord)
                End If
                If Not Reg.GetValue("EnableTransparency") = "1" Then
                    AddListview1("EnableTransparency", Reg.GetValue("EnableTransparency"), "VisualEffect", "Đã thay đổi thành 1")
                    Reg.SetValue("EnableTransparency", "1", RegistryValueKind.DWord)
                End If
            End If
        End If


    End Sub

    Private Sub SetDriveIcon(Optional Drive As String = "C", Optional Icon As String = "")
        Dim ImageFile As String = FileSystem32("2CongLC.Vn-" & Drive & ".ico")
        If File.Exists(ImageFile) = False Then
            If File.Exists(Icon) = True Then
                File.Copy(Icon, ImageFile)
            Else
                Dim fs As Stream = File.Create(ImageFile)
                My.Resources._2CongLC_Vn.Save(fs)
                fs.Close()
            End If
        End If

        Reg = Registry.LocalMachine.OpenSubKey("SOFTWARE\Microsoft\Windows\CurrentVersion\Explorer\DriveIcons\" & Drive & "\DefaultIcon", True)
        If Reg IsNot Nothing Then
            If Not Reg.GetValue(Nothing) = ImageFile Then
                AddListview1(Drive, ImageFile, "DriveIcon", "Đã thay đổi thành công !")
                Reg.SetValue(Nothing, ImageFile, RegistryValueKind.String)
            End If
        Else
            My.Computer.Registry.LocalMachine.CreateSubKey("SOFTWARE\Microsoft\Windows\CurrentVersion\Explorer\DriveIcons\" & Drive & "\DefaultIcon")
        End If

    End Sub
    Private Sub SetCommandPromtAdmin(Optional Enable As Boolean = True)
        If Enable = True Then
            If WinExt.PCInfo.IsWinXP = False Then
                Reg = Registry.ClassesRoot.OpenSubKey("Directory\shell\runas", True)
                If Reg Is Nothing Then
                    My.Computer.Registry.ClassesRoot.CreateSubKey("Directory\shell\runas").SetValue("", "Open Command Window Here (Admin)", RegistryValueKind.String)
                    My.Computer.Registry.ClassesRoot.OpenSubKey("Directory\shell\runas", True).SetValue("HasLUAShield", "", RegistryValueKind.String)
                    My.Computer.Registry.ClassesRoot.CreateSubKey("Directory\shell\runas\Command").SetValue("", "cmd.exe /s /k pushd " & Chr(34) & "%V" & Chr(34), RegistryValueKind.String)
                    AddListview1("Directory", "CommandPromt", "ShellFolder", "Đã thay đổi thành công !")
                End If

                Reg = Registry.ClassesRoot.OpenSubKey("Drive\shell\runas", True)
                If Reg Is Nothing Then
                    My.Computer.Registry.ClassesRoot.CreateSubKey("Drive\shell\runas").SetValue("", "Open Command Window Here (Admin)", RegistryValueKind.String)
                    My.Computer.Registry.ClassesRoot.OpenSubKey("Drive\shell\runas", True).SetValue("HasLUAShield", "", RegistryValueKind.String)
                    My.Computer.Registry.ClassesRoot.CreateSubKey("Drive\shell\runas\Command").SetValue("", "cmd.exe /s /k pushd " & Chr(34) & "%V" & Chr(34), RegistryValueKind.String)
                    AddListview1("Drive", "CommandPromt", "ShellFolder", "Đã thay đổi thành công !")
                End If
            End If
        End If
    End Sub

    Private Sub SetNetFramework(Optional Enable As Boolean = True, Optional Target As EnvironmentVariableTarget = EnvironmentVariableTarget.Machine)
        If Enable = True Then
            Dim Version As String = System.Runtime.InteropServices.RuntimeEnvironment.GetSystemVersion
            Dim Netframework As String = Environment.ExpandEnvironmentVariables("%SystemRoot%") & "\" & "Microsoft.NET\Framework\" & Version & "\"
            Dim Envpath As String = ""
            If Target = EnvironmentVariableTarget.User Then
                Reg = Registry.CurrentUser.OpenSubKey("Environment", True)
                If Reg IsNot Nothing Then
                    Envpath = Reg.GetValue("Path")
                    If Envpath.Contains(Netframework) = False Then
                        Reg.SetValue("Path", Envpath & ";" & Netframework, RegistryValueKind.String)
                    End If
                End If
            ElseIf Target = EnvironmentVariableTarget.Machine Then
                Reg = Registry.LocalMachine.OpenSubKey("SYSTEM\CurrentControlSet\Control\Session Manager\Environment", True)
                If Reg IsNot Nothing Then
                    Envpath = Reg.GetValue("Path")
                    If Envpath.Contains(Netframework) = False Then
                        Reg.SetValue("Path", Envpath & ";" & Netframework, RegistryValueKind.String)
                    End If
                End If

            End If

        End If
    End Sub
    ''' <summary>
    ''' Sử dụng Unikey với bảng mã Unicode Dựng sẵn để hiển thị tiếng việt
    ''' </summary>
    ''' <param name="Enable"></param>
    ''' <param name="Logo"></param>
    ''' <param name="Manufacturer"></param>
    ''' <param name="Model"></param>
    ''' <param name="SupportHours"></param>
    ''' <param name="SupportURL"></param>
    Private Sub SetOEM(Optional Enable As Boolean = True, Optional Logo As String = "",
                       Optional Manufacturer As String = "2CongLC.Vn",
                       Optional Model As String = "Lào Cai - Việt Nam",
                       Optional SupportHours As String = "GMT +7",
                       Optional SupportURL As String = "http://fb.com/2conglc.vn")

        If Enable = True Then
            Dim ImageFile As String = FileSystem32("oemlogo.bmp")
            If File.Exists(ImageFile) = False Then
                If File.Exists(Logo) = True Then
                    File.Copy(Logo, FileSystem32("oemlogo.bmp"))
                Else
                    My.Resources.oemlogo.Save(FileSystem32("oemlogo.bmp"), Imaging.ImageFormat.Bmp)
                End If
            End If

            If WinExt.PCInfo.IsWinXP = True Then
                If File.Exists(FileSystem32("oeminfo.ini")) = False Then
                    File.WriteAllBytes(FileSystem32("oeminfo.ini"), Encoding.UTF8.GetBytes(My.Resources.oeminfo))
                End If
            Else
                Reg = Registry.LocalMachine.OpenSubKey("SOFTWARE\Microsoft\Windows\CurrentVersion\OEMInformation", True)
                If Reg IsNot Nothing Then
                    If Not Reg.GetValue("Logo") = ImageFile Then
                        AddListview1("Logo", Reg.GetValue("Logo"), "OEM", "Đã thay đổi thành công !")
                        Reg.SetValue("Logo", ImageFile, RegistryValueKind.String)
                    End If
                    If Not Reg.GetValue("Manufacturer") = Manufacturer Then
                        AddListview1("Manufacturer", Reg.GetValue("Manufacturer"), "OEM", "Đã thay đổi thành công !")
                        Reg.SetValue("Manufacturer", Manufacturer, RegistryValueKind.String)
                    End If
                    If Not Reg.GetValue("Model") = Model Then
                        AddListview1("Model", Reg.GetValue("Model"), "OEM", "Đã thay đổi thành công !")
                        Reg.SetValue("Model", Model, RegistryValueKind.String)
                    End If
                    If Not Reg.GetValue("SupportHours") = SupportHours Then
                        AddListview1("SupportHours", Reg.GetValue("SupportHours"), "OEM", "Đã thay đổi thành công !")
                        Reg.SetValue("SupportHours", SupportHours, RegistryValueKind.String)
                    End If
                    If Not Reg.GetValue("SupportURL") = SupportURL Then
                        AddListview1("SupportURL", Reg.GetValue("SupportURL"), "OEM", "Đã thay đổi thành công !")
                        Reg.SetValue("SupportURL", SupportURL, RegistryValueKind.String)
                    End If
                End If

            End If

        End If
    End Sub
    Private Sub SetTakeOwnerShip(Optional Enable As Boolean = True)
        If Enable = True Then
            If WinExt.PCInfo.IsWinXP = False Then
                Dim cmd As String = "cmd.exe /c takeown /f \" & Chr(34) & "%1\" & Chr(34) & " " & Chr(38) & Chr(38) & " icacls \" & Chr(34) & "%1\" & Chr(34) & " /grant administrators:F"
                Reg = Registry.ClassesRoot.OpenSubKey("exefile\shell\takeownership", True)
                If Reg Is Nothing Then
                    My.Computer.Registry.ClassesRoot.CreateSubKey("exefile\shell\takeownership").SetValue("", "Take Ownership", RegistryValueKind.String)
                    My.Computer.Registry.ClassesRoot.OpenSubKey("exefile\shell\takeownership", True).SetValue("HasLUAShield", "", RegistryValueKind.String)
                    My.Computer.Registry.ClassesRoot.OpenSubKey("exefile\shell\takeownership", True).SetValue("NoWorkingDirectory", "", RegistryValueKind.String)
                    My.Computer.Registry.ClassesRoot.CreateSubKey("exefile\shell\takeownership\command").SetValue("", cmd, RegistryValueKind.String)
                    My.Computer.Registry.ClassesRoot.OpenSubKey("exefile\shell\takeownership\command", True).SetValue("IsolatedCommand", cmd, RegistryValueKind.String)
                    AddListview1("exefile", "takeownership", "ContextMenu", "Đã thay đổi thành công !")
                End If

                Reg = Registry.ClassesRoot.OpenSubKey("dllfile\shell\takeownership", True)
                If Reg Is Nothing Then
                    My.Computer.Registry.ClassesRoot.CreateSubKey("dllfile\shell\takeownership").SetValue("", "Take Ownership", RegistryValueKind.String)
                    My.Computer.Registry.ClassesRoot.OpenSubKey("dllfile\shell\takeownership", True).SetValue("HasLUAShield", "", RegistryValueKind.String)
                    My.Computer.Registry.ClassesRoot.OpenSubKey("dllfile\shell\takeownership", True).SetValue("NoWorkingDirectory", "", RegistryValueKind.String)
                    My.Computer.Registry.ClassesRoot.CreateSubKey("dllfile\shell\takeownership\command").SetValue("", cmd, RegistryValueKind.String)
                    My.Computer.Registry.ClassesRoot.OpenSubKey("dllfile\shell\takeownership\command", True).SetValue("IsolatedCommand", cmd, RegistryValueKind.String)
                    AddListview1("dllfile", "takeownership", "ContextMenu", "Đã thay đổi thành công !")
                End If

                Reg = Registry.ClassesRoot.OpenSubKey("Directory\shell\takeownership", True)
                If Reg Is Nothing Then
                    My.Computer.Registry.ClassesRoot.CreateSubKey("Directory\shell\takeownership").SetValue("", "Take Ownership", RegistryValueKind.String)
                    My.Computer.Registry.ClassesRoot.OpenSubKey("Directory\shell\takeownership", True).SetValue("HasLUAShield", "", RegistryValueKind.String)
                    My.Computer.Registry.ClassesRoot.OpenSubKey("Directory\shell\takeownership", True).SetValue("NoWorkingDirectory", "", RegistryValueKind.String)
                    My.Computer.Registry.ClassesRoot.CreateSubKey("Directory\shell\takeownership\command").SetValue("", cmd, RegistryValueKind.String)
                    My.Computer.Registry.ClassesRoot.OpenSubKey("Directory\shell\takeownership\command", True).SetValue("IsolatedCommand", cmd, RegistryValueKind.String)
                    AddListview1("Directory", "takeownership", "ContextMenu", "Đã thay đổi thành công !")
                End If
            End If
        End If

    End Sub
    Private Sub SetShellNew(Optional Enable As Boolean = True)
        If Enable = True Then
            Reg = Registry.ClassesRoot.OpenSubKey("Briefcase\ShellNew", True)
            If Reg IsNot Nothing Then
                My.Computer.Registry.ClassesRoot.DeleteSubKey("Briefcase\ShellNew")
                AddListview1("Briefcase", "", "Shellnew", "Đã thay đổi thành công !")
            End If

            Reg = Registry.ClassesRoot.OpenSubKey(".contact\ShellNew", True)
            If Reg IsNot Nothing Then
                My.Computer.Registry.ClassesRoot.DeleteSubKey(".contact\ShellNew")
                AddListview1(".contact", "", "Shellnew", "Đã thay đổi thành công !")
            End If

            Reg = Registry.ClassesRoot.OpenSubKey(".rar\ShellNew", True)
            If Reg IsNot Nothing Then
                My.Computer.Registry.ClassesRoot.DeleteSubKey(".rar\ShellNew")
                AddListview1(".rar", "", "Shellnew", "Đã thay đổi thành công !")
            End If

            Reg = Registry.ClassesRoot.OpenSubKey(".zip\CompressedFolder\ShellNew", True)
            If Reg IsNot Nothing Then
                My.Computer.Registry.ClassesRoot.DeleteSubKey(".zip\CompressedFolder\ShellNew")
                AddListview1(".zip", "", "Shellnew", "Đã thay đổi thành công !")
            End If

            Reg = Registry.ClassesRoot.OpenSubKey(".bmp\ShellNew", True)
            If Reg Is Nothing Then
                My.Computer.Registry.ClassesRoot.CreateSubKey(".bmp\ShellNew").SetValue("ItemName", "@%systemroot%\system32\mspaint.exe,-59414", RegistryValueKind.ExpandString)
                AddListview1(".bmp", "", "Shellnew", "Đã thay đổi thành công !")
            End If

            Reg = Registry.ClassesRoot.OpenSubKey(".lnk\ShellNew", True)
            If Reg Is Nothing Then
                My.Computer.Registry.ClassesRoot.CreateSubKey(".lnk\ShellNew").SetValue("Handler", "{ceefea1b-3e29-4ef1-b34c-fec79c4f70af}", RegistryValueKind.String)
                My.Computer.Registry.ClassesRoot.OpenSubKey(".lnk\ShellNew", True).SetValue("IconPath", "%SystemRoot%\system32\shell32.dll,-16769", RegistryValueKind.ExpandString)
                My.Computer.Registry.ClassesRoot.OpenSubKey(".lnk\ShellNew", True).SetValue("ItemName", "@shell32.dll,-30397", RegistryValueKind.String)
                My.Computer.Registry.ClassesRoot.OpenSubKey(".lnk\ShellNew", True).SetValue("MenuText", "@shell32.dll,-30318", RegistryValueKind.String)
                My.Computer.Registry.ClassesRoot.OpenSubKey(".lnk\ShellNew", True).SetValue("NullFile", "", RegistryValueKind.String)
                AddListview1(".lnk", "", "Shellnew", "Đã thay đổi thành công !")
            End If

            Reg = Registry.ClassesRoot.OpenSubKey("Folder\ShellNew", True)
            If Reg Is Nothing Then
                My.Computer.Registry.ClassesRoot.CreateSubKey("Folder\ShellNew").SetValue("Directory", "{ceefea1b-3e29-4ef1-b34c-fec79c4f70af}", RegistryValueKind.String)
                My.Computer.Registry.ClassesRoot.OpenSubKey("Folder\ShellNew", True).SetValue("IconPath", "%SystemRoot%\system32\shell32.dll,3", RegistryValueKind.ExpandString)
                My.Computer.Registry.ClassesRoot.OpenSubKey("Folder\ShellNew", True).SetValue("ItemName", "@shell32.dll,-30396", RegistryValueKind.String)
                My.Computer.Registry.ClassesRoot.OpenSubKey("Folder\ShellNew", True).SetValue("MenuText", "@shell32.dll,-30317", RegistryValueKind.String)
                My.Computer.Registry.ClassesRoot.OpenSubKey("Folder\ShellNew", True).SetValue("NonLFNFileSpec", "@shell32.dll,-30319", RegistryValueKind.String)
                My.Computer.Registry.ClassesRoot.CreateSubKey("Folder\ShellNew\Config").SetValue("AllDrives", "", RegistryValueKind.String)
                My.Computer.Registry.ClassesRoot.OpenSubKey("Folder\ShellNew\Config", True).SetValue("IsFolder", "", RegistryValueKind.String)
                My.Computer.Registry.ClassesRoot.OpenSubKey("Folder\ShellNew\Config", True).SetValue("NoExtension", "", RegistryValueKind.String)
                AddListview1("Folder", "", "Shellnew", "Đã thay đổi thành công !")
            End If

            Reg = Registry.ClassesRoot.OpenSubKey(".rtf\ShellNew", True)
            If Reg Is Nothing Then
                My.Computer.Registry.ClassesRoot.CreateSubKey(".rtf\ShellNew").SetValue("Data", "", RegistryValueKind.String)
                My.Computer.Registry.ClassesRoot.OpenSubKey(".rtf\ShellNew", True).SetValue("ItemName", "@%ProgramFiles%\Windows NT\Accessories\WORDPAD.EXE,-213", RegistryValueKind.ExpandString)
                AddListview1(".rtf", "", "Shellnew", "Đã thay đổi thành công !")
            End If

            Reg = Registry.ClassesRoot.OpenSubKey(".txt\ShellNew", True)
            If Reg Is Nothing Then
                My.Computer.Registry.ClassesRoot.CreateSubKey(".txt\ShellNew").SetValue("NullFile", "", RegistryValueKind.String)
                My.Computer.Registry.ClassesRoot.OpenSubKey(".txt\ShellNew", True).SetValue("ItemName", "@%SystemRoot%\system32\notepad.exe,-470", RegistryValueKind.ExpandString)
                AddListview1(".txt", "", "Shellnew", "Đã thay đổi thành công !")
            End If

        End If
    End Sub

#End Region
#Region "Sửa lỗi Kết nối mạng"

    Private Sub AddListview2(pcname As String, domain As String, localip As String, hostname As String, wanip As String)
        Dim items As ListViewItem = New ListViewItem(pcname)
        items.SubItems.Add(domain)
        items.SubItems.Add(localip)
        items.SubItems.Add(hostname)
        items.SubItems.Add(wanip)
        ListView2.Invoke(CType(Sub()
                                   ListView2.BeginUpdate()
                                   ListView2.Items.Add(items)
                                   ListView2.EndUpdate()
                               End Sub, Action))

    End Sub
#End Region
#Region "Điều khiển , cài đặt"
    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            If WinExt.PCInfo.IsWinXP = True Then
                Button5.Enabled = True
            Else
                Button5.Enabled = False
            End If
            If File.Exists(ImagePath) = True Then
                PictureBox1.Image = Image.FromFile(ImagePath)
            Else
                PictureBox1.Image = My.Resources.oemlogo
            End If
            If File.Exists(IconPath) = True Then
                PictureBox2.Image = Image.FromFile(IconPath)
            Else
                PictureBox2.Image = My.Resources._2CongLC_Vn.ToBitmap
            End If
            ComboBox1.Items.AddRange(GetAllLogicalDrives)
            ComboBox1.SelectedIndex = 0
            DriveName = ComboBox1.SelectedItem.ToString.Replace(":\", "")
            LoadLAN()
            LoadWAN()
        Catch ex As Exception
            '  MessageBox.Show(ex.ToString)
        End Try
    End Sub
    Private Sub ComboBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox1.SelectedIndexChanged
        Try
            Dim cbo = DirectCast(sender, ComboBox)
            If cbo.SelectedIndex = -1 Then Return
            DriveName = DirectCast(cbo.SelectedItem, String).Replace(":\", "")
            PictureBox2.Image = Image.FromFile(FileSystem32("2CongLC.Vn-" & DriveName & ".ico"))
        Catch ex As Exception
            PictureBox2.Image = Nothing
        End Try
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If ListView1.Items.Count <> 0 Then ListView1.Items.Clear()

        '-------------------------
        DisableUAC(CheckBox1.Checked)
        DisableMetro(CheckBox2.Checked)
        DisableAutoPlay(CheckBox3.Checked)
        DisableCrashControl(CheckBox4.Checked)
        DisableSecurity(CheckBox5.Checked)
        DisableWindowsUpdate(CheckBox6.Checked)
        DisableWindowsFirewall(CheckBox7.Checked)
        '-------------------------
        RemoveMMC()
        RemovePoliciesMicrosoftControlPanelInternational()
        RemovePoliciesMicrosoftControlPanelDesktop()
        RemovePoliciesMicrosoftWindowsControlPanelDesktop()
        RemovePoliciesMicrosoftSystem()
        RemovePoliciesMicrosoftExplorer()
        RemoveWindowsPoliciesExplorer()
        RemoveWindowsPoliciesSystem()
        ResetWindowsPoliciesSystem()
        '-------------------------
        EnableDriverSearch()
        EnableStartMenu()
        EnableControlPanel()
        EnablePrinter()
        EnableTaskbar()
        EnableDesktop()
        ResetSystemControlSet()
        EnableNetwork()
        EnableFolders()
        '-------------------------
        SetDateTimeVN()
        SetIE()
        SetDektopIcon(CheckBox8.Checked, CheckBox9.Checked, CheckBox10.Checked, CheckBox11.Checked, CheckBox12.Checked, CheckBox13.Checked, CheckBox14.Checked)
        SetVisualEffect()
        SetDriveIcon()
        SetCommandPromtAdmin()
        SetNetFramework()
        SetOEM(True, ImagePath)
        SetTakeOwnerShip()
        SetShellNew()
        '-------------------------
        If ListView1.Items.Count = 0 Then MessageBox.Show("Không có lỗi hệ thống !") Else MessageBox.Show("Đã sửa tổng cộng : " & ListView1.Items.Count & " lỗi !")
    End Sub
    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        If OpenFileDialog1.ShowDialog = DialogResult.OK Then
            Dim temp As String = OpenFileDialog1.FileName
            If GetImageFormat(temp) = "Bmp" Then
                If GetImageSize(temp) = True Then
                    ImagePath = temp
                    PictureBox1.Image = Image.FromFile(ImagePath)
                Else
                    MessageBox.Show("Kích thước ảnh Width = 110, Height = 95")
                End If
            Else
                MessageBox.Show("Định dạng ảnh phải là .bmp")
            End If
        End If
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        Try
            If File.Exists(ImagePath) = True Then
                If String.IsNullOrWhiteSpace(TextBox1.Text) AndAlso String.IsNullOrWhiteSpace(TextBox2.Text) AndAlso
                    String.IsNullOrWhiteSpace(TextBox3.Text) AndAlso String.IsNullOrWhiteSpace(TextBox4.Text) Then

                    SetOEM(True, ImagePath, TextBox1.Text, TextBox2.Text, TextBox3.Text, TextBox4.Text)
                Else
                    MessageBox.Show("Hãy nhập đầy đủ thông tin vào các mục !")
                End If
            Else
                    MessageBox.Show("Hãy chọn ảnh trước khi tiến hành cài đặt !")
            End If
        Catch ex As Exception
            MessageBox.Show(ex.ToString)
        End Try
    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        Try
            Process.Start(FileSystem32("oeminfo.ini"))
        Catch ex As Exception
            MessageBox.Show(ex.ToString)
        End Try
    End Sub
    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        If OpenFileDialog1.ShowDialog = DialogResult.OK Then
            Dim temp As String = OpenFileDialog1.FileName
            If GetImageFormat(temp) = "Icon" Then
                IconPath = temp
                PictureBox2.Image = DirectCast(Image.FromFile(IconPath), Bitmap)
            Else
                MessageBox.Show("Định dạng tệp phải là Icon (.ico)")
            End If
        End If
    End Sub

    Private Sub Button7_Click(sender As Object, e As EventArgs) Handles Button7.Click
        Try
            If File.Exists(IconPath) Then
                SetDriveIcon(DriveName, IconPath)
                MessageBox.Show("Đã tạo " & DriveName & " với Icon là : " & IconPath)
            Else
                MessageBox.Show("Hãy chọn Icon trước khi tiến hành cài đặt")
            End If
        Catch ex As Exception
            MessageBox.Show(ex.ToString)
        End Try
    End Sub

    Private Sub Button8_Click(sender As Object, e As EventArgs) Handles Button8.Click
        Try
            My.Computer.Registry.LocalMachine.OpenSubKey("SOFTWARE\Microsoft\Windows\CurrentVersion\Explorer\DriveIcons", True).DeleteSubKeyTree(DriveName)
            File.Delete(FileSystem32("2CongLC.Vn-" & DriveName & ".ico"))
            MessageBox.Show("Đã gỡ  " & DriveName & " với Icon là : " & FileSystem32("2CongLC.Vn-" & DriveName & ".ico"))
        Catch ex As Exception
            MessageBox.Show("Drive Icon đã được gỡ hoặc không được cài đặt !")
        End Try
    End Sub

    Private Sub LoadLAN()
        Dim netinfo As New NetworkExtension()
        TextBox5.Text = netinfo.ConnectionName
        TextBox6.Text = netinfo.ConnectDescription
        TextBox7.Text = netinfo.IPV4Address
        TextBox8.Text = netinfo.SubnetMask
        TextBox9.Text = netinfo.PreferredDnsServer
        TextBox10.Text = netinfo.AlternateDnsServer
        TextBox11.Text = netinfo.Gateway
        Label14.Text = String.Format("IsDnsEnabled : {0}", netinfo.IsDnsEnable)
        Label15.Text = String.Format("IsDynamicDnsEnabled : {0}", netinfo.IsDynamicDnsEnabled)
        Label16.Text = String.Format("IsDHCPEnabled : {0}", netinfo.IsDHCPEnabled)
    End Sub
    Private Sub LoadWAN()
        AddListview2(WinExt.PCInfo.PCname,
                    WinExt.PCInfo.Domain,
                    WinExt.NetInfo.LocalIP,
                    WinExt.NetInfo.Hostname,
                    WinExt.NetInfo.WanIP)
    End Sub

    Private Sub Button9_Click(sender As Object, e As EventArgs) Handles Button9.Click
        Try
            Process.Start("http://" & TextBox11.Text.Trim & "/")
        Catch ex As Exception

        End Try
    End Sub
    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click

        Dim netinfo As New NetworkExtension()

        If My.Computer.Network.IsAvailable = True Then
            If WinExt.NetInfo.IsInternet = True Then
                PictureBox3.Image = ImageList1.Images(0)
                LoadWAN()
            Else
                If netinfo.IsDHCPEnabled = False Then
                    MessageBox.Show("DHCP Server đã bị tắt !")
                End If
                PictureBox3.Image = ImageList1.Images(1)
                LoadWAN()
                MessageBox.Show("Không có kết nối mạng (Internet), kiểm tra lại bộ định tuyến (Router) !" & vbNewLine &
                               "Nếu sử dụng kết nối là Wifi hãy kiểm tra kết nối có bị tắt hay không (Disabled)")

            End If
        Else
            PictureBox3.Image = ImageList1.Images(1)
            MessageBox.Show("Kết nối mạng bị tắt (Disabled) !")
        End If
    End Sub

    Private Sub LinkLabel1_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabel1.LinkClicked
        Try
            Process.Start("https://2conglc-vn.blogspot.com/2020/05/visual-basic-windows-repair.html")
        Catch ex As Exception

        End Try
    End Sub
#End Region
End Class
