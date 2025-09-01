using System;
using System.Runtime.InteropServices;

namespace System.Security.Cryptography
{
	/// <summary>Determines the set of valid key sizes for the symmetric cryptographic algorithms.</summary>
	// Token: 0x02000482 RID: 1154
	[ComVisible(true)]
	public sealed class KeySizes
	{
		/// <summary>Specifies the minimum key size in bits.</summary>
		/// <returns>The minimum key size in bits.</returns>
		// Token: 0x17000619 RID: 1561
		// (get) Token: 0x06002E8F RID: 11919 RVA: 0x000A67BC File Offset: 0x000A49BC
		public int MinSize
		{
			get
			{
				return this.m_minSize;
			}
		}

		/// <summary>Specifies the maximum key size in bits.</summary>
		/// <returns>The maximum key size in bits.</returns>
		// Token: 0x1700061A RID: 1562
		// (get) Token: 0x06002E90 RID: 11920 RVA: 0x000A67C4 File Offset: 0x000A49C4
		public int MaxSize
		{
			get
			{
				return this.m_maxSize;
			}
		}

		/// <summary>Specifies the interval between valid key sizes in bits.</summary>
		/// <returns>The interval between valid key sizes in bits.</returns>
		// Token: 0x1700061B RID: 1563
		// (get) Token: 0x06002E91 RID: 11921 RVA: 0x000A67CC File Offset: 0x000A49CC
		public int SkipSize
		{
			get
			{
				return this.m_skipSize;
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.KeySizes" /> class with the specified key values.</summary>
		/// <param name="minSize">The minimum valid key size.</param>
		/// <param name="maxSize">The maximum valid key size.</param>
		/// <param name="skipSize">The interval between valid key sizes.</param>
		// Token: 0x06002E92 RID: 11922 RVA: 0x000A67D4 File Offset: 0x000A49D4
		public KeySizes(int minSize, int maxSize, int skipSize)
		{
			this.m_minSize = minSize;
			this.m_maxSize = maxSize;
			this.m_skipSize = skipSize;
		}

		// Token: 0x06002E93 RID: 11923 RVA: 0x000A67F4 File Offset: 0x000A49F4
		internal bool IsLegal(int keySize)
		{
			int num = keySize - this.MinSize;
			bool flag = num >= 0 && keySize <= this.MaxSize;
			if (this.SkipSize != 0)
			{
				return flag && num % this.SkipSize == 0;
			}
			return flag;
		}

		// Token: 0x06002E94 RID: 11924 RVA: 0x000A6838 File Offset: 0x000A4A38
		internal static bool IsLegalKeySize(KeySizes[] legalKeys, int size)
		{
			for (int i = 0; i < legalKeys.Length; i++)
			{
				if (legalKeys[i].IsLegal(size))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x0400213F RID: 8511
		private int m_minSize;

		// Token: 0x04002140 RID: 8512
		private int m_maxSize;

		// Token: 0x04002141 RID: 8513
		private int m_skipSize;
	}
}
