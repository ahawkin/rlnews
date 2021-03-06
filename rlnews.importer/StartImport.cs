﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using rlnews.importer.RssSources;

namespace rlnews.importer
{
    public class StartImport
    {

        private int _totalImported = 0;

        private void ImportAllSources()
        {
            Console.WriteLine("RLNews import starting...");
            Console.WriteLine(" ");
            ImportBbcNews();
            ImportTheGuardian();
            ImportDailymail();
            Console.WriteLine("==========================================");
            Console.WriteLine("Import complete - Total items imported: " + _totalImported);
            Console.WriteLine("==========================================");
        }

        /// <summary>
        /// Executes the import of RSS feeds from BBC News
        /// </summary>
        private void ImportBbcNews()
        {
            BbcNews bbcNews = new BbcNews();
            Console.WriteLine("Importing BBC News RSS Feeds...");
            bbcNews.StartImport();
            Console.WriteLine(bbcNews.ReturnImportMessage());
            Console.WriteLine(" ");
            _totalImported = _totalImported + bbcNews.ReturnImportNumber();
        }

        /// <summary>
        /// Executes the import of RSS feeds from BBC News
        /// </summary>
        private void ImportTheGuardian()
        {
            TheGuardian guardian = new TheGuardian();
            Console.WriteLine("Importing The Guardian RSS Feeds...");
            guardian.StartImport();
            Console.WriteLine(guardian.ReturnImportMessage());
            Console.WriteLine(" ");
            _totalImported = _totalImported + guardian.ReturnImportNumber();
        }

        /// <summary>
        /// Executes the import of RSS feeds from BBC News
        /// </summary>
        private void ImportDailymail()
        {
            RssSource dailymail = new RssSource();
            Console.WriteLine("Importing Dailymail RSS Feeds...");
            dailymail.StartImport();
            Console.WriteLine(dailymail.ReturnImportMessage());
            Console.WriteLine(" ");
            _totalImported = _totalImported + dailymail.ReturnImportNumber();
        }

        /// <summary>
        /// Test method for distance
        /// </summary>
        public int TestDistance(string inputString1, string inputString2)
        {
            Distance distance = new Distance();

            string test1 = inputString1;
            string test2 = inputString2;

            Console.WriteLine("Match score = " + distance.GetMatchScore(test1, test2));

            return distance.GetMatchScore(test1, test2);
        }

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            StartImport start = new StartImport();

            // Executes the import method       
            start.ImportAllSources();
            Console.ReadKey();
        }
    }
}
