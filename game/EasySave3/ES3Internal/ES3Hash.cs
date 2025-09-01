using System;
using System.Security.Cryptography;
using System.Text;

namespace ES3Internal
{
	// Token: 0x020000D0 RID: 208
	public static class ES3Hash
	{
		// Token: 0x0600041C RID: 1052 RVA: 0x0001ADFC File Offset: 0x00018FFC
		public static string SHA1Hash(string input)
		{
			string @string;
			using (SHA1Managed sha1Managed = new SHA1Managed())
			{
				@string = Encoding.UTF8.GetString(sha1Managed.ComputeHash(Encoding.UTF8.GetBytes(input)));
			}
			return @string;
		}
	}
}
