# EFramework
使用说明：

项目版本：VS2012+，数据库版本：SQL2008+

eFrameWork为公开框架,所以默认帐号、密码、目录结构等信息也是公开的,为保证系统安全性，请务必注意以下内容修改。
1.修改管理员默认用户名、密码。
2.Manage文件夹为开发平台，实际项目开发完成后可考虑不上传些文件夹。如需在网络上开启，请重命名该文件夹。
3.System文件夹为系统管理端，实际项目请重命名该文件夹。
4.Examples文件夹为框架学习示例，实际项目请整个文件夹删除。
5.参数安全性检查统一写成一个通用函数放在Global下，请求产生时调用验证。可根据需要进行完善。
6.数据库打开及关闭默认放在Global下统一打开和关闭，使用比较方便。但如果访问量大的站建议需要用到数据库时再打开，用完关闭。
7.系统默认用户名：eketeam 密码：123456 可登录开发平台和管理系统。
8.数据库还原后,记得修改web.config连接字符串
9.框架将不断完善更新,敬请关注frame.eketeam.com
10.下载框架请到官网frame.eketeam.com进行，同版本有些小BUG会在官网保持最新。

如果在使用过程中有问题或有好的建议欢迎与我们联系。
frame.eketeam.com