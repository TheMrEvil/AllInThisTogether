using System;

namespace System.Net
{
	// Token: 0x020005E1 RID: 1505
	internal static class ExceptionHelper
	{
		// Token: 0x170009A8 RID: 2472
		// (get) Token: 0x0600305A RID: 12378 RVA: 0x000A6AB0 File Offset: 0x000A4CB0
		internal static NotImplementedException MethodNotImplementedException
		{
			get
			{
				return new NotImplementedException(SR.GetString("This method is not implemented by this class."));
			}
		}

		// Token: 0x170009A9 RID: 2473
		// (get) Token: 0x0600305B RID: 12379 RVA: 0x000A6AC1 File Offset: 0x000A4CC1
		internal static NotImplementedException PropertyNotImplementedException
		{
			get
			{
				return new NotImplementedException(SR.GetString("This property is not implemented by this class."));
			}
		}

		// Token: 0x170009AA RID: 2474
		// (get) Token: 0x0600305C RID: 12380 RVA: 0x000A6AD2 File Offset: 0x000A4CD2
		internal static WebException TimeoutException
		{
			get
			{
				return new WebException("The operation has timed out.");
			}
		}

		// Token: 0x170009AB RID: 2475
		// (get) Token: 0x0600305D RID: 12381 RVA: 0x000A6ADE File Offset: 0x000A4CDE
		internal static NotSupportedException MethodNotSupportedException
		{
			get
			{
				return new NotSupportedException(SR.GetString("This method is not supported by this class."));
			}
		}

		// Token: 0x170009AC RID: 2476
		// (get) Token: 0x0600305E RID: 12382 RVA: 0x000A6AEF File Offset: 0x000A4CEF
		internal static NotSupportedException PropertyNotSupportedException
		{
			get
			{
				return new NotSupportedException(SR.GetString("This property is not supported by this class."));
			}
		}

		// Token: 0x170009AD RID: 2477
		// (get) Token: 0x0600305F RID: 12383 RVA: 0x000A6B00 File Offset: 0x000A4D00
		internal static WebException IsolatedException
		{
			get
			{
				return new WebException(NetRes.GetWebStatusString("net_requestaborted", WebExceptionStatus.KeepAliveFailure), WebExceptionStatus.KeepAliveFailure, WebExceptionInternalStatus.Isolated, null);
			}
		}

		// Token: 0x170009AE RID: 2478
		// (get) Token: 0x06003060 RID: 12384 RVA: 0x000A6B17 File Offset: 0x000A4D17
		internal static WebException RequestAbortedException
		{
			get
			{
				return new WebException(NetRes.GetWebStatusString("net_requestaborted", WebExceptionStatus.RequestCanceled), WebExceptionStatus.RequestCanceled);
			}
		}

		// Token: 0x170009AF RID: 2479
		// (get) Token: 0x06003061 RID: 12385 RVA: 0x000A6B2A File Offset: 0x000A4D2A
		internal static WebException CacheEntryNotFoundException
		{
			get
			{
				return new WebException(NetRes.GetWebStatusString("net_requestaborted", WebExceptionStatus.CacheEntryNotFound), WebExceptionStatus.CacheEntryNotFound);
			}
		}

		// Token: 0x170009B0 RID: 2480
		// (get) Token: 0x06003062 RID: 12386 RVA: 0x000A6B3F File Offset: 0x000A4D3F
		internal static WebException RequestProhibitedByCachePolicyException
		{
			get
			{
				return new WebException(NetRes.GetWebStatusString("net_requestaborted", WebExceptionStatus.RequestProhibitedByCachePolicy), WebExceptionStatus.RequestProhibitedByCachePolicy);
			}
		}
	}
}
