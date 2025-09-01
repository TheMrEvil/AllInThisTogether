using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace UnityEngine.Playables
{
	// Token: 0x02000439 RID: 1081
	public class Notification : INotification
	{
		// Token: 0x0600258D RID: 9613 RVA: 0x0003F681 File Offset: 0x0003D881
		public Notification(string name)
		{
			this.id = new PropertyName(name);
		}

		// Token: 0x17000724 RID: 1828
		// (get) Token: 0x0600258E RID: 9614 RVA: 0x0003F697 File Offset: 0x0003D897
		public PropertyName id
		{
			[CompilerGenerated]
			get
			{
				return this.<id>k__BackingField;
			}
		}

		// Token: 0x04000E0B RID: 3595
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		[CompilerGenerated]
		private readonly PropertyName <id>k__BackingField;
	}
}
