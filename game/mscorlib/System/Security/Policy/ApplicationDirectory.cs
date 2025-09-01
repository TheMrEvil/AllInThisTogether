using System;
using System.IO;
using System.Runtime.InteropServices;

namespace System.Security.Policy
{
	/// <summary>Provides the application directory as evidence for policy evaluation. This class cannot be inherited.</summary>
	// Token: 0x020003FF RID: 1023
	[ComVisible(true)]
	[Serializable]
	public sealed class ApplicationDirectory : EvidenceBase, IBuiltInEvidence
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Policy.ApplicationDirectory" /> class.</summary>
		/// <param name="name">The path of the application directory.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="name" /> parameter is <see langword="null" />.</exception>
		// Token: 0x060029D0 RID: 10704 RVA: 0x00097EB1 File Offset: 0x000960B1
		public ApplicationDirectory(string name)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			if (name.Length < 1)
			{
				throw new FormatException(Locale.GetText("Empty"));
			}
			this.directory = name;
		}

		/// <summary>Gets the path of the application directory.</summary>
		/// <returns>The path of the application directory.</returns>
		// Token: 0x1700051F RID: 1311
		// (get) Token: 0x060029D1 RID: 10705 RVA: 0x00097EE7 File Offset: 0x000960E7
		public string Directory
		{
			get
			{
				return this.directory;
			}
		}

		/// <summary>Creates a new copy of the <see cref="T:System.Security.Policy.ApplicationDirectory" />.</summary>
		/// <returns>A new, identical copy of the <see cref="T:System.Security.Policy.ApplicationDirectory" />.</returns>
		// Token: 0x060029D2 RID: 10706 RVA: 0x00097EEF File Offset: 0x000960EF
		public object Copy()
		{
			return new ApplicationDirectory(this.Directory);
		}

		/// <summary>Determines whether instances of the same type of an evidence object are equivalent.</summary>
		/// <param name="o">An object of same type as the current evidence object.</param>
		/// <returns>
		///   <see langword="true" /> if the two instances are equivalent; otherwise, <see langword="false" />.</returns>
		// Token: 0x060029D3 RID: 10707 RVA: 0x00097EFC File Offset: 0x000960FC
		public override bool Equals(object o)
		{
			ApplicationDirectory applicationDirectory = o as ApplicationDirectory;
			if (applicationDirectory != null)
			{
				this.ThrowOnInvalid(applicationDirectory.directory);
				return this.directory == applicationDirectory.directory;
			}
			return false;
		}

		/// <summary>Gets the hash code of the current application directory.</summary>
		/// <returns>The hash code of the current application directory.</returns>
		// Token: 0x060029D4 RID: 10708 RVA: 0x00097F32 File Offset: 0x00096132
		public override int GetHashCode()
		{
			return this.Directory.GetHashCode();
		}

		/// <summary>Gets a string representation of the state of the <see cref="T:System.Security.Policy.ApplicationDirectory" /> evidence object.</summary>
		/// <returns>A representation of the state of the <see cref="T:System.Security.Policy.ApplicationDirectory" /> evidence object.</returns>
		// Token: 0x060029D5 RID: 10709 RVA: 0x00097F40 File Offset: 0x00096140
		public override string ToString()
		{
			this.ThrowOnInvalid(this.Directory);
			SecurityElement securityElement = new SecurityElement("System.Security.Policy.ApplicationDirectory");
			securityElement.AddAttribute("version", "1");
			securityElement.AddChild(new SecurityElement("Directory", this.directory));
			return securityElement.ToString();
		}

		// Token: 0x060029D6 RID: 10710 RVA: 0x00097F8E File Offset: 0x0009618E
		int IBuiltInEvidence.GetRequiredSize(bool verbose)
		{
			return (verbose ? 3 : 1) + this.directory.Length;
		}

		// Token: 0x060029D7 RID: 10711 RVA: 0x0000DDA9 File Offset: 0x0000BFA9
		[MonoTODO("IBuiltInEvidence")]
		int IBuiltInEvidence.InitFromBuffer(char[] buffer, int position)
		{
			return 0;
		}

		// Token: 0x060029D8 RID: 10712 RVA: 0x0000DDA9 File Offset: 0x0000BFA9
		[MonoTODO("IBuiltInEvidence")]
		int IBuiltInEvidence.OutputToBuffer(char[] buffer, int position, bool verbose)
		{
			return 0;
		}

		// Token: 0x060029D9 RID: 10713 RVA: 0x00097FA3 File Offset: 0x000961A3
		private void ThrowOnInvalid(string appdir)
		{
			if (appdir.IndexOfAny(Path.InvalidPathChars) != -1)
			{
				throw new ArgumentException(string.Format(Locale.GetText("Invalid character(s) in directory {0}"), appdir), "other");
			}
		}

		// Token: 0x04001F53 RID: 8019
		private string directory;
	}
}
