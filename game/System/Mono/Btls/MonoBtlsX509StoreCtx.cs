using System;
using System.Runtime.InteropServices;

namespace Mono.Btls
{
	// Token: 0x0200010A RID: 266
	internal class MonoBtlsX509StoreCtx : MonoBtlsObject
	{
		// Token: 0x17000103 RID: 259
		// (get) Token: 0x0600062B RID: 1579 RVA: 0x000113C2 File Offset: 0x0000F5C2
		internal new MonoBtlsX509StoreCtx.BoringX509StoreCtxHandle Handle
		{
			get
			{
				return (MonoBtlsX509StoreCtx.BoringX509StoreCtxHandle)base.Handle;
			}
		}

		// Token: 0x0600062C RID: 1580
		[DllImport("libmono-btls-shared")]
		private static extern IntPtr mono_btls_x509_store_ctx_new();

		// Token: 0x0600062D RID: 1581
		[DllImport("libmono-btls-shared")]
		private static extern IntPtr mono_btls_x509_store_ctx_from_ptr(IntPtr ctx);

		// Token: 0x0600062E RID: 1582
		[DllImport("libmono-btls-shared")]
		private static extern MonoBtlsX509Error mono_btls_x509_store_ctx_get_error(IntPtr handle, out IntPtr error_string);

		// Token: 0x0600062F RID: 1583
		[DllImport("libmono-btls-shared")]
		private static extern int mono_btls_x509_store_ctx_get_error_depth(IntPtr handle);

		// Token: 0x06000630 RID: 1584
		[DllImport("libmono-btls-shared")]
		private static extern IntPtr mono_btls_x509_store_ctx_get_chain(IntPtr handle);

		// Token: 0x06000631 RID: 1585
		[DllImport("libmono-btls-shared")]
		private static extern int mono_btls_x509_store_ctx_init(IntPtr handle, IntPtr store, IntPtr chain);

		// Token: 0x06000632 RID: 1586
		[DllImport("libmono-btls-shared")]
		private static extern int mono_btls_x509_store_ctx_set_param(IntPtr handle, IntPtr param);

		// Token: 0x06000633 RID: 1587
		[DllImport("libmono-btls-shared")]
		private static extern int mono_btls_x509_store_ctx_verify_cert(IntPtr handle);

		// Token: 0x06000634 RID: 1588
		[DllImport("libmono-btls-shared")]
		private static extern IntPtr mono_btls_x509_store_ctx_get_by_subject(IntPtr handle, IntPtr name);

		// Token: 0x06000635 RID: 1589
		[DllImport("libmono-btls-shared")]
		private static extern IntPtr mono_btls_x509_store_ctx_get_current_cert(IntPtr handle);

		// Token: 0x06000636 RID: 1590
		[DllImport("libmono-btls-shared")]
		private static extern IntPtr mono_btls_x509_store_ctx_get_current_issuer(IntPtr handle);

		// Token: 0x06000637 RID: 1591
		[DllImport("libmono-btls-shared")]
		private static extern IntPtr mono_btls_x509_store_ctx_get_verify_param(IntPtr handle);

		// Token: 0x06000638 RID: 1592
		[DllImport("libmono-btls-shared")]
		private static extern IntPtr mono_btls_x509_store_ctx_get_untrusted(IntPtr handle);

		// Token: 0x06000639 RID: 1593
		[DllImport("libmono-btls-shared")]
		private static extern IntPtr mono_btls_x509_store_ctx_up_ref(IntPtr handle);

		// Token: 0x0600063A RID: 1594
		[DllImport("libmono-btls-shared")]
		private static extern void mono_btls_x509_store_ctx_free(IntPtr handle);

		// Token: 0x0600063B RID: 1595 RVA: 0x000113CF File Offset: 0x0000F5CF
		internal MonoBtlsX509StoreCtx() : base(new MonoBtlsX509StoreCtx.BoringX509StoreCtxHandle(MonoBtlsX509StoreCtx.mono_btls_x509_store_ctx_new(), true))
		{
		}

		// Token: 0x0600063C RID: 1596 RVA: 0x000113E2 File Offset: 0x0000F5E2
		private static MonoBtlsX509StoreCtx.BoringX509StoreCtxHandle Create_internal(IntPtr store_ctx)
		{
			IntPtr intPtr = MonoBtlsX509StoreCtx.mono_btls_x509_store_ctx_from_ptr(store_ctx);
			if (intPtr == IntPtr.Zero)
			{
				throw new MonoBtlsException();
			}
			return new MonoBtlsX509StoreCtx.BoringX509StoreCtxHandle(intPtr, true);
		}

