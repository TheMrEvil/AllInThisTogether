using System;

namespace System.Runtime.CompilerServices
{
	// Token: 0x020007DA RID: 2010
	[AttributeUsage(AttributeTargets.Method, Inherited = false, AllowMultiple = false)]
	public sealed class AsyncIteratorStateMachineAttribute : StateMachineAttribute
	{
		// Token: 0x060045C0 RID: 17856 RVA: 0x000E51B6 File Offset: 0x000E33B6
		public AsyncIteratorStateMachineAttribute(Type stateMachineType) : base(stateMachineType)
		{
		}
	}
}
