/*
 * Yes, this is extracted from the original Essentials plugin.
 * Credit goes to the original creators of these commands.
 * 
 */

using System;
using System.Reflection;
using Terraria;
using TerrariaApi.Server;
using TShockAPI;

namespace Navigation
{
	[ApiVersion(2,1)]
    public class NavMain : TerrariaPlugin
    {
		#region PluginInfo
		public override string Author => "Zaicon";
		public override string Description => "Basic navigational commands for tShock.";
		public override string Name => "Navigation";
		public override Version Version => Assembly.GetExecutingAssembly().GetName().Version;
		#endregion

		#region Initialize/Dispose
		public override void Initialize()
		{
			ServerApi.Hooks.GameInitialize.Register(this, OnInitialize);
		}

		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				ServerApi.Hooks.GameInitialize.Deregister(this, OnInitialize);
			}
			base.Dispose(disposing);
		}
		#endregion

		public NavMain(Main game) : base (game)
		{

		}

		#region Hooks
		private void OnInitialize(EventArgs args)
		{
			Commands.ChatCommands.Add(new Command("navigation.top", Top, "top"));
			Commands.ChatCommands.Add(new Command("navigation.up", Up, "up"));
			Commands.ChatCommands.Add(new Command("navigation.down", Down, "down"));
			Commands.ChatCommands.Add(new Command("navigation.left", Left, "left"));
			Commands.ChatCommands.Add(new Command("navigation.right", Right, "right"));
		}
		#endregion

		#region Commands
		private void Top(CommandArgs args)
		{
			int Y = Utils.GetTop(args.Player.TileX);
			if (Y == -1)
			{
				args.Player.SendErrorMessage("You are already on the top.");
				return;
			}
			args.Player.Teleport(args.Player.TileX * 16F, Y * 16F);
			args.Player.SendSuccessMessage("Teleported you to the top.");
		}

		private void Up(CommandArgs args)
		{
			int levels = 1;
			if (args.Parameters.Count > 0 && !int.TryParse(args.Parameters[0], out levels))
			{
				args.Player.SendErrorMessage($"Usage: {TShock.Config.CommandSpecifier}up [No. levels]");
				return;
			}

			int Y = Utils.GetUp(args.Player.TileX, args.Player.TileY);
			if (Y == -1)
			{
				args.Player.SendErrorMessage("You are already on the top.");
				return;
			}
			bool limit = false;
			for (int i = 1; i < levels; i++)
			{
				int newY = Utils.GetUp(args.Player.TileX, Y);
				if (newY == -1)
				{
					levels = i;
					limit = true;
					break;
				}
				Y = newY;
			}

			args.Player.Teleport(args.Player.TileX * 16F, Y * 16F);
			args.Player.SendSuccessMessage($"Teleported you up {levels} level(s). {(limit ? "You can't go up any further." : string.Empty)}");
		}

		private void Down(CommandArgs args)
		{
			int levels = 1;
			if (args.Parameters.Count > 0 && !int.TryParse(args.Parameters[0], out levels))
			{
				args.Player.SendErrorMessage($"Usage: {TShock.Config.CommandSpecifier}down [No. levels]");
				return;
			}

			int Y = Utils.GetDown(args.Player.TileX, args.Player.TileY);
			if (Y == -1)
			{
				args.Player.SendErrorMessage("You are already on the bottom.");
				return;
			}
			bool limit = false;
			for (int i = 1; i < levels; i++)
			{
				int newY = Utils.GetDown(args.Player.TileX, Y);
				if (newY == -1)
				{
					levels = i;
					limit = true;
					break;
				}
				Y = newY;
			}

			args.Player.Teleport(args.Player.TileX * 16F, Y * 16F);
			args.Player.SendSuccessMessage($"Teleported you down {levels} level(s). {(limit ? "You can't go down any further." : string.Empty)}");
		}

		private void Left(CommandArgs args)
		{
			int levels = 1;
			if (args.Parameters.Count > 0 && !int.TryParse(args.Parameters[0], out levels))
			{
				args.Player.SendErrorMessage("Usage: /left [No. times]");
				return;
			}

			int X = Utils.GetLeft(args.Player.TileX, args.Player.TileY);
			if (X == -1)
			{
				args.Player.SendErrorMessage("You cannot go any further left.");
				return;
			}
			bool limit = false;
			for (int i = 1; i < levels; i++)
			{
				int newX = Utils.GetLeft(X, args.Player.TileY);
				if (newX == -1)
				{
					levels = i;
					limit = true;
					break;
				}
				X = newX;
			}

			args.Player.Teleport(X * 16F, args.Player.TileY * 16F);
			args.Player.SendSuccessMessage($"Teleported you to the left {levels} time(s). {(limit ? "You can't go any further." : string.Empty)}");
		}

		private void Right(CommandArgs args)
		{
			int levels = 1;
			if (args.Parameters.Count > 0 && !int.TryParse(args.Parameters[0], out levels))
			{
				args.Player.SendErrorMessage($"Usage: {TShock.Config.CommandSpecifier}right [No. times]");
				return;
			}

			int X = Utils.GetRight(args.Player.TileX, args.Player.TileY);
			if (X == -1)
			{
				args.Player.SendErrorMessage("You cannot go any further right.");
				return;
			}
			bool limit = false;
			for (int i = 1; i < levels; i++)
			{
				int newX = Utils.GetRight(X, args.Player.TileY);
				if (newX == -1)
				{
					levels = i;
					limit = true;
					break;
				}
				X = newX;
			}

			args.Player.Teleport(X * 16F, args.Player.TileY * 16F);
			args.Player.SendSuccessMessage($"Teleported you to the right {levels} time(s). {(limit ? "You can't go any further." : string.Empty)}");
		}
		#endregion
	}
}
