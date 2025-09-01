using System;
using System.Runtime.InteropServices;

namespace Mono.Btls
{
	// Token: 0x02000110 RID: 272
	internal class MonoBtlsX509VerifyParam : MonoBtlsObject
	{
		// Token: 0x17000105 RID: 261
		// (get) Token: 0x06000653 RID: 1619 RVA: 0x00011895 File Offset: 0x0000FA95
		internal new MonoBtlsX509VerifyParam.BoringX509VerifyParamHandle Handle
		{
			get
			{
				return (MonoBtlsX509VerifyParam.BoringX509VerifyParamHandle)base.Handle;
			}
		}

		// Token: 0x06000654 RID: 1620
		[DllImport("libmono-btls-shared")]
		private static extern IntPtr mono_btls_x509_verify_param_new();

		// Token: 0x06000655 RID: 1621
		[DllImport("libmono-btls-shared")]
		private static extern IntPtr mono_btls_x509_verify_param_copy(IntPtr handle);

		// Token: 0x06000656 RID: 1622
		[DllImport("libmono-btls-shared")]
		private static extern IntPtr mono_btls_x509_verify_param_lookup(IntPtr name);

		// Token: 0x06000657 RID: 1623
		[DllImport("libmono-btls-shared")]
		private static extern int mono_btls_x509_verify_param_can_modify(IntPtr param);

		// Token: 0x06000658 RID: 1624
		[DllImport("libmono-btls-shared")]
		private static extern int mono_btls_x509_verify_param_set_name(IntPtr handle, IntPtr name);

		// Token: 0x06000659 RID: 1625
		[DllImport("libmono-btls-shared")]
		private static extern int mono_btls_x509_verify_param_set_host(IntPtr handle, IntPtr name, int namelen);

		// Token: 0x0600065A RID: 1626
		[DllImport("libmono-btls-shared")]
		private static extern int mono_btls_x509_verify_param_add_host(IntPtr handle, IntPtr name, int namelen);

		// Token: 0x0600065B RID: 1627
		[DllImport("libmono-btls-shared")]
		private static extern ulong mono_btls_x509_verify_param_get_flags(IntPtr handle);

		// Token: 0x0600065C RID: 1628
		[DllImport("libmono-btls-shared")]
		private static extern int mono_btls_x509_verify_param_set_flags(IntPtr handle, ulong flags);

		// Token: 0x0600065D RID: 1629
		[DllImport("libmono-btls-shared")]
		private static extern MonoBtlsX509VerifyFlags mono_btls_x509_verify_param_get_mono_flags(IntPtr handle);

		// Token: 0x0600065E RID: 1630
		[DllImport("libmono-btls-shared")]
		private static extern int mono_btls_x509_verify_param_set_mono_flags(IntPtr handle, MonoBtlsX509VerifyFlags flags);

		// Token: 0x0600065F RID: 1631
		[DllImport("libmono-btls-shared")]
		private static extern int mono_btls_x509_verify_param_set_purpose(IntPtr handle, MonoBtlsX509Purpose purpose);

		// Token: 0x06000660 RID: 1632
		[DllImport("libmono-btls-shared")]
		private static extern int mono_btls_x509_verify_param_get_depth(IntPtr handle);

		// Token: 0x06000661 RID: 1633
		[DllImport("libmono-btls-shared")]
		private static extern int mono_btls_x509_verify_param_set_depth(IntPtr handle, int depth);

		// Token: 0x06000662 RID: 1634
		[DllImport("libmono-btls-shared")]
		private static extern int mono_btls_x509_verify_param_set_time(IntPtr handle, long time);

		// Token: 0x06000663 RID: 1635
		[DllImport("libmono-btls-shared")]
		private static extern IntPtr mono_btls_x509_verify_param_get_peername(IntPtr handle);

		// Token: 0x06000664 RID: 1636
		[DllImport("libmono-btls-shared")]
		private static extern void mono_btls_x509_verify_param_free(IntPtr handle);

		// Token: 0x06000665 RID: 1637 RVA: 0x000118A2 File Offset: 0x0000FAA2
		internal MonoBtlsX509VerifyParam() : base(new MonoBtlsX509VerifyParam.BoringX509VerifyParamHandle(MonoBtlsX509VerifyParam.mono_btls_x509_verify_param_new()))
		{
		}

		// Token: 0x06000666 RID: 1638 RVA: 0x0000CA92 File Offset: 0x0000AC92
		internal MonoBtlsX509VerifyParam(MonoBtlsX509VerifyParam.BoringX509VerifyParamHandle handle) : base(handle)
		{
		}

