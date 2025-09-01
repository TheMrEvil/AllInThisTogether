using System;
using System.Collections;

namespace System.Xml.Xsl.XsltOld
{
	// Token: 0x0200038B RID: 907
	internal class HtmlAttributeProps
	{
		// Token: 0x060024D4 RID: 9428 RVA: 0x000E0B4E File Offset: 0x000DED4E
		public static HtmlAttributeProps Create(bool abr, bool uri, bool name)
		{
			return new HtmlAttributeProps
			{
				abr = abr,
				uri = uri,
				name = name
			};
		}

		// Token: 0x1700072A RID: 1834
		// (get) Token: 0x060024D5 RID: 9429 RVA: 0x000E0B6A File Offset: 0x000DED6A
		public bool Abr
		{
			get
			{
				return this.abr;
			}
		}

		// Token: 0x1700072B RID: 1835
		// (get) Token: 0x060024D6 RID: 9430 RVA: 0x000E0B72 File Offset: 0x000DED72
		public bool Uri
		{
			get
			{
				return this.uri;
			}
		}

		// Token: 0x1700072C RID: 1836
		// (get) Token: 0x060024D7 RID: 9431 RVA: 0x000E0B7A File Offset: 0x000DED7A
		public bool Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x060024D8 RID: 9432 RVA: 0x000E0B82 File Offset: 0x000DED82
		public static HtmlAttributeProps GetProps(string name)
		{
			return (HtmlAttributeProps)HtmlAttributeProps.s_table[name];
		}

		// Token: 0x060024D9 RID: 9433 RVA: 0x000E0B94 File Offset: 0x000DED94
		private static Hashtable CreatePropsTable()
		{
			bool flag = false;
			bool flag2 = true;
			return new Hashtable(26, StringComparer.OrdinalIgnoreCase)
			{
				{
					"action",
					HtmlAttributeProps.Create(flag, flag2, flag)
				},
				{
					"checked",
					HtmlAttributeProps.Create(flag2, flag, flag)
				},
				{
					"cite",
					HtmlAttributeProps.Create(flag, flag2, flag)
				},
				{
					"classid",
					HtmlAttributeProps.Create(flag, flag2, flag)
				},
				{
					"codebase",
					HtmlAttributeProps.Create(flag, flag2, flag)
				},
				{
					"compact",
					HtmlAttributeProps.Create(flag2, flag, flag)
				},
				{
					"data",
					HtmlAttributeProps.Create(flag, flag2, flag)
				},
				{
					"datasrc",
					HtmlAttributeProps.Create(flag, flag2, flag)
				},
				{
					"declare",
					HtmlAttributeProps.Create(flag2, flag, flag)
				},
				{
					"defer",
					HtmlAttributeProps.Create(flag2, flag, flag)
				},
				{
					"disabled",
					HtmlAttributeProps.Create(flag2, flag, flag)
				},
				{
					"for",
					HtmlAttributeProps.Create(flag, flag2, flag)
				},
				{
					"href",
					HtmlAttributeProps.Create(flag, flag2, flag)
				},
				{
					"ismap",
					HtmlAttributeProps.Create(flag2, flag, flag)
				},
				{
					"longdesc",
					HtmlAttributeProps.Create(flag, flag2, flag)
				},
				{
					"multiple",
					HtmlAttributeProps.Create(flag2, flag, flag)
				},
				{
					"name",
					HtmlAttributeProps.Create(flag, flag, flag2)
				},
				{
					"nohref",
					HtmlAttributeProps.Create(flag2, flag, flag)
				},
				{
					"noresize",
					HtmlAttributeProps.Create(flag2, flag, flag)
				},
				{
					"noshade",
					HtmlAttributeProps.Create(flag2, flag, flag)
				},
				{
					"nowrap",
					HtmlAttributeProps.Create(flag2, flag, flag)
				},
				{
					"profile",
					HtmlAttributeProps.Create(flag, flag2, flag)
				},
				{
					"readonly",
					HtmlAttributeProps.Create(flag2, flag, flag)
				},
				{
					"selected",
					HtmlAttributeProps.Create(flag2, flag, flag)
				},
				{
					"src",
					HtmlAttributeProps.Create(flag, flag2, flag)
				},
				{
					"usemap",
					HtmlAttributeProps.Create(flag, flag2, flag)
				}
			};
		}

		// Token: 0x060024DA RID: 9434 RVA: 0x0000216B File Offset: 0x0000036B
		public HtmlAttributeProps()
		{
		}

		// Token: 0x060024DB RID: 9435 RVA: 0x000E0D9F File Offset: 0x000DEF9F
		// Note: this type is marked as 'beforefieldinit'.
		static HtmlAttributeProps()
		{
		}

		// Token: 0x04001D19 RID: 7449
		private bool abr;

		// Token: 0x04001D1A RID: 7450
		private bool uri;

		// Token: 0x04001D1B RID: 7451
		private bool name;

		// Token: 0x04001D1C RID: 7452
		private static Hashtable s_table = HtmlAttributeProps.CreatePropsTable();
	}
}