		// Token: 0x0600063D RID: 1597 RVA: 0x00011403 File Offset: 0x0000F603
		internal MonoBtlsX509StoreCtx(int preverify_ok, IntPtr store_ctx) : base(MonoBtlsX509StoreCtx.Create_internal(store_ctx))
		{
			this.verifyResult = new int?(preverify_ok);
		}

		// Token: 0x0600063E RID: 1598 RVA: 0x0001141D File Offset: 0x0000F61D
		internal MonoBtlsX509StoreCtx(MonoBtlsX509StoreCtx.BoringX509StoreCtxHandle ptr, int? verifyResult) : base(ptr)
		{
			this.verifyResult = verifyResult;
		}

		// Token: 0x0600063F RID: 1599 RVA: 0x00011430 File Offset: 0x0000F630
		public MonoBtlsX509Error GetError()
		{
			IntPtr intPtr;
			return MonoBtlsX509StoreCtx.mono_btls_x509_store_ctx_get_error(this.Handle.DangerousGetHandle(), out intPtr);
		}

		// Token: 0x06000640 RID: 1600 RVA: 0x0001144F File Offset: 0x0000F64F
		public int GetErrorDepth()
		{
			return MonoBtlsX509StoreCtx.mono_btls_x509_store_ctx_get_error_depth(this.Handle.DangerousGetHandle());
		}

		// Token: 0x06000641 RID: 1601 RVA: 0x00011464 File Offset: 0x0000F664
		public MonoBtlsX509Exception GetException()
		{
			IntPtr intPtr;
			MonoBtlsX509Error monoBtlsX509Error = MonoBtlsX509StoreCtx.mono_btls_x509_store_ctx_get_error(this.Handle.DangerousGetHandle(), out intPtr);
			if (monoBtlsX509Error == MonoBtlsX509Error.OK)
			{
				return null;
			}
			if (intPtr != IntPtr.Zero)
			{
				string message = Marshal.PtrToStringAnsi(intPtr);
				return new MonoBtlsX509Exception(monoBtlsX509Error, message);
			}
			return new MonoBtlsX509Exception(monoBtlsX509Error, "Unknown verify error.");
		}

		// Token: 0x06000642 RID: 1602 RVA: 0x000114B0 File Offset: 0x0000F6B0
		public MonoBtlsX509Chain GetChain()
		{
			IntPtr intPtr = MonoBtlsX509StoreCtx.mono_btls_x509_store_ctx_get_chain(this.Handle.DangerousGetHandle());
			base.CheckError(intPtr != IntPtr.Zero, "GetChain");
			return new MonoBtlsX509Chain(new MonoBtlsX509Chain.BoringX509ChainHandle(intPtr));
		}

		// Token: 0x06000643 RID: 1603 RVA: 0x000114F0 File Offset: 0x0000F6F0
		public MonoBtlsX509Chain GetUntrusted()
		{
			IntPtr intPtr = MonoBtlsX509StoreCtx.mono_btls_x509_store_ctx_get_untrusted(this.Handle.DangerousGetHandle());
			base.CheckError(intPtr != IntPtr.Zero, "GetUntrusted");
			return new MonoBtlsX509Chain(new MonoBtlsX509Chain.BoringX509ChainHandle(intPtr));
		}

		// Token: 0x06000644 RID: 1604 RVA: 0x00011530 File Offset: 0x0000F730
		public void Initialize(MonoBtlsX509Store store, MonoBtlsX509Chain chain)
		{
			int ret = MonoBtlsX509StoreCtx.mono_btls_x509_store_ctx_init(this.Handle.DangerousGetHandle(), store.Handle.DangerousGetHandle(), chain.Handle.DangerousGetHandle());
			base.CheckError(ret, "Initialize");
		}

		// Token: 0x06000645 RID: 1605 RVA: 0x00011570 File Offset: 0x0000F770
		public void SetVerifyParam(MonoBtlsX509VerifyParam param)
		{
			int ret = MonoBtlsX509StoreCtx.mono_btls_x509_store_ctx_set_param(this.Handle.DangerousGetHandle(), param.Handle.DangerousGetHandle());
			base.CheckError(ret, "SetVerifyParam");
		}

