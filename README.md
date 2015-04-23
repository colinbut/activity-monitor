# Activity Monitor

A component of a wider application for my University degree final year project/dissertation during 2009/2010. 

### Overview
This is a background tool (like a service) that runs in the background of a Windows Operating System which actively monitors for 'activity' changes and sends the captured data in custom format to a consuming client application. 

Activity can be any abstract form of "tasks" that any user performs on their personal workstation. 
e.g. 
* creating/reading/viewing/editing/deleting a file
* creating/browsing/closing/deleting a folder (directory in Windows Explorer)
* emails tasks
* creating/opening/deleting files ... of Microsoft Office applications (WORD, EXCEL, POWERPOINT etc...)
* Browsing web site(s) 

### Technologies/Tools
+ C# 
+ .NET Framework (& dlls)
+ MSMQ
+ COM Add-ins
+ Visual Studio 

### Design
Various 'hooks' using .NET framework and Microsoft related technologies are placed where it captures changes in activity ("events") which are then forwarded internally via a MSMQ to be processed. The not needed data are then filtered out before it is transmitted through the TCP sockets for client applications to consume. 

These client applications can be any applications written in any language. I built an application in Java to do this for my university project. 

This activity monitor mechanism acts as the 'Server' of a Client-Server architecture. 

Only user browsing activity on the Internet Explorer web browser is available for capture.
