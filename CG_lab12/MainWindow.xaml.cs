using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace CG_lab12
{
    // Создайте анимированную модель 3D шкафа, с возможностью открывания дверей и выдвижения ящиков.
    public partial class MainWindow : Window
    {
        private bool ldOpen, rdOpen, udOpen, bdOpen = false;
        DispatcherTimer timer_ld, timer_rd, timer_ud, timer_bd;

        public MainWindow()
        {
            InitializeComponent();

            timer_ld = new DispatcherTimer(); 
            timer_rd = new DispatcherTimer();
            timer_ud = new DispatcherTimer();
            timer_bd = new DispatcherTimer();
            timer_ld.Tick += new EventHandler(timer_ld_Tick);
            timer_rd.Tick += new EventHandler(timer_rd_Tick);
            timer_ud.Tick += new EventHandler(timer_ud_Tick);
            timer_bd.Tick += new EventHandler(timer_bd_Tick);
            timer_ld.Interval = new TimeSpan(100000);
            timer_rd.Interval = new TimeSpan(100000);
            timer_ud.Interval = new TimeSpan(100000);
            timer_bd.Interval = new TimeSpan(100000);
        }

        private void buttonLeftDoor_Click(object sender, RoutedEventArgs e)
        {
/*            if (!ldOpen)
                lDoorOffset.OffsetX = -2;
            else
                lDoorOffset.OffsetX = 0;*/
            ldOpen = !ldOpen;
            timer_ld.Start();
        }

        private void buttonRightDoor_Click(object sender, RoutedEventArgs e)
        {
/*            if (!rdOpen)
                rDoorOffset.OffsetX = 2;
            else
                rDoorOffset.OffsetX = 0;*/
            rdOpen = !rdOpen;
            timer_rd.Start();
        }

        private void buttonUpperDrawer_Click(object sender, RoutedEventArgs e)
        {
            udOpen = !udOpen;
            timer_ud.Start();
        }

        private void buttonBottomDrawer_Click(object sender, RoutedEventArgs e)
        {
/*            double _newZ;
            if (!bdOpen)
                _newZ = 2;
            else
                _newZ = 0;

            bDrawerZBottom.OffsetZ = _newZ;
            bDrawerZFront.OffsetZ = _newZ;
            bDrawerZLeft.OffsetZ = _newZ;
            bDrawerZRight.OffsetZ = _newZ;*/

            bdOpen = !bdOpen;
            timer_bd.Start();
        }

        private void timer_ld_Tick(object sender, EventArgs e)
        {
            double target_ld_x = ldOpen ? -2 : 0;
            double cur_ld_x = lDoorOffset.OffsetX;

            if (target_ld_x != cur_ld_x)
            {
                cur_ld_x = lerp(cur_ld_x, target_ld_x, 0.1);
                lDoorOffset.OffsetX = cur_ld_x;
            }
            else
                timer_ld.Stop();
        }

        private void timer_rd_Tick(object sender, EventArgs e)
        {
            double target_rd_x = rdOpen ? 2 : 0;
            double cur_rd_x = rDoorOffset.OffsetX;

            if (target_rd_x != cur_rd_x)
            {
                cur_rd_x = lerp(cur_rd_x, target_rd_x, 0.1);
                rDoorOffset.OffsetX = cur_rd_x;
            }
            else
                timer_rd.Stop();
        }

        private void timer_ud_Tick(object sender, EventArgs e)
        {
            double target_ud_z = udOpen ? 2 : 0;
            double cur_ud_z = uDrawerZBottom.OffsetZ;

            if (target_ud_z != cur_ud_z)
            {
                cur_ud_z = lerp(cur_ud_z, target_ud_z, 0.1);
                uDrawerZBottom.OffsetZ = cur_ud_z;
                uDrawerZFront.OffsetZ = cur_ud_z;
                uDrawerZLeft.OffsetZ = cur_ud_z;
                uDrawerZRight.OffsetZ = cur_ud_z;
            }
            else
                timer_ud.Stop();
        }

            private void timer_bd_Tick(object sender, EventArgs e)
        {
            double target_bd_z = bdOpen ? 2 : 0;
            double cur_bd_z = bDrawerZBottom.OffsetZ;

            if (target_bd_z != cur_bd_z)
            {
                cur_bd_z = lerp(cur_bd_z, target_bd_z, 0.1);
                bDrawerZBottom.OffsetZ = cur_bd_z;
                bDrawerZFront.OffsetZ = cur_bd_z;
                bDrawerZLeft.OffsetZ = cur_bd_z;
                bDrawerZRight.OffsetZ = cur_bd_z;
            }
            else
                timer_bd.Stop();
        }

        private double lerp(double point_first, double point_second, double by)
        {
            return point_first * (1 - by) + point_second * by;
        }
    }
}
