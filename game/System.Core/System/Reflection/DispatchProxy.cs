using System;

namespace System.Reflection
{
	// Token: 0x02000065 RID: 101
	public abstract class DispatchProxy
	{
		// Token: 0x06000227 RID: 551 RVA: 0x00002162 File Offset: 0x00000362
		protected DispatchProxy()
		{
		}

		// Token: 0x06000228 RID: 552
		protected abstract object Invoke(MethodInfo targetMethod, object[] args);

		// Token: 0x06000229 RID: 553 RVA: 0x00006268 File Offset: 0x00004468
		public static T Create<T, TProxy>() where TProxy : DispatchProxy
		{
			return (T)((object)DispatchProxyGenerator.CreateProxyInstance(typeof(TProxy), typeof(T)));
		}
	}
}
