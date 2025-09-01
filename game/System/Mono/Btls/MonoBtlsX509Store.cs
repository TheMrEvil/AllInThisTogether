using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;
using Mono.Security.Interface;

namespace Mono.Btls
{
	// Token: 0x02000108 RID: 264
	internal class MonoBtlsX509Store : MonoBtlsObject
	{
		// Token: 0x17000102 RID: 258
		// (get) Token: 0x06000610 RID: 1552 RVA: 0x000110EC File Offset: 0x0000F2EC
		internal new MonoBtlsX509Store.BoringX509StoreHandle Handle
		{
			get
			{
				return (MonoBtlsX509Store.BoringX509StoreHandle)base.Handle;
			}
		}

		// Token: 0x06000611 RID: 1553
		[DllImport("libmono-btls-shared")]
		private static extern IntPtr mono_btls_x509_store_new();

		// Token: 0x06000612 RID: 1554
		[DllImport("libmono-btls-shared")]
		private static extern IntPtr mono_btls_x509_store_from_ctx(IntPtr ctx);

		// Token: 0x06000613 RID: 1555
		[DllImport("libmono-btls-shared")]
		private static extern IntPtr mono_btls_x509_store_from_ssl_ctx(IntPtr handle);

		// Token: 0x06000614 RID: 1556
		[DllImport("libmono-btls-shared")]
		private static extern int mono_btls_x509_store_load_locations(IntPtr handle, IntPtr file, IntPtr path);

		// Token: 0x06000615 RID: 1557
		[DllImport("libmono-btls-shared")]
		private static extern int mono_btls_x509_store_set_default_paths(IntPtr handle);

		// Token: 0x06000616 RID: 1558
		[DllImport("libmono-btls-shared")]
		private static extern int mono_btls_x509_store_add_cert(IntPtr handle, IntPtr x509);

		// Token: 0x06000617 RID: 1559
		[DllImport("libmono-btls-shared")]
		private static extern int mono_btls_x509_store_get_count(IntPtr handle);

		// Token: 0x06000618 RID: 1560
		[DllImport("libmono-btls-shared")]
		private static extern void mono_btls_x509_store_free(IntPtr handle);

		// Token: 0x06000619 RID: 1561 RVA: 0x000110FC File Offset: 0x0000F2FC
		public void LoadLocations(string file, string path)
		{
			IntPtr intPtr = IntPtr.Zero;
			IntPtr intPtr2 = IntPtr.Zero;
			try
			{
				if (file != null)
				{
					intPtr = Marshal.StringToHGlobalAnsi(file);
				}
				if (path != null)
				{
					intPtr2 = Marshal.StringToHGlobalAnsi(path);
				}
				int ret = MonoBtlsX509Store.mono_btls_x509_store_load_locations(this.Handle.DangerousGetHandle(), intPtr, intPtr2);
				base.CheckError(ret, "LoadLocations");
			}
			finally
			{
				if (intPtr != IntPtr.Zero)
				{
					Marshal.FreeHGlobal(intPtr);
				}
				if (intPtr2 != IntPtr.Zero)
				{
					Marshal.FreeHGlobal(intPtr2);
				}
			}
		}

		// Token: 0x0600061A RID: 1562 RVA: 0x00011184 File Offset: 0x0000F384
		public void SetDefaultPaths()
		{
			int ret = MonoBtlsX509Store.mono_btls_x509_store_set_default_paths(this.Handle.DangerousGetHandle());
			base.CheckError(ret, "SetDefaultPaths");
		}

		// Token: 0x0600061B RID: 1563 RVA: 0x000111AE File Offset: 0x0000F3AE
		private static MonoBtlsX509Store.BoringX509StoreHandle Create_internal()
		{
			IntPtr intPtr = MonoBtlsX509Store.mono_btls_x509_store_new();
			if (intPtr == IntPtr.Zero)
			{
				throw new MonoBtlsException();
			}
			return new MonoBtlsX509Store.BoringX509StoreHandle(intPtr);
		}

		// Token: 0x0600061C RID: 1564 RVA: 0x000111CD File Offset: 0x0000F3CD
		private static MonoBtlsX509Store.BoringX509StoreHandle Create_internal(IntPtr store_ctx)
		{
			IntPtr intPtr = MonoBtlsX509Store.mono_btls_x509_store_from_ssl_ctx(store_ctx);
			if (intPtr == IntPtr.Zero)
			{
				throw new MonoBtlsException();
			}
			return new MonoBtlsX509Store.BoringX509StoreHandle(intPtr);
		}

		// Token: 0x0600061D RID: 1565 RVA: 0x000111ED File Offset: 0x0000F3ED
		private static MonoBtlsX509Store.BoringX509StoreHandle Create_internal(MonoBtlsSslCtx.BoringSslCtxHandle ctx)
		{
			IntPtr intPtr = MonoBtlsX509Store.mono_btls_x509_store_from_ssl_ctx(ctx.DangerousGetHandle());
			if (intPtr == IntPtr.Zero)
			{
				throw new MonoBtlsException();
			}
			return new MonoBtlsX509Store.BoringX509StoreHandle(intPtr);
		}

