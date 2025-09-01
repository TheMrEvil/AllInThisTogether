using System;
using System.Security.Cryptography;

namespace Internal.Cryptography
{
	// Token: 0x02000106 RID: 262
	internal static class CryptoThrowHelper
	{
		// Token: 0x060006C1 RID: 1729 RVA: 0x0001C1DC File Offset: 0x0001A3DC
		public static CryptographicException ToCryptographicException(this int hr)
		{
			string message = Interop.Kernel32.GetMessage(hr);
			return new CryptoThrowHelper.WindowsCryptographicException(hr, message);
		}

		// Token: 0x02000107 RID: 263
		private sealed class WindowsCryptographicException : CryptographicException
		{
			// Token: 0x060006C2 RID: 1730 RVA: 0x0001C1F7 File Offset: 0x0001A3F7
			public WindowsCryptographicException(int hr, string message) : base(message)
			{
				base.HResult = hr;
			}
		}
	}
}
