using System;
using System.IO;
using System.Collections.Generic;
using System.Windows.Forms;

using AnySerializer;
using Shortcut = QuickLaunch.Data.Shortcut;

namespace QuickLaunch.Utils
{
    internal class SerializationUtils
    {
        // Binary File for Serialization / Deserialization
        public const String CacheFile = "shortcuts.bin";

        public static bool CacheFileExists()
        {
            // Check Cache File Exists
            return File.Exists(CacheFile);
        }

        public static List<Shortcut> DeserializeShortcuts()
        {
            try
            {
                // Check Serialized Shortcuts in Cache
                if (File.Exists(CacheFile))
                {
                    // Open File Stream for Reading the Cache File for Deserialization
                    using (FileStream stream = new FileStream(CacheFile, FileMode.Open, FileAccess.Read))
                    {
                        // Deserialize Shortcuts using File Stream of Cache File
                        return Serializer.Deserialize<List<Shortcut>>(stream, SerializerOptions.None);
                    }
                }
            }
            catch (Exception ex)
            {
                // Error During Deserialization of Shortcuts -> Show Error Message to User
                MessageBox.Show(ex.Message, "Error During Loading of Shortcuts!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            // Return Empty List
            return new List<Shortcut>();
        }

        public static bool SerializeShortcuts(List<Shortcut> shortcuts)
        {
            try
            {
                // Serialize Shortcuts using Any Serializer Library
                byte[] bytes = Serializer.Serialize(shortcuts);

                // Write Serialized Bytes to Cache File
                File.WriteAllBytes(CacheFile, bytes);

                // Success
                return true;
            }
            catch (Exception ex)
            {
                // Error During Serialization of Shortcuts -> Show Error Message to User
                MessageBox.Show(ex.Message, "Error During Saving of Shortcuts!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            // Failed
            return false;
        }
    }
}
