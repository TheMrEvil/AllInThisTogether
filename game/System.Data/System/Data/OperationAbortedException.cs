using System;
using System.Runtime.Serialization;
using Unity;

namespace System.Data
{
	/// <summary>This exception is thrown when an ongoing operation is aborted by the user.</summary>
	// Token: 0x02000152 RID: 338
	[Serializable]
	public sealed class OperationAbortedException : SystemException
	{
		// Token: 0x06001208 RID: 4616 RVA: 0x0005530C File Offset: 0x0005350C
		private OperationAbortedException(string message, Exception innerException) : base(message, innerException)
		{
			base.HResult = -2146232010;
		}

		// Token: 0x06001209 RID: 4617 RVA: 0x00010C10 File Offset: 0x0000EE10
		private OperationAbortedException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600120A RID: 4618 RVA: 0x00055324 File Offset: 0x00053524
		internal static OperationAbortedException Aborted(Exception inner)
		{
			OperationAbortedException result;
			if (inner == null)
			{
				result = new OperationAbortedException(SR.GetString("Operation aborted."), null);
			}
			else
			{
				result = new OperationAbortedException(SR.GetString("Operation aborted due to an exception (see InnerException for details)."), inner);
			}
			return result;
		}

		// Token: 0x0600120B RID: 4619 RVA: 0x000108A6 File Offset: 0x0000EAA6
		internal OperationAbortedException()
		{
			ThrowStub.ThrowNotSupportedException();
		}
	}
}
