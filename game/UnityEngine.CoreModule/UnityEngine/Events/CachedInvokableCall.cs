using System;
using System.Reflection;

namespace UnityEngine.Events
{
	// Token: 0x020002BE RID: 702
	internal class CachedInvokableCall<T> : InvokableCall<T>
	{
		// Token: 0x06001D57 RID: 7511 RVA: 0x0002F3C8 File Offset: 0x0002D5C8
		public CachedInvokableCall(Object target, MethodInfo theFunction, T argument) : base(target, theFunction)
		{
			this.m_Arg1 = argument;
		}

		// Token: 0x06001D58 RID: 7512 RVA: 0x0002F3DB File Offset: 0x0002D5DB
		public override void Invoke(object[] args)
		{
			base.Invoke(this.m_Arg1);
		}

		// Token: 0x06001D59 RID: 7513 RVA: 0x0002F3DB File Offset: 0x0002D5DB
		public override void Invoke(T arg0)
		{
			base.Invoke(this.m_Arg1);
		}

		// Token: 0x0400099D RID: 2461
		private readonly T m_Arg1;
	}
}
