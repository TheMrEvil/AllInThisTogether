using System;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Contexts;

namespace System.Runtime.Remoting
{
	/// <summary>Holds values for an object type registered on the service end as one that can be activated on request from a client.</summary>
	// Token: 0x02000559 RID: 1369
	[ComVisible(true)]
	public class ActivatedServiceTypeEntry : TypeEntry
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.Remoting.ActivatedServiceTypeEntry" /> class with the given <see cref="T:System.Type" />.</summary>
		/// <param name="type">The <see cref="T:System.Type" /> of the client-activated service type.</param>
		// Token: 0x060035D7 RID: 13783 RVA: 0x000C22EA File Offset: 0x000C04EA
		public ActivatedServiceTypeEntry(Type type)
		{
			base.AssemblyName = type.Assembly.FullName;
			base.TypeName = type.FullName;
			this.obj_type = type;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.Remoting.ActivatedServiceTypeEntry" /> class with the given type name and assembly name.</summary>
		/// <param name="typeName">The type name of the client-activated service type.</param>
		/// <param name="assemblyName">The assembly name of the client-activated service type.</param>
		// Token: 0x060035D8 RID: 13784 RVA: 0x000C2318 File Offset: 0x000C0518
		public ActivatedServiceTypeEntry(string typeName, string assemblyName)
		{
			base.AssemblyName = assemblyName;
			base.TypeName = typeName;
			Assembly assembly = Assembly.Load(assemblyName);
			this.obj_type = assembly.GetType(typeName);
			if (this.obj_type == null)
			{
				throw new RemotingException("Type not found: " + typeName + ", " + assemblyName);
			}
		}

		/// <summary>Gets or sets the context attributes for the client-activated service type.</summary>
		/// <returns>The context attributes for the client-activated service type.</returns>
		// Token: 0x1700077D RID: 1917
		// (get) Token: 0x060035D9 RID: 13785 RVA: 0x0000AF5E File Offset: 0x0000915E
		// (set) Token: 0x060035DA RID: 13786 RVA: 0x00004BF9 File Offset: 0x00002DF9
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

		/// <summary>Gets the <see cref="T:System.Type" /> of the client-activated service type.</summary>
		/// <returns>The <see cref="T:System.Type" /> of the client-activated service type.</returns>
		// Token: 0x1700077E RID: 1918
		// (get) Token: 0x060035DB RID: 13787 RVA: 0x000C2372 File Offset: 0x000C0572
		public Type ObjectType
		{
			get
			{
				return this.obj_type;
			}
		}

		/// <summary>Returns the type and assembly name of the client-activated service type as a <see cref="T:System.String" />.</summary>
		/// <returns>The type and assembly name of the client-activated service type as a <see cref="T:System.String" />.</returns>
		// Token: 0x060035DC RID: 13788 RVA: 0x000C237A File Offset: 0x000C057A
		public override string ToString()
		{
			return base.AssemblyName + base.TypeName;
		}

		// Token: 0x04002515 RID: 9493
		private Type obj_type;
	}
}
