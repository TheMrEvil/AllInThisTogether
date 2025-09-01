using System;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Contexts;

namespace System.Runtime.Remoting
{
	/// <summary>Holds values for an object type registered on the service end as a server-activated type object (single call or singleton).</summary>
	// Token: 0x0200057A RID: 1402
	[ComVisible(true)]
	public class WellKnownServiceTypeEntry : TypeEntry
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.Remoting.WellKnownServiceTypeEntry" /> class with the given <see cref="T:System.Type" />, object URI, and <see cref="T:System.Runtime.Remoting.WellKnownObjectMode" />.</summary>
		/// <param name="type">The <see cref="T:System.Type" /> of the server-activated service type object.</param>
		/// <param name="objectUri">The URI of the server-activated type.</param>
		/// <param name="mode">The <see cref="T:System.Runtime.Remoting.WellKnownObjectMode" /> of the type, which defines how the object is activated.</param>
		// Token: 0x060036FF RID: 14079 RVA: 0x000C6961 File Offset: 0x000C4B61
		public WellKnownServiceTypeEntry(Type type, string objectUri, WellKnownObjectMode mode)
		{
			base.AssemblyName = type.Assembly.FullName;
			base.TypeName = type.FullName;
			this.obj_type = type;
			this.obj_uri = objectUri;
			this.obj_mode = mode;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.Remoting.WellKnownServiceTypeEntry" /> class with the given type name, assembly name, object URI, and <see cref="T:System.Runtime.Remoting.WellKnownObjectMode" />.</summary>
		/// <param name="typeName">The full type name of the server-activated service type.</param>
		/// <param name="assemblyName">The assembly name of the server-activated service type.</param>
		/// <param name="objectUri">The URI of the server-activated object.</param>
		/// <param name="mode">The <see cref="T:System.Runtime.Remoting.WellKnownObjectMode" /> of the type, which defines how the object is activated.</param>
		// Token: 0x06003700 RID: 14080 RVA: 0x000C699C File Offset: 0x000C4B9C
		public WellKnownServiceTypeEntry(string typeName, string assemblyName, string objectUri, WellKnownObjectMode mode)
		{
			base.AssemblyName = assemblyName;
			base.TypeName = typeName;
			Assembly assembly = Assembly.Load(assemblyName);
			this.obj_type = assembly.GetType(typeName);
			this.obj_uri = objectUri;
			this.obj_mode = mode;
			if (this.obj_type == null)
			{
				throw new RemotingException("Type not found: " + typeName + ", " + assemblyName);
			}
		}

		/// <summary>Gets or sets the context attributes for the server-activated service type.</summary>
		/// <returns>The context attributes for the server-activated service type.</returns>
		// Token: 0x170007AA RID: 1962
		// (get) Token: 0x06003701 RID: 14081 RVA: 0x0000AF5E File Offset: 0x0000915E
		// (set) Token: 0x06003702 RID: 14082 RVA: 0x00004BF9 File Offset: 0x00002DF9
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

		/// <summary>Gets the <see cref="T:System.Runtime.Remoting.WellKnownObjectMode" /> of the server-activated service type.</summary>
		/// <returns>The <see cref="T:System.Runtime.Remoting.WellKnownObjectMode" /> of the server-activated service type.</returns>
		// Token: 0x170007AB RID: 1963
		// (get) Token: 0x06003703 RID: 14083 RVA: 0x000C6A05 File Offset: 0x000C4C05
		public WellKnownObjectMode Mode
		{
			get
			{
				return this.obj_mode;
			}
		}

		/// <summary>Gets the <see cref="T:System.Type" /> of the server-activated service type.</summary>
		/// <returns>The <see cref="T:System.Type" /> of the server-activated service type.</returns>
		// Token: 0x170007AC RID: 1964
		// (get) Token: 0x06003704 RID: 14084 RVA: 0x000C6A0D File Offset: 0x000C4C0D
		public Type ObjectType
		{
			get
			{
				return this.obj_type;
			}
		}

		/// <summary>Gets the URI of the well-known service type.</summary>
		/// <returns>The URI of the server-activated service type.</returns>
		// Token: 0x170007AD RID: 1965
		// (get) Token: 0x06003705 RID: 14085 RVA: 0x000C6A15 File Offset: 0x000C4C15
		public string ObjectUri
		{
			get
			{
				return this.obj_uri;
			}
		}

		/// <summary>Returns the type name, assembly name, object URI and the <see cref="T:System.Runtime.Remoting.WellKnownObjectMode" /> of the server-activated type as a <see cref="T:System.String" />.</summary>
		/// <returns>The type name, assembly name, object URI, and the <see cref="T:System.Runtime.Remoting.WellKnownObjectMode" /> of the server-activated type as a <see cref="T:System.String" />.</returns>
		// Token: 0x06003706 RID: 14086 RVA: 0x000C6A1D File Offset: 0x000C4C1D
		public override string ToString()
		{
			return string.Concat(new string[]
			{
				base.TypeName,
				", ",
				base.AssemblyName,
				" ",
				this.ObjectUri
			});
		}

		// Token: 0x04002573 RID: 9587
		private Type obj_type;

		// Token: 0x04002574 RID: 9588
		private string obj_uri;

		// Token: 0x04002575 RID: 9589
		private WellKnownObjectMode obj_mode;
	}
}
