   M S B A M L   `   `   `  ����M  ILucky13_Milestone2, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null7 clr-namespace:Lucky13_Milestone2Lucky13_Milestone2  5   	   (    "Lucky13_Milestone2.BusinessReviews   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   2local clr-namespace:Lucky13_Milestone2     5   	     � Title$  BusinessReviews��5   	   $��589.89��6!   $��1145.701��61    � WindowStartupLocation$ CenterScreen=�6B   .��5	      � .+�5
   
   �� -   5         ��Name$ BusinessName��5
      $��Business Name��6&   $
��Left=�6>   $��	64,10,0,0q�6Y   $	��Top=�6l   �� 5   
   -   5         ��Name$ busName��5      $
��Left=�6#   $��26��6>   $��
166,10,0,0q�6J    �� TextWrapping$
 Wrap=�6^   $�� ��6r   $	��Top=�6z   $	��244��6�    �� 
IsReadOnly . 6�   &   System.Windows.Controls.DataGrid  5   
   -   5          Name$ 
reviewGrid��5      $	��215��6'   $��65,75,66.667,0q�64   $	��Top=�6L   $��14(�6d      CanUserResizeRows .     
IsReadOnly . 6�   �� 5   
   -   5        	 ��Name$	 Business_Reviews��5      $��Business Reviews��6-   $
��Left=�6G   $	��259��6b   $��	50,49,0,0q�6o   $	��Top=�6�   $
��1039��6�   
 J� RenderTransformOrigin$
 0.5,0.5&�6�   z�5      V� 5      .~�5      �� �� 5       �� AngleX$ 0.116Q�6$   �� 5      T� 5       T� X$ 0.307Q�6)   5      5      �� 5   
   -   5        $	 groupBox��5      $��Business Recommendations��6%   $
��Left=�6G   $	��248��6b   $��48,316,0,-0.333q�6o   $	��Top=�6�   $
��1039��6�   .��5        -   5        $ recommendationsDataGrid��5      $
��Left=�68   $	��208��6S   $��9,10,0,0q�6`   $	��Top=�6r   $	��691��6�   5      �� 5   
   -   5        $	 
groupBox10��5      $��Minimum Stars��6'   $
��Left=�6>   $��61��6Y   $��773,361,0,0q�6e   $	��Top=�6z   $	��292��6�   .��5      �� -   5         ��Name$ minRatingList��5      $
��Left=�6.   $��27��6I   $��15,6,0,0q�6U   $	��Top=�6g   $	��254��6   6�   5      �� 5   
   -	   5        $	 groupBox10_Copy��5      $��Limit on Recommendations��6,   $
��Left=�6N   $��61��6i   $��773,444,0,0q�6u   $	��Top=�6�   $	��292��6�   .��5      �� -
   5        $ 
limRecList��5      $
��Left=�6+   $��27��6F   $��15,6,0,0q�6R   $	��Top=�6d   $	��254��6|   6�   5      5!      5"      