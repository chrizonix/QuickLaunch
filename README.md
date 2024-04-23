# Quick Launch Toolbar for Windows 11
[![GitHub Release](https://img.shields.io/github/release/chrizonix/QuickLaunch.svg)](https://github.com/chrizonix/QuickLaunch/releases/tag/v1.2.8.0)
[![Github Downloads](https://img.shields.io/github/downloads/chrizonix/QuickLaunch/total.svg)](https://github.com/chrizonix/QuickLaunch/releases/tag/v1.2.8.0)
[![Github Commits (since latest release)](https://img.shields.io/github/commits-since/chrizonix/QuickLaunch/latest.svg)](https://github.com/chrizonix/QuickLaunch/compare/v1.2.8.0...master)
[![GitHub Repo Size in Bytes](https://img.shields.io/github/repo-size/chrizonix/QuickLaunch.svg)](https://github.com/chrizonix/QuickLaunch)
[![Github License](https://img.shields.io/github/license/chrizonix/QuickLaunch.svg)](LICENSE.md)

Simple Tool to create a Quick Launch Toolbar in Windows 11 similar to the toolbar in Windows 10.

## How Does It Work?
This tool allows you to create a Context Menu in the Taskbar of Windows 11 by selecting a local folder containing:
* Links
* Shortcuts
* Folders
* Documents
* etc.

It uses the Windows Shell32 and User32 API to Extract the Icons of the Shortcuts in the selected folder.  
You can then re-order the items, and then save the config and serialize the Context Menu to disk.

Afterwards you can create a Quick Launch Icon in the Taskbar, that will use the Shell32 API to Launch the Shortcuts.

## How To / Usage
* Copy/Extract the `QuickLaunch.exe` to a Permanent Folder (e.g. Software/Tools)
* Select your Shortcut Folder containing the Links/Shortcuts/Documents
* Re-Order the Context Menu, and Setup the Context Menu Position
* Click on the "Save Config" Menu Button
* Create a Desktop Shortcut to `QuickLaunch.exe`
* Add `--show` or `--show --hide-taskbar` to the "Target" Parameter in the Desktop Shortcut
* Drag & Drop the Desktop Shortcut to your Windows Taskbar
* To use the Quick Launch Toolbar, just click the Icon in the Windows Taskbar

## Command Line Options
* `--show`
  * Show Context Menu instead of Main Application
  * ***Hint:*** During Startup of the programm you can also hold the `SHIFT` key to always start the Main Application
* `--hide-taskbar`
  * By default, WinForms creates a Window in the Taskbar. This option hides the Window in the Taskbar
  * ***Hint:*** Due to the simplicity of the CLI Parser, this option has to be the second, e.g. `--show --hide-taskbar`

## Hidden Features
* You can rename Shortcut Links by editing the value in the `[Shortcuts]` section of `config.ini` (see Screenshot)
* Hold down the `SHIFT` Key while clicking on the Taskbar Icon, to start the Main Application for Config Changes
* Folders and Sub-Folders are also supported. This will create Cascading Context Menus (see Screenshot)

![Screenshot](https://github.com/chrizonix/QuickLaunch/releases/download/v1.2.8.0/Screenshot.png)

## Changelog v1.2.8.0 ([Latest Version](https://github.com/chrizonix/QuickLaunch/releases/tag/v1.2.8.0))
* Added 64-bit Binaries

## Changelog v1.2.6.0
* Added Caching / Serialization of Context Menu for Faster Load Times

## Build Instructions
* Use Visual Studio 2022 and .NET Framework 4.8
* Always Build the Release in 64-bit Mode (Platform x64)
  * Otherwise you cannot Extract Icons or Launch 64-bit Shortcuts!
* Changes in the AnySerializer have to be manually copied to the "Resources" Folder
  * Make sure to set the `*.dll` in the "Resources" Folder to "Do not copy"

## Credits
* [AnySerializer by Michael Brown](https://github.com/replaysMike/AnySerializer)
* [Simple INI by Ronen Ness](https://github.com/RonenNess/sini)
* [Embedded Assembly by adriancs](https://www.codeproject.com/Articles/528178/Load-DLL-From-Embedded-Resource)
