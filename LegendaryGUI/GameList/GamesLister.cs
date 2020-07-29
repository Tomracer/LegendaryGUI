﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LegendaryGUI.GameList
{
    public abstract class GamesLister
    {
        public List<GameInfo> Games { get; protected set; }
        protected ListViewSafeWriter lv;
        protected string args = "";

        public GamesLister(ListViewSafeWriter lv)
        {
            this.lv = lv;
        }

        public void RefreshListing(bool forceRefresh)
        {
            if (Games == null || forceRefresh)
            {
                LaunchProcess proc = new LaunchProcess(args);
                proc.ReturnFunc = Parser;
                proc.Run();
                return;
            }

            ListWriter();
        }

        private void Parser(LaunchProcess proc)
        {
            Games = new List<GameInfo>();
            foreach (string line in proc.Output)
            {
                if (line.StartsWith(" *"))
                {
                    Games.Add(new GameInfo(line));
                }
            }

            RefreshListing(false);

            FormConsole.WriteLine("Parsing done!");
        }

        public abstract void ListWriter();
    }
}
