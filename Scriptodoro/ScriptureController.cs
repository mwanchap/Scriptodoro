using System;
using System.IO;
using System.Linq;
using System.Net;

namespace Scriptodoro
{
    static class ScriptureController
    {
        public static string GetScripture(string verseRef)
        {
            using (WebClient client = new WebClient())
            {
                var url = $"http://www.esvapi.org/v2/rest/verse?key=IP&output-format=plain-text&include-verse-numbers=false&include-passage-references=false&include-footnotes=false&include-short-copyright=false&include-passage-horizontal-lines=false&line-length=0&passage=+{verseRef}";
                var resp = client.DownloadString(url);
                return resp;
            }

        }
    }
}
