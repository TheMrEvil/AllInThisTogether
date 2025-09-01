using System;

namespace Mono.CSharp
{
	// Token: 0x0200011F RID: 287
	public abstract class Attributable
	{
		// Token: 0x06000E07 RID: 3591 RVA: 0x00034664 File Offset: 0x00032864
		public void AddAttributes(Attributes attrs, IMemberContext context)
		{
			if (attrs == null)
			{
				return;
			}
			if (this.attributes == null)
			{
				this.attributes = attrs;
			}
			else
			{
				this.attributes.AddAttributes(attrs.Attrs);
			}
			attrs.AttachTo(this, context);
		}

		// Token: 0x17000401 RID: 1025
		// (get) Token: 0x06000E08 RID: 3592 RVA: 0x00034694 File Offset: 0x00032894
		// (set) Token: 0x06000E09 RID: 3593 RVA: 0x0003469C File Offset: 0x0003289C
		public Attributes OptAttributes
		{
			get
			{
				return this.attributes;
			}
			set
			{
				this.attributes = value;
			}
		}

		// Token: 0x06000E0A RID: 3594
		public abstract void ApplyAttributeBuilder(Attribute a, MethodSpec ctor, byte[] cdata, PredefinedAttributes pa);

		// Token: 0x17000402 RID: 1026
		// (get) Token: 0x06000E0B RID: 3595
		public abstract AttributeTargets AttributeTargets { get; }

		// Token: 0x06000E0C RID: 3596
		public abstract bool IsClsComplianceRequired();

		// Token: 0x17000403 RID: 1027
		// (get) Token: 0x06000E0D RID: 3597
		public abstract string[] ValidAttributeTargets { get; }

		// Token: 0x06000E0E RID: 3598 RVA: 0x00002CCC File Offset: 0x00000ECC
		protected Attributable()
		{
		}

		// Token: 0x04000681 RID: 1665
		protected Attributes attributes;
	}
}
