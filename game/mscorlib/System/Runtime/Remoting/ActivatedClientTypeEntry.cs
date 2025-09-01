using System;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Contexts;

namespace System.Runtime.Remoting
{
	/// <summary>Holds values for an object type registered on the client end as a type that can be activated on the server.</summary>
	// Token: 0x02000558 RID: 1368
	[ComVisible(true)]
	public class ActivatedClientTypeEntry : TypeEntry
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.Remoting.ActivatedClientTypeEntry" /> class with the given <see cref="T:System.Type" /> and application URL.</summary>
		/// <param name="type">The <see cref="T:System.Type" /> of the client activated type.</param>
		/// <param name="appUrl">The URL of the application to activate the type in.</param>
		// Token: 0x060035D0 RID: 13776 RVA: 0x000C222D File Offset: 0x000C042D
		public ActivatedClientTypeEntry(Type type, string appUrl)
		{
			base.AssemblyName = type.Assembly.FullName;
			base.TypeName = type.FullName;
			this.applicationUrl = appUrl;
			this.obj_type = type;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.Remoting.ActivatedClientTypeEntry" /> class with the given type name, assembly name, and application URL.</summary>
		/// <param name="typeName">The type name of the client activated type.</param>
		/// <param name="assemblyName">The assembly name of the client activated type.</param>
		/// <param name="appUrl">The URL of the application to activate the type in.</param>
		// Token: 0x060035D1 RID: 13777 RVA: 0x000C2260 File Offset: 0x000C0460
		public ActivatedClientTypeEntry(string typeName, string assemblyName, string appUrl)
		{
			base.AssemblyName = assemblyName;
			base.TypeName = typeName;
			this.applicationUrl = appUrl;
			Assembly assembly = Assembly.Load(assemblyName);
			this.obj_type = assembly.GetType(typeName);
			if (this.obj_type == null)
			{
				throw new RemotingException("Type not found: " + typeName + ", " + assemblyName);
			}
		}

		/// <summary>Gets the URL of the application to activate the type in.</summary>
		/// <returns>The URL of the application to activate the type in.</returns>
		// Token: 0x1700077A RID: 1914
		// (get) Token: 0x060035D2 RID: 13778 RVA: 0x000C22C1 File Offset: 0x000C04C1
		public string ApplicationUrl
		{
			get
			{
				return this.applicationUrl;
			}
		}

		/// <summary>Gets or sets the context attributes for the client-activated type.</summary>
		/// <returns>The context attributes for the client activated type.</returns>
		// Token: 0x1700077B RID: 1915
		// (get) Token: 0x060035D3 RID: 13779 RVA: 0x0000AF5E File Offset: 0x0000915E
		// (set) Token: 0x060035D4 RID: 13780 RVA: 0x00004BF9 File Offset: 0x00002DF9
		public IContextAttribute[] ContextAttributes
		{
			get
			{
				return null;
			}
			set
			{
			}
		}

		/// <summary>Gets the <see cref="T:System.Type" /> of the client-activated type.</summary>
		/// <returns>Gets the <see cref="T:System.Type" /> of the client-activated type.</returns>
		// Token: 0x1700077C RID: 1916
		// (get) Token: 0x060035D5 RID: 13781 RVA: 0x000C22C9 File Offset: 0x000C04C9
		public Type ObjectType
		{
			get
			{
				return this.obj_type;
			}
		}

		/// <summary>Returns the type name, assembly name, and application URL of the client-activated type as a <see cref="T:System.String" />.</summary>
		/// <returns>The type name, assembly name, and application URL of the client-activated type as a <see cref="T:System.String" />.</returns>
		// Token: 0x060035D6 RID: 13782 RVA: 0x000C22D1 File Offset: 0x000C04D1
		public override string ToString()
		{
			return base.TypeName + base.AssemblyName + this.ApplicationUrl;
		}

		// Token: 0x04002513 RID: 9491
		private string applicationUrl;

		// Token: 0x04002514 RID: 9492
		private Type obj_type;
	}
}
