using System;
using System.Runtime.InteropServices;

namespace Mono.Btls
{
	// Token: 0x020000F3 RID: 243
	internal class MonoBtlsX509Chain : MonoBtlsObject
	{
		// Token: 0x170000F9 RID: 249
		// (get) Token: 0x0600058B RID: 1419 RVA: 0x00010257 File Offset: 0x0000E457
		internal new MonoBtlsX509Chain.BoringX509ChainHandle Handle
		{
			get
			{
				return (MonoBtlsX509Chain.BoringX509ChainHandle)base.Handle;
			}
		}

		// Token: 0x0600058C RID: 1420
		[DllImport("libmono-btls-shared")]
		private static extern IntPtr mono_btls_x509_chain_new();

		// Token: 0x0600058D RID: 1421
		[DllImport("libmono-btls-shared")]
		private static extern int mono_btls_x509_chain_get_count(IntPtr handle);

		// Token: 0x0600058E RID: 1422
		[DllImport("libmono-btls-shared")]
		private static extern IntPtr mono_btls_x509_chain_get_cert(IntPtr Handle, int index);

		// Token: 0x0600058F RID: 1423
		[DllImport("libmono-btls-shared")]
		private static extern int mono_btls_x509_chain_add_cert(IntPtr chain, IntPtr x509);

		// Token: 0x06000590 RID: 1424
		[DllImport("libmono-btls-shared")]
		private static extern IntPtr mono_btls_x509_chain_up_ref(IntPtr handle);

		// Token: 0x06000591 RID: 1425
		[DllImport("libmono-btls-shared")]
		private static extern void mono_btls_x509_chain_free(IntPtr handle);

		// Token: 0x06000592 RID: 1426 RVA: 0x00010264 File Offset: 0x0000E464
		public MonoBtlsX509Chain() : base(new MonoBtlsX509Chain.BoringX509ChainHandle(MonoBtlsX509Chain.mono_btls_x509_chain_new()))
		{
		}

		// Token: 0x06000593 RID: 1427 RVA: 0x0000CA92 File Offset: 0x0000AC92
		internal MonoBtlsX509Chain(MonoBtlsX509Chain.BoringX509ChainHandle handle) : base(handle)
		{
		}

		// Token: 0x170000FA RID: 250
		// (get) Token: 0x06000594 RID: 1428 RVA: 0x00010276 File Offset: 0x0000E476
		public int Count
		{
			get
			{
				return MonoBtlsX509Chain.mono_btls_x509_chain_get_count(this.Handle.DangerousGetHandle());
			}
		}

		// Token: 0x06000595 RID: 1429 RVA: 0x00010288 File Offset: 0x0000E488
		public MonoBtlsX509 GetCertificate(int index)
		{
			if (index >= this.Count)
			{
				throw new IndexOutOfRangeException();
			}
			IntPtr intPtr = MonoBtlsX509Chain.mono_btls_x509_chain_get_cert(this.Handle.DangerousGetHandle(), index);
			base.CheckError(intPtr != IntPtr.Zero, "GetCertificate");
			return new MonoBtlsX509(new MonoBtlsX509.BoringX509Handle(intPtr));
		}

		// Token: 0x06000596 RID: 1430 RVA: 0x000102D8 File Offset: 0x0000E4D8
		public void Dump()
		{
			Console.Error.WriteLine("CHAIN: {0:x} {1}", this.Handle, this.Count);
			for (int i = 0; i < this.Count; i++)
			{
				using (MonoBtlsX509 certificate = this.GetCertificate(i))
				{
					Console.Error.WriteLine("  CERT #{0}: {1}", i, certificate.GetSubjectNameString());
				}
			}
		}

		// Token: 0x06000597 RID: 1431 RVA: 0x00010358 File Offset: 0x0000E558
		public void AddCertificate(MonoBtlsX509 x509)
		{
			MonoBtlsX509Chain.mono_btls_x509_chain_add_cert(this.Handle.DangerousGetHandle(), x509.Handle.DangerousGetHandle());
		}

		// Token: 0x06000598 RID: 1432 RVA: 0x00010378 File Offset: 0x0000E578
		internal MonoBtlsX509Chain Copy()
		{
			IntPtr intPtr = MonoBtlsX509Chain.mono_btls_x509_chain_up_ref(this.Handle.DangerousGetHandle());
			base.CheckError(intPtr != IntPtr.Zero, "Copy");
			return new MonoBtlsX509Chain(new MonoBtlsX509Chain.BoringX509ChainHandle(intPtr));
		}

		// Token: 0x020000F4 RID: 244
		internal class BoringX509ChainHandle : MonoBtlsObject.MonoBtlsHandle
		{
			// Token: 0x06000599 RID: 1433 RVA: 0x0000CD48 File Offset: 0x0000AF48
			public BoringX509ChainHandle(IntPtr handle) : base(handle, true)
			{
			}

			// Token: 0x0600059A RID: 1434 RVA: 0x000103B7 File Offset: 0x0000E5B7
			protected override bool ReleaseHandle()
			{
				MonoBtlsX509Chain.mono_btls_x509_chain_free(this.handle);
				return true;
			}
		}
	}
}
