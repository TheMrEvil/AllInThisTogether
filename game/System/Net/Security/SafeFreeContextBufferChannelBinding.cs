using System;
using System.Runtime.InteropServices;
using System.Security.Authentication.ExtendedProtection;

namespace System.Net.Security
{
	// Token: 0x0200084A RID: 2122
	internal abstract class SafeFreeContextBufferChannelBinding : ChannelBinding
	{
		// Token: 0x17000F26 RID: 3878
		// (get) Token: 0x06004384 RID: 17284 RVA: 0x000EB970 File Offset: 0x000E9B70
		public override int Size
		{
			get
			{
				return this._size;
			}
		}

		// Token: 0x17000F27 RID: 3879
		// (get) Token: 0x06004385 RID: 17285 RVA: 0x000EB978 File Offset: 0x000E9B78
		public override bool IsInvalid
		{
			get
			{
				return this.handle == new IntPtr(0) || this.handle == new IntPtr(-1);
			}
		}

		// Token: 0x06004386 RID: 17286 RVA: 0x00013B95 File Offset: 0x00011D95
		internal void Set(IntPtr value)
		{
			this.handle = value;
		}

		// Token: 0x06004387 RID: 17287 RVA: 0x000EB9A0 File Offset: 0x000E9BA0
		internal static SafeFreeContextBufferChannelBinding CreateEmptyHandle()
		{
			return new SafeFreeContextBufferChannelBinding_SECURITY();
		}

		// Token: 0x06004388 RID: 17288 RVA: 0x000EB9A8 File Offset: 0x000E9BA8
		public unsafe static int QueryContextChannelBinding(SafeDeleteContext phContext, Interop.SspiCli.ContextAttribute contextAttribute, SecPkgContext_Bindings* buffer, SafeFreeContextBufferChannelBinding refHandle)
		{
			int num = -2146893055;
			if (contextAttribute != Interop.SspiCli.ContextAttribute.SECPKG_ATTR_ENDPOINT_BINDINGS && contextAttribute != Interop.SspiCli.ContextAttribute.SECPKG_ATTR_UNIQUE_BINDINGS)
			{
				return num;
			}
			try
			{
				bool flag = false;
				phContext.DangerousAddRef(ref flag);
				num = Interop.SspiCli.QueryContextAttributesW(ref phContext._handle, contextAttribute, (void*)buffer);
			}
			finally
			{
				phContext.DangerousRelease();
			}
			if (num == 0 && refHandle != null)
			{
				refHandle.Set(buffer->Bindings);
				refHandle._size = buffer->BindingsLength;
			}
			if (num != 0 && refHandle != null)
			{
				refHandle.SetHandleAsInvalid();
			}
			return num;
		}

		// Token: 0x06004389 RID: 17289 RVA: 0x000EBA24 File Offset: 0x000E9C24
		public override string ToString()
		{
			if (this.IsInvalid)
			{
				return null;
			}
			byte[] array = new byte[this._size];
			Marshal.Copy(this.handle, array, 0, array.Length);
			return BitConverter.ToString(array).Replace('-', ' ');
		}

		// Token: 0x0600438A RID: 17290 RVA: 0x000EBA66 File Offset: 0x000E9C66
		protected SafeFreeContextBufferChannelBinding()
		{
		}

		// Token: 0x040028CF RID: 10447
		private int _size;
	}
}
