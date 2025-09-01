using System;
using System.Collections.Generic;
using System.Reflection;
using System.Security;
using System.Security.Permissions;

namespace System.Xml
{
	// Token: 0x0200005A RID: 90
	internal class SecureStringHasher : IEqualityComparer<string>
	{
		// Token: 0x06000290 RID: 656 RVA: 0x0000FE13 File Offset: 0x0000E013
		public SecureStringHasher()
		{
			this.hashCodeRandomizer = Environment.TickCount;
		}

		// Token: 0x06000291 RID: 657 RVA: 0x0000FE26 File Offset: 0x0000E026
		public bool Equals(string x, string y)
		{
			return string.Equals(x, y, StringComparison.Ordinal);
		}

		// Token: 0x06000292 RID: 658 RVA: 0x0000FE30 File Offset: 0x0000E030
		[SecuritySafeCritical]
		public int GetHashCode(string key)
		{
			if (SecureStringHasher.hashCodeDelegate == null)
			{
				SecureStringHasher.hashCodeDelegate = SecureStringHasher.GetHashCodeDelegate();
			}
			return SecureStringHasher.hashCodeDelegate(key, key.Length, (long)this.hashCodeRandomizer);
		}

		// Token: 0x06000293 RID: 659 RVA: 0x0000FE5C File Offset: 0x0000E05C
		[SecurityCritical]
		private static int GetHashCodeOfString(string key, int sLen, long additionalEntropy)
		{
			int num = (int)additionalEntropy;
			for (int i = 0; i < key.Length; i++)
			{
				num += (num << 7 ^ (int)key[i]);
			}
			num -= num >> 17;
			num -= num >> 11;
			return num - (num >> 5);
		}

		// Token: 0x06000294 RID: 660 RVA: 0x0000FEA0 File Offset: 0x0000E0A0
		[SecuritySafeCritical]
		[ReflectionPermission(SecurityAction.Assert, Unrestricted = true)]
		private static SecureStringHasher.HashCodeOfStringDelegate GetHashCodeDelegate()
		{
			MethodInfo method = typeof(string).GetMethod("InternalMarvin32HashString", BindingFlags.Static | BindingFlags.NonPublic);
			if (method != null)
			{
				return (SecureStringHasher.HashCodeOfStringDelegate)Delegate.CreateDelegate(typeof(SecureStringHasher.HashCodeOfStringDelegate), method);
			}
			return new SecureStringHasher.HashCodeOfStringDelegate(SecureStringHasher.GetHashCodeOfString);
		}

		// Token: 0x04000696 RID: 1686
		[SecurityCritical]
		private static SecureStringHasher.HashCodeOfStringDelegate hashCodeDelegate;

		// Token: 0x04000697 RID: 1687
		private int hashCodeRandomizer;

		// Token: 0x0200005B RID: 91
		// (Invoke) Token: 0x06000296 RID: 662
		[SecurityCritical]
		private delegate int HashCodeOfStringDelegate(string s, int sLen, long additionalEntropy);
	}
}
