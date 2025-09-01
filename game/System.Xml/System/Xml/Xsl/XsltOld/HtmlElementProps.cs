using System;
using System.Collections;

namespace System.Xml.Xsl.XsltOld
{
	// Token: 0x0200038A RID: 906
	internal class HtmlElementProps
	{
		// Token: 0x060024C8 RID: 9416 RVA: 0x000E043F File Offset: 0x000DE63F
		public static HtmlElementProps Create(bool empty, bool abrParent, bool uriParent, bool noEntities, bool blockWS, bool head, bool nameParent)
		{
			return new HtmlElementProps
			{
				empty = empty,
				abrParent = abrParent,
				uriParent = uriParent,
				noEntities = noEntities,
				blockWS = blockWS,
				head = head,
				nameParent = nameParent
			};
		}

		// Token: 0x17000723 RID: 1827
		// (get) Token: 0x060024C9 RID: 9417 RVA: 0x000E047A File Offset: 0x000DE67A
		public bool Empty
		{
			get
			{
				return this.empty;
			}
		}

		// Token: 0x17000724 RID: 1828
		// (get) Token: 0x060024CA RID: 9418 RVA: 0x000E0482 File Offset: 0x000DE682
		public bool AbrParent
		{
			get
			{
				return this.abrParent;
			}
		}

		// Token: 0x17000725 RID: 1829
		// (get) Token: 0x060024CB RID: 9419 RVA: 0x000E048A File Offset: 0x000DE68A
		public bool UriParent
		{
			get
			{
				return this.uriParent;
			}
		}

		// Token: 0x17000726 RID: 1830
		// (get) Token: 0x060024CC RID: 9420 RVA: 0x000E0492 File Offset: 0x000DE692
		public bool NoEntities
		{
			get
			{
				return this.noEntities;
			}
		}

		// Token: 0x17000727 RID: 1831
		// (get) Token: 0x060024CD RID: 9421 RVA: 0x000E049A File Offset: 0x000DE69A
		public bool BlockWS
		{
			get
			{
				return this.blockWS;
			}
		}

		// Token: 0x17000728 RID: 1832
		// (get) Token: 0x060024CE RID: 9422 RVA: 0x000E04A2 File Offset: 0x000DE6A2
		public bool Head
		{
			get
			{
				return this.head;
			}
		}

		// Token: 0x17000729 RID: 1833
		// (get) Token: 0x060024CF RID: 9423 RVA: 0x000E04AA File Offset: 0x000DE6AA
		public bool NameParent
		{
			get
			{
				return this.nameParent;
			}
		}

		// Token: 0x060024D0 RID: 9424 RVA: 0x000E04B2 File Offset: 0x000DE6B2
		public static HtmlElementProps GetProps(string name)
		{
			return (HtmlElementProps)HtmlElementProps.s_table[name];
		}

