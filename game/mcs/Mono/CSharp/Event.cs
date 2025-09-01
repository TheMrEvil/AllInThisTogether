using System;
using System.Reflection;
using System.Reflection.Emit;
using Mono.CompilerServices.SymbolWriter;

namespace Mono.CSharp
{
	// Token: 0x0200027B RID: 635
	public abstract class Event : PropertyBasedMember
	{
		// Token: 0x06001F1A RID: 7962 RVA: 0x00099694 File Offset: 0x00097894
		protected Event(TypeDefinition parent, FullNamedExpression type, Modifiers mod_flags, MemberName name, Attributes attrs) : base(parent, type, mod_flags, (parent.PartialContainer.Kind == MemberKind.Interface) ? (Modifiers.NEW | Modifiers.UNSAFE) : ((parent.PartialContainer.Kind == MemberKind.Struct) ? (Modifiers.PROTECTED | Modifiers.PUBLIC | Modifiers.PRIVATE | Modifiers.INTERNAL | Modifiers.NEW | Modifiers.STATIC | Modifiers.OVERRIDE | Modifiers.EXTERN | Modifiers.UNSAFE) : (Modifiers.PROTECTED | Modifiers.PUBLIC | Modifiers.PRIVATE | Modifiers.INTERNAL | Modifiers.NEW | Modifiers.ABSTRACT | Modifiers.SEALED | Modifiers.STATIC | Modifiers.VIRTUAL | Modifiers.OVERRIDE | Modifiers.EXTERN | Modifiers.UNSAFE)), name, attrs)
		{
		}

		// Token: 0x1700072C RID: 1836
		// (get) Token: 0x06001F1B RID: 7963 RVA: 0x000996E5 File Offset: 0x000978E5
		public override AttributeTargets AttributeTargets
		{
			get
			{
				return AttributeTargets.Event;
			}
		}

		// Token: 0x1700072D RID: 1837
		// (get) Token: 0x06001F1C RID: 7964 RVA: 0x000996EC File Offset: 0x000978EC
		// (set) Token: 0x06001F1D RID: 7965 RVA: 0x000996F4 File Offset: 0x000978F4
		public Event.AEventAccessor Add
		{
			get
			{
				return this.add;
			}
			set
			{
				this.add = value;
				this.Parent.AddNameToContainer(value, value.MemberName.Basename);
			}
		}

		// Token: 0x1700072E RID: 1838
		// (get) Token: 0x06001F1E RID: 7966 RVA: 0x00099714 File Offset: 0x00097914
		public override Variance ExpectedMemberTypeVariance
		{
			get
			{
				return Variance.Contravariant;
			}
		}

		// Token: 0x1700072F RID: 1839
		// (get) Token: 0x06001F1F RID: 7967 RVA: 0x00099717 File Offset: 0x00097917
		// (set) Token: 0x06001F20 RID: 7968 RVA: 0x0009971F File Offset: 0x0009791F
		public Event.AEventAccessor Remove
		{
			get
			{
				return this.remove;
			}
			set
			{
				this.remove = value;
				this.Parent.AddNameToContainer(value, value.MemberName.Basename);
			}
		}

		// Token: 0x06001F21 RID: 7969 RVA: 0x0009973F File Offset: 0x0009793F
		public override void ApplyAttributeBuilder(Attribute a, MethodSpec ctor, byte[] cdata, PredefinedAttributes pa)
		{
			if (a.HasSecurityAttribute)
			{
				a.Error_InvalidSecurityParent();
				return;
			}
			this.EventBuilder.SetCustomAttribute((ConstructorInfo)ctor.GetMetaInfo(), cdata);
		}

		// Token: 0x06001F22 RID: 7970 RVA: 0x00099768 File Offset: 0x00097968
		protected override bool CheckOverrideAgainstBase(MemberSpec base_member)
		{
			bool result = base.CheckOverrideAgainstBase(base_member);
			if (!InterfaceMemberBase.CheckAccessModifiers(this, base_member))
			{
				base.Error_CannotChangeAccessModifiers(this, base_member);
				result = false;
			}
			return result;
		}

