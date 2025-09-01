using System;
using System.Runtime.InteropServices;
using Microsoft.Win32.SafeHandles;

namespace Mono.Btls
{
	// Token: 0x020000DF RID: 223
	internal class MonoBtlsPkcs12 : MonoBtlsObject
	{
		// Token: 0x170000EA RID: 234
		// (get) Token: 0x06000499 RID: 1177 RVA: 0x0000DF7E File Offset: 0x0000C17E
		internal new MonoBtlsPkcs12.BoringPkcs12Handle Handle
		{
			get
			{
				return (MonoBtlsPkcs12.BoringPkcs12Handle)base.Handle;
			}
		}

		// Token: 0x0600049A RID: 1178
		[DllImport("libmono-btls-shared")]
		private static extern void mono_btls_pkcs12_free(IntPtr handle);

		// Token: 0x0600049B RID: 1179
		[DllImport("libmono-btls-shared")]
		private static extern IntPtr mono_btls_pkcs12_new();

		// Token: 0x0600049C RID: 1180
		[DllImport("libmono-btls-shared")]
		private static extern int mono_btls_pkcs12_get_count(IntPtr handle);

		// Token: 0x0600049D RID: 1181
		[DllImport("libmono-btls-shared")]
		private static extern IntPtr mono_btls_pkcs12_get_cert(IntPtr Handle, int index);

		// Token: 0x0600049E RID: 1182
		[DllImport("libmono-btls-shared")]
		private static extern int mono_btls_pkcs12_add_cert(IntPtr chain, IntPtr x509);

		// Token: 0x0600049F RID: 1183
		[DllImport("libmono-btls-shared")]
		private unsafe static extern int mono_btls_pkcs12_import(IntPtr chain, void* data, int len, SafePasswordHandle password);

		// Token: 0x060004A0 RID: 1184
		[DllImport("libmono-btls-shared")]
		private static extern int mono_btls_pkcs12_has_private_key(IntPtr pkcs12);

		// Token: 0x060004A1 RID: 1185
		[DllImport("libmono-btls-shared")]
		private static extern IntPtr mono_btls_pkcs12_get_private_key(IntPtr pkcs12);

		// Token: 0x060004A2 RID: 1186 RVA: 0x0000DF8B File Offset: 0x0000C18B
		internal MonoBtlsPkcs12() : base(new MonoBtlsPkcs12.BoringPkcs12Handle(MonoBtlsPkcs12.mono_btls_pkcs12_new()))
		{
		}

		// Token: 0x060004A3 RID: 1187 RVA: 0x0000CA92 File Offset: 0x0000AC92
		internal MonoBtlsPkcs12(MonoBtlsPkcs12.BoringPkcs12Handle handle) : base(handle)
		{
		}

		// Token: 0x170000EB RID: 235
		// (get) Token: 0x060004A4 RID: 1188 RVA: 0x0000DF9D File Offset: 0x0000C19D
		public int Count
		{
			get
			{
				return MonoBtlsPkcs12.mono_btls_pkcs12_get_count(this.Handle.DangerousGetHandle());
			}
		}

		// Token: 0x060004A5 RID: 1189 RVA: 0x0000DFB0 File Offset: 0x0000C1B0
		public MonoBtlsX509 GetCertificate(int index)
		{
			if (index >= this.Count)
			{
				throw new IndexOutOfRangeException();
			}
			IntPtr intPtr = MonoBtlsPkcs12.mono_btls_pkcs12_get_cert(this.Handle.DangerousGetHandle(), index);
			base.CheckError(intPtr != IntPtr.Zero, "GetCertificate");
			return new MonoBtlsX509(new MonoBtlsX509.BoringX509Handle(intPtr));
		}

		// Token: 0x060004A6 RID: 1190 RVA: 0x0000DFFF File Offset: 0x0000C1FF
		public void AddCertificate(MonoBtlsX509 x509)
		{
			MonoBtlsPkcs12.mono_btls_pkcs12_add_cert(this.Handle.DangerousGetHandle(), x509.Handle.DangerousGetHandle());
		}

		// Token: 0x060004A7 RID: 1191 RVA: 0x0000E020 File Offset: 0x0000C220
		public unsafe void Import(byte[] buffer, SafePasswordHandle password)
		{
			fixed (byte[] array = buffer)
			{
				void* data;
				if (buffer == null || array.Length == 0)
				{
					data = null;
				}
				else
				{
					data = (void*)(&array[0]);
				}
				int ret = MonoBtlsPkcs12.mono_btls_pkcs12_import(this.Handle.DangerousGetHandle(), data, buffer.Length, password);
				base.CheckError(ret, "Import");
			}
		}

		// Token: 0x170000EC RID: 236
		// (get) Token: 0x060004A8 RID: 1192 RVA: 0x0000E069 File Offset: 0x0000C269
		public bool HasPrivateKey
		{
			get
			{
				return MonoBtlsPkcs12.mono_btls_pkcs12_has_private_key(this.Handle.DangerousGetHandle()) != 0;
			}
		}

		// Token: 0x060004A9 RID: 1193 RVA: 0x0000E080 File Offset: 0x0000C280
		public MonoBtlsKey GetPrivateKey()
		{
			if (!this.HasPrivateKey)
			{
				throw new InvalidOperationException();
			}
			if (this.privateKey == null)
			{
				IntPtr intPtr = MonoBtlsPkcs12.mono_btls_pkcs12_get_private_key(this.Handle.DangerousGetHandle());
				base.CheckError(intPtr != IntPtr.Zero, "GetPrivateKey");
				this.privateKey = new MonoBtlsKey(new MonoBtlsKey.BoringKeyHandle(intPtr));
			}
			return this.privateKey;
		}

		// Token: 0x040003B2 RID: 946
		private MonoBtlsKey privateKey;

		// Token: 0x020000E0 RID: 224
		internal class BoringPkcs12Handle : MonoBtlsObject.MonoBtlsHandle
		{
			// Token: 0x060004AA RID: 1194 RVA: 0x0000CD48 File Offset: 0x0000AF48
			public BoringPkcs12Handle(IntPtr handle) : base(handle, true)
			{
			}

			// Token: 0x060004AB RID: 1195 RVA: 0x0000E0E1 File Offset: 0x0000C2E1
			protected override bool ReleaseHandle()
			{
				MonoBtlsPkcs12.mono_btls_pkcs12_free(this.handle);
				return true;
			}
		}
	}
}
