   M S B A M L   `   `   `  ����F  BDataMapping, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null)clr-namespace:DataMappingDataMapping  5   	   I)clr-namespace:DataMapping.ValueConvertersDataMapping.ValueConverters  5   	       DataMapping.MainWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   +localclr-namespace:DataMapping     5   	   <local1)clr-namespace:DataMapping.ValueConverters     5   	     � Title$  Data Mapping��5	   	   $	��700��6   $
��1100��6+   
�����w� � 	Resources 5      =  7DataMapping.ValueConverters.PercentageGreenRowConverter   percentageGreenRowConverter;  5DataMapping.ValueConverters.PercentageRedRowConverter  percentageRedRowConverter>  8DataMapping.ValueConverters.PercentageYellowRowConverter   percentageYellowRowConverter��%W   &	        &	      &	 &       5   
   6T     5   
   6P     5   
   6V   5      .��5      �  � RowDefinitions 5   
   �� 5      $��* �6   �� 5      $��* �6   �� 5      $	��10* �6   �� 5      $
��0.6* �6    � ColumnDefinitions 5   
   �� 5      $��* �6   �� 5      $��9* �6   .+�5   
   �� $��Data Cleaning��6   $��30(�6(   $
��Bold"�66   $��Center=�6H   $��Center=�6e   $��2��6�   6�   �� 5   
   $��1��6   $��Center=�6"    �� Orientation$ 
Horizontal=�6=   $��2��6V   .+�5       �� $��File:��6   $��15(�6$   $��10, 0, 0, 0q�62   $��Center=�6G   6d   �� 5!      $��10,0,0,0q�61   $
��Bold"�6C   $	��600��6U   $��15(�6a   $��Center=�6o   ��6   �� *
FilePath+6�   �� 5"      $��Table:��6   $��15(�6%   $��10, 0, 0, 0q�63   $��Center=�6H   6e   �� 5#      $��10,0,0,0q�62   $
��Bold"�6D   $	��300��6V   $��15(�6b   $��Center=�6p   ��6   �� *	TableName+6�   5$      &   System.Windows.Controls.DataGrid  5&   
   -   5        $��GridEmployees��5&      $��2��6(   $��10q�65   $��1��6i   ��6A   �� *ComparisonTable+   RowStyle 5'      �� 5(       �� 
TargetType)  #System.Windows.Controls.DataGridRow"  6    �� Triggers 5)      �� 5*       �� Binding 6&   �� *PercentageMatching+	 �� 	Converter#	 [ 
 �� Value
 true6�   .a�5+      ��  �� Property � �� �� Value �#FF6F6F6M   5,      �� 5-       6&   �� *PercentageMatching+#	 [ 
 true6�   .a�5.      ��  � �� �#FFFF646M   5/      �� 50       6&   �� *PercentageMatching+#	 [  
 true6�   .a�51      ��  � �� �#79FF7A6M   52      54      56      �� 58   
   $��2��6   $	��Top=�6"   $ Vertical=�6:   $��
0, 5, 0, 0q�6Q   .+�59      �� -   5        $��
Load Table��59      $��15(�6*   $��5q�68   $��5q�6C   6p   �� 5:      -   5        $��Compare��5:      $��15(�6'   $��5q�65   $��5q�6@   6g   �� 5;      -   5        $��
Table Data��5;      $��15(�6*   $��5q�68   $��5q�6C   6p   �� 5<      -   5        $��
Set Delete��5<      $��15(�6*   $��5q�68   $��5q�6C   6p   �� 5=      -   5        $��Clean - Save��5=      $��15(�6,   $��5q�6:   $��5q�6E   6r   5>      �� 5@   
   $��3��6   $��2��6   $��15(�6R   $
��Bold"�6`   $��Center=�6r   $��Center=�6�   
���  ����61   �� *ErrorMessage+6�   5A      5B      