   M S B A M L   `   `   `  ����M  ILucky13_Milestone2, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null7 clr-namespace:Lucky13_Milestone2Lucky13_Milestone2  5   	   %    Lucky13_Milestone2.BusinessTips   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   2local clr-namespace:Lucky13_Milestone2     5   	     � Title$  BusinessTips��5   	   $��589.89��6   $��1145.701��6.    � WindowStartupLocation$ CenterScreen=�6?   .��5	      � .+�5
   
   �� -   5         ��Name$ BusinessName��5
      $��Business Name��6&   $
��Left=�6>   $��
163,15,0,0q�6Y   $	��Top=�6m   �� 5   
   -   5         ��Name$ busName��5      $
��Left=�6#   $��26��6>   $��
265,15,0,0q�6J    �� TextWrapping$
 Wrap=�6^   $�� ��6r   $	��Top=�6z   $	��498��6�    �� 
IsReadOnly . 6�   �� 5   
   -   5        $ 
tipTextBox��5      $
��Left=�6&   $��61��6A   $��
265,54,0,0q�6M   $
 Wrap=�6a   $�� ��6u   $	��Top=�6}   $	��498��6�   6�   �� 5   
   -   5        $ newTip��5      $��
Insert Tip��6    $
��Left=�65   $��
188,54,0,0q�6P   $	��Top=�6d   �� 5   
   -   5         ��Name$ addTipButton��5      $��Add Tip��6'   $
��Left=�69   $��26��6T   $��
815,23,0,0q�6`   $	��Top=�6t   $	��133��6�   6�   &   System.Windows.Controls.DataGrid  5   
   -   5          Name$ tipGrid��5      $	��373��6$   $��65,151,66.667,0q�61   $	��Top=�6J   $��14(�6b      CanUserResizeRows .  	   
IsReadOnly	 . 6�   �� 5   
   -   5        
 ��Name$
 Business_Tips��5      $��Business Tips��6*   $
��Left=�6A   $	��415��6\   $��
50,125,0,0q�6i   $	��Top=�6}   $
��1039��6�    J� RenderTransformOrigin$ 0.5,0.5&�6�   z�5      V� 5      .~�5      �� �� 5       �� AngleX$ 0.116Q�6$   �� 5      T� 5       T� X$ 0.307Q�6)   5      5      �� 5   
   -   5        $ likeTipButton��5      $��Like Selected Tip��6(   $
��Left=�6D   $��26��6_   $��
815,80,0,0q�6k   $	��Top=�6   $	��133��6�   6�   5      5      