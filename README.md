# autoAdb
这是一个可以在安卓设备连接的时候自动运行某个程序的工具

需要一个配置文件
config.txt

配置内容如下

设备名 程序运行路径 程序名 [命令行参数]

命令行参数支持 %name%(设备名)
[设备名] 支持 any  任何设备
例子:

// 127.0.0.1:5555 这个设备连接的时候执行cmd
127.0.0.1:5555 ./ cmd


// 有设备连接的时候将设备的名字写入到 d:/log.log 文件中
any ./ echo %name%>>d:/log.log


127.0.0.1:5555 ./ test.bat

注意:
当设备断开的时候会使程序启动的程序关闭

检测断开时间大概7s左右


#AutoAdb
This is a tool that can automatically run a program when an Android device is connected

Need a configuration file
Config.txt

The configuration content is as follows

Device Name Program Run Path Program Name [Command Line Parameters]

Command line parameters support% name% (device name)
[Device Name] supports any device
Example:

127.0.0.1:5555/ Test.bat

Attention:
When the device is disconnected, it will cause the program that started to close

The detection disconnection time is about 7 seconds
