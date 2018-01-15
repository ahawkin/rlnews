using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace rlnews.importer
{
    public class Distance
    {
        /// <summary>
        /// Compute Levenshtein Distance
        /// Sam Allen. (2016). Levenshtein [Online]. Available From: <http://www.dotnetperls.com/levenshtein>. [Accessed 20th April 2016]
        /// </summary>
        /// <param name="s"></param>
        /// <param name="t"></param>
        /// <returns></returns>
        public static int Compute(string s, string t)
        {
            int n = s.Length;
            int m = t.Length;
            int[,] d = new int[n + 1, m + 1];

            // Step 1
            if (n == 0)
            {
                return m;
            }

            if (m == 0)
            {
                return n;
            }

            // Step 2
            for (int i = 0; i <= n; d[i, 0] = i++)
            {
            }

            for (int j = 0; j <= m; d[0, j] = j++)
            {
            }

            // Step 3
            for (int i = 1; i <= n; i++)
            {
                //Step 4
                for (int j = 1; j <= m; j++)
                {
                    // Step 5
                    int cost = (t[j - 1] == s[i - 1]) ? 0 : 1;

                    // Step 6
                    d[i, j] = Math.Min(
                        Math.Min(d[i - 1, j] + 1, d[i, j - 1] + 1),
                        d[i - 1, j - 1] + cost);
                }
            }
            // Step 7
            return d[n, m];
        }

        public bool HasUpperCase(string str)
        {
            return !string.IsNullOrEmpty(str) && str.Any(c => char.IsUpper(c));
        }

        public int GetMatchScore(string titleRss, string titleDb)
        {
            StopWords stopWords = new StopWords();

            int nounScore = 0;
            int matchedWords = 0;
            int overallScore = 0;

            titleRss = StopWords.RemoveStopwords(titleRss);
            titleDb = StopWords.RemoveStopwords(titleDb);

            string[] titleRssSplit = titleRss.Split(' ');
            string[] titleDbSplit = titleDb.Split(' ');


            for (int i = 0; i < titleDbSplit.Count(); i++)
            {
                foreach (string str in titleRssSplit.Where(str => Distance.Compute(str, titleDbSplit[i]) <= 1))
                {

                    matchedWords++;

                    if (HasUpperCase(str) && HasUpperCase(titleDbSplit[i]))
                    {
                        nounScore = nounScore + 10;
                    }
                }
            }

            overallScore = matchedWords + nounScore;

            return overallScore;
        }

        public int CheckRelated(string titleRss)
        {

            Distance distance = new Distance();

            var dbContext = new rlnews.DAL.RlnewsDb();

            DateTime nowMinus24 = DateTime.Now;
            DateTime now = DateTime.Now;
            nowMinus24 = nowMinus24.AddHours(-24);

            var dbObj = dbContext.NewsItems.Where(x => x.PubDateTime > nowMinus24 && x.PubDateTime <= now && x.ClusterType == "Parent");


            foreach (var item in dbObj)
            {
                if (distance.GetMatchScore(titleRss, item.Title) >= 5)
                {             
                    return item.NewsId;
                }
            }
            return 0;
        }
    }
}
