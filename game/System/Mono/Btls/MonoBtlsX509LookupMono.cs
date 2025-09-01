using System;
using System.Runtime.InteropServices;
using Mono.Util;

namespace Mono.Btls
{
	// Token: 0x020000FD RID: 253
	internal abstract class MonoBtlsX509LookupMono : MonoBtlsObject
	{
		// Token: 0x170000FF RID: 255
		// (get) Token: 0x060005D3 RID: 1491 RVA: 0x000109C6 File Offset: 0x0000EBC6
		internal new MonoBtlsX509LookupMono.BoringX509LookupMonoHandle Handle
		{
			get
			{
				return (MonoBtlsX509LookupMono.BoringX509LookupMonoHandle)base.Handle;
			}
		}

		// Token: 0x060005D4 RID: 1492
		[DllImport("libmono-btls-shared")]
		private static extern IntPtr mono_btls_x509_lookup_mono_new();

		// Token: 0x060005D5 RID: 1493
		[DllImport("libmono-btls-shared")]
		private static extern void mono_btls_x509_lookup_mono_init(IntPtr handle, IntPtr instance, IntPtr by_subject_func);

		// Token: 0x060005D6 RID: 1494
		[DllImport("libmono-btls-shared")]
		private static extern int mono_btls_x509_lookup_mono_free(IntPtr handle);

		// Token: 0x060005D7 RID: 1495 RVA: 0x000109D4 File Offset: 0x0000EBD4
		internal MonoBtlsX509LookupMono() : base(new MonoBtlsX509LookupMono.BoringX509LookupMonoHandle(MonoBtlsX509LookupMono.mono_btls_x509_lookup_mono_new()))
		{
			this.gch = GCHandle.Alloc(this);
			this.instance = GCHandle.ToIntPtr(this.gch);
			this.bySubjectFunc = new MonoBtlsX509LookupMono.BySubjectFunc(MonoBtlsX509LookupMono.OnGetBySubject);
			this.bySubjectFuncPtr = Marshal.GetFunctionPointerForDelegate<MonoBtlsX509LookupMono.BySubjectFunc>(this.bySubjectFunc);
			MonoBtlsX509LookupMono.mono_btls_x509_lookup_mono_init(this.Handle.DangerousGetHandle(), this.instance, this.bySubjectFuncPtr);
		}

		// Token: 0x060005D8 RID: 1496 RVA: 0x00010A4D File Offset: 0x0000EC4D
		internal void Install(MonoBtlsX509Lookup lookup)
		{
			if (this.lookup != null)
			{
				throw new InvalidOperationException();
			}
			this.lookup = lookup;
		}

		// Token: 0x060005D9 RID: 1497 RVA: 0x00010A64 File Offset: 0x0000EC64
		protected void AddCertificate(MonoBtlsX509 certificate)
		{
			this.lookup.AddCertificate(certificate);
		}

		// Token: 0x060005DA RID: 1498
		protected abstract MonoBtlsX509 OnGetBySubject(MonoBtlsX509Name name);

		// Token: 0x060005DB RID: 1499 RVA: 0x00010A74 File Offset: 0x0000EC74
		[MonoPInvokeCallback(typeof(MonoBtlsX509LookupMono.BySubjectFunc))]
		private static int OnGetBySubject(IntPtr instance, IntPtr name_ptr, out IntPtr x509_ptr)
		{
			int result;
			try
			{
				MonoBtlsX509Name.BoringX509NameHandle boringX509NameHandle = null;
				try
				{
					MonoBtlsX509LookupMono monoBtlsX509LookupMono = (MonoBtlsX509LookupMono)GCHandle.FromIntPtr(instance).Target;
					boringX509NameHandle = new MonoBtlsX509Name.BoringX509NameHandle(name_ptr, false);
					MonoBtlsX509Name name = new MonoBtlsX509Name(boringX509NameHandle);
					MonoBtlsX509 monoBtlsX = monoBtlsX509LookupMono.OnGetBySubject(name);
					if (monoBtlsX != null)
					{
						x509_ptr = monoBtlsX.Handle.StealHandle();
						result = 1;
					}
					else
					{
						x509_ptr = IntPtr.Zero;
						result = 0;
					}
				}
				finally
				{
					if (boringX509NameHandle != null)
					{
						boringX509NameHandle.Dispose();
					}
				}
			}
			catch (Exception arg)
			{
				Console.WriteLine("LOOKUP METHOD - GET BY SUBJECT EX: {0}", arg);
				x509_ptr = IntPtr.Zero;
				result = 0;
			}
			return result;
		}

		// Token: 0x060005DC RID: 1500 RVA: 0x00010B10 File Offset: 0x0000ED10
		protected override void Close()
		{
			try
			{
				if (this.gch.IsAllocated)
				{
					this.gch.Free();
				}
			}
			finally
			{
				this.instance = IntPtr.Zero;
				this.bySubjectFunc = null;
				this.bySubjectFuncPtr = IntPtr.Zero;
				base.Close();
			}
		}

		// Token: 0x04000423 RID: 1059
		private GCHandle gch;

		// Token: 0x04000424 RID: 1060
		private IntPtr instance;

		// Token: 0x04000425 RID: 1061
		private MonoBtlsX509LookupMono.BySubjectFunc bySubjectFunc;

		// Token: 0x04000426 RID: 1062
		private IntPtr bySubjectFuncPtr;

		// Token: 0x04000427 RID: 1063
		private MonoBtlsX509Lookup lookup;

		// Token: 0x020000FE RID: 254
		internal class BoringX509LookupMonoHandle : MonoBtlsObject.MonoBtlsHandle
		{
			// Token: 0x060005DD RID: 1501 RVA: 0x0000CD48 File Offset: 0x0000AF48
			public BoringX509LookupMonoHandle(IntPtr handle) : base(handle, true)
			{
			}

			// Token: 0x060005DE RID: 1502 RVA: 0x00010B6C File Offset: 0x0000ED6C
			protected override bool ReleaseHandle()
			{
				MonoBtlsX509LookupMono.mono_btls_x509_lookup_mono_free(this.handle);
				return true;
			}
		}

		// Token: 0x020000FF RID: 255
		// (Invoke) Token: 0x060005E0 RID: 1504
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		private delegate int BySubjectFunc(IntPtr instance, IntPtr name, out IntPtr x509_ptr);
	}
}
