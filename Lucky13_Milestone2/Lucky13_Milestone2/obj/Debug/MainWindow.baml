   M S B A M L   `   `   `  ����M  ILucky13_Milestone2, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null7 clr-namespace:Lucky13_Milestone2Lucky13_Milestone2  5   	   #    Lucky13_Milestone2.MainWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   2local clr-namespace:Lucky13_Milestone2     5   	     � Title$  
MainWindow��5   	   $��614.03��6   $��1204.814��6,    � WindowStartupLocation$ CenterScreen=�6=   .��5	      � .+�5
   
   �� -   5         ��Name$ 
tabControl��5
      $
��Left=�6)   $	��568��6D   $	��Top=�6Q   $
��1181��6i   $��	10,10,0,0q�6v   .@�5      �� $��User Information��6   .��5      � ���	#FFE5E5E5.+�5      �� -   5         ��Name$ setCurrentUserGroupBox��5      $��Set Current User��6?   $
��Left=�6Y   $	��290��6t   $��	10,10,0,0q�6�   $	��Top=�6�   $	��235��6�   �� 5      -   5         ��Name$ inputUserTextBox��5      $
��Left=�68   $��22��6S   $��	20,32,0,0q�6_    �� TextWrapping$
 Wrap=�6r   $�� ��6�   $	��Top=�6�   $	��145��6�   6�   �� 5      -   5         ��Name$ listBox��5      $
��Left=�6/   $	��227��6J   $��	20,62,0,0q�6W   $	��Top=�6j   $	��210��6�   6�   �� 5      -   5        $ 	groupBox1��5      $��Friends��62   $
��Left=�6C   $	��217��6^   $��	7,305,0,0q�6k   $	��Top=�6~   $	��514��6�   .��5      &   System.Windows.Controls.DataGrid  -   5          Name$ friendsDataGrid��5   #   $
��Left=�6<   $	��228��6W   $��6,4,0,0q�6d   $	��Top=�6u   $	��488��6�   5      �� 5      -   5        $ groupBox��5      $��User Information��61   $
��Left=�6K   $	��290��6f   $��
252,10,0,0q�6s   $	��Top=�6�   $	��269��6�   .��5      �� -   5         ��Name$ label1��5       $��Fans:��60   $
��Left=�6@   $��30��6[   $��
131,24,0,0q�6g   $	��Top=�6{   $��41��6�   5      �� 5      -	   5        $ nameTextBox��5      $
��Left=�63   $��22��6N   $��
313,29,0,0q�6Z   $
 Wrap=�6n   $�� ��6�   $	��Top=�6�   $	��186��6�   	 �� 
IsReadOnly	 . 6�   �� 5      -
   5        $ label��5      $��Stars:��6+   $
��Left=�6<   $��
272,52,0,0q�6W   $	��Top=�6k   �� 5      -   5        $ starsTextBox��5      $
��Left=�64   $��22��6O   $��
313,55,0,0q�6[   $
 Wrap=�6o   $�� ��6�   $	��Top=�6�   $��63��6�   	 . 6�   �� 5      -   5        $ fansTextBox��5      $
��Left=�63   $��22��6N   $��
429,54,0,0q�6Z   $
 Wrap=�6n   $�� ��6�   $	��Top=�6�   $��70��6�   	 . 6�   �� 5      -   5        $ label2��5      $��Name:��6,   $
��Left=�6<   $��25��6W   $��
270,26,0,0q�6c   $	��Top=�6w   $��72��6�   �� 5      -   5        $ yelpSinceTxt��5      $
��Left=�64   $��22��6O   $��
359,79,0,0q�6[   $
 Wrap=�6o   $�� ��6�   $	��Top=�6�   $	��140��6�   	 . 6�   �� 5      -   5        $ label3��5      $��Yelping Since:��6,   $
��Left=�6E   $��37��6`   $��
270,76,0,0q�6l   $	��Top=�6�   $	��188��6�   �� 5      -   5        $ label4��5      $+��%      Funny:      Cool:       Useful:��6,   $
��Left=�6\   $��28��6w   $��
282,95,0,0q�6�   $	��Top=�6�   $	��212��6�   �� 5      -   5        $ funnyTxt��5      $
��Left=�60   $��22��6K   $��310,119,0,0q�6W   $
 Wrap=�6l   $�� ��6�   $	��Top=�6�   $��28��6�   
 J� RenderTransformOrigin$
 -0.397,4.421&�6�   	 . 6�   �� 5      -   5        $ coolTxt��5      $
