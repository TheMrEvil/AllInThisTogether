using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace UnityEngine.XR
{
	// Token: 0x02000010 RID: 16
	public struct InputFeatureUsage<T> : IEquatable<InputFeatureUsage<T>>
	{
		// Token: 0x17000014 RID: 20
		// (get) Token: 0x06000049 RID: 73 RVA: 0x000029BD File Offset: 0x00000BBD
		// (set) Token: 0x0600004A RID: 74 RVA: 0x000029C5 File Offset: 0x00000BC5
		public string name
		{
			[CompilerGenerated]
			readonly get
			{
				return this.<name>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<name>k__BackingField = value;
			}
		}

		// Token: 0x0600004B RID: 75 RVA: 0x000029CE File Offset: 0x00000BCE
		public InputFeatureUsage(string usageName)
		{
			this.name = usageName;
		}

		// Token: 0x0600004C RID: 76 RVA: 0x000029DC File Offset: 0x00000BDC
		public override bool Equals(object obj)
		{
			bool flag = !(obj is InputFeatureUsage<T>);
			return !flag && this.Equals((InputFeatureUsage<T>)obj);
		}

		// Token: 0x0600004D RID: 77 RVA: 0x00002A10 File Offset: 0x00000C10
		public bool Equals(InputFeatureUsage<T> other)
		{
			return this.name == other.name;
		}

		// Token: 0x0600004E RID: 78 RVA: 0x00002A34 File Offset: 0x00000C34
		public override int GetHashCode()
		{
			return this.name.GetHashCode();
		}

		// Token: 0x0600004F RID: 79 RVA: 0x00002A54 File Offset: 0x00000C54
		public static bool operator ==(InputFeatureUsage<T> a, InputFeatureUsage<T> b)
		{
			return a.Equals(b);
		}

		// Token: 0x06000050 RID: 80 RVA: 0x00002A70 File Offset: 0x00000C70
		public static bool operator !=(InputFeatureUsage<T> a, InputFeatureUsage<T> b)
		{
			return !(a == b);
		}

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x06000051 RID: 81 RVA: 0x00002A8C File Offset: 0x00000C8C
		private Type usageType
		{
			get
			{
				return typeof(T);
			}
		}

		// Token: 0x06000052 RID: 82 RVA: 0x00002AA8 File Offset: 0x00000CA8
		public static explicit operator InputFeatureUsage(InputFeatureUsage<T> self)
		{
			InputFeatureType inputFeatureType = (InputFeatureType)4294967295U;
			Type usageType = self.usageType;
			bool flag = usageType == typeof(bool);
			if (flag)
			{
				inputFeatureType = InputFeatureType.Binary;
			}
			else
			{
				bool flag2 = usageType == typeof(uint);
				if (flag2)
				{
					inputFeatureType = InputFeatureType.DiscreteStates;
				}
				else
				{
					bool flag3 = usageType == typeof(float);
					if (flag3)
					{
						inputFeatureType = InputFeatureType.Axis1D;
					}
					else
					{
						bool flag4 = usageType == typeof(Vector2);
						if (flag4)
						{
							inputFeatureType = InputFeatureType.Axis2D;
						}
						else
						{
							bool flag5 = usageType == typeof(Vector3);
							if (flag5)
							{
								inputFeatureType = InputFeatureType.Axis3D;
							}
							else
							{
								bool flag6 = usageType == typeof(Quaternion);
								if (flag6)
								{
									inputFeatureType = InputFeatureType.Rotation;
								}
								else
								{
									bool flag7 = usageType == typeof(Hand);
									if (flag7)
									{
										inputFeatureType = InputFeatureType.Hand;
									}
									else
									{
										bool flag8 = usageType == typeof(Bone);
										if (flag8)
										{
											inputFeatureType = InputFeatureType.Bone;
										}
										else
										{
											bool flag9 = usageType == typeof(Eyes);
											if (flag9)
											{
												inputFeatureType = InputFeatureType.Eyes;
											}
											else
											{
												bool flag10 = usageType == typeof(byte[]);
												if (flag10)
												{
													inputFeatureType = InputFeatureType.Custom;
												}
												else
												{
													bool isEnum = usageType.IsEnum;
													if (isEnum)
													{
														inputFeatureType = InputFeatureType.DiscreteStates;
													}
												}
											}
										}
									}
								}
							}
						}
					}
				}
			}
			bool flag11 = inputFeatureType != (InputFeatureType)4294967295U;
			if (flag11)
			{
				return new InputFeatureUsage(self.name, inputFeatureType);
			}
			throw new InvalidCastException("No valid InputFeatureType for " + self.name + ".");
		}

		// Token: 0x0400005D RID: 93
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private string <name>k__BackingField;
	}
}
