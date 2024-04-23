/*
 * https://stackoverflow.com/questions/20735935/using-shfileinfo-to-get-file-icons
 */

using System;
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Interop;
using System.Windows.Media.Imaging;

using static QuickLaunch.Utils.WindowsShellAPI;

namespace QuickLaunch.Utils
{
    public enum IconSize
    {
        Small,
        Large
    }

    public class IconHelper
    {
        // Constants that we need in the function call
        private const int SHGFI_ICON = 0x100;
        private const int SHGFI_SMALLICON = 0x1;
        private const int SHGFI_LARGEICON = 0x0;

        private const int SHIL_JUMBO = 0x4;
        private const int SHIL_EXTRALARGE = 0x2;

        // This structure will contain information about the file
        public struct SHFILEINFO
        {
            // Handle to the icon representing the file
            public IntPtr hIcon;

            // Index of the icon within the image list
            public int iIcon;

            // Various attributes of the file
            public uint dwAttributes;

            // Path to the file
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 256)]
            public string szDisplayName;

            // File type
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 80)]
            public string szTypeName;
        };

        public class IconStruct
        {
            public byte[] Bytes { get; private set; }

            public Icon Icon;
            public String Base64;

            /// <summary>
            /// Default Constructor needed for Deserialization
            /// </summary>
            public IconStruct()
            {
                // Set Default Values
                this.Bytes = null;
                this.Icon = null;
            }

            public IconStruct(byte[] bytes, Icon icon)
            {
                // Set Byte Array and Icon
                this.Bytes = bytes;
                this.Icon = icon;

                // Store Bytes for Serialization
                StoreBytesToBase64();
            }

            public void StoreBytesToBase64()
            {
                // Store Bytes as Base64 for Serialization
                Base64 = Convert.ToBase64String(Bytes);
            }

            public void RestoreBytesFromBase64()
            {
                // Restore Bytes from Base64 after Deserialization
                Bytes = Convert.FromBase64String(Base64);
            }

            public Image GetImage()
            {
                // Convert Bytes of Icon to Image
                return BytesToImage(Bytes);
            }
        }

        [System.Runtime.InteropServices.DllImport("Kernel32.dll")]
        public static extern Boolean CloseHandle(IntPtr handle);

        public struct IMAGELISTDRAWPARAMS
        {
            public int cbSize;
            public IntPtr himl;
            public int i;
            public IntPtr hdcDst;
            public int x;
            public int y;
            public int cx;
            public int cy;
            public int xBitmap;        // x offset from the upper left of bitmap
            public int yBitmap;        // y offset from the upper left of bitmap
            public int rgbBk;
            public int rgbFg;
            public int fStyle;
            public int dwRop;
            public int fState;
            public int Frame;
            public int crEffect;
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct IMAGEINFO
        {
            public IntPtr hbmImage;
            public IntPtr hbmMask;
            public int Unused1;
            public int Unused2;
            public Rect rcImage;
        }

        [DllImport("shell32.dll", EntryPoint = "#727")]
        private extern static int SHGetImageList(
            int iImageList,
            ref Guid riid,
            out IImageList ppv
            );

        // The signature of SHGetFileInfo (located in Shell32.dll)
        [DllImport("Shell32.dll", CharSet = CharSet.Unicode)]
        public static extern int SHGetFileInfo(string pszPath, int dwFileAttributes, ref SHFILEINFO psfi, int cbFileInfo, uint uFlags);

        [DllImport("Shell32.dll", CharSet = CharSet.Unicode)]
        public static extern int SHGetFileInfo(IntPtr pszPath, uint dwFileAttributes, ref SHFILEINFO psfi, int cbFileInfo, uint uFlags);

        [DllImport("shell32.dll", SetLastError = true)]
        static extern int SHGetSpecialFolderLocation(IntPtr hwndOwner, Int32 nFolder, ref IntPtr ppidl);

        [DllImport("user32")]
        public static extern int DestroyIcon(IntPtr hIcon);

        public struct Pair
        {
            public System.Drawing.Icon Icon { get; set; }
            public IntPtr IconHandleToDestroy { set; get; }

        }

        public static Image BytesToImage(byte[] iconBytes)
        {
            using (var stream = new MemoryStream(iconBytes))
            {
                return Image.FromStream(stream);
            }
        }

        public static byte[] BytesFromIcon(System.Drawing.Icon ic)
        {
            var icon = Imaging.CreateBitmapSourceFromHIcon(ic.Handle, Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions());
            icon.Freeze();
            byte[] data;
            PngBitmapEncoder encoder = new PngBitmapEncoder();
            encoder.Frames.Add(BitmapFrame.Create(icon));
            using (MemoryStream ms = new MemoryStream())
            {
                encoder.Save(ms);
                data = ms.ToArray();
            }
            return data;
        }

        public static IconStruct GetIcon(string FileName, IconSize iconSize)
        {
            SHFILEINFO shinfo = new SHFILEINFO();
            uint flags;
            if (iconSize == IconSize.Small)
            {
                flags = SHGFI_ICON | SHGFI_SMALLICON;
            }
            else
            {
                flags = SHGFI_ICON | SHGFI_LARGEICON;
            }
            var res = SHGetFileInfo(FileName, 0, ref shinfo, Marshal.SizeOf(shinfo), flags);
            if (res == 0)
            {
                throw (new System.IO.FileNotFoundException());
            }

            // Get Icon from Shell Handle
            var icon = (System.Drawing.Icon)System.Drawing.Icon.FromHandle(shinfo.hIcon);
            var clone = (System.Drawing.Icon)icon.Clone();
            var bytes = BytesFromIcon(icon);

            // Free Icon Memory
            icon.Dispose();
            DestroyIcon(shinfo.hIcon);

            // Return Bytes and Icon Clone
            return new IconStruct(bytes, clone);
        }

        public static IconStruct GetLargeIcon(string FileName)
        {
            SHFILEINFO shinfo = new SHFILEINFO();
            uint SHGFI_SYSICONINDEX = 0x4000;
            int FILE_ATTRIBUTE_NORMAL = 0x80;
            uint flags;
            flags = SHGFI_SYSICONINDEX;
            var res = SHGetFileInfo(FileName, FILE_ATTRIBUTE_NORMAL, ref shinfo, Marshal.SizeOf(shinfo), flags);
            if (res == 0)
            {
                throw (new System.IO.FileNotFoundException());
            }
            var iconIndex = shinfo.iIcon;
            Guid iidImageList = new Guid("46EB5926-582E-4017-9FDF-E8998DAA0950");
            IImageList iml;
            int size = SHIL_EXTRALARGE;
            var hres = SHGetImageList(size, ref iidImageList, out iml); // writes iml
            IntPtr hIcon = IntPtr.Zero;
            int ILD_TRANSPARENT = 1;
            hres = iml.GetIcon(iconIndex, ILD_TRANSPARENT, ref hIcon);

            // Get Icon from Shell Handle
            var icon = System.Drawing.Icon.FromHandle(hIcon);
            var clone = (System.Drawing.Icon)icon.Clone();
            var bytes = BytesFromIcon(icon);

            // Free Icon Memory
            icon.Dispose();
            DestroyIcon(hIcon);

            // Return Bytes and Icon Clone
            return new IconStruct(bytes, clone);
        }
    }
}
