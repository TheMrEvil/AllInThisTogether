using System;
using System.Runtime.InteropServices;
using System.Text;

namespace Mono.Btls
{
	// Token: 0x02000102 RID: 258
	internal class MonoBtlsX509Name : MonoBtlsObject
	{
		// Token: 0x060005E7 RID: 1511
		[DllImport("libmono-btls-shared")]
		private static extern int mono_btls_x509_name_print_bio(IntPtr handle, IntPtr bio);

		// Token: 0x060005E8 RID: 1512
		[DllImport("libmono-btls-shared")]
		private static extern int mono_btls_x509_name_print_string(IntPtr handle, IntPtr buffer, int size);

		// Token: 0x060005E9 RID: 1513
		[DllImport("libmono-btls-shared")]
		private static extern int mono_btls_x509_name_get_raw_data(IntPtr handle, out IntPtr buffer, int use_canon_enc);

		// Token: 0x060005EA RID: 1514
		[DllImport("libmono-btls-shared")]
		private static extern long mono_btls_x509_name_hash(IntPtr handle);

		// Token: 0x060005EB RID: 1515
		[DllImport("libmono-btls-shared")]
		private static extern long mono_btls_x509_name_hash_old(IntPtr handle);

		// Token: 0x060005EC RID: 1516
		[DllImport("libmono-btls-shared")]
		private static extern int mono_btls_x509_name_get_entry_count(IntPtr handle);

		// Token: 0x060005ED RID: 1517
		[DllImport("libmono-btls-shared")]
		private static extern MonoBtlsX509NameEntryType mono_btls_x509_name_get_entry_type(IntPtr name, int index);

		// Token: 0x060005EE RID: 1518
		[DllImport("libmono-btls-shared")]
		private static extern int mono_btls_x509_name_get_entry_oid(IntPtr name, int index, IntPtr buffer, int size);

		// Token: 0x060005EF RID: 1519
		[DllImport("libmono-btls-shared")]
		private static extern int mono_btls_x509_name_get_entry_oid_data(IntPtr name, int index, out IntPtr data);

		// Token: 0x060005F0 RID: 1520
		[DllImport("libmono-btls-shared")]
		private static extern int mono_btls_x509_name_get_entry_value(IntPtr name, int index, out int tag, out IntPtr str);

		// Token: 0x060005F1 RID: 1521
		[DllImport("libmono-btls-shared")]
		private unsafe static extern IntPtr mono_btls_x509_name_from_data(void* data, int len, int use_canon_enc);

		// Token: 0x060005F2 RID: 1522
		[DllImport("libmono-btls-shared")]
		private static extern void mono_btls_x509_name_free(IntPtr handle);

		// Token: 0x17000100 RID: 256
		// (get) Token: 0x060005F3 RID: 1523 RVA: 0x00010CF4 File Offset: 0x0000EEF4
		internal new MonoBtlsX509Name.BoringX509NameHandle Handle
		{
			get
			{
				return (MonoBtlsX509Name.BoringX509NameHandle)base.Handle;
			}
		}

		// Token: 0x060005F4 RID: 1524 RVA: 0x0000CA92 File Offset: 0x0000AC92
		internal MonoBtlsX509Name(MonoBtlsX509Name.BoringX509NameHandle handle) : base(handle)
		{
		}

		// Token: 0x060005F5 RID: 1525 RVA: 0x00010D04 File Offset: 0x0000EF04
		public string GetString()
		{
			IntPtr intPtr = Marshal.AllocHGlobal(4096);
			string result;
			try
			{
				int ret = MonoBtlsX509Name.mono_btls_x509_name_print_string(this.Handle.DangerousGetHandle(), intPtr, 4096);
				base.CheckError(ret, "GetString");
				result = Marshal.PtrToStringAnsi(intPtr);
			}
			finally
			{
				Marshal.FreeHGlobal(intPtr);
			}
			return result;
		}

		// Token: 0x060005F6 RID: 1526 RVA: 0x00010D60 File Offset: 0x0000EF60
		public void PrintBio(MonoBtlsBio bio)
		{
			int ret = MonoBtlsX509Name.mono_btls_x509_name_print_bio(this.Handle.DangerousGetHandle(), bio.Handle.DangerousGetHandle());
			base.CheckError(ret, "PrintBio");
		}

		// Token: 0x060005F7 RID: 1527 RVA: 0x00010D98 File Offset: 0x0000EF98
		public byte[] GetRawData(bool use_canon_enc)
		{
			IntPtr intPtr;
			int num = MonoBtlsX509Name.mono_btls_x509_name_get_raw_data(this.Handle.DangerousGetHandle(), out intPtr, use_canon_enc ? 1 : 0);
			base.CheckError(num > 0, "GetRawData");
			byte[] array = new byte[num];
			Marshal.Copy(intPtr, array, 0, num);
			base.FreeDataPtr(intPtr);
			return array;
		}

