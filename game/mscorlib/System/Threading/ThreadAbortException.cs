using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security;

namespace System.Threading
{
	/// <summary>The exception that is thrown when a call is made to the <see cref="M:System.Threading.Thread.Abort(System.Object)" /> method. This class cannot be inherited.</summary>
	// Token: 0x020002D7 RID: 727
	[ComVisible(true)]
	[Serializable]
	public sealed class ThreadAbortException : SystemException
	{
		// Token: 0x06001FF5 RID: 8181 RVA: 0x0007497A File Offset: 0x00072B7A
		private ThreadAbortException() : base(Exception.GetMessageFromNativeResources(Exception.ExceptionMessageKind.ThreadAbort))
		{
			base.SetErrorCode(-2146233040);
		}

		// Token: 0x06001FF6 RID: 8182 RVA: 0x00020A65 File Offset: 0x0001EC65
		internal ThreadAbortException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		/// <summary>Gets an object that contains application-specific information related to the thread abort.</summary>
		/// <returns>An object containing application-specific information.</returns>
		// Token: 0x170003CE RID: 974
		// (get) Token: 0x06001FF7 RID: 8183 RVA: 0x00074993 File Offset: 0x00072B93
		public object ExceptionState
		{
			[SecuritySafeCritical]
			get
			{
				return Thread.CurrentThread.AbortReason;
			}
		}
	}
}
