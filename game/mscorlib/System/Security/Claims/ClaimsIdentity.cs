using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Permissions;
using System.Security.Principal;

namespace System.Security.Claims
{
	/// <summary>Represents a claims-based identity.</summary>
	// Token: 0x020004F3 RID: 1267
	[ComVisible(true)]
	[Serializable]
	public class ClaimsIdentity : IIdentity
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Claims.ClaimsIdentity" /> class with an empty claims collection.</summary>
		// Token: 0x0600329D RID: 12957 RVA: 0x000BA1E9 File Offset: 0x000B83E9
		public ClaimsIdentity() : this(null)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Claims.ClaimsIdentity" /> class using the name and authentication type from the specified <see cref="T:System.Security.Principal.IIdentity" />.</summary>
		/// <param name="identity">The identity from which to base the new claims identity.</param>
		// Token: 0x0600329E RID: 12958 RVA: 0x000BA1F2 File Offset: 0x000B83F2
		public ClaimsIdentity(IIdentity identity) : this(identity, null)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Claims.ClaimsIdentity" /> class using an enumerated collection of <see cref="T:System.Security.Claims.Claim" /> objects.</summary>
		/// <param name="claims">The claims with which to populate the claims identity.</param>
		// Token: 0x0600329F RID: 12959 RVA: 0x000BA1FC File Offset: 0x000B83FC
		public ClaimsIdentity(IEnumerable<Claim> claims) : this(null, claims, null, null, null)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Claims.ClaimsIdentity" /> class with an empty claims collection and the specified authentication type.</summary>
		/// <param name="authenticationType">The type of authentication used.</param>
		// Token: 0x060032A0 RID: 12960 RVA: 0x000BA209 File Offset: 0x000B8409
		public ClaimsIdentity(string authenticationType) : this(null, null, authenticationType, null, null)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Claims.ClaimsIdentity" /> class with the specified claims and authentication type.</summary>
		/// <param name="claims">The claims with which to populate the claims identity.</param>
		/// <param name="authenticationType">The type of authentication used.</param>
		// Token: 0x060032A1 RID: 12961 RVA: 0x000BA216 File Offset: 0x000B8416
		public ClaimsIdentity(IEnumerable<Claim> claims, string authenticationType) : this(null, claims, authenticationType, null, null)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Claims.ClaimsIdentity" /> class using the specified claims and the specified <see cref="T:System.Security.Principal.IIdentity" />.</summary>
		/// <param name="identity">The identity from which to base the new claims identity.</param>
		/// <param name="claims">The claims with which to populate the claims identity.</param>
		// Token: 0x060032A2 RID: 12962 RVA: 0x000BA223 File Offset: 0x000B8423
		public ClaimsIdentity(IIdentity identity, IEnumerable<Claim> claims) : this(identity, claims, null, null, null)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Claims.ClaimsIdentity" /> class with the specified authentication type, name claim type, and role claim type.</summary>
		/// <param name="authenticationType">The type of authentication used.</param>
		/// <param name="nameType">The claim type to use for name claims.</param>
		/// <param name="roleType">The claim type to use for role claims.</param>
		// Token: 0x060032A3 RID: 12963 RVA: 0x000BA230 File Offset: 0x000B8430
		public ClaimsIdentity(string authenticationType, string nameType, string roleType) : this(null, null, authenticationType, nameType, roleType)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Claims.ClaimsIdentity" /> class with the specified claims, authentication type, name claim type, and role claim type.</summary>
		/// <param name="claims">The claims with which to populate the claims identity.</param>
		/// <param name="authenticationType">The type of authentication used.</param>
		/// <param name="nameType">The claim type to use for name claims.</param>
		/// <param name="roleType">The claim type to use for role claims.</param>
		// Token: 0x060032A4 RID: 12964 RVA: 0x000BA23D File Offset: 0x000B843D
		public ClaimsIdentity(IEnumerable<Claim> claims, string authenticationType, string nameType, string roleType) : this(null, claims, authenticationType, nameType, roleType)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Claims.ClaimsIdentity" /> class from the specified <see cref="T:System.Security.Principal.IIdentity" /> using the specified claims, authentication type, name claim type, and role claim type.</summary>
		/// <param name="identity">The identity from which to base the new claims identity.</param>
		/// <param name="claims">The claims with which to populate the new claims identity.</param>
		/// <param name="authenticationType">The type of authentication used.</param>
		/// <param name="nameType">The claim type to use for name claims.</param>
		/// <param name="roleType">The claim type to use for role claims.</param>
		// Token: 0x060032A5 RID: 12965 RVA: 0x000BA24B File Offset: 0x000B844B
		public ClaimsIdentity(IIdentity identity, IEnumerable<Claim> claims, string authenticationType, string nameType, string roleType) : this(identity, claims, authenticationType, nameType, roleType, true)
		{
		}

		// Token: 0x060032A6 RID: 12966 RVA: 0x000BA25C File Offset: 0x000B845C
		internal ClaimsIdentity(IIdentity identity, IEnumerable<Claim> claims, string authenticationType, string nameType, string roleType, bool checkAuthType)
		{
			this.m_instanceClaims = new List<Claim>();
			this.m_externalClaims = new Collection<IEnumerable<Claim>>();
			this.m_nameType = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name";
			this.m_roleType = "http://schemas.microsoft.com/ws/2008/06/identity/claims/role";
			this.m_version = "1.0";
			base..ctor();
			bool flag = false;
			bool flag2 = false;
			if (checkAuthType && identity != null && string.IsNullOrEmpty(authenticationType))
			{
				if (identity is WindowsIdentity)
				{
					try
					{
						this.m_authenticationType = identity.AuthenticationType;
						goto IL_85;
					}
					catch (UnauthorizedAccessException)
					{
						this.m_authenticationType = null;
						goto IL_85;
					}
				}
				this.m_authenticationType = identity.AuthenticationType;
			}
			else
			{
				this.m_authenticationType = authenticationType;
			}
			IL_85:
			if (!string.IsNullOrEmpty(nameType))
			{
				this.m_nameType = nameType;
				flag = true;
			}
			if (!string.IsNullOrEmpty(roleType))
			{
				this.m_roleType = roleType;
				flag2 = true;
			}
			ClaimsIdentity claimsIdentity = identity as ClaimsIdentity;
			if (claimsIdentity != null)
			{
				this.m_label = claimsIdentity.m_label;
				if (!flag)
				{
					this.m_nameType = claimsIdentity.m_nameType;
				}
				if (!flag2)
				{
					this.m_roleType = claimsIdentity.m_roleType;
				}
				this.m_bootstrapContext = claimsIdentity.m_bootstrapContext;
				if (claimsIdentity.Actor != null)
				{
					if (this.IsCircular(claimsIdentity.Actor))
					{
						throw new InvalidOperationException(Environment.GetResourceString("Actor cannot be set so that circular directed graph will exist chaining the subjects together."));
					}
					if (!AppContextSwitches.SetActorAsReferenceWhenCopyingClaimsIdentity)
					{
						this.m_actor = claimsIdentity.Actor.Clone();
					}
					else
					{
						this.m_actor = claimsIdentity.Actor;
					}
				}
				if (claimsIdentity is WindowsIdentity && !(this is WindowsIdentity))
				{
					this.SafeAddClaims(claimsIdentity.Claims);
				}
				else
				{
					this.SafeAddClaims(claimsIdentity.m_instanceClaims);
				}
				if (claimsIdentity.m_userSerializationData != null)
				{
					this.m_userSerializationData = (claimsIdentity.m_userSerializationData.Clone() as byte[]);
				}
			}
			else if (identity != null && !string.IsNullOrEmpty(identity.Name))
			{
				this.SafeAddClaim(new Claim(this.m_nameType, identity.Name, "http://www.w3.org/2001/XMLSchema#string", "LOCAL AUTHORITY", "LOCAL AUTHORITY", this));
			}
			if (claims != null)
			{
				this.SafeAddClaims(claims);
			}
		}

