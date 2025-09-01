using System;
using System.Runtime.InteropServices;

namespace System.Security.Cryptography
{
	/// <summary>Represents the abstract base class from which all classes that derive byte sequences of a specified length inherit.</summary>
	// Token: 0x02000487 RID: 1159
	[ComVisible(true)]
	public abstract class DeriveBytes : IDisposable
	{
		/// <summary>When overridden in a derived class, returns pseudo-random key bytes.</summary>
		/// <param name="cb">The number of pseudo-random key bytes to generate.</param>
		/// <returns>A byte array filled with pseudo-random key bytes.</returns>
		// Token: 0x06002EB1 RID: 11953
		public abstract byte[] GetBytes(int cb);

		/// <summary>When overridden in a derived class, resets the state of the operation.</summary>
		// Token: 0x06002EB2 RID: 11954
		public abstract void Reset();

		/// <summary>When overridden in a derived class, releases all resources used by the current instance of the <see cref="T:System.Security.Cryptography.DeriveBytes" /> class.</summary>
		// Token: 0x06002EB3 RID: 11955 RVA: 0x000A6B03 File Offset: 0x000A4D03
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		/// <summary>When overridden in a derived class, releases the unmanaged resources used by the <see cref="T:System.Security.Cryptography.DeriveBytes" /> class and optionally releases the managed resources.</summary>
		/// <param name="disposing">
		///   <see langword="true" /> to release both managed and unmanaged resources; <see langword="false" /> to release only unmanaged resources.</param>
		// Token: 0x06002EB4 RID: 11956 RVA: 0x00004BF9 File Offset: 0x00002DF9
		protected virtual void Dispose(bool disposing)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.DeriveBytes" /> class.</summary>
		// Token: 0x06002EB5 RID: 11957 RVA: 0x0000259F File Offset: 0x0000079F
		protected DeriveBytes()
		{
		}
	}
}
