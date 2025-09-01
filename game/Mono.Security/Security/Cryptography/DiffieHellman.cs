using System;
using System.Security;
using System.Security.Cryptography;
using System.Text;
using Mono.Xml;

namespace Mono.Security.Cryptography
{
	// Token: 0x02000054 RID: 84
	public abstract class DiffieHellman : AsymmetricAlgorithm
	{
		// Token: 0x06000324 RID: 804 RVA: 0x000108D7 File Offset: 0x0000EAD7
		public new static DiffieHellman Create()
		{
			return DiffieHellman.Create("Mono.Security.Cryptography.DiffieHellman");
		}

		// Token: 0x06000325 RID: 805 RVA: 0x000108E3 File Offset: 0x0000EAE3
		public new static DiffieHellman Create(string algName)
		{
			return (DiffieHellman)CryptoConfig.CreateFromName(algName);
		}

		// Token: 0x06000326 RID: 806
		public abstract byte[] CreateKeyExchange();

		// Token: 0x06000327 RID: 807
		public abstract byte[] DecryptKeyExchange(byte[] keyex);

		// Token: 0x06000328 RID: 808
		public abstract DHParameters ExportParameters(bool includePrivate);

		// Token: 0x06000329 RID: 809
		public abstract void ImportParameters(DHParameters parameters);

		// Token: 0x0600032A RID: 810 RVA: 0x000108F0 File Offset: 0x0000EAF0
		private byte[] GetNamedParam(SecurityElement se, string param)
		{
			SecurityElement securityElement = se.SearchForChildByTag(param);
			if (securityElement == null)
			{
				return null;
			}
			return Convert.FromBase64String(securityElement.Text);
		}

		// Token: 0x0600032B RID: 811 RVA: 0x00010918 File Offset: 0x0000EB18
		public override void FromXmlString(string xmlString)
		{
			if (xmlString == null)
			{
				throw new ArgumentNullException("xmlString");
			}
			DHParameters dhparameters = default(DHParameters);
			try
			{
				SecurityParser securityParser = new SecurityParser();
				securityParser.LoadXml(xmlString);
				SecurityElement securityElement = securityParser.ToXml();
				if (securityElement.Tag != "DHKeyValue")
				{
					throw new CryptographicException();
				}
				dhparameters.P = this.GetNamedParam(securityElement, "P");
				dhparameters.G = this.GetNamedParam(securityElement, "G");
				dhparameters.X = this.GetNamedParam(securityElement, "X");
				this.ImportParameters(dhparameters);
			}
			finally
			{
				if (dhparameters.P != null)
				{
					Array.Clear(dhparameters.P, 0, dhparameters.P.Length);
				}
				if (dhparameters.G != null)
				{
					Array.Clear(dhparameters.G, 0, dhparameters.G.Length);
				}
				if (dhparameters.X != null)
				{
					Array.Clear(dhparameters.X, 0, dhparameters.X.Length);
				}
			}
		}

		// Token: 0x0600032C RID: 812 RVA: 0x00010A0C File Offset: 0x0000EC0C
		public override string ToXmlString(bool includePrivateParameters)
		{
			StringBuilder stringBuilder = new StringBuilder();
			DHParameters dhparameters = this.ExportParameters(includePrivateParameters);
			try
			{
				stringBuilder.Append("<DHKeyValue>");
				stringBuilder.Append("<P>");
				stringBuilder.Append(Convert.ToBase64String(dhparameters.P));
				stringBuilder.Append("</P>");
				stringBuilder.Append("<G>");
				stringBuilder.Append(Convert.ToBase64String(dhparameters.G));
				stringBuilder.Append("</G>");
				if (includePrivateParameters)
				{
					stringBuilder.Append("<X>");
					stringBuilder.Append(Convert.ToBase64String(dhparameters.X));
					stringBuilder.Append("</X>");
				}
				stringBuilder.Append("</DHKeyValue>");
			}
			finally
			{
				Array.Clear(dhparameters.P, 0, dhparameters.P.Length);
				Array.Clear(dhparameters.G, 0, dhparameters.G.Length);
				if (dhparameters.X != null)
				{
					Array.Clear(dhparameters.X, 0, dhparameters.X.Length);
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x0600032D RID: 813 RVA: 0x00010B20 File Offset: 0x0000ED20
		protected DiffieHellman()
		{
		}
	}
}