		// Token: 0x0600061E RID: 1566 RVA: 0x00011212 File Offset: 0x0000F412
		internal MonoBtlsX509Store() : base(MonoBtlsX509Store.Create_internal())
		{
		}

		// Token: 0x0600061F RID: 1567 RVA: 0x0001121F File Offset: 0x0000F41F
		internal MonoBtlsX509Store(IntPtr store_ctx) : base(MonoBtlsX509Store.Create_internal(store_ctx))
		{
		}

		// Token: 0x06000620 RID: 1568 RVA: 0x0001122D File Offset: 0x0000F42D
		internal MonoBtlsX509Store(MonoBtlsSslCtx.BoringSslCtxHandle ctx) : base(MonoBtlsX509Store.Create_internal(ctx))
		{
		}

		// Token: 0x06000621 RID: 1569 RVA: 0x0001123C File Offset: 0x0000F43C
		public void AddCertificate(MonoBtlsX509 x509)
		{
			int ret = MonoBtlsX509Store.mono_btls_x509_store_add_cert(this.Handle.DangerousGetHandle(), x509.Handle.DangerousGetHandle());
			base.CheckError(ret, "AddCertificate");
		}

		// Token: 0x06000622 RID: 1570 RVA: 0x00011271 File Offset: 0x0000F471
		public int GetCount()
		{
			return MonoBtlsX509Store.mono_btls_x509_store_get_count(this.Handle.DangerousGetHandle());
		}

		// Token: 0x06000623 RID: 1571 RVA: 0x00011283 File Offset: 0x0000F483
		internal void AddTrustedRoots()
		{
			MonoBtlsProvider.SetupCertificateStore(this, MonoTlsSettings.DefaultSettings, false);
		}

		// Token: 0x06000624 RID: 1572 RVA: 0x00011294 File Offset: 0x0000F494
		public MonoBtlsX509Lookup AddLookup(MonoBtlsX509LookupType type)
		{
			if (this.lookupHash == null)
			{
				this.lookupHash = new Dictionary<IntPtr, MonoBtlsX509Lookup>();
			}
			MonoBtlsX509Lookup monoBtlsX509Lookup = new MonoBtlsX509Lookup(this, type);
			IntPtr nativeLookup = monoBtlsX509Lookup.GetNativeLookup();
			if (this.lookupHash.ContainsKey(nativeLookup))
			{
				monoBtlsX509Lookup.Dispose();
				monoBtlsX509Lookup = this.lookupHash[nativeLookup];
			}
			else
			{
				this.lookupHash.Add(nativeLookup, monoBtlsX509Lookup);
			}
			return monoBtlsX509Lookup;
		}

		// Token: 0x06000625 RID: 1573 RVA: 0x000112F4 File Offset: 0x0000F4F4
		public void AddDirectoryLookup(string dir, MonoBtlsX509FileType type)
		{
			this.AddLookup(MonoBtlsX509LookupType.HASH_DIR).AddDirectory(dir, type);
		}

		// Token: 0x06000626 RID: 1574 RVA: 0x00011304 File Offset: 0x0000F504
		public void AddFileLookup(string file, MonoBtlsX509FileType type)
		{
			this.AddLookup(MonoBtlsX509LookupType.FILE).LoadFile(file, type);
		}

		// Token: 0x06000627 RID: 1575 RVA: 0x00011314 File Offset: 0x0000F514
		public void AddCollection(X509CertificateCollection collection, MonoBtlsX509TrustKind trust)
		{
			MonoBtlsX509LookupMonoCollection monoLookup = new MonoBtlsX509LookupMonoCollection(collection, trust);
			new MonoBtlsX509Lookup(this, MonoBtlsX509LookupType.MONO).AddMono(monoLookup);
		}

		// Token: 0x06000628 RID: 1576 RVA: 0x00011338 File Offset: 0x0000F538
		protected override void Close()
		{
			try
			{
				if (this.lookupHash != null)
				{
					foreach (MonoBtlsX509Lookup monoBtlsX509Lookup in this.lookupHash.Values)
					{
						monoBtlsX509Lookup.Dispose();
					}
					this.lookupHash = null;
				}
			}
			finally
			{
				base.Close();
			}
		}

		// Token: 0x0400044E RID: 1102
		private Dictionary<IntPtr, MonoBtlsX509Lookup> lookupHash;

		// Token: 0x02000109 RID: 265
		internal class BoringX509StoreHandle : MonoBtlsObject.MonoBtlsHandle
		{
			// Token: 0x06000629 RID: 1577 RVA: 0x0000CD48 File Offset: 0x0000AF48
			public BoringX509StoreHandle(IntPtr handle) : base(handle, true)
			{
			}

			// Token: 0x0600062A RID: 1578 RVA: 0x000113B4 File Offset: 0x0000F5B4
			protected override bool ReleaseHandle()
			{
				MonoBtlsX509Store.mono_btls_x509_store_free(this.handle);
				return true;
			}
		}
	}
}