		// Token: 0x06001F23 RID: 7971 RVA: 0x00099794 File Offset: 0x00097994
		public override bool Define()
		{
			if (!base.Define())
			{
				return false;
			}
			if (!base.MemberType.IsDelegate)
			{
				base.Report.Error(66, base.Location, "`{0}': event must be of a delegate type", this.GetSignatureForError());
			}
			if (!this.CheckBase())
			{
				return false;
			}
			this.add.Define(this.Parent);
			this.remove.Define(this.Parent);
			this.EventBuilder = this.Parent.TypeBuilder.DefineEvent(base.GetFullName(base.MemberName), EventAttributes.None, base.MemberType.GetMetaInfo());
			this.spec = new EventSpec(this.Parent.Definition, this, base.MemberType, base.ModFlags, this.Add.Spec, this.remove.Spec);
			this.Parent.MemberCache.AddMember(this, base.GetFullName(base.MemberName), this.spec);
			this.Parent.MemberCache.AddMember(this, this.Add.Spec.Name, this.Add.Spec);
			this.Parent.MemberCache.AddMember(this, this.Remove.Spec.Name, this.remove.Spec);
			return true;
		}

		// Token: 0x06001F24 RID: 7972 RVA: 0x000998E8 File Offset: 0x00097AE8
		public override void Emit()
		{
			base.CheckReservedNameConflict(null, this.add.Spec);
			base.CheckReservedNameConflict(null, this.remove.Spec);
			if (base.OptAttributes != null)
			{
				base.OptAttributes.Emit();
			}
			ConstraintChecker.Check(this, this.member_type, this.type_expr.Location);
			this.Add.Emit(this.Parent);
			this.Remove.Emit(this.Parent);
			base.Emit();
		}

		// Token: 0x06001F25 RID: 7973 RVA: 0x0009996C File Offset: 0x00097B6C
		public override void PrepareEmit()
		{
			this.add.PrepareEmit();
			this.remove.PrepareEmit();
			this.EventBuilder.SetAddOnMethod(this.add.MethodData.MethodBuilder);
			this.EventBuilder.SetRemoveOnMethod(this.remove.MethodData.MethodBuilder);
		}

		// Token: 0x06001F26 RID: 7974 RVA: 0x000999C5 File Offset: 0x00097BC5
		public override void WriteDebugSymbol(MonoSymbolFile file)
		{
			this.add.WriteDebugSymbol(file);
			this.remove.WriteDebugSymbol(file);
		}

		// Token: 0x17000730 RID: 1840
		// (get) Token: 0x06001F27 RID: 7975 RVA: 0x000999DF File Offset: 0x00097BDF
		public override string DocCommentHeader
		{
			get
			{
				return "E:";
			}
		}

		// Token: 0x04000B6F RID: 2927
		private Event.AEventAccessor add;

		// Token: 0x04000B70 RID: 2928
		private Event.AEventAccessor remove;

		// Token: 0x04000B71 RID: 2929
		private EventBuilder EventBuilder;

		// Token: 0x04000B72 RID: 2930
		protected EventSpec spec;

		// Token: 0x020003E4 RID: 996
		public abstract class AEventAccessor : AbstractPropertyEventMethod
		{
			// Token: 0x060027B6 RID: 10166 RVA: 0x000BCE73 File Offset: 0x000BB073
			protected AEventAccessor(Event method, string prefix, Attributes attrs, Location loc) : base(method, prefix, attrs, loc)
			{
				this.method = method;
				base.ModFlags = method.ModFlags;
				this.parameters = ParametersCompiled.CreateImplicitParameter(method.TypeExpression, loc);
			}

			// Token: 0x170008F5 RID: 2293
			// (get) Token: 0x060027B7 RID: 10167 RVA: 0x000BCEA6 File Offset: 0x000BB0A6
			public bool IsInterfaceImplementation
			{
				get
				{
					return this.method_data.implementing != null;
				}
			}

			// Token: 0x060027B8 RID: 10168 RVA: 0x000BCEB6 File Offset: 0x000BB0B6
			public override void ApplyAttributeBuilder(Attribute a, MethodSpec ctor, byte[] cdata, PredefinedAttributes pa)
			{
				if (a.Type == pa.MethodImpl)
				{
					this.method.is_external_implementation = a.IsInternalCall();
				}
				base.ApplyAttributeBuilder(a, ctor, cdata, pa);
			}

