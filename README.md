# tarkov-settings (Fork)

> **This is a fork of [incheon-kim/tarkov-settings](https://github.com/incheon-kim/tarkov-settings).**
> See [Changes from upstream](#changes-from-upstream) for what's new in this fork.

![screenshot](./1.png)

## [-> **DOWNLOAD Latest** <-](https://github.com/ChrisLPJones/tarkov-settings/releases/latest)

Automatically change color settings for [Escape from Tarkov](https://escapefromtarkov.com).

## How it works
- Changes Digital Vibrance value from Nvidia Settings using [NvAPIWrapper](https://github.com/falahati/NvAPIWrapper)
- Changes Brightness, Contrast, and Gamma using [Win32 API calls](https://docs.microsoft.com/en-us/windows/win32/api/wingdi/nf-wingdi-setdevicegammaramp)

Colors are applied when Escape from Tarkov's window is in focus (or always, if Always On is enabled). This prevents sudden flashes when Alt-tabbing.

## Supported Graphics Cards
- Nvidia GPU — **fully supported** (Brightness / Contrast / Gamma / Saturation)
- AMD GPU — **partially supported** (Brightness / Contrast / Gamma only, no Saturation)
- Intel / other — **not supported**

## Features
1. Brightness, Contrast, Gamma, Digital Vibrance (Saturation) adjustment
2. **Profiles** — two independent colour profiles, switchable at any time
3. **Always On mode** — apply colours regardless of which window is focused
4. **Dark Mode** — full dark/light theme with native title bar colouring
5. Settings saved automatically on exit to `settings.json`
6. Minimize to system tray
7. Monitor / display selector
8. Custom process target list (works with games other than EFT)

## How to Use
1. Open the application (SmartScreen may warn — the app is not code-signed)
2. Adjust the colour sliders to your preference
   - Double-click any slider label to reset it to its default value
3. Optionally enable **Always On** to keep settings active outside EFT
4. Minimize and play EFT
5. Exit via the system tray icon to save your settings

**Settings are only written to `settings.json` when you exit through the tray icon.**

### Profiles
- Switch between **Profile 1** and **Profile 2** using the buttons at the top
- **Right-click** a profile button to rename it
- Each profile stores its own Brightness, Contrast, Gamma, and Saturation values

## Warning
1. The display may blink briefly when EFT gains focus — this is normal
2. **Disclaimer: Use at your own risk. It is unknown whether BSG will ban for using this tool.**
3. AMD GPUs only support Brightness / Contrast / Gamma — Saturation is not available
4. Intel graphics are not supported
5. Works in **Borderless window mode** only
6. Nvidia Optimus (most laptops with dual GPU) is untested

## Changes from upstream

This fork (v1.3.0.0) adds the following on top of the original [incheon-kim/tarkov-settings](https://github.com/incheon-kim/tarkov-settings):

### New features
- **Profiles** — two independently named colour presets, switchable with a button click; right-click to rename
- **Always On mode** — colour settings stay active even when EFT is not the focused window
- **Dark Mode** — full dark/light theme toggle, including native Windows title bar colouring via DWM

### Bug fixes & improvements
- Gamma loop refactored from a cancellable async task to a persistent background thread, preventing Windows from reverting the gamma ramp between updates
- Smooth slider preview: a dedicated preview thread applies slider values off the UI thread so dragging stays responsive
- DVL (Digital Vibrance) now expressed as a 0–100 percentage to match the Nvidia Control Panel scale
- Desktop switch events now correctly reset colours when switching virtual desktops
- AMD `Close()` no longer throws `NotImplementedException`
- GPU detection fixed for cards reporting `GeForce` (capital F)
- JSON settings deserialisation uses `ObjectCreationHandling.Replace` to prevent collection fields merging with defaults on load
- Update version check strips trailing whitespace that could cause version comparisons to fail

## TODO / Feature
- [x] Process focus awareness
- [x] Digital Vibrance value change
- [x] Gamma value change
- [x] Brightness, Contrast, Gamma adjustment
- [x] GUI
- [x] JSON configuration
- [x] Custom process target
- [x] Display / monitor selector
- [x] Minimize to tray
- [x] Profiles
- [x] Always On mode
- [x] Dark Mode
- [ ] Hot Keys
- [ ] EFT in-game setting modification (frame limit, graphics quality)

## Credits
- Original project: [incheon-kim/tarkov-settings](https://github.com/incheon-kim/tarkov-settings)
- [NvAPIWrapper](https://github.com/falahati/NvAPIWrapper) — Nvidia API bindings
- [Newtonsoft.Json](https://www.newtonsoft.com/json) — JSON serialisation
