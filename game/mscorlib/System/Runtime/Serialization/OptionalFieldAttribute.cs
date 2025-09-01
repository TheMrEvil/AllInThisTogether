using System;
using System.Runtime.InteropServices;

namespace System.Runtime.Serialization
{
	/// <summary>Specifies that a field can be missing from a serialization stream so that the <see cref="T:System.Runtime.Serialization.Formatters.Binary.BinaryFormatter" /> and the <see cref="T:System.Runtime.Serialization.Formatters.Soap.SoapFormatter" /> does not throw an exception.</summary>
	// Token: 0x0200066C RID: 1644
	[AttributeUsage(AttributeTargets.Field, Inherited = false)]
	[ComVisible(true)]
	public sealed class OptionalFieldAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.Serialization.OptionalFieldAttribute" /> class.</summary>
		// Token: 0x06003D64 RID: 15716 RVA: 0x000D4A40 File Offset: 0x000D2C40
		public OptionalFieldAttribute()
		{
		}

		/// <summary>Gets or sets a version number to indicate when the optional field was added.</summary>
		/// <returns>The version of the <see cref="T:System.Runtime.Serialization.OptionalFieldAttribute" />.</returns>
		// Token: 0x1700096E RID: 2414
		// (get) Token: 0x06003D65 RID: 15717 RVA: 0x000D4A4F File Offset: 0x000D2C4F
		// (set) Token: 0x06003D66 RID: 15718 RVA: 0x000D4A57 File Offset: 0x000D2C57
		public int VersionAdded
		{
			get
			{
				return this.versionAdded;
			}
			set
			{
				if (value < 1)
				{
					throw new ArgumentException(Environment.GetResourceString("Version value must be positive."));
				}
				this.versionAdded = value;
			}
		}

		// Token: 0x04002781 RID: 10113
		private int versionAdded = 1;
	}
}
