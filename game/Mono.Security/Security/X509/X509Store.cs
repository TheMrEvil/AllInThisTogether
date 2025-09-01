using System;
using System.Collections;
using System.Globalization;
using System.IO;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using Mono.Security.Cryptography;
using Mono.Security.X509.Extensions;

namespace Mono.Security.X509
{
	// Token: 0x0200001A RID: 26
	public class X509Store
	{
		// Token: 0x06000153 RID: 339 RVA: 0x0000ABAC File Offset: 0x00008DAC
		internal X509Store(string path, bool crl, bool newFormat)
		{
			this._storePath = path;
			this._crl = crl;
			this._newFormat = newFormat;
		}

		// Token: 0x1700004D RID: 77
		// (get) Token: 0x06000154 RID: 340 RVA: 0x0000ABC9 File Offset: 0x00008DC9
		public X509CertificateCollection Certificates
		{
			get
			{
				if (this._certificates == null)
				{
					this._certificates = this.BuildCertificatesCollection(this._storePath);
				}
				return this._certificates;
			}
		}

		// Token: 0x1700004E RID: 78
		// (get) Token: 0x06000155 RID: 341 RVA: 0x0000ABEB File Offset: 0x00008DEB
		public ArrayList Crls
		{
			get
			{
				if (!this._crl)
				{
					this._crls = new ArrayList();
				}
				if (this._crls == null)
				{
					this._crls = this.BuildCrlsCollection(this._storePath);
				}
				return this._crls;
			}
		}

		// Token: 0x1700004F RID: 79
		// (get) Token: 0x06000156 RID: 342 RVA: 0x0000AC20 File Offset: 0x00008E20
		public string Name
		{
			get
			{
				if (this._name == null)
				{
					int num = this._storePath.LastIndexOf(Path.DirectorySeparatorChar);
					this._name = this._storePath.Substring(num + 1);
				}
				return this._name;
			}
		}

		// Token: 0x06000157 RID: 343 RVA: 0x0000AC60 File Offset: 0x00008E60
		public void Clear()
		{
			this.ClearCertificates();
			this.ClearCrls();
		}

		// Token: 0x06000158 RID: 344 RVA: 0x0000AC6E File Offset: 0x00008E6E
		private void ClearCertificates()
		{
			if (this._certificates != null)
			{
				this._certificates.Clear();
			}
			this._certificates = null;
		}

		// Token: 0x06000159 RID: 345 RVA: 0x0000AC8A File Offset: 0x00008E8A
		private void ClearCrls()
		{
			if (this._crls != null)
			{
				this._crls.Clear();
			}
			this._crls = null;
		}

		// Token: 0x0600015A RID: 346 RVA: 0x0000ACA8 File Offset: 0x00008EA8
		public void Import(X509Certificate certificate)
		{
			this.CheckStore(this._storePath, true);
			if (this._newFormat)
			{
				this.ImportNewFormat(certificate);
				return;
			}
			string text = Path.Combine(this._storePath, this.GetUniqueName(certificate, null));
			if (!File.Exists(text))
			{
				text = Path.Combine(this._storePath, this.GetUniqueNameWithSerial(certificate));
				if (!File.Exists(text))
				{
					using (FileStream fileStream = File.Create(text))
					{
						byte[] rawData = certificate.RawData;
						fileStream.Write(rawData, 0, rawData.Length);
						fileStream.Close();
					}
					this.ClearCertificates();
				}
			}
			else
			{
				string path = Path.Combine(this._storePath, this.GetUniqueNameWithSerial(certificate));
				if (this.GetUniqueNameWithSerial(this.LoadCertificate(text)) != this.GetUniqueNameWithSerial(certificate))
				{
					using (FileStream fileStream2 = File.Create(path))
					{
						byte[] rawData2 = certificate.RawData;
						fileStream2.Write(rawData2, 0, rawData2.Length);
						fileStream2.Close();
					}
					this.ClearCertificates();
				}
			}
			CspParameters cspParameters = new CspParameters();
			cspParameters.KeyContainerName = CryptoConvert.ToHex(certificate.Hash);
			if (this._storePath.StartsWith(X509StoreManager.LocalMachinePath) || this._storePath.StartsWith(X509StoreManager.NewLocalMachinePath))
			{
				cspParameters.Flags = CspProviderFlags.UseMachineKeyStore;
			}
			this.ImportPrivateKey(certificate, cspParameters);
		}

