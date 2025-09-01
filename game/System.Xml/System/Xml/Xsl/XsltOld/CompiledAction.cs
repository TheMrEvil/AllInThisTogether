using System;
using System.Xml.XPath;

namespace System.Xml.Xsl.XsltOld
{
	// Token: 0x0200035D RID: 861
	internal abstract class CompiledAction : Action
	{
		// Token: 0x06002372 RID: 9074
		internal abstract void Compile(Compiler compiler);

		// Token: 0x06002373 RID: 9075 RVA: 0x0000D1C5 File Offset: 0x0000B3C5
		internal virtual bool CompileAttribute(Compiler compiler)
		{
			return false;
		}

		// Token: 0x06002374 RID: 9076 RVA: 0x000DC18C File Offset: 0x000DA38C
		public void CompileAttributes(Compiler compiler)
		{
			NavigatorInput input = compiler.Input;
			string localName = input.LocalName;
			if (input.MoveToFirstAttribute())
			{
				do
				{
					if (input.NamespaceURI.Length == 0)
					{
						try
						{
							if (!this.CompileAttribute(compiler))
							{
								throw XsltException.Create("'{0}' is an invalid attribute for the '{1}' element.", new string[]
								{
									input.LocalName,
									localName
								});
							}
						}
						catch
						{
							if (!compiler.ForwardCompatibility)
							{
								throw;
							}
						}
					}
				}
				while (input.MoveToNextAttribute());
				input.ToParent();
			}
		}

		// Token: 0x06002375 RID: 9077 RVA: 0x000DC214 File Offset: 0x000DA414
		internal static string PrecalculateAvt(ref Avt avt)
		{
			string result = null;
			if (avt != null && avt.IsConstant)
			{
				result = avt.Evaluate(null, null);
				avt = null;
			}
			return result;
		}

		// Token: 0x06002376 RID: 9078 RVA: 0x000DC240 File Offset: 0x000DA440
		public void CheckEmpty(Compiler compiler)
		{
			string name = compiler.Input.Name;
			if (compiler.Recurse())
			{
				for (;;)
				{
					XPathNodeType nodeType = compiler.Input.NodeType;
					if (nodeType != XPathNodeType.Whitespace && nodeType != XPathNodeType.Comment && nodeType != XPathNodeType.ProcessingInstruction)
					{
						break;
					}
					if (!compiler.Advance())
					{
						goto Block_4;
					}
				}
				throw XsltException.Create("The contents of '{0}' must be empty.", new string[]
				{
					name
				});
				Block_4:
				compiler.ToParent();
			}
		}

		// Token: 0x06002377 RID: 9079 RVA: 0x000DC29D File Offset: 0x000DA49D
		public void CheckRequiredAttribute(Compiler compiler, object attrValue, string attrName)
		{
			this.CheckRequiredAttribute(compiler, attrValue != null, attrName);
		}

		// Token: 0x06002378 RID: 9080 RVA: 0x000DC2AB File Offset: 0x000DA4AB
		public void CheckRequiredAttribute(Compiler compiler, bool attr, string attrName)
		{
			if (!attr)
			{
				throw XsltException.Create("Missing mandatory attribute '{0}'.", new string[]
				{
					attrName
				});
			}
		}

		// Token: 0x06002379 RID: 9081 RVA: 0x000DC2C5 File Offset: 0x000DA4C5
		protected CompiledAction()
		{
		}
	}
}
