using System;
using System.Runtime.CompilerServices;
using System.Text;

namespace System
{
	/// <summary>Contains information used to uniquely identify a manifest-based application. This class cannot be inherited.</summary>
	// Token: 0x020001C8 RID: 456
	[Serializable]
	public sealed class ApplicationId
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ApplicationId" /> class.</summary>
		/// <param name="publicKeyToken">The array of bytes representing the raw public key data.</param>
		/// <param name="name">The name of the application.</param>
		/// <param name="version">A <see cref="T:System.Version" /> object that specifies the version of the application.</param>
		/// <param name="processorArchitecture">The processor architecture of the application.</param>
		/// <param name="culture">The culture of the application.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="name" /> is <see langword="null" />.  
		/// -or-  
		/// <paramref name="version" /> is <see langword="null" />.  
		/// -or-  
		/// <paramref name="publicKeyToken" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="name" /> is an empty string.</exception>
		// Token: 0x060013A9 RID: 5033 RVA: 0x0004E4E8 File Offset: 0x0004C6E8
		public ApplicationId(byte[] publicKeyToken, string name, Version version, string processorArchitecture, string culture)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			if (name.Length == 0)
			{
				throw new ArgumentException("ApplicationId cannot have an empty string for the name.");
			}
			if (version == null)
			{
				throw new ArgumentNullException("version");
			}
			if (publicKeyToken == null)
			{
				throw new ArgumentNullException("publicKeyToken");
			}
			this._publicKeyToken = (byte[])publicKeyToken.Clone();
			this.Name = name;
			this.Version = version;
			this.ProcessorArchitecture = processorArchitecture;
			this.Culture = culture;
		}

		/// <summary>Gets a string representing the culture information for the application.</summary>
		/// <returns>The culture information for the application.</returns>
		// Token: 0x170001D3 RID: 467
		// (get) Token: 0x060013AA RID: 5034 RVA: 0x0004E56D File Offset: 0x0004C76D
		public string Culture
		{
			[CompilerGenerated]
			get
			{
				return this.<Culture>k__BackingField;
			}
		}

		/// <summary>Gets the name of the application.</summary>
		/// <returns>The name of the application.</returns>
		// Token: 0x170001D4 RID: 468
		// (get) Token: 0x060013AB RID: 5035 RVA: 0x0004E575 File Offset: 0x0004C775
		public string Name
		{
			[CompilerGenerated]
			get
			{
				return this.<Name>k__BackingField;
			}
		}

		/// <summary>Gets the target processor architecture for the application.</summary>
		/// <returns>The processor architecture of the application.</returns>
		// Token: 0x170001D5 RID: 469
		// (get) Token: 0x060013AC RID: 5036 RVA: 0x0004E57D File Offset: 0x0004C77D
		public string ProcessorArchitecture
		{
			[CompilerGenerated]
			get
			{
				return this.<ProcessorArchitecture>k__BackingField;
			}
		}

		/// <summary>Gets the version of the application.</summary>
		/// <returns>A <see cref="T:System.Version" /> that specifies the version of the application.</returns>
		// Token: 0x170001D6 RID: 470
		// (get) Token: 0x060013AD RID: 5037 RVA: 0x0004E585 File Offset: 0x0004C785
		public Version Version
		{
			[CompilerGenerated]
			get
			{
				return this.<Version>k__BackingField;
			}
		}

		/// <summary>Gets the public key token for the application.</summary>
		/// <returns>A byte array containing the public key token for the application.</returns>
		// Token: 0x170001D7 RID: 471
		// (get) Token: 0x060013AE RID: 5038 RVA: 0x0004E58D File Offset: 0x0004C78D
		public byte[] PublicKeyToken
		{
			get
			{
				return (byte[])this._publicKeyToken.Clone();
			}
		}

		/// <summary>Creates and returns an identical copy of the current application identity.</summary>
		/// <returns>An <see cref="T:System.ApplicationId" /> object that represents an exact copy of the original.</returns>
		// Token: 0x060013AF RID: 5039 RVA: 0x0004E59F File Offset: 0x0004C79F
		public ApplicationId Copy()
		{
			return new ApplicationId(this._publicKeyToken, this.Name, this.Version, this.ProcessorArchitecture, this.Culture);
		}

		/// <summary>Creates and returns a string representation of the application identity.</summary>
		/// <returns>A string representation of the application identity.</returns>
		// Token: 0x060013B0 RID: 5040 RVA: 0x0004E5C4 File Offset: 0x0004C7C4
		public override string ToString()
		{
			StringBuilder stringBuilder = StringBuilderCache.Acquire(16);
			stringBuilder.Append(this.Name);
			if (this.Culture != null)
			{
				stringBuilder.Append(", culture=\"");
				stringBuilder.Append(this.Culture);
				stringBuilder.Append('"');
			}
			stringBuilder.Append(", version=\"");
			stringBuilder.Append(this.Version.ToString());
			stringBuilder.Append('"');
			if (this._publicKeyToken != null)
			{
				stringBuilder.Append(", publicKeyToken=\"");
				stringBuilder.Append(ApplicationId.EncodeHexString(this._publicKeyToken));
				stringBuilder.Append('"');
			}
			if (this.ProcessorArchitecture != null)
			{
				stringBuilder.Append(", processorArchitecture =\"");
				stringBuilder.Append(this.ProcessorArchitecture);
				stringBuilder.Append('"');
			}
			return StringBuilderCache.GetStringAndRelease(stringBuilder);
		}

		// Token: 0x060013B1 RID: 5041 RVA: 0x0004E696 File Offset: 0x0004C896
		private static char HexDigit(int num)
		{
			return (char)((num < 10) ? (num + 48) : (num + 55));
		}

		// Token: 0x060013B2 RID: 5042 RVA: 0x0004E6A8 File Offset: 0x0004C8A8
		private static string EncodeHexString(byte[] sArray)
		{
			string result = null;
			if (sArray != null)
			{
				char[] array = new char[sArray.Length * 2];
				int i = 0;
				int num = 0;
				while (i < sArray.Length)
				{
					int num2 = (sArray[i] & 240) >> 4;
					array[num++] = ApplicationId.HexDigit(num2);
					num2 = (int)(sArray[i] & 15);
					array[num++] = ApplicationId.HexDigit(num2);
					i++;
				}
				result = new string(array);
			}
			return result;
		}

		/// <summary>Determines whether the specified <see cref="T:System.ApplicationId" /> object is equivalent to the current <see cref="T:System.ApplicationId" />.</summary>
		/// <param name="o">The <see cref="T:System.ApplicationId" /> object to compare to the current <see cref="T:System.ApplicationId" />.</param>
		/// <returns>
		///   <see langword="true" /> if the specified <see cref="T:System.ApplicationId" /> object is equivalent to the current <see cref="T:System.ApplicationId" />; otherwise, <see langword="false" />.</returns>
		// Token: 0x060013B3 RID: 5043 RVA: 0x0004E710 File Offset: 0x0004C910
		public override bool Equals(object o)
		{
			ApplicationId applicationId = o as ApplicationId;
			if (applicationId == null)
			{
				return false;
			}
			if (!object.Equals(this.Name, applicationId.Name) || !object.Equals(this.Version, applicationId.Version) || !object.Equals(this.ProcessorArchitecture, applicationId.ProcessorArchitecture) || !object.Equals(this.Culture, applicationId.Culture))
			{
				return false;
			}
			if (this._publicKeyToken.Length != applicationId._publicKeyToken.Length)
			{
				return false;
			}
			for (int i = 0; i < this._publicKeyToken.Length; i++)
			{
				if (this._publicKeyToken[i] != applicationId._publicKeyToken[i])
				{
					return false;
				}
			}
			return true;
		}

		/// <summary>Gets the hash code for the current application identity.</summary>
		/// <returns>The hash code for the current application identity.</returns>
		// Token: 0x060013B4 RID: 5044 RVA: 0x0004E7B3 File Offset: 0x0004C9B3
		public override int GetHashCode()
		{
			return this.Name.GetHashCode() ^ this.Version.GetHashCode();
		}

		// Token: 0x04001447 RID: 5191
		private readonly byte[] _publicKeyToken;

		// Token: 0x04001448 RID: 5192
		[CompilerGenerated]
		private readonly string <Culture>k__BackingField;

		// Token: 0x04001449 RID: 5193
		[CompilerGenerated]
		private readonly string <Name>k__BackingField;

		// Token: 0x0400144A RID: 5194
		[CompilerGenerated]
		private readonly string <ProcessorArchitecture>k__BackingField;

		// Token: 0x0400144B RID: 5195
		[CompilerGenerated]
		private readonly Version <Version>k__BackingField;
	}
}
