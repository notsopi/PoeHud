using System.Collections.Generic;
using System.Linq;

namespace ExileHUD.EntityComponents
{
	public class Sockets : Component
	{
		public int LargestLinkSize
		{
			get
			{
				if (this.address == 0)
				{
					return 0;
				}
				int num = this.m.ReadInt(this.address + 60);
				int num2 = this.m.ReadInt(this.address + 64);
				int num3 = num2 - num;
				if (num3 <= 0 || num3 > 6)
				{
					return 0;
				}
				int num4 = 0;
				for (int i = 0; i < num3; i++)
				{
					int num5 = (int)this.m.ReadByte(num + i);
					if (num5 > num4)
					{
						num4 = num5;
					}
				}
				return num4;
			}
		}
		public List<int[]> Links
		{
			get
			{
				List<int[]> list = new List<int[]>();
				if (this.address == 0)
				{
					return list;
				}
				int num = this.m.ReadInt(this.address + 60);
				int num2 = this.m.ReadInt(this.address + 64);
				int num3 = num2 - num;
				if (num3 <= 0 || num3 > 6)
				{
					return list;
				}
				int num4 = 0;
				List<int> socketList = this.SocketList;
				for (int i = 0; i < num3; i++)
				{
					int num5 = (int)this.m.ReadByte(num + i);
					int[] array = new int[num5];
					for (int j = 0; j < num5; j++)
					{
						array[j] = socketList[j + num4];
					}
					list.Add(array);
					num4 += num5;
				}
				return list;
			}
		}
		public List<int> SocketList
		{
			get
			{
				List<int> list = new List<int>();
				if (this.address == 0)
				{
					return list;
				}
				int num = this.address + 12;
				for (int i = 0; i < 6; i++)
				{
					int num2 = this.m.ReadInt(num);
					if (num2 >= 1 && num2 <= 3)
					{
						list.Add(this.m.ReadInt(num));
					}
					num += 4;
				}
				return list;
			}
		}
		public int NumberOfSockets
		{
			get
			{
				return this.SocketList.Count;
			}
		}
		public bool IsRGB
		{
			get
			{
				if (this.address == 0)
				{
					return false;
				}
				foreach (int[] current in this.Links)
				{
					if (current.Length >= 3 && current.Contains(1) && current.Contains(2) && current.Contains(3))
					{
						return true;
					}
				}
				return false;
			}
		}
	}
}
