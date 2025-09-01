using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Runtime.Serialization;

namespace System.Security.Claims
{
	/// <summary>Represents a claim.</summary>
	// Token: 0x020004F0 RID: 1264
	[Serializable]
	public class Claim
	{
		/// <summary>Initializes an instance of <see cref="T:System.Security.Claims.Claim" /> with the specified <see cref="T:System.IO.BinaryReader" />.</summary>
		/// <param name="reader">A <see cref="T:System.IO.BinaryReader" /> pointing to a <see cref="T:System.Security.Claims.Claim" />.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="reader" /> is <see langword="null" />.</exception>
		// Token: 0x06003283 RID: 12931 RVA: 0x000B9B3C File Offset: 0x000B7D3C
		public Claim(BinaryReader reader) : this(reader, null)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Claims.Claim" /> class with the specified reader and subject.</summary>
		/// <param name="reader">The binary reader.</param>
		/// <param name="subject">The subject that this claim describes.</param>
		// Token: 0x06003284 RID: 12932 RVA: 0x000B9B46 File Offset: 0x000B7D46
		public Claim(BinaryReader reader, ClaimsIdentity subject)
		{
			this.m_propertyLock = new object();
			base..ctor();
			if (reader == null)
			{
				throw new ArgumentNullException("reader");
			}
			this.Initialize(reader, subject);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Claims.Claim" /> class with the specified claim type, and value.</summary>
		/// <param name="type">The claim type.</param>
		/// <param name="value">The claim value.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="type" /> or <paramref name="value" /> is <see langword="null" />.</exception>
		// Token: 0x06003285 RID: 12933 RVA: 0x000B9B6F File Offset: 0x000B7D6F
		public Claim(string type, string value) : this(type, value, "http://www.w3.org/2001/XMLSchema#string", "LOCAL AUTHORITY", "LOCAL AUTHORITY", null)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Claims.Claim" /> class with the specified claim type, value, and value type.</summary>
		/// <param name="type">The claim type.</param>
		/// <param name="value">The claim value.</param>
		/// <param name="valueType">The claim value type. If this parameter is <see langword="null" />, then <see cref="F:System.Security.Claims.ClaimValueTypes.String" /> is used.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="type" /> or <paramref name="value" /> is <see langword="null" />.</exception>
		// Token: 0x06003286 RID: 12934 RVA: 0x000B9B89 File Offset: 0x000B7D89
		public Claim(string type, string value, string valueType) : this(type, value, valueType, "LOCAL AUTHORITY", "LOCAL AUTHORITY", null)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Claims.Claim" /> class with the specified claim type, value, value type, and issuer.</summary>
		/// <param name="type">The claim type.</param>
		/// <param name="value">The claim value.</param>
		/// <param name="valueType">The claim value type. If this parameter is <see langword="null" />, then <see cref="F:System.Security.Claims.ClaimValueTypes.String" /> is used.</param>
		/// <param name="issuer">The claim issuer. If this parameter is empty or <see langword="null" />, then <see cref="F:System.Security.Claims.ClaimsIdentity.DefaultIssuer" /> is used.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="type" /> or <paramref name="value" /> is <see langword="null" />.</exception>
		// Token: 0x06003287 RID: 12935 RVA: 0x000B9B9F File Offset: 0x000B7D9F
		public Claim(string type, string value, string valueType, string issuer) : this(type, value, valueType, issuer, issuer, null)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Claims.Claim" /> class with the specified claim type, value, value type, issuer,  and original issuer.</summary>
		/// <param name="type">The claim type.</param>
		/// <param name="value">The claim value.</param>
		/// <param name="valueType">The claim value type. If this parameter is <see langword="null" />, then <see cref="F:System.Security.Claims.ClaimValueTypes.String" /> is used.</param>
		/// <param name="issuer">The claim issuer. If this parameter is empty or <see langword="null" />, then <see cref="F:System.Security.Claims.ClaimsIdentity.DefaultIssuer" /> is used.</param>
		/// <param name="originalIssuer">The original issuer of the claim. If this parameter is empty or <see langword="null" />, then the <see cref="P:System.Security.Claims.Claim.OriginalIssuer" /> property is set to the value of the <see cref="P:System.Security.Claims.Claim.Issuer" /> property.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="type" /> or <paramref name="value" /> is <see langword="null" />.</exception>
		// Token: 0x06003288 RID: 12936 RVA: 0x000B9BAF File Offset: 0x000B7DAF
		public Claim(string type, string value, string valueType, string issuer, string originalIssuer) : this(type, value, valueType, issuer, originalIssuer, null)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Claims.Claim" /> class with the specified claim type, value, value type, issuer, original issuer and subject.</summary>
		/// <param name="type">The claim type.</param>
		/// <param name="value">The claim value.</param>
		/// <param name="valueType">The claim value type. If this parameter is <see langword="null" />, then <see cref="F:System.Security.Claims.ClaimValueTypes.String" /> is used.</param>
		/// <param name="issuer">The claim issuer. If this parameter is empty or <see langword="null" />, then <see cref="F:System.Security.Claims.ClaimsIdentity.DefaultIssuer" /> is used.</param>
		/// <param name="originalIssuer">The original issuer of the claim. If this parameter is empty or <see langword="null" />, then the <see cref="P:System.Security.Claims.Claim.OriginalIssuer" /> property is set to the value of the <see cref="P:System.Security.Claims.Claim.Issuer" /> property.</param>
		/// <param name="subject">The subject that this claim describes.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="type" /> or <paramref name="value" /> is <see langword="null" />.</exception>
		// Token: 0x06003289 RID: 12937 RVA: 0x000B9BC0 File Offset: 0x000B7DC0
		public Claim(string type, string value, string valueType, string issuer, string originalIssuer, ClaimsIdentity subject) : this(type, value, valueType, issuer, originalIssuer, subject, null, null)
		{
		}

		// Token: 0x0600328A RID: 12938 RVA: 0x000B9BE0 File Offset: 0x000B7DE0
		internal Claim(string type, string value, string valueType, string issuer, string originalIssuer, ClaimsIdentity subject, string propertyKey, string propertyValue)
		{
			this.m_propertyLock = new object();
			base..ctor();
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			this.m_type = type;
			this.m_value = value;
			if (string.IsNullOrEmpty(valueType))
			{
				this.m_valueType = "http://www.w3.org/2001/XMLSchema#string";
			}
			else
			{
				this.m_valueType = valueType;
			}
			if (string.IsNullOrEmpty(issuer))
			{
				this.m_issuer = "LOCAL AUTHORITY";
			}
			else
			{
				this.m_issuer = issuer;
			}
			if (string.IsNullOrEmpty(originalIssuer))
			{
				this.m_originalIssuer = this.m_issuer;
			}
			else
			{
				this.m_originalIssuer = originalIssuer;
			}
			this.m_subject = subject;
			if (propertyKey != null)
			{
				this.Properties.Add(propertyKey, propertyValue);
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Claims.Claim" /> class.</summary>
		/// <param name="other">The security claim.</param>
		// Token: 0x0600328B RID: 12939 RVA: 0x000B9C9C File Offset: 0x000B7E9C
		protected Claim(Claim other) : this(other, (other == null) ? null : other.m_subject)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Claims.Claim" /> class with the specified security claim and subject.</summary>
		/// <param name="other">The security claim.</param>
		/// <param name="subject">The subject that this claim describes.</param>
		// Token: 0x0600328C RID: 12940 RVA: 0x000B9CB4 File Offset: 0x000B7EB4
		protected Claim(Claim other, ClaimsIdentity subject)
		{
			this.m_propertyLock = new object();
			base..ctor();
			if (other == null)
			{
				throw new ArgumentNullException("other");
			}
			this.m_issuer = other.m_issuer;
			this.m_originalIssuer = other.m_originalIssuer;
			this.m_subject = subject;
			this.m_type = other.m_type;
			this.m_value = other.m_value;
			this.m_valueType = other.m_valueType;
			if (other.m_properties != null)
			{
				this.m_properties = new Dictionary<string, string>();
				foreach (string key in other.m_properties.Keys)
				{
					this.m_properties.Add(key, other.m_properties[key]);
				}
			}
			if (other.m_userSerializationData != null)
			{
				this.m_userSerializationData = (other.m_userSerializationData.Clone() as byte[]);
			}
		}

		/// <summary>Contains any additional data provided by a derived type.</summary>
		/// <returns>A <see cref="T:System.Byte" /> array representing the additional serialized data.</returns>
		// Token: 0x170006C6 RID: 1734
		// (get) Token: 0x0600328D RID: 12941 RVA: 0x000B9DB0 File Offset: 0x000B7FB0
		protected virtual byte[] CustomSerializationData
		{
			get
			{
				return this.m_userSerializationData;
			}
		}

		/// <summary>Gets the issuer of the claim.</summary>
		/// <returns>A name that refers to the issuer of the claim.</returns>
		// Token: 0x170006C7 RID: 1735
		// (get) Token: 0x0600328E RID: 12942 RVA: 0x000B9DB8 File Offset: 0x000B7FB8
		public string Issuer
		{
			get
			{
				return this.m_issuer;
			}
		}

		// Token: 0x0600328F RID: 12943 RVA: 0x000B9DC0 File Offset: 0x000B7FC0
		[OnDeserialized]
		private void OnDeserializedMethod(StreamingContext context)
		{
			this.m_propertyLock = new object();
		}

		/// <summary>Gets the original issuer of the claim.</summary>
		/// <returns>A name that refers to the original issuer of the claim.</returns>
		// Token: 0x170006C8 RID: 1736
		// (get) Token: 0x06003290 RID: 12944 RVA: 0x000B9DCD File Offset: 0x000B7FCD
		public string OriginalIssuer
		{
			get
			{
				return this.m_originalIssuer;
			}
		}

		/// <summary>Gets a dictionary that contains additional properties associated with this claim.</summary>
		/// <returns>A dictionary that contains additional properties associated with the claim. The properties are represented as name-value pairs.</returns>
		// Token: 0x170006C9 RID: 1737
		// (get) Token: 0x06003291 RID: 12945 RVA: 0x000B9DD8 File Offset: 0x000B7FD8
		public IDictionary<string, string> Properties
		{
			get
			{
				if (this.m_properties == null)
				{
					object propertyLock = this.m_propertyLock;
					lock (propertyLock)
					{
						if (this.m_properties == null)
						{
							this.m_properties = new Dictionary<string, string>();
						}
					}
				}
				return this.m_properties;
			}
		}

		/// <summary>Gets the subject of the claim.</summary>
		/// <returns>The subject of the claim.</returns>
		// Token: 0x170006CA RID: 1738
		// (get) Token: 0x06003292 RID: 12946 RVA: 0x000B9E34 File Offset: 0x000B8034
		// (set) Token: 0x06003293 RID: 12947 RVA: 0x000B9E3C File Offset: 0x000B803C
		public ClaimsIdentity Subject
		{
			get
			{
				return this.m_subject;
			}
			internal set
			{
				this.m_subject = value;
			}
		}

		/// <summary>Gets the claim type of the claim.</summary>
		/// <returns>The claim type.</returns>
		// Token: 0x170006CB RID: 1739
		// (get) Token: 0x06003294 RID: 12948 RVA: 0x000B9E45 File Offset: 0x000B8045
		public string Type
		{
			get
			{
				return this.m_type;
			}
		}

		/// <summary>Gets the value of the claim.</summary>
		/// <returns>The claim value.</returns>
		// Token: 0x170006CC RID: 1740
		// (get) Token: 0x06003295 RID: 12949 RVA: 0x000B9E4D File Offset: 0x000B804D
		public string Value
		{
			get
			{
				return this.m_value;
			}
		}

		/// <summary>Gets the value type of the claim.</summary>
		/// <returns>The claim value type.</returns>
		// Token: 0x170006CD RID: 1741
		// (get) Token: 0x06003296 RID: 12950 RVA: 0x000B9E55 File Offset: 0x000B8055
		public string ValueType
		{
			get
			{
				return this.m_valueType;
			}
		}

		/// <summary>Returns a new <see cref="T:System.Security.Claims.Claim" /> object copied from this object. The new claim does not have a subject.</summary>
		/// <returns>The new claim object.</returns>
		// Token: 0x06003297 RID: 12951 RVA: 0x000B9E5D File Offset: 0x000B805D
		public virtual Claim Clone()
		{
			return this.Clone(null);
		}

		/// <summary>Returns a new <see cref="T:System.Security.Claims.Claim" /> object copied from this object. The subject of the new claim is set to the specified ClaimsIdentity.</summary>
		/// <param name="identity">The intended subject of the new claim.</param>
		/// <returns>The new claim object.</returns>
		// Token: 0x06003298 RID: 12952 RVA: 0x000B9E66 File Offset: 0x000B8066
		public virtual Claim Clone(ClaimsIdentity identity)
		{
			return new Claim(this, identity);
		}

		// Token: 0x06003299 RID: 12953 RVA: 0x000B9E70 File Offset: 0x000B8070
		private void Initialize(BinaryReader reader, ClaimsIdentity subject)
		{
			if (reader == null)
			{
				throw new ArgumentNullException("reader");
			}
			this.m_subject = subject;
			Claim.SerializationMask serializationMask = (Claim.SerializationMask)reader.ReadInt32();
			int num = 1;
			int num2 = reader.ReadInt32();
			this.m_value = reader.ReadString();
			if ((serializationMask & Claim.SerializationMask.NameClaimType) == Claim.SerializationMask.NameClaimType)
			{
				this.m_type = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name";
			}
			else if ((serializationMask & Claim.SerializationMask.RoleClaimType) == Claim.SerializationMask.RoleClaimType)
			{
				this.m_type = "http://schemas.microsoft.com/ws/2008/06/identity/claims/role";
			}
			else
			{
				this.m_type = reader.ReadString();
				num++;
			}
			if ((serializationMask & Claim.SerializationMask.StringType) == Claim.SerializationMask.StringType)
			{
				this.m_valueType = reader.ReadString();
				num++;
			}
			else
			{
				this.m_valueType = "http://www.w3.org/2001/XMLSchema#string";
			}
			if ((serializationMask & Claim.SerializationMask.Issuer) == Claim.SerializationMask.Issuer)
			{
				this.m_issuer = reader.ReadString();
				num++;
			}
			else
			{
				this.m_issuer = "LOCAL AUTHORITY";
			}
			if ((serializationMask & Claim.SerializationMask.OriginalIssuerEqualsIssuer) == Claim.SerializationMask.OriginalIssuerEqualsIssuer)
			{
				this.m_originalIssuer = this.m_issuer;
			}
			else if ((serializationMask & Claim.SerializationMask.OriginalIssuer) == Claim.SerializationMask.OriginalIssuer)
			{
				this.m_originalIssuer = reader.ReadString();
				num++;
			}
			else
			{
				this.m_originalIssuer = "LOCAL AUTHORITY";
			}
			if ((serializationMask & Claim.SerializationMask.HasProperties) == Claim.SerializationMask.HasProperties)
			{
				int num3 = reader.ReadInt32();
				for (int i = 0; i < num3; i++)
				{
					this.Properties.Add(reader.ReadString(), reader.ReadString());
				}
			}
			if ((serializationMask & Claim.SerializationMask.UserData) == Claim.SerializationMask.UserData)
			{
				int count = reader.ReadInt32();
				this.m_userSerializationData = reader.ReadBytes(count);
				num++;
			}
			for (int j = num; j < num2; j++)
			{
				reader.ReadString();
			}
		}

		/// <summary>Writes this <see cref="T:System.Security.Claims.Claim" /> to the writer.</summary>
		/// <param name="writer">The writer to use for data storage.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="writer" /> is <see langword="null" />.</exception>
		// Token: 0x0600329A RID: 12954 RVA: 0x000B9FDA File Offset: 0x000B81DA
		public virtual void WriteTo(BinaryWriter writer)
		{
			this.WriteTo(writer, null);
		}

		/// <summary>Writes this <see cref="T:System.Security.Claims.Claim" /> to the writer.</summary>
		/// <param name="writer">The writer to write this claim.</param>
		/// <param name="userData">The user data to claim.</param>
		// Token: 0x0600329B RID: 12955 RVA: 0x000B9FE4 File Offset: 0x000B81E4
		protected virtual void WriteTo(BinaryWriter writer, byte[] userData)
		{
			if (writer == null)
			{
				throw new ArgumentNullException("writer");
			}
			int num = 1;
			Claim.SerializationMask serializationMask = Claim.SerializationMask.None;
			if (string.Equals(this.m_type, "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name"))
			{
				serializationMask |= Claim.SerializationMask.NameClaimType;
			}
			else if (string.Equals(this.m_type, "http://schemas.microsoft.com/ws/2008/06/identity/claims/role"))
			{
				serializationMask |= Claim.SerializationMask.RoleClaimType;
			}
			else
			{
				num++;
			}
			if (!string.Equals(this.m_valueType, "http://www.w3.org/2001/XMLSchema#string", StringComparison.Ordinal))
			{
				num++;
				serializationMask |= Claim.SerializationMask.StringType;
			}
			if (!string.Equals(this.m_issuer, "LOCAL AUTHORITY", StringComparison.Ordinal))
			{
				num++;
				serializationMask |= Claim.SerializationMask.Issuer;
			}
			if (string.Equals(this.m_originalIssuer, this.m_issuer, StringComparison.Ordinal))
			{
				serializationMask |= Claim.SerializationMask.OriginalIssuerEqualsIssuer;
			}
			else if (!string.Equals(this.m_originalIssuer, "LOCAL AUTHORITY", StringComparison.Ordinal))
			{
				num++;
				serializationMask |= Claim.SerializationMask.OriginalIssuer;
			}
			if (this.Properties.Count > 0)
			{
				num++;
				serializationMask |= Claim.SerializationMask.HasProperties;
			}
			if (userData != null && userData.Length != 0)
			{
				num++;
				serializationMask |= Claim.SerializationMask.UserData;
			}
			writer.Write((int)serializationMask);
			writer.Write(num);
			writer.Write(this.m_value);
			if ((serializationMask & Claim.SerializationMask.NameClaimType) != Claim.SerializationMask.NameClaimType && (serializationMask & Claim.SerializationMask.RoleClaimType) != Claim.SerializationMask.RoleClaimType)
			{
				writer.Write(this.m_type);
			}
			if ((serializationMask & Claim.SerializationMask.StringType) == Claim.SerializationMask.StringType)
			{
				writer.Write(this.m_valueType);
			}
			if ((serializationMask & Claim.SerializationMask.Issuer) == Claim.SerializationMask.Issuer)
			{
				writer.Write(this.m_issuer);
			}
			if ((serializationMask & Claim.SerializationMask.OriginalIssuer) == Claim.SerializationMask.OriginalIssuer)
			{
				writer.Write(this.m_originalIssuer);
			}
			if ((serializationMask & Claim.SerializationMask.HasProperties) == Claim.SerializationMask.HasProperties)
			{
				writer.Write(this.Properties.Count);
				foreach (string text in this.Properties.Keys)
				{
					writer.Write(text);
					writer.Write(this.Properties[text]);
				}
			}
			if ((serializationMask & Claim.SerializationMask.UserData) == Claim.SerializationMask.UserData)
			{
				writer.Write(userData.Length);
				writer.Write(userData);
			}
			writer.Flush();
		}

		/// <summary>Returns a string representation of this <see cref="T:System.Security.Claims.Claim" /> object.</summary>
		/// <returns>The string representation of this <see cref="T:System.Security.Claims.Claim" /> object.</returns>
		// Token: 0x0600329C RID: 12956 RVA: 0x000BA1CC File Offset: 0x000B83CC
		public override string ToString()
		{
			return string.Format(CultureInfo.InvariantCulture, "{0}: {1}", this.m_type, this.m_value);
		}

		// Token: 0x04002381 RID: 9089
		private string m_issuer;

		// Token: 0x04002382 RID: 9090
		private string m_originalIssuer;

		// Token: 0x04002383 RID: 9091
		private string m_type;

		// Token: 0x04002384 RID: 9092
		private string m_value;

		// Token: 0x04002385 RID: 9093
		private string m_valueType;

		// Token: 0x04002386 RID: 9094
		[NonSerialized]
		private byte[] m_userSerializationData;

		// Token: 0x04002387 RID: 9095
		private Dictionary<string, string> m_properties;

		// Token: 0x04002388 RID: 9096
		[NonSerialized]
		private object m_propertyLock;

		// Token: 0x04002389 RID: 9097
		[NonSerialized]
		private ClaimsIdentity m_subject;

		// Token: 0x020004F1 RID: 1265
		private enum SerializationMask
		{
			// Token: 0x0400238B RID: 9099
			None,
			// Token: 0x0400238C RID: 9100
			NameClaimType,
			// Token: 0x0400238D RID: 9101
			RoleClaimType,
			// Token: 0x0400238E RID: 9102
			StringType = 4,
			// Token: 0x0400238F RID: 9103
			Issuer = 8,
			// Token: 0x04002390 RID: 9104
			OriginalIssuerEqualsIssuer = 16,
			// Token: 0x04002391 RID: 9105
			OriginalIssuer = 32,
			// Token: 0x04002392 RID: 9106
			HasProperties = 64,
			// Token: 0x04002393 RID: 9107
			UserData = 128
		}
	}
}
