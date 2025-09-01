using System;
using System.Globalization;
using System.IO;
using System.Runtime.CompilerServices;
using System.Security;
using System.Security.Cryptography;
using System.Text;
using Mono.Xml;

namespace Mono.Security.Cryptography
{
	// Token: 0x02000056 RID: 86
	public class KeyPairPersistence
	{
		// Token: 0x0600033D RID: 829 RVA: 0x00010F70 File Offset: 0x0000F170
		public KeyPairPersistence(CspParameters parameters) : this(parameters, null)
		{
		}

		// Token: 0x0600033E RID: 830 RVA: 0x00010F7A File Offset: 0x0000F17A
		public KeyPairPersistence(CspParameters parameters, string keyPair)
		{
			if (parameters == null)
			{
				throw new ArgumentNullException("parameters");
			}
			this._params = this.Copy(parameters);
			this._keyvalue = keyPair;
		}

		// Token: 0x170000F2 RID: 242
		// (get) Token: 0x0600033F RID: 831 RVA: 0x00010FA4 File Offset: 0x0000F1A4
		public string Filename
		{
			get
			{
				if (this._filename == null)
				{
					this._filename = string.Format(CultureInfo.InvariantCulture, "[{0}][{1}][{2}].xml", this._params.ProviderType, this.ContainerName, this._params.KeyNumber);
					if (this.UseMachineKeyStore)
					{
						this._filename = Path.Combine(KeyPairPersistence.MachinePath, this._filename);
					}
					else
					{
						this._filename = Path.Combine(KeyPairPersistence.UserPath, this._filename);
					}
				}
				return this._filename;
			}
		}

		// Token: 0x170000F3 RID: 243
		// (get) Token: 0x06000340 RID: 832 RVA: 0x00011030 File Offset: 0x0000F230
		// (set) Token: 0x06000341 RID: 833 RVA: 0x00011038 File Offset: 0x0000F238
		public string KeyValue
		{
			get
			{
				return this._keyvalue;
			}
			set
			{
				if (this.CanChange)
				{
					this._keyvalue = value;
				}
			}
		}

		// Token: 0x170000F4 RID: 244
		// (get) Token: 0x06000342 RID: 834 RVA: 0x00011049 File Offset: 0x0000F249
		public CspParameters Parameters
		{
			get
			{
				return this.Copy(this._params);
			}
		}

		// Token: 0x06000343 RID: 835 RVA: 0x00011058 File Offset: 0x0000F258
		public bool Load()
		{
			bool flag = File.Exists(this.Filename);
			if (flag)
			{
				using (StreamReader streamReader = File.OpenText(this.Filename))
				{
					this.FromXml(streamReader.ReadToEnd());
				}
			}
			return flag;
		}

		// Token: 0x06000344 RID: 836 RVA: 0x000110AC File Offset: 0x0000F2AC
		public void Save()
		{
			using (FileStream fileStream = File.Open(this.Filename, FileMode.Create))
			{
				StreamWriter streamWriter = new StreamWriter(fileStream, Encoding.UTF8);
				streamWriter.Write(this.ToXml());
				streamWriter.Close();
			}
			if (this.UseMachineKeyStore)
			{
				KeyPairPersistence.ProtectMachine(this.Filename);
				return;
			}
			KeyPairPersistence.ProtectUser(this.Filename);
		}

		// Token: 0x06000345 RID: 837 RVA: 0x00011120 File Offset: 0x0000F320
		public void Remove()
		{
			File.Delete(this.Filename);
		}

