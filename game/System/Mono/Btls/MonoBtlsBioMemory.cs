using System;
using System.Runtime.InteropServices;

namespace Mono.Btls
{
	// Token: 0x020000CF RID: 207
	internal class MonoBtlsBioMemory : MonoBtlsBio
	{
		// Token: 0x06000418 RID: 1048
		[DllImport("libmono-btls-shared")]
		private static extern IntPtr mono_btls_bio_mem_new();

		// Token: 0x06000419 RID: 1049
		[DllImport("libmono-btls-shared")]
		private static extern int mono_btls_bio_mem_get_data(IntPtr handle, out IntPtr data);

		// Token: 0x0600041A RID: 1050 RVA: 0x0000CD7D File Offset: 0x0000AF7D
		public MonoBtlsBioMemory() : base(new MonoBtlsBio.BoringBioHandle(MonoBtlsBioMemory.mono_btls_bio_mem_new()))
		{
		}

		// Token: 0x0600041B RID: 1051 RVA: 0x0000CD90 File Offset: 0x0000AF90
		public byte[] GetData()
		{
			bool flag = false;
			byte[] result;
			try
			{
				base.Handle.DangerousAddRef(ref flag);
				IntPtr source;
				int num = MonoBtlsBioMemory.mono_btls_bio_mem_get_data(base.Handle.DangerousGetHandle(), out source);
				base.CheckError(num > 0, "GetData");
				byte[] array = new byte[num];
				Marshal.Copy(source, array, 0, num);
				result = array;
			}
			finally
			{
				if (flag)
				{
					base.Handle.DangerousRelease();
				}
			}
			return result;
		}
	}
}
