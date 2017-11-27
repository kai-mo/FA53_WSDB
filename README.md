# FA53_WSDB
- School Project C# with interfaces

# Group number
\# 7

# Team
- User interface: KM
- functional specification / team commander: TK
- Data storage: MLS

# Summary
Create new games and developers and define relations between them.

# User interface
- Console
- Windows Forms

# Data storage
- SQLite as relational database
- JSON

# How to get the program working including Json.Net and SQLite
- Download Release File for 10.0.3 (Json100r3.zip) from here https://github.com/JamesNK/Newtonsoft.Json/releases
- Copy Newtonsoft.Json.dll, Newtonsoft.Json.xml and Newtonsoft.Json.pdb into your project root
- Download Precompiled Binaries for 64-bit Windows (.NET Framework 3.5 SP1) (sqlite-netFx35-binary-bundle-x64-2008-1.0.106.0.zip) from here https://system.data.sqlite.org/index.html/doc/trunk/www/downloads.wiki
- Copy System.Data.SQLite.dll, System.Data.SQLite.xml and System.Data.SQLite.pdb into your project root
- Execute ./Compile.BAT to compile the program
```
./Compile.BAT
```
- This creates GamesManager.exe open it with ./GamesManager.exe (tui|gui) (1|2) (1|2)
```
./GamesManager.exe (tui|gui) (1|2) (1|2)
```