		// Token: 0x170000F5 RID: 245
		// (get) Token: 0x06000346 RID: 838 RVA: 0x00011130 File Offset: 0x0000F330
		private static string UserPath
		{
			get
			{
				object obj = KeyPairPersistence.lockobj;
				lock (obj)
				{
					if (KeyPairPersistence._userPath == null || !KeyPairPersistence._userPathExists)
					{
						KeyPairPersistence._userPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), ".mono");
						KeyPairPersistence._userPath = Path.Combine(KeyPairPersistence._userPath, "keypairs");
						KeyPairPersistence._userPathExists = Directory.Exists(KeyPairPersistence._userPath);
						if (!KeyPairPersistence._userPathExists)
						{
							try
							{
								Directory.CreateDirectory(KeyPairPersistence._userPath);
							}
							catch (Exception inner)
							{
								throw new CryptographicException(string.Format(Locale.GetText("Could not create user key store '{0}'."), KeyPairPersistence._userPath), inner);
							}
							KeyPairPersistence._userPathExists = true;
						}
					}
					if (!KeyPairPersistence.IsUserProtected(KeyPairPersistence._userPath) && !KeyPairPersistence.ProtectUser(KeyPairPersistence._userPath))
					{
						throw new IOException(string.Format(Locale.GetText("Could not secure user key store '{0}'."), KeyPairPersistence._userPath));
					}
				}
				if (!KeyPairPersistence.IsUserProtected(KeyPairPersistence._userPath))
				{
					throw new CryptographicException(string.Format(Locale.GetText("Improperly protected user's key pairs in '{0}'."), KeyPairPersistence._userPath));
				}
				return KeyPairPersistence._userPath;
			}
		}

		// Token: 0x170000F6 RID: 246
		// (get) Token: 0x06000347 RID: 839 RVA: 0x00011250 File Offset: 0x0000F450
		private static string MachinePath
		{
			get
			{
				object obj = KeyPairPersistence.lockobj;
				lock (obj)
				{
					if (KeyPairPersistence._machinePath == null || !KeyPairPersistence._machinePathExists)
					{
						KeyPairPersistence._machinePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData), ".mono");
						KeyPairPersistence._machinePath = Path.Combine(KeyPairPersistence._machinePath, "keypairs");
						KeyPairPersistence._machinePathExists = Directory.Exists(KeyPairPersistence._machinePath);
						if (!KeyPairPersistence._machinePathExists)
						{
							try
							{
								Directory.CreateDirectory(KeyPairPersistence._machinePath);
							}
							catch (Exception inner)
							{
								throw new CryptographicException(string.Format(Locale.GetText("Could not create machine key store '{0}'."), KeyPairPersistence._machinePath), inner);
							}
							KeyPairPersistence._machinePathExists = true;
						}
					}
					if (!KeyPairPersistence.IsMachineProtected(KeyPairPersistence._machinePath) && !KeyPairPersistence.ProtectMachine(KeyPairPersistence._machinePath))
					{
						throw new IOException(string.Format(Locale.GetText("Could not secure machine key store '{0}'."), KeyPairPersistence._machinePath));
					}
				}
				if (!KeyPairPersistence.IsMachineProtected(KeyPairPersistence._machinePath))
				{
					throw new CryptographicException(string.Format(Locale.GetText("Improperly protected machine's key pairs in '{0}'."), KeyPairPersistence._machinePath));
				}
				return KeyPairPersistence._machinePath;
			}
		}

		// Token: 0x06000348 RID: 840 RVA: 0x00011370 File Offset: 0x0000F570
		internal unsafe static bool _CanSecure(char* root)
		{
			return true;
		}

		// Token: 0x06000349 RID: 841 RVA: 0x00011373 File Offset: 0x0000F573
		internal unsafe static bool _ProtectUser(char* path)
		{
			return true;
		}

		// Token: 0x0600034A RID: 842 RVA: 0x00011376 File Offset: 0x0000F576
		internal unsafe static bool _ProtectMachine(char* path)
		{
			return true;
		}

		// Token: 0x0600034B RID: 843 RVA: 0x00011379 File Offset: 0x0000F579
		internal unsafe static bool _IsUserProtected(char* path)
		{
			return true;
		}

		// Token: 0x0600034C RID: 844 RVA: 0x0001137C File Offset: 0x0000F57C
		internal unsafe static bool _IsMachineProtected(char* path)
		{
			return true;
		}

		// Token: 0x0600034D RID: 845 RVA: 0x00011380 File Offset: 0x0000F580
		private unsafe static bool CanSecure(string path)
		{
			int platform = (int)Environment.OSVersion.Platform;
			if (platform == 4 || platform == 128 || platform == 6)
			{
				return true;
			}
			char* ptr = path;
			if (ptr != null)
			{
				ptr += RuntimeHelpers.OffsetToStringData / 2;
			}
			return KeyPairPersistence._CanSecure(ptr);
		}

		// Token: 0x0600034E RID: 846 RVA: 0x000113C0 File Offset: 0x0000F5C0
		private unsafe static bool ProtectUser(string path)
		{
			if (KeyPairPersistence.CanSecure(path))
			{
				char* ptr = path;
				if (ptr != null)
				{
					ptr += RuntimeHelpers.OffsetToStringData / 2;
				}
				return KeyPairPersistence._ProtectUser(ptr);
			}
			return true;
		}

		// Token: 0x0600034F RID: 847 RVA: 0x000113F0 File Offset: 0x0000F5F0
		private unsafe static bool ProtectMachine(string path)
		{
			if (KeyPairPersistence.CanSecure(path))
			{
				char* ptr = path;
				if (ptr != null)
				{
					ptr += RuntimeHelpers.OffsetToStringData / 2;
				}
				return KeyPairPersistence._ProtectMachine(ptr);
			}
			return true;
		}

		// Token: 0x06000350 RID: 848 RVA: 0x00011420 File Offset: 0x0000F620
		private unsafe static bool IsUserProtected(string path)
		{
			if (KeyPairPersistence.CanSecure(path))
			{
				char* ptr = path;
				if (ptr != null)
				{
					ptr += RuntimeHelpers.OffsetToStringData / 2;
				}
				return KeyPairPersistence._IsUserProtected(ptr);
			}
			return true;
		}

		// Token: 0x06000351 RID: 849 RVA: 0x00011450 File Offset: 0x0000F650
		private unsafe static bool IsMachineProtected(string path)
		{
			if (KeyPairPersistence.CanSecure(path))
			{
				char* ptr = path;
				if (ptr != null)
				{
					ptr += RuntimeHelpers.OffsetToStringData / 2;
				}
				return KeyPairPersistence._IsMachineProtected(ptr);
			}
			return true;
		}

		// Token: 0x170000F7 RID: 247
		// (get) Token: 0x06000352 RID: 850 RVA: 0x0001147D File Offset: 0x0000F67D
		private bool CanChange
		{
			get
			{
				return this._keyvalue == null;
			}
		}

		// Token: 0x170000F8 RID: 248
		// (get) Token: 0x06000353 RID: 851 RVA: 0x00011488 File Offset: 0x0000F688
		private bool UseDefaultKeyContainer
		{
			get
			{
				return (this._params.Flags & CspProviderFlags.UseDefaultKeyContainer) == CspProviderFlags.UseDefaultKeyContainer;
			}
		}

		// Token: 0x170000F9 RID: 249
		// (get) Token: 0x06000354 RID: 852 RVA: 0x0001149A File Offset: 0x0000F69A
		private bool UseMachineKeyStore
		{
			get
			{
				return (this._params.Flags & CspProviderFlags.UseMachineKeyStore) == CspProviderFlags.UseMachineKeyStore;
			}
		}

		// Token: 0x170000FA RID: 250
		// (get) Token: 0x06000355 RID: 853 RVA: 0x000114AC File Offset: 0x0000F6AC
		private string ContainerName
		{
			get
			{
				if (this._container == null)
				{
					if (this.UseDefaultKeyContainer)
					{
						this._container = "default";
					}
					else if (this._params.KeyContainerName == null || this._params.KeyContainerName.Length == 0)
					{
						this._container = Guid.NewGuid().ToString();
					}
					else
					{
						byte[] bytes = Encoding.UTF8.GetBytes(this._params.KeyContainerName);
						byte[] b = MD5.Create().ComputeHash(bytes);
						this._container = new Guid(b).ToString();
					}
				}
				return this._container;
			}
		}

		// Token: 0x06000356 RID: 854 RVA: 0x00011555 File Offset: 0x0000F755
		private CspParameters Copy(CspParameters p)
		{
			return new CspParameters(p.ProviderType, p.ProviderName, p.KeyContainerName)
			{
				KeyNumber = p.KeyNumber,
				Flags = p.Flags
			};
		}

		// Token: 0x06000357 RID: 855 RVA: 0x00011588 File Offset: 0x0000F788
		private void FromXml(string xml)
		{
			SecurityParser securityParser = new SecurityParser();
			securityParser.LoadXml(xml);
			SecurityElement securityElement = securityParser.ToXml();
			if (securityElement.Tag == "KeyPair")
			{
				SecurityElement securityElement2 = securityElement.SearchForChildByTag("KeyValue");
				if (securityElement2.Children.Count > 0)
				{
					this._keyvalue = securityElement2.Children[0].ToString();
				}
			}
		}

		// Token: 0x06000358 RID: 856 RVA: 0x000115EC File Offset: 0x0000F7EC
		private string ToXml()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.AppendFormat("<KeyPair>{0}\t<Properties>{0}\t\t<Provider ", Environment.NewLine);
			if (this._params.ProviderName != null && this._params.ProviderName.Length != 0)
			{
				stringBuilder.AppendFormat("Name=\"{0}\" ", this._params.ProviderName);
			}
			stringBuilder.AppendFormat("Type=\"{0}\" />{1}\t\t<Container ", this._params.ProviderType, Environment.NewLine);
			stringBuilder.AppendFormat("Name=\"{0}\" />{1}\t</Properties>{1}\t<KeyValue", this.ContainerName, Environment.NewLine);
			if (this._params.KeyNumber != -1)
			{
				stringBuilder.AppendFormat(" Id=\"{0}\" ", this._params.KeyNumber);
			}
			stringBuilder.AppendFormat(">{1}\t\t{0}{1}\t</KeyValue>{1}</KeyPair>{1}", this.KeyValue, Environment.NewLine);
			return stringBuilder.ToString();
		}

		// Token: 0x06000359 RID: 857 RVA: 0x000116C5 File Offset: 0x0000F8C5
		// Note: this type is marked as 'beforefieldinit'.
		static KeyPairPersistence()
		{
		}

		// Token: 0x040002B6 RID: 694
		private static bool _userPathExists;

		// Token: 0x040002B7 RID: 695
		private static string _userPath;

		// Token: 0x040002B8 RID: 696
		private static bool _machinePathExists;

		// Token: 0x040002B9 RID: 697
		private static string _machinePath;

		// Token: 0x040002BA RID: 698
		private CspParameters _params;

		// Token: 0x040002BB RID: 699
		private string _keyvalue;

		// Token: 0x040002BC RID: 700
		private string _filename;

		// Token: 0x040002BD RID: 701
		private string _container;

		// Token: 0x040002BE RID: 702
		private static object lockobj = new object();
	}
}
