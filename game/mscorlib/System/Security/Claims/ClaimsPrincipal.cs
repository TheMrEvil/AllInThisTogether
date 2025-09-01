using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Permissions;
using System.Security.Principal;
using System.Threading;

namespace System.Security.Claims
{
	/// <summary>An <see cref="T:System.Security.Principal.IPrincipal" /> implementation that supports multiple claims-based identities.</summary>
	// Token: 0x020004F6 RID: 1270
	[ComVisible(true)]
	[Serializable]
	public class ClaimsPrincipal : IPrincipal
	{
		// Token: 0x060032DB RID: 13019 RVA: 0x000BB688 File Offset: 0x000B9888
		private static ClaimsIdentity SelectPrimaryIdentity(IEnumerable<ClaimsIdentity> identities)
		{
			if (identities == null)
			{
				throw new ArgumentNullException("identities");
			}
			ClaimsIdentity claimsIdentity = null;
			foreach (ClaimsIdentity claimsIdentity2 in identities)
			{
				if (claimsIdentity2 is WindowsIdentity)
				{
					claimsIdentity = claimsIdentity2;
					break;
				}
				if (claimsIdentity == null)
				{
					claimsIdentity = claimsIdentity2;
				}
			}
			return claimsIdentity;
		}

		// Token: 0x060032DC RID: 13020 RVA: 0x000BB6EC File Offset: 0x000B98EC
		private static ClaimsPrincipal SelectClaimsPrincipal()
		{
			ClaimsPrincipal claimsPrincipal = Thread.CurrentPrincipal as ClaimsPrincipal;
			if (claimsPrincipal != null)
			{
				return claimsPrincipal;
			}
			return new ClaimsPrincipal(Thread.CurrentPrincipal);
		}

		/// <summary>Gets or sets the delegate used to select the claims identity returned by the <see cref="P:System.Security.Claims.ClaimsPrincipal.Identity" /> property.</summary>
		/// <returns>The delegate. The default is <see langword="null" />.</returns>
		// Token: 0x170006DB RID: 1755
		// (get) Token: 0x060032DD RID: 13021 RVA: 0x000BB713 File Offset: 0x000B9913
		// (set) Token: 0x060032DE RID: 13022 RVA: 0x000BB71A File Offset: 0x000B991A
		public static Func<IEnumerable<ClaimsIdentity>, ClaimsIdentity> PrimaryIdentitySelector
		{
			get
			{
				return ClaimsPrincipal.s_identitySelector;
			}
			[SecurityCritical]
			set
			{
				ClaimsPrincipal.s_identitySelector = value;
			}
		}

