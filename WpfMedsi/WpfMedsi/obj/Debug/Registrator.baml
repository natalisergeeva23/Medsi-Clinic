   M S B A M L   `   `   `  ����C  ?WpfMedsi, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null#clr-namespace:WpfMedsiWpfMedsi  5   	       WpfMedsi.Registrator   5   	   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35U QScottPlot.WPF, Version=4.1.68.0, Culture=neutral, PublicKeyToken=e53b06131e34a3aa\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   (localclr-namespace:WpfMedsi     5   	     � Title$  Registrator��5   	   $	��450��6   $	��800��6*    � 	Resources 5	       �� 
TargetType �� Property �� Value �� 
TargetType �� CornerRadius��%  '��       �� 5
   
   " ��6   .�5      ��  � �� 5      �� 5      " ��6&   .R�5      �� #��z��6!   #��z��5   !   #��z��5   !   $ 10��5   !   ._�5      �� $��Center=�6/   $��Center=�6L   5      5      5      5      5      .��5      � ��5   
   �� 5       �� EndPoint$ 0.5,1&�6"    �� 
StartPoint$ 0.5,0&�63   .��5      � 	 � Color$	 	#FF14F9F0��6   � 5      $	 	#FFF416EF��6   
 � Offset$
 1Q�61   � 5       $	 	#FF6A1CED��6   $
 0.763Q�61   5!      .+�5#   
   �� -   5        $��Right=�5#      $	��Top=�6-   $��80��6E   $��
Выход��6P   6`   �� 5%   
   $��0,20,0,0q�6   .@�5&      �� -   5        $��Запись��5&   0   .��5'      � ��5(      �� 5)      $ 0.5,1&�6.   $ 0.5,0&�6?   .��5*      � $	 	#FF14F9F0��6+   � 5+      $	 	#FFF416EF��6+   $
 1Q�6=   � 5,      $	 	#FF6A1CED��6+   $
 0.763Q�6=   5-       � ColumnDefinitions 5/      �� 50      �� 51      �� 52       � RowDefinitions 54      �� 55      $
��166* �6(   �� 56      $	��37* �6(   .+�5:      &   System.Windows.Controls.DataGrid  -   5        $��dgRecording��5:      $��3��62   $��0��6F   $��20q�6V   �� 5<      -   5        $��30��5<      $��0��6)   $��1��69   $��Добавить��6F   $��Center=�6Y   $��Center=�6w   5>      �� 5?      -   5        $��30��5?      $��1��6)   $��1��69   $��Изменить��6F   $��Center=�6Y   $��Center=�6w   5A      �� 5B      -   5        $��30��5B      $��2��6)   $��1��69   $��Удалить��6F   $��Center=�6X   $��Center=�6v   5D      5E      5F      5H      5I      5J      