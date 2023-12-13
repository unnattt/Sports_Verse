using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

 
public static class AssetBundleMetaExtensions
{
    public static string GetManifestHash(this string manifestRawText)
    {
        return GetManifestValue(manifestRawText, "Hash:");
    }
    public static string GetManifestCRC(this string manifestRawText)
    {
        return GetManifestValue(manifestRawText, "CRC:");
    }
    public static string GetManifestValue(this string manifestRawText, string lookFor = "Hash:")
    {
        // We don't really need to chop the string, but if we did, that's how I'd do it.
        //string choppedManifest = manifestRawText.Substring(manifestRawText.IndexOf("AssetFileHash:"));
        //choppedManifest = choppedManifest.Remove(choppedManifest.IndexOf("TypeTreeHash:"));

        var hashes = manifestRawText
            // separate the text into lines, in an list/array of strings
            .Split(Environment.NewLine.ToCharArray(), StringSplitOptions.RemoveEmptyEntries)
            // select only the lines that starts with 'lookFor', ignoring white spaces
            .Where(x => x.Trim().StartsWith(lookFor))
            // convert result to array
            .ToArray();
        string myParsedHash = hashes
            // on the manifest file, the hash we need is always the first (AssetFileHash), so here we just take the first one found.
            .FirstOrDefault()
            // ignore whitespaces
            .Trim();
        myParsedHash = string.IsNullOrEmpty(myParsedHash) ? "" :
            // get the right part of the string after what 'lookfor'. +1 because the substring index starts at zero
            myParsedHash.Substring(lookFor.Length + 1);

        return myParsedHash;
    }
}