		// Token: 0x17000104 RID: 260
		// (get) Token: 0x06000646 RID: 1606 RVA: 0x000115A5 File Offset: 0x0000F7A5
		public int VerifyResult
		{
			get
			{
				if (this.verifyResult == null)
				{
					throw new InvalidOperationException();
				}
				return this.verifyResult.Value;
			}
		}

		// Token: 0x06000647 RID: 1607 RVA: 0x000115C5 File Offset: 0x0000F7C5
		public int Verify()
		{
			this.verifyResult = new int?(MonoBtlsX509StoreCtx.mono_btls_x509_store_ctx_verify_cert(this.Handle.DangerousGetHandle()));
			return this.verifyResult.Value;
		}

		// Token: 0x06000648 RID: 1608 RVA: 0x000115F0 File Offset: 0x0000F7F0
		public MonoBtlsX509 LookupBySubject(MonoBtlsX509Name name)
		{
			IntPtr intPtr = MonoBtlsX509StoreCtx.mono_btls_x509_store_ctx_get_by_subject(this.Handle.DangerousGetHandle(), name.Handle.DangerousGetHandle());
			if (intPtr == IntPtr.Zero)
			{
				return null;
			}
			return new MonoBtlsX509(new MonoBtlsX509.BoringX509Handle(intPtr));
		}

		// Token: 0x06000649 RID: 1609 RVA: 0x00011634 File Offset: 0x0000F834
		public MonoBtlsX509 GetCurrentCertificate()
		{
			IntPtr intPtr = MonoBtlsX509StoreCtx.mono_btls_x509_store_ctx_get_current_cert(this.Handle.DangerousGetHandle());
			if (intPtr == IntPtr.Zero)
			{
				return null;
			}
			return new MonoBtlsX509(new MonoBtlsX509.BoringX509Handle(intPtr));
		}

		// Token: 0x0600064A RID: 1610 RVA: 0x0001166C File Offset: 0x0000F86C
		public MonoBtlsX509 GetCurrentIssuer()
		{
			IntPtr intPtr = MonoBtlsX509StoreCtx.mono_btls_x509_store_ctx_get_current_issuer(this.Handle.DangerousGetHandle());
			if (intPtr == IntPtr.Zero)
			{
				return null;
			}
			return new MonoBtlsX509(new MonoBtlsX509.BoringX509Handle(intPtr));
		}

		// Token: 0x0600064B RID: 1611 RVA: 0x000116A4 File Offset: 0x0000F8A4
		public MonoBtlsX509VerifyParam GetVerifyParam()
		{
			IntPtr intPtr = MonoBtlsX509StoreCtx.mono_btls_x509_store_ctx_get_verify_param(this.Handle.DangerousGetHandle());
			if (intPtr == IntPtr.Zero)
			{
				return null;
			}
			return new MonoBtlsX509VerifyParam(new MonoBtlsX509VerifyParam.BoringX509VerifyParamHandle(intPtr));
		}

		// Token: 0x0600064C RID: 1612 RVA: 0x000116DC File Offset: 0x0000F8DC
		public MonoBtlsX509StoreCtx Copy()
		{
			IntPtr intPtr = MonoBtlsX509StoreCtx.mono_btls_x509_store_ctx_up_ref(this.Handle.DangerousGetHandle());
			base.CheckError(intPtr != IntPtr.Zero, "Copy");
			return new MonoBtlsX509StoreCtx(new MonoBtlsX509StoreCtx.BoringX509StoreCtxHandle(intPtr, true), this.verifyResult);
		}

		// Token: 0x0400044F RID: 1103
		private int? verifyResult;

		// Token: 0x0200010B RID: 267
		internal class BoringX509StoreCtxHandle : MonoBtlsObject.MonoBtlsHandle
		{
			// Token: 0x0600064D RID: 1613 RVA: 0x00011722 File Offset: 0x0000F922
			internal BoringX509StoreCtxHandle(IntPtr handle, bool ownsHandle = true) : base(handle, ownsHandle)
			{
				this.dontFree = !ownsHandle;
			}

			// Token: 0x0600064E RID: 1614 RVA: 0x00011736 File Offset: 0x0000F936
			protected override bool ReleaseHandle()
			{
				if (!this.dontFree)
				{
					MonoBtlsX509StoreCtx.mono_btls_x509_store_ctx_free(this.handle);
				}
				return true;
			}

			// Token: 0x04000450 RID: 1104
			private bool dontFree;
		}
	}
}
