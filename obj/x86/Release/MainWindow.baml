   M S B A M L   `   `   `  ����   SMC-24-Reader    SMC_24_Reader.MainWindow   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml       � Title$  SMC AR24��$	��600��$	��800��.��� .+�� -   $��grid1��$��36��$��Stretch=�$	��Top=�$
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
���  ���� -   $��label8��$��Reader ID :��$��36��$
��Left=�$��	474,1,0,0q�$	��Top=�$��Center=��� -   $��cb_ReaderId��$��23��$
��Left=�$��	543,7,0,0q�$	��Top=�$	��120���� -	   $��
cb_IsRs485��$��Is RS485��$��16��$
��Left=�$��
669,11,0,0q�$	��Top=� d� 	IsChecked$
 TrueY��� -
   $��tabControl1��$
��Auto��$��Stretch=�$��0,42,0,0q�$��Stretch=�$
��Auto��.@��� -   $��tabItem1��$��Reader��$	��120��$��36��.��� .+��� -   $��label3��$��Parameter :��$��28��$
��Left=�$��6,6,0,0q�$	��Top=�$��Center=��� -   $��cb_ReaderParameter��$��23��$
��Left=�$��74,6,0,0q�$	��Top=�$	��120���� -   $��label4��$��Value :��$��28��$
��Left=�$��6,40,0,0q�$	��Top=�$��Center=��� -   $��cb_ReaderParameterValue��$��23��$
��Left=�$��	74,40,0,0q�$	��Top=�$	��120���� -   $��bt_ReadReaderParameter��$��Read Reader Parameter��$��62��$
��Left=�$��	224,6,0,0q�$	��Top=�$	��156���� -   $��bt_WriteReaderParameter��$��Write Reader Parameter��$��62��$
��Left=�$��	412,6,0,0q�$	��Top=�$	��156���� -   $��label5��$#��Read / Write Reader parameter��$��28��$
��Left=�$��6,84,0,0q�$	��Top=��� -   $��tb_RW_R_Parameter��$
��Auto��$��Stretch=�$��	6,106,6,6q�$��Stretch=�$
��Auto���� -   $��bt_ReadTagIdBuffer��$��Read Tag ID Buffer��$��62��$
��Left=�$��	600,6,0,0q�$	��Top=�$	��156���� $	��Tag��$	��120��$��36��.��� $��Stretch=�$��Stretch=�.+��� -   $��label6��$��Parameter :��$��28��$
��Left=�$��	10,10,0,0q�$	��Top=�$��Center=��� -   $��cb_TagParameter��$��23��$
��Left=�$��	78,10,0,0q�$	��Top=�$	��120���� -   $��label7��$��Value :��$��28��$
��Left=�$��	10,44,0,0q�$	��Top=�$��Center=��� -   $��cb_TagParameterValue��$��23��$
��Left=�$��	78,44,0,0q�$	��Top=�$	��120���� -   $��bt_ReadTagParameter��$��Read Tag Parameter��$��62��$
��Left=�$��
228,10,0,0q�$	��Top=�$	��156���� -   $��bt_WriteTagParameter��$��Write Tag Parameter��$��62��$
��Left=�$��
416,10,0,0q�$	��Top=�$	��156���� -   $��bt_SetTagLedBlinking��$��Set Tag LED Blinking��$��62��$
��Left=�$��
600,10,0,0q�$	��Top=�$	��156��