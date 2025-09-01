using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;

namespace System.Security.Cryptography
{
	/// <summary>Provides the base class for data protectors.</summary>
	// Token: 0x02000012 RID: 18
	public abstract class DataProtector
	{
		/// <summary>Creates a new instance of the <see cref="T:System.Security.Cryptography.DataProtector" /> class by using the provided application name, primary purpose, and specific purposes.</summary>
		/// <param name="applicationName">The name of the application.</param>
		/// <param name="primaryPurpose">The primary purpose for the protected data.</param>
		/// <param name="specificPurposes">The specific purposes for the protected data.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="applicationName" /> is an empty string or <see langword="null" />.  
		/// -or-  
		/// <paramref name="primaryPurpose" /> is an empty string or <see langword="null" />.  
		/// -or-  
		/// <paramref name="specificPurposes" /> contains an empty string or <see langword="null" />.</exception>
		// Token: 0x06000046 RID: 70 RVA: 0x00002B8C File Offset: 0x00000D8C
		protected DataProtector(string applicationName, string primaryPurpose, string[] specificPurposes)
		{
			if (string.IsNullOrWhiteSpace(applicationName))
			{
				throw new ArgumentException("Invalid application name and/or purpose", "applicationName");
			}
			if (string.IsNullOrWhiteSpace(primaryPurpose))
			{
				throw new ArgumentException("Invalid application name and/or purpose", "primaryPurpose");
			}
			if (specificPurposes != null)
			{
				for (int i = 0; i < specificPurposes.Length; i++)
				{
					if (string.IsNullOrWhiteSpace(specificPurposes[i]))
					{
						throw new ArgumentException("Invalid application name and/or purpose", "specificPurposes");
					}
				}
			}
			this.m_applicationName = applicationName;
			this.m_primaryPurpose = primaryPurpose;
			List<string> list = new List<string>();
			if (specificPurposes != null)
			{
				list.AddRange(specificPurposes);
			}
			this.m_specificPurposes = list;
		}

		/// <summary>Gets the name of the application.</summary>
		/// <returns>The name of the application.</returns>
		// Token: 0x1700000F RID: 15
		// (get) Token: 0x06000047 RID: 71 RVA: 0x00002C21 File Offset: 0x00000E21
		protected string ApplicationName
		{
			get
			{
				return this.m_applicationName;
			}
		}

		/// <summary>Specifies whether the hash is prepended to the text array before encryption.</summary>
		/// <returns>Always <see langword="true" />.</returns>
		// Token: 0x17000010 RID: 16
		// (get) Token: 0x06000048 RID: 72 RVA: 0x00002C29 File Offset: 0x00000E29
		protected virtual bool PrependHashedPurposeToPlaintext
		{
			get
			{
				return true;
			}
		}

		/// <summary>Creates a hash of the property values specified by the constructor.</summary>
		/// <returns>An array of bytes that contain the hash of the <see cref="P:System.Security.Cryptography.DataProtector.ApplicationName" />, <see cref="P:System.Security.Cryptography.DataProtector.PrimaryPurpose" />, and <see cref="P:System.Security.Cryptography.DataProtector.SpecificPurposes" /> properties.</returns>
		// Token: 0x06000049 RID: 73 RVA: 0x00002C2C File Offset: 0x00000E2C
		protected virtual byte[] GetHashedPurpose()
		{
			if (this.m_hashedPurpose == null)
			{
				using (HashAlgorithm hashAlgorithm = HashAlgorithm.Create("System.Security.Cryptography.Sha256Cng"))
				{
					using (BinaryWriter binaryWriter = new BinaryWriter(new CryptoStream(new MemoryStream(), hashAlgorithm, CryptoStreamMode.Write), new UTF8Encoding(false, true)))
					{
						binaryWriter.Write(this.ApplicationName);
						binaryWriter.Write(this.PrimaryPurpose);
						foreach (string value in this.SpecificPurposes)
						{
							binaryWriter.Write(value);
						}
					}
					this.m_hashedPurpose = hashAlgorithm.Hash;
				}
			}
			return this.m_hashedPurpose;
		}

		/// <summary>Determines if re-encryption is required for the specified encrypted data.</summary>
		/// <param name="encryptedData">The encrypted data to be evaluated.</param>
		/// <returns>
		///   <see langword="true" /> if the data must be re-encrypted; otherwise, <see langword="false" />.</returns>
		// Token: 0x0600004A RID: 74
		public abstract bool IsReprotectRequired(byte[] encryptedData);

		/// <summary>Gets the primary purpose for the protected data.</summary>
		/// <returns>The primary purpose for the protected data.</returns>
		// Token: 0x17000011 RID: 17
		// (get) Token: 0x0600004B RID: 75 RVA: 0x00002D08 File Offset: 0x00000F08
		protected string PrimaryPurpose
		{
			get
			{
				return this.m_primaryPurpose;
			}
		}

