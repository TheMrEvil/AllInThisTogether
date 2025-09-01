using System;
using System.Runtime.CompilerServices;
using Unity;

namespace System.Security.Authentication.ExtendedProtection
{
	/// <summary>Contains APIs used for token binding.</summary>
	// Token: 0x020002A3 RID: 675
	public class TokenBinding
	{
		// Token: 0x06001528 RID: 5416 RVA: 0x00055A68 File Offset: 0x00053C68
		internal TokenBinding(TokenBindingType bindingType, byte[] rawData)
		{
			this.BindingType = bindingType;
			this._rawTokenBindingId = rawData;
		}

		/// <summary>Gets the raw token binding Id.</summary>
		/// <returns>The raw token binding Id. The first byte of the raw Id, which represents the binding type, is removed.</returns>
		// Token: 0x06001529 RID: 5417 RVA: 0x00055A7E File Offset: 0x00053C7E
		public byte[] GetRawTokenBindingId()
		{
			if (this._rawTokenBindingId == null)
			{
				return null;
			}
			return (byte[])this._rawTokenBindingId.Clone();
		}

		/// <summary>Gets the token binding type.</summary>
		/// <returns>The token binding type.</returns>
		// Token: 0x170003FA RID: 1018
		// (get) Token: 0x0600152A RID: 5418 RVA: 0x00055A9A File Offset: 0x00053C9A
		// (set) Token: 0x0600152B RID: 5419 RVA: 0x00055AA2 File Offset: 0x00053CA2
		public TokenBindingType BindingType
		{
			[CompilerGenerated]
			get
			{
				return this.<BindingType>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<BindingType>k__BackingField = value;
			}
		}

		// Token: 0x0600152C RID: 5420 RVA: 0x00013BCA File Offset: 0x00011DCA
		internal TokenBinding()
		{
			ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x04000BF3 RID: 3059
		private byte[] _rawTokenBindingId;

		// Token: 0x04000BF4 RID: 3060
		[CompilerGenerated]
		private TokenBindingType <BindingType>k__BackingField;
	}
}
