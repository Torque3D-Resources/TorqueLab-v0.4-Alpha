# TorqueLab v0.4 - Alpha Release
**Alpha version warning (Messy scripts and unused files present)**
This is the first stabilized release which would lead to first beta once tested and reported issues fixed. Some components still can produced unexpected crash and those would be fixed as soon as possible once reported. If you use this TorqueLab release, please report any major issues to the GitHub issue system or on the official TorqueLab forum: http://www.nordiklab.com/forum/torquelab . 

For those looking for a 100% stable T3D editor, please wait for the incoming Beta release.

## Developer notes
This is mostly a personal project that I decided to share with others because I think it make it easier to manage T3D projects than default T3D editors. TorqueLab is based on default T3D tools scripts which I have rearranged to be more modular and easier to edit. The scripts are not all optimized yet and some formatting are not looking good, I will try to improve the scripts during the development.
[https://github.com/NordikLab/TorqueLab/wiki/Unfinished-plugin-ports](https://github.com/NordikLab/TorqueLab/wiki/Unfinished-plugin-ports "Unfinished plugins ports")

## What's TorqueLab
TorqueLab is a completly revamping on the native Torque3D game editors (tools folder). The initial releases doesn't provide much new features, the work is focus on the scripts structure and the interface. Once those are completed, new features would be added.
For more informations, visit the official TorqueLab Wiki:
[https://github.com/NordikLab/TorqueLab/wiki](https://github.com/NordikLab/TorqueLab/wiki "TorqueLab Official Wiki")

## Instalation
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

For git based installation that allow to quickly pull new changes into your project, please visit the installation page.
[https://github.com/NordikLab/TorqueLab/wiki/Installation](https://github.com/NordikLab/TorqueLab/wiki/Installation "TorqueLab Installation WiKi")

## Notes
* TorqueLab will work without any code changes but some features might requires some changes in the code. Those would be disabled unless you make the needed changes.
* For current Pre-Alpha version, I have included my personnal helpers scripts since some are use in TorqueLab. I will make sure to embed those used inside TorqueLab in future release.

## Known major issues
* The Clone on object drag function is not working since it require some code change to work. I will examine to see if I can get it to work without code changes. If not, I will post the code changes needed. 