		/// <summary>Gets or sets the delegate used to select the claims principal returned by the <see cref="P:System.Security.Claims.ClaimsPrincipal.Current" /> property.</summary>
		/// <returns>The delegate. The default is <see langword="null" />.</returns>
		// Token: 0x170006DC RID: 1756
		// (get) Token: 0x060032DF RID: 13023 RVA: 0x000BB722 File Offset: 0x000B9922
		// (set) Token: 0x060032E0 RID: 13024 RVA: 0x000BB729 File Offset: 0x000B9929
		public static Func<ClaimsPrincipal> ClaimsPrincipalSelector
		{
			get
			{
				return ClaimsPrincipal.s_principalSelector;
			}
			[SecurityCritical]
			set
			{
				ClaimsPrincipal.s_principalSelector = value;
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Claims.ClaimsPrincipal" /> class.</summary>
		// Token: 0x060032E1 RID: 13025 RVA: 0x000BB731 File Offset: 0x000B9931
		public ClaimsPrincipal()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Claims.ClaimsPrincipal" /> class using the specified claims identities.</summary>
		/// <param name="identities">The identities from which to initialize the new claims principal.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="identities" /> is null.</exception>
		// Token: 0x060032E2 RID: 13026 RVA: 0x000BB74F File Offset: 0x000B994F
		public ClaimsPrincipal(IEnumerable<ClaimsIdentity> identities)
		{
			if (identities == null)
			{
				throw new ArgumentNullException("identities");
			}
			this.m_identities.AddRange(identities);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Claims.ClaimsPrincipal" /> class from the specified identity.</summary>
		/// <param name="identity">The identity from which to initialize the new claims principal.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="identity" /> is null.</exception>
		// Token: 0x060032E3 RID: 13027 RVA: 0x000BB788 File Offset: 0x000B9988
		public ClaimsPrincipal(IIdentity identity)
		{
			if (identity == null)
			{
				throw new ArgumentNullException("identity");
			}
			ClaimsIdentity claimsIdentity = identity as ClaimsIdentity;
			if (claimsIdentity != null)
			{
				this.m_identities.Add(claimsIdentity);
				return;
			}
			this.m_identities.Add(new ClaimsIdentity(identity));
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Claims.ClaimsPrincipal" /> class from the specified principal.</summary>
		/// <param name="principal">The principal from which to initialize the new claims principal.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="principal" /> is null.</exception>
		// Token: 0x060032E4 RID: 13028 RVA: 0x000BB7E8 File Offset: 0x000B99E8
		public ClaimsPrincipal(IPrincipal principal)
		{
			if (principal == null)
			{
				throw new ArgumentNullException("principal");
			}
			ClaimsPrincipal claimsPrincipal = principal as ClaimsPrincipal;
			if (claimsPrincipal == null)
			{
				this.m_identities.Add(new ClaimsIdentity(principal.Identity));
				return;
			}
			if (claimsPrincipal.Identities != null)
			{
				this.m_identities.AddRange(claimsPrincipal.Identities);
			}
		}

		/// <summary>Initializes an instance of <see cref="T:System.Security.Claims.ClaimsPrincipal" /> with the specified <see cref="T:System.IO.BinaryReader" />.</summary>
		/// <param name="reader">A <see cref="T:System.IO.BinaryReader" /> pointing to a <see cref="T:System.Security.Claims.ClaimsPrincipal" />.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="reader" /> is <see langword="null" />.</exception>
		// Token: 0x060032E5 RID: 13029 RVA: 0x000BB859 File Offset: 0x000B9A59
		public ClaimsPrincipal(BinaryReader reader)
		{
			if (reader == null)
			{
				throw new ArgumentNullException("reader");
			}
			this.Initialize(reader);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Claims.ClaimsPrincipal" /> class from a serialized stream created by using <see cref="T:System.Runtime.Serialization.ISerializable" />.</summary>
		/// <param name="info">The serialized data.</param>
		/// <param name="context">The context for serialization.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="info" /> is null.</exception>
		// Token: 0x060032E6 RID: 13030 RVA: 0x000BB88C File Offset: 0x000B9A8C
		[SecurityCritical]
		protected ClaimsPrincipal(SerializationInfo info, StreamingContext context)
		{
			if (info == null)
			{
				throw new ArgumentNullException("info");
			}
			this.Deserialize(info, context);
		}

		/// <summary>Contains any additional data provided by a derived type. Typically set when calling <see cref="M:System.Security.Claims.ClaimsIdentity.WriteTo(System.IO.BinaryWriter,System.Byte[])" />.</summary>
		/// <returns>A <see cref="T:System.Byte" /> array representing the additional serialized data.</returns>
		// Token: 0x170006DD RID: 1757
		// (get) Token: 0x060032E7 RID: 13031 RVA: 0x000BB8C0 File Offset: 0x000B9AC0
		protected virtual byte[] CustomSerializationData
		{
			get
			{
				return this.m_userSerializationData;
			}
		}

		/// <summary>Returns a copy of this instance.</summary>
		/// <returns>A new copy of the <see cref="T:System.Security.Claims.ClaimsPrincipal" /> object.</returns>
		// Token: 0x060032E8 RID: 13032 RVA: 0x000BB8C8 File Offset: 0x000B9AC8
		public virtual ClaimsPrincipal Clone()
		{
			return new ClaimsPrincipal(this);
		}

		/// <summary>Creates a new claims identity.</summary>
		/// <param name="reader">The binary reader.</param>
		/// <returns>The created claims identity.</returns>
		// Token: 0x060032E9 RID: 13033 RVA: 0x000BB8D0 File Offset: 0x000B9AD0
		protected virtual ClaimsIdentity CreateClaimsIdentity(BinaryReader reader)
		{
			if (reader == null)
			{
				throw new ArgumentNullException("reader");
			}
			return new ClaimsIdentity(reader);
		}

		// Token: 0x060032EA RID: 13034 RVA: 0x000BB8E6 File Offset: 0x000B9AE6
		[SecurityCritical]
		[OnSerializing]
		private void OnSerializingMethod(StreamingContext context)
		{
			if (this is ISerializable)
			{
				return;
			}
			this.m_serializedClaimsIdentities = this.SerializeIdentities();
		}

		// Token: 0x060032EB RID: 13035 RVA: 0x000BB8FD File Offset: 0x000B9AFD
		[OnDeserialized]
		[SecurityCritical]
		private void OnDeserializedMethod(StreamingContext context)
		{
			if (this is ISerializable)
			{
				return;
			}
			this.DeserializeIdentities(this.m_serializedClaimsIdentities);
			this.m_serializedClaimsIdentities = null;
		}

		/// <summary>Populates the <see cref="T:System.Runtime.Serialization.SerializationInfo" /> with data needed to serialize the current <see cref="T:System.Security.Claims.ClaimsPrincipal" /> object.</summary>
		/// <param name="info">The object to populate with data.</param>
		/// <param name="context">The destination for this serialization. Can be <see langword="null" />.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="info" /> is <see langword="null" />.</exception>
		// Token: 0x060032EC RID: 13036 RVA: 0x000BB91B File Offset: 0x000B9B1B
		[SecurityCritical]
		[SecurityPermission(SecurityAction.Assert, SerializationFormatter = true)]
		protected virtual void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			if (info == null)
			{
				throw new ArgumentNullException("info");
			}
			info.AddValue("System.Security.ClaimsPrincipal.Identities", this.SerializeIdentities());
			info.AddValue("System.Security.ClaimsPrincipal.Version", this.m_version);
		}

		// Token: 0x060032ED RID: 13037 RVA: 0x000BB950 File Offset: 0x000B9B50
		[SecurityCritical]
		[SecurityPermission(SecurityAction.Assert, SerializationFormatter = true)]
		private void Deserialize(SerializationInfo info, StreamingContext context)
		{
			if (info == null)
			{
				throw new ArgumentNullException("info");
			}
			SerializationInfoEnumerator enumerator = info.GetEnumerator();
			while (enumerator.MoveNext())
			{
				string name = enumerator.Name;
				if (!(name == "System.Security.ClaimsPrincipal.Identities"))
				{
					if (name == "System.Security.ClaimsPrincipal.Version")
					{
						this.m_version = info.GetString("System.Security.ClaimsPrincipal.Version");
					}
				}
				else
				{
					this.DeserializeIdentities(info.GetString("System.Security.ClaimsPrincipal.Identities"));
				}
			}
		}

		// Token: 0x060032EE RID: 13038 RVA: 0x000BB9C4 File Offset: 0x000B9BC4
		[SecurityCritical]
		private void DeserializeIdentities(string identities)
		{
			this.m_identities = new List<ClaimsIdentity>();
			if (!string.IsNullOrEmpty(identities))
			{
				List<string> list = null;
				BinaryFormatter binaryFormatter = new BinaryFormatter();
				using (MemoryStream memoryStream = new MemoryStream(Convert.FromBase64String(identities)))
				{
					list = (List<string>)binaryFormatter.Deserialize(memoryStream, null, false);
					for (int i = 0; i < list.Count; i += 2)
					{
						ClaimsIdentity claimsIdentity = null;
						using (MemoryStream memoryStream2 = new MemoryStream(Convert.FromBase64String(list[i + 1])))
						{
							claimsIdentity = (ClaimsIdentity)binaryFormatter.Deserialize(memoryStream2, null, false);
						}
						if (!string.IsNullOrEmpty(list[i]))
						{
							long value;
							if (!long.TryParse(list[i], NumberStyles.Integer, NumberFormatInfo.InvariantInfo, out value))
							{
								throw new SerializationException(Environment.GetResourceString("Invalid BinaryFormatter stream."));
							}
							claimsIdentity = new WindowsIdentity(claimsIdentity, new IntPtr(value));
						}
						this.m_identities.Add(claimsIdentity);
					}
				}
			}
		}

		// Token: 0x060032EF RID: 13039 RVA: 0x000BBAD4 File Offset: 0x000B9CD4
		[SecurityCritical]
		private string SerializeIdentities()
		{
			List<string> list = new List<string>();
			BinaryFormatter binaryFormatter = new BinaryFormatter();
			foreach (ClaimsIdentity claimsIdentity in this.m_identities)
			{
				if (claimsIdentity.GetType() == typeof(WindowsIdentity))
				{
					WindowsIdentity windowsIdentity = claimsIdentity as WindowsIdentity;
					list.Add(windowsIdentity.GetTokenInternal().ToInt64().ToString(NumberFormatInfo.InvariantInfo));
					using (MemoryStream memoryStream = new MemoryStream())
					{
						binaryFormatter.Serialize(memoryStream, windowsIdentity.CloneAsBase(), null, false);
						list.Add(Convert.ToBase64String(memoryStream.GetBuffer(), 0, (int)memoryStream.Length));
						continue;
					}
				}
				using (MemoryStream memoryStream2 = new MemoryStream())
				{
					list.Add("");
					binaryFormatter.Serialize(memoryStream2, claimsIdentity, null, false);
					list.Add(Convert.ToBase64String(memoryStream2.GetBuffer(), 0, (int)memoryStream2.Length));
				}
			}
			string result;
			using (MemoryStream memoryStream3 = new MemoryStream())
			{
				binaryFormatter.Serialize(memoryStream3, list, null, false);
				result = Convert.ToBase64String(memoryStream3.GetBuffer(), 0, (int)memoryStream3.Length);
			}
			return result;
		}

		/// <summary>Adds the specified claims identity to this claims principal.</summary>
		/// <param name="identity">The claims identity to add.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="identity" /> is null.</exception>
		// Token: 0x060032F0 RID: 13040 RVA: 0x000BBC5C File Offset: 0x000B9E5C
		[SecurityCritical]
		public virtual void AddIdentity(ClaimsIdentity identity)
		{
			if (identity == null)
			{
				throw new ArgumentNullException("identity");
			}
			this.m_identities.Add(identity);
		}

		/// <summary>Adds the specified claims identities to this claims principal.</summary>
		/// <param name="identities">The claims identities to add.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="identities" /> is null.</exception>
		// Token: 0x060032F1 RID: 13041 RVA: 0x000BBC78 File Offset: 0x000B9E78
		[SecurityCritical]
		public virtual void AddIdentities(IEnumerable<ClaimsIdentity> identities)
		{
			if (identities == null)
			{
				throw new ArgumentNullException("identities");
			}
			this.m_identities.AddRange(identities);
		}

		/// <summary>Gets a collection that contains all of the claims from all of the claims identities associated with this claims principal.</summary>
		/// <returns>The claims associated with this principal.</returns>
		// Token: 0x170006DE RID: 1758
		// (get) Token: 0x060032F2 RID: 13042 RVA: 0x000BBC94 File Offset: 0x000B9E94
		public virtual IEnumerable<Claim> Claims
		{
			get
			{
				foreach (ClaimsIdentity claimsIdentity in this.Identities)
				{
					foreach (Claim claim in claimsIdentity.Claims)
					{
						yield return claim;
					}
					IEnumerator<Claim> enumerator2 = null;
				}
				IEnumerator<ClaimsIdentity> enumerator = null;
				yield break;
				yield break;
			}
		}

		/// <summary>Gets the current claims principal.</summary>
		/// <returns>The current claims principal.</returns>
		// Token: 0x170006DF RID: 1759
		// (get) Token: 0x060032F3 RID: 13043 RVA: 0x000BBCA4 File Offset: 0x000B9EA4
		public static ClaimsPrincipal Current
		{
			get
			{
				if (ClaimsPrincipal.s_principalSelector != null)
				{
					return ClaimsPrincipal.s_principalSelector();
				}
				return ClaimsPrincipal.SelectClaimsPrincipal();
			}
		}

		/// <summary>Retrieves all of the claims that are matched by the specified predicate.</summary>
		/// <param name="match">The function that performs the matching logic.</param>
		/// <returns>The matching claims.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="match" /> is null.</exception>
		// Token: 0x060032F4 RID: 13044 RVA: 0x000BBCC0 File Offset: 0x000B9EC0
		public virtual IEnumerable<Claim> FindAll(Predicate<Claim> match)
		{
			if (match == null)
			{
				throw new ArgumentNullException("match");
			}
			List<Claim> list = new List<Claim>();
			foreach (ClaimsIdentity claimsIdentity in this.Identities)
			{
				if (claimsIdentity != null)
				{
					foreach (Claim item in claimsIdentity.FindAll(match))
					{
						list.Add(item);
					}
				}
			}
			return list.AsReadOnly();
		}

		/// <summary>Retrieves all or the claims that have the specified claim type.</summary>
		/// <param name="type">The claim type against which to match claims.</param>
		/// <returns>The matching claims.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="type" /> is null.</exception>
		// Token: 0x060032F5 RID: 13045 RVA: 0x000BBD64 File Offset: 0x000B9F64
		public virtual IEnumerable<Claim> FindAll(string type)
		{
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			List<Claim> list = new List<Claim>();
			foreach (ClaimsIdentity claimsIdentity in this.Identities)
			{
				if (claimsIdentity != null)
				{
					foreach (Claim item in claimsIdentity.FindAll(type))
					{
						list.Add(item);
					}
				}
			}
			return list.AsReadOnly();
		}

		/// <summary>Retrieves the first claim that is matched by the specified predicate.</summary>
		/// <param name="match">The function that performs the matching logic.</param>
		/// <returns>The first matching claim or <see langword="null" /> if no match is found.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="match" /> is null.</exception>
		// Token: 0x060032F6 RID: 13046 RVA: 0x000BBE08 File Offset: 0x000BA008
		public virtual Claim FindFirst(Predicate<Claim> match)
		{
			if (match == null)
			{
				throw new ArgumentNullException("match");
			}
			Claim claim = null;
			foreach (ClaimsIdentity claimsIdentity in this.Identities)
			{
				if (claimsIdentity != null)
				{
					claim = claimsIdentity.FindFirst(match);
					if (claim != null)
					{
						return claim;
					}
				}
			}
			return claim;
		}

		/// <summary>Retrieves the first claim with the specified claim type.</summary>
		/// <param name="type">The claim type to match.</param>
		/// <returns>The first matching claim or <see langword="null" /> if no match is found.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="type" /> is null.</exception>
		// Token: 0x060032F7 RID: 13047 RVA: 0x000BBE74 File Offset: 0x000BA074
		public virtual Claim FindFirst(string type)
		{
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			Claim claim = null;
			for (int i = 0; i < this.m_identities.Count; i++)
			{
				if (this.m_identities[i] != null)
				{
					claim = this.m_identities[i].FindFirst(type);
					if (claim != null)
					{
						return claim;
					}
				}
			}
			return claim;
		}

		/// <summary>Determines whether any of the claims identities associated with this claims principal contains a claim that is matched by the specified predicate.</summary>
		/// <param name="match">The function that performs the matching logic.</param>
		/// <returns>
		///   <see langword="true" /> if a matching claim exists; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="match" /> is null.</exception>
		// Token: 0x060032F8 RID: 13048 RVA: 0x000BBED0 File Offset: 0x000BA0D0
		public virtual bool HasClaim(Predicate<Claim> match)
		{
			if (match == null)
			{
				throw new ArgumentNullException("match");
			}
			for (int i = 0; i < this.m_identities.Count; i++)
			{
				if (this.m_identities[i] != null && this.m_identities[i].HasClaim(match))
				{
					return true;
				}
			}
			return false;
		}

		/// <summary>Determines whether any of the claims identities associated with this claims principal contains a claim with the specified claim type and value.</summary>
		/// <param name="type">The type of the claim to match.</param>
		/// <param name="value">The value of the claim to match.</param>
		/// <returns>
		///   <see langword="true" /> if a matching claim exists; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="type" /> is null.  
		/// -or-  
		/// <paramref name="value" /> is null.</exception>
		// Token: 0x060032F9 RID: 13049 RVA: 0x000BBF28 File Offset: 0x000BA128
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
			for (int i = 0; i < this.m_identities.Count; i++)
			{
				if (this.m_identities[i] != null && this.m_identities[i].HasClaim(type, value))
				{
					return true;
				}
			}
			return false;
		}

		/// <summary>Gets a collection that contains all of the claims identities associated with this claims principal.</summary>
		/// <returns>The collection of claims identities.</returns>
		// Token: 0x170006E0 RID: 1760
		// (get) Token: 0x060032FA RID: 13050 RVA: 0x000BBF8D File Offset: 0x000BA18D
		public virtual IEnumerable<ClaimsIdentity> Identities
		{
			get
			{
				return this.m_identities.AsReadOnly();
			}
		}

		/// <summary>Gets the primary claims identity associated with this claims principal.</summary>
		/// <returns>The primary claims identity associated with this claims principal.</returns>
		// Token: 0x170006E1 RID: 1761
		// (get) Token: 0x060032FB RID: 13051 RVA: 0x000BBF9A File Offset: 0x000BA19A
		public virtual IIdentity Identity
		{
			get
			{
				if (ClaimsPrincipal.s_identitySelector != null)
				{
					return ClaimsPrincipal.s_identitySelector(this.m_identities);
				}
				return ClaimsPrincipal.SelectPrimaryIdentity(this.m_identities);
			}
		}

		/// <summary>Returns a value that indicates whether the entity (user) represented by this claims principal is in the specified role.</summary>
		/// <param name="role">The role for which to check.</param>
		/// <returns>
		///   <see langword="true" /> if claims principal is in the specified role; otherwise, <see langword="false" />.</returns>
		// Token: 0x060032FC RID: 13052 RVA: 0x000BBFC0 File Offset: 0x000BA1C0
		public virtual bool IsInRole(string role)
		{
			for (int i = 0; i < this.m_identities.Count; i++)
			{
				if (this.m_identities[i] != null && this.m_identities[i].HasClaim(this.m_identities[i].RoleClaimType, role))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x060032FD RID: 13053 RVA: 0x000BC01C File Offset: 0x000BA21C
		private void Initialize(BinaryReader reader)
		{
			if (reader == null)
			{
				throw new ArgumentNullException("reader");
			}
			ClaimsPrincipal.SerializationMask serializationMask = (ClaimsPrincipal.SerializationMask)reader.ReadInt32();
			int num = reader.ReadInt32();
			int num2 = 0;
			if ((serializationMask & ClaimsPrincipal.SerializationMask.HasIdentities) == ClaimsPrincipal.SerializationMask.HasIdentities)
			{
				num2++;
				int num3 = reader.ReadInt32();
				for (int i = 0; i < num3; i++)
				{
					this.m_identities.Add(this.CreateClaimsIdentity(reader));
				}
			}
			if ((serializationMask & ClaimsPrincipal.SerializationMask.UserData) == ClaimsPrincipal.SerializationMask.UserData)
			{
				int count = reader.ReadInt32();
				this.m_userSerializationData = reader.ReadBytes(count);
				num2++;
			}
			for (int j = num2; j < num; j++)
			{
				reader.ReadString();
			}
		}

		/// <summary>Serializes using a <see cref="T:System.IO.BinaryWriter" />.</summary>
		/// <param name="writer">The writer to use for data storage.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="writer" /> is <see langword="null" />.</exception>
		// Token: 0x060032FE RID: 13054 RVA: 0x000BC0B1 File Offset: 0x000BA2B1
		public virtual void WriteTo(BinaryWriter writer)
		{
			this.WriteTo(writer, null);
		}

		/// <summary>Serializes using a <see cref="T:System.IO.BinaryWriter" />.</summary>
		/// <param name="writer">The writer to use for data storage.</param>
		/// <param name="userData">Additional data provided by the derived type.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="writer" /> is <see langword="null" />.</exception>
		// Token: 0x060032FF RID: 13055 RVA: 0x000BC0BC File Offset: 0x000BA2BC
		protected virtual void WriteTo(BinaryWriter writer, byte[] userData)
		{
			if (writer == null)
			{
				throw new ArgumentNullException("writer");
			}
			int num = 0;
			ClaimsPrincipal.SerializationMask serializationMask = ClaimsPrincipal.SerializationMask.None;
			if (this.m_identities.Count > 0)
			{
				serializationMask |= ClaimsPrincipal.SerializationMask.HasIdentities;
				num++;
			}
			if (userData != null && userData.Length != 0)
			{
				num++;
				serializationMask |= ClaimsPrincipal.SerializationMask.UserData;
			}
			writer.Write((int)serializationMask);
			writer.Write(num);
			if ((serializationMask & ClaimsPrincipal.SerializationMask.HasIdentities) == ClaimsPrincipal.SerializationMask.HasIdentities)
			{
				writer.Write(this.m_identities.Count);
				foreach (ClaimsIdentity claimsIdentity in this.m_identities)
				{
					claimsIdentity.WriteTo(writer);
				}
			}
			if ((serializationMask & ClaimsPrincipal.SerializationMask.UserData) == ClaimsPrincipal.SerializationMask.UserData)
			{
				writer.Write(userData.Length);
				writer.Write(userData);
			}
			writer.Flush();
		}

		// Token: 0x06003300 RID: 13056 RVA: 0x000BC188 File Offset: 0x000BA388
		// Note: this type is marked as 'beforefieldinit'.
		static ClaimsPrincipal()
		{
		}

		// Token: 0x040023DD RID: 9181
		[NonSerialized]
		private byte[] m_userSerializationData;

		// Token: 0x040023DE RID: 9182
		[NonSerialized]
		private const string PreFix = "System.Security.ClaimsPrincipal.";

		// Token: 0x040023DF RID: 9183
		[NonSerialized]
		private const string IdentitiesKey = "System.Security.ClaimsPrincipal.Identities";

		// Token: 0x040023E0 RID: 9184
		[NonSerialized]
		private const string VersionKey = "System.Security.ClaimsPrincipal.Version";

		// Token: 0x040023E1 RID: 9185
		[OptionalField(VersionAdded = 2)]
		private string m_version = "1.0";

		// Token: 0x040023E2 RID: 9186
		[OptionalField(VersionAdded = 2)]
		private string m_serializedClaimsIdentities;

		// Token: 0x040023E3 RID: 9187
		[NonSerialized]
		private List<ClaimsIdentity> m_identities = new List<ClaimsIdentity>();

		// Token: 0x040023E4 RID: 9188
		[NonSerialized]
		private static Func<IEnumerable<ClaimsIdentity>, ClaimsIdentity> s_identitySelector = new Func<IEnumerable<ClaimsIdentity>, ClaimsIdentity>(ClaimsPrincipal.SelectPrimaryIdentity);

		// Token: 0x040023E5 RID: 9189
		[NonSerialized]
		private static Func<ClaimsPrincipal> s_principalSelector = ClaimsPrincipal.ClaimsPrincipalSelector;

		// Token: 0x020004F7 RID: 1271
		private enum SerializationMask
		{
			// Token: 0x040023E7 RID: 9191
			None,
			// Token: 0x040023E8 RID: 9192
			HasIdentities,
			// Token: 0x040023E9 RID: 9193
			UserData
		}

		// Token: 0x020004F8 RID: 1272
		[CompilerGenerated]
		private sealed class <get_Claims>d__37 : IEnumerable<Claim>, IEnumerable, IEnumerator<Claim>, IDisposable, IEnumerator
		{
			// Token: 0x06003301 RID: 13057 RVA: 0x000BC1A5 File Offset: 0x000BA3A5
			[DebuggerHidden]
			public <get_Claims>d__37(int <>1__state)
			{
				this.<>1__state = <>1__state;
				this.<>l__initialThreadId = Environment.CurrentManagedThreadId;
			}

			// Token: 0x06003302 RID: 13058 RVA: 0x000BC1C0 File Offset: 0x000BA3C0
			[DebuggerHidden]
			void IDisposable.Dispose()
			{
				int num = this.<>1__state;
				if (num - -4 <= 1 || num == 1)
				{
					try
					{
						if (num == -4 || num == 1)
						{
							try
							{
							}
							finally
							{
								this.<>m__Finally2();
							}
						}
					}
					finally
					{
						this.<>m__Finally1();
					}
				}
			}

			// Token: 0x06003303 RID: 13059 RVA: 0x000BC218 File Offset: 0x000BA418
			bool IEnumerator.MoveNext()
			{
				bool result;
				try
				{
					int num = this.<>1__state;
					ClaimsPrincipal claimsPrincipal = this;
					if (num == 0)
					{
						this.<>1__state = -1;
						enumerator = claimsPrincipal.Identities.GetEnumerator();
						this.<>1__state = -3;
						goto IL_A7;
					}
					if (num != 1)
					{
						return false;
					}
					this.<>1__state = -4;
					IL_8D:
					if (enumerator2.MoveNext())
					{
						Claim claim = enumerator2.Current;
						this.<>2__current = claim;
						this.<>1__state = 1;
						return true;
					}
					this.<>m__Finally2();
					enumerator2 = null;
					IL_A7:
					if (enumerator.MoveNext())
					{
						ClaimsIdentity claimsIdentity = enumerator.Current;
						enumerator2 = claimsIdentity.Claims.GetEnumerator();
						this.<>1__state = -4;
						goto IL_8D;
					}
					this.<>m__Finally1();
					enumerator = null;
					result = false;
				}
				catch
				{
					this.System.IDisposable.Dispose();
					throw;
				}
				return result;
			}

			// Token: 0x06003304 RID: 13060 RVA: 0x000BC304 File Offset: 0x000BA504
			private void <>m__Finally1()
			{
				this.<>1__state = -1;
				if (enumerator != null)
				{
					enumerator.Dispose();
				}
			}

			// Token: 0x06003305 RID: 13061 RVA: 0x000BC320 File Offset: 0x000BA520
			private void <>m__Finally2()
			{
				this.<>1__state = -3;
				if (enumerator2 != null)
				{
					enumerator2.Dispose();
				}
			}

			// Token: 0x170006E2 RID: 1762
			// (get) Token: 0x06003306 RID: 13062 RVA: 0x000BC33D File Offset: 0x000BA53D
			Claim IEnumerator<Claim>.Current
			{
				[DebuggerHidden]
				get
				{
					return this.<>2__current;
				}
			}

			// Token: 0x06003307 RID: 13063 RVA: 0x000472C8 File Offset: 0x000454C8
			[DebuggerHidden]
			void IEnumerator.Reset()
			{
				throw new NotSupportedException();
			}

			// Token: 0x170006E3 RID: 1763
			// (get) Token: 0x06003308 RID: 13064 RVA: 0x000BC33D File Offset: 0x000BA53D
			object IEnumerator.Current
			{
				[DebuggerHidden]
				get
				{
					return this.<>2__current;
				}
			}

			// Token: 0x06003309 RID: 13065 RVA: 0x000BC348 File Offset: 0x000BA548
			[DebuggerHidden]
			IEnumerator<Claim> IEnumerable<Claim>.GetEnumerator()
			{
				ClaimsPrincipal.<get_Claims>d__37 <get_Claims>d__;
				if (this.<>1__state == -2 && this.<>l__initialThreadId == Environment.CurrentManagedThreadId)
				{
					this.<>1__state = 0;
					<get_Claims>d__ = this;
				}
				else
				{
					<get_Claims>d__ = new ClaimsPrincipal.<get_Claims>d__37(0);
					<get_Claims>d__.<>4__this = this;
				}
				return <get_Claims>d__;
			}

			// Token: 0x0600330A RID: 13066 RVA: 0x000BC38B File Offset: 0x000BA58B
			[DebuggerHidden]
			IEnumerator IEnumerable.GetEnumerator()
			{
				return this.System.Collections.Generic.IEnumerable<System.Security.Claims.Claim>.GetEnumerator();
			}

			// Token: 0x040023EA RID: 9194
			private int <>1__state;

			// Token: 0x040023EB RID: 9195
			private Claim <>2__current;

			// Token: 0x040023EC RID: 9196
			private int <>l__initialThreadId;

			// Token: 0x040023ED RID: 9197
			public ClaimsPrincipal <>4__this;

			// Token: 0x040023EE RID: 9198
			private IEnumerator<ClaimsIdentity> <>7__wrap1;

			// Token: 0x040023EF RID: 9199
			private IEnumerator<Claim> <>7__wrap2;
		}
	}
}
