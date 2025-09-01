using System;
using System.Runtime.Serialization;

namespace System.Reflection
{
	/// <summary>Represents a missing <see cref="T:System.Object" />. This class cannot be inherited.</summary>
	// Token: 0x020008B0 RID: 2224
	public sealed class Missing : ISerializable
	{
		// Token: 0x06004970 RID: 18800 RVA: 0x0000259F File Offset: 0x0000079F
		private Missing()
		{
		}

		/// <summary>Sets a <see cref="T:System.Runtime.Serialization.SerializationInfo" /> object with the logical context information needed to recreate the sole instance of the <see cref="T:System.Reflection.Missing" /> object.</summary>
		/// <param name="info">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> object to be populated with serialization information.</param>
		/// <param name="context">The <see cref="T:System.Runtime.Serialization.StreamingContext" /> object representing the destination context of the serialization.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="info" /> is <see langword="null" />.</exception>
		// Token: 0x06004971 RID: 18801 RVA: 0x0001B98B File Offset: 0x00019B8B
		void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06004972 RID: 18802 RVA: 0x000EEEA2 File Offset: 0x000ED0A2
		// Note: this type is marked as 'beforefieldinit'.
		static Missing()
		{
		}

		/// <summary>Represents the sole instance of the <see cref="T:System.Reflection.Missing" /> class.</summary>
		// Token: 0x04002EEF RID: 12015
		public static readonly Missing Value = new Missing();
	}
}
