using System;
using System.Collections.Generic;
using System.Security.Claims;

namespace System.Security.Principal
{
	/// <summary>Represents a generic user.</summary>
	// Token: 0x020004DC RID: 1244
	[Serializable]
	public class GenericIdentity : ClaimsIdentity
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Principal.GenericIdentity" /> class representing the user with the specified name.</summary>
		/// <param name="name">The name of the user on whose behalf the code is running.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="name" /> parameter is <see langword="null" />.</exception>
		// Token: 0x060031DA RID: 12762 RVA: 0x000B79BC File Offset: 0x000B5BBC
		public GenericIdentity(string name)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			this.m_name = name;
			this.m_type = "";
			this.AddNameClaim();
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Principal.GenericIdentity" /> class representing the user with the specified name and authentication type.</summary>
		/// <param name="name">The name of the user on whose behalf the code is running.</param>
		/// <param name="type">The type of authentication used to identify the user.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="name" /> parameter is <see langword="null" />.  
		///  -or-  
		///  The <paramref name="type" /> parameter is <see langword="null" />.</exception>
		// Token: 0x060031DB RID: 12763 RVA: 0x000B79EA File Offset: 0x000B5BEA
		public GenericIdentity(string name, string type)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			this.m_name = name;
			this.m_type = type;
			this.AddNameClaim();
		}

		// Token: 0x060031DC RID: 12764 RVA: 0x000B7A22 File Offset: 0x000B5C22
		private GenericIdentity()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Principal.GenericIdentity" /> class by using the specified <see cref="T:System.Security.Principal.GenericIdentity" /> object.</summary>
		/// <param name="identity">The object from which to construct the new instance of <see cref="T:System.Security.Principal.GenericIdentity" />.</param>
		// Token: 0x060031DD RID: 12765 RVA: 0x000B7A2A File Offset: 0x000B5C2A
		protected GenericIdentity(GenericIdentity identity) : base(identity)
		{
			this.m_name = identity.m_name;
			this.m_type = identity.m_type;
		}

		/// <summary>Creates a new object that is a copy of the current instance.</summary>
		/// <returns>A copy of the current instance.</returns>
		// Token: 0x060031DE RID: 12766 RVA: 0x000B7A4B File Offset: 0x000B5C4B
		public override ClaimsIdentity Clone()
		{
			return new GenericIdentity(this);
		}

		/// <summary>Gets all claims for the user represented by this generic identity.</summary>
		/// <returns>A collection of claims for this <see cref="T:System.Security.Principal.GenericIdentity" /> object.</returns>
		// Token: 0x1700069B RID: 1691
		// (get) Token: 0x060031DF RID: 12767 RVA: 0x000B7A53 File Offset: 0x000B5C53
		public override IEnumerable<Claim> Claims
		{
			get
			{
				return base.Claims;
			}
		}

		/// <summary>Gets the user's name.</summary>
		/// <returns>The name of the user on whose behalf the code is being run.</returns>
		// Token: 0x1700069C RID: 1692
		// (get) Token: 0x060031E0 RID: 12768 RVA: 0x000B7A5B File Offset: 0x000B5C5B
		public override string Name
		{
			get
			{
				return this.m_name;
			}
		}

		/// <summary>Gets the type of authentication used to identify the user.</summary>
		/// <returns>The type of authentication used to identify the user.</returns>
		// Token: 0x1700069D RID: 1693
		// (get) Token: 0x060031E1 RID: 12769 RVA: 0x000B7A63 File Offset: 0x000B5C63
		public override string AuthenticationType
		{
			get
			{
				return this.m_type;
			}
		}

		/// <summary>Gets a value indicating whether the user has been authenticated.</summary>
		/// <returns>
		///   <see langword="true" /> if the user was has been authenticated; otherwise, <see langword="false" />.</returns>
		// Token: 0x1700069E RID: 1694
		// (get) Token: 0x060031E2 RID: 12770 RVA: 0x000B7A6B File Offset: 0x000B5C6B
		public override bool IsAuthenticated
		{
			get
			{
				return !this.m_name.Equals("");
			}
		}

		// Token: 0x060031E3 RID: 12771 RVA: 0x000B7A80 File Offset: 0x000B5C80
		private void AddNameClaim()
		{
			if (this.m_name != null)
			{
				base.AddClaim(new Claim(base.NameClaimType, this.m_name, "http://www.w3.org/2001/XMLSchema#string", "LOCAL AUTHORITY", "LOCAL AUTHORITY", this));
			}
		}

		// Token: 0x040022A3 RID: 8867
		private readonly string m_name;

		// Token: 0x040022A4 RID: 8868
		private readonly string m_type;
	}
}
