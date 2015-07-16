# TorqueLab v0.3 - Alpha Release
**Early-Alpha version warning (Messy scripts and unused files present)**
This is an early release which should be use for testing purpose mostly. It seem stable enough to be used as replacement editor for Torque3D game projects but some project incompatibilities might happen causing strange behaviours that might cause lost of data. (Ex: Forest .data files). You have been WARMED! Please post any major issue you encountered so those can be fixed in incoming releases.

## Developer notes
This is mostly a personal project that I decided to share with others because I think it make it easier to manage T3D projects than default T3D editors. TorqueLab is based on default T3D tools scripts which I have rearranged to be more modular and easier to edit. The scripts are not all optimized yet and some formatting are not looking good, I will try to improve the scripts during the development.
[https://github.com/NordikLab/TorqueLab/wiki/Unfinished-plugin-ports](https://github.com/NordikLab/TorqueLab/wiki/Unfinished-plugin-ports "Unfinished plugins ports")

## What's TorqueLab
TorqueLab is a completly revamping on the native Torque3D game editors (tools folder). The initial releases doesn't provide much new features, the work is focus on the scripts structure and the interface. Once those are completed, new features would be added.
For more informations, visit the official TorqueLab Wiki:
[https://github.com/NordikLab/TorqueLab/wiki](https://github.com/NordikLab/TorqueLab/wiki "TorqueLab Official Wiki")

## Instalation
The installation process have been simplified for v0.3 version. You can simply download the files, delete existing tools/ folder, then paste both tools/ and tlab/ folder provided in the root of your project. Then, if you haven't modified how the default editor are loaded, you should be able to use TorqueLab the same way as the standard editor. (F11 to load TorqueLab from inside a mission, F10 to load the GuiEditor from anywhere.

**You need to add few code changes to use TorqueLab, some of for extra features but some are needed else the game will crash when loading the editor. Instruction to add those changes are provide in installation page.**

For git based installation that allow to quickly pull new changes into your project, please visit the installation page.
[https://github.com/NordikLab/TorqueLab/wiki/Installation](https://github.com/NordikLab/TorqueLab/wiki/Installation "TorqueLab Installation WiKi")

## Notes
* TorqueLab will work without any code changes but some features might requires some changes in the code. Those would be disabled unless you make the needed changes.
* For current Pre-Alpha version, I have included my personnal helpers scripts since some are use in TorqueLab. I will make sure to embed those used inside TorqueLab in future release.

## Known major issues
* The Clone on object drag function is not working since it require some code change to work. I will examine to see if I can get it to work without code changes. If not, I will post the code changes needed. 
