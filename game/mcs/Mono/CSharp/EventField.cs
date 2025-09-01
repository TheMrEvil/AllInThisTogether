using System;
using System.Collections.Generic;

namespace Mono.CSharp
{
	// Token: 0x0200027A RID: 634
	public class EventField : Event
	{
		// Token: 0x06001F0F RID: 7951 RVA: 0x0009937F File Offset: 0x0009757F
		public EventField(TypeDefinition parent, FullNamedExpression type, Modifiers mod_flags, MemberName name, Attributes attrs) : base(parent, type, mod_flags, name, attrs)
		{
			base.Add = new EventField.AddDelegateMethod(this);
			base.Remove = new EventField.RemoveDelegateMethod(this);
		}

		// Token: 0x17000728 RID: 1832
		// (get) Token: 0x06001F10 RID: 7952 RVA: 0x000993A6 File Offset: 0x000975A6
		public List<FieldDeclarator> Declarators
		{
			get
			{
				return this.declarators;
			}
		}

		// Token: 0x17000729 RID: 1833
		// (get) Token: 0x06001F11 RID: 7953 RVA: 0x000993AE File Offset: 0x000975AE
		private bool HasBackingField
		{
			get
			{
				return !this.IsInterface && (base.ModFlags & Modifiers.ABSTRACT) == (Modifiers)0;
			}
		}

		// Token: 0x1700072A RID: 1834
		// (get) Token: 0x06001F12 RID: 7954 RVA: 0x000993C6 File Offset: 0x000975C6
		// (set) Token: 0x06001F13 RID: 7955 RVA: 0x000993CE File Offset: 0x000975CE
		public Expression Initializer
		{
			get
			{
				return this.initializer;
			}
			set
			{
				this.initializer = value;
			}
		}

		// Token: 0x1700072B RID: 1835
		// (get) Token: 0x06001F14 RID: 7956 RVA: 0x000993D7 File Offset: 0x000975D7
		public override string[] ValidAttributeTargets
		{
			get
			{
				if (!this.HasBackingField)
				{
					return EventField.attribute_targets_interface;
				}
				return EventField.attribute_targets;
			}
		}

		// Token: 0x06001F15 RID: 7957 RVA: 0x000993EC File Offset: 0x000975EC
		public override void Accept(StructuralVisitor visitor)
		{
			visitor.Visit(this);
		}

		// Token: 0x06001F16 RID: 7958 RVA: 0x000993F5 File Offset: 0x000975F5
		public void AddDeclarator(FieldDeclarator declarator)
		{
			if (this.declarators == null)
			{
				this.declarators = new List<FieldDeclarator>(2);
			}
			this.declarators.Add(declarator);
			this.Parent.AddNameToContainer(this, declarator.Name.Value);
		}

		// Token: 0x06001F17 RID: 7959 RVA: 0x00099430 File Offset: 0x00097630
		public override void ApplyAttributeBuilder(Attribute a, MethodSpec ctor, byte[] cdata, PredefinedAttributes pa)
		{
			if (a.Target == AttributeTargets.Field)
			{
				this.backing_field.ApplyAttributeBuilder(a, ctor, cdata, pa);
				return;
			}
			if (a.Target == AttributeTargets.Method)
			{
				int errors = base.Report.Errors;
				base.Add.ApplyAttributeBuilder(a, ctor, cdata, pa);
				if (errors == base.Report.Errors)
				{
					base.Remove.ApplyAttributeBuilder(a, ctor, cdata, pa);
				}
				return;
			}
			base.ApplyAttributeBuilder(a, ctor, cdata, pa);
		}

