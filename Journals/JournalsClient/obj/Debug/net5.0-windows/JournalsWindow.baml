   M S B A M L   `   `   `  ����I  EJournalsClient, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null/clr-namespace:JournalsClientJournalsClient  5   	   #    JournalsClient.JournalsWindow   5   	   \ XPresentationFramework, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089W SPresentationCore, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   .localclr-namespace:JournalsClient     5   	     � Title$)  #Электронный журнал��5   	   $	��700��6$   $
��1100��61   $	��700��6>   $
��1100��6N   .��5	      �  � RowDefinitions 5
   
   �� 5      $
��700* �6    � ColumnDefinitions 5   
   �� 5      $	��150 �6   �� 5      $��* �6   .+�5   
   �� 
����ح�6   �� 5   
   $
��Wrap=�6   $��0,0,0,0q�6(   �� 6:    � Language$ ru-ru�6>   $,�&Иванов Иван Иванович��6O   6n   �� 5   
   -   5        $��JournalList��5      $#��Список журналов��6$   $��Center=�6>   $��0,40,0,0q�6[   $	��Top=�6m   $	��150��6�   $��30��6�   6�   �� 5   
   -   5        $��JournalCreating��5      $%��Создание журнала��6(   $��Center=�6C   $��0,75,0,0q�6`   $	��Top=�6r   $	��150��6�   $��30��6�   6�   �� 5   
   -   5        $��Account��5      $/��)Управление уч. записью��6!   $��Center=�6B   $��	0,110,0,0q�6_   $	��Top=�6r   $	��150��6�   $��30��6�   6�   �� 5   
   -   5        $��SettingsDepartment��5      $%��Настройки отдела��6+   $��Center=�6F   $��	0,145,0,0q�6c   $	��Top=�6v   $	��150��6�   $��30��6�   �� 5   
   -   5        $
��Exit��5      $��
Выход��6   $��Center=�6-   $��	0,180,0,0q�6J   $	��Top=�6]   $	��150��6u   $��30��6�    � 5   
   -   5        $��	WorkFrame��5      $��1��6"   
�������  � NavigationUIVisibility$ Hidden=�6E   5      5       