using System;
using System.Runtime.InteropServices;
using Steamworks.Data;

namespace Steamworks
{
	// Token: 0x02000017 RID: 23
	internal class ISteamInput : SteamInterface
	{
		// Token: 0x060002AE RID: 686 RVA: 0x0000610D File Offset: 0x0000430D
		internal ISteamInput(bool IsGameServer)
		{
			base.SetupInterface(IsGameServer);
		}

		// Token: 0x060002AF RID: 687
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl)]
		internal static extern IntPtr SteamAPI_SteamInput_v001();

		// Token: 0x060002B0 RID: 688 RVA: 0x0000611F File Offset: 0x0000431F
		public override IntPtr GetUserInterfacePointer()
		{
			return ISteamInput.SteamAPI_SteamInput_v001();
		}

		// Token: 0x060002B1 RID: 689
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamInput_Init")]
		[return: MarshalAs(UnmanagedType.I1)]
		private static extern bool _Init(IntPtr self);

		// Token: 0x060002B2 RID: 690 RVA: 0x00006128 File Offset: 0x00004328
		internal bool Init()
		{
			return ISteamInput._Init(this.Self);
		}

		// Token: 0x060002B3 RID: 691
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamInput_Shutdown")]
		[return: MarshalAs(UnmanagedType.I1)]
		private static extern bool _Shutdown(IntPtr self);

		// Token: 0x060002B4 RID: 692 RVA: 0x00006148 File Offset: 0x00004348
		internal bool Shutdown()
		{
			return ISteamInput._Shutdown(this.Self);
		}

		// Token: 0x060002B5 RID: 693
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamInput_RunFrame")]
		private static extern void _RunFrame(IntPtr self);

		// Token: 0x060002B6 RID: 694 RVA: 0x00006167 File Offset: 0x00004367
		internal void RunFrame()
		{
			ISteamInput._RunFrame(this.Self);
		}

		// Token: 0x060002B7 RID: 695
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamInput_GetConnectedControllers")]
		private static extern int _GetConnectedControllers(IntPtr self, [In] [Out] InputHandle_t[] handlesOut);

		// Token: 0x060002B8 RID: 696 RVA: 0x00006178 File Offset: 0x00004378
		internal int GetConnectedControllers([In] [Out] InputHandle_t[] handlesOut)
		{
			return ISteamInput._GetConnectedControllers(this.Self, handlesOut);
		}

		// Token: 0x060002B9 RID: 697
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamInput_GetActionSetHandle")]
		private static extern InputActionSetHandle_t _GetActionSetHandle(IntPtr self, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = Steamworks.Utf8StringToNative)] string pszActionSetName);

		// Token: 0x060002BA RID: 698 RVA: 0x00006198 File Offset: 0x00004398
		internal InputActionSetHandle_t GetActionSetHandle([MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = Steamworks.Utf8StringToNative)] string pszActionSetName)
		{
			return ISteamInput._GetActionSetHandle(this.Self, pszActionSetName);
		}

		// Token: 0x060002BB RID: 699
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamInput_ActivateActionSet")]
		private static extern void _ActivateActionSet(IntPtr self, InputHandle_t inputHandle, InputActionSetHandle_t actionSetHandle);

		// Token: 0x060002BC RID: 700 RVA: 0x000061B8 File Offset: 0x000043B8
		internal void ActivateActionSet(InputHandle_t inputHandle, InputActionSetHandle_t actionSetHandle)
		{
			ISteamInput._ActivateActionSet(this.Self, inputHandle, actionSetHandle);
		}

		// Token: 0x060002BD RID: 701
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamInput_GetCurrentActionSet")]
		private static extern InputActionSetHandle_t _GetCurrentActionSet(IntPtr self, InputHandle_t inputHandle);

		// Token: 0x060002BE RID: 702 RVA: 0x000061CC File Offset: 0x000043CC
		internal InputActionSetHandle_t GetCurrentActionSet(InputHandle_t inputHandle)
		{
			return ISteamInput._GetCurrentActionSet(this.Self, inputHandle);
		}

		// Token: 0x060002BF RID: 703
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamInput_ActivateActionSetLayer")]
		private static extern void _ActivateActionSetLayer(IntPtr self, InputHandle_t inputHandle, InputActionSetHandle_t actionSetLayerHandle);

		// Token: 0x060002C0 RID: 704 RVA: 0x000061EC File Offset: 0x000043EC
		internal void ActivateActionSetLayer(InputHandle_t inputHandle, InputActionSetHandle_t actionSetLayerHandle)
		{
			ISteamInput._ActivateActionSetLayer(this.Self, inputHandle, actionSetLayerHandle);
		}

		// Token: 0x060002C1 RID: 705
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamInput_DeactivateActionSetLayer")]
		private static extern void _DeactivateActionSetLayer(IntPtr self, InputHandle_t inputHandle, InputActionSetHandle_t actionSetLayerHandle);

		// Token: 0x060002C2 RID: 706 RVA: 0x000061FD File Offset: 0x000043FD
		internal void DeactivateActionSetLayer(InputHandle_t inputHandle, InputActionSetHandle_t actionSetLayerHandle)
		{
			ISteamInput._DeactivateActionSetLayer(this.Self, inputHandle, actionSetLayerHandle);
		}

		// Token: 0x060002C3 RID: 707
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamInput_DeactivateAllActionSetLayers")]
		private static extern void _DeactivateAllActionSetLayers(IntPtr self, InputHandle_t inputHandle);

		// Token: 0x060002C4 RID: 708 RVA: 0x0000620E File Offset: 0x0000440E
		internal void DeactivateAllActionSetLayers(InputHandle_t inputHandle)
		{
			ISteamInput._DeactivateAllActionSetLayers(this.Self, inputHandle);
		}

		// Token: 0x060002C5 RID: 709
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamInput_GetActiveActionSetLayers")]
		private static extern int _GetActiveActionSetLayers(IntPtr self, InputHandle_t inputHandle, [In] [Out] InputActionSetHandle_t[] handlesOut);

		// Token: 0x060002C6 RID: 710 RVA: 0x00006220 File Offset: 0x00004420
		internal int GetActiveActionSetLayers(InputHandle_t inputHandle, [In] [Out] InputActionSetHandle_t[] handlesOut)
		{
			return ISteamInput._GetActiveActionSetLayers(this.Self, inputHandle, handlesOut);
		}

		// Token: 0x060002C7 RID: 711
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamInput_GetDigitalActionHandle")]
		private static extern InputDigitalActionHandle_t _GetDigitalActionHandle(IntPtr self, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = Steamworks.Utf8StringToNative)] string pszActionName);

		// Token: 0x060002C8 RID: 712 RVA: 0x00006244 File Offset: 0x00004444
		internal InputDigitalActionHandle_t GetDigitalActionHandle([MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = Steamworks.Utf8StringToNative)] string pszActionName)
		{
			return ISteamInput._GetDigitalActionHandle(this.Self, pszActionName);
		}

		// Token: 0x060002C9 RID: 713
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamInput_GetDigitalActionData")]
		private static extern DigitalState _GetDigitalActionData(IntPtr self, InputHandle_t inputHandle, InputDigitalActionHandle_t digitalActionHandle);

		// Token: 0x060002CA RID: 714 RVA: 0x00006264 File Offset: 0x00004464
		internal DigitalState GetDigitalActionData(InputHandle_t inputHandle, InputDigitalActionHandle_t digitalActionHandle)
		{
			return ISteamInput._GetDigitalActionData(this.Self, inputHandle, digitalActionHandle);
		}

		// Token: 0x060002CB RID: 715
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamInput_GetDigitalActionOrigins")]
		private static extern int _GetDigitalActionOrigins(IntPtr self, InputHandle_t inputHandle, InputActionSetHandle_t actionSetHandle, InputDigitalActionHandle_t digitalActionHandle, ref InputActionOrigin originsOut);

		// Token: 0x060002CC RID: 716 RVA: 0x00006288 File Offset: 0x00004488
		internal int GetDigitalActionOrigins(InputHandle_t inputHandle, InputActionSetHandle_t actionSetHandle, InputDigitalActionHandle_t digitalActionHandle, ref InputActionOrigin originsOut)
		{
			return ISteamInput._GetDigitalActionOrigins(this.Self, inputHandle, actionSetHandle, digitalActionHandle, ref originsOut);
		}

		// Token: 0x060002CD RID: 717
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamInput_GetAnalogActionHandle")]
		private static extern InputAnalogActionHandle_t _GetAnalogActionHandle(IntPtr self, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = Steamworks.Utf8StringToNative)] string pszActionName);

		// Token: 0x060002CE RID: 718 RVA: 0x000062AC File Offset: 0x000044AC
		internal InputAnalogActionHandle_t GetAnalogActionHandle([MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = Steamworks.Utf8StringToNative)] string pszActionName)
		{
			return ISteamInput._GetAnalogActionHandle(this.Self, pszActionName);
		}

		// Token: 0x060002CF RID: 719
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamInput_GetAnalogActionData")]
		private static extern AnalogState _GetAnalogActionData(IntPtr self, InputHandle_t inputHandle, InputAnalogActionHandle_t analogActionHandle);

		// Token: 0x060002D0 RID: 720 RVA: 0x000062CC File Offset: 0x000044CC
		internal AnalogState GetAnalogActionData(InputHandle_t inputHandle, InputAnalogActionHandle_t analogActionHandle)
		{
			return ISteamInput._GetAnalogActionData(this.Self, inputHandle, analogActionHandle);
		}

		// Token: 0x060002D1 RID: 721
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamInput_GetAnalogActionOrigins")]
		private static extern int _GetAnalogActionOrigins(IntPtr self, InputHandle_t inputHandle, InputActionSetHandle_t actionSetHandle, InputAnalogActionHandle_t analogActionHandle, ref InputActionOrigin originsOut);

		// Token: 0x060002D2 RID: 722 RVA: 0x000062F0 File Offset: 0x000044F0
		internal int GetAnalogActionOrigins(InputHandle_t inputHandle, InputActionSetHandle_t actionSetHandle, InputAnalogActionHandle_t analogActionHandle, ref InputActionOrigin originsOut)
		{
			return ISteamInput._GetAnalogActionOrigins(this.Self, inputHandle, actionSetHandle, analogActionHandle, ref originsOut);
		}

		// Token: 0x060002D3 RID: 723
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamInput_GetGlyphForActionOrigin")]
		private static extern Utf8StringPointer _GetGlyphForActionOrigin(IntPtr self, InputActionOrigin eOrigin);

		// Token: 0x060002D4 RID: 724 RVA: 0x00006314 File Offset: 0x00004514
		internal string GetGlyphForActionOrigin(InputActionOrigin eOrigin)
		{
			Utf8StringPointer p = ISteamInput._GetGlyphForActionOrigin(this.Self, eOrigin);
			return p;
		}

		// Token: 0x060002D5 RID: 725
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamInput_GetStringForActionOrigin")]
		private static extern Utf8StringPointer _GetStringForActionOrigin(IntPtr self, InputActionOrigin eOrigin);

		// Token: 0x060002D6 RID: 726 RVA: 0x0000633C File Offset: 0x0000453C
		internal string GetStringForActionOrigin(InputActionOrigin eOrigin)
		{
			Utf8StringPointer p = ISteamInput._GetStringForActionOrigin(this.Self, eOrigin);
			return p;
		}

		// Token: 0x060002D7 RID: 727
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamInput_StopAnalogActionMomentum")]
		private static extern void _StopAnalogActionMomentum(IntPtr self, InputHandle_t inputHandle, InputAnalogActionHandle_t eAction);

		// Token: 0x060002D8 RID: 728 RVA: 0x00006361 File Offset: 0x00004561
		internal void StopAnalogActionMomentum(InputHandle_t inputHandle, InputAnalogActionHandle_t eAction)
		{
			ISteamInput._StopAnalogActionMomentum(this.Self, inputHandle, eAction);
		}

		// Token: 0x060002D9 RID: 729
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamInput_GetMotionData")]
		private static extern MotionState _GetMotionData(IntPtr self, InputHandle_t inputHandle);

		// Token: 0x060002DA RID: 730 RVA: 0x00006374 File Offset: 0x00004574
		internal MotionState GetMotionData(InputHandle_t inputHandle)
		{
			return ISteamInput._GetMotionData(this.Self, inputHandle);
		}

		// Token: 0x060002DB RID: 731
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamInput_TriggerVibration")]
		private static extern void _TriggerVibration(IntPtr self, InputHandle_t inputHandle, ushort usLeftSpeed, ushort usRightSpeed);

		// Token: 0x060002DC RID: 732 RVA: 0x00006394 File Offset: 0x00004594
		internal void TriggerVibration(InputHandle_t inputHandle, ushort usLeftSpeed, ushort usRightSpeed)
		{
			ISteamInput._TriggerVibration(this.Self, inputHandle, usLeftSpeed, usRightSpeed);
		}

		// Token: 0x060002DD RID: 733
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamInput_SetLEDColor")]
		private static extern void _SetLEDColor(IntPtr self, InputHandle_t inputHandle, byte nColorR, byte nColorG, byte nColorB, uint nFlags);

		// Token: 0x060002DE RID: 734 RVA: 0x000063A6 File Offset: 0x000045A6
		internal void SetLEDColor(InputHandle_t inputHandle, byte nColorR, byte nColorG, byte nColorB, uint nFlags)
		{
			ISteamInput._SetLEDColor(this.Self, inputHandle, nColorR, nColorG, nColorB, nFlags);
		}

		// Token: 0x060002DF RID: 735
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamInput_TriggerHapticPulse")]
		private static extern void _TriggerHapticPulse(IntPtr self, InputHandle_t inputHandle, SteamControllerPad eTargetPad, ushort usDurationMicroSec);

		// Token: 0x060002E0 RID: 736 RVA: 0x000063BC File Offset: 0x000045BC
		internal void TriggerHapticPulse(InputHandle_t inputHandle, SteamControllerPad eTargetPad, ushort usDurationMicroSec)
		{
			ISteamInput._TriggerHapticPulse(this.Self, inputHandle, eTargetPad, usDurationMicroSec);
		}

		// Token: 0x060002E1 RID: 737
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamInput_TriggerRepeatedHapticPulse")]
		private static extern void _TriggerRepeatedHapticPulse(IntPtr self, InputHandle_t inputHandle, SteamControllerPad eTargetPad, ushort usDurationMicroSec, ushort usOffMicroSec, ushort unRepeat, uint nFlags);

		// Token: 0x060002E2 RID: 738 RVA: 0x000063CE File Offset: 0x000045CE
		internal void TriggerRepeatedHapticPulse(InputHandle_t inputHandle, SteamControllerPad eTargetPad, ushort usDurationMicroSec, ushort usOffMicroSec, ushort unRepeat, uint nFlags)
		{
			ISteamInput._TriggerRepeatedHapticPulse(this.Self, inputHandle, eTargetPad, usDurationMicroSec, usOffMicroSec, unRepeat, nFlags);
		}

		// Token: 0x060002E3 RID: 739
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamInput_ShowBindingPanel")]
		[return: MarshalAs(UnmanagedType.I1)]
		private static extern bool _ShowBindingPanel(IntPtr self, InputHandle_t inputHandle);

		// Token: 0x060002E4 RID: 740 RVA: 0x000063E8 File Offset: 0x000045E8
		internal bool ShowBindingPanel(InputHandle_t inputHandle)
		{
			return ISteamInput._ShowBindingPanel(this.Self, inputHandle);
		}

		// Token: 0x060002E5 RID: 741
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamInput_GetInputTypeForHandle")]
		private static extern InputType _GetInputTypeForHandle(IntPtr self, InputHandle_t inputHandle);

		// Token: 0x060002E6 RID: 742 RVA: 0x00006408 File Offset: 0x00004608
		internal InputType GetInputTypeForHandle(InputHandle_t inputHandle)
		{
			return ISteamInput._GetInputTypeForHandle(this.Self, inputHandle);
		}

		// Token: 0x060002E7 RID: 743
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamInput_GetControllerForGamepadIndex")]
		private static extern InputHandle_t _GetControllerForGamepadIndex(IntPtr self, int nIndex);

		// Token: 0x060002E8 RID: 744 RVA: 0x00006428 File Offset: 0x00004628
		internal InputHandle_t GetControllerForGamepadIndex(int nIndex)
		{
			return ISteamInput._GetControllerForGamepadIndex(this.Self, nIndex);
		}

		// Token: 0x060002E9 RID: 745
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamInput_GetGamepadIndexForController")]
		private static extern int _GetGamepadIndexForController(IntPtr self, InputHandle_t ulinputHandle);

		// Token: 0x060002EA RID: 746 RVA: 0x00006448 File Offset: 0x00004648
		internal int GetGamepadIndexForController(InputHandle_t ulinputHandle)
		{
			return ISteamInput._GetGamepadIndexForController(this.Self, ulinputHandle);
		}

		// Token: 0x060002EB RID: 747
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamInput_GetStringForXboxOrigin")]
		private static extern Utf8StringPointer _GetStringForXboxOrigin(IntPtr self, XboxOrigin eOrigin);

		// Token: 0x060002EC RID: 748 RVA: 0x00006468 File Offset: 0x00004668
		internal string GetStringForXboxOrigin(XboxOrigin eOrigin)
		{
			Utf8StringPointer p = ISteamInput._GetStringForXboxOrigin(this.Self, eOrigin);
			return p;
		}

		// Token: 0x060002ED RID: 749
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamInput_GetGlyphForXboxOrigin")]
		private static extern Utf8StringPointer _GetGlyphForXboxOrigin(IntPtr self, XboxOrigin eOrigin);

		// Token: 0x060002EE RID: 750 RVA: 0x00006490 File Offset: 0x00004690
		internal string GetGlyphForXboxOrigin(XboxOrigin eOrigin)
		{
			Utf8StringPointer p = ISteamInput._GetGlyphForXboxOrigin(this.Self, eOrigin);
			return p;
		}

		// Token: 0x060002EF RID: 751
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamInput_GetActionOriginFromXboxOrigin")]
		private static extern InputActionOrigin _GetActionOriginFromXboxOrigin(IntPtr self, InputHandle_t inputHandle, XboxOrigin eOrigin);

		// Token: 0x060002F0 RID: 752 RVA: 0x000064B8 File Offset: 0x000046B8
		internal InputActionOrigin GetActionOriginFromXboxOrigin(InputHandle_t inputHandle, XboxOrigin eOrigin)
		{
			return ISteamInput._GetActionOriginFromXboxOrigin(this.Self, inputHandle, eOrigin);
		}

		// Token: 0x060002F1 RID: 753
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamInput_TranslateActionOrigin")]
		private static extern InputActionOrigin _TranslateActionOrigin(IntPtr self, InputType eDestinationInputType, InputActionOrigin eSourceOrigin);

		// Token: 0x060002F2 RID: 754 RVA: 0x000064DC File Offset: 0x000046DC
		internal InputActionOrigin TranslateActionOrigin(InputType eDestinationInputType, InputActionOrigin eSourceOrigin)
		{
			return ISteamInput._TranslateActionOrigin(this.Self, eDestinationInputType, eSourceOrigin);
		}

		// Token: 0x060002F3 RID: 755
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamInput_GetDeviceBindingRevision")]
		[return: MarshalAs(UnmanagedType.I1)]
		private static extern bool _GetDeviceBindingRevision(IntPtr self, InputHandle_t inputHandle, ref int pMajor, ref int pMinor);

		// Token: 0x060002F4 RID: 756 RVA: 0x00006500 File Offset: 0x00004700
		internal bool GetDeviceBindingRevision(InputHandle_t inputHandle, ref int pMajor, ref int pMinor)
		{
			return ISteamInput._GetDeviceBindingRevision(this.Self, inputHandle, ref pMajor, ref pMinor);
		}

		// Token: 0x060002F5 RID: 757
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamInput_GetRemotePlaySessionID")]
		private static extern uint _GetRemotePlaySessionID(IntPtr self, InputHandle_t inputHandle);

		// Token: 0x060002F6 RID: 758 RVA: 0x00006524 File Offset: 0x00004724
		internal uint GetRemotePlaySessionID(InputHandle_t inputHandle)
		{
			return ISteamInput._GetRemotePlaySessionID(this.Self, inputHandle);
		}
	}
}
