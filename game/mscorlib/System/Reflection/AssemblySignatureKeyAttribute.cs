using System;
using System.Runtime.CompilerServices;

namespace System.Reflection
{
	/// <summary>Provides migration from an older, simpler strong name key to a larger key with a stronger hashing algorithm.</summary>
	// Token: 0x0200088E RID: 2190
	[AttributeUsage(AttributeTargets.Assembly, Inherited = false, AllowMultiple = false)]
	public sealed class AssemblySignatureKeyAttribute : Attribute
	{
		/// <summary>Creates a new instance of the <see cref="T:System.Reflection.AssemblySignatureKeyAttribute" /> class by using the specified public key and countersignature.</summary>
		/// <param name="publicKey">The public or identity key.</param>
		/// <param name="countersignature">The countersignature, which is the signature key portion of the strong-name key.</param>
		// Token: 0x06004871 RID: 18545 RVA: 0x000EE14B File Offset: 0x000EC34B
		public AssemblySignatureKeyAttribute(string publicKey, string countersignature)
		{
			this.PublicKey = publicKey;
			this.Countersignature = countersignature;
		}

		/// <summary>Gets the public key for the strong name used to sign the assembly.</summary>
		/// <returns>The public key for this assembly.</returns>
		// Token: 0x17000B31 RID: 2865
		// (get) Token: 0x06004872 RID: 18546 RVA: 0x000EE161 File Offset: 0x000EC361
		public string PublicKey
		{
			[CompilerGenerated]
			get
			{
				return this.<PublicKey>k__BackingField;
			}
		}

		/// <summary>Gets the countersignature for the strong name for this assembly.</summary>
		/// <returns>The countersignature for this signature key.</returns>
		// Token: 0x17000B32 RID: 2866
		// (get) Token: 0x06004873 RID: 18547 RVA: 0x000EE169 File Offset: 0x000EC369
		public string Countersignature
		{
			[CompilerGenerated]
			get
			{
				return this.<Countersignature>k__BackingField;
			}
		}

		// Token: 0x04002E62 RID: 11874
		[CompilerGenerated]
		private readonly string <PublicKey>k__BackingField;

		// Token: 0x04002E63 RID: 11875
		[CompilerGenerated]
		private readonly string <Countersignature>k__BackingField;
	}
}
