# autoAdb
这是一个可以在安卓设备连接的时候自动运行某个程序的工具

需要一个配置文件
config.txt

配置内容如下

[设备名] [程序名] [命令行参数]

命令行参数支持 %name%(设备名)
例子:

127.0.0.1:5555 adb devices

注意:
当设备断开的时候会使程序启动的程序关闭

检测断开时间大概7s左右
