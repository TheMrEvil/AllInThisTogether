using System;
using System.Runtime.InteropServices;

namespace System.Configuration.Assemblies
{
	/// <summary>Represents a hash of an assembly manifest's contents.</summary>
	// Token: 0x02000A0E RID: 2574
	[ComVisible(true)]
	[Obsolete]
	[Serializable]
	public struct AssemblyHash : ICloneable
	{
		/// <summary>Gets or sets the hash algorithm.</summary>
		/// <returns>An assembly hash algorithm.</returns>
		// Token: 0x17000FBB RID: 4027
		// (get) Token: 0x06005B66 RID: 23398 RVA: 0x001349EB File Offset: 0x00132BEB
		// (set) Token: 0x06005B67 RID: 23399 RVA: 0x001349F3 File Offset: 0x00132BF3
		[Obsolete]
		public AssemblyHashAlgorithm Algorithm
		{
			get
			{
				return this._algorithm;
			}
			set
			{
				this._algorithm = value;
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Configuration.Assemblies.AssemblyHash" /> structure with the specified hash algorithm and the hash value.</summary>
		/// <param name="algorithm">The algorithm used to generate the hash. Values for this parameter come from the <see cref="T:System.Configuration.Assemblies.AssemblyHashAlgorithm" /> enumeration.</param>
		/// <param name="value">The hash value.</param>
		// Token: 0x06005B68 RID: 23400 RVA: 0x001349FC File Offset: 0x00132BFC
		[Obsolete]
		public AssemblyHash(AssemblyHashAlgorithm algorithm, byte[] value)
		{
			this._algorithm = algorithm;
			if (value != null)
			{
				this._value = (byte[])value.Clone();
				return;
			}
			this._value = null;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Configuration.Assemblies.AssemblyHash" /> structure with the specified hash value. The hash algorithm defaults to <see cref="F:System.Configuration.Assemblies.AssemblyHashAlgorithm.SHA1" />.</summary>
		/// <param name="value">The hash value.</param>
		// Token: 0x06005B69 RID: 23401 RVA: 0x00134A21 File Offset: 0x00132C21
		[Obsolete]
		public AssemblyHash(byte[] value)
		{
			this = new AssemblyHash(AssemblyHashAlgorithm.SHA1, value);
		}

		/// <summary>Clones this object.</summary>
		/// <returns>An exact copy of this object.</returns>
		// Token: 0x06005B6A RID: 23402 RVA: 0x00134A2F File Offset: 0x00132C2F
		[Obsolete]
		public object Clone()
		{
			return new AssemblyHash(this._algorithm, this._value);
		}

		/// <summary>Gets the hash value.</summary>
		/// <returns>The hash value.</returns>
		// Token: 0x06005B6B RID: 23403 RVA: 0x00134A47 File Offset: 0x00132C47
		[Obsolete]
		public byte[] GetValue()
		{
			return this._value;
		}

		/// <summary>Sets the hash value.</summary>
		/// <param name="value">The hash value.</param>
		// Token: 0x06005B6C RID: 23404 RVA: 0x00134A4F File Offset: 0x00132C4F
		[Obsolete]
		public void SetValue(byte[] value)
		{
			this._value = value;
		}

		// Token: 0x06005B6D RID: 23405 RVA: 0x00134A58 File Offset: 0x00132C58
		// Note: this type is marked as 'beforefieldinit'.
		static AssemblyHash()
		{
		}

		// Token: 0x04003861 RID: 14433
		private AssemblyHashAlgorithm _algorithm;

		// Token: 0x04003862 RID: 14434
		private byte[] _value;

		/// <summary>An empty <see cref="T:System.Configuration.Assemblies.AssemblyHash" /> object.</summary>
		// Token: 0x04003863 RID: 14435
		[Obsolete]
		public static readonly AssemblyHash Empty = new AssemblyHash(AssemblyHashAlgorithm.None, null);
	}
}
