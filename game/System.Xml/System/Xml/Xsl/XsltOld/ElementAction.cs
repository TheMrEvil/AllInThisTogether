using System;
using System.Xml.XPath;

namespace System.Xml.Xsl.XsltOld
{
	// Token: 0x02000386 RID: 902
	internal class ElementAction : ContainerAction
	{
		// Token: 0x060024B7 RID: 9399 RVA: 0x000DB75C File Offset: 0x000D995C
		internal ElementAction()
		{
		}

		// Token: 0x060024B8 RID: 9400 RVA: 0x000DFF74 File Offset: 0x000DE174
		private static PrefixQName CreateElementQName(string name, string nsUri, InputScopeManager manager)
		{
			if (nsUri == "http://www.w3.org/2000/xmlns/")
			{
				throw XsltException.Create("Elements and attributes cannot belong to the reserved namespace '{0}'.", new string[]
				{
					nsUri
				});
			}
			PrefixQName prefixQName = new PrefixQName();
			prefixQName.SetQName(name);
			if (nsUri == null)
			{
				prefixQName.Namespace = manager.ResolveXmlNamespace(prefixQName.Prefix);
			}
			else
			{
				prefixQName.Namespace = nsUri;
			}
			return prefixQName;
		}

		// Token: 0x060024B9 RID: 9401 RVA: 0x000DFFD0 File Offset: 0x000DE1D0
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
					this.qname = ElementAction.CreateElementQName(this.name, this.nsUri, compiler.CloneScopeManager());
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
			this.empty = (this.containedActions == null);
		}

		// Token: 0x060024BA RID: 9402 RVA: 0x000E008C File Offset: 0x000DE28C
		internal override bool CompileAttribute(Compiler compiler)
		{
			string localName = compiler.Input.LocalName;
			string value = compiler.Input.Value;
			if (Ref.Equal(localName, compiler.Atoms.Name))
			{
				this.nameAvt = Avt.CompileAvt(compiler, value);
			}
			else if (Ref.Equal(localName, compiler.Atoms.Namespace))
			{
				this.nsAvt = Avt.CompileAvt(compiler, value);
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

		// Token: 0x060024BB RID: 9403 RVA: 0x000E011C File Offset: 0x000DE31C
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
					frame.CalulatedName = ElementAction.CreateElementQName((this.nameAvt == null) ? this.name : this.nameAvt.Evaluate(processor, frame), (this.nsAvt == null) ? this.nsUri : this.nsAvt.Evaluate(processor, frame), this.manager);
				}
				break;
			case 1:
				goto IL_C2;
			case 2:
				break;
			default:
				return;
			}
			PrefixQName calulatedName = frame.CalulatedName;
			if (!processor.BeginEvent(XPathNodeType.Element, calulatedName.Prefix, calulatedName.Name, calulatedName.Namespace, this.empty))
			{
				frame.State = 2;
				return;
			}
			if (!this.empty)
			{
				processor.PushActionFrame(frame);
				frame.State = 1;
				return;
			}
			IL_C2:
			if (!processor.EndEvent(XPathNodeType.Element))
			{
				frame.State = 1;
				return;
			}
			frame.Finished();
		}

		// Token: 0x04001D02 RID: 7426
		private const int NameDone = 2;

		// Token: 0x04001D03 RID: 7427
		private Avt nameAvt;

		// Token: 0x04001D04 RID: 7428
		private Avt nsAvt;

		// Token: 0x04001D05 RID: 7429
		private bool empty;

		// Token: 0x04001D06 RID: 7430
		private InputScopeManager manager;

		// Token: 0x04001D07 RID: 7431
		private string name;

		// Token: 0x04001D08 RID: 7432
		private string nsUri;

		// Token: 0x04001D09 RID: 7433
		private PrefixQName qname;
	}
}
