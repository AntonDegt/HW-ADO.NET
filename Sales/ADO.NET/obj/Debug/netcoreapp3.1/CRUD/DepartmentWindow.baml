   M S B A M L   `   `   `  ????B  >ADO.NET, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null+clr-namespace:ADO.NET.CRUDADO.NET.CRUD  5   	   #    ADO.NET.CRUD.DepartmentWindow   5   	   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   ,localclr-namespace:ADO.NET.CRUD     5   	   -   5          ? Title$  CrudDepartmentWindow??5	   	   $	??250??6&   $	??500??63   .??5
      ??  ?? Orientation$ Vertical=?6   .+?5   
   ?? $'??!Добавление отдела??6   $??Center=?6,   $??30(?5      $??DemiBold"?6   ? 5   
   $??70 30q?6    ? ColumnDefinitions 5      ?? 5      ?? 5      $??5* ?6#    ? RowDefinitions 5      ?? 5      ?? 5      .+?5      ?? $	??Id:??6   $??0??6"   $??0??6/   $??18(?5      ?? 5      $??Name:??6   $??1??6$   $??0??61   $??18(?5      ?? 5      -   5         ??Name$ DepartmentId??5      $??0??6,   $??1??69   ${?False??5      $	??0 5q?6(   ?? 5      -   5        $ DepartmentName??5      $??1??6.   $??1??6;   $
{?True??5      $	??0 5q?6'   5      ?? 5   
   $ 
Horizontal=?6   $??Center=?6.   .+?5       ?? -   5         ??Name$ 
ButtonSave??5       $??Сохранить??6)   $
??30 0q?6=   $
??10 4q?6K   5!      ?? 5"      -   5        $ ButtonDelete??5"      $??Удалить??6+   $
??30 0q?6=   $
??10 4q?6K   5#      ?? 5$      -   5        $ ButtonCancel??5$      $??Отмена??6+   $
??30 0q?6=   $
??10 4q?6K   5%      5&      5'      5(      