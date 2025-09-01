using System;

namespace UnityEngine.UIElements
{
	// Token: 0x0200004C RID: 76
	public abstract class Manipulator : IManipulator
	{
		// Token: 0x060001D7 RID: 471
		protected abstract void RegisterCallbacksOnTarget();

		// Token: 0x060001D8 RID: 472
		protected abstract void UnregisterCallbacksFromTarget();

		// Token: 0x17000052 RID: 82
		// (get) Token: 0x060001D9 RID: 473 RVA: 0x000088F0 File Offset: 0x00006AF0
		// (set) Token: 0x060001DA RID: 474 RVA: 0x00008908 File Offset: 0x00006B08
		public VisualElement target
		{
			get
			{
				return this.m_Target;
			}
			set
			{
				bool flag = this.target != null;
				if (flag)
				{
					this.UnregisterCallbacksFromTarget();
				}
				this.m_Target = value;
				bool flag2 = this.target != null;
				if (flag2)
				{
					this.RegisterCallbacksOnTarget();
				}
			}
		}

		// Token: 0x060001DB RID: 475 RVA: 0x000020C2 File Offset: 0x000002C2
		protected Manipulator()
		{
		}

		// Token: 0x040000D3 RID: 211
		private VisualElement m_Target;
	}
}
