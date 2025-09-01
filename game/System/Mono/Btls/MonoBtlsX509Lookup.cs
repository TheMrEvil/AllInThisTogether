using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace Mono.Btls
{
	// Token: 0x020000FB RID: 251
	internal class MonoBtlsX509Lookup : MonoBtlsObject
	{
		// Token: 0x170000FE RID: 254
		// (get) Token: 0x060005BA RID: 1466 RVA: 0x0001067E File Offset: 0x0000E87E
		internal new MonoBtlsX509Lookup.BoringX509LookupHandle Handle
		{
			get
			{
				return (MonoBtlsX509Lookup.BoringX509LookupHandle)base.Handle;
			}
		}

		// Token: 0x060005BB RID: 1467
		[DllImport("libmono-btls-shared")]
		private static extern IntPtr mono_btls_x509_lookup_new(IntPtr store, MonoBtlsX509LookupType type);

		// Token: 0x060005BC RID: 1468
		[DllImport("libmono-btls-shared")]
		private static extern int mono_btls_x509_lookup_load_file(IntPtr handle, IntPtr file, MonoBtlsX509FileType type);

		// Token: 0x060005BD RID: 1469
		[DllImport("libmono-btls-shared")]
		private static extern int mono_btls_x509_lookup_add_dir(IntPtr handle, IntPtr dir, MonoBtlsX509FileType type);

		// Token: 0x060005BE RID: 1470
		[DllImport("libmono-btls-shared")]
		private static extern int mono_btls_x509_lookup_add_mono(IntPtr handle, IntPtr monoLookup);

		// Token: 0x060005BF RID: 1471
		[DllImport("libmono-btls-shared")]
		private static extern int mono_btls_x509_lookup_init(IntPtr handle);

		// Token: 0x060005C0 RID: 1472
		[DllImport("libmono-btls-shared")]
		private static extern int mono_btls_x509_lookup_shutdown(IntPtr handle);

		// Token: 0x060005C1 RID: 1473
		[DllImport("libmono-btls-shared")]
		private static extern IntPtr mono_btls_x509_lookup_by_subject(IntPtr handle, IntPtr name);

		// Token: 0x060005C2 RID: 1474
		[DllImport("libmono-btls-shared")]
		private static extern IntPtr mono_btls_x509_lookup_by_fingerprint(IntPtr handle, IntPtr bytes, int len);

		// Token: 0x060005C3 RID: 1475
		[DllImport("libmono-btls-shared")]
		private static extern void mono_btls_x509_lookup_free(IntPtr handle);

		// Token: 0x060005C4 RID: 1476
		[DllImport("libmono-btls-shared")]
		private static extern IntPtr mono_btls_x509_lookup_peek_lookup(IntPtr handle);

		// Token: 0x060005C5 RID: 1477 RVA: 0x0001068B File Offset: 0x0000E88B
		private static MonoBtlsX509Lookup.BoringX509LookupHandle Create_internal(MonoBtlsX509Store store, MonoBtlsX509LookupType type)
		{
			IntPtr intPtr = MonoBtlsX509Lookup.mono_btls_x509_lookup_new(store.Handle.DangerousGetHandle(), type);
			if (intPtr == IntPtr.Zero)
			{
				throw new MonoBtlsException();
			}
			return new MonoBtlsX509Lookup.BoringX509LookupHandle(intPtr);
		}

		// Token: 0x060005C6 RID: 1478 RVA: 0x000106B6 File Offset: 0x0000E8B6
		internal MonoBtlsX509Lookup(MonoBtlsX509Store store, MonoBtlsX509LookupType type) : base(MonoBtlsX509Lookup.Create_internal(store, type))
		{
			this.store = store;
			this.type = type;
		}

		// Token: 0x060005C7 RID: 1479 RVA: 0x000106D3 File Offset: 0x0000E8D3
		internal IntPtr GetNativeLookup()
		{
			return MonoBtlsX509Lookup.mono_btls_x509_lookup_peek_lookup(this.Handle.DangerousGetHandle());
		}

		// Token: 0x060005C8 RID: 1480 RVA: 0x000106E8 File Offset: 0x0000E8E8
		public void LoadFile(string file, MonoBtlsX509FileType type)
		{
			IntPtr intPtr = IntPtr.Zero;
			try
			{
				if (file != null)
				{
					intPtr = Marshal.StringToHGlobalAnsi(file);
				}
				int ret = MonoBtlsX509Lookup.mono_btls_x509_lookup_load_file(this.Handle.DangerousGetHandle(), intPtr, type);
				base.CheckError(ret, "LoadFile");
			}
			finally
			{
				if (intPtr != IntPtr.Zero)
				{
					Marshal.FreeHGlobal(intPtr);
				}
			}
		}

		// Token: 0x060005C9 RID: 1481 RVA: 0x0001074C File Offset: 0x0000E94C
		public void AddDirectory(string dir, MonoBtlsX509FileType type)
		{
			IntPtr intPtr = IntPtr.Zero;
			try
			{
				if (dir != null)
				{
					intPtr = Marshal.StringToHGlobalAnsi(dir);
				}
				int ret = MonoBtlsX509Lookup.mono_btls_x509_lookup_add_dir(this.Handle.DangerousGetHandle(), intPtr, type);
				base.CheckError(ret, "AddDirectory");
			}
			finally
			{
				if (intPtr != IntPtr.Zero)
				{
					Marshal.FreeHGlobal(intPtr);
				}
			}
		}

		// Token: 0x060005CA RID: 1482 RVA: 0x000107B0 File Offset: 0x0000E9B0
		internal void AddMono(MonoBtlsX509LookupMono monoLookup)
		{
			if (this.type != MonoBtlsX509LookupType.MONO)
			{
				throw new NotSupportedException();
			}
			int ret = MonoBtlsX509Lookup.mono_btls_x509_lookup_add_mono(this.Handle.DangerousGetHandle(), monoLookup.Handle.DangerousGetHandle());
			base.CheckError(ret, "AddMono");
			monoLookup.Install(this);
			if (this.monoLookups == null)
			{
				this.monoLookups = new List<MonoBtlsX509LookupMono>();
			}
			this.monoLookups.Add(monoLookup);
		}

		// Token: 0x060005CB RID: 1483 RVA: 0x0001081C File Offset: 0x0000EA1C
		public void Initialize()
		{
			int ret = MonoBtlsX509Lookup.mono_btls_x509_lookup_init(this.Handle.DangerousGetHandle());
			base.CheckError(ret, "Initialize");
		}

		// Token: 0x060005CC RID: 1484 RVA: 0x00010848 File Offset: 0x0000EA48
		public void Shutdown()
		{
			int ret = MonoBtlsX509Lookup.mono_btls_x509_lookup_shutdown(this.Handle.DangerousGetHandle());
			base.CheckError(ret, "Shutdown");
		}

		// Token: 0x060005CD RID: 1485 RVA: 0x00010874 File Offset: 0x0000EA74
		public MonoBtlsX509 LookupBySubject(MonoBtlsX509Name name)
		{
			IntPtr intPtr = MonoBtlsX509Lookup.mono_btls_x509_lookup_by_subject(this.Handle.DangerousGetHandle(), name.Handle.DangerousGetHandle());
			if (intPtr == IntPtr.Zero)
			{
				return null;
			}
			return new MonoBtlsX509(new MonoBtlsX509.BoringX509Handle(intPtr));
		}

		// Token: 0x060005CE RID: 1486 RVA: 0x000108B8 File Offset: 0x0000EAB8
		public MonoBtlsX509 LookupByFingerPrint(byte[] fingerprint)
		{
			IntPtr intPtr = Marshal.AllocHGlobal(fingerprint.Length);
			MonoBtlsX509 result;
			try
			{
				Marshal.Copy(fingerprint, 0, intPtr, fingerprint.Length);
				IntPtr intPtr2 = MonoBtlsX509Lookup.mono_btls_x509_lookup_by_fingerprint(this.Handle.DangerousGetHandle(), intPtr, fingerprint.Length);
				if (intPtr2 == IntPtr.Zero)
				{
					result = null;
				}
				else
				{
					result = new MonoBtlsX509(new MonoBtlsX509.BoringX509Handle(intPtr2));
				}
			}
			finally
			{
				if (intPtr != IntPtr.Zero)
				{
					Marshal.FreeHGlobal(intPtr);
				}
			}
			return result;
		}

		// Token: 0x060005CF RID: 1487 RVA: 0x00010934 File Offset: 0x0000EB34
		internal void AddCertificate(MonoBtlsX509 certificate)
		{
			this.store.AddCertificate(certificate);
		}

		// Token: 0x060005D0 RID: 1488 RVA: 0x00010944 File Offset: 0x0000EB44
		protected override void Close()
		{
			try
			{
				if (this.monoLookups != null)
				{
					foreach (MonoBtlsX509LookupMono monoBtlsX509LookupMono in this.monoLookups)
					{
						monoBtlsX509LookupMono.Dispose();
					}
					this.monoLookups = null;
				}
			}
			finally
			{
				base.Close();
			}
		}

		// Token: 0x04000420 RID: 1056
		private MonoBtlsX509Store store;

		// Token: 0x04000421 RID: 1057
		private MonoBtlsX509LookupType type;

		// Token: 0x04000422 RID: 1058
		private List<MonoBtlsX509LookupMono> monoLookups;

		// Token: 0x020000FC RID: 252
		internal class BoringX509LookupHandle : MonoBtlsObject.MonoBtlsHandle
		{
			// Token: 0x060005D1 RID: 1489 RVA: 0x0000CD48 File Offset: 0x0000AF48
			public BoringX509LookupHandle(IntPtr handle) : base(handle, true)
			{
			}

			// Token: 0x060005D2 RID: 1490 RVA: 0x000109B8 File Offset: 0x0000EBB8
			protected override bool ReleaseHandle()
			{
				MonoBtlsX509Lookup.mono_btls_x509_lookup_free(this.handle);
				return true;
			}
		}
	}
}
