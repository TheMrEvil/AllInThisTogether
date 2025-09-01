using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security.Cryptography;
using System.Security.Permissions;
using Mono.Security;
using Mono.Security.Cryptography;

namespace System.Reflection
{
	/// <summary>Encapsulates access to a public or private key pair used to sign strong name assemblies.</summary>
	// Token: 0x02000905 RID: 2309
	[ComVisible(true)]
	[Serializable]
	public class StrongNameKeyPair : ISerializable, IDeserializationCallback
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Reflection.StrongNameKeyPair" /> class, building the key pair from a <see langword="byte" /> array.</summary>
		/// <param name="keyPairArray">An array of type <see langword="byte" /> containing the key pair.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="keyPairArray" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission.</exception>
		// Token: 0x06004E44 RID: 20036 RVA: 0x000F5DDE File Offset: 0x000F3FDE
		[SecurityPermission(SecurityAction.Demand, UnmanagedCode = true)]
		public StrongNameKeyPair(byte[] keyPairArray)
		{
			if (keyPairArray == null)
			{
				throw new ArgumentNullException("keyPairArray");
			}
			this.LoadKey(keyPairArray);
			this.GetRSA();
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Reflection.StrongNameKeyPair" /> class, building the key pair from a <see langword="FileStream" />.</summary>
		/// <param name="keyPairFile">A <see langword="FileStream" /> containing the key pair.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="keyPairFile" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission.</exception>
		// Token: 0x06004E45 RID: 20037 RVA: 0x000F5E04 File Offset: 0x000F4004
		[SecurityPermission(SecurityAction.Demand, UnmanagedCode = true)]
		public StrongNameKeyPair(FileStream keyPairFile)
		{
			if (keyPairFile == null)
			{
				throw new ArgumentNullException("keyPairFile");
			}
			byte[] array = new byte[keyPairFile.Length];
			keyPairFile.Read(array, 0, array.Length);
			this.LoadKey(array);
			this.GetRSA();
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Reflection.StrongNameKeyPair" /> class, building the key pair from a <see langword="String" />.</summary>
		/// <param name="keyPairContainer">A string containing the key pair.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="keyPairContainer" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission.</exception>
		// Token: 0x06004E46 RID: 20038 RVA: 0x000F5E4C File Offset: 0x000F404C
		[SecurityPermission(SecurityAction.Demand, UnmanagedCode = true)]
		public StrongNameKeyPair(string keyPairContainer)
		{
			if (keyPairContainer == null)
			{
				throw new ArgumentNullException("keyPairContainer");
			}
			this._keyPairContainer = keyPairContainer;
			this.GetRSA();
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Reflection.StrongNameKeyPair" /> class, building the key pair from serialized data.</summary>
		/// <param name="info">A <see cref="T:System.Runtime.Serialization.SerializationInfo" /> object that holds the serialized object data.</param>
		/// <param name="context">A <see cref="T:System.Runtime.Serialization.StreamingContext" /> object that contains contextual information about the source or destination.</param>
		// Token: 0x06004E47 RID: 20039 RVA: 0x000F5E70 File Offset: 0x000F4070
		protected StrongNameKeyPair(SerializationInfo info, StreamingContext context)
		{
			this._publicKey = (byte[])info.GetValue("_publicKey", typeof(byte[]));
			this._keyPairContainer = info.GetString("_keyPairContainer");
			this._keyPairExported = info.GetBoolean("_keyPairExported");
			this._keyPairArray = (byte[])info.GetValue("_keyPairArray", typeof(byte[]));
		}

		/// <summary>Sets the <see cref="T:System.Runtime.Serialization.SerializationInfo" /> object with all the data required to reinstantiate the current <see cref="T:System.Reflection.StrongNameKeyPair" /> object.</summary>
		/// <param name="info">The object to be populated with serialization information.</param>
		/// <param name="context">The destination context of the serialization.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="info" /> is <see langword="null" />.</exception>
		// Token: 0x06004E48 RID: 20040 RVA: 0x000F5EE8 File Offset: 0x000F40E8
		void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
		{
			info.AddValue("_publicKey", this._publicKey, typeof(byte[]));
			info.AddValue("_keyPairContainer", this._keyPairContainer);
			info.AddValue("_keyPairExported", this._keyPairExported);
			info.AddValue("_keyPairArray", this._keyPairArray, typeof(byte[]));
		}

		/// <summary>Runs when the entire object graph has been deserialized.</summary>
		/// <param name="sender">The object that initiated the callback.</param>
		// Token: 0x06004E49 RID: 20041 RVA: 0x00004BF9 File Offset: 0x00002DF9
		void IDeserializationCallback.OnDeserialization(object sender)
		{
		}

		// Token: 0x06004E4A RID: 20042 RVA: 0x000F5F50 File Offset: 0x000F4150
		private RSA GetRSA()
		{
			if (this._rsa != null)
			{
				return this._rsa;
			}
			if (this._keyPairArray != null)
			{
				try
				{
					this._rsa = CryptoConvert.FromCapiKeyBlob(this._keyPairArray);
					goto IL_5A;
				}
				catch
				{
					this._keyPairArray = null;
					goto IL_5A;
				}
			}
			if (this._keyPairContainer != null)
			{
				this._rsa = new RSACryptoServiceProvider(new CspParameters
				{
					KeyContainerName = this._keyPairContainer
				});
			}
			IL_5A:
			return this._rsa;
		}

		// Token: 0x06004E4B RID: 20043 RVA: 0x000F5FD0 File Offset: 0x000F41D0
		private void LoadKey(byte[] key)
		{
			try
			{
				if (key.Length == 16)
				{
					int i = 0;
					int num = 0;
					while (i < key.Length)
					{
						num += (int)key[i++];
					}
					if (num == 4)
					{
						this._publicKey = (byte[])key.Clone();
					}
				}
				else
				{
					this._keyPairArray = key;
				}
			}
			catch
			{
			}
		}

		/// <summary>Gets the public part of the public key or public key token of the key pair.</summary>
		/// <returns>An array of type <see langword="byte" /> containing the public key or public key token of the key pair.</returns>
		// Token: 0x17000CC7 RID: 3271
		// (get) Token: 0x06004E4C RID: 20044 RVA: 0x000F6030 File Offset: 0x000F4230
		public byte[] PublicKey
		{
			get
			{
				if (this._publicKey == null)
				{
					RSA rsa = this.GetRSA();
					if (rsa == null)
					{
						throw new ArgumentException("invalid keypair");
					}
					byte[] array = CryptoConvert.ToCapiKeyBlob(rsa, false);
					this._publicKey = new byte[array.Length + 12];
					this._publicKey[0] = 0;
					this._publicKey[1] = 36;
					this._publicKey[2] = 0;
					this._publicKey[3] = 0;
					this._publicKey[4] = 4;
					this._publicKey[5] = 128;
					this._publicKey[6] = 0;
					this._publicKey[7] = 0;
					int num = array.Length;
					this._publicKey[8] = (byte)(num % 256);
					this._publicKey[9] = (byte)(num / 256);
					this._publicKey[10] = 0;
					this._publicKey[11] = 0;
					Buffer.BlockCopy(array, 0, this._publicKey, 12, array.Length);
				}
				return this._publicKey;
			}
		}

		// Token: 0x06004E4D RID: 20045 RVA: 0x000F6114 File Offset: 0x000F4314
		internal StrongName StrongName()
		{
			RSA rsa = this.GetRSA();
			if (rsa != null)
			{
				return new StrongName(rsa);
			}
			if (this._publicKey != null)
			{
				return new StrongName(this._publicKey);
			}
			return null;
		}

		// Token: 0x0400307E RID: 12414
		private byte[] _publicKey;

		// Token: 0x0400307F RID: 12415
		private string _keyPairContainer;

		// Token: 0x04003080 RID: 12416
		private bool _keyPairExported;

		// Token: 0x04003081 RID: 12417
		private byte[] _keyPairArray;

		// Token: 0x04003082 RID: 12418
		[NonSerialized]
		private RSA _rsa;
	}
}
