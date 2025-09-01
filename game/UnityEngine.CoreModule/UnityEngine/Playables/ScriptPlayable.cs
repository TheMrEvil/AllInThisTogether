using System;

namespace UnityEngine.Playables
{
	// Token: 0x0200044B RID: 1099
	public struct ScriptPlayable<T> : IPlayable, IEquatable<ScriptPlayable<T>> where T : class, IPlayableBehaviour, new()
	{
		// Token: 0x17000732 RID: 1842
		// (get) Token: 0x060026DB RID: 9947 RVA: 0x00040CF4 File Offset: 0x0003EEF4
		public static ScriptPlayable<T> Null
		{
			get
			{
				return ScriptPlayable<T>.m_NullPlayable;
			}
		}

		// Token: 0x060026DC RID: 9948 RVA: 0x00040D0C File Offset: 0x0003EF0C
		public static ScriptPlayable<T> Create(PlayableGraph graph, int inputCount = 0)
		{
			PlayableHandle handle = ScriptPlayable<T>.CreateHandle(graph, default(T), inputCount);
			return new ScriptPlayable<T>(handle);
		}

		// Token: 0x060026DD RID: 9949 RVA: 0x00040D38 File Offset: 0x0003EF38
		public static ScriptPlayable<T> Create(PlayableGraph graph, T template, int inputCount = 0)
		{
			PlayableHandle handle = ScriptPlayable<T>.CreateHandle(graph, template, inputCount);
			return new ScriptPlayable<T>(handle);
		}

		// Token: 0x060026DE RID: 9950 RVA: 0x00040D5C File Offset: 0x0003EF5C
		private static PlayableHandle CreateHandle(PlayableGraph graph, T template, int inputCount)
		{
			bool flag = template == null;
			object obj;
			if (flag)
			{
				obj = ScriptPlayable<T>.CreateScriptInstance();
			}
			else
			{
				obj = ScriptPlayable<T>.CloneScriptInstance(template);
			}
			bool flag2 = obj == null;
			PlayableHandle result;
			if (flag2)
			{
				string str = "Could not create a ScriptPlayable of Type ";
				Type typeFromHandle = typeof(T);
				Debug.LogError(str + ((typeFromHandle != null) ? typeFromHandle.ToString() : null));
				result = PlayableHandle.Null;
			}
			else
			{
				PlayableHandle playableHandle = graph.CreatePlayableHandle();
				bool flag3 = !playableHandle.IsValid();
				if (flag3)
				{
					result = PlayableHandle.Null;
				}
				else
				{
					playableHandle.SetInputCount(inputCount);
					playableHandle.SetScriptInstance(obj);
					result = playableHandle;
				}
			}
			return result;
		}

		// Token: 0x060026DF RID: 9951 RVA: 0x00040E04 File Offset: 0x0003F004
		private static object CreateScriptInstance()
		{
			bool flag = typeof(ScriptableObject).IsAssignableFrom(typeof(T));
			IPlayableBehaviour result;
			if (flag)
			{
				result = (ScriptableObject.CreateInstance(typeof(T)) as T);
			}
			else
			{
				result = Activator.CreateInstance<T>();
			}
			return result;
		}

		// Token: 0x060026E0 RID: 9952 RVA: 0x00040E64 File Offset: 0x0003F064
		private static object CloneScriptInstance(IPlayableBehaviour source)
		{
			Object @object = source as Object;
			bool flag = @object != null;
			object result;
			if (flag)
			{
				result = ScriptPlayable<T>.CloneScriptInstanceFromEngineObject(@object);
			}
			else
			{
				ICloneable cloneable = source as ICloneable;
				bool flag2 = cloneable != null;
				if (flag2)
				{
					result = ScriptPlayable<T>.CloneScriptInstanceFromIClonable(cloneable);
				}
				else
				{
					result = null;
				}
			}
			return result;
		}

		// Token: 0x060026E1 RID: 9953 RVA: 0x00040EAC File Offset: 0x0003F0AC
		private static object CloneScriptInstanceFromEngineObject(Object source)
		{
			Object @object = Object.Instantiate(source);
			bool flag = @object != null;
			if (flag)
			{
				@object.hideFlags |= HideFlags.DontSave;
			}
			return @object;
		}

		// Token: 0x060026E2 RID: 9954 RVA: 0x00040EE4 File Offset: 0x0003F0E4
		private static object CloneScriptInstanceFromIClonable(ICloneable source)
		{
			return source.Clone();
		}

		// Token: 0x060026E3 RID: 9955 RVA: 0x00040EFC File Offset: 0x0003F0FC
		internal ScriptPlayable(PlayableHandle handle)
		{
			bool flag = handle.IsValid();
			if (flag)
			{
				bool flag2 = !typeof(T).IsAssignableFrom(handle.GetPlayableType());
				if (flag2)
				{
					throw new InvalidCastException(string.Format("Incompatible handle: Trying to assign a playable data of type `{0}` that is not compatible with the PlayableBehaviour of type `{1}`.", handle.GetPlayableType(), typeof(T)));
				}
			}
			this.m_Handle = handle;
		}

		// Token: 0x060026E4 RID: 9956 RVA: 0x00040F5C File Offset: 0x0003F15C
		public PlayableHandle GetHandle()
		{
			return this.m_Handle;
		}

		// Token: 0x060026E5 RID: 9957 RVA: 0x00040F74 File Offset: 0x0003F174
		public T GetBehaviour()
		{
			return this.m_Handle.GetObject<T>();
		}

		// Token: 0x060026E6 RID: 9958 RVA: 0x00040F94 File Offset: 0x0003F194
		public static implicit operator Playable(ScriptPlayable<T> playable)
		{
			return new Playable(playable.GetHandle());
		}

		// Token: 0x060026E7 RID: 9959 RVA: 0x00040FB4 File Offset: 0x0003F1B4
		public static explicit operator ScriptPlayable<T>(Playable playable)
		{
			return new ScriptPlayable<T>(playable.GetHandle());
		}

		// Token: 0x060026E8 RID: 9960 RVA: 0x00040FD4 File Offset: 0x0003F1D4
		public bool Equals(ScriptPlayable<T> other)
		{
			return this.GetHandle() == other.GetHandle();
		}

		// Token: 0x060026E9 RID: 9961 RVA: 0x00040FF8 File Offset: 0x0003F1F8
		// Note: this type is marked as 'beforefieldinit'.
		static ScriptPlayable()
		{
		}

		// Token: 0x04000E33 RID: 3635
		private PlayableHandle m_Handle;

		// Token: 0x04000E34 RID: 3636
		private static readonly ScriptPlayable<T> m_NullPlayable = new ScriptPlayable<T>(PlayableHandle.Null);
	}
}
