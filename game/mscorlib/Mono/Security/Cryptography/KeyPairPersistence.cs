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
	// Token: 0x02000089 RID: 137
	internal class KeyPairPersistence
	{
		// Token: 0x060002DE RID: 734 RVA: 0x0000F1FB File Offset: 0x0000D3FB
		public KeyPairPersistence(CspParameters parameters) : this(parameters, null)
		{
		}

		// Token: 0x060002DF RID: 735 RVA: 0x0000F205 File Offset: 0x0000D405
		public KeyPairPersistence(CspParameters parameters, string keyPair)
		{
			if (parameters == null)
			{
				throw new ArgumentNullException("parameters");
			}
			this._params = this.Copy(parameters);
			this._keyvalue = keyPair;
		}

		// Token: 0x1700004A RID: 74
		// (get) Token: 0x060002E0 RID: 736 RVA: 0x0000F230 File Offset: 0x0000D430
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

		// Token: 0x1700004B RID: 75
		// (get) Token: 0x060002E1 RID: 737 RVA: 0x0000F2BC File Offset: 0x0000D4BC
		// (set) Token: 0x060002E2 RID: 738 RVA: 0x0000F2C4 File Offset: 0x0000D4C4
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

		// Token: 0x1700004C RID: 76
		// (get) Token: 0x060002E3 RID: 739 RVA: 0x0000F2D5 File Offset: 0x0000D4D5
		public CspParameters Parameters
		{
			get
			{
				return this.Copy(this._params);
			}
		}

		// Token: 0x060002E4 RID: 740 RVA: 0x0000F2E4 File Offset: 0x0000D4E4
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

		// Token: 0x060002E5 RID: 741 RVA: 0x0000F338 File Offset: 0x0000D538
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

		// Token: 0x060002E6 RID: 742 RVA: 0x0000F3AC File Offset: 0x0000D5AC
		public void Remove()
		{
			File.Delete(this.Filename);
		}

		// Token: 0x1700004D RID: 77
		// (get) Token: 0x060002E7 RID: 743 RVA: 0x0000F3BC File Offset: 0x0000D5BC
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

		// Token: 0x1700004E RID: 78
		// (get) Token: 0x060002E8 RID: 744 RVA: 0x0000F4DC File Offset: 0x0000D6DC
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

		// Token: 0x060002E9 RID: 745
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal unsafe static extern bool _CanSecure(char* root);

		// Token: 0x060002EA RID: 746
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal unsafe static extern bool _ProtectUser(char* path);

		// Token: 0x060002EB RID: 747
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal unsafe static extern bool _ProtectMachine(char* path);

		// Token: 0x060002EC RID: 748
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal unsafe static extern bool _IsUserProtected(char* path);

		// Token: 0x060002ED RID: 749
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal unsafe static extern bool _IsMachineProtected(char* path);

		// Token: 0x060002EE RID: 750 RVA: 0x0000F5FC File Offset: 0x0000D7FC
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

		// Token: 0x060002EF RID: 751 RVA: 0x0000F63C File Offset: 0x0000D83C
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

		// Token: 0x060002F0 RID: 752 RVA: 0x0000F66C File Offset: 0x0000D86C
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

		// Token: 0x060002F1 RID: 753 RVA: 0x0000F69C File Offset: 0x0000D89C
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

		// Token: 0x060002F2 RID: 754 RVA: 0x0000F6CC File Offset: 0x0000D8CC
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

		// Token: 0x1700004F RID: 79
		// (get) Token: 0x060002F3 RID: 755 RVA: 0x0000F6F9 File Offset: 0x0000D8F9
		private bool CanChange
		{
			get
			{
				return this._keyvalue == null;
			}
		}

		// Token: 0x17000050 RID: 80
		// (get) Token: 0x060002F4 RID: 756 RVA: 0x0000F704 File Offset: 0x0000D904
		private bool UseDefaultKeyContainer
		{
			get
			{
				return (this._params.Flags & CspProviderFlags.UseDefaultKeyContainer) == CspProviderFlags.UseDefaultKeyContainer;
			}
		}

		// Token: 0x17000051 RID: 81
		// (get) Token: 0x060002F5 RID: 757 RVA: 0x0000F716 File Offset: 0x0000D916
		private bool UseMachineKeyStore
		{
			get
			{
				return (this._params.Flags & CspProviderFlags.UseMachineKeyStore) == CspProviderFlags.UseMachineKeyStore;
			}
		}

		// Token: 0x17000052 RID: 82
		// (get) Token: 0x060002F6 RID: 758 RVA: 0x0000F728 File Offset: 0x0000D928
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

		// Token: 0x060002F7 RID: 759 RVA: 0x0000F7D1 File Offset: 0x0000D9D1
		private CspParameters Copy(CspParameters p)
		{
			return new CspParameters(p.ProviderType, p.ProviderName, p.KeyContainerName)
			{
				KeyNumber = p.KeyNumber,
				Flags = p.Flags
			};
		}

		// Token: 0x060002F8 RID: 760 RVA: 0x0000F804 File Offset: 0x0000DA04
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

		// Token: 0x060002F9 RID: 761 RVA: 0x0000F868 File Offset: 0x0000DA68
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

		// Token: 0x060002FA RID: 762 RVA: 0x0000F941 File Offset: 0x0000DB41
		// Note: this type is marked as 'beforefieldinit'.
		static KeyPairPersistence()
		{
		}

		// Token: 0x04000EDB RID: 3803
		private static bool _userPathExists;

		// Token: 0x04000EDC RID: 3804
		private static string _userPath;

		// Token: 0x04000EDD RID: 3805
		private static bool _machinePathExists;

		// Token: 0x04000EDE RID: 3806
		private static string _machinePath;

		// Token: 0x04000EDF RID: 3807
		private CspParameters _params;

		// Token: 0x04000EE0 RID: 3808
		private string _keyvalue;

		// Token: 0x04000EE1 RID: 3809
		private string _filename;

		// Token: 0x04000EE2 RID: 3810
		private string _container;

		// Token: 0x04000EE3 RID: 3811
		private static object lockobj = new object();
	}
}
