using System;
using System.ComponentModel;
using UnityEngine.Bindings;

namespace UnityEngine.Playables
{
	// Token: 0x02000440 RID: 1088
	public struct PlayableBinding
	{
		// Token: 0x1700072A RID: 1834
		// (get) Token: 0x060025AC RID: 9644 RVA: 0x0003F824 File Offset: 0x0003DA24
		// (set) Token: 0x060025AD RID: 9645 RVA: 0x0003F83C File Offset: 0x0003DA3C
		public string streamName
		{
			get
			{
				return this.m_StreamName;
			}
			set
			{
				this.m_StreamName = value;
			}
		}

		// Token: 0x1700072B RID: 1835
		// (get) Token: 0x060025AE RID: 9646 RVA: 0x0003F848 File Offset: 0x0003DA48
		// (set) Token: 0x060025AF RID: 9647 RVA: 0x0003F860 File Offset: 0x0003DA60
		public Object sourceObject
		{
			get
			{
				return this.m_SourceObject;
			}
			set
			{
				this.m_SourceObject = value;
			}
		}

		// Token: 0x1700072C RID: 1836
		// (get) Token: 0x060025B0 RID: 9648 RVA: 0x0003F86C File Offset: 0x0003DA6C
		public Type outputTargetType
		{
			get
			{
				return this.m_SourceBindingType;
			}
		}

		// Token: 0x1700072D RID: 1837
		// (get) Token: 0x060025B1 RID: 9649 RVA: 0x0003F884 File Offset: 0x0003DA84
		// (set) Token: 0x060025B2 RID: 9650 RVA: 0x00004563 File Offset: 0x00002763
		[Obsolete("sourceBindingType is no longer supported on PlayableBinding. Use outputBindingType instead to get the required output target type, and the appropriate binding create method (e.g. AnimationPlayableBinding.Create(name, key)) to create PlayableBindings", true)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public Type sourceBindingType
		{
			get
			{
				return this.m_SourceBindingType;
			}
			set
			{
			}
		}

		// Token: 0x1700072E RID: 1838
		// (get) Token: 0x060025B3 RID: 9651 RVA: 0x0003F89C File Offset: 0x0003DA9C
		// (set) Token: 0x060025B4 RID: 9652 RVA: 0x00004563 File Offset: 0x00002763
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Obsolete("streamType is no longer supported on PlayableBinding. Use the appropriate binding create method (e.g. AnimationPlayableBinding.Create(name, key)) instead.", true)]
		public DataStreamType streamType
		{
			get
			{
				return DataStreamType.None;
			}
			set
			{
			}
		}

		// Token: 0x060025B5 RID: 9653 RVA: 0x0003F8B0 File Offset: 0x0003DAB0
		internal PlayableOutput CreateOutput(PlayableGraph graph)
		{
			bool flag = this.m_CreateOutputMethod != null;
			PlayableOutput result;
			if (flag)
			{
				result = this.m_CreateOutputMethod(graph, this.m_StreamName);
			}
			else
			{
				result = PlayableOutput.Null;
			}
			return result;
		}

		// Token: 0x060025B6 RID: 9654 RVA: 0x0003F8EC File Offset: 0x0003DAEC
		[VisibleToOtherModules]
		internal static PlayableBinding CreateInternal(string name, Object sourceObject, Type sourceType, PlayableBinding.CreateOutputMethod createFunction)
		{
			return new PlayableBinding
			{
				m_StreamName = name,
				m_SourceObject = sourceObject,
				m_SourceBindingType = sourceType,
				m_CreateOutputMethod = createFunction
			};
		}

		// Token: 0x060025B7 RID: 9655 RVA: 0x0003F927 File Offset: 0x0003DB27
		// Note: this type is marked as 'beforefieldinit'.
		static PlayableBinding()
		{
		}

		// Token: 0x04000E17 RID: 3607
		private string m_StreamName;

		// Token: 0x04000E18 RID: 3608
		private Object m_SourceObject;

		// Token: 0x04000E19 RID: 3609
		private Type m_SourceBindingType;

		// Token: 0x04000E1A RID: 3610
		private PlayableBinding.CreateOutputMethod m_CreateOutputMethod;

		// Token: 0x04000E1B RID: 3611
		public static readonly PlayableBinding[] None = new PlayableBinding[0];

		// Token: 0x04000E1C RID: 3612
		public static readonly double DefaultDuration = double.PositiveInfinity;

		// Token: 0x02000441 RID: 1089
		// (Invoke) Token: 0x060025B9 RID: 9657
		[VisibleToOtherModules]
		internal delegate PlayableOutput CreateOutputMethod(PlayableGraph graph, string name);
	}
}
