using System.Collections.Generic;
using ExileHUD.ExileBot;
using ExileHUD.Game;

namespace ExileHUD.EntityComponents
{
	public class Mods : Component
	{
		public ItemRarity ItemRarity
		{
			get
			{
				if (this.address != 0)
				{
					return (ItemRarity)this.m.ReadInt(this.address + 48);
				}
				return ItemRarity.White;
			}
		}
		public int ItemLevel
		{
			get
			{
				if (this.address != 0)
				{
					return this.m.ReadInt(this.address + 212);
				}
				return 1;
			}
		}
		public string UniqueName
		{
			get
			{
				if (this.address != 0)
				{
					return this.m.ReadStringU(this.m.ReadInt(this.address + 12, new int[]
					{
						4,
						4
					}), 256, true);
				}
				return "";
			}
		}
		public ItemStats ItemStats
		{
			get
			{
				return new ItemStats(base.Owner);
			}
		}
		public List<Poe_ItemMod> ItemMods
		{
			get
			{
				List<Poe_ItemMod> list = new List<Poe_ItemMod>();
				if (this.address == 0)
				{
					return list;
				}
				int i = this.m.ReadInt(this.address + 68);
				int num = this.m.ReadInt(this.address + 72);
				int num2 = (num - i) / 24;
				if (num2 > 12)
				{
					return list;
				}
				while (i < num)
				{
					list.Add(base.GetObject<Poe_ItemMod>(i));
					i += 24;
				}
				return list;
			}
		}
		public List<Poe_ItemMod> ImplicitMods
		{
			get
			{
				List<Poe_ItemMod> list = new List<Poe_ItemMod>();
				if (this.address == 0)
				{
					return list;
				}
				int i = this.m.ReadInt(this.address + 52);
				int num = this.m.ReadInt(this.address + 56);
				int num2 = (num - i) / 24;
				if (num2 > 100 || num2 <= 0)
				{
					return list;
				}
				while (i < num)
				{
					list.Add(base.GetObject<Poe_ItemMod>(i));
					i += 24;
				}
				return list;
			}
		}
	}
}
