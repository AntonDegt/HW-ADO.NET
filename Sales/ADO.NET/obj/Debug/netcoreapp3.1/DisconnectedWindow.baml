   M S B A M L   `   `   `  ????B  >ADO.NET, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null!clr-namespace:ADO.NETADO.NET  5   	        ADO.NET.DisconnectedWindow   5   	   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   'localclr-namespace:ADO.NET     5   	     ? Title$  Disconnected Window??5   	   $	??450??6%   $	??800??62   .??5
      ?  ? ShowGridLines$
 True??6    ? ColumnDefinitions 5   
   ?? 5      ?? 5       ? RowDefinitions 5   
   ?? 5      ?? 5      .+?5   
   ??  ?? Orientation$ Vertical=?6   $??0??5      $??0??6"   .+?5      ?? $??Отделы??6   ?? 5      $??Visible=?5      $	??150??5      ??5      ?? *Departments+ ?? View 5      ?? 5      .P?5      ?? $??Id??5        ?? DisplayMemberBinding 6)   ??  ?? Path$ Id??? 5!      $
??Name??5"       6+   ?? $
 Name?5#       ? 	Resources 5%      	 ?? 
TargetType??%E   '??       ?? 5&      "	 ??6   .?5'      -   5        5*      5+      5,      ?? 5-      -   5        
 ??Name$
 AddDepartment??5.      $!??Добавить отдел??5-      $??5q?50      51      ?? 53   
   $ Vertical=?6   $??1??54      $??0??6"   .+?55      ?? $??Товары??6   ?? 58      $??Visible=?59      $	??150??5:      ??58      ?? *
Products+ 5;      ?? 5<      .P?5>      ?? $??Id??5?       6)   ?? $ Id??? 5@      $
??Name??5A       6+   ?? $
 Name??? 5B      $
??Name??5C       6+   ?? $ Price?5D       5F      ??%E   '??       ?? 5G      "	 ??6   .?5H      -   5        5K      5L      5M      5N      ?? 5P   
   $ Vertical=?6   $??0??5Q      $??1??6"   .+?5R      ?? $??Сотрудники??6   ?? 5U      $??Visible=?5V      $	??150??5W      ??5U      ?? *
Managers+ 5X      ?? 5Y      .P?5[      ?? $??Id??6)    5\      ?? $ Id??? 5]      $
??Name??6)    5^      ?? $
 Name??? 5_      $??Surname??6)    5`      ?? $ Surname??? 5a      $
??Chif??6)    5b      ?? $ IdChief?5c       5e      ??%E   '??       ?? 5f      "	 ??6   .?5g      -   5        5j      5k      5l      ?? 5m      -   5        $
 
AddManager??5n      $+??%Добавить сотрудника??5m      $??5q?5p      5q      5r      5s      