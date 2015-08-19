# TorqueLab v0.4 - Alpha Release
**Alpha version warning (Messy scripts and unused files present)**
This is an alpha release and might have issues that could cause Torque3D engine crash. Version 0.4 seem to be very stable, no crash have been experienced with it on our test setup but some issues might cause crash for other test environment. If you experience a crash, please report it with the most informations you can on the GitHub issues page or on our public forum dedicated to TorqueLab: http://www.nordiklab.com/forum/torquelab 

**Before testing TorqueLab, make sure you have applied the minimal code changes (see details below).**
## Developer notes
This is mostly a personal project that I decided to share with others because I think it make it easier to manage T3D projects than default T3D editors. TorqueLab is based on default T3D tools scripts which I have rearranged to be more modular and easier to edit. The scripts are not all optimized yet and some formatting are not looking good, I will keep improving the scripts during the development.

## What's TorqueLab
TorqueLab is a completly revamping on the native Torque3D game editors (tools folder). The initial releases doesn't provide much new features, the work is focus on the scripts structure and the interface. Once those are completed, new features would be added.
TorqueLab use my personal scripts helpers library which is included in the repository files. It contain only the required helpers (core), if you'd like to have a look at the entire library, visit the [ HelpersLab GitHub Page]( https://github.com/NordikLab/HelpersLab "HelpersLab GitHub Page")

For more informations, visit the official TorqueLab Wiki:
[https://github.com/NordikLab/TorqueLab/wiki](https://github.com/NordikLab/TorqueLab/wiki "TorqueLab Official Wiki")

## Installation
The installation process have been simplified for v0.4 version. You can simply download the files, delete existing tools/ folder, then paste both tools/ and tlab/ folder provided in the root of your project. Then, if you haven't modified how the default editor are loaded, you should be able to use TorqueLab the same way as the standard editor. (F11 to load TorqueLab from inside a mission, F10 to load the GuiEditor from anywhere.
https://github.com/NordikLab/TorqueLab/wiki/TorqueLab-required-source-code-changes
#### You need to recompile your project with the required changes added to your source code:
[Adding required code changes to your project](https://github.com/NordikLab/TorqueLab/wiki/TorqueLab-required-source-code-changes "Info")
### Quick Installation
- Delete default tools/ folder from project root
- Paste both tlab/ and tools/ folder from the GitHug TorqueLab repository
- Apply the required code changes to your project source code
- Compile your project with the source changes ([Info](https://github.com/NordikLab/TorqueLab/wiki/TorqueLab-required-source-code-changes "Info")) 
- Enjoy TorqueLab editor the same way as default editor (F11 to load TorqueLab from inside a mission, F10 to load the GuiEditor from anywhere)

### Advanced Installation
Visit the special wiki pages for advanced instruction about how to install TorqueLab from Git so you can easily update your TorqueLab installation with latest changes.
[https://github.com/NordikLab/TorqueLab/wiki/Installation](https://github.com/NordikLab/TorqueLab/wiki/Installation "TorqueLab Installation WiKi")

**You need to add few code changes to use TorqueLab, some of for extra features but some are needed else the game will crash when loading the editor. Instruction to add those changes are provide in installation page.**

## Known major issues
* None reported for now