��Left=�6/   $��22��6J   $��362,119,0,0q�6V   $
 Wrap=�6k   $�� ��6   $	��Top=�6�   $��28��6�   $
 -0.397,4.421&�6�   	 . 6�   �� 5       -   5        $ 	usefulTxt��5       $
��Left=�61   $��22��6L   $��414,119,0,0q�6X   $
 Wrap=�6m   $�� ��6�   $	��Top=�6�   $��28��6�   $
 -0.397,4.421&�6�   	 . 6�   �� 5!      -   5        $ label5��5!      $��
Tip Count:��6,   $
��Left=�6A   $��38��6\   $��274,141,0,0q�6h   $	��Top=�6}   $��63��6�   �� 5"      -   5        $ tipCountTxt��5"      $
��Left=�63   $��22��6N   $��363,146,0,0q�6Z   $
 Wrap=�6o   $�� ��6�   $	��Top=�6�   $	��111��6�   	 . 6�   �� 5#      -   5        $ label5_Copy��5#      $��Total Tip Likes:��61   $
��Left=�6L   $��38��6g   $��273,165,0,0q�6s   $	��Top=�6�   $	��108��6�   �� 5$      -   5        $ totalTipLikesTxt��5$      $
��Left=�68   $��22��6S   $��363,170,0,0q�6_   $
 Wrap=�6t   $�� ��6�   $	��Top=�6�   $	��111��6�   	 . 6�   �� 5%      -   5        $ label5_Copy1��5%      $
��Lat:��62   $
��Left=�6A   $��38��6\   $��284,207,0,0q�6h   $	��Top=�6}   $��63��6�   �� 5&      -   5        $ latTxt��5&      $
��Left=�6.   $��22��6I   $��332,209,0,0q�6U   $
 Wrap=�6j   $�� ��6~   $	��Top=�6�   $	��111��6�   	 . 6�   �� 5'      -   5        $ 	longLabel��5'      $��Long:��6/   $
