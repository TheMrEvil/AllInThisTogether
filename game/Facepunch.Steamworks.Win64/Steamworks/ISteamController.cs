using System;
using System.Runtime.InteropServices;
using Steamworks.Data;

namespace Steamworks
{
	// Token: 0x02000010 RID: 16
	internal class ISteamController : SteamInterface
	{
		// Token: 0x060000BD RID: 189 RVA: 0x00004556 File Offset: 0x00002756
		internal ISteamController(bool IsGameServer)
		{
			base.SetupInterface(IsGameServer);
		}

		// Token: 0x060000BE RID: 190
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl)]
		internal static extern IntPtr SteamAPI_SteamController_v007();

		// Token: 0x060000BF RID: 191 RVA: 0x00004568 File Offset: 0x00002768
		public override IntPtr GetUserInterfacePointer()
		{
			return ISteamController.SteamAPI_SteamController_v007();
		}

		// Token: 0x060000C0 RID: 192
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamController_Init")]
		[return: MarshalAs(UnmanagedType.I1)]
		private static extern bool _Init(IntPtr self);

		// Token: 0x060000C1 RID: 193 RVA: 0x00004570 File Offset: 0x00002770
		internal bool Init()
		{
			return ISteamController._Init(this.Self);
		}

		// Token: 0x060000C2 RID: 194
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamController_Shutdown")]
		[return: MarshalAs(UnmanagedType.I1)]
		private static extern bool _Shutdown(IntPtr self);

		// Token: 0x060000C3 RID: 195 RVA: 0x00004590 File Offset: 0x00002790
		internal bool Shutdown()
		{
			return ISteamController._Shutdown(this.Self);
		}

		// Token: 0x060000C4 RID: 196
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamController_RunFrame")]
		private static extern void _RunFrame(IntPtr self);

		// Token: 0x060000C5 RID: 197 RVA: 0x000045AF File Offset: 0x000027AF
		internal void RunFrame()
		{
			ISteamController._RunFrame(this.Self);
		}

		// Token: 0x060000C6 RID: 198
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamController_GetConnectedControllers")]
		private static extern int _GetConnectedControllers(IntPtr self, [In] [Out] ControllerHandle_t[] handlesOut);

		// Token: 0x060000C7 RID: 199 RVA: 0x000045C0 File Offset: 0x000027C0
		internal int GetConnectedControllers([In] [Out] ControllerHandle_t[] handlesOut)
		{
			return ISteamController._GetConnectedControllers(this.Self, handlesOut);
		}

		// Token: 0x060000C8 RID: 200
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamController_GetActionSetHandle")]
		private static extern ControllerActionSetHandle_t _GetActionSetHandle(IntPtr self, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = Steamworks.Utf8StringToNative)] string pszActionSetName);

		// Token: 0x060000C9 RID: 201 RVA: 0x000045E0 File Offset: 0x000027E0
		internal ControllerActionSetHandle_t GetActionSetHandle([MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = Steamworks.Utf8StringToNative)] string pszActionSetName)
		{
			return ISteamController._GetActionSetHandle(this.Self, pszActionSetName);
		}

		// Token: 0x060000CA RID: 202
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamController_ActivateActionSet")]
		private static extern void _ActivateActionSet(IntPtr self, ControllerHandle_t controllerHandle, ControllerActionSetHandle_t actionSetHandle);

		// Token: 0x060000CB RID: 203 RVA: 0x00004600 File Offset: 0x00002800
		internal void ActivateActionSet(ControllerHandle_t controllerHandle, ControllerActionSetHandle_t actionSetHandle)
		{
			ISteamController._ActivateActionSet(this.Self, controllerHandle, actionSetHandle);
		}

		// Token: 0x060000CC RID: 204
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamController_GetCurrentActionSet")]
		private static extern ControllerActionSetHandle_t _GetCurrentActionSet(IntPtr self, ControllerHandle_t controllerHandle);

		// Token: 0x060000CD RID: 205 RVA: 0x00004614 File Offset: 0x00002814
		internal ControllerActionSetHandle_t GetCurrentActionSet(ControllerHandle_t controllerHandle)
		{
			return ISteamController._GetCurrentActionSet(this.Self, controllerHandle);
		}

		// Token: 0x060000CE RID: 206
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamController_ActivateActionSetLayer")]
		private static extern void _ActivateActionSetLayer(IntPtr self, ControllerHandle_t controllerHandle, ControllerActionSetHandle_t actionSetLayerHandle);

		// Token: 0x060000CF RID: 207 RVA: 0x00004634 File Offset: 0x00002834
		internal void ActivateActionSetLayer(ControllerHandle_t controllerHandle, ControllerActionSetHandle_t actionSetLayerHandle)
		{
			ISteamController._ActivateActionSetLayer(this.Self, controllerHandle, actionSetLayerHandle);
		}

		// Token: 0x060000D0 RID: 208
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamController_DeactivateActionSetLayer")]
		private static extern void _DeactivateActionSetLayer(IntPtr self, ControllerHandle_t controllerHandle, ControllerActionSetHandle_t actionSetLayerHandle);

		// Token: 0x060000D1 RID: 209 RVA: 0x00004645 File Offset: 0x00002845
		internal void DeactivateActionSetLayer(ControllerHandle_t controllerHandle, ControllerActionSetHandle_t actionSetLayerHandle)
		{
			ISteamController._DeactivateActionSetLayer(this.Self, controllerHandle, actionSetLayerHandle);
		}

		// Token: 0x060000D2 RID: 210
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamController_DeactivateAllActionSetLayers")]
		private static extern void _DeactivateAllActionSetLayers(IntPtr self, ControllerHandle_t controllerHandle);

		// Token: 0x060000D3 RID: 211 RVA: 0x00004656 File Offset: 0x00002856
		internal void DeactivateAllActionSetLayers(ControllerHandle_t controllerHandle)
		{
			ISteamController._DeactivateAllActionSetLayers(this.Self, controllerHandle);
		}

		// Token: 0x060000D4 RID: 212
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamController_GetActiveActionSetLayers")]
		private static extern int _GetActiveActionSetLayers(IntPtr self, ControllerHandle_t controllerHandle, [In] [Out] ControllerActionSetHandle_t[] handlesOut);

		// Token: 0x060000D5 RID: 213 RVA: 0x00004668 File Offset: 0x00002868
		internal int GetActiveActionSetLayers(ControllerHandle_t controllerHandle, [In] [Out] ControllerActionSetHandle_t[] handlesOut)
		{
			return ISteamController._GetActiveActionSetLayers(this.Self, controllerHandle, handlesOut);
		}

		// Token: 0x060000D6 RID: 214
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamController_GetDigitalActionHandle")]
		private static extern ControllerDigitalActionHandle_t _GetDigitalActionHandle(IntPtr self, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = Steamworks.Utf8StringToNative)] string pszActionName);

		// Token: 0x060000D7 RID: 215 RVA: 0x0000468C File Offset: 0x0000288C
		internal ControllerDigitalActionHandle_t GetDigitalActionHandle([MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = Steamworks.Utf8StringToNative)] string pszActionName)
		{
			return ISteamController._GetDigitalActionHandle(this.Self, pszActionName);
		}

		// Token: 0x060000D8 RID: 216
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamController_GetDigitalActionData")]
		private static extern DigitalState _GetDigitalActionData(IntPtr self, ControllerHandle_t controllerHandle, ControllerDigitalActionHandle_t digitalActionHandle);

		// Token: 0x060000D9 RID: 217 RVA: 0x000046AC File Offset: 0x000028AC
		internal DigitalState GetDigitalActionData(ControllerHandle_t controllerHandle, ControllerDigitalActionHandle_t digitalActionHandle)
		{
			return ISteamController._GetDigitalActionData(this.Self, controllerHandle, digitalActionHandle);
		}

		// Token: 0x060000DA RID: 218
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamController_GetDigitalActionOrigins")]
		private static extern int _GetDigitalActionOrigins(IntPtr self, ControllerHandle_t controllerHandle, ControllerActionSetHandle_t actionSetHandle, ControllerDigitalActionHandle_t digitalActionHandle, ref ControllerActionOrigin originsOut);

		// Token: 0x060000DB RID: 219 RVA: 0x000046D0 File Offset: 0x000028D0
		internal int GetDigitalActionOrigins(ControllerHandle_t controllerHandle, ControllerActionSetHandle_t actionSetHandle, ControllerDigitalActionHandle_t digitalActionHandle, ref ControllerActionOrigin originsOut)
		{
			return ISteamController._GetDigitalActionOrigins(this.Self, controllerHandle, actionSetHandle, digitalActionHandle, ref originsOut);
		}

		// Token: 0x060000DC RID: 220
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamController_GetAnalogActionHandle")]
		private static extern ControllerAnalogActionHandle_t _GetAnalogActionHandle(IntPtr self, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = Steamworks.Utf8StringToNative)] string pszActionName);

		// Token: 0x060000DD RID: 221 RVA: 0x000046F4 File Offset: 0x000028F4
		internal ControllerAnalogActionHandle_t GetAnalogActionHandle([MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = Steamworks.Utf8StringToNative)] string pszActionName)
		{
			return ISteamController._GetAnalogActionHandle(this.Self, pszActionName);
		}

		// Token: 0x060000DE RID: 222
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamController_GetAnalogActionData")]
		private static extern AnalogState _GetAnalogActionData(IntPtr self, ControllerHandle_t controllerHandle, ControllerAnalogActionHandle_t analogActionHandle);

		// Token: 0x060000DF RID: 223 RVA: 0x00004714 File Offset: 0x00002914
		internal AnalogState GetAnalogActionData(ControllerHandle_t controllerHandle, ControllerAnalogActionHandle_t analogActionHandle)
		{
			return ISteamController._GetAnalogActionData(this.Self, controllerHandle, analogActionHandle);
		}

		// Token: 0x060000E0 RID: 224
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamController_GetAnalogActionOrigins")]
		private static extern int _GetAnalogActionOrigins(IntPtr self, ControllerHandle_t controllerHandle, ControllerActionSetHandle_t actionSetHandle, ControllerAnalogActionHandle_t analogActionHandle, ref ControllerActionOrigin originsOut);

		// Token: 0x060000E1 RID: 225 RVA: 0x00004738 File Offset: 0x00002938
		internal int GetAnalogActionOrigins(ControllerHandle_t controllerHandle, ControllerActionSetHandle_t actionSetHandle, ControllerAnalogActionHandle_t analogActionHandle, ref ControllerActionOrigin originsOut)
		{
			return ISteamController._GetAnalogActionOrigins(this.Self, controllerHandle, actionSetHandle, analogActionHandle, ref originsOut);
		}

		// Token: 0x060000E2 RID: 226
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamController_GetGlyphForActionOrigin")]
		private static extern Utf8StringPointer _GetGlyphForActionOrigin(IntPtr self, ControllerActionOrigin eOrigin);

		// Token: 0x060000E3 RID: 227 RVA: 0x0000475C File Offset: 0x0000295C
		internal string GetGlyphForActionOrigin(ControllerActionOrigin eOrigin)
		{
			Utf8StringPointer p = ISteamController._GetGlyphForActionOrigin(this.Self, eOrigin);
			return p;
		}

		// Token: 0x060000E4 RID: 228
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamController_GetStringForActionOrigin")]
		private static extern Utf8StringPointer _GetStringForActionOrigin(IntPtr self, ControllerActionOrigin eOrigin);

		// Token: 0x060000E5 RID: 229 RVA: 0x00004784 File Offset: 0x00002984
		internal string GetStringForActionOrigin(ControllerActionOrigin eOrigin)
		{
			Utf8StringPointer p = ISteamController._GetStringForActionOrigin(this.Self, eOrigin);
			return p;
		}

		// Token: 0x060000E6 RID: 230
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamController_StopAnalogActionMomentum")]
		private static extern void _StopAnalogActionMomentum(IntPtr self, ControllerHandle_t controllerHandle, ControllerAnalogActionHandle_t eAction);

		// Token: 0x060000E7 RID: 231 RVA: 0x000047A9 File Offset: 0x000029A9
		internal void StopAnalogActionMomentum(ControllerHandle_t controllerHandle, ControllerAnalogActionHandle_t eAction)
		{
			ISteamController._StopAnalogActionMomentum(this.Self, controllerHandle, eAction);
		}

		// Token: 0x060000E8 RID: 232
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamController_GetMotionData")]
		private static extern MotionState _GetMotionData(IntPtr self, ControllerHandle_t controllerHandle);

		// Token: 0x060000E9 RID: 233 RVA: 0x000047BC File Offset: 0x000029BC
		internal MotionState GetMotionData(ControllerHandle_t controllerHandle)
		{
			return ISteamController._GetMotionData(this.Self, controllerHandle);
		}

		// Token: 0x060000EA RID: 234
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamController_TriggerHapticPulse")]
		private static extern void _TriggerHapticPulse(IntPtr self, ControllerHandle_t controllerHandle, SteamControllerPad eTargetPad, ushort usDurationMicroSec);

		// Token: 0x060000EB RID: 235 RVA: 0x000047DC File Offset: 0x000029DC
		internal void TriggerHapticPulse(ControllerHandle_t controllerHandle, SteamControllerPad eTargetPad, ushort usDurationMicroSec)
		{
			ISteamController._TriggerHapticPulse(this.Self, controllerHandle, eTargetPad, usDurationMicroSec);
		}

		// Token: 0x060000EC RID: 236
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamController_TriggerRepeatedHapticPulse")]
		private static extern void _TriggerRepeatedHapticPulse(IntPtr self, ControllerHandle_t controllerHandle, SteamControllerPad eTargetPad, ushort usDurationMicroSec, ushort usOffMicroSec, ushort unRepeat, uint nFlags);

		// Token: 0x060000ED RID: 237 RVA: 0x000047EE File Offset: 0x000029EE
		internal void TriggerRepeatedHapticPulse(ControllerHandle_t controllerHandle, SteamControllerPad eTargetPad, ushort usDurationMicroSec, ushort usOffMicroSec, ushort unRepeat, uint nFlags)
		{
			ISteamController._TriggerRepeatedHapticPulse(this.Self, controllerHandle, eTargetPad, usDurationMicroSec, usOffMicroSec, unRepeat, nFlags);
		}

		// Token: 0x060000EE RID: 238
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamController_TriggerVibration")]
		private static extern void _TriggerVibration(IntPtr self, ControllerHandle_t controllerHandle, ushort usLeftSpeed, ushort usRightSpeed);

		// Token: 0x060000EF RID: 239 RVA: 0x00004806 File Offset: 0x00002A06
		internal void TriggerVibration(ControllerHandle_t controllerHandle, ushort usLeftSpeed, ushort usRightSpeed)
		{
			ISteamController._TriggerVibration(this.Self, controllerHandle, usLeftSpeed, usRightSpeed);
		}

		// Token: 0x060000F0 RID: 240
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamController_SetLEDColor")]
		private static extern void _SetLEDColor(IntPtr self, ControllerHandle_t controllerHandle, byte nColorR, byte nColorG, byte nColorB, uint nFlags);

		// Token: 0x060000F1 RID: 241 RVA: 0x00004818 File Offset: 0x00002A18
		internal void SetLEDColor(ControllerHandle_t controllerHandle, byte nColorR, byte nColorG, byte nColorB, uint nFlags)
		{
			ISteamController._SetLEDColor(this.Self, controllerHandle, nColorR, nColorG, nColorB, nFlags);
		}

		// Token: 0x060000F2 RID: 242
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamController_ShowBindingPanel")]
		[return: MarshalAs(UnmanagedType.I1)]
		private static extern bool _ShowBindingPanel(IntPtr self, ControllerHandle_t controllerHandle);

		// Token: 0x060000F3 RID: 243 RVA: 0x00004830 File Offset: 0x00002A30
		internal bool ShowBindingPanel(ControllerHandle_t controllerHandle)
		{
			return ISteamController._ShowBindingPanel(this.Self, controllerHandle);
		}

		// Token: 0x060000F4 RID: 244
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamController_GetInputTypeForHandle")]
		private static extern InputType _GetInputTypeForHandle(IntPtr self, ControllerHandle_t controllerHandle);

		// Token: 0x060000F5 RID: 245 RVA: 0x00004850 File Offset: 0x00002A50
		internal InputType GetInputTypeForHandle(ControllerHandle_t controllerHandle)
		{
			return ISteamController._GetInputTypeForHandle(this.Self, controllerHandle);
		}

		// Token: 0x060000F6 RID: 246
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamController_GetControllerForGamepadIndex")]
		private static extern ControllerHandle_t _GetControllerForGamepadIndex(IntPtr self, int nIndex);

		// Token: 0x060000F7 RID: 247 RVA: 0x00004870 File Offset: 0x00002A70
		internal ControllerHandle_t GetControllerForGamepadIndex(int nIndex)
		{
			return ISteamController._GetControllerForGamepadIndex(this.Self, nIndex);
		}

		// Token: 0x060000F8 RID: 248
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamController_GetGamepadIndexForController")]
		private static extern int _GetGamepadIndexForController(IntPtr self, ControllerHandle_t ulControllerHandle);

		// Token: 0x060000F9 RID: 249 RVA: 0x00004890 File Offset: 0x00002A90
		internal int GetGamepadIndexForController(ControllerHandle_t ulControllerHandle)
		{
			return ISteamController._GetGamepadIndexForController(this.Self, ulControllerHandle);
		}

		// Token: 0x060000FA RID: 250
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamController_GetStringForXboxOrigin")]
		private static extern Utf8StringPointer _GetStringForXboxOrigin(IntPtr self, XboxOrigin eOrigin);

		// Token: 0x060000FB RID: 251 RVA: 0x000048B0 File Offset: 0x00002AB0
		internal string GetStringForXboxOrigin(XboxOrigin eOrigin)
		{
			Utf8StringPointer p = ISteamController._GetStringForXboxOrigin(this.Self, eOrigin);
			return p;
		}

		// Token: 0x060000FC RID: 252
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamController_GetGlyphForXboxOrigin")]
		private static extern Utf8StringPointer _GetGlyphForXboxOrigin(IntPtr self, XboxOrigin eOrigin);

		// Token: 0x060000FD RID: 253 RVA: 0x000048D8 File Offset: 0x00002AD8
		internal string GetGlyphForXboxOrigin(XboxOrigin eOrigin)
		{
			Utf8StringPointer p = ISteamController._GetGlyphForXboxOrigin(this.Self, eOrigin);
			return p;
		}

		// Token: 0x060000FE RID: 254
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamController_GetActionOriginFromXboxOrigin")]
		private static extern ControllerActionOrigin _GetActionOriginFromXboxOrigin(IntPtr self, ControllerHandle_t controllerHandle, XboxOrigin eOrigin);

		// Token: 0x060000FF RID: 255 RVA: 0x00004900 File Offset: 0x00002B00
		internal ControllerActionOrigin GetActionOriginFromXboxOrigin(ControllerHandle_t controllerHandle, XboxOrigin eOrigin)
		{
			return ISteamController._GetActionOriginFromXboxOrigin(this.Self, controllerHandle, eOrigin);
		}

		// Token: 0x06000100 RID: 256
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamController_TranslateActionOrigin")]
		private static extern ControllerActionOrigin _TranslateActionOrigin(IntPtr self, InputType eDestinationInputType, ControllerActionOrigin eSourceOrigin);

		// Token: 0x06000101 RID: 257 RVA: 0x00004924 File Offset: 0x00002B24
		internal ControllerActionOrigin TranslateActionOrigin(InputType eDestinationInputType, ControllerActionOrigin eSourceOrigin)
		{
			return ISteamController._TranslateActionOrigin(this.Self, eDestinationInputType, eSourceOrigin);
		}

		// Token: 0x06000102 RID: 258
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamController_GetControllerBindingRevision")]
		[return: MarshalAs(UnmanagedType.I1)]
		private static extern bool _GetControllerBindingRevision(IntPtr self, ControllerHandle_t controllerHandle, ref int pMajor, ref int pMinor);

		// Token: 0x06000103 RID: 259 RVA: 0x00004948 File Offset: 0x00002B48
		internal bool GetControllerBindingRevision(ControllerHandle_t controllerHandle, ref int pMajor, ref int pMinor)
		{
			return ISteamController._GetControllerBindingRevision(this.Self, controllerHandle, ref pMajor, ref pMinor);
		}
	}
}