		// Token: 0x0600015B RID: 347 RVA: 0x0000AE10 File Offset: 0x00009010
		public void Import(X509Crl crl)
		{
			this.CheckStore(this._storePath, true);
			if (this._newFormat)
			{
				throw new NotSupportedException();
			}
			string path = Path.Combine(this._storePath, this.GetUniqueName(crl));
			if (!File.Exists(path))
			{
				using (FileStream fileStream = File.Create(path))
				{
					byte[] rawData = crl.RawData;
					fileStream.Write(rawData, 0, rawData.Length);
				}
				this.ClearCrls();
			}
		}

		// Token: 0x0600015C RID: 348 RVA: 0x0000AE90 File Offset: 0x00009090
		public void Remove(X509Certificate certificate)
		{
			if (this._newFormat)
			{
				this.RemoveNewFormat(certificate);
				return;
			}
			string path = Path.Combine(this._storePath, this.GetUniqueNameWithSerial(certificate));
			if (File.Exists(path))
			{
				File.Delete(path);
				this.ClearCertificates();
				return;
			}
			path = Path.Combine(this._storePath, this.GetUniqueName(certificate, null));
			if (File.Exists(path))
			{
				File.Delete(path);
				this.ClearCertificates();
			}
		}

		// Token: 0x0600015D RID: 349 RVA: 0x0000AF00 File Offset: 0x00009100
		public void Remove(X509Crl crl)
		{
			if (this._newFormat)
			{
				throw new NotSupportedException();
			}
			string path = Path.Combine(this._storePath, this.GetUniqueName(crl));
			if (File.Exists(path))
			{
				File.Delete(path);
				this.ClearCrls();
			}
		}

		// Token: 0x0600015E RID: 350 RVA: 0x0000AF44 File Offset: 0x00009144
		private void ImportNewFormat(X509Certificate certificate)
		{
			using (X509Certificate x509Certificate = new X509Certificate(certificate.RawData))
			{
				long subjectNameHash = X509Helper2.GetSubjectNameHash(x509Certificate);
				string path = Path.Combine(this._storePath, string.Format("{0:x8}.0", subjectNameHash));
				if (!File.Exists(path))
				{
					using (FileStream fileStream = File.Create(path))
					{
						X509Helper2.ExportAsPEM(x509Certificate, fileStream, true);
					}
					this.ClearCertificates();
				}
			}
		}

		// Token: 0x0600015F RID: 351 RVA: 0x0000AFD4 File Offset: 0x000091D4
		private void RemoveNewFormat(X509Certificate certificate)
		{
			using (X509Certificate x509Certificate = new X509Certificate(certificate.RawData))
			{
				long subjectNameHash = X509Helper2.GetSubjectNameHash(x509Certificate);
				string path = Path.Combine(this._storePath, string.Format("{0:x8}.0", subjectNameHash));
				if (File.Exists(path))
				{
					File.Delete(path);
					this.ClearCertificates();
				}
			}
		}

		// Token: 0x06000160 RID: 352 RVA: 0x0000B040 File Offset: 0x00009240
		private string GetUniqueNameWithSerial(X509Certificate certificate)
		{
			return this.GetUniqueName(certificate, certificate.SerialNumber);
		}

		// Token: 0x06000161 RID: 353 RVA: 0x0000B050 File Offset: 0x00009250
		private string GetUniqueName(X509Certificate certificate, byte[] serial = null)
		{
			byte[] array = this.GetUniqueName(certificate.Extensions, serial);
			string method;
			if (array == null)
			{
				method = "tbp";
				array = certificate.Hash;
			}
			else
			{
				method = "ski";
			}
			return this.GetUniqueName(method, array, ".cer");
		}

