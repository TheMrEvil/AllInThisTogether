using System;
using System.Runtime.InteropServices;

namespace System.Drawing.Drawing2D
{
	// Token: 0x02000150 RID: 336
	internal class SafeCustomLineCapHandle : SafeHandle
	{
		// Token: 0x06000E4F RID: 3663 RVA: 0x00020711 File Offset: 0x0001E911
		internal SafeCustomLineCapHandle(IntPtr h) : base(IntPtr.Zero, true)
		{
			base.SetHandle(h);
		}

		// Token: 0x06000E50 RID: 3664 RVA: 0x00020728 File Offset: 0x0001E928
		protected override bool ReleaseHandle()
		{
			int num = 0;
			if (!this.IsInvalid)
			{
				try
				{
					num = GDIPlus.GdipDeleteCustomLineCap(new HandleRef(this, this.handle));
				}
				catch (Exception ex)
				{
					if (ClientUtils.IsSecurityOrCriticalException(ex))
					{
						throw;
					}
				}
				finally
				{
					this.handle = IntPtr.Zero;
				}
			}
			return num == 0;
		}

		// Token: 0x170003CF RID: 975
		// (get) Token: 0x06000E51 RID: 3665 RVA: 0x0002078C File Offset: 0x0001E98C
		public override bool IsInvalid
		{
			get
			{
				return this.handle == IntPtr.Zero;
			}
		}

		// Token: 0x06000E52 RID: 3666 RVA: 0x0002079E File Offset: 0x0001E99E
		public static implicit operator IntPtr(SafeCustomLineCapHandle handle)
		{
			if (handle == null)
			{
				return IntPtr.Zero;
			}
			return handle.handle;
		}

		// Token: 0x06000E53 RID: 3667 RVA: 0x000207AF File Offset: 0x0001E9AF
		public static explicit operator SafeCustomLineCapHandle(IntPtr handle)
		{
			return new SafeCustomLineCapHandle(handle);
		}
	}
}