		// Token: 0x06001F18 RID: 7960 RVA: 0x000994AC File Offset: 0x000976AC
		public override bool Define()
		{
			Modifiers modifiers = base.ModFlags;
			if (!base.Define())
			{
				return false;
			}
			if (this.declarators != null)
			{
				if ((modifiers & Modifiers.DEFAULT_ACCESS_MODIFIER) != (Modifiers)0)
				{
					modifiers &= ~(Modifiers.PROTECTED | Modifiers.PUBLIC | Modifiers.PRIVATE | Modifiers.INTERNAL | Modifiers.DEFAULT_ACCESS_MODIFIER);
				}
				TypeExpression type = new TypeExpression(base.MemberType, base.TypeExpression.Location);
				foreach (FieldDeclarator fieldDeclarator in this.declarators)
				{
					EventField eventField = new EventField(this.Parent, type, modifiers, new MemberName(fieldDeclarator.Name.Value, fieldDeclarator.Name.Location), base.OptAttributes);
					if (fieldDeclarator.Initializer != null)
					{
						eventField.initializer = fieldDeclarator.Initializer;
					}
					eventField.Define();
					this.Parent.PartialContainer.Members.Add(eventField);
				}
			}
			if (!this.HasBackingField)
			{
				base.SetIsUsed();
				return true;
			}
			this.backing_field = new Field(this.Parent, new TypeExpression(base.MemberType, base.Location), Modifiers.PRIVATE | Modifiers.COMPILER_GENERATED | Modifiers.BACKING_FIELD | (base.ModFlags & (Modifiers.STATIC | Modifiers.UNSAFE)), base.MemberName, null);
			this.Parent.PartialContainer.Members.Add(this.backing_field);
			this.backing_field.Initializer = this.Initializer;
			this.backing_field.ModFlags &= ~Modifiers.COMPILER_GENERATED;
			this.backing_field.Define();
			this.spec.BackingField = this.backing_field.Spec;
			return true;
		}

		// Token: 0x06001F19 RID: 7961 RVA: 0x00099654 File Offset: 0x00097854
		// Note: this type is marked as 'beforefieldinit'.
		static EventField()
		{
		}

		// Token: 0x04000B6A RID: 2922
		private static readonly string[] attribute_targets = new string[]
		{
			"event",
			"field",
			"method"
		};

		// Token: 0x04000B6B RID: 2923
		private static readonly string[] attribute_targets_interface = new string[]
		{
			"event",
			"method"
		};

		// Token: 0x04000B6C RID: 2924
		private Expression initializer;

		// Token: 0x04000B6D RID: 2925
		private Field backing_field;

		// Token: 0x04000B6E RID: 2926
		private List<FieldDeclarator> declarators;

		// Token: 0x020003E1 RID: 993
		private abstract class EventFieldAccessor : Event.AEventAccessor
		{
			// Token: 0x060027AE RID: 10158 RVA: 0x000BCA8B File Offset: 0x000BAC8B
			protected EventFieldAccessor(EventField method, string prefix) : base(method, prefix, null, method.Location)
			{
			}

			// Token: 0x060027AF RID: 10159
			protected abstract MethodSpec GetOperation(Location loc);

			// Token: 0x060027B0 RID: 10160 RVA: 0x000BCA9C File Offset: 0x000BAC9C
			public override void Emit(TypeDefinition parent)
			{
				if ((this.method.ModFlags & (Modifiers.ABSTRACT | Modifiers.EXTERN)) == (Modifiers)0 && !this.Compiler.Settings.WriteMetadataOnly)
				{
					this.block = new ToplevelBlock(this.Compiler, this.ParameterInfo, base.Location, (Block.Flags)0)
					{
						IsCompilerGenerated = true
					};
					this.FabricateBodyStatement();
				}
				base.Emit(parent);
			}

