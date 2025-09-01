using System;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using Mono.Security.Cryptography;

namespace Mono.Btls
{
	// Token: 0x020000DB RID: 219
	internal class MonoBtlsKey : MonoBtlsObject
	{
		// Token: 0x06000479 RID: 1145
		[DllImport("libmono-btls-shared")]
		private static extern IntPtr mono_btls_key_new();

		// Token: 0x0600047A RID: 1146
		[DllImport("libmono-btls-shared")]
		private static extern void mono_btls_key_free(IntPtr handle);

		// Token: 0x0600047B RID: 1147
		[DllImport("libmono-btls-shared")]
		private static extern IntPtr mono_btls_key_up_ref(IntPtr handle);

		// Token: 0x0600047C RID: 1148
		[DllImport("libmono-btls-shared")]
		private static extern int mono_btls_key_get_bytes(IntPtr handle, out IntPtr data, out int size, int include_private_bits);

		// Token: 0x0600047D RID: 1149
		[DllImport("libmono-btls-shared")]
		private static extern int mono_btls_key_get_bits(IntPtr handle);

		// Token: 0x0600047E RID: 1150
		[DllImport("libmono-btls-shared")]
		private static extern int mono_btls_key_is_rsa(IntPtr handle);

		// Token: 0x0600047F RID: 1151
		[DllImport("libmono-btls-shared")]
		private static extern int mono_btls_key_assign_rsa_private_key(IntPtr handle, byte[] der, int der_length);

		// Token: 0x170000E5 RID: 229
		// (get) Token: 0x06000480 RID: 1152 RVA: 0x0000DC47 File Offset: 0x0000BE47
		internal new MonoBtlsKey.BoringKeyHandle Handle
		{
			get
			{
				return (MonoBtlsKey.BoringKeyHandle)base.Handle;
			}
		}

		// Token: 0x06000481 RID: 1153 RVA: 0x0000CA92 File Offset: 0x0000AC92
		internal MonoBtlsKey(MonoBtlsKey.BoringKeyHandle handle) : base(handle)
		{
		}

		// Token: 0x06000482 RID: 1154 RVA: 0x0000DC54 File Offset: 0x0000BE54
		public byte[] GetBytes(bool include_private_bits)
		{
			IntPtr intPtr;
			int num;
			int ret = MonoBtlsKey.mono_btls_key_get_bytes(this.Handle.DangerousGetHandle(), out intPtr, out num, include_private_bits ? 1 : 0);
			base.CheckError(ret, "GetBytes");
			byte[] array = new byte[num];
			Marshal.Copy(intPtr, array, 0, num);
			base.FreeDataPtr(intPtr);
			return array;
		}

		// Token: 0x170000E6 RID: 230
		// (get) Token: 0x06000483 RID: 1155 RVA: 0x0000DCA1 File Offset: 0x0000BEA1
		public bool IsRsa
		{
			get
			{
				return MonoBtlsKey.mono_btls_key_is_rsa(this.Handle.DangerousGetHandle()) != 0;
			}
		}

		// Token: 0x06000484 RID: 1156 RVA: 0x0000DCB8 File Offset: 0x0000BEB8
		public MonoBtlsKey Copy()
		{
			base.CheckThrow();
			IntPtr intPtr = MonoBtlsKey.mono_btls_key_up_ref(this.Handle.DangerousGetHandle());
			base.CheckError(intPtr != IntPtr.Zero, "Copy");
			return new MonoBtlsKey(new MonoBtlsKey.BoringKeyHandle(intPtr));
		}

		// Token: 0x06000485 RID: 1157 RVA: 0x0000DD00 File Offset: 0x0000BF00
		public static MonoBtlsKey CreateFromRSAPrivateKey(RSA privateKey)
		{
			byte[] array = PKCS8.PrivateKeyInfo.Encode(privateKey);
			MonoBtlsKey monoBtlsKey = new MonoBtlsKey(new MonoBtlsKey.BoringKeyHandle(MonoBtlsKey.mono_btls_key_new()));
			if (MonoBtlsKey.mono_btls_key_assign_rsa_private_key(monoBtlsKey.Handle.DangerousGetHandle(), array, array.Length) == 0)
			{
				throw new MonoBtlsException("Assigning private key failed.");
			}
			return monoBtlsKey;
		}

		// Token: 0x020000DC RID: 220
		internal class BoringKeyHandle : MonoBtlsObject.MonoBtlsHandle
		{
			// Token: 0x06000486 RID: 1158 RVA: 0x0000CD48 File Offset: 0x0000AF48
			internal BoringKeyHandle(IntPtr handle) : base(handle, true)
			{
			}

			// Token: 0x06000487 RID: 1159 RVA: 0x0000DD44 File Offset: 0x0000BF44
			protected override bool ReleaseHandle()
			{
				MonoBtlsKey.mono_btls_key_free(this.handle);
				return true;
			}
		}
	}
}