		// Token: 0x060005F8 RID: 1528 RVA: 0x00010DE6 File Offset: 0x0000EFE6
		public long GetHash()
		{
			return MonoBtlsX509Name.mono_btls_x509_name_hash(this.Handle.DangerousGetHandle());
		}

		// Token: 0x060005F9 RID: 1529 RVA: 0x00010DF8 File Offset: 0x0000EFF8
		public long GetHashOld()
		{
			return MonoBtlsX509Name.mono_btls_x509_name_hash_old(this.Handle.DangerousGetHandle());
		}

		// Token: 0x060005FA RID: 1530 RVA: 0x00010E0A File Offset: 0x0000F00A
		public int GetEntryCount()
		{
			return MonoBtlsX509Name.mono_btls_x509_name_get_entry_count(this.Handle.DangerousGetHandle());
		}

		// Token: 0x060005FB RID: 1531 RVA: 0x00010E1C File Offset: 0x0000F01C
		public MonoBtlsX509NameEntryType GetEntryType(int index)
		{
			if (index >= this.GetEntryCount())
			{
				throw new ArgumentOutOfRangeException();
			}
			return MonoBtlsX509Name.mono_btls_x509_name_get_entry_type(this.Handle.DangerousGetHandle(), index);
		}

		// Token: 0x060005FC RID: 1532 RVA: 0x00010E40 File Offset: 0x0000F040
		public string GetEntryOid(int index)
		{
			if (index >= this.GetEntryCount())
			{
				throw new ArgumentOutOfRangeException();
			}
			IntPtr intPtr = Marshal.AllocHGlobal(4096);
			string result;
			try
			{
				int num = MonoBtlsX509Name.mono_btls_x509_name_get_entry_oid(this.Handle.DangerousGetHandle(), index, intPtr, 4096);
				base.CheckError(num > 0, "GetEntryOid");
				result = Marshal.PtrToStringAnsi(intPtr);
			}
			finally
			{
				Marshal.FreeHGlobal(intPtr);
			}
			return result;
		}

		// Token: 0x060005FD RID: 1533 RVA: 0x00010EB0 File Offset: 0x0000F0B0
		public byte[] GetEntryOidData(int index)
		{
			IntPtr source;
			int num = MonoBtlsX509Name.mono_btls_x509_name_get_entry_oid_data(this.Handle.DangerousGetHandle(), index, out source);
			base.CheckError(num > 0, "GetEntryOidData");
			byte[] array = new byte[num];
			Marshal.Copy(source, array, 0, num);
			return array;
		}

		// Token: 0x060005FE RID: 1534 RVA: 0x00010EF4 File Offset: 0x0000F0F4
		public unsafe string GetEntryValue(int index, out int tag)
		{
			if (index >= this.GetEntryCount())
			{
				throw new ArgumentOutOfRangeException();
			}
			IntPtr intPtr;
			int num = MonoBtlsX509Name.mono_btls_x509_name_get_entry_value(this.Handle.DangerousGetHandle(), index, out tag, out intPtr);
			if (num <= 0)
			{
				return null;
			}
			string @string;
			try
			{
				@string = new UTF8Encoding().GetString((byte*)((void*)intPtr), num);
			}
			finally
			{
				if (intPtr != IntPtr.Zero)
				{
					base.FreeDataPtr(intPtr);
				}
			}
			return @string;
		}

		// Token: 0x060005FF RID: 1535 RVA: 0x00010F68 File Offset: 0x0000F168
		public unsafe static MonoBtlsX509Name CreateFromData(byte[] data, bool use_canon_enc)
		{
			void* data2;
			if (data == null || data.Length == 0)
			{
				data2 = null;
			}
			else
			{
				data2 = (void*)(&data[0]);
			}
			IntPtr intPtr = MonoBtlsX509Name.mono_btls_x509_name_from_data(data2, data.Length, use_canon_enc ? 1 : 0);
			if (intPtr == IntPtr.Zero)
			{
				throw new MonoBtlsException("mono_btls_x509_name_from_data() failed.");
			}
			return new MonoBtlsX509Name(new MonoBtlsX509Name.BoringX509NameHandle(intPtr, false));
		}

		// Token: 0x02000103 RID: 259
		internal class BoringX509NameHandle : MonoBtlsObject.MonoBtlsHandle
		{
			// Token: 0x06000600 RID: 1536 RVA: 0x00010FC0 File Offset: 0x0000F1C0
			internal BoringX509NameHandle(IntPtr handle, bool ownsHandle) : base(handle, ownsHandle)
			{
				this.dontFree = !ownsHandle;
			}

			// Token: 0x06000601 RID: 1537 RVA: 0x00010FD4 File Offset: 0x0000F1D4
			protected override bool ReleaseHandle()
			{
				if (!this.dontFree)
				{
					MonoBtlsX509Name.mono_btls_x509_name_free(this.handle);
				}
				return true;
			}

			// Token: 0x04000431 RID: 1073
			private bool dontFree;
		}
	}
}