			// Token: 0x060027B1 RID: 10161 RVA: 0x000BCB00 File Offset: 0x000BAD00
			private void FabricateBodyStatement()
			{
				Field backing_field = ((EventField)this.method).backing_field;
				FieldExpr fieldExpr = new FieldExpr(backing_field, base.Location);
				if (!base.IsStatic)
				{
					fieldExpr.InstanceExpression = new CompilerGeneratedThis(this.Parent.CurrentType, base.Location);
				}
				LocalVariable li = LocalVariable.CreateCompilerGenerated(backing_field.MemberType, this.block, base.Location);
				LocalVariable li2 = LocalVariable.CreateCompilerGenerated(backing_field.MemberType, this.block, base.Location);
				this.block.AddStatement(new StatementExpression(new SimpleAssign(new LocalVariableReference(li, base.Location), fieldExpr)));
				BooleanExpression bool_expr = new BooleanExpression(new Binary(Binary.Operator.Inequality, new Cast(new TypeExpression(this.Module.Compiler.BuiltinTypes.Object, base.Location), new LocalVariableReference(li, base.Location), base.Location), new Cast(new TypeExpression(this.Module.Compiler.BuiltinTypes.Object, base.Location), new LocalVariableReference(li2, base.Location), base.Location)));
				ExplicitBlock explicitBlock = new ExplicitBlock(this.block, base.Location, base.Location);
				this.block.AddStatement(new Do(explicitBlock, bool_expr, base.Location, base.Location));
				explicitBlock.AddStatement(new StatementExpression(new SimpleAssign(new LocalVariableReference(li2, base.Location), new LocalVariableReference(li, base.Location))));
				Arguments arguments = new Arguments(2);
				arguments.Add(new Argument(new LocalVariableReference(li2, base.Location)));
				arguments.Add(new Argument(this.block.GetParameterReference(0, base.Location)));
				MethodSpec operation = this.GetOperation(base.Location);
				Arguments arguments2 = new Arguments(3);
				arguments2.Add(new Argument(fieldExpr, Argument.AType.Ref));
				Arguments arguments3 = arguments2;
				Expression cast_type = new TypeExpression(backing_field.MemberType, base.Location);
				MethodSpec methodSpec = operation;
				arguments3.Add(new Argument(new Cast(cast_type, new Invocation(MethodGroupExpr.CreatePredefined(methodSpec, methodSpec.DeclaringType, base.Location), arguments), base.Location)));
				arguments2.Add(new Argument(new LocalVariableReference(li, base.Location)));
				MethodSpec methodSpec2 = this.Module.PredefinedMembers.InterlockedCompareExchange_T.Get();
				if (methodSpec2 != null)
				{
					Block block = explicitBlock;
					Expression target = new LocalVariableReference(li, base.Location);
					MethodSpec methodSpec3 = methodSpec2;
					block.AddStatement(new StatementExpression(new SimpleAssign(target, new Invocation(MethodGroupExpr.CreatePredefined(methodSpec3, methodSpec3.DeclaringType, base.Location), arguments2))));
					return;
				}
				if (this.Module.PredefinedMembers.MonitorEnter_v4.Get() != null || this.Module.PredefinedMembers.MonitorEnter.Get() != null)
				{
					explicitBlock.AddStatement(new Lock(this.block.GetParameterReference(0, base.Location), new StatementExpression(new SimpleAssign(fieldExpr, arguments2[1].Expr, base.Location), base.Location), base.Location));
					return;
				}
				this.Module.PredefinedMembers.InterlockedCompareExchange_T.Resolve(base.Location);
			}
		}

		// Token: 0x020003E2 RID: 994
		private sealed class AddDelegateMethod : EventField.EventFieldAccessor
		{
			// Token: 0x060027B2 RID: 10162 RVA: 0x000BCE27 File Offset: 0x000BB027
			public AddDelegateMethod(EventField method) : base(method, "add_")
			{
			}

			// Token: 0x060027B3 RID: 10163 RVA: 0x000BCE35 File Offset: 0x000BB035
			protected override MethodSpec GetOperation(Location loc)
			{
				return this.Module.PredefinedMembers.DelegateCombine.Resolve(loc);
			}
		}

		// Token: 0x020003E3 RID: 995
		private sealed class RemoveDelegateMethod : EventField.EventFieldAccessor
		{
			// Token: 0x060027B4 RID: 10164 RVA: 0x000BCE4D File Offset: 0x000BB04D
			public RemoveDelegateMethod(EventField method) : base(method, "remove_")
			{
			}

			// Token: 0x060027B5 RID: 10165 RVA: 0x000BCE5B File Offset: 0x000BB05B
			protected override MethodSpec GetOperation(Location loc)
			{
				return this.Module.PredefinedMembers.DelegateRemove.Resolve(loc);
			}
		}
	}
}