		// Token: 0x060024D1 RID: 9425 RVA: 0x000E04C4 File Offset: 0x000DE6C4
		private static Hashtable CreatePropsTable()
		{
			bool flag = false;
			bool flag2 = true;
			return new Hashtable(71, StringComparer.OrdinalIgnoreCase)
			{
				{
					"a",
					HtmlElementProps.Create(flag, flag, flag2, flag, flag, flag, flag2)
				},
				{
					"address",
					HtmlElementProps.Create(flag, flag, flag, flag, flag2, flag, flag)
				},
				{
					"applet",
					HtmlElementProps.Create(flag, flag, flag, flag, flag2, flag, flag)
				},
				{
					"area",
					HtmlElementProps.Create(flag2, flag2, flag2, flag, flag2, flag, flag)
				},
				{
					"base",
					HtmlElementProps.Create(flag2, flag, flag2, flag, flag2, flag, flag)
				},
				{
					"basefont",
					HtmlElementProps.Create(flag2, flag, flag, flag, flag2, flag, flag)
				},
				{
					"blockquote",
					HtmlElementProps.Create(flag, flag, flag2, flag, flag2, flag, flag)
				},
				{
					"body",
					HtmlElementProps.Create(flag, flag, flag, flag, flag2, flag, flag)
				},
				{
					"br",
					HtmlElementProps.Create(flag2, flag, flag, flag, flag, flag, flag)
				},
				{
					"button",
					HtmlElementProps.Create(flag, flag2, flag, flag, flag, flag, flag)
				},
				{
					"caption",
					HtmlElementProps.Create(flag, flag, flag, flag, flag2, flag, flag)
				},
				{
					"center",
					HtmlElementProps.Create(flag, flag, flag, flag, flag2, flag, flag)
				},
				{
					"col",
					HtmlElementProps.Create(flag2, flag, flag, flag, flag2, flag, flag)
				},
				{
					"colgroup",
					HtmlElementProps.Create(flag, flag, flag, flag, flag2, flag, flag)
				},
				{
					"dd",
					HtmlElementProps.Create(flag, flag, flag, flag, flag2, flag, flag)
				},
				{
					"del",
					HtmlElementProps.Create(flag, flag, flag2, flag, flag2, flag, flag)
				},
				{
					"dir",
					HtmlElementProps.Create(flag, flag2, flag, flag, flag2, flag, flag)
				},
				{
					"div",
					HtmlElementProps.Create(flag, flag, flag, flag, flag2, flag, flag)
				},
				{
					"dl",
					HtmlElementProps.Create(flag, flag2, flag, flag, flag2, flag, flag)
				},
				{
					"dt",
					HtmlElementProps.Create(flag, flag, flag, flag, flag2, flag, flag)
				},
				{
					"fieldset",
					HtmlElementProps.Create(flag, flag, flag, flag, flag2, flag, flag)
				},
				{
					"font",
					HtmlElementProps.Create(flag, flag, flag, flag, flag2, flag, flag)
				},
				{
					"form",
					HtmlElementProps.Create(flag, flag, flag2, flag, flag2, flag, flag)
				},
				{
					"frame",
					HtmlElementProps.Create(flag2, flag2, flag, flag, flag2, flag, flag)
				},
				{
					"frameset",
					HtmlElementProps.Create(flag, flag, flag, flag, flag2, flag, flag)
				},
				{
					"h1",
					HtmlElementProps.Create(flag, flag, flag, flag, flag2, flag, flag)
				},
				{
					"h2",
					HtmlElementProps.Create(flag, flag, flag, flag, flag2, flag, flag)
				},
				{
					"h3",
					HtmlElementProps.Create(flag, flag, flag, flag, flag2, flag, flag)
				},
				{
					"h4",
					HtmlElementProps.Create(flag, flag, flag, flag, flag2, flag, flag)
				},
				{
					"h5",
					HtmlElementProps.Create(flag, flag, flag, flag, flag2, flag, flag)
				},
				{
					"h6",
					HtmlElementProps.Create(flag, flag, flag, flag, flag2, flag, flag)
				},
				{
					"head",
					HtmlElementProps.Create(flag, flag, flag2, flag, flag2, flag2, flag)
				},
				{
					"hr",
					HtmlElementProps.Create(flag2, flag2, flag, flag, flag2, flag, flag)
				},
				{
					"html",
					HtmlElementProps.Create(flag, flag, flag, flag, flag2, flag, flag)
				},
				{
					"iframe",
					HtmlElementProps.Create(flag, flag, flag, flag, flag2, flag, flag)
				},
				{
					"img",
					HtmlElementProps.Create(flag2, flag2, flag2, flag, flag, flag, flag)
				},
				{
					"input",
					HtmlElementProps.Create(flag2, flag2, flag2, flag, flag, flag, flag)
				},
				{
					"ins",
					HtmlElementProps.Create(flag, flag, flag2, flag, flag2, flag, flag)
				},
				{
					"isindex",
					HtmlElementProps.Create(flag2, flag, flag, flag, flag2, flag, flag)
				},
				{
					"legend",
					HtmlElementProps.Create(flag, flag, flag, flag, flag2, flag, flag)
				},
				{
					"li",
					HtmlElementProps.Create(flag, flag, flag, flag, flag2, flag, flag)
				},
				{
					"link",
					HtmlElementProps.Create(flag2, flag, flag2, flag, flag2, flag, flag)
				},
				{
					"map",
					HtmlElementProps.Create(flag, flag, flag, flag, flag2, flag, flag)
				},
				{
					"menu",
					HtmlElementProps.Create(flag, flag2, flag, flag, flag2, flag, flag)
				},
				{
					"meta",
					HtmlElementProps.Create(flag2, flag, flag, flag, flag2, flag, flag)
				},
				{
					"noframes",
					HtmlElementProps.Create(flag, flag, flag, flag, flag2, flag, flag)
				},
				{
					"noscript",
					HtmlElementProps.Create(flag, flag, flag, flag, flag2, flag, flag)
				},
				{
					"object",
					HtmlElementProps.Create(flag, flag2, flag2, flag, flag, flag, flag)
				},
				{
					"ol",
					HtmlElementProps.Create(flag, flag2, flag, flag, flag2, flag, flag)
				},
				{
					"optgroup",
					HtmlElementProps.Create(flag, flag2, flag, flag, flag2, flag, flag)
				},
				{
					"option",
					HtmlElementProps.Create(flag, flag2, flag, flag, flag2, flag, flag)
				},
				{
					"p",
					HtmlElementProps.Create(flag, flag, flag, flag, flag2, flag, flag)
				},
				{
					"param",
					HtmlElementProps.Create(flag2, flag, flag, flag, flag2, flag, flag)
				},
				{
					"pre",
					HtmlElementProps.Create(flag, flag, flag, flag, flag2, flag, flag)
				},
				{
					"q",
					HtmlElementProps.Create(flag, flag, flag2, flag, flag, flag, flag)
				},
				{
					"s",
					HtmlElementProps.Create(flag, flag, flag, flag, flag2, flag, flag)
				},
				{
					"script",
					HtmlElementProps.Create(flag, flag2, flag2, flag2, flag, flag, flag)
				},
				{
					"select",
					HtmlElementProps.Create(flag, flag2, flag, flag, flag, flag, flag)
				},
				{
					"strike",
					HtmlElementProps.Create(flag, flag, flag, flag, flag2, flag, flag)
				},
				{
					"style",
					HtmlElementProps.Create(flag, flag, flag, flag2, flag2, flag, flag)
				},
				{
					"table",
					HtmlElementProps.Create(flag, flag, flag2, flag, flag2, flag, flag)
				},
				{
					"tbody",
					HtmlElementProps.Create(flag, flag, flag, flag, flag2, flag, flag)
				},
				{
					"td",
					HtmlElementProps.Create(flag, flag2, flag, flag, flag2, flag, flag)
				},
				{
					"textarea",
					HtmlElementProps.Create(flag, flag2, flag, flag, flag, flag, flag)
				},
				{
					"tfoot",
					HtmlElementProps.Create(flag, flag, flag, flag, flag2, flag, flag)
				},
				{
					"th",
					HtmlElementProps.Create(flag, flag2, flag, flag, flag2, flag, flag)
				},
				{
					"thead",
					HtmlElementProps.Create(flag, flag, flag, flag, flag2, flag, flag)
				},
				{
					"title",
					HtmlElementProps.Create(flag, flag, flag, flag, flag2, flag, flag)
				},
				{
					"tr",
					HtmlElementProps.Create(flag, flag, flag, flag, flag2, flag, flag)
				},
				{
					"ul",
					HtmlElementProps.Create(flag, flag2, flag, flag, flag2, flag, flag)
				},
				{
					"xmp",
					HtmlElementProps.Create(flag, flag, flag, flag, flag, flag, flag)
				}
			};
		}

		// Token: 0x060024D2 RID: 9426 RVA: 0x0000216B File Offset: 0x0000036B
		public HtmlElementProps()
		{
		}

		// Token: 0x060024D3 RID: 9427 RVA: 0x000E0B42 File Offset: 0x000DED42
		// Note: this type is marked as 'beforefieldinit'.
		static HtmlElementProps()
		{
		}

		// Token: 0x04001D11 RID: 7441
		private bool empty;

		// Token: 0x04001D12 RID: 7442
		private bool abrParent;

		// Token: 0x04001D13 RID: 7443
		private bool uriParent;

		// Token: 0x04001D14 RID: 7444
		private bool noEntities;

		// Token: 0x04001D15 RID: 7445
		private bool blockWS;

		// Token: 0x04001D16 RID: 7446
		private bool head;

		// Token: 0x04001D17 RID: 7447
		private bool nameParent;

		// Token: 0x04001D18 RID: 7448
		private static Hashtable s_table = HtmlElementProps.CreatePropsTable();
	}
}