		// Token: 0x06000162 RID: 354 RVA: 0x0000B090 File Offset: 0x00009290
		private string GetUniqueName(X509Crl crl)
		{
			byte[] array = this.GetUniqueName(crl.Extensions, null);
			string method;
			if (array == null)
			{
				method = "tbp";
				array = crl.Hash;
			}
			else
			{
				method = "ski";
			}
			return this.GetUniqueName(method, array, ".crl");
		}

		// Token: 0x06000163 RID: 355 RVA: 0x0000B0D0 File Offset: 0x000092D0
		private byte[] GetUniqueName(X509ExtensionCollection extensions, byte[] serial = null)
		{
			X509Extension x509Extension = extensions["2.5.29.14"];
			if (x509Extension == null)
			{
				return null;
			}
			SubjectKeyIdentifierExtension subjectKeyIdentifierExtension = new SubjectKeyIdentifierExtension(x509Extension);
			if (serial == null)
			{
				return subjectKeyIdentifierExtension.Identifier;
			}
			byte[] array = new byte[subjectKeyIdentifierExtension.Identifier.Length + serial.Length];
			Buffer.BlockCopy(subjectKeyIdentifierExtension.Identifier, 0, array, 0, subjectKeyIdentifierExtension.Identifier.Length);
			Buffer.BlockCopy(serial, 0, array, subjectKeyIdentifierExtension.Identifier.Length, serial.Length);
			return array;
		}

		// Token: 0x06000164 RID: 356 RVA: 0x0000B13C File Offset: 0x0000933C
		private string GetUniqueName(string method, byte[] name, string fileExtension)
		{
			StringBuilder stringBuilder = new StringBuilder(method);
			stringBuilder.Append("-");
			foreach (byte b in name)
			{
				stringBuilder.Append(b.ToString("X2", CultureInfo.InvariantCulture));
			}
			stringBuilder.Append(fileExtension);
			return stringBuilder.ToString();
		}

		// Token: 0x06000165 RID: 357 RVA: 0x0000B198 File Offset: 0x00009398
		private byte[] Load(string filename)
		{
			byte[] array = null;
			using (FileStream fileStream = File.OpenRead(filename))
			{
				array = new byte[fileStream.Length];
				fileStream.Read(array, 0, array.Length);
				fileStream.Close();
			}
			return array;
		}

		// Token: 0x06000166 RID: 358 RVA: 0x0000B1EC File Offset: 0x000093EC
		private X509Certificate LoadCertificate(string filename)
		{
			X509Certificate x509Certificate = new X509Certificate(this.Load(filename));
			CspParameters cspParameters = new CspParameters();
			cspParameters.KeyContainerName = CryptoConvert.ToHex(x509Certificate.Hash);
			if (this._storePath.StartsWith(X509StoreManager.LocalMachinePath) || this._storePath.StartsWith(X509StoreManager.NewLocalMachinePath))
			{
				cspParameters.Flags = CspProviderFlags.UseMachineKeyStore;
			}
			KeyPairPersistence keyPairPersistence = new KeyPairPersistence(cspParameters);
			try
			{
				if (!keyPairPersistence.Load())
				{
					return x509Certificate;
				}
			}
			catch
			{
				return x509Certificate;
			}
			if (x509Certificate.RSA != null)
			{
				x509Certificate.RSA = new RSACryptoServiceProvider(cspParameters);
			}
			else if (x509Certificate.DSA != null)
			{
				x509Certificate.DSA = new DSACryptoServiceProvider(cspParameters);
			}
			return x509Certificate;
		}

		// Token: 0x06000167 RID: 359 RVA: 0x0000B2A0 File Offset: 0x000094A0
		private X509Crl LoadCrl(string filename)
		{
			return new X509Crl(this.Load(filename));
		}

		// Token: 0x06000168 RID: 360 RVA: 0x0000B2B0 File Offset: 0x000094B0
		private bool CheckStore(string path, bool throwException)
		{
			bool result;
			try
			{
				if (Directory.Exists(path))
				{
					result = true;
				}
				else
				{
					Directory.CreateDirectory(path);
					result = Directory.Exists(path);
				}
			}
			catch
			{
				if (throwException)
				{
					throw;
				}
				result = false;
			}
			return result;
		}

