using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace UnityEngine.UIElements
{
	// Token: 0x02000214 RID: 532
	internal static class PointerDeviceState
	{
		// Token: 0x06001043 RID: 4163 RVA: 0x00041B3C File Offset: 0x0003FD3C
		internal static void Reset()
		{
			for (int i = 0; i < PointerId.maxPointers; i++)
			{
				PointerDeviceState.s_PlayerPointerLocations[i].SetLocation(Vector2.zero, null);
				PointerDeviceState.s_PressedButtons[i] = 0;
				PointerDeviceState.s_PlayerPanelWithSoftPointerCapture[i] = null;
			}
		}

		// Token: 0x06001044 RID: 4164 RVA: 0x00041B88 File Offset: 0x0003FD88
		internal static void RemovePanelData(IPanel panel)
		{
			for (int i = 0; i < PointerId.maxPointers; i++)
			{
				bool flag = PointerDeviceState.s_PlayerPointerLocations[i].Panel == panel;
				if (flag)
				{
					PointerDeviceState.s_PlayerPointerLocations[i].SetLocation(Vector2.zero, null);
				}
				bool flag2 = PointerDeviceState.s_PlayerPanelWithSoftPointerCapture[i] == panel;
				if (flag2)
				{
					PointerDeviceState.s_PlayerPanelWithSoftPointerCapture[i] = null;
				}
			}
		}

		// Token: 0x06001045 RID: 4165 RVA: 0x00041BF0 File Offset: 0x0003FDF0
		public static void SavePointerPosition(int pointerId, Vector2 position, IPanel panel, ContextType contextType)
		{
			if (contextType > ContextType.Editor)
			{
			}
			PointerDeviceState.s_PlayerPointerLocations[pointerId].SetLocation(position, panel);
		}

		// Token: 0x06001046 RID: 4166 RVA: 0x00041C1D File Offset: 0x0003FE1D
		public static void PressButton(int pointerId, int buttonId)
		{
			Debug.Assert(buttonId >= 0);
			Debug.Assert(buttonId < 32);
			PointerDeviceState.s_PressedButtons[pointerId] |= 1 << buttonId;
		}

		// Token: 0x06001047 RID: 4167 RVA: 0x00041C4D File Offset: 0x0003FE4D
		public static void ReleaseButton(int pointerId, int buttonId)
		{
			Debug.Assert(buttonId >= 0);
			Debug.Assert(buttonId < 32);
			PointerDeviceState.s_PressedButtons[pointerId] &= ~(1 << buttonId);
		}

		// Token: 0x06001048 RID: 4168 RVA: 0x00041C7E File Offset: 0x0003FE7E
		public static void ReleaseAllButtons(int pointerId)
		{
			PointerDeviceState.s_PressedButtons[pointerId] = 0;
		}

		// Token: 0x06001049 RID: 4169 RVA: 0x00041C8C File Offset: 0x0003FE8C
		public static Vector2 GetPointerPosition(int pointerId, ContextType contextType)
		{
			if (contextType > ContextType.Editor)
			{
			}
			return PointerDeviceState.s_PlayerPointerLocations[pointerId].Position;
		}

		// Token: 0x0600104A RID: 4170 RVA: 0x00041CB8 File Offset: 0x0003FEB8
		public static IPanel GetPanel(int pointerId, ContextType contextType)
		{
			if (contextType > ContextType.Editor)
			{
			}
			return PointerDeviceState.s_PlayerPointerLocations[pointerId].Panel;
		}

		// Token: 0x0600104B RID: 4171 RVA: 0x00041CE4 File Offset: 0x0003FEE4
		private static bool HasFlagFast(PointerDeviceState.LocationFlag flagSet, PointerDeviceState.LocationFlag flag)
		{
			return (flagSet & flag) == flag;
		}

		// Token: 0x0600104C RID: 4172 RVA: 0x00041CFC File Offset: 0x0003FEFC
		public static bool HasLocationFlag(int pointerId, ContextType contextType, PointerDeviceState.LocationFlag flag)
		{
			if (contextType > ContextType.Editor)
			{
			}
			return PointerDeviceState.HasFlagFast(PointerDeviceState.s_PlayerPointerLocations[pointerId].Flags, flag);
		}

		// Token: 0x0600104D RID: 4173 RVA: 0x00041D30 File Offset: 0x0003FF30
		public static int GetPressedButtons(int pointerId)
		{
			return PointerDeviceState.s_PressedButtons[pointerId];
		}

		// Token: 0x0600104E RID: 4174 RVA: 0x00041D4C File Offset: 0x0003FF4C
		internal static bool HasAdditionalPressedButtons(int pointerId, int exceptButtonId)
		{
			return (PointerDeviceState.s_PressedButtons[pointerId] & ~(1 << exceptButtonId)) != 0;
		}

		// Token: 0x0600104F RID: 4175 RVA: 0x00041D70 File Offset: 0x0003FF70
		internal static void SetPlayerPanelWithSoftPointerCapture(int pointerId, IPanel panel)
		{
			PointerDeviceState.s_PlayerPanelWithSoftPointerCapture[pointerId] = panel;
		}

		// Token: 0x06001050 RID: 4176 RVA: 0x00041D7C File Offset: 0x0003FF7C
		internal static IPanel GetPlayerPanelWithSoftPointerCapture(int pointerId)
		{
			return PointerDeviceState.s_PlayerPanelWithSoftPointerCapture[pointerId];
		}

		// Token: 0x06001051 RID: 4177 RVA: 0x00041D95 File Offset: 0x0003FF95
		// Note: this type is marked as 'beforefieldinit'.
		static PointerDeviceState()
		{
		}

		// Token: 0x0400073D RID: 1853
		private static PointerDeviceState.PointerLocation[] s_PlayerPointerLocations = new PointerDeviceState.PointerLocation[PointerId.maxPointers];

		// Token: 0x0400073E RID: 1854
		private static int[] s_PressedButtons = new int[PointerId.maxPointers];

		// Token: 0x0400073F RID: 1855
		private static readonly IPanel[] s_PlayerPanelWithSoftPointerCapture = new IPanel[PointerId.maxPointers];

		// Token: 0x02000215 RID: 533
		[Flags]
		internal enum LocationFlag
		{
			// Token: 0x04000741 RID: 1857
			None = 0,
			// Token: 0x04000742 RID: 1858
			OutsidePanel = 1
		}

		// Token: 0x02000216 RID: 534
		private struct PointerLocation
		{
			// Token: 0x17000377 RID: 887
			// (get) Token: 0x06001052 RID: 4178 RVA: 0x00041DC4 File Offset: 0x0003FFC4
			// (set) Token: 0x06001053 RID: 4179 RVA: 0x00041DCC File Offset: 0x0003FFCC
			internal Vector2 Position
			{
				[CompilerGenerated]
				readonly get
				{
					return this.<Position>k__BackingField;
				}
				[CompilerGenerated]
				private set
				{
					this.<Position>k__BackingField = value;
				}
			}

			// Token: 0x17000378 RID: 888
			// (get) Token: 0x06001054 RID: 4180 RVA: 0x00041DD5 File Offset: 0x0003FFD5
			// (set) Token: 0x06001055 RID: 4181 RVA: 0x00041DDD File Offset: 0x0003FFDD
			internal IPanel Panel
			{
				[CompilerGenerated]
				readonly get
				{
					return this.<Panel>k__BackingField;
				}
				[CompilerGenerated]
				private set
				{
					this.<Panel>k__BackingField = value;
				}
			}

			// Token: 0x17000379 RID: 889
			// (get) Token: 0x06001056 RID: 4182 RVA: 0x00041DE6 File Offset: 0x0003FFE6
			// (set) Token: 0x06001057 RID: 4183 RVA: 0x00041DEE File Offset: 0x0003FFEE
			internal PointerDeviceState.LocationFlag Flags
			{
				[CompilerGenerated]
				readonly get
				{
					return this.<Flags>k__BackingField;
				}
				[CompilerGenerated]
				private set
				{
					this.<Flags>k__BackingField = value;
				}
			}

			// Token: 0x06001058 RID: 4184 RVA: 0x00041DF8 File Offset: 0x0003FFF8
			internal void SetLocation(Vector2 position, IPanel panel)
			{
				this.Position = position;
				this.Panel = panel;
				this.Flags = PointerDeviceState.LocationFlag.None;
				bool flag = panel == null || !panel.visualTree.layout.Contains(position);
				if (flag)
				{
					this.Flags |= PointerDeviceState.LocationFlag.OutsidePanel;
				}
			}

			// Token: 0x04000743 RID: 1859
			[DebuggerBrowsable(DebuggerBrowsableState.Never)]
			[CompilerGenerated]
			private Vector2 <Position>k__BackingField;

			// Token: 0x04000744 RID: 1860
			[DebuggerBrowsable(DebuggerBrowsableState.Never)]
			[CompilerGenerated]
			private IPanel <Panel>k__BackingField;

			// Token: 0x04000745 RID: 1861
			[DebuggerBrowsable(DebuggerBrowsableState.Never)]
			[CompilerGenerated]
			private PointerDeviceState.LocationFlag <Flags>k__BackingField;
		}
	}
}
