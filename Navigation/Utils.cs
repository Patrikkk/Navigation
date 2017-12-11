using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;

namespace Navigation
{
	public static class Utils
	{
		public static int GetTop(int TileX)
		{
			for (int y = 0; y < Main.maxTilesY; y++)
			{
				if (WorldGen.SolidTile(TileX, y))
					return y - 3;
			}
			return -1;
		}

		public static int GetUp(int TileX, int TileY)
		{
			for (int y = TileY - 1; y > 0; y--)
			{
				if (WorldGen.SolidTile(TileX, y + 1) && !WorldGen.SolidTile(TileX, y) && !WorldGen.SolidTile(TileX, y - 1) && !WorldGen.SolidTile(TileX, y - 2))
					return y - 2;
			}
			return -1;
		}

		public static int GetDown(int TileX, int TileY)
		{
			for (int y = TileY + 3; y < Main.maxTilesY; y++)
			{
				if (WorldGen.SolidTile(TileX, y + 1) && !WorldGen.SolidTile(TileX, y) && !WorldGen.SolidTile(TileX, y - 1) && !WorldGen.SolidTile(TileX, y - 2))
					return y - 2;
			}
			return -1;
		}

		public static int GetLeft(int TileX, int TileY)
		{
			bool FoundSolid = false;
			for (int x = TileX; x > 0; x--)
			{
				if (!FoundSolid)
				{
					if (!WorldGen.SolidTile(x, TileY))
						continue;
					else
						FoundSolid = true;
				}
				else if (!WorldGen.SolidTile(x - 1, TileY) &&
					!WorldGen.SolidTile(x, TileY + 1) && !WorldGen.SolidTile(x - 1, TileY + 1) &&
					!WorldGen.SolidTile(x, TileY + 2) && !WorldGen.SolidTile(x - 1, TileY + 2))
					return x - 1;
				else
					FoundSolid = true;
			}
			return -1;
		}

		public static int GetRight(int TileX, int TileY)
		{
			bool FoundSolid = false;
			for (int x = TileX + 1; x < Main.maxTilesX; x++)
			{
				if (!FoundSolid)
				{
					if (!WorldGen.SolidTile(x, TileY))
						continue;
					else
						FoundSolid = true;
				}
				else if (!WorldGen.SolidTile(x + 1, TileY) &&
					!WorldGen.SolidTile(x, TileY + 1) && !WorldGen.SolidTile(x + 1, TileY + 1) &&
					!WorldGen.SolidTile(x, TileY + 2) && !WorldGen.SolidTile(x + 1, TileY + 2))
					return x + 1;
			}
			return -1;
		}
	}
}
