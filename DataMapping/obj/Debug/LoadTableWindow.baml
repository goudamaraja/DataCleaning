   M S B A M L   `   `   `  ����F  BDataMapping, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null)clr-namespace:DataMappingDataMapping  5   	   !    DataMapping.LoadTableWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   +localclr-namespace:DataMapping     5   	     � Title$  	LoadTable��5   	   $	��250��6   $	��670��6(   
�����w�.��5	      �  � RowDefinitions 5
   
   �� 5      $��* �6   �� 5      $��* �6   �� 5      $
��0.5* �6   �� 5      $��* �6   .+�5   
   �� $��0��6   $��Center=�6"    �� Orientation$ 
Horizontal=�6=   .+�5      �� $��File:��6   $��15(�6$   $��10, 0, 0, 0q�62   $��Center=�6G   6d   �� 5      $��10,0,0,0q�60   $	��500��6B   $��15(�6N   $��Center=�6\   ��6   �� *
FilePath+6�   �� 5      -   5        $��Search��5      $��15(�6&   $��10,0,0,0q�64   $��5q�6F   6p   5      �� 5   
   $��1��6   $��Center=�6"   $ 
Horizontal=�6=   .+�5      �� $��Table:��6   $��15(�6%   $��10, 0, 0, 0q�63   $��Center=�6H   6e   �� 5      -   5        $��cmbbx_TableName��5      $��10,0,0,0q�6P   $	��300��6b   $��15(�6n   $��Center=�6|   ��6.   �� *	TableList+6�   5      �� 5   
   $��2��6   $��15(�6>   $
��Bold"�6L   $��Center=�6^   $��Center=�6y   
���  ����6   �� *ErrorMessage+6�   �� 5   
   -   5        $��3��5      $��Enter��6   $��15(�6.   $��5q�6<   $��Center=�6c   $��Center=�6~   6�   5      5      