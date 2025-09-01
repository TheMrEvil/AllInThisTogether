using System;

namespace Mono.CSharp
{
	// Token: 0x02000279 RID: 633
	public class EventProperty : Event
	{
		// Token: 0x06001F0A RID: 7946 RVA: 0x00099338 File Offset: 0x00097538
		public EventProperty(TypeDefinition parent, FullNamedExpression type, Modifiers mod_flags, MemberName name, Attributes attrs) : base(parent, type, mod_flags, name, attrs)
		{
		}

		// Token: 0x06001F0B RID: 7947 RVA: 0x00099347 File Offset: 0x00097547
		public override void Accept(StructuralVisitor visitor)
		{
			visitor.Visit(this);
		}

		// Token: 0x06001F0C RID: 7948 RVA: 0x00099350 File Offset: 0x00097550
		public override bool Define()
		{
			if (!base.Define())
			{
				return false;
			}
			base.SetIsUsed();
			return true;
		}

		// Token: 0x17000727 RID: 1831
		// (get) Token: 0x06001F0D RID: 7949 RVA: 0x00099363 File Offset: 0x00097563
		public override string[] ValidAttributeTargets
		{
			get
			{
				return EventProperty.attribute_targets;
			}
		}

		// Token: 0x06001F0E RID: 7950 RVA: 0x0009936A File Offset: 0x0009756A
		// Note: this type is marked as 'beforefieldinit'.
		static EventProperty()
		{
		}

		// Token: 0x04000B69 RID: 2921
		private static readonly string[] attribute_targets = new string[]
		{
			"event"
		};

		// Token: 0x020003DE RID: 990
		public abstract class AEventPropertyAccessor : Event.AEventAccessor
		{
			// Token: 0x060027A9 RID: 10153 RVA: 0x000BCA15 File Offset: 0x000BAC15
			protected AEventPropertyAccessor(EventProperty method, string prefix, Attributes attrs, Location loc) : base(method, prefix, attrs, loc)
			{
			}

			// Token: 0x060027AA RID: 10154 RVA: 0x000BCA22 File Offset: 0x000BAC22
			public override void Define(TypeContainer ds)
			{
				base.CheckAbstractAndExtern(this.block != null);
				base.Define(ds);
			}

			// Token: 0x060027AB RID: 10155 RVA: 0x000BCA3B File Offset: 0x000BAC3B
			public override string GetSignatureForError()
			{
				return this.method.GetSignatureForError() + "." + this.prefix.Substring(0, this.prefix.Length - 1);
			}
		}

		// Token: 0x020003DF RID: 991
		public sealed class AddDelegateMethod : EventProperty.AEventPropertyAccessor
		{
			// Token: 0x060027AC RID: 10156 RVA: 0x000BCA6B File Offset: 0x000BAC6B
			public AddDelegateMethod(EventProperty method, Attributes attrs, Location loc) : base(method, "add_", attrs, loc)
			{
			}
		}

		// Token: 0x020003E0 RID: 992
		public sealed class RemoveDelegateMethod : EventProperty.AEventPropertyAccessor
		{
			// Token: 0x060027AD RID: 10157 RVA: 0x000BCA7B File Offset: 0x000BAC7B
			public RemoveDelegateMethod(EventProperty method, Attributes attrs, Location loc) : base(method, "remove_", attrs, loc)
			{
			}
		}
	}
}