		/// <summary>Initializes an instance of <see cref="T:System.Security.Claims.ClaimsIdentity" /> with the specified <see cref="T:System.IO.BinaryReader" />.</summary>
		/// <param name="reader">A <see cref="T:System.IO.BinaryReader" /> pointing to a <see cref="T:System.Security.Claims.ClaimsIdentity" />.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="reader" /> is <see langword="null" />.</exception>
		// Token: 0x060032A7 RID: 12967 RVA: 0x000BA440 File Offset: 0x000B8640
		public ClaimsIdentity(BinaryReader reader)
		{
			this.m_instanceClaims = new List<Claim>();
			this.m_externalClaims = new Collection<IEnumerable<Claim>>();
			this.m_nameType = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name";
			this.m_roleType = "http://schemas.microsoft.com/ws/2008/06/identity/claims/role";
			this.m_version = "1.0";
			base..ctor();
			if (reader == null)
			{
				throw new ArgumentNullException("reader");
			}
			this.Initialize(reader);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Claims.ClaimsIdentity" /> class from an existing <see cref="T:System.Security.Claims.ClaimsIdentity" /> instance.</summary>
		/// <param name="other">The <see cref="T:System.Security.Claims.ClaimsIdentity" /> to copy.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="other" /> is null.</exception>
		// Token: 0x060032A8 RID: 12968 RVA: 0x000BA4A0 File Offset: 0x000B86A0
		protected ClaimsIdentity(ClaimsIdentity other)
		{
			this.m_instanceClaims = new List<Claim>();
			this.m_externalClaims = new Collection<IEnumerable<Claim>>();
			this.m_nameType = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name";
			this.m_roleType = "http://schemas.microsoft.com/ws/2008/06/identity/claims/role";
			this.m_version = "1.0";
			base..ctor();
			if (other == null)
			{
				throw new ArgumentNullException("other");
			}
			if (other.m_actor != null)
			{
				this.m_actor = other.m_actor.Clone();
			}
			this.m_authenticationType = other.m_authenticationType;
			this.m_bootstrapContext = other.m_bootstrapContext;
			this.m_label = other.m_label;
			this.m_nameType = other.m_nameType;
			this.m_roleType = other.m_roleType;
			if (other.m_userSerializationData != null)
			{
				this.m_userSerializationData = (other.m_userSerializationData.Clone() as byte[]);
			}
			this.SafeAddClaims(other.m_instanceClaims);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Claims.ClaimsIdentity" /> class from a serialized stream created by using <see cref="T:System.Runtime.Serialization.ISerializable" />.</summary>
		/// <param name="info">The serialized data.</param>
		/// <param name="context">The context for serialization.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="info" /> is null.</exception>
		// Token: 0x060032A9 RID: 12969 RVA: 0x000BA578 File Offset: 0x000B8778
		[SecurityCritical]
		protected ClaimsIdentity(SerializationInfo info, StreamingContext context)
		{
			this.m_instanceClaims = new List<Claim>();
			this.m_externalClaims = new Collection<IEnumerable<Claim>>();
			this.m_nameType = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name";
			this.m_roleType = "http://schemas.microsoft.com/ws/2008/06/identity/claims/role";
			this.m_version = "1.0";
			base..ctor();
			if (info == null)
			{
				throw new ArgumentNullException("info");
			}
			this.Deserialize(info, context, true);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Claims.ClaimsIdentity" /> class from a serialized stream created by using <see cref="T:System.Runtime.Serialization.ISerializable" />.</summary>
		/// <param name="info">The serialized data.</param>
		// Token: 0x060032AA RID: 12970 RVA: 0x000BA5DC File Offset: 0x000B87DC
		[SecurityCritical]
		protected ClaimsIdentity(SerializationInfo info)
		{
			this.m_instanceClaims = new List<Claim>();
			this.m_externalClaims = new Collection<IEnumerable<Claim>>();
			this.m_nameType = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name";
			this.m_roleType = "http://schemas.microsoft.com/ws/2008/06/identity/claims/role";
			this.m_version = "1.0";
			base..ctor();
			if (info == null)
			{
				throw new ArgumentNullException("info");
			}
			this.Deserialize(info, default(StreamingContext), false);
		}

		/// <summary>Gets the authentication type.</summary>
		/// <returns>The authentication type.</returns>
		// Token: 0x170006CE RID: 1742
		// (get) Token: 0x060032AB RID: 12971 RVA: 0x000BA645 File Offset: 0x000B8845
		public virtual string AuthenticationType
		{
			get
			{
				return this.m_authenticationType;
			}
		}

		/// <summary>Gets a value that indicates whether the identity has been authenticated.</summary>
		/// <returns>
		///   <see langword="true" /> if the identity has been authenticated; otherwise, <see langword="false" />.</returns>
		// Token: 0x170006CF RID: 1743
		// (get) Token: 0x060032AC RID: 12972 RVA: 0x000BA64D File Offset: 0x000B884D
		public virtual bool IsAuthenticated
		{
			get
			{
				return !string.IsNullOrEmpty(this.m_authenticationType);
			}
		}

		/// <summary>Gets or sets the identity of the calling party that was granted delegation rights.</summary>
		/// <returns>The calling party that was granted delegation rights.</returns>
		/// <exception cref="T:System.InvalidOperationException">An attempt to set the property to the current instance occurs.</exception>
		// Token: 0x170006D0 RID: 1744
		// (get) Token: 0x060032AD RID: 12973 RVA: 0x000BA65D File Offset: 0x000B885D
		// (set) Token: 0x060032AE RID: 12974 RVA: 0x000BA665 File Offset: 0x000B8865
		public ClaimsIdentity Actor
		{
			get
			{
				return this.m_actor;
			}
			set
			{
				if (value != null && this.IsCircular(value))
				{
					throw new InvalidOperationException(Environment.GetResourceString("Actor cannot be set so that circular directed graph will exist chaining the subjects together."));
				}
				this.m_actor = value;
			}
		}

		/// <summary>Gets or sets the token that was used to create this claims identity.</summary>
		/// <returns>The bootstrap context.</returns>
		// Token: 0x170006D1 RID: 1745
		// (get) Token: 0x060032AF RID: 12975 RVA: 0x000BA68A File Offset: 0x000B888A
		// (set) Token: 0x060032B0 RID: 12976 RVA: 0x000BA692 File Offset: 0x000B8892
		public object BootstrapContext
		{
			get
			{
				return this.m_bootstrapContext;
			}
			[SecurityCritical]
			set
			{
				this.m_bootstrapContext = value;
			}
		}

		/// <summary>Gets the claims associated with this claims identity.</summary>
		/// <returns>The collection of claims associated with this claims identity.</returns>
		// Token: 0x170006D2 RID: 1746
		// (get) Token: 0x060032B1 RID: 12977 RVA: 0x000BA69B File Offset: 0x000B889B
		public virtual IEnumerable<Claim> Claims
		{
			get
			{
				int num;
				for (int i = 0; i < this.m_instanceClaims.Count; i = num + 1)
				{
					yield return this.m_instanceClaims[i];
					num = i;
				}
				if (this.m_externalClaims != null)
				{
					for (int i = 0; i < this.m_externalClaims.Count; i = num + 1)
					{
						if (this.m_externalClaims[i] != null)
						{
							foreach (Claim claim in this.m_externalClaims[i])
							{
								yield return claim;
							}
							IEnumerator<Claim> enumerator = null;
						}
						num = i;
					}
				}
				yield break;
				yield break;
			}
		}

		/// <summary>Contains any additional data provided by a derived type. Typically set when calling <see cref="M:System.Security.Claims.ClaimsIdentity.WriteTo(System.IO.BinaryWriter,System.Byte[])" />.</summary>
		/// <returns>A <see cref="T:System.Byte" /> array representing the additional serialized data.</returns>
		// Token: 0x170006D3 RID: 1747
		// (get) Token: 0x060032B2 RID: 12978 RVA: 0x000BA6AB File Offset: 0x000B88AB
		protected virtual byte[] CustomSerializationData
		{
			get
			{
				return this.m_userSerializationData;
			}
		}

		// Token: 0x170006D4 RID: 1748
		// (get) Token: 0x060032B3 RID: 12979 RVA: 0x000BA6B3 File Offset: 0x000B88B3
		internal Collection<IEnumerable<Claim>> ExternalClaims
		{
			[FriendAccessAllowed]
			get
			{
				return this.m_externalClaims;
			}
		}

		/// <summary>Gets or sets the label for this claims identity.</summary>
		/// <returns>The label.</returns>
		// Token: 0x170006D5 RID: 1749
		// (get) Token: 0x060032B4 RID: 12980 RVA: 0x000BA6BB File Offset: 0x000B88BB
		// (set) Token: 0x060032B5 RID: 12981 RVA: 0x000BA6C3 File Offset: 0x000B88C3
		public string Label
		{
			get
			{
				return this.m_label;
			}
			set
			{
				this.m_label = value;
			}
		}

		/// <summary>Gets the name of this claims identity.</summary>
		/// <returns>The name or <see langword="null" />.</returns>
		// Token: 0x170006D6 RID: 1750
		// (get) Token: 0x060032B6 RID: 12982 RVA: 0x000BA6CC File Offset: 0x000B88CC
		public virtual string Name
		{
			get
			{
				Claim claim = this.FindFirst(this.m_nameType);
				if (claim != null)
				{
					return claim.Value;
				}
				return null;
			}
		}

		/// <summary>Gets the claim type that is used to determine which claims provide the value for the <see cref="P:System.Security.Claims.ClaimsIdentity.Name" /> property of this claims identity.</summary>
		/// <returns>The name claim type.</returns>
		// Token: 0x170006D7 RID: 1751
		// (get) Token: 0x060032B7 RID: 12983 RVA: 0x000BA6F1 File Offset: 0x000B88F1
		public string NameClaimType
		{
			get
			{
				return this.m_nameType;
			}
		}

		/// <summary>Gets the claim type that will be interpreted as a .NET Framework role among the claims in this claims identity.</summary>
		/// <returns>The role claim type.</returns>
		// Token: 0x170006D8 RID: 1752
		// (get) Token: 0x060032B8 RID: 12984 RVA: 0x000BA6F9 File Offset: 0x000B88F9
		public string RoleClaimType
		{
			get
			{
				return this.m_roleType;
			}
		}

		/// <summary>Returns a new <see cref="T:System.Security.Claims.ClaimsIdentity" /> copied from this claims identity.</summary>
		/// <returns>A copy of the current instance.</returns>
		// Token: 0x060032B9 RID: 12985 RVA: 0x000BA704 File Offset: 0x000B8904
		public virtual ClaimsIdentity Clone()
		{
			ClaimsIdentity claimsIdentity = new ClaimsIdentity(this.m_instanceClaims);
			claimsIdentity.m_authenticationType = this.m_authenticationType;
			claimsIdentity.m_bootstrapContext = this.m_bootstrapContext;
			claimsIdentity.m_label = this.m_label;
			claimsIdentity.m_nameType = this.m_nameType;
			claimsIdentity.m_roleType = this.m_roleType;
			if (this.Actor != null)
			{
				if (this.IsCircular(this.Actor))
				{
					throw new InvalidOperationException(Environment.GetResourceString("Actor cannot be set so that circular directed graph will exist chaining the subjects together."));
				}
				if (!AppContextSwitches.SetActorAsReferenceWhenCopyingClaimsIdentity)
				{
					claimsIdentity.Actor = this.Actor.Clone();
				}
				else
				{
					claimsIdentity.Actor = this.Actor;
				}
			}
			return claimsIdentity;
		}

		/// <summary>Adds a single claim to this claims identity.</summary>
		/// <param name="claim">The claim to add.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="claim" /> is <see langword="null" />.</exception>
		// Token: 0x060032BA RID: 12986 RVA: 0x000BA7A8 File Offset: 0x000B89A8
		[SecurityCritical]
		public virtual void AddClaim(Claim claim)
		{
			if (claim == null)
			{
				throw new ArgumentNullException("claim");
			}
			if (claim.Subject == this)
			{
				this.m_instanceClaims.Add(claim);
				return;
			}
			this.m_instanceClaims.Add(claim.Clone(this));
		}

		/// <summary>Adds a list of claims to this claims identity.</summary>
		/// <param name="claims">The claims to add.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="claims" /> is <see langword="null" />.</exception>
		// Token: 0x060032BB RID: 12987 RVA: 0x000BA7E0 File Offset: 0x000B89E0
		[SecurityCritical]
		public virtual void AddClaims(IEnumerable<Claim> claims)
		{
			if (claims == null)
			{
				throw new ArgumentNullException("claims");
			}
			foreach (Claim claim in claims)
			{
				if (claim != null)
				{
					this.AddClaim(claim);
				}
			}
		}

		/// <summary>Attempts to remove a claim from the claims identity.</summary>
		/// <param name="claim">The claim to remove.</param>
		/// <returns>
		///   <see langword="true" /> if the claim was successfully removed; otherwise, <see langword="false" />.</returns>
		// Token: 0x060032BC RID: 12988 RVA: 0x000BA83C File Offset: 0x000B8A3C
		[SecurityCritical]
		public virtual bool TryRemoveClaim(Claim claim)
		{
			bool result = false;
			for (int i = 0; i < this.m_instanceClaims.Count; i++)
			{
				if (this.m_instanceClaims[i] == claim)
				{
					this.m_instanceClaims.RemoveAt(i);
					result = true;
					break;
				}
			}
			return result;
		}

		/// <summary>Attempts to remove a claim from the claims identity.</summary>
		/// <param name="claim">The claim to remove.</param>
		/// <exception cref="T:System.InvalidOperationException">The claim cannot be removed.</exception>
		// Token: 0x060032BD RID: 12989 RVA: 0x000BA881 File Offset: 0x000B8A81
		[SecurityCritical]
		public virtual void RemoveClaim(Claim claim)
		{
			if (!this.TryRemoveClaim(claim))
			{
				throw new InvalidOperationException(Environment.GetResourceString("The Claim '{0}' was not able to be removed.  It is either not part of this Identity or it is a claim that is owned by the Principal that contains this Identity. For example, the Principal will own the claim when creating a GenericPrincipal with roles. The roles will be exposed through the Identity that is passed in the constructor, but not actually owned by the Identity.  Similar logic exists for a RolePrincipal.", new object[]
				{
					claim
				}));
			}
		}

		// Token: 0x060032BE RID: 12990 RVA: 0x000BA8A8 File Offset: 0x000B8AA8
		[SecuritySafeCritical]
		private void SafeAddClaims(IEnumerable<Claim> claims)
		{
			foreach (Claim claim in claims)
			{
				if (claim.Subject == this)
				{
					this.m_instanceClaims.Add(claim);
				}
				else
				{
					this.m_instanceClaims.Add(claim.Clone(this));
				}
			}
		}

		// Token: 0x060032BF RID: 12991 RVA: 0x000BA914 File Offset: 0x000B8B14
		[SecuritySafeCritical]
		private void SafeAddClaim(Claim claim)
		{
			if (claim.Subject == this)
			{
				this.m_instanceClaims.Add(claim);
				return;
			}
			this.m_instanceClaims.Add(claim.Clone(this));
		}

		/// <summary>Retrieves all of the claims that are matched by the specified predicate.</summary>
		/// <param name="match">The function that performs the matching logic.</param>
		/// <returns>The matching claims. The list is read-only.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="match" /> is null.</exception>
		// Token: 0x060032C0 RID: 12992 RVA: 0x000BA940 File Offset: 0x000B8B40
		public virtual IEnumerable<Claim> FindAll(Predicate<Claim> match)
		{
			if (match == null)
			{
				throw new ArgumentNullException("match");
			}
			List<Claim> list = new List<Claim>();
			foreach (Claim claim in this.Claims)
			{
				if (match(claim))
				{
					list.Add(claim);
				}
			}
			return list.AsReadOnly();
		}

		/// <summary>Retrieves all of the claims that have the specified claim type.</summary>
		/// <param name="type">The claim type against which to match claims.</param>
		/// <returns>The matching claims. The list is read-only.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="type" /> is null.</exception>
		// Token: 0x060032C1 RID: 12993 RVA: 0x000BA9B0 File Offset: 0x000B8BB0
		public virtual IEnumerable<Claim> FindAll(string type)
		{
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			List<Claim> list = new List<Claim>();
			foreach (Claim claim in this.Claims)
			{
				if (claim != null && string.Equals(claim.Type, type, StringComparison.OrdinalIgnoreCase))
				{
					list.Add(claim);
				}
			}
			return list.AsReadOnly();
		}

		/// <summary>Determines whether this claims identity has a claim that is matched by the specified predicate.</summary>
		/// <param name="match">The function that performs the matching logic.</param>
		/// <returns>
		///   <see langword="true" /> if a matching claim exists; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="match" /> is null.</exception>
		// Token: 0x060032C2 RID: 12994 RVA: 0x000BAA2C File Offset: 0x000B8C2C
		public virtual bool HasClaim(Predicate<Claim> match)
		{
			if (match == null)
			{
				throw new ArgumentNullException("match");
			}
			foreach (Claim obj in this.Claims)
			{
				if (match(obj))
				{
					return true;
				}
			}
			return false;
		}

		/// <summary>Determines whether this claims identity has a claim with the specified claim type and value.</summary>
		/// <param name="type">The type of the claim to match.</param>
		/// <param name="value">The value of the claim to match.</param>
		/// <returns>
		///   <see langword="true" /> if a match is found; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="type" /> is null.  
		/// -or-  
		/// <paramref name="value" /> is null.</exception>
		// Token: 0x060032C3 RID: 12995 RVA: 0x000BAA90 File Offset: 0x000B8C90
		public virtual bool HasClaim(string type, string value)
		{
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			foreach (Claim claim in this.Claims)
			{
				if (claim != null && claim != null && string.Equals(claim.Type, type, StringComparison.OrdinalIgnoreCase) && string.Equals(claim.Value, value, StringComparison.Ordinal))
				{
					return true;
				}
			}
			return false;
		}

		/// <summary>Retrieves the first claim that is matched by the specified predicate.</summary>
		/// <param name="match">The function that performs the matching logic.</param>
		/// <returns>The first matching claim or <see langword="null" /> if no match is found.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="match" /> is null.</exception>
		// Token: 0x060032C4 RID: 12996 RVA: 0x000BAB20 File Offset: 0x000B8D20
		public virtual Claim FindFirst(Predicate<Claim> match)
		{
			if (match == null)
			{
				throw new ArgumentNullException("match");
			}
			foreach (Claim claim in this.Claims)
			{
				if (match(claim))
				{
					return claim;
				}
			}
			return null;
		}

		/// <summary>Retrieves the first claim with the specified claim type.</summary>
		/// <param name="type">The claim type to match.</param>
		/// <returns>The first matching claim or <see langword="null" /> if no match is found.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="type" /> is null.</exception>
		// Token: 0x060032C5 RID: 12997 RVA: 0x000BAB84 File Offset: 0x000B8D84
		public virtual Claim FindFirst(string type)
		{
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			foreach (Claim claim in this.Claims)
			{
				if (claim != null && string.Equals(claim.Type, type, StringComparison.OrdinalIgnoreCase))
				{
					return claim;
				}
			}
			return null;
		}

		// Token: 0x060032C6 RID: 12998 RVA: 0x000BABF4 File Offset: 0x000B8DF4
		[OnSerializing]
		[SecurityCritical]
		private void OnSerializingMethod(StreamingContext context)
		{
			if (this is ISerializable)
			{
				return;
			}
			this.m_serializedClaims = this.SerializeClaims();
			this.m_serializedNameType = this.m_nameType;
			this.m_serializedRoleType = this.m_roleType;
		}

		// Token: 0x060032C7 RID: 12999 RVA: 0x000BAC24 File Offset: 0x000B8E24
		[OnDeserialized]
		[SecurityCritical]
		private void OnDeserializedMethod(StreamingContext context)
		{
			if (this is ISerializable)
			{
				return;
			}
			if (!string.IsNullOrEmpty(this.m_serializedClaims))
			{
				this.DeserializeClaims(this.m_serializedClaims);
				this.m_serializedClaims = null;
			}
			this.m_nameType = (string.IsNullOrEmpty(this.m_serializedNameType) ? "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name" : this.m_serializedNameType);
			this.m_roleType = (string.IsNullOrEmpty(this.m_serializedRoleType) ? "http://schemas.microsoft.com/ws/2008/06/identity/claims/role" : this.m_serializedRoleType);
		}

		// Token: 0x060032C8 RID: 13000 RVA: 0x000BAC9A File Offset: 0x000B8E9A
		[OnDeserializing]
		private void OnDeserializingMethod(StreamingContext context)
		{
			if (this is ISerializable)
			{
				return;
			}
			this.m_instanceClaims = new List<Claim>();
			this.m_externalClaims = new Collection<IEnumerable<Claim>>();
		}

		/// <summary>Populates the <see cref="T:System.Runtime.Serialization.SerializationInfo" /> with data needed to serialize the current <see cref="T:System.Security.Claims.ClaimsIdentity" /> object.</summary>
		/// <param name="info">The object to populate with data.</param>
		/// <param name="context">The destination for this serialization. Can be <see langword="null" />.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="info" /> is <see langword="null" />.</exception>
		// Token: 0x060032C9 RID: 13001 RVA: 0x000BACBC File Offset: 0x000B8EBC
		[SecurityCritical]
		[SecurityPermission(SecurityAction.Assert, SerializationFormatter = true)]
		protected virtual void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			if (info == null)
			{
				throw new ArgumentNullException("info");
			}
			BinaryFormatter binaryFormatter = new BinaryFormatter();
			info.AddValue("System.Security.ClaimsIdentity.version", this.m_version);
			if (!string.IsNullOrEmpty(this.m_authenticationType))
			{
				info.AddValue("System.Security.ClaimsIdentity.authenticationType", this.m_authenticationType);
			}
			info.AddValue("System.Security.ClaimsIdentity.nameClaimType", this.m_nameType);
			info.AddValue("System.Security.ClaimsIdentity.roleClaimType", this.m_roleType);
			if (!string.IsNullOrEmpty(this.m_label))
			{
				info.AddValue("System.Security.ClaimsIdentity.label", this.m_label);
			}
			if (this.m_actor != null)
			{
				using (MemoryStream memoryStream = new MemoryStream())
				{
					binaryFormatter.Serialize(memoryStream, this.m_actor, null, false);
					info.AddValue("System.Security.ClaimsIdentity.actor", Convert.ToBase64String(memoryStream.GetBuffer(), 0, (int)memoryStream.Length));
				}
			}
			info.AddValue("System.Security.ClaimsIdentity.claims", this.SerializeClaims());
			if (this.m_bootstrapContext != null)
			{
				using (MemoryStream memoryStream2 = new MemoryStream())
				{
					binaryFormatter.Serialize(memoryStream2, this.m_bootstrapContext, null, false);
					info.AddValue("System.Security.ClaimsIdentity.bootstrapContext", Convert.ToBase64String(memoryStream2.GetBuffer(), 0, (int)memoryStream2.Length));
				}
			}
		}

		// Token: 0x060032CA RID: 13002 RVA: 0x000BAE08 File Offset: 0x000B9008
		[SecurityCritical]
		private void DeserializeClaims(string serializedClaims)
		{
			if (!string.IsNullOrEmpty(serializedClaims))
			{
				using (MemoryStream memoryStream = new MemoryStream(Convert.FromBase64String(serializedClaims)))
				{
					this.m_instanceClaims = (List<Claim>)new BinaryFormatter().Deserialize(memoryStream, null, false);
					for (int i = 0; i < this.m_instanceClaims.Count; i++)
					{
						this.m_instanceClaims[i].Subject = this;
					}
				}
			}
			if (this.m_instanceClaims == null)
			{
				this.m_instanceClaims = new List<Claim>();
			}
		}

		// Token: 0x060032CB RID: 13003 RVA: 0x000BAE98 File Offset: 0x000B9098
		[SecurityCritical]
		private string SerializeClaims()
		{
			string result;
			using (MemoryStream memoryStream = new MemoryStream())
			{
				new BinaryFormatter().Serialize(memoryStream, this.m_instanceClaims, null, false);
				result = Convert.ToBase64String(memoryStream.GetBuffer(), 0, (int)memoryStream.Length);
			}
			return result;
		}

		// Token: 0x060032CC RID: 13004 RVA: 0x000BAEF0 File Offset: 0x000B90F0
		private bool IsCircular(ClaimsIdentity subject)
		{
			if (this == subject)
			{
				return true;
			}
			ClaimsIdentity claimsIdentity = subject;
			while (claimsIdentity.Actor != null)
			{
				if (this == claimsIdentity.Actor)
				{
					return true;
				}
				claimsIdentity = claimsIdentity.Actor;
			}
			return false;
		}

		// Token: 0x060032CD RID: 13005 RVA: 0x000BAF24 File Offset: 0x000B9124
		private void Initialize(BinaryReader reader)
		{
			if (reader == null)
			{
				throw new ArgumentNullException("reader");
			}
			int num = reader.ReadInt32();
			if ((num & 1) == 1)
			{
				this.m_authenticationType = reader.ReadString();
			}
			if ((num & 2) == 2)
			{
				this.m_bootstrapContext = reader.ReadString();
			}
			if ((num & 4) == 4)
			{
				this.m_nameType = reader.ReadString();
			}
			else
			{
				this.m_nameType = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name";
			}
			if ((num & 8) == 8)
			{
				this.m_roleType = reader.ReadString();
			}
			else
			{
				this.m_roleType = "http://schemas.microsoft.com/ws/2008/06/identity/claims/role";
			}
			if ((num & 16) == 16)
			{
				int num2 = reader.ReadInt32();
				for (int i = 0; i < num2; i++)
				{
					Claim item = new Claim(reader, this);
					this.m_instanceClaims.Add(item);
				}
			}
		}

		/// <summary>Provides an extensibility point for derived types to create a custom <see cref="T:System.Security.Claims.Claim" />.</summary>
		/// <param name="reader">The <see cref="T:System.IO.BinaryReader" /> that points to the claim.</param>
		/// <returns>A new <see cref="T:System.Security.Claims.Claim" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="reader" /> is <see langword="null" />.</exception>
		// Token: 0x060032CE RID: 13006 RVA: 0x000BAFD5 File Offset: 0x000B91D5
		protected virtual Claim CreateClaim(BinaryReader reader)
		{
			if (reader == null)
			{
				throw new ArgumentNullException("reader");
			}
			return new Claim(reader, this);
		}

		/// <summary>Serializes using a <see cref="T:System.IO.BinaryWriter" />.</summary>
		/// <param name="writer">The writer to use for data storage.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="writer" /> is <see langword="null" />.</exception>
		// Token: 0x060032CF RID: 13007 RVA: 0x000BAFEC File Offset: 0x000B91EC
		public virtual void WriteTo(BinaryWriter writer)
		{
			this.WriteTo(writer, null);
		}

		/// <summary>Serializes using a <see cref="T:System.IO.BinaryWriter" />.</summary>
		/// <param name="writer">The writer to use for data storage.</param>
		/// <param name="userData">Additional data provided by the derived type.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="writer" /> is <see langword="null" />.</exception>
		// Token: 0x060032D0 RID: 13008 RVA: 0x000BAFF8 File Offset: 0x000B91F8
		protected virtual void WriteTo(BinaryWriter writer, byte[] userData)
		{
			if (writer == null)
			{
				throw new ArgumentNullException("writer");
			}
			int num = 0;
			ClaimsIdentity.SerializationMask serializationMask = ClaimsIdentity.SerializationMask.None;
			if (this.m_authenticationType != null)
			{
				serializationMask |= ClaimsIdentity.SerializationMask.AuthenticationType;
				num++;
			}
			if (this.m_bootstrapContext != null && this.m_bootstrapContext is string)
			{
				serializationMask |= ClaimsIdentity.SerializationMask.BootstrapConext;
				num++;
			}
			if (!string.Equals(this.m_nameType, "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name", StringComparison.Ordinal))
			{
				serializationMask |= ClaimsIdentity.SerializationMask.NameClaimType;
				num++;
			}
			if (!string.Equals(this.m_roleType, "http://schemas.microsoft.com/ws/2008/06/identity/claims/role", StringComparison.Ordinal))
			{
				serializationMask |= ClaimsIdentity.SerializationMask.RoleClaimType;
				num++;
			}
			if (!string.IsNullOrWhiteSpace(this.m_label))
			{
				serializationMask |= ClaimsIdentity.SerializationMask.HasLabel;
				num++;
			}
			if (this.m_instanceClaims.Count > 0)
			{
				serializationMask |= ClaimsIdentity.SerializationMask.HasClaims;
				num++;
			}
			if (this.m_actor != null)
			{
				serializationMask |= ClaimsIdentity.SerializationMask.Actor;
				num++;
			}
			if (userData != null && userData.Length != 0)
			{
				num++;
				serializationMask |= ClaimsIdentity.SerializationMask.UserData;
			}
			writer.Write((int)serializationMask);
			writer.Write(num);
			if ((serializationMask & ClaimsIdentity.SerializationMask.AuthenticationType) == ClaimsIdentity.SerializationMask.AuthenticationType)
			{
				writer.Write(this.m_authenticationType);
			}
			if ((serializationMask & ClaimsIdentity.SerializationMask.BootstrapConext) == ClaimsIdentity.SerializationMask.BootstrapConext)
			{
				writer.Write(this.m_bootstrapContext as string);
			}
			if ((serializationMask & ClaimsIdentity.SerializationMask.NameClaimType) == ClaimsIdentity.SerializationMask.NameClaimType)
			{
				writer.Write(this.m_nameType);
			}
			if ((serializationMask & ClaimsIdentity.SerializationMask.RoleClaimType) == ClaimsIdentity.SerializationMask.RoleClaimType)
			{
				writer.Write(this.m_roleType);
			}
			if ((serializationMask & ClaimsIdentity.SerializationMask.HasLabel) == ClaimsIdentity.SerializationMask.HasLabel)
			{
				writer.Write(this.m_label);
			}
			if ((serializationMask & ClaimsIdentity.SerializationMask.HasClaims) == ClaimsIdentity.SerializationMask.HasClaims)
			{
				writer.Write(this.m_instanceClaims.Count);
				foreach (Claim claim in this.m_instanceClaims)
				{
					claim.WriteTo(writer);
				}
			}
			if ((serializationMask & ClaimsIdentity.SerializationMask.Actor) == ClaimsIdentity.SerializationMask.Actor)
			{
				this.m_actor.WriteTo(writer);
			}
			if ((serializationMask & ClaimsIdentity.SerializationMask.UserData) == ClaimsIdentity.SerializationMask.UserData)
			{
				writer.Write(userData.Length);
				writer.Write(userData);
			}
			writer.Flush();
		}

		// Token: 0x060032D1 RID: 13009 RVA: 0x000BB1D0 File Offset: 0x000B93D0
		[SecurityCritical]
		[SecurityPermission(SecurityAction.Assert, SerializationFormatter = true)]
		private void Deserialize(SerializationInfo info, StreamingContext context, bool useContext)
		{
			if (info == null)
			{
				throw new ArgumentNullException("info");
			}
			BinaryFormatter binaryFormatter;
			if (useContext)
			{
				binaryFormatter = new BinaryFormatter(null, context);
			}
			else
			{
				binaryFormatter = new BinaryFormatter();
			}
			SerializationInfoEnumerator enumerator = info.GetEnumerator();
			while (enumerator.MoveNext())
			{
				string name = enumerator.Name;
				uint num = <PrivateImplementationDetails>.ComputeStringHash(name);
				if (num <= 959168042U)
				{
					if (num <= 623923795U)
					{
						if (num != 373632733U)
						{
							if (num == 623923795U)
							{
								if (name == "System.Security.ClaimsIdentity.roleClaimType")
								{
									this.m_roleType = info.GetString("System.Security.ClaimsIdentity.roleClaimType");
								}
							}
						}
						else if (name == "System.Security.ClaimsIdentity.label")
						{
							this.m_label = info.GetString("System.Security.ClaimsIdentity.label");
						}
					}
					else if (num != 656336169U)
					{
						if (num == 959168042U)
						{
							if (name == "System.Security.ClaimsIdentity.nameClaimType")
							{
								this.m_nameType = info.GetString("System.Security.ClaimsIdentity.nameClaimType");
							}
						}
					}
					else if (name == "System.Security.ClaimsIdentity.authenticationType")
					{
						this.m_authenticationType = info.GetString("System.Security.ClaimsIdentity.authenticationType");
					}
				}
				else if (num <= 1476368026U)
				{
					if (num != 1453716852U)
					{
						if (num != 1476368026U)
						{
							continue;
						}
						if (!(name == "System.Security.ClaimsIdentity.actor"))
						{
							continue;
						}
						using (MemoryStream memoryStream = new MemoryStream(Convert.FromBase64String(info.GetString("System.Security.ClaimsIdentity.actor"))))
						{
							this.m_actor = (ClaimsIdentity)binaryFormatter.Deserialize(memoryStream, null, false);
							continue;
						}
					}
					else if (!(name == "System.Security.ClaimsIdentity.claims"))
					{
						continue;
					}
					this.DeserializeClaims(info.GetString("System.Security.ClaimsIdentity.claims"));
				}
				else if (num != 2480284791U)
				{
					if (num == 3659022112U)
					{
						if (name == "System.Security.ClaimsIdentity.bootstrapContext")
						{
							using (MemoryStream memoryStream2 = new MemoryStream(Convert.FromBase64String(info.GetString("System.Security.ClaimsIdentity.bootstrapContext"))))
							{
								this.m_bootstrapContext = binaryFormatter.Deserialize(memoryStream2, null, false);
							}
						}
					}
				}
				else if (name == "System.Security.ClaimsIdentity.version")
				{
					info.GetString("System.Security.ClaimsIdentity.version");
				}
			}
		}

		// Token: 0x040023B4 RID: 9140
		[NonSerialized]
		private byte[] m_userSerializationData;

		// Token: 0x040023B5 RID: 9141
		[NonSerialized]
		private const string PreFix = "System.Security.ClaimsIdentity.";

		// Token: 0x040023B6 RID: 9142
		[NonSerialized]
		private const string ActorKey = "System.Security.ClaimsIdentity.actor";

		// Token: 0x040023B7 RID: 9143
		[NonSerialized]
		private const string AuthenticationTypeKey = "System.Security.ClaimsIdentity.authenticationType";

		// Token: 0x040023B8 RID: 9144
		[NonSerialized]
		private const string BootstrapContextKey = "System.Security.ClaimsIdentity.bootstrapContext";

		// Token: 0x040023B9 RID: 9145
		[NonSerialized]
		private const string ClaimsKey = "System.Security.ClaimsIdentity.claims";

		// Token: 0x040023BA RID: 9146
		[NonSerialized]
		private const string LabelKey = "System.Security.ClaimsIdentity.label";

		// Token: 0x040023BB RID: 9147
		[NonSerialized]
		private const string NameClaimTypeKey = "System.Security.ClaimsIdentity.nameClaimType";

		// Token: 0x040023BC RID: 9148
		[NonSerialized]
		private const string RoleClaimTypeKey = "System.Security.ClaimsIdentity.roleClaimType";

		// Token: 0x040023BD RID: 9149
		[NonSerialized]
		private const string VersionKey = "System.Security.ClaimsIdentity.version";

		/// <summary>The default issuer; "LOCAL AUTHORITY".</summary>
		// Token: 0x040023BE RID: 9150
		[NonSerialized]
		public const string DefaultIssuer = "LOCAL AUTHORITY";

		/// <summary>The default name claim type; <see cref="F:System.Security.Claims.ClaimTypes.Name" />.</summary>
		// Token: 0x040023BF RID: 9151
		[NonSerialized]
		public const string DefaultNameClaimType = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name";

		/// <summary>The default role claim type; <see cref="F:System.Security.Claims.ClaimTypes.Role" />.</summary>
		// Token: 0x040023C0 RID: 9152
		[NonSerialized]
		public const string DefaultRoleClaimType = "http://schemas.microsoft.com/ws/2008/06/identity/claims/role";

		// Token: 0x040023C1 RID: 9153
		[NonSerialized]
		private List<Claim> m_instanceClaims;

		// Token: 0x040023C2 RID: 9154
		[NonSerialized]
		private Collection<IEnumerable<Claim>> m_externalClaims;

		// Token: 0x040023C3 RID: 9155
		[NonSerialized]
		private string m_nameType;

		// Token: 0x040023C4 RID: 9156
		[NonSerialized]
		private string m_roleType;

		// Token: 0x040023C5 RID: 9157
		[OptionalField(VersionAdded = 2)]
		private string m_version;

		// Token: 0x040023C6 RID: 9158
		[OptionalField(VersionAdded = 2)]
		private ClaimsIdentity m_actor;

		// Token: 0x040023C7 RID: 9159
		[OptionalField(VersionAdded = 2)]
		private string m_authenticationType;

		// Token: 0x040023C8 RID: 9160
		[OptionalField(VersionAdded = 2)]
		private object m_bootstrapContext;

		// Token: 0x040023C9 RID: 9161
		[OptionalField(VersionAdded = 2)]
		private string m_label;

		// Token: 0x040023CA RID: 9162
		[OptionalField(VersionAdded = 2)]
		private string m_serializedNameType;

		// Token: 0x040023CB RID: 9163
		[OptionalField(VersionAdded = 2)]
		private string m_serializedRoleType;

		// Token: 0x040023CC RID: 9164
		[OptionalField(VersionAdded = 2)]
		private string m_serializedClaims;

		// Token: 0x020004F4 RID: 1268
		private enum SerializationMask
		{
			// Token: 0x040023CE RID: 9166
			None,
			// Token: 0x040023CF RID: 9167
			AuthenticationType,
			// Token: 0x040023D0 RID: 9168
			BootstrapConext,
			// Token: 0x040023D1 RID: 9169
			NameClaimType = 4,
			// Token: 0x040023D2 RID: 9170
			RoleClaimType = 8,
			// Token: 0x040023D3 RID: 9171
			HasClaims = 16,
			// Token: 0x040023D4 RID: 9172
			HasLabel = 32,
			// Token: 0x040023D5 RID: 9173
			Actor = 64,
			// Token: 0x040023D6 RID: 9174
			UserData = 128
		}

		// Token: 0x020004F5 RID: 1269
		[CompilerGenerated]
		private sealed class <get_Claims>d__51 : IEnumerable<Claim>, IEnumerable, IEnumerator<Claim>, IDisposable, IEnumerator
		{
			// Token: 0x060032D2 RID: 13010 RVA: 0x000BB450 File Offset: 0x000B9650
			[DebuggerHidden]
			public <get_Claims>d__51(int <>1__state)
			{
				this.<>1__state = <>1__state;
				this.<>l__initialThreadId = Environment.CurrentManagedThreadId;
			}

			// Token: 0x060032D3 RID: 13011 RVA: 0x000BB46C File Offset: 0x000B966C
			[DebuggerHidden]
			void IDisposable.Dispose()
			{
				int num = this.<>1__state;
				if (num == -3 || num == 2)
				{
					try
					{
					}
					finally
					{
						this.<>m__Finally1();
					}
				}
			}

			// Token: 0x060032D4 RID: 13012 RVA: 0x000BB4A4 File Offset: 0x000B96A4
			bool IEnumerator.MoveNext()
			{
				bool result;
				try
				{
					int num = this.<>1__state;
					ClaimsIdentity claimsIdentity = this;
					int num2;
					switch (num)
					{
					case 0:
						this.<>1__state = -1;
						i = 0;
						break;
					case 1:
						this.<>1__state = -1;
						num2 = i;
						i = num2 + 1;
						break;
					case 2:
						this.<>1__state = -3;
						goto IL_FE;
					default:
						return false;
					}
					if (i < claimsIdentity.m_instanceClaims.Count)
					{
						this.<>2__current = claimsIdentity.m_instanceClaims[i];
						this.<>1__state = 1;
						return true;
					}
					if (claimsIdentity.m_externalClaims != null)
					{
						i = 0;
						goto IL_128;
					}
					goto IL_13E;
					IL_FE:
					if (enumerator.MoveNext())
					{
						Claim claim = enumerator.Current;
						this.<>2__current = claim;
						this.<>1__state = 2;
						return true;
					}
					this.<>m__Finally1();
					enumerator = null;
					IL_118:
					num2 = i;
					i = num2 + 1;
					IL_128:
					if (i < claimsIdentity.m_externalClaims.Count)
					{
						if (claimsIdentity.m_externalClaims[i] != null)
						{
							enumerator = claimsIdentity.m_externalClaims[i].GetEnumerator();
							this.<>1__state = -3;
							goto IL_FE;
						}
						goto IL_118;
					}
					IL_13E:
					result = false;
				}
				catch
				{
					this.System.IDisposable.Dispose();
					throw;
				}
				return result;
			}

			// Token: 0x060032D5 RID: 13013 RVA: 0x000BB618 File Offset: 0x000B9818
			private void <>m__Finally1()
			{
				this.<>1__state = -1;
				if (enumerator != null)
				{
					enumerator.Dispose();
				}
			}

			// Token: 0x170006D9 RID: 1753
			// (get) Token: 0x060032D6 RID: 13014 RVA: 0x000BB634 File Offset: 0x000B9834
			Claim IEnumerator<Claim>.Current
			{
				[DebuggerHidden]
				get
				{
					return this.<>2__current;
				}
			}

			// Token: 0x060032D7 RID: 13015 RVA: 0x000472C8 File Offset: 0x000454C8
			[DebuggerHidden]
			void IEnumerator.Reset()
			{
				throw new NotSupportedException();
			}

			// Token: 0x170006DA RID: 1754
			// (get) Token: 0x060032D8 RID: 13016 RVA: 0x000BB634 File Offset: 0x000B9834
			object IEnumerator.Current
			{
				[DebuggerHidden]
				get
				{
					return this.<>2__current;
				}
			}

			// Token: 0x060032D9 RID: 13017 RVA: 0x000BB63C File Offset: 0x000B983C
			[DebuggerHidden]
			IEnumerator<Claim> IEnumerable<Claim>.GetEnumerator()
			{
				ClaimsIdentity.<get_Claims>d__51 <get_Claims>d__;
				if (this.<>1__state == -2 && this.<>l__initialThreadId == Environment.CurrentManagedThreadId)
				{
					this.<>1__state = 0;
					<get_Claims>d__ = this;
				}
				else
				{
					<get_Claims>d__ = new ClaimsIdentity.<get_Claims>d__51(0);
					<get_Claims>d__.<>4__this = this;
				}
				return <get_Claims>d__;
			}

			// Token: 0x060032DA RID: 13018 RVA: 0x000BB67F File Offset: 0x000B987F
			[DebuggerHidden]
			IEnumerator IEnumerable.GetEnumerator()
			{
				return this.System.Collections.Generic.IEnumerable<System.Security.Claims.Claim>.GetEnumerator();
			}

			// Token: 0x040023D7 RID: 9175
			private int <>1__state;

			// Token: 0x040023D8 RID: 9176
			private Claim <>2__current;

			// Token: 0x040023D9 RID: 9177
			private int <>l__initialThreadId;

			// Token: 0x040023DA RID: 9178
			public ClaimsIdentity <>4__this;

			// Token: 0x040023DB RID: 9179
			private int <i>5__2;

			// Token: 0x040023DC RID: 9180
			private IEnumerator<Claim> <>7__wrap2;
		}
	}
}
