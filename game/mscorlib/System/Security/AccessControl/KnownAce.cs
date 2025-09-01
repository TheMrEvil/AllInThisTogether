using System;
using System.Globalization;
using System.Security.Principal;
using System.Text;
using Unity;

namespace System.Security.AccessControl
{
	/// <summary>Encapsulates all Access Control Entry (ACE) types currently defined by Microsoft Corporation. All <see cref="T:System.Security.AccessControl.KnownAce" /> objects contain a 32-bit access mask and a <see cref="T:System.Security.Principal.SecurityIdentifier" /> object.</summary>
	// Token: 0x02000530 RID: 1328
	public abstract class KnownAce : GenericAce
	{
		// Token: 0x0600349A RID: 13466 RVA: 0x000BF87A File Offset: 0x000BDA7A
		internal KnownAce(AceType type, AceFlags flags) : base(type, flags)
		{
		}

		// Token: 0x0600349B RID: 13467 RVA: 0x000BF884 File Offset: 0x000BDA84
		internal KnownAce(byte[] binaryForm, int offset) : base(binaryForm, offset)
		{
		}

		/// <summary>Gets or sets the access mask for this <see cref="T:System.Security.AccessControl.KnownAce" /> object.</summary>
		/// <returns>The access mask for this <see cref="T:System.Security.AccessControl.KnownAce" /> object.</returns>
		// Token: 0x17000739 RID: 1849
		// (get) Token: 0x0600349C RID: 13468 RVA: 0x000BF88E File Offset: 0x000BDA8E
		// (set) Token: 0x0600349D RID: 13469 RVA: 0x000BF896 File Offset: 0x000BDA96
		public int AccessMask
		{
			get
			{
				return this.access_mask;
			}
			set
			{
				this.access_mask = value;
			}
		}

		/// <summary>Gets or sets the <see cref="T:System.Security.Principal.SecurityIdentifier" /> object associated with this <see cref="T:System.Security.AccessControl.KnownAce" /> object.</summary>
		/// <returns>The <see cref="T:System.Security.Principal.SecurityIdentifier" /> object associated with this <see cref="T:System.Security.AccessControl.KnownAce" /> object.</returns>
		// Token: 0x1700073A RID: 1850
		// (get) Token: 0x0600349E RID: 13470 RVA: 0x000BF89F File Offset: 0x000BDA9F
		// (set) Token: 0x0600349F RID: 13471 RVA: 0x000BF8A7 File Offset: 0x000BDAA7
		public SecurityIdentifier SecurityIdentifier
		{
			get
			{
				return this.identifier;
			}
			set
			{
				this.identifier = value;
			}
		}

		// Token: 0x060034A0 RID: 13472 RVA: 0x000BF8B0 File Offset: 0x000BDAB0
		internal static string GetSddlAccessRights(int accessMask)
		{
			string sddlAliasRights = KnownAce.GetSddlAliasRights(accessMask);
			if (!string.IsNullOrEmpty(sddlAliasRights))
			{
				return sddlAliasRights;
			}
			return string.Format(CultureInfo.InvariantCulture, "0x{0:x}", accessMask);
		}

		// Token: 0x060034A1 RID: 13473 RVA: 0x000BF8E4 File Offset: 0x000BDAE4
		private static string GetSddlAliasRights(int accessMask)
		{
			SddlAccessRight[] array = SddlAccessRight.Decompose(accessMask);
			if (array == null)
			{
				return null;
			}
			StringBuilder stringBuilder = new StringBuilder();
			foreach (SddlAccessRight sddlAccessRight in array)
			{
				stringBuilder.Append(sddlAccessRight.Name);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x060034A2 RID: 13474 RVA: 0x000173AD File Offset: 0x000155AD
		internal KnownAce()
		{
			ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x040024AE RID: 9390
		private int access_mask;

		// Token: 0x040024AF RID: 9391
		private SecurityIdentifier identifier;
	}
}
