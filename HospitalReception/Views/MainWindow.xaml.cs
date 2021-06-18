using HospitalReception.Views;
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

namespace HospitalReception
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : NavigationWindow
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        private void NavigateDoctors(object sender, EventArgs e)
        {
            NavigationService.Navigate(new DoctorsPage());
        }
        private void NavigatePatients(object sender, EventArgs e)
        {
            NavigationService.Navigate(new PatientsPage());
        }
        private void NavigateDoctorsSearch(object sender, EventArgs e)
        {
            NavigationService.Navigate(new DoctorSearchPage());
        }
        private void NavigatePatientsSearch(object sender, EventArgs e)
        {
            NavigationService.Navigate(new PatientSearchPage());
        }

        private void NavigateNewAppointment(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AppointmentPage());
        } 
        private void NavigateSchedule(object sender, RoutedEventArgs e)
        {

        }
        private void NavigateHistory(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new PatientsHistoryPage());
        }
    }
}