��Left=�6?   $��38��6Z   $��283,231,0,0q�6f   $	��Top=�6{   $	��108��6�   �� 5(      -   5        $ longTxt��5(      $
��Left=�6/   $��22��6J   $��332,236,0,0q�6V   $
 Wrap=�6k   $�� ��6   $	��Top=�6�   $	��111��6�   	 . 6�   �� 5)      -   5        $ label6��5)      $��	Location:��6,   $
��Left=�6@   $��24��6[   $��272,191,0,0q�6g   $	��Top=�6|   $��89��6�   �� 5*      -   5         ��Name$ editLocationButton��5*      $
��Edit��69   $
��Left=�6H   $��24��6c   $��455,0,0,310q�6o   $��Bottom=�6�   $��54��6�   6�   �� 5+      -   5        $ updateLocationButton��5+      $��Update��6;   $
��Left=�6L   $��24��6g   $��455,236,0,0q�6s   $	��Top=�6�   $��54��6�   6�   �� 5,      -   5        $ 	groupBox2��5,      $ ��Latest tips of my friends?��62   $
��Left=�6V   $	��512��6q   $��
533,10,0,0q�6~   $	��Top=�6�   $	��632��6�   .��5-        -    5        $ friendsTipsDataGrid��5-   #   $
��Left=�6@   $	��475��6[   $��	12,10,0,0q�6h   $	��Top=�6{   $	��599��6�   5.      �� 5/      -!   5        $! friendRecommendationsButton��5/      $!��View Friend Recommendations��6B   $
��Left=�6h   $��24��6�   $��284,0,0,251q�6�   $��Bottom=�6�   $	��190��6�   6�   50      51      �� 52      $��Business Search��6   .��53      � ���	#FFE5E5E5.+�54      �� -"   5        $ 	groupBox3��54      $��Select Location��62   $
��Left=�6K   $	��139��6f   $��7,22,0,0q�6s   $	��Top=�6�   $	��218��6�   �� 55      -#   5         ��Name$ 	StateList��55      $
��Left=�62   $��	60,50,0,0q�6M   $	��Top=�6`   $��62��6x   $��14(�6�   $
 0.325,0.581&�6�   �� 56      -$   5        $ State��56      $��State��6+   $
��Left=�6;   $��	12,46,0,0q�6V   $	��Top=�6i   $��14(�6�   $
 -0.286,0.144&�6�   �� 57      -%   5        $ CityList��57      $
��Left=�61   $��	60,80,0,0q�6L   $	��Top=�6_   $	��150��6w   $��14(�6�   $��29��6�   �� 58      -&   5        $
 City��58      $
��City��6*   $
��Left=�69   $��	15,80,0,0q�6T   $	��Top=�6g   $��14(�6   $
 2.777,2.051&�6�   �� 59      -'   5        $ ZipList��59      $
��Left=�60   $��
60,116,0,0q�6K   $	��Top=�6_   $��89��6w   $��14(�6�   $��25��6�   �� 5:      -(   5        $	 Zip��5:      $	��Zip��6)   $
��Left=�67   $��
17,112,0,0q�6R   $	��Top=�6f   $��14(�6~   $
 3.092,-4.29&�6�   �� 5;      -)   5        $ 	groupBox4��5;      $��Business Category��62   $
��Left=�6M   $	��356��6h   $��	7,166,0,0q�6u   $	��Top=�6�   $	��218��6�   �� 5<      -*   5        $ categorylistBox��5<      $
��Left=�67   $	��165��6R   $��
17,188,0,0q�6_   $	��Top=�6s   $	��193��6�   6�   �� 5=      -+   5        $ addCatButton��5=      $	��Add��63   $
��Left=�6A   $��22��6\   $��
47,358,0,0q�6h   $	��Top=�6|   $��61��6�   6�   �� 5>      -,   5        $ removeCatButton��5>      $��Remove��66   $
��Left=�6G   $��22��6b   $��113,358,0,0q�6n   $	��Top=�6�   $��61��6�   6�   �� 5?      --   5        $ categorySelectedListBox��5?      $
��Left=�6?   $	��100��6Z   $��
17,385,0,0q�6g   $	��Top=�6{   $	��193��6�   6�   �� 5@      -.   5        $ searchBusinessesButton��5@      $��Search Businesses��6=   $
��Left=�6Y   $��22��6t   $��
56,490,0,0q�6�   $	��Top=�6�   $	��110��6�   6�   �� 5A      -/   5        $ 	groupBox5��5A      $��SEARCH RESULTS��62   $
��Left=�6J   $	��331��6e   $��
252,22,0,0q�6r   $	��Top=�6�   $	��601��6�     5B      -0   5        $ businessGrid��5B      $
��Left=�65   $	��291��6P   $��
269,46,0,0q�6]   $	��Top=�6q   $	��568��6�   $��10(�6�   6�   �� 5C      -1   5        $ numOfBusinesses��5C      $��# of businesses: 0��65   $
��Left=�6R   $��32��6m   $��	691,3,0,0q�6y   $	��Top=�6�   $	��146��6�   �� 5D      -2   5        $ 	groupBox6��5D      $��SELECTED BUSINESS��62   $
��Left=�6M   $	��172��6h   $��254,358,0,0q�6u   $	��Top=�6�   $	��911��6�   .��5E      �� -3   5         ��Name$ BusNameTextBlock��5E   $   $
��Left=�6>   $��29��6Y   $��	14,13,0,0q�6e   $
��Wrap=�6x   $�� ��6�   $	��Top=�6�   $	��452��6�   5F      �� 5G      -4   5        $ addresseBusTextBlock��5G       $
��Left=�6>   $��36��6Y   $��275,416,0,0q�6e   $
��Wrap=�6z   $�� ��6�   $	��Top=�6�   $	��485��6�   �� 5H      -5   5        $ hoursBusTextBlock��5H       $
��Left=�6;   $��41��6V   $��275,446,0,0q�6b   $
��Wrap=�6w   $�� ��6�   $	��Top=�6�   $	��451��6�   �� 5I      -6   5        $ showReviewsButton��5I      $��Show Reviews��68   $
��Left=�6O   $��32��6j   $��752,395,0,0q�6v   $	��Top=�6�   $	��118��6�   6�   �� 5J      -7   5        $ showTipsButton��5J      $��	Show Tips��65   $
��Left=�6I   $��32��6d   $��752,457,0,0q�6p   $	��Top=�6�   $	��118��6�   6�   �� 5K      -8   5        $ 	groupBox7��5K      $��Filter by Price:��62   $
��Left=�6L   $��74��6g   $��
870,22,0,0q�6s   $	��Top=�6�   $	��295��6�   �� 5L      -9   5         ��Name$ twoMoneyBox��5L      $��CheckBox��64   $
��Left=�6G   $��17��6b   $��
898,69,0,0q�6n   $	��Top=�6�   $��16��6�   6�   �� 5M      -:   5        $ label7��5M      $��$$��6,   $
��Left=�69   $��32��6T   $��
916,62,0,0q�6`   $	��Top=�6t   $��32��6�   �� 5N      -;   5        $ oneMoneyBox��5N      $��CheckBox��64   $
��Left=�6G   $��16��6b   $��
898,48,0,0q�6n   $	��Top=�6�   $��18��6�   6�   �� 5O      -<   5        $ label7_Copy��5O      $��$��61   $
��Left=�6=   $��32��6X   $��
916,43,0,0q�6d   $	��Top=�6x   $��32��6�   $
 0.672,0.552&�6�   �� 5P      -=   5        $ threeMoneyBox��5P      $��CheckBox��66   $
��Left=�6I   $��15��6d   $��
982,48,0,0q�6p   $	��Top=�6�   $��18��6�   6�   �� 5Q      ->   5        $ label7_Copy1��5Q      $	��$$$��62   $
��Left=�6@   $��32��6[   $��1000,43,0,0q�6g   $	��Top=�6|   $��32��6�   �� 5R      -?   5        $ fourMoneyBox��5R      $��CheckBox��65   $
��Left=�6H   $��16��6c   $��
982,70,0,0q�6o   $	��Top=�6�   $��18��6�   6�   �� 5S      -@   5        $ label7_Copy2��5S      $
��$$$$��62   $
��Left=�6A   $��32��6\   $��1000,63,0,0q�6h   $	��Top=�6}   $��56��6�   �� 5T      -A   5        $ 	groupBox8��5T      $��Filter by Attributes:��62   $
��Left=�6Q   $	��186��6l   $��870,101,0,0q�6y   $	��Top=�6�   $	��295��6�   �� 5U      -B   5        $ label8��5U      $��Accepts Credit Cards��6,   $
��Left=�6K   $��25��6f   $��907,116,0,0q�6r   $	��Top=�6�   $	��132��6�   $��10(�6�   �� 5V      -C   5        $ label8_Copy��5V      $��Takes Reservations ��61   $
��Left=�6O   $��27��6j   $��906,132,0,0q�6v   $	��Top=�6�   $	��132��6�   $��10(�6�   �� 5W      -D   5        $ label8_Copy1��5W      $��Wheelchair Accessible ��62   $
��Left=�6S   $��27��6n   $��906,148,0,0q�6z   $	��Top=�6�   $	��132��6�   $��10(�6�   �� 5X      -E   5        $ acceptsCardBox��5X      $��CheckBox��67   $
��Left=�6J   $��14��6e   $��885,120,0,0q�6q   $	��Top=�6�   $��16��6�   6�   �� 5Y      -F   5        $ takesReservBox��5Y      $��CheckBox��67   $
��Left=�6J   $��14��6e   $��885,136,0,0q�6q   $	��Top=�6�   $��16��6�   6�   �� 5Z      -G   5        $ wheelchairBox��5Z      $��CheckBox��66   $
��Left=�6I   $��14��6d   $��885,152,0,0q�6p   $	��Top=�6�   $��16��6�   6�   �� 5[      -H   5        $ label8_Copy2��5[      $��Outdoor Seating ��62   $
��Left=�6M   $��27��6h   $��906,164,0,0q�6t   $	��Top=�6�   $	��132��6�   $��10(�6�   �� 5\      -I   5        $ outdoorSeatingBox��5\      $��CheckBox��6:   $
��Left=�6M   $��14��6h   $��885,168,0,0q�6t   $	��Top=�6�   $��16��6�   6�   �� 5]      -J   5        $ label8_Copy3��5]      $��Good for Kids��62   $
��Left=�6J   $��27��6e   $��906,180,0,0q�6q   $	��Top=�6�   $	��132��6�   $��10(�6�   �� 5^      -K   5        $ kidsBox��5^      $��CheckBox��60   $
��Left=�6C   $��14��6^   $��885,184,0,0q�6j   $	��Top=�6   $��16��6�   6�   �� 5_      -L   5        $ label8_Copy4��5_      $��Good for Groups��62   $
��Left=�6L   $��27��6g   $��906,196,0,0q�6s   $	��Top=�6�   $	��132��6�   $��10(�6�   �� 5`      -M   5        $ 	groupsBox��5`      $��CheckBox��62   $
��Left=�6E   $��14��6`   $��885,200,0,0q�6l   $	��Top=�6�   $��16��6�   6�   �� 5a      -N   5        $ label8_Copy5��5a      $��Delivery��62   $
��Left=�6E   $��27��6`   $��906,212,0,0q�6l   $	��Top=�6�   $	��132��6�   $��10(�6�   �� 5b      -O   5        $ deliveryBox��5b      $��CheckBox��64   $
��Left=�6G   $��14��6b   $��885,216,0,0q�6n   $	��Top=�6�   $��16��6�   6�   �� 5c      -P   5        $ label8_Copy6��5c      $��Take Out��62   $
��Left=�6E   $��27��6`   $��906,228,0,0q�6l   $	��Top=�6�   $	��132��6�   $��10(�6�   �� 5d      -Q   5        $ 
takeOutBox��5d      $��CheckBox��63   $
��Left=�6F   $��14��6a   $��885,232,0,0q�6m   $	��Top=�6�   $��16��6�   6�   �� 5e      -R   5        $ label8_Copy7��5e      $��
Free Wi-Fi��62   $
��Left=�6G   $��27��6b   $��906,244,0,0q�6n   $	��Top=�6�   $	��132��6�   $��10(�6�   �� 5f      -S   5        $ wifiBox��5f      $��CheckBox��60   $
��Left=�6C   $��14��6^   $��885,248,0,0q�6j   $	��Top=�6   $��16��6�   6�   �� 5g      -T   5        $ label8_Copy8��5g      $��Bike Parking��62   $
��Left=�6I   $��27��6d   $��906,260,0,0q�6p   $	��Top=�6�   $	��132��6�   $��10(�6�   �� 5h      -U   5        $ bikeParkingBox��5h      $��CheckBox��67   $
��Left=�6J   $��14��6e   $��885,264,0,0q�6q   $	��Top=�6�   $��16��6�   6�   �� 5i      -V   5        $$ selectedBusinessDetailsListBox��5i      $
��Left=�6F   $	��145��6a   $��885,376,0,0q�6n   $	��Top=�6�   $	��272��6�   �� 5j      -W   5        $ 
groupBox10��5j      $��Sort Results By:��63   $
��Left=�6M   $��66��6h   $��873,287,0,0q�6t   $	��Top=�6�   $	��292��6�   .��5k      �� -X   5        $ sortResultsList��5k   #   $
��Left=�6<   $��29��6W   $��15,8,0,0q�6c   $	��Top=�6u   $	��247��6�   6�   5l      5m      5n      5o      5q      5r      