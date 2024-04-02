using UnityEditor;
using UnityEditor.Callbacks;
using UnityEditor.iOS.Xcode;
using System.IO;

public class IosBuildPostprocessor
{

    [PostProcessBuild(1)]
    public static void EditPlist(BuildTarget target, string path)
    {
        if (target != BuildTarget.iOS)
            return;


        string plistPath = path + "/Info.plist";
        PlistDocument plist = new PlistDocument();
        plist.ReadFromFile(plistPath);

        PlistElementDict rootDict = plist.root;

        // Add ITSAppUsesNonExemptEncryption to Info.plist
        rootDict.SetString("ITSAppUsesNonExemptEncryption", "false");
        rootDict.SetString("NSCameraUsageDescription", "true");


        File.WriteAllText(plistPath, plist.WriteToString());
    }
}