			// Token: 0x060027B9 RID: 10169 RVA: 0x000BCEE8 File Offset: 0x000BB0E8
			protected override void ApplyToExtraTarget(Attribute a, MethodSpec ctor, byte[] cdata, PredefinedAttributes pa)
			{
				if (a.Target == AttributeTargets.Parameter)
				{
					this.parameters[0].ApplyAttributeBuilder(a, ctor, cdata, pa);
					return;
				}
				base.ApplyToExtraTarget(a, ctor, cdata, pa);
			}

			// Token: 0x170008F6 RID: 2294
			// (get) Token: 0x060027BA RID: 10170 RVA: 0x0008DC6A File Offset: 0x0008BE6A
			public override AttributeTargets AttributeTargets
			{
				get
				{
					return AttributeTargets.Method;
				}
			}

			// Token: 0x060027BB RID: 10171 RVA: 0x000BCF19 File Offset: 0x000BB119
			public override bool IsClsComplianceRequired()
			{
				return this.method.IsClsComplianceRequired();
			}

			// Token: 0x060027BC RID: 10172 RVA: 0x000BCF28 File Offset: 0x000BB128
			public virtual void Define(TypeContainer parent)
			{
				((Parameter)this.parameters.FixedParameters[0]).Type = this.method.member_type;
				this.parameters.Types = new TypeSpec[]
				{
					this.method.member_type
				};
				this.method_data = new MethodData(this.method, this.method.ModFlags, this.method.flags | MethodAttributes.HideBySig | MethodAttributes.SpecialName, this);
				if (!this.method_data.Define(parent.PartialContainer, this.method.GetFullName(base.MemberName)))
				{
					return;
				}
				if (this.Compiler.Settings.WriteMetadataOnly)
				{
					this.block = null;
				}
				base.Spec = new MethodSpec(MemberKind.Method, parent.PartialContainer.Definition, this, this.ReturnType, this.ParameterInfo, this.method.ModFlags);
				base.Spec.IsAccessor = true;
			}

			// Token: 0x170008F7 RID: 2295
			// (get) Token: 0x060027BD RID: 10173 RVA: 0x000BC6A4 File Offset: 0x000BA8A4
			public override TypeSpec ReturnType
			{
				get
				{
					return this.Parent.Compiler.BuiltinTypes.Void;
				}
			}

			// Token: 0x060027BE RID: 10174 RVA: 0x000BD023 File Offset: 0x000BB223
			public override ObsoleteAttribute GetAttributeObsolete()
			{
				return this.method.GetAttributeObsolete();
			}

			// Token: 0x170008F8 RID: 2296
			// (get) Token: 0x060027BF RID: 10175 RVA: 0x000BD030 File Offset: 0x000BB230
			public MethodData MethodData
			{
				get
				{
					return this.method_data;
				}
			}

			// Token: 0x170008F9 RID: 2297
			// (get) Token: 0x060027C0 RID: 10176 RVA: 0x000BD038 File Offset: 0x000BB238
			public override string[] ValidAttributeTargets
			{
				get
				{
					return Event.AEventAccessor.attribute_targets;
				}
			}

			// Token: 0x170008FA RID: 2298
			// (get) Token: 0x060027C1 RID: 10177 RVA: 0x000BD03F File Offset: 0x000BB23F
			public override ParametersCompiled ParameterInfo
			{
				get
				{
					return this.parameters;
				}
			}

			// Token: 0x060027C2 RID: 10178 RVA: 0x000BD047 File Offset: 0x000BB247
			// Note: this type is marked as 'beforefieldinit'.
			static AEventAccessor()
			{
			}

			// Token: 0x0400110F RID: 4367
			protected readonly Event method;

			// Token: 0x04001110 RID: 4368
			private readonly ParametersCompiled parameters;

			// Token: 0x04001111 RID: 4369
			private static readonly string[] attribute_targets = new string[]
			{
				"method",
				"param",
				"return"
			};

			// Token: 0x04001112 RID: 4370
			public const string AddPrefix = "add_";

			// Token: 0x04001113 RID: 4371
			public const string RemovePrefix = "remove_";
		}
	}
}
