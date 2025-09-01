using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace UnityEngine.UIElements
{
	// Token: 0x0200004F RID: 79
	public abstract class MouseManipulator : Manipulator
	{
		// Token: 0x17000053 RID: 83
		// (get) Token: 0x060001E1 RID: 481 RVA: 0x00008A49 File Offset: 0x00006C49
		// (set) Token: 0x060001E2 RID: 482 RVA: 0x00008A51 File Offset: 0x00006C51
		public List<ManipulatorActivationFilter> activators
		{
			[CompilerGenerated]
			get
			{
				return this.<activators>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<activators>k__BackingField = value;
			}
		}

		// Token: 0x060001E3 RID: 483 RVA: 0x00008A5A File Offset: 0x00006C5A
		protected MouseManipulator()
		{
			this.activators = new List<ManipulatorActivationFilter>();
		}

		// Token: 0x060001E4 RID: 484 RVA: 0x00008A70 File Offset: 0x00006C70
		protected bool CanStartManipulation(IMouseEvent e)
		{
			foreach (ManipulatorActivationFilter currentActivator in this.activators)
			{
				bool flag = currentActivator.Matches(e);
				if (flag)
				{
					this.m_currentActivator = currentActivator;
					return true;
				}
			}
			return false;
		}

		// Token: 0x060001E5 RID: 485 RVA: 0x00008AE0 File Offset: 0x00006CE0
		protected bool CanStopManipulation(IMouseEvent e)
		{
			bool flag = e == null;
			return !flag && e.button == (int)this.m_currentActivator.button;
		}

		// Token: 0x040000DA RID: 218
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		[CompilerGenerated]
		private List<ManipulatorActivationFilter> <activators>k__BackingField;

		// Token: 0x040000DB RID: 219
		private ManipulatorActivationFilter m_currentActivator;
	}
}