		// Token: 0x06000169 RID: 361 RVA: 0x0000B2F4 File Offset: 0x000094F4
		private X509CertificateCollection BuildCertificatesCollection(string storeName)
		{
			X509CertificateCollection x509CertificateCollection = new X509CertificateCollection();
			string path = Path.Combine(this._storePath, storeName);
			if (!this.CheckStore(path, false))
			{
				return x509CertificateCollection;
			}
			string[] files = Directory.GetFiles(path, this._newFormat ? "*.0" : "*.cer");
			if (files != null && files.Length != 0)
			{
				foreach (string filename in files)
				{
					try
					{
						X509Certificate value = this.LoadCertificate(filename);
						x509CertificateCollection.Add(value);
					}
					catch
					{
					}
				}
			}
			return x509CertificateCollection;
		}

		// Token: 0x0600016A RID: 362 RVA: 0x0000B388 File Offset: 0x00009588
		private ArrayList BuildCrlsCollection(string storeName)
		{
			ArrayList arrayList = new ArrayList();
			string path = Path.Combine(this._storePath, storeName);
			if (!this.CheckStore(path, false))
			{
				return arrayList;
			}
			string[] files = Directory.GetFiles(path, "*.crl");
			if (files != null && files.Length != 0)
			{
				foreach (string filename in files)
				{
					try
					{
						X509Crl value = this.LoadCrl(filename);
						arrayList.Add(value);
					}
					catch
					{
					}
				}
			}
			return arrayList;
		}

		// Token: 0x0600016B RID: 363 RVA: 0x0000B40C File Offset: 0x0000960C
		private void ImportPrivateKey(X509Certificate certificate, CspParameters cspParams)
		{
			RSACryptoServiceProvider rsacryptoServiceProvider = certificate.RSA as RSACryptoServiceProvider;
			if (rsacryptoServiceProvider != null)
			{
				if (rsacryptoServiceProvider.PublicOnly)
				{
					return;
				}
				RSACryptoServiceProvider rsacryptoServiceProvider2 = new RSACryptoServiceProvider(cspParams);
				rsacryptoServiceProvider2.ImportParameters(rsacryptoServiceProvider.ExportParameters(true));
				rsacryptoServiceProvider2.PersistKeyInCsp = true;
				return;
			}
			else
			{
				RSAManaged rsamanaged = certificate.RSA as RSAManaged;
				if (rsamanaged == null)
				{
					DSACryptoServiceProvider dsacryptoServiceProvider = certificate.DSA as DSACryptoServiceProvider;
					if (dsacryptoServiceProvider != null)
					{
						if (dsacryptoServiceProvider.PublicOnly)
						{
							return;
						}
						DSACryptoServiceProvider dsacryptoServiceProvider2 = new DSACryptoServiceProvider(cspParams);
						dsacryptoServiceProvider2.ImportParameters(dsacryptoServiceProvider.ExportParameters(true));
						dsacryptoServiceProvider2.PersistKeyInCsp = true;
					}
					return;
				}
				if (rsamanaged.PublicOnly)
				{
					return;
				}
				RSACryptoServiceProvider rsacryptoServiceProvider3 = new RSACryptoServiceProvider(cspParams);
				rsacryptoServiceProvider3.ImportParameters(rsamanaged.ExportParameters(true));
				rsacryptoServiceProvider3.PersistKeyInCsp = true;
				return;
			}
		}

		// Token: 0x040000C2 RID: 194
		private string _storePath;

		// Token: 0x040000C3 RID: 195
		private X509CertificateCollection _certificates;

		// Token: 0x040000C4 RID: 196
		private ArrayList _crls;

		// Token: 0x040000C5 RID: 197
		private bool _crl;

		// Token: 0x040000C6 RID: 198
		private bool _newFormat;

		// Token: 0x040000C7 RID: 199
		private string _name;
	}
}
