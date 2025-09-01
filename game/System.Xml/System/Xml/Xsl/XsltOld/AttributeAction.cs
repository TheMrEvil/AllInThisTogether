using System;
using System.Xml.XPath;

namespace System.Xml.Xsl.XsltOld
{
	// Token: 0x02000354 RID: 852
	internal class AttributeAction : ContainerAction
	{
		// Token: 0x0600233C RID: 9020 RVA: 0x000DB450 File Offset: 0x000D9650
		private static PrefixQName CreateAttributeQName(string name, string nsUri, InputScopeManager manager)
		{
			if (name == "xmlns")
			{
				return null;
			}
			if (nsUri == "http://www.w3.org/2000/xmlns/")
			{
				throw XsltException.Create("Elements and attributes cannot belong to the reserved namespace '{0}'.", new string[]
				{
					nsUri
				});
			}
			PrefixQName prefixQName = new PrefixQName();
			prefixQName.SetQName(name);
			prefixQName.Namespace = ((nsUri != null) ? nsUri : manager.ResolveXPathNamespace(prefixQName.Prefix));
			if (prefixQName.Prefix.StartsWith("xml", StringComparison.Ordinal))
			{
				if (prefixQName.Prefix.Length == 3)
				{
					if (!(prefixQName.Namespace == "http://www.w3.org/XML/1998/namespace") || (!(prefixQName.Name == "lang") && !(prefixQName.Name == "space")))
					{
						prefixQName.ClearPrefix();
					}
				}
				else if (prefixQName.Prefix == "xmlns")
				{
					if (prefixQName.Namespace == "http://www.w3.org/2000/xmlns/")
					{
						throw XsltException.Create("Prefix '{0}' is not defined.", new string[]
						{
							prefixQName.Prefix
						});
					}
					prefixQName.ClearPrefix();
				}
			}
			return prefixQName;
		}

		// Token: 0x0600233D RID: 9021 RVA: 0x000DB55C File Offset: 0x000D975C
		internal override void Compile(Compiler compiler)
		{
			base.CompileAttributes(compiler);
			base.CheckRequiredAttribute(compiler, this.nameAvt, "name");
			this.name = CompiledAction.PrecalculateAvt(ref this.nameAvt);
			this.nsUri = CompiledAction.PrecalculateAvt(ref this.nsAvt);
			if (this.nameAvt == null && this.nsAvt == null)
			{
				if (this.name != "xmlns")
				{
					this.qname = AttributeAction.CreateAttributeQName(this.name, this.nsUri, compiler.CloneScopeManager());
				}
			}
			else
			{
				this.manager = compiler.CloneScopeManager();
			}
			if (compiler.Recurse())
			{
				base.CompileTemplate(compiler);
				compiler.ToParent();
			}
		}

		// Token: 0x0600233E RID: 9022 RVA: 0x000DB608 File Offset: 0x000D9808
		internal override bool CompileAttribute(Compiler compiler)
		{
			string localName = compiler.Input.LocalName;
			string value = compiler.Input.Value;
			if (Ref.Equal(localName, compiler.Atoms.Name))
			{
				this.nameAvt = Avt.CompileAvt(compiler, value);
			}
			else
			{
				if (!Ref.Equal(localName, compiler.Atoms.Namespace))
				{
					return false;
				}
				this.nsAvt = Avt.CompileAvt(compiler, value);
			}
			return true;
		}

		// Token: 0x0600233F RID: 9023 RVA: 0x000DB674 File Offset: 0x000D9874
		internal override void Execute(Processor processor, ActionFrame frame)
		{
			switch (frame.State)
			{
			case 0:
				if (this.qname != null)
				{
					frame.CalulatedName = this.qname;
				}
				else
				{
					frame.CalulatedName = AttributeAction.CreateAttributeQName((this.nameAvt == null) ? this.name : this.nameAvt.Evaluate(processor, frame), (this.nsAvt == null) ? this.nsUri : this.nsAvt.Evaluate(processor, frame), this.manager);
					if (frame.CalulatedName == null)
					{
						frame.Finished();
						return;
					}
				}
				break;
			case 1:
				if (!processor.EndEvent(XPathNodeType.Attribute))
				{
					frame.State = 1;
					return;
				}
				frame.Finished();
				return;
			case 2:
				break;
			default:
				return;
			}
			PrefixQName calulatedName = frame.CalulatedName;
			if (!processor.BeginEvent(XPathNodeType.Attribute, calulatedName.Prefix, calulatedName.Name, calulatedName.Namespace, false))
			{
				frame.State = 2;
				return;
			}
			processor.PushActionFrame(frame);
			frame.State = 1;
		}

		// Token: 0x06002340 RID: 9024 RVA: 0x000DB75C File Offset: 0x000D995C
		public AttributeAction()
		{
		}

		// Token: 0x04001C76 RID: 7286
		private const int NameDone = 2;

		// Token: 0x04001C77 RID: 7287
		private Avt nameAvt;

		// Token: 0x04001C78 RID: 7288
		private Avt nsAvt;

		// Token: 0x04001C79 RID: 7289
		private InputScopeManager manager;

		// Token: 0x04001C7A RID: 7290
		private string name;

		// Token: 0x04001C7B RID: 7291
		private string nsUri;

		// Token: 0x04001C7C RID: 7292
		private PrefixQName qname;
	}
}
