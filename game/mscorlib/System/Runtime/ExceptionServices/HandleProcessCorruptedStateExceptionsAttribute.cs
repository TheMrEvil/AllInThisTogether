using System;

namespace System.Runtime.ExceptionServices
{
	/// <summary>Enables managed code to handle exceptions that indicate a corrupted process state.</summary>
	// Token: 0x020007D1 RID: 2001
	[AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = false)]
	public sealed class HandleProcessCorruptedStateExceptionsAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.ExceptionServices.HandleProcessCorruptedStateExceptionsAttribute" /> class.</summary>
		// Token: 0x060045AA RID: 17834 RVA: 0x00002050 File Offset: 0x00000250
		public HandleProcessCorruptedStateExceptionsAttribute()
		{
		}
	}
}
