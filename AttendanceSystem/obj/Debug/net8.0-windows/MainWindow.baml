   M S B A M L   `   `   `  ����  AttendanceSystem!    AttendanceSystem.MainWindow   5   	   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	     � Title$  Attendance System��5   	   $	��450��6#   $	��800��60    � 	Resources 5          BooleanToVisibilityConverter �� 
TargetType �� Property �� Value&   System.Windows.Controls.DataGrid=  7System.Windows.Controls.Primitives.DataGridColumnHeader��%�  &	        '��      '�� �     '  M    '  �    �� 5   
   6'   �� 5   
   " ��6   .�5      ��  � ��$ 5q�6'   �� 5	       � ��$ 5q�6(   �� 5
       � ��$ 14(�6)   �� 5       � �� �	#FF007ACC6,   �� 5       � ��$ 2q�60   5      �� 5   
   " ��6   .�5      ��  � ��$ 5q�6'   �� 5       � ��$ 10q�6(   �� 5       � ��$ 14(�6)   �� 5       � �� �	#FF007ACC6+   �� 5       � ��
 �����6+   �� 5       � �� �	#FF007ACC6,   �� 5       � ��$ 2q�60   �� 5       �@�Cursor$
 Hand��6'   5      �� 5   
   "  6   .�5      ��  � ��$ 10q�6'   �� 5       � ��$ 14(�6)   �� 5       �@ RowBackground �	#FFE6F7FF6.   �� 5        �@ AlternatingRowBackground �	#FFD6EFFF69   �� 5       �@ GridLinesVisibility$
 None=�64   �� 5       � �� �	#FF007ACC6,   �� 5       � ��$ 2q�60   5       �� 5!   
   "  6   .�5"      ��  � �� �	#FF007ACC6+   �� 5#       � ��
 �����6+   �� 5$       � ��$ 14(�6)   �� 5%       � ��$ 10q�6(   5&      5'      .��5(      � ���	#FFF0F4F8 � RowDefinitions 5)   
   �� 5*      $
��Auto �6   �� 5+      $��* �6   .+�5-   
   ��  �� Orientation$ 
Horizontal=�6   $��10q�6.   .+�5.      � $��5q�6   .+�5/      �� -   5         ��Name$ txtName��5/      $	��200��6+   $��Center=�67   �� 50      $��Enter your name��6   
�������$��Center=�52      $��10,0,0,0q�53       J� IsHitTestVisible$ False��54      y�55      �� *Text.IsEmpty+	 �� ElementName$	 txtName��
 �� 	Converter#
 [  56      �� 57      -   5         ��Name$ btnMarkTimeIn��57      $��Mark Time In��6,   6C   �� 58      -   5        $ btnMarkTimeOut��58      $��Mark Time Out��6-   6E   �� 59      -   5        $ btnOpenAdminDashboard��59      $��Admin Dashboard��64   6N   5:        5;   
   -   5          Name$ dataGridAttendance��5;      $��1��6/      AutoGenerateColumns$ False��6<      Columns 5<      0  *System.Windows.Controls.DataGridTextColumn  5=      ,  &System.Windows.Controls.DataGridColumn   Header$ Employee Name��6%      Width5  /System.Windows.Controls.DataGridLengthConverter$ * 6Z   1  +System.Windows.Controls.DataGridBoundColumn   Binding 6<   �� *	StaffName+6Z     5>      $ Time In��6%   $ * 6Q    66   �� *TimeIn+6Q     5?      $ Time Out��6%   $ * 6S    67   �� *	TimeOut+6S   5A      5B      5C      