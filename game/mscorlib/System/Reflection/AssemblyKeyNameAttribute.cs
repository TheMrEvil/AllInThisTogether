using System;
using System.Runtime.CompilerServices;

namespace System.Reflection
{
	/// <summary>Specifies the name of a key container within the CSP containing the key pair used to generate a strong name.</summary>
	// Token: 0x0200088A RID: 2186
	[AttributeUsage(AttributeTargets.Assembly, Inherited = false)]
	public sealed class AssemblyKeyNameAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Reflection.AssemblyKeyNameAttribute" /> class with the name of the container holding the key pair used to generate a strong name for the assembly being attributed.</summary>
		/// <param name="keyName">The name of the container containing the key pair.</param>
		// Token: 0x0600486A RID: 18538 RVA: 0x000EE0F7 File Offset: 0x000EC2F7
		public AssemblyKeyNameAttribute(string keyName)
		{
			this.KeyName = keyName;
		}

		/// <summary>Gets the name of the container having the key pair that is used to generate a strong name for the attributed assembly.</summary>
		/// <returns>A string containing the name of the container that has the relevant key pair.</returns>
		// Token: 0x17000B2D RID: 2861
		// (get) Token: 0x0600486B RID: 18539 RVA: 0x000EE106 File Offset: 0x000EC306
		public string KeyName
		{
			[CompilerGenerated]
			get
			{
				return this.<KeyName>k__BackingField;
			}
		}

		// Token: 0x04002E58 RID: 11864
		[CompilerGenerated]
		private readonly string <KeyName>k__BackingField;
	}
}
