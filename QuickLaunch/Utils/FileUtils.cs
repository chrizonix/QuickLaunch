using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;

namespace QuickLaunch.Utils
{
    public enum NotFoundMode
    {
        EmptyString,
        FileName
    }

    public class FileUtils
    {
        /// <summary>
        /// Searches Files including Files in Sub-Directories
        /// Returning Relative Paths to Parent Path.
        /// 
        /// - file1.txt
        /// - file2.txt
        /// - subfolder1/file3.txt
        /// - subfolder2/file4.txt
        /// </summary>
        /// <param name="dirPath"></param>
        /// <param name="pattern"></param>
        public static IEnumerable<string> ListFiles(String dirPath, String pattern)
        {
            // Check Directory Exists
            if (!Directory.Exists(dirPath))
            {
                // Return Empty List
                return new List<String>();
            }

            // Enumerate Files also in Sub-Directories and Strip Parent Folder
            return Directory.EnumerateFiles(dirPath, pattern, SearchOption.AllDirectories)
                            .Select(f => f.Substring(CalculateDirectoryLength(dirPath)));
        }

        /// <summary>
        /// Removes the Base Folder Path from given Directory Path
        /// </summary>
        /// <param name="dirPath"></param>
        /// <param name="baseFolderPath"></param>
        /// <returns></returns>
        public static String RemoveBaseFolderPath(String dirPath, String baseFolderPath)
        {
            // Strip / Remove the Base Folder Path of given Directory Path
            return dirPath.Substring(CalculateDirectoryLength(baseFolderPath));
        }

        /// <summary>
        /// Calculates the Length of the Folder Path including Directory Separator
        /// </summary>
        /// <param name="dirPath"></param>
        /// <returns></returns>
        public static int CalculateDirectoryLength(String baseFolderPath)
        {
            // Length of Base Folder Path
            int directoryLength = baseFolderPath.Length;

            // Because we don't want it to be prefixed by a slash
            // If dirPath like "C:\MyFolder", rather than "C:\MyFolder\"
            if (!baseFolderPath.EndsWith("" + Path.DirectorySeparatorChar))
            {
                // Add Directory Separator
                directoryLength++;
            }

            // Return Calculated Length
            return directoryLength;
        }

        /// <summary>
        /// Returns Root Folder of Relative Path.
        /// 
        /// e.g. my_folder/sub_folder/path, will return "my_folder"
        /// 
        /// </summary>
        /// <param name="dirPath"></param>
        /// <param name="mode">Based on the NotFoundMode it will return an Empty String or the File Name</param>
        /// <returns></returns>
        public static String GetPathRoot(String dirPath, NotFoundMode mode)
        {
            // Sanity Check
            if (dirPath.Contains(Path.DirectorySeparatorChar))
            {
                // Return Root Folder of Relative Path
                return dirPath.Split(Path.DirectorySeparatorChar).First();
            }

            // Not Found Mode
            switch (mode)
            {
                case NotFoundMode.EmptyString:
                    return String.Empty;
                case NotFoundMode.FileName:
                    return dirPath;
            }

            // Default Value
            return null;
        }

        /// <summary>
        /// Get Directory Path without Root Folder of Relative Path.
        /// 
        /// e.g. my_folder/sub_folder/path, will return "sub_folder/path"
        /// 
        /// </summary>
        /// <param name="dirPath"></param>
        /// <returns></returns>
        public static String RemovePathRoot(String dirPath)
        {
            // Sanity Check
            if (dirPath.Contains(Path.DirectorySeparatorChar))
            {
                // Get Directory Separator Char
                char separator = Path.DirectorySeparatorChar;

                // Get Directory Path without Root Folder of Relative Path
                return String.Join(separator.ToString(), dirPath.Split(separator).Skip(1));
            }

            // Default Value
            return dirPath;
        }

        /// <summary>
        /// Builds an Ordered Unique Dictionary including only the Folders and Files of the Top Level
        /// </summary>
        /// <param name="files"></param>
        /// <returns></returns>
        public static OrderedDictionary BuildUniqueDictionaryTopLevel(List<String> files)
        {
            // Unique List of Files in Collection
            OrderedDictionary uniqueFiles = new OrderedDictionary();

            // Build Unique List of Files using Root Folder
            foreach (String currentFile in files)
            {
                // Extract Root Folder of Relative Path
                String root = FileUtils.GetPathRoot(currentFile, NotFoundMode.FileName);

                // Check Existing Shortcuts
                if (!uniqueFiles.Contains(root))
                {
                    // Add Unique Shortcut to Dictionary
                    uniqueFiles.Add(root, root);
                }
            }

            // Return Unique List of Files
            return uniqueFiles;
        }
    }
}
