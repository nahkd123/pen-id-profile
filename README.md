# Pen ID profile
Apply different effect based on pen ID

## Installation
### Build from source
1. Clone this repository and build with .NET 8 SDK
1. Copy `bin/<Configuration>/net8.0` folder to `<OTD app data path>/Plugins` (
and rename it)
	+ Windows: `%localappdata%\OpenTabletDriver\Plugins`
	+ TODO MacOS and Linux
1. Restart OpenTabletDriver

## Setting up plugin
### Getting pen ID
1. Activate any "Pen ID profile" filter with "Print pen ID in console" option
1. Open Console tab then use the pen

### Double pressure
For double pressure filter, simple add your pen ID in "Pen IDs" textbox.
Separate each ID by a comma. For example, `42-07-27,42-13-37` means a pen with
ID `42-07-27` and another pen with ID `42-13-37`.