		// Token: 0x06000667 RID: 1639 RVA: 0x000118B4 File Offset: 0x0000FAB4
		public MonoBtlsX509VerifyParam Copy()
		{
			IntPtr intPtr = MonoBtlsX509VerifyParam.mono_btls_x509_verify_param_copy(this.Handle.DangerousGetHandle());
			base.CheckError(intPtr != IntPtr.Zero, "Copy");
			return new MonoBtlsX509VerifyParam(new MonoBtlsX509VerifyParam.BoringX509VerifyParamHandle(intPtr));
		}

		// Token: 0x06000668 RID: 1640 RVA: 0x000118F3 File Offset: 0x0000FAF3
		public static MonoBtlsX509VerifyParam GetSslClient()
		{
			return MonoBtlsX509VerifyParam.Lookup("ssl_client", true);
		}

		// Token: 0x06000669 RID: 1641 RVA: 0x00011900 File Offset: 0x0000FB00
		public static MonoBtlsX509VerifyParam GetSslServer()
		{
			return MonoBtlsX509VerifyParam.Lookup("ssl_server", true);
		}

		// Token: 0x0600066A RID: 1642 RVA: 0x00011910 File Offset: 0x0000FB10
		public static MonoBtlsX509VerifyParam Lookup(string name, bool fail = false)
		{
			IntPtr intPtr = IntPtr.Zero;
			IntPtr intPtr2 = IntPtr.Zero;
			MonoBtlsX509VerifyParam result;
			try
			{
				intPtr = Marshal.StringToHGlobalAnsi(name);
				intPtr2 = MonoBtlsX509VerifyParam.mono_btls_x509_verify_param_lookup(intPtr);
				if (intPtr2 == IntPtr.Zero)
				{
					if (fail)
					{
						throw new MonoBtlsException("X509_VERIFY_PARAM_lookup() could not find '{0}'.", new object[]
						{
							name
						});
					}
					result = null;
				}
				else
				{
					result = new MonoBtlsX509VerifyParam(new MonoBtlsX509VerifyParam.BoringX509VerifyParamHandle(intPtr2));
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

		// Token: 0x17000106 RID: 262
		// (get) Token: 0x0600066B RID: 1643 RVA: 0x00011994 File Offset: 0x0000FB94
		public bool CanModify
		{
			get
			{
				return MonoBtlsX509VerifyParam.mono_btls_x509_verify_param_can_modify(this.Handle.DangerousGetHandle()) != 0;
			}
		}

		// Token: 0x0600066C RID: 1644 RVA: 0x000119A9 File Offset: 0x0000FBA9
		private void WantToModify()
		{
			if (!this.CanModify)
			{
				throw new MonoBtlsException("Attempting to modify read-only MonoBtlsX509VerifyParam instance.");
			}
		}

		// Token: 0x0600066D RID: 1645 RVA: 0x000119C0 File Offset: 0x0000FBC0
		public void SetName(string name)
		{
			this.WantToModify();
			IntPtr intPtr = IntPtr.Zero;
			try
			{
				intPtr = Marshal.StringToHGlobalAnsi(name);
				int ret = MonoBtlsX509VerifyParam.mono_btls_x509_verify_param_set_name(this.Handle.DangerousGetHandle(), intPtr);
				base.CheckError(ret, "SetName");
			}
			finally
			{
				if (intPtr != IntPtr.Zero)
				{
					Marshal.FreeHGlobal(intPtr);
				}
			}
		}

		// Token: 0x0600066E RID: 1646 RVA: 0x00011A24 File Offset: 0x0000FC24
		public void SetHost(string name)
		{
			this.WantToModify();
			IntPtr intPtr = IntPtr.Zero;
			try
			{
				intPtr = Marshal.StringToHGlobalAnsi(name);
				int ret = MonoBtlsX509VerifyParam.mono_btls_x509_verify_param_set_host(this.Handle.DangerousGetHandle(), intPtr, name.Length);
				base.CheckError(ret, "SetHost");
			}
			finally
			{
				if (intPtr != IntPtr.Zero)
				{
					Marshal.FreeHGlobal(intPtr);
				}
			}
		}

		// Token: 0x0600066F RID: 1647 RVA: 0x00011A90 File Offset: 0x0000FC90
		public void AddHost(string name)
		{
			this.WantToModify();
			IntPtr intPtr = IntPtr.Zero;
			try
			{
				intPtr = Marshal.StringToHGlobalAnsi(name);
				int ret = MonoBtlsX509VerifyParam.mono_btls_x509_verify_param_add_host(this.Handle.DangerousGetHandle(), intPtr, name.Length);
				base.CheckError(ret, "AddHost");
			}
			finally
			{
				if (intPtr != IntPtr.Zero)
				{
					Marshal.FreeHGlobal(intPtr);
				}
			}
		}

		// Token: 0x06000670 RID: 1648 RVA: 0x00011AFC File Offset: 0x0000FCFC
		public ulong GetFlags()
		{
			return MonoBtlsX509VerifyParam.mono_btls_x509_verify_param_get_flags(this.Handle.DangerousGetHandle());
		}

		// Token: 0x06000671 RID: 1649 RVA: 0x00011B10 File Offset: 0x0000FD10
		public void SetFlags(ulong flags)
		{
			this.WantToModify();
			int ret = MonoBtlsX509VerifyParam.mono_btls_x509_verify_param_set_flags(this.Handle.DangerousGetHandle(), flags);
			base.CheckError(ret, "SetFlags");
		}

		// Token: 0x06000672 RID: 1650 RVA: 0x00011B41 File Offset: 0x0000FD41
		public MonoBtlsX509VerifyFlags GetMonoFlags()
		{
			return MonoBtlsX509VerifyParam.mono_btls_x509_verify_param_get_mono_flags(this.Handle.DangerousGetHandle());
		}

		// Token: 0x06000673 RID: 1651 RVA: 0x00011B54 File Offset: 0x0000FD54
		public void SetMonoFlags(MonoBtlsX509VerifyFlags flags)
		{
			this.WantToModify();
			int ret = MonoBtlsX509VerifyParam.mono_btls_x509_verify_param_set_mono_flags(this.Handle.DangerousGetHandle(), flags);
			base.CheckError(ret, "SetMonoFlags");
		}

		// Token: 0x06000674 RID: 1652 RVA: 0x00011B88 File Offset: 0x0000FD88
		public void SetPurpose(MonoBtlsX509Purpose purpose)
		{
			this.WantToModify();
			int ret = MonoBtlsX509VerifyParam.mono_btls_x509_verify_param_set_purpose(this.Handle.DangerousGetHandle(), purpose);
			base.CheckError(ret, "SetPurpose");
		}

		// Token: 0x06000675 RID: 1653 RVA: 0x00011BB9 File Offset: 0x0000FDB9
		public int GetDepth()
		{
			return MonoBtlsX509VerifyParam.mono_btls_x509_verify_param_get_depth(this.Handle.DangerousGetHandle());
		}

		// Token: 0x06000676 RID: 1654 RVA: 0x00011BCC File Offset: 0x0000FDCC
		public void SetDepth(int depth)
		{
			this.WantToModify();
			int ret = MonoBtlsX509VerifyParam.mono_btls_x509_verify_param_set_depth(this.Handle.DangerousGetHandle(), depth);
			base.CheckError(ret, "SetDepth");
		}

		// Token: 0x06000677 RID: 1655 RVA: 0x00011C00 File Offset: 0x0000FE00
		public void SetTime(DateTime time)
		{
			this.WantToModify();
			DateTime value = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
			long time2 = (long)time.Subtract(value).TotalSeconds;
			int ret = MonoBtlsX509VerifyParam.mono_btls_x509_verify_param_set_time(this.Handle.DangerousGetHandle(), time2);
			base.CheckError(ret, "SetTime");
		}

		// Token: 0x06000678 RID: 1656 RVA: 0x00011C58 File Offset: 0x0000FE58
		public string GetPeerName()
		{
			IntPtr intPtr = MonoBtlsX509VerifyParam.mono_btls_x509_verify_param_get_peername(this.Handle.DangerousGetHandle());
			if (intPtr == IntPtr.Zero)
			{
				return null;
			}
			return Marshal.PtrToStringAnsi(intPtr);
		}

		// Token: 0x02000111 RID: 273
		internal class BoringX509VerifyParamHandle : MonoBtlsObject.MonoBtlsHandle
		{
			// Token: 0x06000679 RID: 1657 RVA: 0x0000CD48 File Offset: 0x0000AF48
			public BoringX509VerifyParamHandle(IntPtr handle) : base(handle, true)
			{
			}

			// Token: 0x0600067A RID: 1658 RVA: 0x00011C8B File Offset: 0x0000FE8B
			protected override bool ReleaseHandle()
			{
				MonoBtlsX509VerifyParam.mono_btls_x509_verify_param_free(this.handle);
				return true;
			}
		}
	}
}
