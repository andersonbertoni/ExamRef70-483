using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace RandomAverages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
        }

        private double computeAverages(long noOfValues)
        {
            double total = 0;
            Random rand = new Random();

            for(long values = 0; values < noOfValues; values++)
            {
                total = total + rand.NextDouble();
            }

            return total / noOfValues;
        }

        /* Code without task (large number of averages causes the entire user interface to lock up while the program runs
         * the event handler behind the "Start" button */
        /*private void StartButton_Click(object sender, RoutedEventArgs e)
        {
            long noOfValues = long.Parse(NumberOfValuesTextBox.Text);
            ResultTextBlock.Text = "Result: " + computeAverages(noOfValues);
        } */

        /* Code executed with task. This code is correct from a task management point of view, but it will fail when it runs.
         * This is because interaction with display components is strictly managed by the process that generates the display. */
        /*private void StartButton_Click(object sender, RoutedEventArgs e)
        {
            long noOfValues = long.Parse(NumberOfValuesTextBox.Text);
            Task.Run(() =>
            {
                ResultTextBlock.Text = "Result: " + computeAverages(noOfValues);
            });            
        }*/

        /* Each component on a display has a Dispatcher property that can be used to run tasks in the context of the display.
         * The RunAsync method is given a priority level for the task, followed by the action that is to be performed on the thread.
         * Note that the code does not show good programming practice. The RunAsync method is designed to be called asynchronously. The code does not do this,
         * which will result in compiler warnings being produced when you build the example. */
        /*private void StartButton_Click(object sender, RoutedEventArgs e)
        {
            long noOfValues = long.Parse(NumberOfValuesTextBox.Text);
            Task.Run(() =>
            {
                double result = computeAverages(noOfValues);
                ResultTextBlock.Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, () =>
                {
                    ResultTextBlock.Text = "Result: " + result.ToString();
                });
                
            });
        }*/

        private Task<double> asyncComputeAverages(long noOfValues)
        {
            return Task<double>.Run(() =>
            {
                return computeAverages(noOfValues);
            });
        }

        private async void StartButton_Click(object sender, RoutedEventArgs e)
        {
            long noOfValues = long.Parse(NumberOfValuesTextBox.Text);
            ResultTextBlock.Text = "Calculating";

            double result = await asyncComputeAverages(noOfValues);
            ResultTextBlock.Text = "Result: " + result.ToString();                
        }
    }
}