		/// <summary>Gets the specific purposes for the protected data.</summary>
		/// <returns>A collection of the specific purposes for the protected data.</returns>
		// Token: 0x17000012 RID: 18
		// (get) Token: 0x0600004C RID: 76 RVA: 0x00002D10 File Offset: 0x00000F10
		protected IEnumerable<string> SpecificPurposes
		{
			get
			{
				return this.m_specificPurposes;
			}
		}

		/// <summary>Creates an instance of a data protector implementation by using the specified class name of the data protector, the application name, the primary purpose, and the specific purposes.</summary>
		/// <param name="providerClass">The class name for the data protector.</param>
		/// <param name="applicationName">The name of the application.</param>
		/// <param name="primaryPurpose">The primary purpose for the protected data.</param>
		/// <param name="specificPurposes">The specific purposes for the protected data.</param>
		/// <returns>A data protector implementation object.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="providerClass" /> is <see langword="null" />.</exception>
		// Token: 0x0600004D RID: 77 RVA: 0x00002D18 File Offset: 0x00000F18
		public static DataProtector Create(string providerClass, string applicationName, string primaryPurpose, params string[] specificPurposes)
		{
			if (providerClass == null)
			{
				throw new ArgumentNullException("providerClass");
			}
			return (DataProtector)CryptoConfig.CreateFromName(providerClass, new object[]
			{
				applicationName,
				primaryPurpose,
				specificPurposes
			});
		}

		/// <summary>Protects the specified user data.</summary>
		/// <param name="userData">The data to be protected.</param>
		/// <returns>A byte array that contains the encrypted data.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="userData" /> is <see langword="null" />.</exception>
		// Token: 0x0600004E RID: 78 RVA: 0x00002D48 File Offset: 0x00000F48
		public byte[] Protect(byte[] userData)
		{
			if (userData == null)
			{
				throw new ArgumentNullException("userData");
			}
			if (this.PrependHashedPurposeToPlaintext)
			{
				byte[] hashedPurpose = this.GetHashedPurpose();
				byte[] array = new byte[userData.Length + hashedPurpose.Length];
				Array.Copy(hashedPurpose, 0, array, 0, hashedPurpose.Length);
				Array.Copy(userData, 0, array, hashedPurpose.Length, userData.Length);
				userData = array;
			}
			return this.ProviderProtect(userData);
		}

		/// <summary>Specifies the delegate method in the derived class that the <see cref="M:System.Security.Cryptography.DataProtector.Protect(System.Byte[])" /> method in the base class calls back into.</summary>
		/// <param name="userData">The data to be encrypted.</param>
		/// <returns>A byte array that contains the encrypted data.</returns>
		// Token: 0x0600004F RID: 79
		protected abstract byte[] ProviderProtect(byte[] userData);

		/// <summary>Specifies the delegate method in the derived class that the <see cref="M:System.Security.Cryptography.DataProtector.Unprotect(System.Byte[])" /> method in the base class calls back into.</summary>
		/// <param name="encryptedData">The data to be unencrypted.</param>
		/// <returns>The unencrypted data.</returns>
		// Token: 0x06000050 RID: 80
		protected abstract byte[] ProviderUnprotect(byte[] encryptedData);

		/// <summary>Unprotects the specified protected data.</summary>
		/// <param name="encryptedData">The encrypted data to be unprotected.</param>
		/// <returns>A byte array that contains the plain-text data.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="encryptedData" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">
		///   <paramref name="encryptedData" /> contained an invalid purpose.</exception>
		// Token: 0x06000051 RID: 81 RVA: 0x00002DA4 File Offset: 0x00000FA4
		[MethodImpl(MethodImplOptions.NoInlining | MethodImplOptions.NoOptimization)]
		public byte[] Unprotect(byte[] encryptedData)
		{
			if (encryptedData == null)
			{
				throw new ArgumentNullException("encryptedData");
			}
			if (!this.PrependHashedPurposeToPlaintext)
			{
				return this.ProviderUnprotect(encryptedData);
			}
			byte[] array = this.ProviderUnprotect(encryptedData);
			byte[] hashedPurpose = this.GetHashedPurpose();
			bool flag = array.Length >= hashedPurpose.Length;
			for (int i = 0; i < hashedPurpose.Length; i++)
			{
				if (hashedPurpose[i] != array[i % array.Length])
				{
					flag = false;
				}
			}
			if (!flag)
			{
				throw new CryptographicException("Invalid data protection purpose");
			}
			byte[] array2 = new byte[array.Length - hashedPurpose.Length];
			Array.Copy(array, hashedPurpose.Length, array2, 0, array2.Length);
			return array2;
		}

		// Token: 0x04000089 RID: 137
		private string m_applicationName;

		// Token: 0x0400008A RID: 138
		private string m_primaryPurpose;

		// Token: 0x0400008B RID: 139
		private IEnumerable<string> m_specificPurposes;

		// Token: 0x0400008C RID: 140
		private volatile byte[] m_hashedPurpose;
	}
}
