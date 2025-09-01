using System;
using System.Xml.XPath;

namespace System.Xml.Xsl.XsltOld
{
	// Token: 0x02000355 RID: 853
	internal class AttributeSetAction : ContainerAction
	{
		// Token: 0x17000702 RID: 1794
		// (get) Token: 0x06002341 RID: 9025 RVA: 0x000DB764 File Offset: 0x000D9964
		internal XmlQualifiedName Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x06002342 RID: 9026 RVA: 0x000DB76C File Offset: 0x000D996C
		internal override void Compile(Compiler compiler)
		{
			base.CompileAttributes(compiler);
			base.CheckRequiredAttribute(compiler, this.name, "name");
			this.CompileContent(compiler);
		}

		// Token: 0x06002343 RID: 9027 RVA: 0x000DB790 File Offset: 0x000D9990
		internal override bool CompileAttribute(Compiler compiler)
		{
			string localName = compiler.Input.LocalName;
			string value = compiler.Input.Value;
			if (Ref.Equal(localName, compiler.Atoms.Name))
			{
				this.name = compiler.CreateXPathQName(value);
			}
			else
			{
				if (!Ref.Equal(localName, compiler.Atoms.UseAttributeSets))
				{
					return false;
				}
				base.AddAction(compiler.CreateUseAttributeSetsAction());
			}
			return true;
		}

		// Token: 0x06002344 RID: 9028 RVA: 0x000DB7FC File Offset: 0x000D99FC
		private void CompileContent(Compiler compiler)
		{
			NavigatorInput input = compiler.Input;
			if (compiler.Recurse())
			{
				for (;;)
				{
					XPathNodeType nodeType = input.NodeType;
					if (nodeType != XPathNodeType.Element)
					{
						if (nodeType - XPathNodeType.SignificantWhitespace > 3)
						{
							break;
						}
					}
					else
					{
						compiler.PushNamespaceScope();
						string namespaceURI = input.NamespaceURI;
						string localName = input.LocalName;
						if (!Ref.Equal(namespaceURI, input.Atoms.UriXsl) || !Ref.Equal(localName, input.Atoms.Attribute))
						{
							goto IL_6B;
						}
						base.AddAction(compiler.CreateAttributeAction());
						compiler.PopScope();
					}
					if (!compiler.Advance())
					{
						goto Block_5;
					}
				}
				throw XsltException.Create("The contents of '{0}' are invalid.", new string[]
				{
					"attribute-set"
				});
				IL_6B:
				throw compiler.UnexpectedKeyword();
				Block_5:
				compiler.ToParent();
			}
		}

		// Token: 0x06002345 RID: 9029 RVA: 0x000DB8B0 File Offset: 0x000D9AB0
		internal void Merge(AttributeSetAction attributeAction)
		{
			int num = 0;
			Action action;
			while ((action = attributeAction.GetAction(num)) != null)
			{
				base.AddAction(action);
				num++;
			}
		}

		// Token: 0x06002346 RID: 9030 RVA: 0x000DB75C File Offset: 0x000D995C
		public AttributeSetAction()
		{
		}

		// Token: 0x04001C7D RID: 7293
		internal XmlQualifiedName name;
	}
}
