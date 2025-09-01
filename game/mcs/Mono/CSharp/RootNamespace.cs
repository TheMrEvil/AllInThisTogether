using System;
using System.Collections.Generic;

namespace Mono.CSharp
{
	// Token: 0x02000259 RID: 601
	public class RootNamespace : Namespace
	{
		// Token: 0x06001DC6 RID: 7622 RVA: 0x00091811 File Offset: 0x0008FA11
		public RootNamespace(string alias_name)
		{
			this.alias_name = alias_name;
			this.RegisterNamespace(this);
			this.all_namespaces = new Dictionary<string, Namespace>();
			this.all_namespaces.Add("", this);
		}

		// Token: 0x170006D5 RID: 1749
		// (get) Token: 0x06001DC7 RID: 7623 RVA: 0x00091843 File Offset: 0x0008FA43
		public string Alias
		{
			get
			{
				return this.alias_name;
			}
		}

		// Token: 0x06001DC8 RID: 7624 RVA: 0x0009184B File Offset: 0x0008FA4B
		public static void Error_GlobalNamespaceRedefined(Report report, Location loc)
		{
			report.Error(1681, loc, "The global extern alias cannot be redefined");
		}

		// Token: 0x06001DC9 RID: 7625 RVA: 0x00091860 File Offset: 0x0008FA60
		public List<string> FindTypeNamespaces(IMemberContext ctx, string name, int arity)
		{
			List<string> list = null;
			foreach (KeyValuePair<string, Namespace> keyValuePair in this.all_namespaces)
			{
				if (keyValuePair.Value.LookupType(ctx, name, arity, LookupMode.Normal, Location.Null) != null)
				{
					if (list == null)
					{
						list = new List<string>();
					}
					list.Add(keyValuePair.Key);
				}
			}
			return list;
		}

		// Token: 0x06001DCA RID: 7626 RVA: 0x000918DC File Offset: 0x0008FADC
		public List<string> FindExtensionMethodNamespaces(IMemberContext ctx, string name, int arity)
		{
			List<string> list = null;
			foreach (KeyValuePair<string, Namespace> keyValuePair in this.all_namespaces)
			{
				if (keyValuePair.Key.Length != 0 && keyValuePair.Value.LookupExtensionMethod(ctx, name, arity) != null)
				{
					if (list == null)
					{
						list = new List<string>();
					}
					list.Add(keyValuePair.Key);
				}
			}
			return list;
		}

		// Token: 0x06001DCB RID: 7627 RVA: 0x00091960 File Offset: 0x0008FB60
		public void RegisterNamespace(Namespace child)
		{
			if (child != this)
			{
				this.all_namespaces.Add(child.Name, child);
			}
		}

		// Token: 0x06001DCC RID: 7628 RVA: 0x00091978 File Offset: 0x0008FB78
		public override string GetSignatureForError()
		{
			return this.alias_name + "::";
		}

		// Token: 0x04000B14 RID: 2836
		private readonly string alias_name;

		// Token: 0x04000B15 RID: 2837
		private readonly Dictionary<string, Namespace> all_namespaces;
	}
}
