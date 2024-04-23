/*
 * https://www.ipentec.com/document/csharp-shell-namespace-get-big-icon-from-file-path
 * https://raw.githubusercontent.com/bryful/AE_utils/master/AE_Calc/WindowsShellAPI.cs
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;

namespace QuickLaunch.Utils
{
    public class WindowsShellAPI
    {
        [DllImport("shell32.dll")]
        public static extern IntPtr SHGetFileInfo(string pszPath, uint dwFileAttribs, out SHFILEINFO psfi, uint cbFileInfo, SHGFI uFlags);

        [DllImport("shell32.dll", EntryPoint = "#727")]
        public static extern int SHGetImageList(int iImageList, ref Guid riid, ref IImageList ppv);

        //[DllImport("shell32.dll", EntryPoint = "#727")]
        //public static extern int SHGetImageList(SHIL iImageList, [MarshalAs(UnmanagedType.LPStruct)] Guid riid, ref IImageList ppv);

        [DllImport("shell32.dll", EntryPoint = "#727")]
        public static extern int SHGetImageList(SHIL iImageList, ref Guid riid, out IImageList ppv);

        //
        [DllImport("shell32.dll", EntryPoint = "#727")]
        public static extern int SHGetImageList(SHIL iImageList, ref Guid riid, out IntPtr ppv);

        [DllImport("comctl32.dll", SetLastError = true)]
        public static extern IntPtr ImageList_GetIcon(IntPtr himl, int i, int flags);

        //SHFILEINFO
        [Flags]
        public enum SHGFI
        {
            SHGFI_ICON = 0x000000100,
            SHGFI_DISPLAYNAME = 0x000000200,
            SHGFI_TYPENAME = 0x000000400,
            SHGFI_ATTRIBUTES = 0x000000800,
            SHGFI_ICONLOCATION = 0x000001000,
            SHGFI_EXETYPE = 0x000002000,
            SHGFI_SYSICONINDEX = 0x000004000,
            SHGFI_LINKOVERLAY = 0x000008000,
            SHGFI_SELECTED = 0x000010000,
            SHGFI_ATTR_SPECIFIED = 0x000020000,
            SHGFI_LARGEICON = 0x000000000,
            SHGFI_SMALLICON = 0x000000001,
            SHGFI_OPENICON = 0x000000002,
            SHGFI_SHELLICONSIZE = 0x000000004,
            SHGFI_PIDL = 0x000000008,
            SHGFI_USEFILEATTRIBUTES = 0x000000010,
            SHGFI_ADDOVERLAYS = 0x000000020,
            SHGFI_OVERLAYINDEX = 0x000000040
        }

        public struct SHFILEINFO
        {
            public IntPtr hIcon;
            public int iIcon;
            public uint dwAttributes;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 260)]
            public string szDisplayName;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 80)]
            public string szTypeName;
        }



        //IMAGE LIST
        public static Guid IID_IImageList = new Guid("46EB5926-582E-4017-9FDF-E8998DAA0950");
        public static Guid IID_IImageList2 = new Guid("192B9D83-50FC-457B-90A0-2B82A8B5DAE1");
        //Private Const IID_IImageList    As String = "{46EB5926-582E-4017-9FDF-E8998DAA0950}"
        //Private Const IID_IImageList2   As String = "{192B9D83-50FC-457B-90A0-2B82A8B5DAE1}"


        [Flags]
        public enum SHIL
        {
            SHIL_JUMBO = 0x0004,
            SHIL_EXTRALARGE = 0x0002
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct POINT
        {
            int x;
            int y;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct RECT
        {
            public int left, top, right, bottom;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct IMAGEINFO
        {
            public IntPtr hbmImage;
            public IntPtr hbmMask;
            public int Unused1;
            public int Unused2;
            public RECT rcImage;
        }

        [StructLayout(LayoutKind.Sequential)]
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
            public int xBitmap;    // x offest from the upperleft of bitmap
            public int yBitmap;    // y offset from the upperleft of bitmap
            public int rgbBk;
            public int rgbFg;
            public int fStyle;
            public int dwRop;
            public int fState;
            public int Frame;
            public int crEffect;
        }

        [Flags]
        public enum ImageListDrawItemConstants : int
        {
            /// <summary>
            /// Draw item normally.
            /// </summary>
            ILD_NORMAL = 0x0,
            /// <summary>
            /// Draw item transparently.
            /// </summary>
            ILD_TRANSPARENT = 0x1,
            /// <summary>
            /// Draw item blended with 25% of the specified foreground colour
            /// or the Highlight colour if no foreground colour specified.
            /// </summary>
            ILD_BLEND25 = 0x2,
            /// <summary>
            /// Draw item blended with 50% of the specified foreground colour
            /// or the Highlight colour if no foreground colour specified.
            /// </summary>
            ILD_SELECTED = 0x4,
            /// <summary>
            /// Draw the icon's mask
            /// </summary>
            ILD_MASK = 0x10,
            /// <summary>
            /// Draw the icon image without using the mask
            /// </summary>
            ILD_IMAGE = 0x20,
            /// <summary>
            /// Draw the icon using the ROP specified.
            /// </summary>
            ILD_ROP = 0x40,
            /// <summary>
            /// ?
            /// </summary>
            ILD_OVERLAYMASK = 0xF00,
            /// <summary>
            /// Preserves the alpha channel in dest. XP only.
            /// </summary>
            ILD_PRESERVEALPHA = 0x1000, // 
            /// <summary>
            /// Scale the image to cx, cy instead of clipping it.  XP only.
            /// </summary>
            ILD_SCALE = 0x2000,
            /// <summary>
            /// Scale the image to the current DPI of the display. XP only.
            /// </summary>
            ILD_DPISCALE = 0x4000
        }


        // interface COM IImageList
        [ComImportAttribute()]
        [GuidAttribute("46EB5926-582E-4017-9FDF-E8998DAA0950")]
        [InterfaceTypeAttribute(ComInterfaceType.InterfaceIsIUnknown)]
        public interface IImageList
        {
            [PreserveSig]
            int Add(IntPtr hbmImage, IntPtr hbmMask, ref int pi);

            [PreserveSig]
            int ReplaceIcon(int i, IntPtr hicon, ref int pi);

            [PreserveSig]
            int SetOverlayImage(int iImage, int iOverlay);

            [PreserveSig]
            int Replace(int i, IntPtr hbmImage, IntPtr hbmMask);

            [PreserveSig]
            int AddMasked(IntPtr hbmImage, int crMask, ref int pi);

            [PreserveSig]
            int Draw(ref IMAGELISTDRAWPARAMS pimldp);

            [PreserveSig]
            int Remove(int i);

            [PreserveSig]
            int GetIcon(int i, int flags, ref IntPtr picon);
        };

        // https://learn.microsoft.com/en-us/answers/questions/705658/in-c-can-i-write-code-to-run-a-windows-11-shortcut
        public enum ShowWindowCommands : int
        {
            SW_HIDE = 0,
            SW_SHOWNORMAL = 1,
            SW_NORMAL = 1,
            SW_SHOWMINIMIZED = 2,
            SW_SHOWMAXIMIZED = 3,
            SW_MAXIMIZE = 3,
            SW_SHOWNOACTIVATE = 4,
            SW_SHOW = 5,
            SW_MINIMIZE = 6,
            SW_SHOWMINNOACTIVE = 7,
            SW_SHOWNA = 8,
            SW_RESTORE = 9,
            SW_SHOWDEFAULT = 10,
            SW_MAX = 10
        }

        [DllImport("shell32.dll")]
        public static extern IntPtr ShellExecute(
            IntPtr hwnd,
            string lpszOp,
            string lpszFile,
            string lpszParams,
            string lpszDir,
            ShowWindowCommands FsShowCmd
        );

        public static void ShellExecuteRun(String sLink, ShowWindowCommands FsShowCmd)
        {
            ShellExecute(IntPtr.Zero, "open", sLink, null, null, FsShowCmd);
        }
    }
}