﻿# Changelog for Snoop

## 3.0.0 (preview)

- ### Bug fixes

  - [#40](../../issues/40) - Message: Cannot set Expression. It is marked as 'NonShareable' and has already been used.
  - [#45](../../issues/45) - Keystrokes go to Visual Studio main window when inspecting Visual Studio (thanks @KirillOsenkov)
  - [#66](../../issues/66) - System.InvalidOperationException: Collection was modified; enumeration operation may not execute.
  - [#101](../../issues/101) - My Style is being applied to the "Change Target" Window
  - [#120](../../issues/120) - Screenshot feature produces pixelated low-res image for larger windows
  - [#150](../../issues/150) - Format and parse property values with the same format provider
  - [#151](../../issues/151) - Dependency properties are filtered wrong and less properties are shown than should be
  - [#152](../../issues/152) - Magnified view only works for main window
  - [#156](../../issues/156) - Delve BindingExpression throws exception
  - [#159](../../issues/159) - Errors require STA
  - [#177](../../issues/177) - Could not query process information.
  - Snoop now properly selects the targeted window on startup
  - Snooping multiple app domains now also works for app domains that use shadow copies of assemblies
  - Snooping multiple app domains now also checks for multiple dispatchers in each app domain

- ### Improvements

  - You no longer have to have installed any Microsoft Visual C++ Redistributable(s)
  - Added a lot more tracing to the injection process. This tracing can be viewed with [DbgView](https://docs.microsoft.com/en-us/sysinternals/downloads/debugview).
  - Because of [#151](../../issues/151) there are now a lot more properties being shown.  
    As a way to reduce the noise a new option to filter uncommom properties was added. The default value for that is `true`, so uncommon properties are hidden by default.  
    If you want to show uncommon properties from types like `Typography` or `NumberSubstitution` etc. just disable the new switch right beside the default value switch.
  - Added "Copy XAML" to the context menu of the property grid. Please note that this feature is not finished and the generated XAML is not very good. I hope to improve this in the future.
  - [#82](../../issues/82) - Missing possibility of copying value of the specific node
  - [#98](../../issues/98) - .NETCore 3.0 support
  - [#108](../../issues/108) - SnoopWPF on "Disabled" control state?
  - [#129](../../issues/129) - Command line args
  - [#139](../../issues/139) - Value Input did not support NewLine (\r\n)  
    This is achieved by a new detail value editor.
  - [#140](../../issues/140) - CTRL_SHIFT stops working
  - [#141](../../issues/141) - Add support to view logical tree
  - [#142](../../issues/142) - Add support to view ui automation tree (wpf automation peers)
  - [#144](../../issues/144) - Add support for showing behaviors (added by @dezsiszabi in [#149](../../pull/149))
  - Snoop now filters uncommon properties by default
  - Snoop is now able to show `MergedDictionaries` from `ResourceDictionary`
  - Snoop now has two tracking modes. 
    - Holding CTRL tries to skip template parts
    - Holding CTRL + SHIFT does not skip template parts
  - [#161](../../issues/161) - Drastically improved performance of AppChooser.Refresh() (thanks @mikel785)
  - [#162](../../issues/162) - Usability improvements for process dropdown (thanks @mikel785)
  - [#181](../../issues/181) - Add inspection of Popup without opening it

## 2.11.0

- ### Bug fixes
  
  - [#53](../../issues/53) - Path Data values have wrong format (should use invariant culture) (thanks @jongleur1983)
  - [#55](../../issues/55) - Keyboard events not passed to snoop UI window (thanks @stutton)
  - [#56](../../issues/56) - Snoop crash when application shutdown (solved by using System.Windows.Forms.Clipboard)
  - [#83](../../issues/83) - Unhandled Exception when changing WPF Trace Level to Activity Tracing (thanks @miloush)
  - [#86](../../issues/86) - Fatal ExecutionEngineException when process has hidden windows without composition target (thanks @gix)
  - [#99](../../issues/99) - Prevent window from being restored on screen that's disconnected/off
  - [#100](../../issues/100) - Snoop 2.10 crashes when snooping a WPF App that uses AvalonDock
  - [#106](../../issues/106) - Refresh fails because "process has exited" (thanks @jmbeach)

- ### Improvements

  - [#32](../../issues/32) - Try to use `AutomationProperties.AutomationId` for `VisualTreeItem` name if element name is not specified. (thanks @paulspiteri)
  - [#73](../../issues/73) - Add options to prevent multiple dispatcher question and setting of owner on snoop windows
  - [#89](../../issues/89) - Improved exception handling and error dialog
  - [#92](../../issues/92) - Adding support for snooping elevated processes from a non elevated snoop instance
  - [#116](../../issues/116) - Doesn't find PresentationSource hosted in CustomTaskPane (ElementHost) in Office VSTO Add-in  
  This means snoop is now able to spy on multiple app domains.
  - [#119](../../issues/119) - Adding hyperlink for current delve object to enable explorer navigation
  - The window finder was rewritten to not use a separate window but a dynamically generated mouse cursor instead

## 2.10.0

- ### Breaking changes
  
  - Dropped support for .NET 3.5
  - You now need Visual C++ 2015 Runtime to run snoop

## 2.9.0

- ### Improvements
  
  - Added a new triggers tab to view triggers from ControlTemplates and Styles