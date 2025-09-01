using System;
using System.Runtime.CompilerServices;

namespace UnityEngine.Rendering
{
	// Token: 0x0200006A RID: 106
	internal class DebugActionState
	{
		// Token: 0x17000064 RID: 100
		// (get) Token: 0x0600036D RID: 877 RVA: 0x0000FDD5 File Offset: 0x0000DFD5
		// (set) Token: 0x0600036E RID: 878 RVA: 0x0000FDDD File Offset: 0x0000DFDD
		internal bool runningAction
		{
			[CompilerGenerated]
			get
			{
				return this.<runningAction>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<runningAction>k__BackingField = value;
			}
		}

		// Token: 0x17000065 RID: 101
		// (get) Token: 0x0600036F RID: 879 RVA: 0x0000FDE6 File Offset: 0x0000DFE6
		// (set) Token: 0x06000370 RID: 880 RVA: 0x0000FDEE File Offset: 0x0000DFEE
		internal float actionState
		{
			[CompilerGenerated]
			get
			{
				return this.<actionState>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<actionState>k__BackingField = value;
			}
		}

		// Token: 0x06000371 RID: 881 RVA: 0x0000FDF8 File Offset: 0x0000DFF8
		private void Trigger(int triggerCount, float state)
		{
			this.actionState = state;
			this.runningAction = true;
			this.m_Timer = 0f;
			this.m_TriggerPressedUp = new bool[triggerCount];
			for (int i = 0; i < this.m_TriggerPressedUp.Length; i++)
			{
				this.m_TriggerPressedUp[i] = false;
			}
		}

		// Token: 0x06000372 RID: 882 RVA: 0x0000FE46 File Offset: 0x0000E046
		public void TriggerWithButton(string[] buttons, float state)
		{
			this.m_Type = DebugActionState.DebugActionKeyType.Button;
			this.m_PressedButtons = buttons;
			this.m_PressedAxis = "";
			this.Trigger(buttons.Length, state);
		}

		// Token: 0x06000373 RID: 883 RVA: 0x0000FE6B File Offset: 0x0000E06B
		public void TriggerWithAxis(string axis, float state)
		{
			this.m_Type = DebugActionState.DebugActionKeyType.Axis;
			this.m_PressedAxis = axis;
			this.Trigger(1, state);
		}

		// Token: 0x06000374 RID: 884 RVA: 0x0000FE83 File Offset: 0x0000E083
		public void TriggerWithKey(KeyCode[] keys, float state)
		{
			this.m_Type = DebugActionState.DebugActionKeyType.Key;
			this.m_PressedKeys = keys;
			this.m_PressedAxis = "";
			this.Trigger(keys.Length, state);
		}

		// Token: 0x06000375 RID: 885 RVA: 0x0000FEA8 File Offset: 0x0000E0A8
		private void Reset()
		{
			this.runningAction = false;
			this.m_Timer = 0f;
			this.m_TriggerPressedUp = null;
		}

		// Token: 0x06000376 RID: 886 RVA: 0x0000FEC4 File Offset: 0x0000E0C4
		public void Update(DebugActionDesc desc)
		{
			this.actionState = 0f;
			if (this.m_TriggerPressedUp != null)
			{
				this.m_Timer += Time.deltaTime;
				for (int i = 0; i < this.m_TriggerPressedUp.Length; i++)
				{
					if (this.m_Type == DebugActionState.DebugActionKeyType.Button)
					{
						this.m_TriggerPressedUp[i] |= Input.GetButtonUp(this.m_PressedButtons[i]);
					}
					else if (this.m_Type == DebugActionState.DebugActionKeyType.Axis)
					{
						this.m_TriggerPressedUp[i] |= Mathf.Approximately(Input.GetAxis(this.m_PressedAxis), 0f);
					}
					else
					{
						this.m_TriggerPressedUp[i] |= Input.GetKeyUp(this.m_PressedKeys[i]);
					}
				}
				bool flag = true;
				foreach (bool flag2 in this.m_TriggerPressedUp)
				{
					flag = (flag && flag2);
				}
				if (flag || (this.m_Timer > desc.repeatDelay && desc.repeatMode == DebugActionRepeatMode.Delay))
				{
					this.Reset();
				}
			}
		}

		// Token: 0x06000377 RID: 887 RVA: 0x0000FFC4 File Offset: 0x0000E1C4
		public DebugActionState()
		{
		}

		// Token: 0x0400023B RID: 571
		private DebugActionState.DebugActionKeyType m_Type;

		// Token: 0x0400023C RID: 572
		private string[] m_PressedButtons;

		// Token: 0x0400023D RID: 573
		private string m_PressedAxis = "";

		// Token: 0x0400023E RID: 574
		private KeyCode[] m_PressedKeys;

		// Token: 0x0400023F RID: 575
		private bool[] m_TriggerPressedUp;

		// Token: 0x04000240 RID: 576
		private float m_Timer;

		// Token: 0x04000241 RID: 577
		[CompilerGenerated]
		private bool <runningAction>k__BackingField;

		// Token: 0x04000242 RID: 578
		[CompilerGenerated]
		private float <actionState>k__BackingField;

		// Token: 0x02000147 RID: 327
		private enum DebugActionKeyType
		{
			// Token: 0x04000516 RID: 1302
			Button,
			// Token: 0x04000517 RID: 1303
			Axis,
			// Token: 0x04000518 RID: 1304
			Key
		}
	}
}
