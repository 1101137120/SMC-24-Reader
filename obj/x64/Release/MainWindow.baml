   M S B A M L   `   `   `  ����   SMC-24-Reader    SMC_24_Reader.MainWindow   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml       � Title$  SMC-ARB24-V3.0.1��$��697.902��$	��800��.��� .+�� -   $��grid1��$��36��$��Stretch=�$	��Top=�$
��Auto��.+��� -   $��label1��$��
Com Port :��$��36��$
��Left=�$��0,2,0,0q�$	��Top=�$��Center=��� -   $��
cb_ComPort��$��23��$
��Left=�$��66,7,0,0q�$	��Top=�$	��120���� -   $��label2��$��
Baudrate :��$��36��$
��Left=�$��	202,2,0,0q�$	��Top=�$��Center=��� -   $��cb_Baudrate��$��23��$
��Left=�$��	265,7,0,0q�$	��Top=�$	��120�� �� SelectedIndex$ 0��.@��� $
��9600���� $��14400���� $��19200���� $��28800���� $��38400���� $��57600���� $��115200���� $
��4800���� -   $��lb_LinkStatus��$��Link fail !��$��36��$
��Left=�$��	401,2,0,0q�$	��Top=�$��Center=�
���  ���� -    ��Name$ setPollingTime��$��設定時間��$
��Left=�$��	633,7,0,0q�$	��Top=�$��75���� -    ��Name$ label10��$��Tag Lost Time��$��41��$
��Left=�$��9,41,0,0q�$	��Top=�$��Center=��� -	    ��Name$ 
lv_ListTag��$
��Auto��$
��Left=�$��	9,91,0,12q�$��Stretch=�$	��212��$��16(��� -
   $ label11��$��R/W Tag��$��41��$
��Left=�$��
239,41,0,0q�$	��Top=�$��Center=��� -    ��Name$ 	tb_RW_Tag��$
��Auto��$��Stretch=�$��237,91,17,12q�$��Stretch=�$
��Auto��{�. $ �� HorizontalScrollBarVisibility$
 Auto=�" �� VerticalScrollBarVisibility$
 Auto=� �� 
IsReadOnly . �� -   $ bt_Clear_RWTag��$��Clear��$��33��$��
629,48,0,0q�$	��Top=�	 J� RenderTransformOrigin$	 -0.069,0.491&�$
��Left=�$��75���� -   $ bt_ReadTagIdBuffer��$��Read Tag ID From Buffer��$��41��$
��Left=�$��
308,41,0,0q�$	��Top=�$	��156���� -   
 ��Name$
 chb_IsAutoReadTag��$��Is Auto Read Tag��$��23��$
��Left=�$��
477,53,0,0q�$	��Top=�$��16(��� -   $ bt_ClearListTag��$��Clear��$��33��$
��Left=�$��
146,41,0,0q�$	��Top=�$��75���� -    ��Name$ cb_TagLostTime��$��33��$
��Left=�$��	92,41,0,0q�$	��Top=�$��48��$ 2��.@��� $��3���� $��4���� $��5���� $��6���� $��7���� $��8���� $��9���� $��10���� $��11���� -   $ label9��$��輪詢頻率(MS)��$��28��$
��Left=�$��	474,6,0,0q�$	��Top=��� -   $ textBoxPollingRate��$��25��$
��Left=�$��	562,7,0,0q�$	��Top=�$��60��$	��100��