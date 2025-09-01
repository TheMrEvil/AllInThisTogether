using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace UnityEngine.UIElements
{
	// Token: 0x0200004A RID: 74
	public struct ManipulatorActivationFilter : IEquatable<ManipulatorActivationFilter>
	{
		// Token: 0x1700004E RID: 78
		// (get) Token: 0x060001C5 RID: 453 RVA: 0x000085D5 File Offset: 0x000067D5
		// (set) Token: 0x060001C6 RID: 454 RVA: 0x000085DD File Offset: 0x000067DD
		public MouseButton button
		{
			[CompilerGenerated]
			readonly get
			{
				return this.<button>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<button>k__BackingField = value;
			}
		}

		// Token: 0x1700004F RID: 79
		// (get) Token: 0x060001C7 RID: 455 RVA: 0x000085E6 File Offset: 0x000067E6
		// (set) Token: 0x060001C8 RID: 456 RVA: 0x000085EE File Offset: 0x000067EE
		public EventModifiers modifiers
		{
			[CompilerGenerated]
			readonly get
			{
				return this.<modifiers>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<modifiers>k__BackingField = value;
			}
		}

		// Token: 0x17000050 RID: 80
		// (get) Token: 0x060001C9 RID: 457 RVA: 0x000085F7 File Offset: 0x000067F7
		// (set) Token: 0x060001CA RID: 458 RVA: 0x000085FF File Offset: 0x000067FF
		public int clickCount
		{
			[CompilerGenerated]
			readonly get
			{
				return this.<clickCount>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<clickCount>k__BackingField = value;
			}
		}

		// Token: 0x060001CB RID: 459 RVA: 0x00008608 File Offset: 0x00006808
		public override bool Equals(object obj)
		{
			return obj is ManipulatorActivationFilter && this.Equals((ManipulatorActivationFilter)obj);
		}

		// Token: 0x060001CC RID: 460 RVA: 0x00008634 File Offset: 0x00006834
		public bool Equals(ManipulatorActivationFilter other)
		{
			return this.button == other.button && this.modifiers == other.modifiers && this.clickCount == other.clickCount;
		}

		// Token: 0x060001CD RID: 461 RVA: 0x00008678 File Offset: 0x00006878
		public override int GetHashCode()
		{
			int num = 390957112;
			num = num * -1521134295 + this.button.GetHashCode();
			num = num * -1521134295 + this.modifiers.GetHashCode();
			return num * -1521134295 + this.clickCount.GetHashCode();
		}

		// Token: 0x060001CE RID: 462 RVA: 0x000086E4 File Offset: 0x000068E4
		public bool Matches(IMouseEvent e)
		{
			bool flag = e == null;
			bool result;
			if (flag)
			{
				result = false;
			}
			else
			{
				bool flag2 = this.clickCount == 0 || e.clickCount >= this.clickCount;
				result = (this.button == (MouseButton)e.button && this.HasModifiers(e) && flag2);
			}
			return result;
		}

		// Token: 0x060001CF RID: 463 RVA: 0x0000873C File Offset: 0x0000693C
		private bool HasModifiers(IMouseEvent e)
		{
			bool flag = e == null;
			return !flag && this.MatchModifiers(e.altKey, e.ctrlKey, e.shiftKey, e.commandKey);
		}

		// Token: 0x060001D0 RID: 464 RVA: 0x00008778 File Offset: 0x00006978
		public bool Matches(IPointerEvent e)
		{
			bool flag = e == null;
			bool result;
			if (flag)
			{
				result = false;
			}
			else
			{
				bool flag2 = this.clickCount == 0 || e.clickCount >= this.clickCount;
				result = (this.button == (MouseButton)e.button && this.HasModifiers(e) && flag2);
			}
			return result;
		}

		// Token: 0x060001D1 RID: 465 RVA: 0x000087D0 File Offset: 0x000069D0
		private bool HasModifiers(IPointerEvent e)
		{
			bool flag = e == null;
			return !flag && this.MatchModifiers(e.altKey, e.ctrlKey, e.shiftKey, e.commandKey);
		}

		// Token: 0x060001D2 RID: 466 RVA: 0x0000880C File Offset: 0x00006A0C
		private bool MatchModifiers(bool alt, bool ctrl, bool shift, bool command)
		{
			bool flag = ((this.modifiers & EventModifiers.Alt) != EventModifiers.None && !alt) || ((this.modifiers & EventModifiers.Alt) == EventModifiers.None && alt);
			bool result;
			if (flag)
			{
				result = false;
			}
			else
			{
				bool flag2 = ((this.modifiers & EventModifiers.Control) != EventModifiers.None && !ctrl) || ((this.modifiers & EventModifiers.Control) == EventModifiers.None && ctrl);
				if (flag2)
				{
					result = false;
				}
				else
				{
					bool flag3 = ((this.modifiers & EventModifiers.Shift) != EventModifiers.None && !shift) || ((this.modifiers & EventModifiers.Shift) == EventModifiers.None && shift);
					result = (!flag3 && ((this.modifiers & EventModifiers.Command) == EventModifiers.None || command) && ((this.modifiers & EventModifiers.Command) != EventModifiers.None || !command));
				}
			}
			return result;
		}

		// Token: 0x060001D3 RID: 467 RVA: 0x000088B8 File Offset: 0x00006AB8
		public static bool operator ==(ManipulatorActivationFilter filter1, ManipulatorActivationFilter filter2)
		{
			return filter1.Equals(filter2);
		}

		// Token: 0x060001D4 RID: 468 RVA: 0x000088D4 File Offset: 0x00006AD4
		public static bool operator !=(ManipulatorActivationFilter filter1, ManipulatorActivationFilter filter2)
		{
			return !(filter1 == filter2);
		}

		// Token: 0x040000D0 RID: 208
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		[CompilerGenerated]
		private MouseButton <button>k__BackingField;

		// Token: 0x040000D1 RID: 209
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private EventModifiers <modifiers>k__BackingField;

		// Token: 0x040000D2 RID: 210
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private int <clickCount>k__BackingField;
	}
}
